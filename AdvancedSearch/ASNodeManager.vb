Imports System.Collections.ObjectModel

Public Class ASNodeManager

    Dim _nodeMatch As New List(Of NodeMatch)
    Dim _isLoaded As Boolean = False
    Dim _ASNodeConfiguration As NodeConfiguration = Nothing
    Dim _nodeFactory As NodeFactory

#Region "AsyncLoading"

    Public ReadOnly Property IsLoaded As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Dim _callback As Action(Of Object)
    Public Sub LoadAsync(callback As Action(Of Object))
        _callback = callback
        Dim loadM As New ModelLoader
        If (_isLoaded = False) Then
            loadM.LoadNodeConfiguration(AddressOf mycallback)
        End If
    End Sub

    Private Sub mycallback(config As NodeConfiguration)
        _ASNodeConfiguration = config
        _nodeFactory = New NodeFactory(config)
        _isLoaded = True
        If _callback IsNot Nothing Then
            _callback(Me)
        End If
    End Sub

#End Region

#Region "Public Properties"

    Dim _nodeList As New List(Of IASNode)
    Public ReadOnly Property NodeList As ReadOnlyCollection(Of IASNode)
        Get
            Return New ReadOnlyCollection(Of IASNode)(_nodeList)
        End Get
    End Property

    Public ReadOnly Property MyASNodeConfiguration As NodeConfiguration
        Get
            Return _ASNodeConfiguration
        End Get
    End Property
#End Region

#Region "Public Methods"

    Public Function CanAddNodeType(ByVal type As Integer) As Boolean
        Return _ASNodeConfiguration.ASNodeConfigList.FirstOrDefault(Function(node) node.NodeType = type) IsNot Nothing
    End Function

    Public Function AddNodeType(ByVal type As Integer, ByVal tag As Object) As IASNode
        Dim nodeID As Integer = 1
        If _nodeList.Count > 0 Then
            nodeID = _nodeList.Max(Function(x) x.NodeID) + 1
        End If

        Dim node = _nodeFactory.CreateNode(type, nodeID, tag)
        _nodeList.Add(node)
        Return node
    End Function

    Public Function CanDeleteNode(ByVal node As IASNode) As Boolean
        Return _nodeList.Contains(node)
    End Function

    Public Sub DeleteNode(ByVal node As IASNode)
        _nodeList.Remove(node)
    End Sub

    Public Function CanAddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode) As Boolean
        If nodeParent Is Nothing Or nodeChild Is Nothing Then
            Return False
        End If
        Dim createsCircleReference = AmIMyOwnParent(nodeParent.NodeID, nodeChild.NodeID)
        Dim doIAlreadyHaveAParent = _nodeMatch.FirstOrDefault(Function(n) n.ChildNodeID = nodeChild.NodeID) IsNot Nothing
        Dim connectionConfigExist = _ASNodeConfiguration.ASConnectionConfigList.FirstOrDefault(Function(n) n.ParentNodeType = nodeParent.NodeType And n.ChildNodeType = nodeChild.NodeType) IsNot Nothing

        Return createsCircleReference = False And doIAlreadyHaveAParent = False And connectionConfigExist = True
    End Function

    Public Sub AddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode)
        If CanAddConnection(nodeParent, nodeChild) Then
            Dim contype = _ASNodeConfiguration.ASConnectionConfigList.First(Function(n) n.ChildNodeType = nodeChild.NodeType And n.ParentNodeType = nodeParent.NodeType).ConnectionType
            _nodeMatch.Add(New NodeMatch With {.ParentNodeID = nodeParent.NodeID, .ChildNodeID = nodeChild.NodeID, .ConnentionType = contype})
        End If
    End Sub

    Public Function CanDeleteConnection(ByVal childNode As IASNode) As Boolean
        Return _nodeMatch.FirstOrDefault(Function(n) n.ChildNodeID = childNode.NodeID) IsNot Nothing
    End Function

    Public Sub DeleteConnection(ByVal childNode As IASNode)
        If CanDeleteConnection(childNode) Then
            Dim nm = _nodeMatch.First(Function(n) n.ChildNodeID = childNode.NodeID)
            _nodeMatch.Remove(nm)
        End If
    End Sub

    Public Function GetNodeDisplayState(ByVal node As IASNode) As ASNodeDisplayState
        If _dragStartNode IsNot Nothing Then
            If CanAddConnection(_dragStartNode, node) Then
                Return ASNodeDisplayState.CanDrop
            Else
                Return ASNodeDisplayState.NotDrop
            End If
        Else
            Return ASNodeDisplayState.Normal
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

#Region "Private Functions"

    Private Function AmIMyOwnParent(ByVal parentNode As Integer, ByVal childNode As Integer)
        If parentNode = childNode Then
            Return True
        End If
        Dim nm As NodeMatch = _nodeMatch.FirstOrDefault(Function(n) n.ChildNodeID = parentNode)
        If nm IsNot Nothing Then
            Return AmIMyOwnParent(nm.ParentNodeID, childNode)
        End If
        Return False
    End Function

#End Region

End Class
