Public Class NodeConfiguration

    Public ASConnectionConfigList As New List(Of ASConnectionConfig)
    Public ASNodeConfigList As New List(Of ASNodeConfig)
    Public ASNodeColumnConfigList As New List(Of ASNodeColumnConfig)
    Public ASNodeColumnCriteriaConfigList As New List(Of ASNodeColumnCriteriaConfig)
    Public ASGroupNodeConfigList As New List(Of ASGroupNodeConfig)

End Class


Public Class ASConnectionConfig
    Public ParentNodeType As Integer
    Public ChildNodeType As Integer
    Public ConnectionType As Integer
End Class

Public Class ASNodeConfig
    Public NodeType As Integer
    Public NodeDisplayText As String
End Class

Public Class ASNodeColumnConfig
    Public NodeType As Integer
    Public ColumnType As Integer
    Public ColumnDataType As Type
    Public Property ColumnDisplayName As String
    Public Property IsAggregate As Boolean
End Class

Public Class ASNodeColumnCriteriaConfig
    Public NodeType As Integer
    Public ColumnDisplayName As String
    Public CriteriaTypeOf As Type
    Public DefaultValue As Object
    Public ObjectData As Object
End Class

Public Class ASGroupNodeConfig
    Public ColumnDataType As Type
    Public GroupChooseLookup As Dictionary(Of Integer, String)
End Class

