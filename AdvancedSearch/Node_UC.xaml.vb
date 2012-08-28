
Partial Public Class Node_UC
    Inherits UserControl

    Enum NodeStateEnum
        Normal
        Selected
        CanDrop
        NoDrop
        ErrorState
        ErrorStateSelected
    End Enum

    Public Event NodeSelected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event ConnectorMouseLeftButtonDown(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event NodeDroppedOn(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event NodeMove(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseEventArgs)

    Public Property AllowMove As Boolean

    Dim _NodeState As NodeStateEnum = NodeStateEnum.Normal
    Public Property NodeState() As NodeStateEnum
        Get
            Return _NodeState
        End Get
        Set(ByVal Value As NodeStateEnum)
            _NodeState = Value
            SetNodeState()
        End Set
    End Property

    Public Sub New(ByVal imagePath As String)
        InitializeComponent()
        iNodeImage.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri(imagePath, UriKind.Relative))
        AllowMove = True
        AddHandler top_part.MouseLeftButtonDown, AddressOf Node_MouseLeftButtonDown
        AddHandler top_part.MouseMove, AddressOf Node_MouseMove
        AddHandler top_part.MouseLeftButtonUp, AddressOf Node_MouseLeftButtonUp

        AddHandler bottom_part.MouseLeftButtonDown, AddressOf BottomPartMouseLeftButtonDown

    End Sub


    Private Sub BottomPartMouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        RaiseEvent ConnectorMouseLeftButtonDown(Me, e)
    End Sub

#Region "top part mouse handles"
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
            RaiseEvent NodeMove(Me, e)
        End If
    End Sub

    Private Sub Node_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        If isMouseCaptured Then
            isMouseCaptured = False
            item.ReleaseMouseCapture()
            mouseVerticalPosition = -1
            mouseHorizontalPosition = -1
            RaiseEvent NodeSelected(Me, e)
        Else
            RaiseEvent NodeDroppedOn(Me, e)
        End If
    End Sub
#End Region

#Region "Private Methods"
    Private Sub SetNodeState()

    End Sub
#End Region

End Class
