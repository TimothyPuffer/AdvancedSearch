Public Class CriteriaDataTemplateSelector
    Inherits DataTemplateSelector

    Public Overrides Function SelectTemplate(item As Object, container As System.Windows.DependencyObject) As System.Windows.DataTemplate
        Return MyBase.SelectTemplate(item, container)
    End Function

End Class
