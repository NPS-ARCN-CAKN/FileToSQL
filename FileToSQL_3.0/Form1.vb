Imports System.IO




Public Class Form1



    'Holds the currently selected TreeNode
    Dim CurrentTreeNode As TreeNode

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.SkeeterDataTableControl.ConnectionStringTextBox.Text = My.Settings.ConnectionString
        Me.SkeeterDataTableControl.QueryTextBox.Text = My.Settings.Query
    End Sub

    Private Sub DatasetTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles DatasetTreeView.AfterSelect

        'get the datatable associated with the clicked node
        Dim ClickedNode As SkeeterDatasetTreeNode = e.Node
        Me.SkeeterDataTableControl.SkeeterDatasetTreeNode = ClickedNode
        Dim DT As DataTable = ClickedNode.DataTable
        Dim DS As DataSet = ClickedNode.Dataset

        'set up the skeeterdatatablecontrol
        With Me.SkeeterDataTableControl
            .Dock = DockStyle.Fill

            'set up the source to destination columns mapping DGV
            .ColumnsMappingDataGridView.DataSource = GetMappingsDataTable()

            'set up the source DGV, metadata DGV
            If Not DT Is Nothing Then
                .DataTableBindingSource.DataSource = DT
                '.DataTableDataGridView.DataSource = .DataTableBindingSource
                .DataTableGridEX.DataSource = .DataTableBindingSource
                .DataTableGridEX.RetrieveStructure()
                .MetadataDataGridView.DataSource = GetMetadataDataTable(DT)
                ' .FormatDataGrid(DT)
                '.FormatMetadataDataGridView()
            Else
                .DataTableBindingSource.DataSource = Nothing
                ClearControls()
            End If

        End With
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
            .Filter = "Data files|*.csv;*.txt;*.tab;*.xls;*.xlsx;*.dbf"
            .RestoreDirectory = False
            .Title = "Select a data file to open"
        End With
        If OFD.ShowDialog = DialogResult.OK Then
            Dim DataFileInfo As New FileInfo(OFD.FileName)
            ClearControls()
            LoadSourceDataset(DataFileInfo)
        End If
    End Sub

    ''' <summary>
    ''' Loads the FileInfo into the main interface and builds the data sources TreeView
    ''' </summary>
    ''' <param name="FileInfo"></param>
    Private Sub LoadSourceDataset(FileInfo As FileInfo)

        'clear everything
        Me.SkeeterDataTableControl.SqlTextBox.Text = ""

        Try
            'open the file
            Dim SourceDataset As DataSet = Nothing
            Dim NodeImage As Integer = 0
            If FileInfo.Extension.ToLower = ".xlsx" Or FileInfo.Extension.ToLower = ".xls" Then
                Dim MyConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FileInfo.FullName & ";Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
                SourceDataset = GetDatasetFromExcelWorkbook(MyConnectionString)
                NodeImage = 6 'excel image
            ElseIf FileInfo.Extension.ToLower = ".csv" Or FileInfo.Extension.ToLower = ".txt" Or FileInfo.Extension.ToLower = ".tab" Then
                SourceDataset = GetDatasetFromTextFile(FileInfo, True, Format.Delimited)
                NodeImage = 7 'text file image
            ElseIf FileInfo.Extension.ToLower = ".dbf" Then
                SourceDataset = GetDatasetFromDBF(FileInfo)
                NodeImage = 0 'database file image
            End If

            'load the skeeterdataset into the treeview
            If Not SourceDataset Is Nothing Then
                Dim SkeeterDatasetTreeNode As New SkeeterDatasetTreeNode(FileInfo, SourceDataset, Nothing)
                With SkeeterDatasetTreeNode
                    .Text = FileInfo.Name
                    .FileInfo = FileInfo
                    .ImageIndex = NodeImage
                    .SelectedImageIndex = NodeImage
                    .Expand()
                End With
                Me.DatasetTreeView.Nodes.Add(SkeeterDatasetTreeNode)
                'open the first subnode
                Me.DatasetTreeView.SelectedNode = SkeeterDatasetTreeNode.Nodes(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub OpenSourceFileToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenSourceFileToolStripButton.Click
        Try
            Dim SkeeterDatasetTreeNode As SkeeterDatasetTreeNode = Me.DatasetTreeView.Nodes(0)
            Process.Start(SkeeterDatasetTreeNode.FileInfo.FullName)
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub OpenDataFileToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenDataFileToolStripButton.Click
        OpenFile()
    End Sub

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim Files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
                For Each File As String In Files
                    ClearControls()

                    If My.Computer.FileSystem.FileExists(File) Then
                        Dim DataFileInfo As New FileInfo(File)
                        LoadSourceDataset(DataFileInfo)
                    End If
                Next File
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub



    Private Sub DatasetTreeView_MouseUp(sender As Object, e As MouseEventArgs) Handles DatasetTreeView.MouseUp
        CurrentTreeNode = Me.DatasetTreeView.GetNodeAt(New Point(e.X, e.Y))
        ' Show menu only if Right Mouse button is clicked
        If e.Button = MouseButtons.Right Then
            Me.TreeViewContextMenuStrip.Show()
        End If
    End Sub

    Private Sub RemoveDatasetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveDatasetToolStripMenuItem.Click
        If Not CurrentTreeNode Is Nothing Then
            Me.DatasetTreeView.Nodes.Remove(CurrentTreeNode)
            Me.SkeeterDataTableControl.DataTableGridEX.DataSource = Nothing
            Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
        End If
    End Sub

    Private Sub RemoveAllDatasetsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveAllDatasetsToolStripMenuItem.Click
        Me.DatasetTreeView.Nodes.Clear()
        Me.SkeeterDataTableControl.DataTableGridEX.DataSource = Nothing
        Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing

        ClearControls()
    End Sub

    ''' <summary>
    ''' Sets the data and metadata grids datasources to Nothing
    ''' </summary>
    Private Sub ClearControls()
        'Me.SkeeterDataTableControl.DataTableDataGridView.DataSource = Nothing
        Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
        Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
        Me.SkeeterDataTableControl.ColumnsMappingDataGridView.DataSource = Nothing
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'see if the database connection info changed
        If My.Settings.ConnectionString.Trim.ToLower <> Me.SkeeterDataTableControl.ConnectionStringTextBox.Text.Trim.ToLower Or My.Settings.Query.Trim.ToLower <> Me.SkeeterDataTableControl.SqlTextBox.Text.Trim.ToLower Then
            'the database connection info changed
            'ask If the user wants to save the database connection info for next time
            If MsgBox("Persist the current database connection string and SQL query?", MsgBoxStyle.YesNo, "Save database connection information for next time?") = MsgBoxResult.Yes Then
                My.Settings.ConnectionString = Me.SkeeterDataTableControl.ConnectionStringTextBox.Text
                My.Settings.Query = Me.SkeeterDataTableControl.SqlTextBox.Text
            End If
        End If

    End Sub


End Class
