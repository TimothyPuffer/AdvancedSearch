Partial Public Class MainPage
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
        lb_ResourceType.ItemsSource = ResourceProvider.GetResourceInfos()

        Dim b As New ComponentModel.BackgroundWorker()
    End Sub

#Region "Drag and drop"

#End Region

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim node As Node_UC = New Node_UC()
        canvasNodeDisplay.Children.Add(node)

        node.SetValue(Canvas.TopProperty, Convert.ToDouble(10))
        node.SetValue(Canvas.LeftProperty, Convert.ToDouble(10))

        AddHandler node.NodeSelected, AddressOf Node_Selected

    End Sub

    Private Sub Node_Selected(ByVal sender As Node_UC)

    End Sub
End Class