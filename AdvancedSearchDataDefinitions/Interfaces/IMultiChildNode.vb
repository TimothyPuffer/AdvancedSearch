Imports System.Runtime.CompilerServices

Public Interface IMultiChildNode

    Property ParentNode As IMultiChildNode

    ReadOnly Property ChildrenNodes As List(Of IMultiChildNode)

End Interface

Module IMultiChildNodeExtension

    <Extension()>
    Public Function GetBreadthFirstIndex(ByVal node As IMultiChildNode) As Integer

        Dim headNode = node.GetAllParentNodes().FirstOrDefault(Function(n) n.ParentNode Is Nothing)
        If node.ParentNode Is Nothing Or headNode Is Nothing Then
            Return 0
        End If

        Return p_GetBreadthFirstIndex(headNode, node, 0)

    End Function

    Private Function p_GetBreadthFirstIndex(ByVal currentNode As IMultiChildNode, ByVal nodeToFindIndexOf As IMultiChildNode, ByRef index As Integer) As Integer

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
    Public Function GetAllParentNodes(ByVal node As IMultiChildNode) As IList(Of IMultiChildNode)

        Dim retList = New List(Of IMultiChildNode)

        While node.ParentNode IsNot Nothing
            retList.Add(node.ParentNode)
            node = node.ParentNode
        End While

        Return retList
    End Function

    <Extension()>
    Public Function GetRootNode(ByVal node As IMultiChildNode) As IMultiChildNode

        If (node.ParentNode Is Nothing) Then
            Return node
        End If
        Return node.GetAllParentNodes.First(Function(n) n.ParentNode Is Nothing)
    End Function

    <Extension()>
    Public Function GetAllChildrenNodes(ByVal node As IMultiChildNode) As IList(Of IMultiChildNode)

        Dim retList = New List(Of IMultiChildNode)

        p_GetAllChildrenNodes(node, retList)

        Return retList
    End Function

    <Extension()>
    Public Function GetAllParentNodesNodes(ByVal node As IMultiChildNode) As IList(Of IMultiChildNode)

        Dim retList = New List(Of IMultiChildNode)

        p_GetAllChildrenNodes(node, retList)

        Return retList
    End Function

    <Extension()>
    Public Function RemoveFromTree(ByVal node As IMultiChildNode) As Boolean
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

    Private Sub p_GetAllParentNodesNodes(ByVal node As IMultiChildNode, ByVal list As IList(Of IMultiChildNode))

        If (node.ParentNode IsNot Nothing) Then
            list.Add(node.ParentNode)
            p_GetAllParentNodesNodes(node.ParentNode, list)
        End If

    End Sub

    Private Sub p_GetAllChildrenNodes(ByVal node As IMultiChildNode, ByVal list As IList(Of IMultiChildNode))

        For Each n In node.ChildrenNodes.ToList()
            Dim iterN = n
            list.Add(iterN)
            p_GetAllChildrenNodes(iterN, list)
        Next

    End Sub

End Module