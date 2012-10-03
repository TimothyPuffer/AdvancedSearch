Imports System
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.ServiceModel

Public Class ASConfiguration
    Public ASConnectionConfigList As New List(Of ASConnectionConfig)
    Public ASNodeConfigList As New List(Of ASNodeConfig)
    Public ASNodeColumnConfigList As New List(Of ASNodeColumnConfig)
End Class

Public Class ASConnectionConfig
    Public ParentNodeType As DDNodeEnum
    Public ChildNodeType As DDNodeEnum
    Public ConnectionType As String
End Class

Public Class ASNodeConfig
    Public NodeType As DDNodeEnum
    Public NodeDisplayText As String
End Class

Public Class ASNodeColumnConfig
    Public ColumnID As Integer
    Public NodeType As DDNodeEnum
    Public ColumnDisplayName As String
    Public IsAggregate As Boolean
End Class

