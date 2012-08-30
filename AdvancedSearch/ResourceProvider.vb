Public Class ResourceProvider

    Enum ResourceType
        Account
        Service
        Member
    End Enum

    Enum NodeDisplayState
        Normal
        CanDrop
        NotDrop
        ErrorState
    End Enum

    Enum ConnectorDisplayState
        Connecting
        Normal
        Selected
        ErrorState
        ErrorStateSelected
    End Enum

    Public Class ResourceInfo
        Dim _displayName As String
        Dim _displayObject As Object

        Public Sub New(ByVal displayName As String, ByVal displayObject As Object)
            _displayName = displayName
            _displayObject = displayObject
        End Sub

        Public ReadOnly Property DisplayName() As String
            Get
                Return _displayName
            End Get
        End Property

        Public ReadOnly Property DisplayObject() As String
            Get
                Return _displayObject
            End Get
        End Property

    End Class


    Shared Function GetResourceInfo(ByVal resourceTypeEnum As ResourceType) As ResourceInfo
        FillResourceInfo()
        Dim ret = Nothing
        If _resourcelookup.TryGetValue(resourceTypeEnum, ret) Then
            Return ret
        Else
            Return Nothing
        End If
    End Function

    Shared Function GetResourceInfos() As ResourceInfo()
        FillResourceInfo()
        Return _resourcelookup.Values.ToArray
    End Function


    Shared _resourcelookup As Dictionary(Of ResourceType, ResourceInfo)
    Private Shared Sub FillResourceInfo()
        If _resourcelookup Is Nothing Then
            _resourcelookup = New Dictionary(Of ResourceType, ResourceInfo)
            _resourcelookup.Add(ResourceType.Account, New ResourceInfo("Account", "Images/Info.png"))
            _resourcelookup.Add(ResourceType.Service, New ResourceInfo("Service", "Images/Picture.png"))
            _resourcelookup.Add(ResourceType.Member, New ResourceInfo("Member", "Images/Profile.png"))
        End If
    End Sub


End Class
