﻿Imports AdvancedSearchDataDefinitions
Public NotInheritable Class IDDNodeFactory

    Dim _factoryDictionary As New Dictionary(Of DDNodeEnum, Func(Of IDDNode))
    Public Sub New()
        _factoryDictionary.Add(DDNodeEnum.ERA2_CLAIM, Function() New ERA2_CLAIMNode)
        _factoryDictionary.Add(DDNodeEnum.ERA2_CLAIM_LINE, Function() New ERA2_CLAIM_LINENode)
        _factoryDictionary.Add(DDNodeEnum.ERA2_CLAIM_CAS, Function() New ERA2_CLAIM_CASNode)
    End Sub

    Public Function GetNode(ByVal nodeEnum As DDNodeEnum, ByVal displayName As String) As IDDNode
        Dim ret = _factoryDictionary(nodeEnum)()
        ret.DisplayName = displayName
        If ret.NodeType <> nodeEnum Then
            Throw New Exception("Fix the Code so that the factory method IDDNode.NodeType match the nodeEnum")
        End If
        Return ret
    End Function

    Public Function GetAllNodes() As List(Of IDDNode)
        Dim retList As New List(Of IDDNode)
        For Each kv In _factoryDictionary
            retList.Add(GetNode(kv.Key, Nothing))
        Next
        Return retList
    End Function

    Public Function GetASNodeConfiguration() As ASConfiguration
        Dim ret = New ASConfiguration
        Dim nList = GetAllNodes()

        For Each n In nList
            Dim iterN = n
            ret.ASNodeConfigList.Add(New ASNodeConfig With {.NodeType = n.NodeType, .NodeDisplayText = n.DisplayName})
            n.GetChildNodeJoins.ForEach(Sub(j) ret.ASConnectionConfigList.Add(New ASConnectionConfig With {.ChildNodeType = j.Key, .ParentNodeType = iterN.NodeType, .ConnectionType = j.Value}))
            n.GetColumnChoosingInfo().ForEach(Sub(c) ret.ASNodeColumnConfigList.Add(c))
        Next
        Return ret
    End Function

    Public Function GetASNodeCriteria(ByVal nodeType As DDNodeEnum) As Dictionary(Of Integer, IDDCriteria)
        Return GetNode(nodeType, Nothing).GetNodeCriteria()
    End Function

End Class
