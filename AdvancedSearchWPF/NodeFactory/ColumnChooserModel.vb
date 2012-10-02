Public Class TableChooserModel

    Public Sub New(ByVal tableName As String, ByVal nodeTag As IASNode)
        _tableName = tableName
        _nodeTag = nodeTag
    End Sub

    Dim _tableName As String = Nothing
    Public ReadOnly Property TableName As String
        Get
            Return _tableName
        End Get
    End Property

    Dim _nodeTag As Object = Nothing
    Public ReadOnly Property NodeTag As IASNode
        Get
            Return _nodeTag
        End Get
    End Property

    Public Property TableGroupIsExpanded As Boolean = True

    Public Property TableGroupVisibility As Windows.Visibility = Visibility.Visible

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

    Public Property ColumnIsSelected As Boolean = False

    Public Property ColumnVisibility As Windows.Visibility = Visibility.Visible

End Class


