Public Enum ASNodeDisplayState
    Normal = 0
    CanDrop = 1
    NotDrop = 2
    ErrorState = 3
End Enum

Public Enum ASConnectorDisplayState
    Connecting = 0
    Normal = 1
    Selected = 2
    ErrorState = 3
    ErrorStateSelected = 4
End Enum

Public Enum ASNodeManagerState
    Normal = 0
    DragAndDrop = 1
End Enum

Public Class ResourceInfo
    Dim _displayName As String
    Dim _displayObject As String
    Dim _resourceType As Integer

    Public Sub New(ByVal resouceType As Integer, ByVal displayName As String, ByVal displayObject As String)
        _resourceType = resouceType
        _displayName = displayName
        _displayObject = displayObject
    End Sub

    Public ReadOnly Property ResourceType() As String
        Get
            Return _resourceType
        End Get
    End Property

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