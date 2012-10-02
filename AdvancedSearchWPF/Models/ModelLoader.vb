Imports AdvancedSearchDataDefinitions

Public Class ModelLoader
    Public Delegate Sub NodeConfigurationCallBack(nodeconfigAs As ASConfiguration)

    Dim _callback As NodeConfigurationCallBack
    Dim _ret As ASConfiguration
    Public Sub LoadNodeConfiguration(callback As NodeConfigurationCallBack)
        _callback = callback
        Dim b As New System.ComponentModel.BackgroundWorker
        AddHandler b.DoWork, AddressOf doWork
        AddHandler b.RunWorkerCompleted, AddressOf doWorkComplete
        b.RunWorkerAsync()

    End Sub

    Private Sub doWork(sender As Object, e As ComponentModel.DoWorkEventArgs)
        Dim iddnf As New IDDNodeFactory
        _ret = iddnf.GetASNodeConfiguration()
    End Sub

    Private Sub doWorkComplete(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
        _callback(_ret)
    End Sub

End Class
