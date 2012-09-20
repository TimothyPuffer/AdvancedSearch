Public Class ASNodeBase
    Implements IASNode

    Public Property TableColumnChooser As TableChooserModel

    Public Property ChildrenNodes As New System.Collections.Generic.List(Of IASNode) Implements IASNode.ChildrenNodes

    Public Property ParentNode As IASNode Implements IASNode.ParentNode

    Dim _nodeID As Integer
    Dim _nodeText As String
    Dim _nodeType As Integer
    Dim _tag As Object
    Public Sub New(ByVal nodeID As Integer, ByVal nodeText As String, ByVal nodeType As Integer, ByVal tag As Object)
        _nodeID = nodeID
        _nodeText = nodeText
        _nodeType = nodeType
        _tag = tag
    End Sub

    Public ReadOnly Property NodeID As Integer Implements IASNode.NodeID
        Get
            Return _nodeID
        End Get
    End Property

    Public ReadOnly Property NodeText As String Implements IASNode.NodeText
        Get
            Return _nodeText
        End Get
    End Property

    Public Event NodeTextChanged(sender As Object, text As String) Implements IASNode.NodeTextChanged

    Public ReadOnly Property NodeType As Integer Implements IASNode.NodeType
        Get
            Return _nodeType
        End Get
    End Property

    Public ReadOnly Property Tag As Object Implements IASNode.Tag
        Get
            Return _tag
        End Get
    End Property

End Class
