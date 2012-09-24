Public Class ModelLoader
    Public Delegate Sub NodeConfigurationCallBack(nodeconfigAs As NodeConfiguration)

    Dim _callback As NodeConfigurationCallBack
    Dim _ret As NodeConfiguration
    Public Sub LoadNodeConfiguration(callback As NodeConfigurationCallBack)
        _callback = callback
        _ret = New NodeConfiguration
        _ret.ASConnectionConfigList.Add(New ASConnectionConfig With {.ParentNodeType = 1, .ChildNodeType = 2, .ConnectionType = 1})
        _ret.ASConnectionConfigList.Add(New ASConnectionConfig With {.ParentNodeType = 2, .ChildNodeType = 3, .ConnectionType = 1})

        _ret.ASNodeConfigList.Add(New ASNodeConfig With {.NodeType = 1, .NodeDisplayText = "Account"})
        _ret.ASNodeConfigList.Add(New ASNodeConfig With {.NodeType = 2, .NodeDisplayText = "Service"})
        _ret.ASNodeConfigList.Add(New ASNodeConfig With {.NodeType = 3, .NodeDisplayText = "Item"})

        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Name", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 2, .ColumnDataType = GetType(String), .ColumnDisplayName = "Phone", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Activation Date", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Service Name", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 2, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Date Connected", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Date Disconnected", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Item Name", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 2, .ColumnDataType = GetType(String), .ColumnDisplayName = "Description", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Available Date", .IsAggregate = False})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 4, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Available End Date", .IsAggregate = False})

        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Oustanding Balance", .IsAggregate = True})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Price", .IsAggregate = True})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Discount Price", .IsAggregate = True})

        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "State Tax", .IsAggregate = True})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Federal Tax", .IsAggregate = True})

        Dim dicDate As New Dictionary(Of Integer, String)
        dicDate.Add(1, "Month")
        dicDate.Add(2, "Year")
        dicDate.Add(3, "Day of the week")

        _ret.ASGroupNodeConfigList.Add(New ASGroupNodeConfig With {.ColumnDataType = GetType(DateTime), .GroupChooseLookup = dicDate})

        Dim b As New System.ComponentModel.BackgroundWorker
        AddHandler b.DoWork, AddressOf doWork
        AddHandler b.RunWorkerCompleted, AddressOf doWorkComplete
        b.RunWorkerAsync()

    End Sub

    Private Sub doWork(sender As Object, e As ComponentModel.DoWorkEventArgs)
        System.Threading.Thread.Sleep(4000)
    End Sub

    Private Sub doWorkComplete(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
        _callback(_ret)
    End Sub

End Class
