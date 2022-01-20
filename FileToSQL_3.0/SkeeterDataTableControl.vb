Imports System.Data.SqlClient
Imports System.IO
Imports Janus.Windows.GridEX
Imports SkeeterDataTablesTranslator

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
            LoadMappingsGrid(SkeeterDatasetTreeNode.FileInfo.Name)
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the source datatable to destination datatable columns mappings grid.
    ''' </summary>
    Private Sub LoadMappingsGrid(SourceFilename As String)

        'set up a DGV for the destination datatable
        Dim DestinationDataTable As DataTable

        Try
            'set up the mappings DGV with a new empty table
            MappingsDataTable = GetMappingsDataTable()

            'get the destination database datatable
            Dim Sql As String = Me.QueryTextBox.Text.Trim

            'Build a connectionstring builder so we can get the database name to use later in building the insert queries script
            Dim MyConnectionStringBuilder As New SqlConnectionStringBuilder(Me.ConnectionStringTextBox.Text.Trim)
            My.Settings.CurrentDatabaseName = MyConnectionStringBuilder.InitialCatalog

            'Get the name of the database table from the Sql
            My.Settings.CurrentTableName = GetTableNameFromSelectQuery(Sql)

            'Get the index of the next space after the FROM
            DestinationDataTable = GetSQLServerDatabaseTable(Me.ConnectionStringTextBox.Text, Sql)
            Me.DestinationDataGridView.DataSource = DestinationDataTable

            Dim DefaultValuesList As New List(Of String)
            With DefaultValuesList
                .Add(SourceFilename)
            End With

            Dim TranslatorForm As New SkeeterDataTablesTranslatorForm(SkeeterDatasetTreeNode.DataTable, DestinationDataTable, "Import", "Map the source columns to destination columns.", DefaultValuesList)
            TranslatorForm.ShowDialog()
            DestinationDataTable = TranslatorForm.DestinationDataTable
            Me.DestinationDataGridView.DataSource = DestinationDataTable

            'If we have a destination data table with rows then generate the insert queries
            If DestinationDataTable.Rows.Count > 0 Then
                GenerateInsertQueries(DestinationDataTable.DefaultView.Count, DestinationDataTable.DefaultView)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    ''' <summary>
    ''' Returns the name of the data table from an SQL SELECT query.
    ''' </summary>
    ''' <param name="SelectQuery">SQL SELECT query. String</param>
    ''' <returns>Table name. String</returns>
    Public Function GetTableNameFromSelectQuery(SelectQuery As String) As String
        Dim Tablename As String = "TableName"
        SelectQuery = SelectQuery.Trim
        SelectQuery = SelectQuery.Replace("  ", " ") 'get rid of double spaces
        SelectQuery = SelectQuery.Replace("   ", "") 'get rid of triple spaces
        SelectQuery = SelectQuery.Replace("    ", "") 'get rid of quadruple spaces
        SelectQuery = SelectQuery.Replace("    ", "") 'get rid of quintuple spaces
        SelectQuery = SelectQuery.Replace(vbTab, " ") 'convert tabs to space
        Try
            'Get the table name from the SQL
            Dim Key = "FROM"
            'key is case sensitive, convert sql to upper
            SelectQuery = SelectQuery.ToUpper
            Dim words = SelectQuery.Trim.Split(" "c)
            Dim i = Array.IndexOf(words, Key)
            If i <> -1 AndAlso i <> words.Length - 1 Then
                Tablename = words(i + 1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return Tablename.Trim
    End Function

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
        LoadMappingsGrid(SkeeterDatasetTreeNode.FileInfo.Name)

        'save the database connection info
        My.Settings.ConnectionString = Me.ConnectionStringTextBox.Text.Trim
        My.Settings.Query = Me.QueryTextBox.Text
    End Sub



    Private Sub ColumnsMappingDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
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
            'clear the insert queries list text box
            Me.SqlTextBox.Text = ""

            'build up some documentation on the script
            Dim Intro As String = "-- Data import script" & vbNewLine
            Intro = Intro & "-- Source file: " & Me._SkeeterDatasetTreeNode.FileInfo.FullName & vbNewLine
            Intro = Intro & "-- Generated on " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " by " & My.User.Name & vbNewLine & vbNewLine

            Intro = Intro & "USE " & My.Settings.CurrentDatabaseName.Trim & ";" & vbNewLine
            Intro = Intro & "-- Do not forget to ROLLBACK or COMMIT the transaction below or the database will be left in a hanging state" & vbNewLine
            Intro = Intro & "BEGIN TRANSACTION" & vbNewLine
            Me.SqlTextBox.Text = Intro & vbNewLine

            Dim Counter As Integer = 1
            For Each SourceRow As DataRow In DataView.ToTable.Rows
                Dim Sql As String = ""
                Sql = Sql & "INSERT INTO [" & My.Settings.CurrentTableName.Trim & "]("
                Dim InsertColumns As String = ""
                Dim ValuesList As String = ""

                'build up the column names list and values list
                For Each Col As DataColumn In DataView.ToTable.Columns
                    InsertColumns = InsertColumns & "[" & Col.ColumnName.Trim & "],"
                    Dim Value As String = SourceRow.Item(Col.ColumnName).ToString.Trim

                    'Convert boolean values to 1s or zeroes
                    If Col.DataType.ToString.Contains("Boolean") Then
                        If Value.Trim.ToLower = "true" Or Value.Trim.ToLower = "yes" Then Value = 1
                        If Value.Trim.ToLower = "false" Then Value = 0
                    End If

                    'if the cell value is empty make it NULL
                    If Value = "" Or IsDBNull(Value) = True Then
                        Value = "NULL"
                    End If

                    Dim Quote As String = ""
                    If Value = "NULL" Then
                        'no need to quote NULL values
                        ValuesList = ValuesList & Value & ","
                    Else
                        'quote the value if it's a string or date or text date type
                        If Col.DataType.ToString.Contains("String") Or Col.DataType.ToString.Contains("Date") Then
                            Quote = "'"
                        End If
                        'Replace single quotes with double single quotes to escape them in SQL
                        ValuesList = ValuesList & Quote & Value.Replace("'", "''") & Quote & "," 'also escape any single quotes with two single quotes

                    End If


                Next

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

            'bring the sql tab page forward
            Me.DataTableTabControl.SelectedTab = MappingTabPage
        Catch ex As Exception
            MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub WrapToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles WrapToolStripComboBox.SelectedIndexChanged
        If Me.WrapToolStripComboBox.Text = "True" Then Me.SqlTextBox.WordWrap = True Else Me.SqlTextBox.WordWrap = False
    End Sub

    Private Sub NumberOfPreviewQueriesToolStripTextBox_TextChanged(sender As Object, e As EventArgs) Handles NumberOfPreviewQueriesToolStripTextBox.TextChanged
        'change the number of queries to generate
        'If IsNumeric(Me.NumberOfPreviewQueriesToolStripTextBox.Text) Then
        '    Dim NumberOfPreviewQueries As Integer = Me.NumberOfPreviewQueriesToolStripTextBox.Text
        '    If NumberOfPreviewQueries = 0 Then
        '        NumberOfPreviewQueries = Me.SkeeterDatasetTreeNode.DataTable.DefaultView.ToTable.Rows.Count - 1
        '    End If
        '    GenerateInsertQueries(NumberOfPreviewQueries, Me.SkeeterDatasetTreeNode.DataTable.DefaultView)
        '    GenerateInsertQueries(1, DestinationDataTable.DefaultView)
        'End If
    End Sub

    Private Sub ExportToCSVToolStripButton_Click(sender As Object, e As EventArgs) Handles ExportToCSVToolStripButton.Click

        'Dim CSVFilename As String = _SkeeterDatasetTreeNode.FileInfo.Name.Replace(_SkeeterDatasetTreeNode.FileInfo.Extension, ".csv")
        Dim CSVFilename As String = Me.SkeeterDatasetTreeNode.Text.Replace("$", "").Trim & ".csv"

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
            'set the datasource and retrieve structure before setting autosize attributes
            .ColumnAutoResize = False
            .AutoSizeColumns(.RootTable)
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

    Private Sub SaveScriptToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveScriptToolStripButton.Click
        Try
            Dim FileFilter As String = "SQL files (*.sql)|(*.sql)"
            Dim FileExtension As String = "sql"
            Dim CurrentFilename As String = Me.SkeeterDatasetTreeNode.FileInfo.Name.Trim.Replace(Me.SkeeterDatasetTreeNode.FileInfo.Extension, "")

            'Open a save file dialog to allow the user to save the file someplace
            Dim SFD As New SaveFileDialog
            With SFD
                .AddExtension = True
                .DefaultExt = FileExtension
                .FileName = CurrentFilename.Trim & "." & FileExtension
                .Filter = FileFilter
            End With

            'Show the dialog
            If SFD.ShowDialog = DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SFD.FileName, SqlTextBox.Text.Trim, False)
            End If


        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
End Class
