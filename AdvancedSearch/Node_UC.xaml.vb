
Partial Public Class Node_UC
    Inherits UserControl

    Public Event NodeSelected(ByVal sender As Node_UC)

    Public Property AllowMove As Boolean

    Public Sub New()
        InitializeComponent()
        AllowMove = True
        AddHandler top_part.MouseLeftButtonDown, AddressOf Node_MouseLeftButtonDown
        AddHandler top_part.MouseMove, AddressOf Node_MouseMove
        AddHandler top_part.MouseLeftButtonUp, AddressOf Node_MouseLeftButtonUp
    End Sub

    Dim isMouseCaptured As Boolean
    Dim mouseVerticalPosition As Double
    Dim mouseHorizontalPosition As Double
    Private Sub Node_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        If (AllowMove) Then
            mouseVerticalPosition = e.GetPosition(Nothing).Y
            mouseHorizontalPosition = e.GetPosition(Nothing).X
            isMouseCaptured = True
            item.CaptureMouse()
        End If
    End Sub

    Private Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)
        Dim item As UIElement = sender
        If (isMouseCaptured) Then
            Dim deltaV As Double = e.GetPosition(Nothing).Y - mouseVerticalPosition
            Dim deltaH As Double = e.GetPosition(Nothing).X - mouseHorizontalPosition
            Dim newTop As Double = deltaV + Me.GetValue(Canvas.TopProperty)
            Dim newLeft As Double = deltaH + Me.GetValue(Canvas.LeftProperty)

            Me.SetValue(Canvas.TopProperty, newTop)
            Me.SetValue(Canvas.LeftProperty, newLeft)

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
        RaiseEvent NodeSelected(Me)
    End Sub

End Class
