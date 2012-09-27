Public Class ERA2_CLAIM_LINENode
    Inherits DDNodeBase

    Public Overrides ReadOnly Property NodeType As DDNodeEnum
        Get
            Return DDNodeEnum.ERA2_CLAIM_LINE
        End Get
    End Property

    Public Sub New()
        Dim ds As ASDataSet = New ASDataSet

        SetPrimaryTable(ds.ERA2_CLAIM_LINE)
        AddJoinToChild(DDNodeEnum.ERA2_CLAIM_CAS, {ds.ERA2_CLAIM_LINE.ERA_CLAIM_IDColumn, ds.ERA2_CLAIM_LINE.LINE_NUMColumn}, {ds.ERA2_CLAIM_CAS.ERA_CLAIM_IDColumn, ds.ERA2_CLAIM_CAS.LINE_NUMColumn})

        AddColumn(1, ds.ERA2_CLAIM_LINE.REV_CODEColumn)
        AddColumn(2, ds.ERA2_CLAIM_LINE.SERVICE_IDColumn)
        AddColumnCommaDelimited(3, {ds.ERA2_CLAIM_LINE.SERVICE_MOD1Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD2Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD3Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD4Column})
        AddColumn(4, ds.ERA2_CLAIM_LINE.CHARGESColumn)
        AddColumn(5, ds.ERA2_CLAIM_LINE.PAYMENTColumn)
        AddColumn(6, ds.ERA2_CLAIM_LINE.PATIENT_RESP_AMOUNTColumn)
    End Sub
End Class
