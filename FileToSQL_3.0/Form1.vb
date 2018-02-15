﻿Imports System.Data.SqlClient
Imports System.IO





Public Class Form1

    Dim CurrentTreeNode As TreeNode

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
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
                .FormatSourceDataGridView(DT)
                .FormatMetadataDataGridView()
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
            Me.SkeeterDataTableControl.DataTableDataGridView.DataSource = Nothing
            Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
            LoadSourceDataset(DataFileInfo)
        End If
    End Sub

    ''' <summary>
    ''' Loads the FileInfo into the main interface and builds the data sources TreeView
    ''' </summary>
    ''' <param name="FileInfo"></param>
    Private Sub LoadSourceDataset(FileInfo As FileInfo)
        'clear everything
        'Me.DatasetTreeView.Nodes.Clear()
        Me.SkeeterDataTableControl.SqlTextBox.Text = ""

        'open the file
        Dim SourceDataset As DataSet = Nothing
        Dim NodeImage As Integer = 0
        If FileInfo.Extension.ToLower = ".xlsx" Or FileInfo.Extension.ToLower = ".xls" Then
            Dim MyConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FileInfo.FullName & ";Extended Properties=""Excel 12.0 Xml;HDR=YES"";"
            SourceDataset = GetDatasetFromExcelWorkbook(MyConnectionString)
            NodeImage = 6 'excel image
        ElseIf FileInfo.Extension.ToLower = ".csv" Or FileInfo.Extension.ToLower = ".txt" Or FileInfo.Extension.ToLower = ".tab" Then
            SourceDataset = GetDatasetFromTextFile(FileInfo, True, "Delimited")
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
        End If
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
                Dim i As Integer = 1
                For Each File As String In Files
                    Me.SkeeterDataTableControl.DataTableDataGridView.DataSource = Nothing
                    Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
                    If My.Computer.FileSystem.FileExists(File) Then
                        Dim DataFileInfo As New FileInfo(File)
                        LoadSourceDataset(DataFileInfo)
                    End If
                    i = i + 1
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
        End If
    End Sub

    Private Sub RemoveAllDatasetsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveAllDatasetsToolStripMenuItem.Click
        Me.DatasetTreeView.Nodes.Clear()
        ClearControls()
    End Sub

    Private Sub ClearControls()
        Me.SkeeterDataTableControl.DataTableDataGridView.DataSource = Nothing
        Me.SkeeterDataTableControl.MetadataDataGridView.DataSource = Nothing
        Me.SkeeterDataTableControl.ColumnsMappingDataGridView.DataSource = Nothing
    End Sub
End Class
