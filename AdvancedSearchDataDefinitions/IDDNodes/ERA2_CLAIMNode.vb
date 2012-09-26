Public Class ERA2_CLAIMNode
    Inherits DDNodeBase

    Public Overrides ReadOnly Property NodeType As Integer
        Get
            Return 1
        End Get
    End Property

    Public Sub New()
        Dim ds As ASDataSet = New ASDataSet

        AddColumn(ds.ERA2_CLAIM.PAYER_NAMEColumn)
        AddColumn(ds.ERA2_CLAIM.PATIENT_CONTROL_NUMColumn)
        AddColumn(ds.ERA2_CLAIM.PATIENT_FIRST_NAMEColumn)
        AddColumn(ds.ERA2_CLAIM.PATIENT_LAST_NAMEColumn)
    End Sub


End Class
