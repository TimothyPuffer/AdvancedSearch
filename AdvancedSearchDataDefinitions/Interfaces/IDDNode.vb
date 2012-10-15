Public Interface IDDNode
    Inherits IMultiChildNode

    ReadOnly Property NodeType As DDNodeEnum

    Property DisplayName As String

    Sub AddColumn(ByVal nodeName As String, ByVal columnName As String)

    Function GetChildNodeJoins() As List(Of KeyValuePair(Of DDNodeEnum, String))

    Function GetColumnChoosingInfo() As List(Of ASNodeColumnConfig)

    Function GetNodeCriteria() As Dictionary(Of Integer, IDDCriteria)

    Function GetNodeSQL() As String

End Interface

Public Enum DDNodeEnum
    ERA2_CLAIM = 1
    ERA2_CLAIM_LINE = 2
    ERA2_CLAIM_CAS = 3
End Enum