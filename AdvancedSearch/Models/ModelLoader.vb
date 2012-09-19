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

        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Name"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 2, .ColumnDataType = GetType(String), .ColumnDisplayName = "Phone"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 1, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Activation Date"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Service Name"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 2, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Date Connected"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 2, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Date Disconnected"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 1, .ColumnDataType = GetType(String), .ColumnDisplayName = "Item Name"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 2, .ColumnDataType = GetType(String), .ColumnDisplayName = "Description"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 3, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Available Date"})
        _ret.ASNodeColumnConfigList.Add(New ASNodeColumnConfig With {.NodeType = 3, .ColumnType = 4, .ColumnDataType = GetType(DateTime), .ColumnDisplayName = "Available End Date"})

        Dim dicDec As New Dictionary(Of Integer, String)
        dicDec.Add(1, "Sum")
        dicDec.Add(2, "Max")
        dicDec.Add(3, "Min")

        _ret.ASNodeAggregateColumnConfigList.Add(New ASNodeAggregateColumnConfig With {.NodeType = 1, .ColumnDisplayName = "Oustanding Balance", .AggregateChooseLookup = dicDec})
        _ret.ASNodeAggregateColumnConfigList.Add(New ASNodeAggregateColumnConfig With {.NodeType = 2, .ColumnDisplayName = "Price", .AggregateChooseLookup = dicDec})
        _ret.ASNodeAggregateColumnConfigList.Add(New ASNodeAggregateColumnConfig With {.NodeType = 2, .ColumnDisplayName = "Discount Price", .AggregateChooseLookup = dicDec})

        _ret.ASNodeAggregateColumnConfigList.Add(New ASNodeAggregateColumnConfig With {.NodeType = 3, .ColumnDisplayName = "State Tax", .AggregateChooseLookup = dicDec})
        _ret.ASNodeAggregateColumnConfigList.Add(New ASNodeAggregateColumnConfig With {.NodeType = 3, .ColumnDisplayName = "Federal Tax", .AggregateChooseLookup = dicDec})

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
