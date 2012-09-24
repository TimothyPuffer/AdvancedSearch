Public Class TableChooserModel

    Public Sub New(ByVal tableName As String, ByVal tabletag As Object)
        _tableName = tableName
    End Sub

    Dim _tableName As String = Nothing
    Public ReadOnly Property TableName As String
        Get
            Return _tableName
        End Get
    End Property

    Dim _tabletag As Object = Nothing
    Public ReadOnly Property TableTag As String
        Get
            Return _tabletag
        End Get
    End Property

    Public Property TableGroupIsExpanded

    Public Property ColumnChooserList As New List(Of ColumnChooserModel)

End Class

Public Class ColumnChooserModel

    Public Sub New(ByVal columnDisplayName As String)
        _columnDisplayName = columnDisplayName
    End Sub

    Dim _columnDisplayName As String
    Public ReadOnly Property ColumnDisplayName As String
        Get
            Return _columnDisplayName
        End Get
    End Property

    Public Property ColumnIsSelected As Boolean

End Class


