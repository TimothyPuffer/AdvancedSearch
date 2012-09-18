Public Interface IASConnection

    Event ConnectionTextChanged(ByVal sender As Object, ByVal text As String)

    ReadOnly Property ConnectionID As Integer

    ReadOnly Property ConnectionText As String

    ReadOnly Property Tag As Object

End Interface

