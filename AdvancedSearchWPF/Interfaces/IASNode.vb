Imports AdvancedSearchDataDefinitions

Public Interface IASNode
    Inherits IMultiChildNode

    Event NodeTextChanged(ByVal sender As Object, ByVal text As String)

    ReadOnly Property TableColumnChooserList As List(Of TableChooserModel)

    ReadOnly Property NodeCriteriaList As Dictionary(Of Integer, IDDCriteria)

    ReadOnly Property MyName As String

    ReadOnly Property NodeID As Integer

    ReadOnly Property NodeType As DDNodeEnum

    ReadOnly Property NodeText As String

    ReadOnly Property Tag As Object

End Interface


