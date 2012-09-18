Imports System.Collections.ObjectModel

Public Class ResourceProvider

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

    Private Shared __DefaultImagePath = "Images/Info.png"
    Dim _displaypathLookup As New Dictionary(Of Integer, String)
    Public Sub New(ByVal nodeTypeLookup As Dictionary(Of Integer, String))
        _displaypathLookup.Add(1, "Images/Info.png")
        _displaypathLookup.Add(2, "Images/Picture.png")
        _displaypathLookup.Add(3, "Images/Profile.png")

        For Each t In nodeTypeLookup
            If _displaypathLookup.ContainsKey(t.Key) Then
                _resourceList.Add(New ResourceInfo(t.Key, t.Value, _displaypathLookup(t.Key)))
            Else
                _resourceList.Add(New ResourceInfo(t.Key, t.Value, __DefaultImagePath))
            End If

        Next
    End Sub

    Dim _resourceList As New List(Of ResourceInfo)

    Public Function GetResourceInfos() As ReadOnlyCollection(Of ResourceInfo)
        Return New ReadOnlyCollection(Of ResourceInfo)(_resourceList)
    End Function

End Class
