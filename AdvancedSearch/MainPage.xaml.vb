﻿Partial Public Class MainPage
    Inherits UserControl

    Private Shared ReadOnly __DefaultNodePosition As New Point(10, 10)

    Dim _nodeListTop As New Dictionary(Of Node_UC, List(Of Line))
    Dim _nodeListBot As New Dictionary(Of Node_UC, List(Of Line))

    Public Sub New()
        InitializeComponent()
        InitializeSources()
    End Sub


#Region "Events"

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        AddNode(lb_ResourceType.SelectedItem)
    End Sub

    Private Sub Node_Selected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)

    End Sub

#End Region

#Region "Drag and Drop Events"

    Dim _tmpConnectingLine As Line = Nothing
    Dim _tmpConnectingNode As Node_UC = Nothing
    Private Sub Connector_Selected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        _tmpConnectingLine = New Line()
        _tmpConnectingNode = sender
        canvasNodeDisplay.Children.Add(_tmpConnectingLine)

        SetLineStartToCenter(_tmpConnectingLine, sender.bottom_part)


        _tmpConnectingLine.X2 = _tmpConnectingLine.X1
        _tmpConnectingLine.Y2 = _tmpConnectingLine.Y1
        _tmpConnectingLine.Stroke = New SolidColorBrush(Colors.Black)
        Canvas.SetZIndex(_tmpConnectingLine, -100)
        _tmpConnectingLine.StrokeThickness = 4
    End Sub

    Private Sub Node_DroppedOn(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If _tmpConnectingLine IsNot Nothing Then
            SetLineEndToCenter(_tmpConnectingLine, sender.top_part)

            Dim addlist As List(Of Line) = Nothing
            If _nodeListTop.TryGetValue(_tmpConnectingNode, addlist) Then
                addlist.Add(_tmpConnectingLine)
            End If

            addlist = Nothing
            If _nodeListBot.TryGetValue(sender, addlist) Then
                addlist.Add(_tmpConnectingLine)
            Else
                Dim nl As New List(Of Line)
                nl.Add(_tmpConnectingLine)
                _nodeListBot.Add(sender, nl)
            End If

            _tmpConnectingLine = Nothing
            _tmpConnectingNode = Nothing
        End If
    End Sub

    Private Sub Node_Move(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseEventArgs)
        Dim addlist As List(Of Line) = Nothing
        If _nodeListTop.TryGetValue(sender, addlist) Then
            For Each cl In addlist
                SetLineStartToCenter(cl, sender.bottom_part)
            Next
        End If

        addlist = Nothing
        If _nodeListBot.TryGetValue(sender, addlist) Then
            For Each cl In addlist
                SetLineEndToCenter(cl, sender.top_part)
            Next
        End If

    End Sub


    Private Sub canvasNodeDisplay_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseEventArgs)
        If _tmpConnectingLine IsNot Nothing Then
            _tmpConnectingLine.X2 = e.GetPosition(canvasNodeDisplay).X
            _tmpConnectingLine.Y2 = e.GetPosition(canvasNodeDisplay).Y
        End If
    End Sub

    Private Sub canvasNodeDisplay_MouseLeftButtonUp(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
        If _tmpConnectingLine IsNot Nothing Then
            canvasNodeDisplay.Children.Remove(_tmpConnectingLine)
            _tmpConnectingLine = Nothing
            _tmpConnectingNode = Nothing
        End If
    End Sub

#End Region

#Region "Private Methods"

    Private Sub SetLineStartToCenter(ByVal cline As Line, ByVal toCenterFrameworkElement As FrameworkElement)
        Dim offset = toCenterFrameworkElement.TransformToVisual(canvasNodeDisplay).Transform(New Point(0, 0))
        cline.X1 = offset.X + toCenterFrameworkElement.ActualHeight / 2
        cline.Y1 = offset.Y + toCenterFrameworkElement.ActualWidth / 2
    End Sub

    Private Sub SetLineEndToCenter(ByVal cline As Line, ByVal toCenterFrameworkElement As FrameworkElement)
        Dim offset = toCenterFrameworkElement.TransformToVisual(canvasNodeDisplay).Transform(New Point(0, 0))
        cline.X2 = offset.X + toCenterFrameworkElement.ActualHeight / 2
        cline.Y2 = offset.Y + toCenterFrameworkElement.ActualWidth / 2
    End Sub

    Private Sub AddNode(ByVal nodetype As ResourceProvider.ResourceInfo, ByVal p As Point)
        Dim node As Node_UC = New Node_UC()
        canvasNodeDisplay.Children.Add(node)

        node.SetValue(Canvas.TopProperty, p.Y)
        node.SetValue(Canvas.LeftProperty, p.X)

        AddHandler node.NodeSelected, AddressOf Node_Selected
        AddHandler node.NodeDroppedOn, AddressOf Node_DroppedOn
        AddHandler node.ConnectorMouseLeftButtonDown, AddressOf Connector_Selected
        AddHandler node.NodeMove, AddressOf Node_Move

        _nodeListTop.Add(node, New List(Of Line))
    End Sub

    Private Sub AddNode(ByVal nodetype As ResourceProvider.ResourceInfo)
        AddNode(nodetype, __DefaultNodePosition)
    End Sub

    Private Sub InitializeSources()
        lb_ResourceType.ItemsSource = ResourceProvider.GetResourceInfos()
        lb_ResourceType.SelectedItem = ResourceProvider.GetResourceInfos().First()
    End Sub

#End Region

End Class