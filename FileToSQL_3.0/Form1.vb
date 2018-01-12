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
        LoadSourceDataset(SourceFileInfo)

        'Dim SourceDataset As DataSet = GetDatasetFromExcelWorkbook(SourceFileInfo)
        'Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(SourceFileInfo, SourceDataset, Nothing)
        'With SkeeterDatasetTreeNode
        '    .Text = SkeeterDatasetTreeNode.FileInfo.Name
        '    .FileInfo = SourceFileInfo
        'End With

        'Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
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

    ''' <summary>
    ''' Opens an OpenFileDialog to allow the user to select a source dataset file and then submits it to the LoadSourceDataset routine
    ''' </summary>
    Private Sub OpenFile()
        Dim OFD As New OpenFileDialog()
        With OFD
            .CheckFileExists = True
            .CheckPathExists = True
            .Filter = "Data files|*.csv;*.txt;*.tab;*.xls;*.xlsx"
            .RestoreDirectory = False
            .Title = "Select a data file to open"
        End With
        If OFD.ShowDialog = DialogResult.OK Then
            Dim DataFileInfo As New FileInfo(OFD.FileName)
            LoadSourceDataset(DataFileInfo)
        End If
    End Sub

    ''' <summary>
    ''' Loads the FileInfo into the main interface and builds the data sources TreeView
    ''' </summary>
    ''' <param name="FileInfo"></param>
    Private Sub LoadSourceDataset(FileInfo As FileInfo)
        'clear everything
        Me.DatasetTreeView.Nodes.Clear()

        'open the file

        Dim SourceDataset As DataSet
        Dim NodeImage As Integer = 0
        If FileInfo.Extension = ".xlsx" Or FileInfo.Extension = ".xls" Then
            SourceDataset = GetDatasetFromExcelWorkbook(FileInfo)
            NodeImage = 6 'excel image
        ElseIf FileInfo.Extension = ".csv" Or FileInfo.Extension = ".txt" Then
            SourceDataset = GetDatasetFromCSV(FileInfo, True)
            NodeImage = 7 'text file image
        End If

        Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(FileInfo, SourceDataset, Nothing)
        With SkeeterDatasetTreeNode
            .Text = FileInfo.Name
            .FileInfo = FileInfo
            .ImageIndex = NodeImage
            .SelectedImageIndex = NodeImage
            .Expand()
        End With
        Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
    End Sub

End Class
