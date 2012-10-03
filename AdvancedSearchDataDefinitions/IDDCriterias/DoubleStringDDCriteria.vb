Public Class DoubleStringDDCriteria
    Implements IDDCriteria

    Public Sub New(ByVal displayName1 As String, ByVal displayName2 As String, ByVal defaultValue1 As String, ByVal defaultValue2 As String)
        _displayName1 = displayName1
        _displayName2 = displayName2
        Value1 = defaultValue1
        Value2 = defaultValue2
    End Sub

    Dim _displayName1 As String = Nothing
    Public ReadOnly Property DisplayName1 As String
        Get
            Return _displayName1
        End Get
    End Property

    Dim _displayName2 As String = Nothing
    Public ReadOnly Property DisplayName2 As String
        Get
            Return _displayName2
        End Get
    End Property

    Public Property Value1 As String

    Public Property Value2 As String


    Public Sub Intialize() Implements IDDCriteria.Intialize

    End Sub

End Class
