Public Class NodeFactory
    Dim _nodeConfig As NodeConfiguration

    Public Sub New(nodeConfig As NodeConfiguration)
        _nodeConfig = nodeConfig
    End Sub

    Public Function CreateNode(ByVal nodeType As Integer, ByVal nodeID As Integer, ByVal tag As Object) As IASNode
        Return New ASNodeBase(nodeID, _nodeConfig.ASNodeConfigList.First(Function(n) n.NodeType = nodeType).NodeDisplayText, nodeType, tag)
    End Function

End Class
