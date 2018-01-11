Imports System.Data.SqlClient
Imports System.IO





Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        'Dim MyDataTable As New DataTable

        ''make a SqlConnection using the supplied ConnectionString 
        'Dim MySqlConnection As New SqlConnection(My.Settings.ConnectionString)
        'Dim Sql As String = "SELECT *  FROM Transects" 'Transects has a geography column
        'Using MySqlConnection
        '    'open the connection
        '    MySqlConnection.Open()
        '    Using MySqlDataAdapter As New SqlDataAdapter(Sql, MySqlConnection)
        '        MySqlDataAdapter.Fill(MyDataTable)
        '    End Using
        'End Using




        Dim SourceFileInfo As New FileInfo("C:\Work\VitalSigns\ARCN Muskox\Data\2012 WEAR-208-2012 2012 Seward Peninsula Muskox Composition Count Survey\2012 Comp/22E_2012_comp.xlsx")
        Dim SourceDataset As DataSet = GetDatasetFromExcelWorkbook(SourceFileInfo)
        Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(SourceFileInfo, SourceDataset, Nothing)
        With SkeeterDatasetTreeNode
            .Text = SkeeterDatasetTreeNode.FileInfo.Name
            .FileInfo = SourceFileInfo
        End With

        Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
    End Sub

    Private Sub DatasetTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DatasetTreeView.AfterSelect
        'get the datatable associated with the clicked node
        Dim ClickedNode As SkeeterDatasetTreeNode = e.Node
        Me.SkeeterDataTableControl.SkeeterDatasetTreeNode = ClickedNode
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


    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFile()
    End Sub

    Private Sub OpenFile()
        Dim OFD As New OpenFileDialog()
        With OFD
            .CheckFileExists = True
            .CheckPathExists = True
            .Filter = "Data files|*.csv;*.txt;*.tab;*.xls;*.xlsx"
            .InitialDirectory = "C:\"
            .RestoreDirectory = True
            .Title = "Select a data file to open"
        End With
        If OFD.ShowDialog = DialogResult.OK Then
            'clear everything
            Me.DatasetTreeView.Nodes.Clear()

            'open the file
            Dim DataFile As New FileInfo(OFD.FileName)
            Dim SourceDataset As DataSet = GetDatasetFromExcelWorkbook(DataFile)
            Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(DataFile, SourceDataset, Nothing)
            With SkeeterDatasetTreeNode
                .Text = DataFile.Name
                .FileInfo = DataFile
            End With
            Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
        End If
    End Sub

End Class
