Imports System.IO

Public Class SkeeterDataTableControl

    'datatable to hold the source to destination columns mappings
    Dim MappingsDataTable As DataTable

    Dim NullColor As System.Drawing.Color = Color.Gainsboro
    Dim ZeroLengthColor As System.Drawing.Color = Color.WhiteSmoke

    Private _SkeeterDatasetTreeNode As SkeeterDatasetTreeNode
    Public Property SkeeterDatasetTreeNode() As SkeeterDatasetTreeNode
        Get
            Return _SkeeterDatasetTreeNode
        End Get
        Set(ByVal value As SkeeterDatasetTreeNode)
            _SkeeterDatasetTreeNode = value
        End Set
    End Property



    Private Sub DataTableDataGridView_SelectionChanged(sender As Object, e As EventArgs) Handles DataTableDataGridView.SelectionChanged
        HighlightClickedColumnMetadata()
    End Sub

    Private Sub HighlightClickedColumnMetadata()
        'get the name of the datagridview's clicked column
        If Not Me.DataTableDataGridView.CurrentCell Is Nothing Then
            Dim Index As Integer = Me.DataTableDataGridView.CurrentCell.ColumnIndex
            Dim ClickedColumn As DataGridViewColumn = DataTableDataGridView.Columns(Index)
            Dim ClickedColumnName As String = ClickedColumn.Name

            'loop through the metadata DGV's columns looking for a match.  Select a match.
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
        'we're starting over so clear the controls
        'Me.DataTableDataGridView.DataSource = Nothing

        Try
            'get the tables in the connectionstring's database into a datatable
            'Dim DatabaseTablesDataTable As DataTable = GetDatabaseTables(Me.ConnectionStringTextBox.Text)
            ''load the table names into the combobox
            'For Each Row As DataRow In DatabaseTablesDataTable.Rows
            '    Me.TableComboBox.Items.Add(Row.Item("TABLE_NAME"))
            'Next
            'Me.TableComboBox.Refresh()
            LoadMappingsGrid()
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub LoadMappingsGrid()
        'set up a DGV for the destination datatable
        Dim DestinationDataTable As DataTable

        'set up the mappings DGV with a new empty table
        MappingsDataTable = GetMappingsDataTable()

        Me.ColumnsMappingDataGridView.DataSource = MappingsDataTable

        'get the destination datatable
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
        Try
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Public Sub FormatSourceDataGridView(SourceDataTable As DataTable)
        If SourceDataTable.Rows.Count > 0 Then
            Dim r As Integer = 0 'row counter
            For Each Row As DataRow In SourceDataTable.Rows
                Dim c As Integer = 0 'column counter
                For Each Column As DataColumn In SourceDataTable.Columns

                    If Row.Item(c).ToString.Trim = "" Then
                        Me.DataTableDataGridView.Rows(r).Cells(c).Style.BackColor = ZeroLengthColor
                    ElseIf IsDBNull(Row.Item(c)) Then
                        Me.DataTableDataGridView.Rows(r).Cells(c).Style.BackColor = NullColor
                    Else
                        Me.DataTableDataGridView.Rows(r).Cells(c).Style.BackColor = Color.White
                    End If
                    c = c + 1
                Next
                r = r + 1
            Next
        End If

    End Sub

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
            'these are needed to make invalid polygon geographies valid later in the script
            'Intro = Intro & "DECLARE @geom GEOMETRY" & vbNewLine
            'Intro = Intro & "DECLARE @geog GEOGRAPHY" & vbNewLine
            Me.SqlTextBox.Text = Intro & vbNewLine



            'ensure iterations is <= the number of rows
            'If SourceDataset.DataTable.Rows.Count < Iterations Then Iterations = SourceDataset.DataTable.Rows.Count - 1
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
                    File.WriteAllText(SFD.FileName.Trim & ".Metadata.txt", DataTableToCSV(MetadataDataTable, ","))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub AutosizeColumnsToolStripComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles AutosizeColumnsToolStripComboBox.SelectedIndexChanged
        'change the autosizing columns mode of the main data datagridview
        Select Case Me.AutosizeColumnsToolStripComboBox.Text
            Case "Column header"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader
            Case "All cells except header"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader
            Case "All cells"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
            Case "Displayed cells except header"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader
            Case "Displayed cells"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Case "Fill"
                Me.DataTableDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End Select
    End Sub
End Class
