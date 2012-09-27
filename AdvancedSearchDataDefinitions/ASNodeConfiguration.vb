Imports System
Imports System.Runtime.Serialization
Imports System.Xml
Imports System.Xml.Serialization
Imports System.ServiceModel

<DataContract()> _
Public Class ASConfiguration
    <DataMember()>
    Public ASConnectionConfigList As List(Of ASConnectionConfig)
    <DataMember()>
    Public ASNodeConfigList As List(Of ASNodeConfig)
    'Public Property ASNodeColumnConfigList As ASNodeColumnConfig()
    'Public Property ASNodeColumnCriteriaConfigList As ASNodeColumnCriteriaConfig()

End Class

<DataContract()> _
Public Class ASConnectionConfig
    <DataMember()>
    Public ParentNodeType As Integer
    <DataMember()>
    Public ChildNodeType As Integer
    <DataMember()>
    Public ConnectionType As String
End Class

<DataContract()> _
Public Class ASNodeConfig
    <DataMember()>
    Public NodeType As Integer
    <DataMember()>
    Public NodeDisplayText As String
End Class

<DataContract()> _
Public Class ASNodeColumnConfig
    Public NodeType As Integer
    Public ColumnType As Integer
    Public ColumnDataType As Type
    Public ColumnDisplayName As String
    Public IsAggregate As Boolean
End Class

<DataContract()> _
Public Class ASNodeColumnCriteriaConfig
    Public NodeType As Integer
    Public ColumnDisplayName As String
    Public CriteriaTypeOf As Type
    Public DefaultValue As Object
    Public ObjectData As Object
End Class




