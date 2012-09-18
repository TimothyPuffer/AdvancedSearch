Imports System.Collections.ObjectModel

Public Class ASNodeManager
    Implements System.ComponentModel.INotifyPropertyChanged


    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

    Dim _selectedNode As IASNode = Nothing
    Public Property SelectedNode() As IASNode
        Get
            Return _selectedNode
        End Get

        Set(ByVal Value As IASNode)
            _selectedNode = Value
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs("SelectedNode"))
        End Set
    End Property

    Dim _nodeList As New List(Of IASNode)
    Public ReadOnly Property NodeList As ReadOnlyCollection(Of IASNode)
        Get
            Return New ReadOnlyCollection(Of IASNode)(_nodeList)
        End Get
    End Property

    Dim _connectionList As New List(Of IASConnection)
    Public ReadOnly Property ConnectionList As ReadOnlyCollection(Of IASConnection)
        Get
            Return New ReadOnlyCollection(Of IASConnection)(_connectionList)
        End Get
    End Property


    Public Function CanAddNodeType(ByVal type As Integer) As Boolean
        Return True
    End Function

    Public Function AddNodeType(ByVal type As Integer, ByVal tag As Object) As IASNode
        Return Nothing
    End Function

    Public Function CanDeleteNode(ByVal node As IASNode) As Boolean
        Return True
    End Function

    Public Sub DeleteNode(ByVal node As IASNode)

    End Sub

    Public Function CanAddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode) As Boolean
        Return True
    End Function

    Public Function AddConnection(ByVal nodeParent As IASNode, ByVal nodeChild As IASNode, ByVal tag As Object) As IASConnection
        Return Nothing
    End Function

    Public Function CanDeleteConnection(ByVal node As IASConnection) As Boolean
        Return True
    End Function

    Public Sub DeleteConnection(ByVal node As IASConnection)

    End Sub

    Public Sub SetManagerState(ByVal mode As ASNodeManagerState)

    End Sub

    Public Function GetNodeDisplayState(ByVal node As IASNode) As ASNodeDisplayState
        Return ASNodeDisplayState.CanDrop
    End Function

    Public Function GetConnectionDisplayState(ByVal connection As IASConnection) As ASConnectorDisplayState
        Return ASConnectorDisplayState.Connecting
    End Function

End Class
