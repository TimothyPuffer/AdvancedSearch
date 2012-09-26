Public MustInherit Class DDNodeBase
    Implements IDDNode

    Public MustOverride ReadOnly Property NodeType As Integer Implements IDDNode.NodeType

    Public Sub AddColumn(ByVal columnName As DataColumn)

    End Sub

    Public Sub AddColumnCommaDelimited(ByVal columnName As DataColumn())

    End Sub

End Class
