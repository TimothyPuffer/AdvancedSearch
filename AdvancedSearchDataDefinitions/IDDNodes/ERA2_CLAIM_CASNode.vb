﻿Public Class ERA2_CLAIM_CASNode
    Inherits DDNodeBase

    Public Overrides ReadOnly Property NodeType As DDNodeEnum
        Get
            Return DDNodeEnum.ERA2_CLAIM_CAS
        End Get
    End Property

    Public Sub New()
        Dim ds As ASDataSet = New ASDataSet

        SetPrimaryTable(ds.ERA2_CLAIM_CAS)
        AddColumnAggregate(1, ds.ERA2_CLAIM_CAS.AMOUNTColumn)

        ds.Dispose()
    End Sub

End Class
