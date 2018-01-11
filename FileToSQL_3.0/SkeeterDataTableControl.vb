Public Class SkeeterDataTableControl

    'datatable to hold the source to destination columns mappings
    Dim MappingsDataTable As DataTable

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
        'get the name of the datagridview's clicked column
        Dim Index As Integer = Me.DataTableDataGridView.CurrentCell.ColumnIndex
        Dim ClickedColumn As DataGridViewColumn = DataTableDataGridView.Columns(Index)
        Dim ClickedColumnName As String = ClickedColumn.Name

        'loop through the metadata DGV's columns looking for a match.  Select a match.
        Dim RowIndex As Integer = 0
        For Each Row As DataGridViewRow In MetadataDataGridView.Rows
            If Row.Cells("ColumnName").Value = ClickedColumnName Then
                Me.MetadataDataGridView.Rows(RowIndex).Selected = True
            Else
                Me.MetadataDataGridView.Rows(RowIndex).Selected = False
            End If
            RowIndex = RowIndex + 1
        Next

    End Sub

    Private Sub ConnectButton_Click(sender As Object, e As EventArgs) Handles ConnectButton.Click
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
        'Me.ColumnsMappingDataGridView.AutoGenerateColumns = False
        'For Each column As DataGridViewColumn In Me.ColumnsMappingDataGridView.Columns
        '    Debug.Print(column.DataPropertyName & " " & column.CellType.ToString)
        'Next
        'Exit Sub
        Dim MetadataDataTable As DataTable = Me.MetadataDataGridView.DataSource
        Dim SourceColumnNameDataGridViewComboBoxColumn As DataGridViewComboBoxColumn = Me.ColumnsMappingDataGridView.Columns("SourceColumnName")
        With SourceColumnNameDataGridViewComboBoxColumn
            .Items.Add("Default value")
            For Each Row As DataRow In MetadataDataTable.Rows
                .Items.Add(Row.Item("ColumnName"))
            Next
        End With


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

    Private Sub ExecuteSQLButton_Click(sender As Object, e As EventArgs) Handles ExecuteSQLButton.Click
        LoadMappingsGrid()
    End Sub

    Private Sub ConnectionStringTextBox_TextChanged(sender As Object, e As EventArgs) Handles ConnectionStringTextBox.TextChanged
        Me.ConnectButton.Enabled = Me.ConnectionStringTextBox.Text.Length > 0
        Me.ExecuteSQLButton.Enabled = Me.ConnectButton.Enabled
    End Sub

    Private Sub ColumnsMappingDataGridView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles ColumnsMappingDataGridView.CellEndEdit
        'Dim Sql As String = "INSERT INTO " & _SkeeterDatasetTreeNode.FileInfo.Name & "() VALUES();"
        'Me.OutputTextBox.Text = Sql
        GenerateInsertQueries(3, Me.SkeeterDatasetTreeNode.DataTable.DefaultView)
    End Sub

    Private Sub GenerateInsertQueries(Iterations As Integer, DataView As DataView)
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
        For i As Integer = 0 To Iterations Step 1 ' Each SourceRow As DataRow In SqlSourceDataTable.Rows
            'Try
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


            Sql = Sql & "INSERT INTO " & Me.SkeeterDatasetTreeNode.FileInfo.Name & "("
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
                                'determine if we should use the default value or not
                                If MappingRow.Item("SourceColumnName").ToString.Trim <> "Default value" Then
                                    SourceRowValue = SourceRow.Item(SourceRowColumnName).ToString.Trim

                                    'Convert blanks to NULL values
                                    If SourceRowValue.ToString.Trim = "" Then
                                        SourceRowValue = "NULL"
                                    End If
                                Else
                                    SourceRowValue = MappingRow.Item("DefaultValueColumn").ToString.Trim
                                End If

                                'quote or not
                                If MappingRow.Item("QuotedColumn") = True Then
                                    Dim Value As String = SourceRowValue.Trim
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
            'i = i + 1
        Next
        'Catch ex As Exception
        '    MsgBox(ex.Message & " " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        'End Try
    End Sub
End Class
