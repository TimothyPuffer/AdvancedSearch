Imports System.Collections.Generic
Imports AdvancedSearchDataDefinitions

Public Class ASNodeBase
    Implements IASNode

    Dim _nodeID As Integer
    Dim _nodeText As String
    Dim _nodeType As Integer
    Dim _tag As Object
    Dim _myName As String
    Dim _tableColumnChooserList As New List(Of TableChooserModel)
    Public Sub New(ByVal nodeID As Integer, ByVal myName As String, ByVal nodeText As String, ByVal nodeType As DDNodeEnum, ByVal tag As Object, ByVal nodeCriteriaList As Dictionary(Of Integer, IDDCriteria))
        _nodeID = nodeID
        _myName = myName
        _nodeText = nodeText
        _nodeType = nodeType
        _tag = tag
        _tableColumnChooserList = New List(Of TableChooserModel)
        _nodeCriteriaList = nodeCriteriaList
    End Sub

    Public ReadOnly Property NodeID As Integer Implements IASNode.NodeID
        Get
            Return _nodeID
        End Get
    End Property

    Public ReadOnly Property NodeText As String Implements IASNode.NodeText
        Get
            Return _nodeText
        End Get
    End Property

    Public Event NodeTextChanged(sender As Object, text As String) Implements IASNode.NodeTextChanged

    Public ReadOnly Property NodeType As DDNodeEnum Implements IASNode.NodeType
        Get
            Return _nodeType
        End Get
    End Property

    Public ReadOnly Property Tag As Object Implements IASNode.Tag
        Get
            Return _tag
        End Get
    End Property

    Public ReadOnly Property MyName As String Implements IASNode.MyName
        Get
            Return _myName
        End Get
    End Property

    Public ReadOnly Property TableColumnChooserList As List(Of TableChooserModel) Implements IASNode.TableColumnChooserList
        Get
            Return _tableColumnChooserList
        End Get
    End Property

    Public Property ChildrenNodes As New List(Of IMultiChildNode) Implements IMultiChildNode.ChildrenNodes

    Public Property ParentNode As IMultiChildNode = Nothing Implements IMultiChildNode.ParentNode

    Dim _nodeCriteriaList As Dictionary(Of Integer, IDDCriteria)
    Public ReadOnly Property NodeCriteriaList As Dictionary(Of Integer, IDDCriteria) Implements IASNode.NodeCriteriaList
        Get
            Return _nodeCriteriaList
        End Get
    End Property
End Class
