Public Class Node_UC

    Public Event NodeSelected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event ConnectorMouseLeftButtonDown(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event NodeDroppedOn(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
    Public Event NodeMove(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseEventArgs)

    Public Property AllowMove As Boolean

    Public Sub SetIsSelected(ByVal state As Boolean)
        UpadateIsSelected(state)
    End Sub

    Dim _tranFormGetter As Func(Of Double)
    Public Sub New(ByVal imagePath As String, ByVal tranFormGetter As Func(Of Double))
        InitializeComponent()
        _tranFormGetter = tranFormGetter
        iNodeImage.Source = New System.Windows.Media.Imaging.BitmapImage(New Uri(imagePath, UriKind.Relative))
        AllowMove = True
        AddHandler top_part.MouseLeftButtonDown, AddressOf Node_MouseLeftButtonDown
        AddHandler top_part.MouseMove, AddressOf Node_MouseMove
        AddHandler top_part.MouseLeftButtonUp, AddressOf Node_MouseLeftButtonUp
        AddHandler bottom_part.MouseLeftButtonDown, AddressOf BottomPartMouseLeftButtonDown

        LoadNodeDisplayMap()
        UpdateNodeDisplayState()
    End Sub

#Region "bottom_part Events"

    Private Sub BottomPartMouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        RaiseEvent ConnectorMouseLeftButtonDown(Me, e)
    End Sub

#End Region

#Region "top part mouse handles"
    Dim isMyMouseCaptured As Boolean
    Dim mouseVerticalPosition As Double
    Dim mouseHorizontalPosition As Double
    Private Sub Node_MouseLeftButtonDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        If (AllowMove) Then
            mouseVerticalPosition = e.GetPosition(Nothing).Y
            mouseHorizontalPosition = e.GetPosition(Nothing).X
            isMyMouseCaptured = True
            item.CaptureMouse()
        End If
    End Sub

    Private Sub Node_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)
        Dim item As UIElement = sender
        If (isMyMouseCaptured) Then
            Dim deltaV As Double = e.GetPosition(Nothing).Y - mouseVerticalPosition
            Dim deltaH As Double = e.GetPosition(Nothing).X - mouseHorizontalPosition
            Dim newTop As Double = deltaV / _tranFormGetter() + Me.GetValue(Canvas.TopProperty)
            Dim newLeft As Double = deltaH / _tranFormGetter() + Me.GetValue(Canvas.LeftProperty)

            Me.SetValue(Canvas.TopProperty, newTop)
            Me.SetValue(Canvas.LeftProperty, newLeft)

            mouseVerticalPosition = e.GetPosition(Nothing).Y
            mouseHorizontalPosition = e.GetPosition(Nothing).X
            RaiseEvent NodeMove(Me, e)
        End If
    End Sub

    Private Sub Node_MouseLeftButtonUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        Dim item As UIElement = sender
        If isMyMouseCaptured Then
            isMyMouseCaptured = False
            item.ReleaseMouseCapture()
            mouseVerticalPosition = -1
            mouseHorizontalPosition = -1
            RaiseEvent NodeSelected(Me, e)
        Else
            RaiseEvent NodeDroppedOn(Me, e)
        End If
    End Sub
#End Region

#Region "bottom_part mouseEnter mouseLeave"
    Private Sub bottom_part_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        testcolor.Opacity = 1
    End Sub

    Private Sub bottom_part_MouseLeave(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        testcolor.Opacity = 0.7
    End Sub
#End Region

#Region "Node Dislay state mouse over and background map"

    Protected Class NodeDisplayMap
        Public Property State As ASNodeDisplayState
        Public Property DefaultOpacity As Double
        Public Property MouseOverOpacity As Double
        Public Property Color As System.Windows.Media.Color
    End Class

    Dim _nodeDisplayMapList As List(Of NodeDisplayMap)
    Dim _nodeDisplayState As ASNodeDisplayState = ASNodeDisplayState.Normal
    Dim _isOverTopPart As Boolean = False
    Dim _isSelected As Boolean = False

    Private Sub LoadNodeDisplayMap()
        If _nodeDisplayMapList Is Nothing Then
            _nodeDisplayMapList = New List(Of NodeDisplayMap)
            _nodeDisplayMapList.Add(New NodeDisplayMap With {.State = ASNodeDisplayState.Normal, .DefaultOpacity = 0, .MouseOverOpacity = 0.7, .Color = Colors.Blue})
            _nodeDisplayMapList.Add(New NodeDisplayMap With {.State = ASNodeDisplayState.CanDrop, .DefaultOpacity = 0.8, .MouseOverOpacity = 1, .Color = Colors.Green})
            _nodeDisplayMapList.Add(New NodeDisplayMap With {.State = ASNodeDisplayState.NotDrop, .DefaultOpacity = 0.8, .MouseOverOpacity = 0.9, .Color = Colors.Black})
            _nodeDisplayMapList.Add(New NodeDisplayMap With {.State = ASNodeDisplayState.ErrorState, .DefaultOpacity = 0.8, .MouseOverOpacity = 0.9, .Color = Colors.Red})
        End If
    End Sub

    Public Sub SetNodeDisplayState(ByVal state As ASNodeDisplayState)
        _nodeDisplayState = state
        UpdateNodeDisplayState()
    End Sub

    Private Sub UpadateIsSelected(ByVal state As Boolean)
        _isSelected = state
        If _isSelected Then
            boarder.BorderBrush = New SolidColorBrush(Colors.Gray)
        Else
            boarder.BorderBrush = New SolidColorBrush(Colors.Transparent)
        End If
    End Sub

    Private Sub UpdateNodeDisplayState()
        backgroundColor.Color = _nodeDisplayMapList.First(Function(n) n.State = _nodeDisplayState).Color
        UpdateBackgroundHighlightOpacity()
    End Sub

    Private Sub UpdateBackgroundHighlightOpacity()
        Dim currentNodeDisplayState = _nodeDisplayMapList.First(Function(n) n.State = _nodeDisplayState)
        If _isOverTopPart Then
            backgroundHighlight.Opacity = currentNodeDisplayState.MouseOverOpacity
        Else
            backgroundHighlight.Opacity = currentNodeDisplayState.DefaultOpacity
        End If
    End Sub

    Private Sub top_part_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        _isOverTopPart = True
        UpdateBackgroundHighlightOpacity()
    End Sub

    Private Sub top_part_MouseLeave(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        _isOverTopPart = False
        UpdateBackgroundHighlightOpacity()
    End Sub

#End Region

End Class
