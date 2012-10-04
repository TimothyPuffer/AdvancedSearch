Imports AdvancedSearchDataDefinitions

Public Class CriteriaDataTemplateSelector
    Inherits DataTemplateSelector

    Public Overrides Function SelectTemplate(item As Object, container As System.Windows.DependencyObject) As System.Windows.DataTemplate
        Dim fwe As FrameworkElement = CType(container, FrameworkElement)
        Dim kvp As KeyValuePair(Of Integer, IDDCriteria) = CType(item, KeyValuePair(Of Integer, IDDCriteria))

        Select Case kvp.Value.GetType
            Case GetType(DoubleStringDDCriteria)
                Return fwe.FindResource("DoubleStringDDCriteriaDataTemplate")
            Case Else
                Return Nothing
        End Select

    End Function

End Class
