Imports System.Data.Services
Imports System.Data.Services.Common
Imports System.Linq
Imports System.ServiceModel.Web

Public Class ASDDDataService
    Inherits DataService(Of AdvancedSearchDataDefinitions.ASConfiguration)


    Public Shared Sub InitializeService(ByVal config As DataServiceConfiguration)
        config.SetEntitySetAccessRule("*", EntitySetRights.All)
        config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2
    End Sub

    <WebGet()> _
    Public Function GetASNodeConfiguration() As AdvancedSearchDataDefinitions.ASConfiguration
        Return Nothing
    End Function

End Class
