Public Class ERA2_CLAIM_LINENode
    Inherits DDNodeBase

    Public Overrides ReadOnly Property NodeType As Integer
        Get
            Return 2
        End Get
    End Property

    Public Sub New()
        Dim ds As ASDataSet = New ASDataSet

        AddColumn(ds.ERA2_CLAIM_LINE.REV_CODEColumn)
        AddColumn(ds.ERA2_CLAIM_LINE.SERVICE_IDColumn)
        AddColumnCommaDelimited({ds.ERA2_CLAIM_LINE.SERVICE_MOD1Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD2Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD3Column, ds.ERA2_CLAIM_LINE.SERVICE_MOD4Column})
        AddColumn(ds.ERA2_CLAIM_LINE.CHARGESColumn)
        AddColumn(ds.ERA2_CLAIM_LINE.PAYMENTColumn)
        AddColumn(ds.ERA2_CLAIM_LINE.PATIENT_RESP_AMOUNTColumn)
    End Sub
End Class
