Public Interface IASNode

    Event NodeTextChanged(ByVal sender As Object, ByVal text As String)

    ReadOnly Property NodeID As Integer

    ReadOnly Property NodeType As Integer

    ReadOnly Property NodeText As String

    ReadOnly Property Tag As Object

End Interface

