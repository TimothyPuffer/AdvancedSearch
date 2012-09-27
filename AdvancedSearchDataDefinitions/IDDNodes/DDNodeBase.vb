Imports System.Collections.Generic
Public MustInherit Class DDNodeBase
    Implements IDDNode


    Dim _primaryTable As String = Nothing

    Public MustOverride ReadOnly Property NodeType As DDNodeEnum Implements IDDNode.NodeType

    Public ReadOnly Property DisplayName As String Implements IDDNode.DisplayName
        Get
            Return IIf(_displayTableName IsNot Nothing, _displayTableName, _primaryTable)
        End Get
    End Property

    Public Function GetChildNodeJoins() As List(Of KeyValuePair(Of DDNodeEnum, String)) Implements IDDNode.GetChildNodeJoins
        Return _joinList.Select(Function(j) New KeyValuePair(Of DDNodeEnum, String)(j.ChildEnum, j.JoinName))
    End Function


    Dim _displayTableName As String
    Public Sub SetPrimaryTable(ByVal primaryTable As DataTable, ByVal displayTableName As String)
        _displayTableName = displayTableName
        _primaryTable = primaryTable.TableName
    End Sub

    Public Sub SetPrimaryTable(ByVal primaryTable As DataTable)
        SetPrimaryTable(primaryTable, Nothing)
    End Sub


    Public Sub AddColumn(ByVal columnID As Integer, ByVal columnName As DataColumn)

    End Sub

    Public Sub AddColumnCommaDelimited(ByVal columnID As Integer, ByVal columnName As DataColumn())

    End Sub

    Dim _joinList As New List(Of JoinHold)
    Shared __standardJoins As String() = {"Must Have", "Include All"}
    Public Sub AddJoinToChild(ByVal childEnum As DDNodeEnum, ByVal parentColumns As DataColumn(), ByVal childColumns As DataColumn())
        For Each j In __standardJoins
            _joinList.Add(New JoinHold With {
                          .ChildEnum = childEnum,
                          .ParentColumns = parentColumns.Select(Function(c) c.ColumnName),
                          .ChildColumns = childColumns.Select(Function(c) c.ColumnName),
                          .JoinName = j})
        Next

    End Sub

    Private Class JoinHold
        Public ChildEnum As DDNodeEnum
        Public ParentColumns As String()
        Public ChildColumns As String()
        Public JoinName As String
    End Class

End Class
