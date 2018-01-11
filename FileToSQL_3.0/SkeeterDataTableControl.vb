Public Class SkeeterDataTableControl

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
        Dim MappingsDataTable As DataTable = GetMappingsDataTable()

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
        Dim Sql As String = "INSERT INTO " & _SkeeterDatasetTreeNode.FileInfo.Name & "() VALUES();"
        Me.OutputTextBox.Text = Sql
    End Sub
End Class
