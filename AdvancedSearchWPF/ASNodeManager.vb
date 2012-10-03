Imports System.Collections.ObjectModel
Imports AdvancedSearchDataDefinitions

Public Class ASNodeManager(Of NodeT, LineT)

    Dim _isLoaded As Boolean = False
    Dim _ASNodeConfiguration As ASConfiguration = Nothing
    Dim _nodeList As New List(Of IASNode)
    Dim _nodeListTop As New Dictionary(Of NodeT, List(Of LineT))
    Dim _nodeListBot As New Dictionary(Of NodeT, List(Of LineT))

    Public Sub New()
        Dim fac = New IDDNodeFactory
        _ASNodeConfiguration = fac.GetASNodeConfiguration()
    End Sub

#Region "Public Properties"

    Public ReadOnly Property NodeListTop As Dictionary(Of NodeT, List(Of LineT))
        Get
            Return _nodeListTop
        End Get
    End Property

    Public ReadOnly Property NodeListBot As Dictionary(Of NodeT, List(Of LineT))
        Get
            Return _nodeListBot
        End Get
    End Property

    Public ReadOnly Property NodeList As ReadOnlyCollection(Of IASNode)
        Get
            Return New ReadOnlyCollection(Of IASNode)(_nodeList)
        End Get
    End Property

    Public ReadOnly Property MyASNodeConfiguration As ASConfiguration
        Get
            Return _ASNodeConfiguration
        End Get
    End Property
#End Region

#Region "Public Methods"
    Dim _selectedNode As IASNode = Nothing
    Public Sub SetSelectedNode(ByVal node As IASNode)
        _selectedNode = node
    End Sub

    Public Function GetColumnItemSource() As Object
        If _selectedNode Is Nothing Then
            Return Nothing
        End If
        UpdateColumnChooserModel(_selectedNode)
        Return _selectedNode.TableColumnChooserList.OrderBy(Function(n) n.NodeTag.GetBreadthFirstIndex())
    End Function

    Public Function GetCriteriaItemSource() As Object
        If _selectedNode Is Nothing Then
            Return Nothing
        End If
        Return _selectedNode.NodeCriteriaList
    End Function

    Public Function CanAddNodeType(ByVal type As DDNodeEnum) As Boolean
        Return _ASNodeConfiguration.ASNodeConfigList.FirstOrDefault(Function(node) node.NodeType = type) IsNot Nothing
    End Function

    Public Function AddNodeType(ByVal type As Integer, ByVal tag As Object) As IASNode
        Dim nodeID As Integer = 1
        If _nodeList.Count > 0 Then
            nodeID = _nodeList.Max(Function(x) x.NodeID) + 1
        End If

        Dim node = CreateNode(type, nodeID, tag)
        _nodeList.Add(node)

        Return node
    End Function

    Public Function CanDeleteNode(ByVal node As IASNode) As Boolean
        If node Is Nothing Then
            Return False
        End If
        Return _nodeList.Contains(node)
    End Function

    Public Sub DeleteNode(ByVal node As IASNode)
        node.RemoveFromTree()
        _nodeList.Remove(node)
    End Sub

    Public Function CanAddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode) As Boolean
        If nodeParent Is Nothing Or nodeChild Is Nothing Then
            Return False
        End If
        Dim createsCircleReference = nodeChild.GetAllParentNodes().Contains(nodeChild)
        Dim doIAlreadyHaveAParent = nodeChild.ParentNode IsNot Nothing
        Dim connectionConfigExist = _ASNodeConfiguration.ASConnectionConfigList.FirstOrDefault(Function(n) n.ParentNodeType = nodeParent.NodeType And n.ChildNodeType = nodeChild.NodeType) IsNot Nothing

        Return createsCircleReference = False And doIAlreadyHaveAParent = False And connectionConfigExist = True
    End Function

    Public Sub AddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode)
        If CanAddConnection(nodeParent, nodeChild) Then
            Dim contype = _ASNodeConfiguration.ASConnectionConfigList.First(Function(n) n.ChildNodeType = nodeChild.NodeType And n.ParentNodeType = nodeParent.NodeType).ConnectionType

            nodeChild.ParentNode = nodeParent
            nodeParent.ChildrenNodes.Add(nodeChild)
            _nodeList.ForEach(Sub(n) UpdateColumnChooserModel(n))
        End If
    End Sub

    Public Function CanDeleteConnection(ByVal childNode As IASNode) As Boolean
        Return True
    End Function

    Public Sub DeleteConnection(ByVal childNode As IASNode)
        childNode.ParentNode.ChildrenNodes.Remove(childNode)
        For Each n In childNode.ChildrenNodes
            n.ParentNode = Nothing
        Next
    End Sub

    Public Function GetNodeDisplayState(ByVal node As IASNode) As ASNodeDisplayState
        If _dragStartNode IsNot Nothing Then
            If CanAddConnection(_dragStartNode, node) Then
                Return ASNodeDisplayState.CanDrop
            Else
                Return ASNodeDisplayState.NotDrop
            End If
        Else
            Dim rootNode = _nodeList.OrderBy(Function(n) n.NodeID).FirstOrDefault
            If node.ParentNode Is Nothing And node IsNot rootNode Then
                Return ASNodeDisplayState.ErrorState
            Else
                Return ASNodeDisplayState.Normal
            End If
        End If
    End Function

#End Region

#Region "Drag and Drop Public"
    Dim _dragStartNode As IASNode = Nothing
    Public Sub StartDragDrop(ByVal dragStartNode As IASNode)
        _dragStartNode = dragStartNode
    End Sub

    Public Sub EndDragDrop(ByVal dragEndNode As IASNode)
        If CanAddConnection(_dragStartNode, dragEndNode) Then
            AddConnection(_dragStartNode, dragEndNode)
        End If
        _dragStartNode = Nothing
    End Sub
#End Region

#Region "Factory Methods"
    Public Function CreateNode(ByVal nodeType As DDNodeEnum, ByVal nodeID As Integer, ByVal tag As Object) As IASNode
        Dim disText = _ASNodeConfiguration.ASNodeConfigList.First(Function(n) n.NodeType = nodeType).NodeDisplayText
        Dim fac = New IDDNodeFactory
        Return New ASNodeBase(nodeID, String.Format("{0}({1})", disText, nodeID), disText, nodeType, tag, fac.GetASNodeCriteria(nodeType))
    End Function

    Public Sub UpdateColumnChooserModel(ByVal node As IASNode)
        If (node Is Nothing) Then
            Return
        End If

        node.TableColumnChooserList.Where(Function(n) Not n.Equals(node)).ToList().ForEach(Sub(n) n.TableGroupVisibility = Visibility.Collapsed)

        CheckCreateTableChooserModel(node, node, False)
        node.GetAllParentNodes().ToList().ForEach(Sub(n) CheckCreateTableChooserModel(node, n, False))
        node.GetAllChildrenNodes().ToList().ForEach(Sub(n) CheckCreateTableChooserModel(node, n, True))

    End Sub

    Private Sub CheckCreateTableChooserModel(ByVal povitNode As IASNode, ByVal node As IASNode, ByVal isAggregate As Boolean)
        Dim ptcm As TableChooserModel = povitNode.TableColumnChooserList.FirstOrDefault(Function(n) node.Equals(n.NodeTag))
        If ptcm Is Nothing Then
            ptcm = New TableChooserModel(node.MyName, node) With {.TableGroupVisibility = Visibility.Visible}
            povitNode.TableColumnChooserList.Add(ptcm)
        End If

        ptcm.TableGroupVisibility = Visibility.Visible
        FillTableChooserModelNormal(ptcm, node.NodeType, isAggregate)
    End Sub

    Private Sub FillTableChooserModelNormal(ByVal tcm As TableChooserModel, ByVal nodeType As DDNodeEnum, ByVal isAggregate As Boolean)
        For Each nodeConfig In _ASNodeConfiguration.ASNodeColumnConfigList.Where(Function(n) n.NodeType.Equals(nodeType))

            Dim iterNodeConfig = nodeConfig
            Dim ccm As ColumnChooserModel = tcm.ColumnChooserList.FirstOrDefault(Function(cn) cn.ColumnDisplayName = iterNodeConfig.ColumnDisplayName)
            If ccm Is Nothing Then
                ccm = New ColumnChooserModel(nodeConfig.ColumnDisplayName) With {.ColumnIsSelected = False}
                tcm.ColumnChooserList.Add(ccm)
            End If
            If Not isAggregate Xor iterNodeConfig.IsAggregate Then
                ccm.ColumnVisibility = Visibility.Visible
            Else
                ccm.ColumnVisibility = Visibility.Collapsed
            End If
        Next
    End Sub

#End Region

End Class
