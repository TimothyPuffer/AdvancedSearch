Imports System.Collections.Generic
Public MustInherit Class DDNodeBase
    Implements IDDNode

#Region "IDDNode Implementoins"
    Dim _primaryTable As String = Nothing
    Public MustOverride ReadOnly Property NodeType As DDNodeEnum Implements IDDNode.NodeType

    Public ReadOnly Property DisplayName As String Implements IDDNode.DisplayName
        Get
            Return IIf(_displayTableName IsNot Nothing, _displayTableName, _primaryTable)
        End Get
    End Property

    Public Function GetChildNodeJoins() As List(Of KeyValuePair(Of DDNodeEnum, String)) Implements IDDNode.GetChildNodeJoins
        Return _joinList.Select(Function(j) New KeyValuePair(Of DDNodeEnum, String)(j.ChildEnum, j.JoinName)).ToList
    End Function

    Public Function GetColumnChoosingInfo() As List(Of ASNodeColumnConfig) Implements IDDNode.GetColumnChoosingInfo
        Dim ret As New List(Of ASNodeColumnConfig)
        _columnDic.Select(Function(dic) New ASNodeColumnConfig With {.ColumnID = dic.Key,
                                                                     .ColumnDisplayName = dic.Value.SourceName,
                                                                     .NodeType = Me.NodeType,
                                                                     .IsAggregate = False}).ToList().ForEach(Sub(c) ret.Add(c))
        _columnAggregateDic.Select(Function(dic) New ASNodeColumnConfig With {.ColumnID = dic.Key,
                                                                             .ColumnDisplayName = dic.Value.SourceName,
                                                                             .NodeType = Me.NodeType,
                                                                             .IsAggregate = True}).ToList().ForEach(Sub(c) ret.Add(c))
        Return ret
    End Function

    Public Function GetNodeCriteria() As Dictionary(Of Integer, IDDCriteria) Implements IDDNode.GetNodeCriteria
        Return _criteriaList
    End Function
#End Region

#Region "Table Rule Configuration"
    Dim _displayTableName As String
    Public Sub SetPrimaryTable(ByVal primaryTable As DataTable, ByVal displayTableName As String)
        _displayTableName = displayTableName
        _primaryTable = primaryTable.TableName
    End Sub

    Public Sub SetPrimaryTable(ByVal primaryTable As DataTable)
        SetPrimaryTable(primaryTable, Nothing)
    End Sub

    Dim _columnDic As New Dictionary(Of Integer, ColumnInfo)
    Public Sub AddColumn(ByVal columnID As Integer, ByVal columnName As DataColumn)
        _columnDic.Add(columnID, New ColumnInfo With {.SourceName = columnName.ColumnName, .SourceTable = columnName.Table.TableName})
    End Sub

    Dim _columnAggregateDic As New Dictionary(Of Integer, ColumnInfoAggregate)
    Public Sub AddColumnAggregate(ByVal columnID As Integer, ByVal columnName As DataColumn)
        _columnAggregateDic.Add(columnID, New ColumnInfoAggregate With {.SourceName = columnName.ColumnName, .SourceTable = columnName.Table.TableName})
    End Sub

    Public Sub AddColumnCommaDelimited(ByVal columnID As Integer, ByVal columnName As DataColumn())

    End Sub

    Dim _joinList As New List(Of JoinHold)
    Shared __standardJoins As String() = {"Must Have", "Include All"}
    Public Sub AddJoinToChild(ByVal childEnum As DDNodeEnum, ByVal parentColumns As DataColumn(), ByVal childColumns As DataColumn())
        For Each j In __standardJoins
            _joinList.Add(New JoinHold With {
                          .ChildEnum = childEnum,
                          .ParentColumns = parentColumns.Select(Function(c) c.ColumnName).ToArray(),
                          .ChildColumns = childColumns.Select(Function(c) c.ColumnName).ToArray(),
                          .JoinName = j})
        Next
    End Sub

    Dim _criteriaList As New Dictionary(Of Integer, IDDCriteria)
    Public Sub AddCriteria(ByVal index As Integer, ByVal criteria As IDDCriteria)
        _criteriaList.Add(index, criteria)
    End Sub
#End Region

#Region "Private Classes"
    Private Class JoinHold
        Public ChildEnum As DDNodeEnum
        Public ParentColumns As String()
        Public ChildColumns As String()
        Public JoinName As String
    End Class

    Private Class ColumnInfo
        Public SourceName As String
        Public SourceTable As String
    End Class

    Private Class ColumnInfoAggregate
        Public SourceName As String
        Public SourceTable As String
    End Class
#End Region

End Class
