Class MainWindow 
    Private Shared ReadOnly __DefaultNodePosition As New Point(10, 10)

    Dim _manager As New ASNodeManager

    Dim _nodeListTop As New Dictionary(Of Node_UC, List(Of Line))
    Dim _nodeListBot As New Dictionary(Of Node_UC, List(Of Line))

    Public Sub New()
        InitializeComponent()
        Me.IsEnabled = False
        _manager.LoadAsync(AddressOf loadComplete)
    End Sub

    Private Sub loadComplete(send As Object)
        If (_manager.IsLoaded) Then
            Me.IsEnabled = True
            InitializeSources()
        End If
    End Sub

#Region "Events"

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        AddNode(lb_ResourceType.SelectedItem)
    End Sub

    Dim _selectedNode As Node_UC = Nothing
    Private Sub Node_Selected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        For Each n In _nodeListTop
            n.Key.SetIsSelected(False)
        Next

        _selectedNode = sender
        If sender IsNot Nothing Then
            sender.SetIsSelected(True)
        End If
        lb_ColumnChooser.ItemsSource = Nothing
        Dim selectedIASN = _manager.NodeList.FirstOrDefault(Function(n) n.Tag.Equals(sender))
        lb_ColumnChooser.ItemsSource = _manager.SetSelectedNode(selectedIASN)
        btn_delete_node.IsEnabled = _manager.CanDeleteNode(selectedIASN)
    End Sub

    Private Sub lb_ResourceType_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs)
        Dim _selectedResourceInfo As ResourceInfo = CType(lb_ResourceType.SelectedItem, ResourceInfo)
        btn_AddNode.IsEnabled = _manager.CanAddNodeType(_selectedResourceInfo.ResourceType)
    End Sub

    Private Sub btn_delete_node_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim selectedIASN = _manager.NodeList.First(Function(n) n.Tag.Equals(_selectedNode))
        _manager.DeleteNode(selectedIASN)

        DeleteUIElementConnection(_nodeListBot)
        DeleteUIElementConnection(_nodeListTop)
        If canvasNodeDisplay.Children.Contains(_selectedNode) Then
            canvasNodeDisplay.Children.Remove(_selectedNode)
        End If
        Node_Selected(Nothing, Nothing)
        UpdateNodesState()
    End Sub

    Private Sub DeleteUIElementConnection(ByVal dicHold As Dictionary(Of Node_UC, List(Of Line)))
        Dim lines As List(Of Line) = Nothing

        If dicHold.TryGetValue(_selectedNode, lines) Then
            lines.ForEach(Sub(x) canvasNodeDisplay.Children.Remove(x))
            lines.ForEach(Sub(x) lines.Remove(x))
        End If
    End Sub

#End Region

#Region "Drag and Drop Events"

    Dim _tmpConnectingLine As Line = Nothing
    Dim _tmpConnectingNode As Node_UC = Nothing
    Private Sub Connector_Selected(ByVal sender As Node_UC, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        _manager.StartDragDrop(_manager.NodeList.First(Function(n) n.Tag.Equals(sender)))
        UpdateNodesState()

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
            If Not _manager.CanAddConnection(_manager.NodeList.First(Function(n) n.Tag.Equals(_tmpConnectingNode)), _manager.NodeList.First(Function(n) n.Tag.Equals(sender))) Then
                CancelDragAndDrop()
                Return
            End If

            _manager.EndDragDrop(_manager.NodeList.First(Function(n) n.Tag.Equals(sender)))
            Dim o = lb_ColumnChooser.ItemsSource
            lb_ColumnChooser.ItemsSource = Nothing
            lb_ColumnChooser.ItemsSource = o
            UpdateNodesState()

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
            SetLineStartToCenter(_tmpConnectingLine, _tmpConnectingNode.bottom_part)

        End If
    End Sub

    Private Sub canvasNodeDisplay_MouseLeftButtonUp(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
        CancelDragAndDrop()
    End Sub

    Private Sub CancelDragAndDrop()
        If _tmpConnectingLine IsNot Nothing Then
            canvasNodeDisplay.Children.Remove(_tmpConnectingLine)
            _tmpConnectingLine = Nothing
            _tmpConnectingNode = Nothing
            _manager.EndDragDrop(Nothing)
            UpdateNodesState()
        End If
    End Sub

#End Region

#Region "Private Methods"
    Private Sub UpdateNodesState()
        For Each n In _manager.NodeList
            Dim n_UC As Node_UC = CType(n.Tag, Node_UC)
            n_UC.SetNodeDisplayState(_manager.GetNodeDisplayState(n))
        Next
    End Sub

    Private Function GetTangentOffset(l As Line, circle As FrameworkElement) As Point

        Dim radius = circle.ActualHeight / 2 - 2

        Dim offset = circle.TransformToVisual(canvasNodeDisplay).Transform(New Point(radius, radius))
        Dim y As Double = offset.Y - l.Y2
        Dim x As Double = offset.X - l.X2


        If y = 0 Then
            Return New Point(radius, 0)
        ElseIf x = 0 Then
            Return New Point(0, radius)
        End If
        Dim slope = Math.Abs(y / x)
        Dim theta = Math.Atan(slope)

        Dim newX = radius * Math.Sin(Math.PI / 2 - theta)
        Dim newY = radius * Math.Sin(theta)

        If x < 0 Then
            newX = newX * -1
        End If
        If y < 0 Then
            newY = newY * -1
        End If

        Return New Point(newX * -1, newY * -1)

    End Function

    Private Sub SetLineStartToCenter(ByVal cline As Line, ByVal toCenterFrameworkElement As FrameworkElement)
        Dim offset = toCenterFrameworkElement.TransformToVisual(canvasNodeDisplay).Transform(New Point(0, 0))
        Dim tangentoffset = GetTangentOffset(cline, toCenterFrameworkElement)
        cline.X1 = offset.X + tangentoffset.X + toCenterFrameworkElement.ActualHeight / 2
        cline.Y1 = offset.Y + tangentoffset.Y + toCenterFrameworkElement.ActualWidth / 2
    End Sub

    Private Sub SetLineEndToCenter(ByVal cline As Line, ByVal toCenterFrameworkElement As FrameworkElement)
        Dim offset = toCenterFrameworkElement.TransformToVisual(canvasNodeDisplay).Transform(New Point(0, 0))

        Dim lineStartNode As Node_UC = GetStartNode(cline)
        If lineStartNode IsNot Nothing Then
            SetLineStartToCenter(cline, lineStartNode.bottom_part)
        End If

        cline.X2 = offset.X + toCenterFrameworkElement.ActualHeight / 2
        cline.Y2 = offset.Y + toCenterFrameworkElement.ActualWidth / 2
    End Sub

    Private Function GetStartNode(ByVal cline) As Node_UC
        For Each kv In _nodeListTop
            If kv.Value.Contains(cline) Then
                Return kv.Key
            End If
        Next
        Return Nothing
    End Function

    Private Sub AddNode(ByVal nodetype As ResourceInfo, ByVal p As Point)
        Dim node As Node_UC = New Node_UC(nodetype.DisplayObject)
        canvasNodeDisplay.Children.Add(node)
        Dim iasNode = _manager.AddNodeType(nodetype.ResourceType, node)
        node.SetNodeDisplayState(_manager.GetNodeDisplayState(iasNode))

        node.SetValue(Canvas.TopProperty, p.Y)
        node.SetValue(Canvas.LeftProperty, p.X)

        AddHandler node.NodeSelected, AddressOf Node_Selected
        AddHandler node.NodeDroppedOn, AddressOf Node_DroppedOn
        AddHandler node.ConnectorMouseLeftButtonDown, AddressOf Connector_Selected
        AddHandler node.NodeMove, AddressOf Node_Move

        _nodeListTop.Add(node, New List(Of Line))
        Node_Selected(node, Nothing)
    End Sub

    Private Sub AddNode(ByVal nodetype As ResourceInfo)
        AddNode(nodetype, __DefaultNodePosition)
    End Sub

    Private Shared __DefaultImagePath = "Images/Info.png"
    Private Sub InitializeSources()
        Dim _proxyLookup As New Dictionary(Of Integer, String)
        For Each n In _manager.MyASNodeConfiguration.ASNodeConfigList
            _proxyLookup.Add(n.NodeType, n.NodeDisplayText)
        Next
        Dim _displaypathLookup As New Dictionary(Of Integer, String)
        _displaypathLookup.Add(1, "Images/Info.png")
        _displaypathLookup.Add(2, "Images/Picture.png")
        _displaypathLookup.Add(3, "Images/Profile.png")

        Dim _resourceList As New List(Of ResourceInfo)
        For Each t In _proxyLookup
            If _displaypathLookup.ContainsKey(t.Key) Then
                _resourceList.Add(New ResourceInfo(t.Key, t.Value, _displaypathLookup(t.Key)))
            Else
                _resourceList.Add(New ResourceInfo(t.Key, t.Value, __DefaultImagePath))
            End If
        Next

        lb_ResourceType.ItemsSource = _resourceList
        lb_ResourceType.SelectedItem = _resourceList.First()
    End Sub

#End Region

End Class
