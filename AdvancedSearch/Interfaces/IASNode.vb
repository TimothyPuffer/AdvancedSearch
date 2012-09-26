Imports System.Runtime.CompilerServices

Public Interface IASNode

    Event NodeTextChanged(ByVal sender As Object, ByVal text As String)

    Property ParentNode As IASNode

    Property ChildrenNodes As List(Of IASNode)

    ReadOnly Property TableColumnChooserList As List(Of TableChooserModel)

    ReadOnly Property MyName As String

    ReadOnly Property NodeID As Integer

    ReadOnly Property NodeType As Integer

    ReadOnly Property NodeText As String

    ReadOnly Property Tag As Object

End Interface

Module IASNodeExtension

    <Extension()>
    Public Function GetBreadthFirstIndex(ByVal node As IASNode) As Integer

        Dim headNode = node.GetAllParentNodes().FirstOrDefault(Function(n) n.ParentNode Is Nothing)
        If node.ParentNode Is Nothing Or headNode Is Nothing Then
            Return 0
        End If

        Return p_GetBreadthFirstIndex(headNode, node, 0)

    End Function

    Private Function p_GetBreadthFirstIndex(ByVal currentNode As IASNode, ByVal nodeToFindIndexOf As IASNode, ByRef index As Integer) As Integer

        If currentNode Is nodeToFindIndexOf Then
            Return index
        End If

        For Each n In currentNode.ChildrenNodes
            index = index + 1
            If n Is nodeToFindIndexOf Then
                Return index
            End If
        Next

        For Each n In currentNode.GetAllChildrenNodes()
            Return p_GetBreadthFirstIndex(n, nodeToFindIndexOf, index)
        Next

        Return index + 1

    End Function

    <Extension()>
    Public Function GetAllParentNodes(ByVal node As IASNode) As IList(Of IASNode)

        Dim retList = New List(Of IASNode)

        While node.ParentNode IsNot Nothing
            retList.Add(node.ParentNode)
            node = node.ParentNode
        End While

        Return retList
    End Function

    <Extension()>
    Public Function GetAllChildrenNodes(ByVal node As IASNode) As IList(Of IASNode)

        Dim retList = New List(Of IASNode)

        p_GetAllChildrenNodes(node, retList)

        Return retList
    End Function

    <Extension()>
    Public Function GetAllParentNodesNodes(ByVal node As IASNode) As IList(Of IASNode)

        Dim retList = New List(Of IASNode)

        p_GetAllChildrenNodes(node, retList)

        Return retList
    End Function

    <Extension()>
    Public Function RemoveFromTree(ByVal node As IASNode) As Boolean
        If (node Is Nothing) Then
            Return False
        End If

        For Each n In node.ChildrenNodes
            n.ParentNode = Nothing
        Next

        If node.ParentNode Is Nothing Then
            Return True
        End If

        If node.ParentNode.ChildrenNodes.Contains(node) Then
            node.ParentNode.ChildrenNodes.Remove(node)
        End If

        Return True
    End Function

    Private Sub p_GetAllParentNodesNodes(ByVal node As IASNode, ByVal list As IList(Of IASNode))

        If (node.ParentNode IsNot Nothing) Then
            list.Add(node.ParentNode)
            p_GetAllParentNodesNodes(node.ParentNode, list)
        End If

    End Sub

    Private Sub p_GetAllChildrenNodes(ByVal node As IASNode, ByVal list As IList(Of IASNode))

        For Each n In node.ChildrenNodes.ToList()
            Dim iterN = n
            list.Add(iterN)
            p_GetAllChildrenNodes(iterN, list)
        Next

    End Sub

End Module

