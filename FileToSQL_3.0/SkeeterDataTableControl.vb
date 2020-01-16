Imports System.IO
Imports Janus.Windows.GridEX

Public Class SkeeterDataTableControl

    'datatable to hold the source to destination columns mappings
    Dim MappingsDataTable As DataTable

    Dim NullColor As System.Drawing.Color = Color.Gainsboro
    Dim ZeroLengthColor As System.Drawing.Color = Color.WhiteSmoke

    Private _SkeeterDatasetTreeNode As SkeeterDatasetTreeNode

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Property SkeeterDatasetTreeNode() As SkeeterDatasetTreeNode
        Get
            Return _SkeeterDatasetTreeNode
        End Get
        Set(ByVal value As SkeeterDatasetTreeNode)
            _SkeeterDatasetTreeNode = value
        End Set
    End Property


    ''' <summary>
    ''' When a user clicks on a column in the data grid the accompanying row in the metadata grid should be highlighted
    ''' </summary>
    Private Sub HighlightClickedColumnMetadata()

        'get the name of the grid's clicked column
        If Not Me.DataTableGridEX.CurrentColumn Is Nothing Then
            Dim ClickedColumnName As String = Me.DataTableGridEX.CurrentColumn.Caption

            'loop through the metadata DGV's columns looking for a match.  
            Dim RowIndex As Integer = 0
            Dim SelectedRowIndex As Integer = 0
            If MetadataDataGridView.Rows.Count > 0 Then
                For Each Row As DataGridViewRow In MetadataDataGridView.Rows
                    If Row.Cells("ColumnName").Value = ClickedColumnName Then
                        Me.MetadataDataGridView.Rows(RowIndex).Selected = True
                        SelectedRowIndex = RowIndex
                    Else
                        Me.MetadataDataGridView.Rows(RowIndex).Selected = False
                    End If
                    RowIndex = RowIndex + 1
                Next
                Me.MetadataDataGridView.FirstDisplayedScrollingRowIndex = SelectedRowIndex
            End If
        End If
    End Sub

    Private Sub ConnectButton_Click(sender As Object, e As EventArgs)
        Try
            LoadMappingsGrid()
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the source datatable to destination datatable columns mappings grid.
    ''' </summary>
    Private Sub LoadMappingsGrid()

        'set up a DGV for the destination datatable
        Dim DestinationDataTable As DataTable

        Try
            'set up the mappings DGV with a new empty table
            MappingsDataTable = GetMappingsDataTable()
            Me.ColumnsMappingDataGridView.DataSource = MappingsDataTable

            'get the destination database datatable
            Dim Sql As String = Me.QueryTextBox.Text
            DestinationDataTable = GetSQLServerDatabaseTable(Me.ConnectionStringTextBox.Text, Sql)
            Me.DestinationDataGridView.DataSource = DestinationDataTable

            'load the source column names into the DGV's chooser combobox tool
            If Not Me.MetadataDataGridView.DataSource Is Nothing Then
                Dim MetadataDataTable As DataTable = Me.MetadataDataGridView.DataSource
                Dim SourceColumnNameDataGridViewComboBoxColumn As DataGridViewComboBoxColumn = Me.ColumnsMappingDataGridView.Columns("SourceColumnName")
                With SourceColumnNameDataGridViewComboBoxColumn
                    .Items.Add("Default value")
                    .Items.Add("New GUID")
                    .Items.Add("Autonumber")
                    .Items.Add("Current Datetime")
                    .Items.Add("Username")
                    For Each Row As DataRow In MetadataDataTable.Rows
                        .Items.Add(Row.Item("ColumnName"))
                    Next
                End With
            End If

            'load the destination datatable columns into the DGV
            For Each Column As DataColumn In DestinationDataTable.Columns
                Dim NewRow As DataRow = MappingsDataTable.NewRow
                NewRow.Item("DestinationColumnName") = Column.ColumnName
                If Column.DataType.ToString.Contains("String") Then
                    NewRow.Item("QuotedColumn") = True
                Else
                    NewRow.Item("QuotedColumn") = False
                End If
                MappingsDataTable.Rows.Add(NewRow)
            Next

        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    ''' <summary>
    ''' Shows or hides the totals row for each data table column.
    ''' </summary>
    ''' <param name="ShowTotalsRow"></param>
    Public Sub ShowColumnTotals(ShowTotalsRow As Boolean)
        Me.DataTableGridEX.TotalRow = ShowTotalsRow
        If ShowTotalsRow = True Then
            'add a total summary at the bottom of the gridex for each row
            For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Average
            Next
        End If
    End Sub

    Private Sub ShowColumnTotalsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShowColumnTotalsToolStripComboBox.SelectedIndexChanged
        SelectColumnTotals()
    End Sub

    ''' <summary>
    ''' Allows the user to select an aggregation for each column (average, count, sum, etc.).
    ''' </summary>
    Public Sub SelectColumnTotals()
        If Me.ShowColumnTotalsToolStripComboBox.Text <> "Hide column totals" Then
            Me.DataTableGridEX.TotalRow = InheritableBoolean.True
            Me.DataTableGridEX.GroupTotals = GroupTotals.ExpandedGroup
            Select Case ShowColumnTotalsToolStripComboBox.Text
                Case "Avg"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Average
                    Next
                Case "Count"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Count
                    Next
                Case "Max"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Max
                    Next
                Case "Min"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Min
                    Next
                Case "Std. Dev."
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.StdDeviation
                    Next
                Case "Sum"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.Sum
                    Next
                Case "Value count"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.ValueCount
                    Next
                Case "None"
                    For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
                        Me.DataTableGridEX.RootTable.Columns(i).AggregateFunction = AggregateFunction.None
                    Next
            End Select
        Else
            Me.DataTableGridEX.TotalRow = InheritableBoolean.False
            Me.DataTableGridEX.GroupTotals = GroupTotals.Never
        End If
    End Sub

    Private Sub GroupTotalsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GroupTotalsToolStripComboBox.SelectedIndexChanged
        If Me.GroupTotalsToolStripComboBox.Text = "Always" Then
            Me.DataTableGridEX.GroupTotals = GroupTotals.Always
        ElseIf Me.GroupTotalsToolStripComboBox.Text = "Never" Then
            Me.DataTableGridEX.GroupTotals = GroupTotals.Never
        ElseIf Me.GroupTotalsToolStripComboBox.Text = "Expanded" Then
            Me.DataTableGridEX.GroupTotals = GroupTotals.ExpandedGroup
        Else
            Me.DataTableGridEX.GroupTotals = GroupTotals.Default
        End If
    End Sub

    ''' <summary>
    ''' Adds group totals to the DataTable grid.
    ''' </summary>
    Public Sub AddGroupTotals()
        'loop through the columns and add group totals
        For i As Integer = 0 To Me.DataTableGridEX.RootTable.Columns.Count - 1
            'add a group average header total
            Dim AvgGroupHeaderTotal As New GridEXGroupHeaderTotal()
            With AvgGroupHeaderTotal
                .AggregateFunction = AggregateFunction.Average
                .Column = Me.DataTableGridEX.RootTable.Columns(i)
                .Key = "AvgGroupHeaderTotal" + i.ToString
                .TotalPrefix = "Avg.="
                .TotalSuffix = ""
            End With
            Me.DataTableGridEX.RootTable.GroupHeaderTotals.Add(AvgGroupHeaderTotal)

            'add a group count header total
            Dim CountGroupHeaderTotal As New GridEXGroupHeaderTotal()
            With CountGroupHeaderTotal
                .AggregateFunction = AggregateFunction.Count
                .Column = Me.DataTableGridEX.RootTable.Columns(i)
                .Key = "CountGroupHeaderTotal" + i.ToString
                .TotalPrefix = "n="
                .TotalSuffix = ""
            End With
            Me.DataTableGridEX.RootTable.GroupHeaderTotals.Add(CountGroupHeaderTotal)

            'add a group sum header total
            Dim SumGroupHeaderTotal As New GridEXGroupHeaderTotal()
            With SumGroupHeaderTotal
                .AggregateFunction = AggregateFunction.Sum
                .Column = Me.DataTableGridEX.RootTable.Columns(i)
                .Key = "SumGroupHeaderTotal" + i.ToString
                .TotalPrefix = "Sum="
                .TotalSuffix = ""
            End With
            Me.DataTableGridEX.RootTable.GroupHeaderTotals.Add(SumGroupHeaderTotal)

            'add a group Max header total
            Dim MaxGroupHeaderTotal As New GridEXGroupHeaderTotal()
            With MaxGroupHeaderTotal
                .AggregateFunction = AggregateFunction.Max
                .Column = Me.DataTableGridEX.RootTable.Columns(i)
                .Key = "MaxGroupHeaderTotal" + i.ToString
                .TotalPrefix = "Max="
                .TotalSuffix = ""
            End With
            Me.DataTableGridEX.RootTable.GroupHeaderTotals.Add(MaxGroupHeaderTotal)

            'add a group Min header total
            Dim MinGroupHeaderTotal As New GridEXGroupHeaderTotal()
            With MinGroupHeaderTotal
                .AggregateFunction = AggregateFunction.Min
                .Column = Me.DataTableGridEX.RootTable.Columns(i)
                .Key = "MinGroupHeaderTotal" + i.ToString
                .TotalPrefix = "Min="
                .TotalSuffix = ""
            End With
            Me.DataTableGridEX.RootTable.GroupHeaderTotals.Add(MinGroupHeaderTotal)
        Next
    End Sub


    ''' <summary>
    ''' Highlights null and empty cells in the dataset grid
    ''' </summary>
    ''' <param name="SourceDataTable"></param>
    Public Sub FormatDataGrid(SourceDataTable As DataTable)
        'the purpose of the function is to highlight in colors data deficiencies. GridEX requires FormatStyles to assign to cells
        If Not SourceDataTable Is Nothing Then

            Dim BlankFormatStyle As New GridEXFormatStyle()
            With BlankFormatStyle
                .BackColor = Color.LightSalmon
            End With

            Dim NullFormatStyle As New GridEXFormatStyle()
            With NullFormatStyle
                .BackColor = Color.Red
            End With

            Dim NormalFormatStyle As New GridEXFormatStyle()
            With NormalFormatStyle
                .BackColor = Color.White
            End With

            Dim RedFormatStyle As New GridEXFormatStyle()
            With RedFormatStyle
                .BackColor = Color.Red
            End With

            'if the source datatable has rows
            If SourceDataTable.Rows.Count > 0 Then
                Dim r As Integer = 0 'row counter

                'loop through each row in the DataTable
                For Each Row As DataRow In SourceDataTable.Rows
                    Dim c As Integer = 0 'column counter

                    'loop through each column in Row
                    For Each Column As DataColumn In SourceDataTable.Columns

                        'make sure there is an accompanying GridEX cell
                        If Not Me.DataTableGridEX.GetRow(r).Cells(c) Is Nothing Then
                            'get a reference to the cell at the address r,c
                            Dim CurrentCell As GridEXCell = Me.DataTableGridEX.GetRow(r).Cells(c)

                            'format the cell depending on its contents
                            If Row.Item(c).ToString.Trim = "" Then
                                CurrentCell.FormatStyle = BlankFormatStyle
                            ElseIf IsDBNull(Row.Item(c)) Then
                                CurrentCell.FormatStyle = NullFormatStyle
                            Else
                                CurrentCell.FormatStyle = NormalFormatStyle
                            End If
                        End If
                        c = c + 1
                    Next
                    r = r + 1
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Highlights blanks and Nulls in the metadatagridview
    ''' </summary>
    Public Sub FormatMetadataDataGridView()
        If Me.MetadataDataGridView.Rows.Count > 0 Then
            For Each Row As DataGridViewRow In Me.MetadataDataGridView.Rows
                'highlight blanks
                If Row.Cells("BlankCount").Value > 0 Then
                    Row.Cells("BlankCount").Style.BackColor = ZeroLengthColor
                End If

                'highlight nulls
                If Row.Cells("NullCount").Value > 0 Then
                    Row.Cells("NullCount").Style.BackColor = NullColor
                End If
            Next
        End If

    End Sub

    Private Sub ExecuteSQLButton_Click(sender As Object, e As EventArgs) Handles ExecuteSQLButton.Click
        'load the mappings grid
        LoadMappingsGrid()

        'save the database connection info
        My.Settings.ConnectionString = Me.ConnectionStringTextBox.Text.Trim
        My.Settings.Query = Me.QueryTextBox.Text
    End Sub



    Private Sub ColumnsMappingDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles ColumnsMappingDataGridView.CellEndEdit
        'reconcile the automated column types to the quotedcolumn value, e.g. Autonumber should not be quoted but New GUID should
        For Each Row As DataRow In MappingsDataTable.Rows
            If Not IsDBNull(Row.Item("SourceColumnName")) Then
                If Row.Item("SourceColumnName") = "Autonumber" Then Row.Item("QuotedColumn") = False
                If Row.Item("SourceColumnName") = "New GUID" Then Row.Item("QuotedColumn") = True
                If Row.Item("SourceColumnName") = "Current Datetime" Then Row.Item("QuotedColumn") = True
                If Row.Item("SourceColumnName") = "Username" Then Row.Item("QuotedColumn") = True
            End If
        Next

        'generate the insert queries preview
        GenerateInsertQueries(3, Me.SkeeterDatasetTreeNode.DataTable.DefaultView)
    End Sub

    ''' <summary>
    ''' Generates INSERT queries based on the data source to destination table columns mappings selected in the mappings grid
    ''' </summary>
    ''' <param name="Iterations"></param>
    ''' <param name="DataView"></param>
    Private Sub GenerateInsertQueries(Iterations As Integer, DataView As DataView)
        Try
            'close out any changes to the mappings datatable
            Me.ColumnsMappingDataGridView.EndEdit()

            'build up some documentation on the script
            Dim Intro As String = "-- Data import script" & vbNewLine
            Intro = Intro & "-- Source file: " & Me._SkeeterDatasetTreeNode.FileInfo.FullName & vbNewLine
            Intro = Intro & "-- Generated on " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " by " & My.User.Name & vbNewLine & vbNewLine

            Intro = Intro & "USE Databasename;" & vbNewLine
            Intro = Intro & "-- Do not forget to ROLLBACK or COMMIT the transaction below or the database will be left in a hanging state" & vbNewLine
            Intro = Intro & "BEGIN TRANSACTION" & vbNewLine
            Me.SqlTextBox.Text = Intro & vbNewLine


            'ensure iterations is <= the number of rows
            If DataView.ToTable.Rows.Count < Iterations Then Iterations = DataView.ToTable.Rows.Count - 1

            'loop through the rows
            Dim Counter As Integer = 1
            For i As Integer = 0 To Iterations Step 1 ' Each SourceRow As DataRow In SqlSourceDataTable.Rows

                Dim Sql As String = ""

                'get the source dataset's row
                'this datatable is created in order to get the distinct columns 
                Dim DT As DataTable = DataView.ToTable()
                Dim SourceRow As DataRow = DT.Rows(i)
                'Dim SourceRow As DataRow = DataTable.Rows(i)

                'make sure the wkt column exists and set up sql text to make sure we get a valid polygon
                If SourceRow.Table.Columns.Contains("WKT") Then
                    Sql = Sql & vbNewLine
                    'Sql = Sql & "SET @geom ='" & SourceRow.Item("WKT") & "'" & vbNewLine
                    'Sql = Sql & "SET @geog = @geom.MakeValid().STUnion(@geom.STStartPoint()).STAsText()" & vbNewLine
                End If


                Sql = Sql & "INSERT INTO " & Me.SkeeterDatasetTreeNode.FileInfo.Name.Trim.Replace(Me.SkeeterDatasetTreeNode.FileInfo.Extension, "") & "("
                Dim InsertColumns As String = ""
                Dim ValuesList As String = ""

                'build a list of INSERT query columns from the checked columns in the mapping table
                If Not MappingsDataTable Is Nothing Then
                    'if we have records
                    If MappingsDataTable.Rows.Count > 0 Then
                        'loop through the mappings table row by row and build queries

                        For Each MappingRow As DataRow In MappingsDataTable.Rows
                            Try
                                'if the source and destination columns are each not blank
                                If MappingRow.Item("DestinationColumnName").ToString.Trim <> "" And MappingRow.Item("SourceColumnName").ToString.Trim <> "" Then
                                    'append the insert column name 
                                    InsertColumns = InsertColumns & MappingRow.Item("DestinationColumnName") & ","

                                    'values list
                                    Dim SourceRowColumnName As String = MappingRow.Item("SourceColumnName")


                                    'get the value of the source row item
                                    Dim SourceRowValue As String = ""
                                    'determine if we should use some defaults not
                                    If MappingRow.Item("SourceColumnName").ToString.Trim = "Default value" Then
                                        SourceRowValue = MappingRow.Item("DefaultValueColumn").ToString.Trim
                                    ElseIf MappingRow.Item("SourceColumnName").ToString.Trim = "New GUID" Then
                                        SourceRowValue = Guid.NewGuid().ToString
                                    ElseIf MappingRow.Item("SourceColumnName").ToString.Trim = "Autonumber" Then
                                        SourceRowValue = Counter
                                    ElseIf MappingRow.Item("SourceColumnName").ToString.Trim = "Current Datetime" Then
                                        SourceRowValue = Now.ToString
                                    ElseIf MappingRow.Item("SourceColumnName").ToString.Trim = "Username" Then
                                        SourceRowValue = My.User.Name
                                    Else
                                        SourceRowValue = SourceRow.Item(SourceRowColumnName).ToString.Trim

                                        'Convert blanks to NULL values
                                        If SourceRowValue.ToString.Trim = "" Then
                                            SourceRowValue = "NULL"
                                        End If
                                    End If

                                    'quote or not
                                    Dim Value As String
                                    If MappingRow.Item("QuotedColumn") = True Then
                                        Value = SourceRowValue.Trim
                                        If Not Value = "NULL" Then
                                            ValuesList = ValuesList & "'" & Value & "'" & ","
                                        Else
                                            ValuesList = ValuesList & Value & ","
                                        End If

                                    Else
                                        ValuesList = ValuesList & SourceRowValue.Trim & ","
                                    End If

                                End If
                            Catch ex As Exception
                                MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
                            End Try

                        Next
                    End If
                End If

                'trim trailing comma
                If InsertColumns.Trim.Length > 0 Then
                    InsertColumns = InsertColumns.Substring(0, Len(InsertColumns.Trim) - 1)
                End If

                'trim trailing comma
                If ValuesList.Trim.Length > 0 Then
                    ValuesList = ValuesList.Substring(0, Len(ValuesList.Trim) - 1)
                End If

                Sql = Sql & InsertColumns & ") VALUES(" & ValuesList & ");"
                Me.SqlTextBox.AppendText(Sql & vbNewLine)
                Counter = Counter + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub WrapToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WrapToolStripComboBox.SelectedIndexChanged
        If Me.WrapToolStripComboBox.Text = "True" Then Me.SqlTextBox.WordWrap = True Else Me.SqlTextBox.WordWrap = False
    End Sub

    Private Sub NumberOfPreviewQueriesToolStripTextBox_TextChanged(sender As Object, e As EventArgs) Handles NumberOfPreviewQueriesToolStripTextBox.TextChanged
        'change the number of queries to generate
        If IsNumeric(Me.NumberOfPreviewQueriesToolStripTextBox.Text) Then
            Dim NumberOfPreviewQueries As Integer = Me.NumberOfPreviewQueriesToolStripTextBox.Text
            If NumberOfPreviewQueries = 0 Then
                NumberOfPreviewQueries = Me.SkeeterDatasetTreeNode.DataTable.DefaultView.ToTable.Rows.Count - 1
            End If
            GenerateInsertQueries(NumberOfPreviewQueries, Me.SkeeterDatasetTreeNode.DataTable.DefaultView)
        End If
    End Sub

    Private Sub ExportToCSVToolStripButton_Click(sender As Object, e As EventArgs) Handles ExportToCSVToolStripButton.Click
        Dim CSVFilename As String = _SkeeterDatasetTreeNode.FileInfo.Name.Replace(_SkeeterDatasetTreeNode.FileInfo.Extension, ".csv")
        Dim CSVMetadataFilename As String = _SkeeterDatasetTreeNode.FileInfo.Name.Replace(_SkeeterDatasetTreeNode.FileInfo.Extension, ".csv.meta")
        ' Displays a SaveFileDialog so the user can save the file where needed
        Dim SFD As New SaveFileDialog()
        With SFD
            .AddExtension = True
            .Filter = "Comma separated values file|*.csv"
            .Title = "Save"
            .InitialDirectory = Me._SkeeterDatasetTreeNode.FileInfo.DirectoryName
            .FileName = CSVFilename
            .ShowDialog()
        End With

        ' If the file name is not an empty string open it for saving.  
        Try
            If SFD.FileName <> "" Then
                Dim SourceDataTable As DataTable = _SkeeterDatasetTreeNode.DataTable
                If Not SourceDataTable Is Nothing Then
                    File.WriteAllText(SFD.FileName, DataTableToCSV(SourceDataTable, ","))
                End If

                'metadata datatable
                If Not Me.MetadataDataGridView.DataSource Is Nothing Then
                    Dim MetadataDataTable As DataTable = Me.MetadataDataGridView.DataSource
                    File.WriteAllText(SFD.FileName.Trim & ".Metadata.csv", DataTableToCSV(MetadataDataTable, ","))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DataTableGridEX_SelectionChanged(sender As Object, e As EventArgs) Handles DataTableGridEX.SelectionChanged
        HighlightClickedColumnMetadata()
    End Sub

    Private Sub AutosizeColumnsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AutosizeColumnsToolStripComboBox.SelectedIndexChanged
        '2018-09-18 gridex is not obeying the below
        With Me.DataTableGridEX
            .ColumnAutoResize = True
            Select Case AutosizeColumnsToolStripComboBox.Text
                Case "All cells"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells
                Case "All cells and header"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.AllCellsAndHeader
                Case "Column header"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.ColumnHeader
                Case "Default"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.Default
                Case "Displayed cells"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.DiaplayedCells
                Case "Displayed cells and header"
                    .ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCellsAndHeader
            End Select
        End With

    End Sub


    Private Sub CreateTableQueryToolStripButton_Click(sender As Object, e As EventArgs) Handles CreateTableQueryToolStripButton.Click
        Try
            If Not _SkeeterDatasetTreeNode.DataTable Is Nothing Then
                Dim CurrentDataTable As DataTable = _SkeeterDatasetTreeNode.DataTable
                Dim CurrentDataview As DataView = CurrentDataTable.DefaultView
                Dim Filename As String = _SkeeterDatasetTreeNode.FileInfo.Name
                Dim InsertQuery As String = GetCreateTableQuery(CurrentDataview, Filename)
                Dim TextForm As New SkeeterTextForm(InsertQuery)
                TextForm.Show()
                TextForm.Text = "Insert query generated from " & Filename
            Else
                MsgBox("Select a data table.", MsgBoxStyle.Information, "No data table selected.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub DestinationDataGridView_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DestinationDataGridView.DataError
        Dim Row As Integer = e.RowIndex
        Dim Col As Integer = e.ColumnIndex
        Dim OffendingData As String = ""
        Dim Grid As DataGridView = Me.DestinationDataGridView
        If Not Grid Is Nothing Then
            If Not Grid.Rows(Row) Is Nothing Then
                If Not Grid.Rows(Row).Cells(Col) Is Nothing Then
                    If Not IsDBNull(Grid.Rows(Row).Cells(Col).Value) Then
                        OffendingData = "Row: " & Row & " Column: " & Grid.Columns(Col).Name & " Value: " & Grid.Rows(Row).Cells(Col).Value.ToString
                    End If
                End If
            End If
        End If
        MsgBox(e.Exception.Message & " " & OffendingData)
    End Sub

    Private Sub ShowFilterToolStripButton_Click(sender As Object, e As EventArgs) Handles ShowFilterToolStripButton.Click
        With Me.DataTableGridEX
            If .FilterMode = FilterMode.None Then
                .FilterMode = FilterMode.Automatic
                Me.ShowFilterToolStripButton.Text = "Hide filter"
            Else
                .FilterMode = FilterMode.None
                Me.ShowFilterToolStripButton.Text = "Show filter"
            End If
        End With
    End Sub

    Private Sub ShowGroupByBoxToolStripButton_Click(sender As Object, e As EventArgs) Handles ShowGroupByBoxToolStripButton.Click
        With Me.DataTableGridEX
            If .GroupByBoxVisible = True Then
                .GroupByBoxVisible = False
                Me.ShowGroupByBoxToolStripButton.Text = "Show group by box"
            Else
                .GroupByBoxVisible = True
                Me.ShowGroupByBoxToolStripButton.Text = "Hide group by box"
            End If
        End With
    End Sub

    'Private Sub ImportButton_Click(sender As Object, e As EventArgs) Handles ImportButton.Click
    '    'Dim DataFile As String = "C:\Temp\M3 2019 CAKR Census_group counts.csv"
    '    'ImportData(DataFile, SkeeterDatasetTreeNode.Dataset.t)

    '    'Build a SkeeterDataTablesTranslatorForm, load in the SourceDataTable from above, the destination DataTable, a title, instructions and the default values list.
    '    ' Dim TranslatorForm As New SkeeterDataTablesTranslator.SkeeterDataTablesTranslatorForm(SkeeterDataTableControl.s, DestinationDataTable, "Import muskox data", "Instructions", DefaultValuesList)

    'End Sub

    '''' <summary>
    '''' Imports data from DataFile into the destination data table.
    '''' </summary>
    '''' <param name="DataFile">Data file from which to import.</param>
    'Private Sub ImportData(DataFile As String, DestinationDataTable As DataTable)

    '    'The data file to import.
    '    Dim SourceFileInfo As New System.IO.FileInfo(DataFile)

    '    'The DataTable to hold the imported data.
    '    Dim SourceDataTable As DataTable = GetDataTableFromDelimitedTextFile(SourceFileInfo, ",")

    '    'A list of default values that should be available as options in the SkeeterDataTablesTranslatorForm
    '    Dim DefaultValuesList As New List(Of String)
    '    With DefaultValuesList
    '        .Add("Cape Thompson")
    '        .Add("Seward Peninsula")
    '        .Add("Raw")
    '        .Add("Provisional")
    '    End With

    '    'Build a SkeeterDataTablesTranslatorForm, load in the SourceDataTable from above, the destination DataTable, a title, instructions and the default values list.
    '    Dim TranslatorForm As New SkeeterDataTablesTranslator.SkeeterDataTablesTranslatorForm(SourceDataTable, DestinationDataTable, "Import muskox data", "Instructions", DefaultValuesList)

    '    'Show the form
    '    TranslatorForm.ShowDialog()

    '    'Build a DataTable to hold the transformed data that came out of the SkeeterDataTablesTranslatorForm
    '    Dim ImportDataTable As DataTable = TranslatorForm.DestinationDataTable

    '    'Load the transformed data into the destination DataTable by looping through the rows, creating new destination rows
    '    'and loading them with data.
    '    For Each Row As DataRow In ImportDataTable.Rows

    '        'make a new row
    '        Dim NewRow As DataRow = DestinationDataTable.NewRow
    '        For Each Column As DataColumn In ImportDataTable.Columns
    '            NewRow.Item(Column.ColumnName) = Row.Item(Column.ColumnName)
    '        Next

    '        'Override any selections made on the translator form
    '        NewRow.Item("RecordInsertedDate") = Now
    '        NewRow.Item("RecordInsertedBy") = My.User.Name
    '        NewRow.Item("ID") = Guid.NewGuid.ToString
    '        NewRow.Item("CertificationLevel") = "Raw"

    '        'add the row
    '        DestinationDataTable.Rows.Add(NewRow)
    '    Next
    'End Sub

    ''' <summary>
    ''' Converts a tab delimited text file to a DataTable
    ''' </summary>
    ''' <param name="DelimitedTextFileInfo">Tab delimited text file. FileInfo.</param>
    ''' <returns>DataTable</returns>
    Public Function GetDataTableFromDelimitedTextFile(DelimitedTextFileInfo As FileInfo, Delimiter As String) As DataTable
        Dim TDVDataTable As New DataTable(DelimitedTextFileInfo.Name)
        Try
            Dim MyTextFileParser As New FileIO.TextFieldParser(DelimitedTextFileInfo.FullName)
            MyTextFileParser.Delimiters = New String() {Delimiter}
            TDVDataTable.Columns.AddRange(Array.ConvertAll(MyTextFileParser.ReadFields, Function(s) New DataColumn With {.Caption = s, .ColumnName = s}))
            Do While Not MyTextFileParser.EndOfData
                TDVDataTable.Rows.Add(MyTextFileParser.ReadFields)
            Loop
            MyTextFileParser.Close()
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return TDVDataTable
    End Function
End Class
