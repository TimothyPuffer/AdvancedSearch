Public Interface IDDNode

    ReadOnly Property NodeType As DDNodeEnum

    ReadOnly Property DisplayName As String

    Function GetChildNodeJoins() As List(Of KeyValuePair(Of DDNodeEnum, String))


End Interface

Public Enum DDNodeEnum
    ERA2_CLAIM = 1
    ERA2_CLAIM_LINE = 2
    ERA2_CLAIM_CAS = 3
End Enum