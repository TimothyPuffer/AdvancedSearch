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




