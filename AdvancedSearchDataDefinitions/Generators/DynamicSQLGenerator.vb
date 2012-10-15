Public Class DynamicSQLGenerator

    Dim rootNode As IDDNode = Nothing
    Public Sub AddRootNode(ByVal nodeType As DDNodeEnum, ByVal nodeName As String)
        Dim fac = New IDDNodeFactory
        rootNode = fac.GetNode(nodeType, nodeName)
    End Sub

    Public Sub AddChildNode(ByVal parentNodeName As String, ByVal nodeName As String, ByVal nodeType As DDNodeEnum)
        Dim fac = New IDDNodeFactory
        GetNodeByName(parentNodeName).ChildrenNodes.Add(fac.GetNode(nodeType, nodeName))
    End Sub

    Public Sub AddColumnToNode(ByVal selectedNodeName As String, ByVal nodeName As String, ByVal columnName As String)
        GetNodeByName(selectedNodeName).AddColumn(nodeName, columnName)
    End Sub

    Public Sub GetSingleNodeDynamicSQL(ByVal nodeName)

    End Sub

    Private Function GetNodeByName(ByVal nodeName As String) As IDDNode
        For Each n As IDDNode In rootNode.GetAllChildrenNodes()
            If n.DisplayName = nodeName Then
                Return n
            End If
        Next
        Return Nothing
    End Function

End Class
