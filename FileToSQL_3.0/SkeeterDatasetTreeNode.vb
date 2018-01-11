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
                With DataTableNode
                    .DataTable = DT
                    .Text = DT.TableName
                    .FileInfo = _FileInfo
                End With

                'add the columns
                For Each Col As DataColumn In DT.Columns
                    Dim ColumnNode As New SkeeterDatasetTreeNode(Nothing, Nothing, Nothing)
                    ColumnNode.Text = Col.ColumnName & " - " & Col.DataType.ToString.Replace("System.", "") & " " & Col.MaxLength
                    DataTableNode.Nodes.Add(ColumnNode)
                Next
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
