Public Class ERA2_CLAIMNode
    Inherits DDNodeBase

    Public Overrides ReadOnly Property NodeType As DDNodeEnum
        Get
            Return DDNodeEnum.ERA2_CLAIM
        End Get
    End Property

    Public Sub New()
        Dim ds As ASDataSet = New ASDataSet

        SetPrimaryTable(ds.ERA2_CLAIM)
        AddJoinToChild(DDNodeEnum.ERA2_CLAIM_LINE, {ds.ERA2_CLAIM.ERA_CLAIM_IDColumn}, {ds.ERA2_CLAIM_LINE.ERA_CLAIM_IDColumn})

        AddColumn(1, ds.ERA2_CLAIM.PAYER_NAMEColumn)
        AddColumn(2, ds.ERA2_CLAIM.PATIENT_CONTROL_NUMColumn)
        AddColumn(3, ds.ERA2_CLAIM.PATIENT_FIRST_NAMEColumn)
        AddColumn(4, ds.ERA2_CLAIM.PATIENT_LAST_NAMEColumn)
    End Sub


End Class
