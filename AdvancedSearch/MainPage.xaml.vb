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
        Dim r As Rectangle = New Rectangle()
        r.Height = 20
        r.Width = 30
        r.Fill = New SolidColorBrush(Colors.White)
        c_blue.Children.Add(r)

        r.SetValue(Canvas.TopProperty, Convert.ToDouble(10))
        r.SetValue(Canvas.LeftProperty, Convert.ToDouble(10))


        AddHandler r.MouseLeftButtonDown, AddressOf Node_MouseLeftButtonDown
        AddHandler r.MouseMove, AddressOf Node_MouseMove
        AddHandler r.MouseLeftButtonUp, AddressOf Node_MouseLeftButtonUp


    End Sub


    Dim isMouseCaptured As Boolean
    Dim mouseVerticalPosition As Double
    Dim mouseHorizontalPosition As Double
    Private Sub Node_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        mouseVerticalPosition = e.GetPosition(Nothing).Y
        mouseHorizontalPosition = e.GetPosition(Nothing).X
        isMouseCaptured = True
        item.CaptureMouse()
    End Sub

    Private Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)
        Dim item As UIElement = sender
        If (isMouseCaptured) Then
            Dim deltaV As Double = e.GetPosition(Nothing).Y - mouseVerticalPosition
            Dim deltaH As Double = e.GetPosition(Nothing).X - mouseHorizontalPosition
            Dim newTop As Double = deltaV + item.GetValue(Canvas.TopProperty)
            Dim newLeft As Double = deltaH + item.GetValue(Canvas.LeftProperty)

            item.SetValue(Canvas.TopProperty, newTop)
            item.SetValue(Canvas.LeftProperty, newLeft)

            mouseVerticalPosition = e.GetPosition(Nothing).Y
            mouseHorizontalPosition = e.GetPosition(Nothing).X
        End If
    End Sub

    Private Sub Node_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        isMouseCaptured = False
        item.ReleaseMouseCapture()
        mouseVerticalPosition = -1
        mouseHorizontalPosition = -1
    End Sub
End Class