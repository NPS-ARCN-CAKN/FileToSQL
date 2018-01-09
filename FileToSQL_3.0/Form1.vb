Imports System.IO

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim SourceFileInfo As New FileInfo("C:\Work\VitalSigns\ARCN Muskox\Data\2012 WEAR-208-2012 2012 Seward Peninsula Muskox Composition Count Survey\2012 Comp/22E_2012_comp.xlsx")
        Dim SourceDataset As DataSet = GetDatasetFromExcelWorkbook(SourceFileInfo)
        Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(SourceFileInfo, SourceDataset, Nothing)

        SkeeterDatasetTreeNode.Text = SkeeterDatasetTreeNode.FileInfo.Name
        Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
    End Sub

    Private Sub DatasetTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DatasetTreeView.AfterSelect
        'get the datatable associated with the clicked node
        Dim ClickedNode As SkeeterDatasetTreeNode = e.Node
        Dim DT As DataTable = ClickedNode.DataTable

        'set up the skeeterdatatablecontrol
        With Me.SkeeterDataTableControl
            .Dock = DockStyle.Fill

            'set up the source to destination columns mapping DGV
            .ColumnsMappingDataGridView.DataSource = GetMappingsDataTable()

            'set up the source DGV, metadata DGV
            If Not DT Is Nothing Then
                .DataTableBindingSource.DataSource = DT
                .DataTableDataGridView.DataSource = .DataTableBindingSource
                .MetadataDataGridView.DataSource = GetMetadataDataTable(DT)
            Else
                .DataTableBindingSource.DataSource = Nothing
                .DataTableDataGridView.DataSource = .DataTableBindingSource
                .MetadataDataGridView.DataSource = Nothing
            End If

        End With
        'Me.SplitContainer.Panel2.Controls.Add(SkeeterDataTableControl)
    End Sub
End Class
