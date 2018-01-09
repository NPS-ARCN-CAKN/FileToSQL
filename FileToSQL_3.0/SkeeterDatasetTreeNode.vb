Imports System.IO
''' <summary>
''' An extended TreeNode that holds a Dataset and/or DataTable
''' </summary>
Public Class SkeeterDatasetTreeNode
    Inherits TreeNode

    Public Sub New(FileInfo As FileInfo, Dataset As DataSet, DataTable As DataTable)
        _Dataset = Dataset
        _FileInfo = FileInfo
        _DataTable = DataTable

        'add the datatable nodes
        If Not _Dataset Is Nothing Then
            For Each DT As DataTable In _Dataset.Tables
                Dim DataTableNode As New SkeeterDatasetTreeNode(Nothing, Nothing, DT)
                DataTableNode.DataTable = DT
                DataTableNode.Text = DT.TableName
                Me.Nodes.Add(DataTableNode)
            Next
        End If

    End Sub

    Private _Dataset As DataSet
    ''' <summary>
    ''' A Dataset
    ''' </summary>
    ''' <returns>Dataset</returns>
    Public Property Dataset() As DataSet
        Get
            Return _Dataset
        End Get
        Set(ByVal value As DataSet)
            _Dataset = value


        End Set
    End Property

    ''' <summary>
    ''' DataTable
    ''' </summary>
    Private _DataTable As DataTable
    ''' <summary>
    ''' A DataTable
    ''' </summary>
    ''' <returns>DataTable</returns>
    Public Property DataTable() As DataTable
        Get
            Return _DataTable
        End Get
        Set(ByVal value As DataTable)
            _DataTable = value
        End Set
    End Property

    Private _FileInfo As FileInfo
    ''' <summary>
    ''' The Data source's FileInfo
    ''' </summary>
    ''' <returns>FileInfo</returns>
    Public Property FileInfo() As FileInfo
        Get
            Return _FileInfo
        End Get
        Set(ByVal value As FileInfo)
            _FileInfo = value
        End Set
    End Property
End Class
