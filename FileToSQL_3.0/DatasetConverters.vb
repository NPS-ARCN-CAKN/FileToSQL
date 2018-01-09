Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO

Module DatasetConverters
    ''' <summary>
    ''' Converts a DataTable to a string of delimited values such as CSV
    ''' </summary>
    ''' <param name="DataTable">DataTable to convert. DataTable</param>
    ''' <param name="Delimiter">Values separator</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Function DataTableToCSV(DataTable As DataTable, Delimiter As String) As String
        Dim CSV As String = ""
        Try
            'output the headers
            For Each Column As DataColumn In DataTable.Columns
                CSV = CSV & Column.ColumnName & Delimiter
            Next
            CSV = CSV.Substring(0, CSV.Trim.Length - 1) & vbNewLine

            'output the rows
            For Each Row As DataRow In DataTable.Rows
                For Each Column As DataColumn In DataTable.Columns
                    CSV = CSV & Row.Item(Column.ColumnName) & Delimiter
                Next
                CSV = CSV.Substring(0, CSV.Trim.Length - 1) & vbNewLine
            Next
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return CSV
    End Function


    ''' <summary>
    ''' Converts the submitted CSV text file into a DataTable
    ''' </summary>
    ''' <param name="CSVFileInfo">Input CSV FileInfo</param>
    ''' <param name="Headers">Whether the file has headers or not</param>
    ''' <returns>DataTable</returns>
    Public Function GetDataTableFromCSV(CSVFileInfo As FileInfo, Headers As Boolean) As DataTable
        Dim MyDataTable As New DataTable(CSVFileInfo.Name) 'this datatable will hold the imported data
        Try
            Dim CSVConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CSVFileInfo.DirectoryName & ";Extended Properties=""text;HDR=" & Headers & ";FMT=Delimited"";"
            Using MyOleDBDataAdapter As New OleDbDataAdapter("SELECT * FROM [" & CSVFileInfo.Name & "]", CSVConnectionString)
                MyOleDBDataAdapter.Fill(MyDataTable)
            End Using
            MyDataTable.TableName = CSVFileInfo.Name
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return MyDataTable
    End Function

    Public Function GetDatasetFromExcelWorkbook(ExcelFileInfo As FileInfo) As DataSet
        Dim ExcelDataset As New DataSet
        If My.Computer.FileSystem.FileExists(ExcelFileInfo.FullName) Then
            Try
                Dim ExcelConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & ExcelFileInfo.FullName & ";Extended Properties='Excel 12.0 Xml;HDR=YES';"
                ExcelDataset.DataSetName = ExcelFileInfo.Name
                Dim WorksheetsDataTable As DataTable = GetExcelWorksheets(ExcelConnectionString)
                For Each WorksheetRow As DataRow In WorksheetsDataTable.Rows
                    Dim WorksheetName As String = WorksheetRow.Item("TABLE_NAME")
                    Dim ExcelDataTable As New DataTable(WorksheetName)
                    ExcelDataTable.TableName = WorksheetName
                    Dim Sql As String = "SELECT * FROM [" & WorksheetName & "]"
                    Dim MyConnection As New OleDbConnection(ExcelConnectionString)
                    MyConnection.Open()
                    Dim MyCommand As New OleDbCommand(Sql, MyConnection)
                    Dim MyDataAdapter As New OleDbDataAdapter(MyCommand)
                    MyDataAdapter.Fill(ExcelDataTable)
                    ExcelDataset.Tables.Add(ExcelDataTable)
                    MyConnection.Close()
                Next
            Catch ex As Exception
                MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
            End Try
        Else
            MsgBox(ExcelFileInfo.FullName & " does not exist")
        End If
        Return ExcelDataset
    End Function

    ''' <summary>
    ''' Returns a DataTable of worksheets in the submitted Excel workbook
    ''' </summary>
    ''' <param name="ExcelConnectionString"></param>
    ''' <returns>DataTable</returns>
    Public Function GetExcelWorksheets(ExcelConnectionString As String) As DataTable
        Dim ExcelWorksheetsDataTable As New DataTable
        Try
            Dim MyConnection As New OleDbConnection(ExcelConnectionString)
            MyConnection.Open()
            ExcelWorksheetsDataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            MyConnection.Close()
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return ExcelWorksheetsDataTable
    End Function

    ''' <summary>
    ''' Returns a DataTable containing metadata about the structure of the submitted DataTable
    ''' </summary>
    ''' <param name="InputDataTable">DataTable to describe.</param>
    ''' <returns>DataTable</returns>
    Public Function GetMetadataDataTable(InputDataTable As DataTable) As DataTable
        Dim ColumnsDataTable As New DataTable("Columns")


        'column names column
        Dim ColumnNameColumn As New DataColumn
        With ColumnNameColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Column name"
            .ColumnName = "ColumnName"
        End With

        'ColumnDescription column
        Dim ColumnDescriptionColumn As New DataColumn
        With ColumnDescriptionColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Description"
            .ColumnName = "ColumnDescription"
        End With

        'data type column
        Dim DataTypeColumn As New DataColumn
        With DataTypeColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Data type"
            .ColumnName = "DataType"
        End With

        'units column
        Dim UnitsColumn As New DataColumn
        With UnitsColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Units"
            .ColumnName = "Units"
        End With

        'allowdbnull column
        Dim AllowDBNullColumn As New DataColumn
        With AllowDBNullColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "AllowDBNULL"
            .ColumnName = "AllowDBNull"
        End With

        'AutoIncrement column
        Dim AutoIncrementColumn As New DataColumn
        With AutoIncrementColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "AutoIncrement"
            .ColumnName = "AutoIncrement"
        End With

        'AutoIncrementSeed column
        Dim AutoIncrementSeedColumn As New DataColumn
        With AutoIncrementSeedColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "AutoIncrementSeed"
            .ColumnName = "AutoIncrementSeed"
        End With

        'AutoIncrementStep column
        Dim AutoIncrementStepColumn As New DataColumn
        With AutoIncrementStepColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "AutoIncrementStep"
            .ColumnName = "AutoIncrementStep"
        End With

        'Caption column
        Dim CaptionColumn As New DataColumn
        With CaptionColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Caption"
            .ColumnName = "Caption"
        End With

        'ColumnMapping column
        Dim ColumnMappingColumn As New DataColumn
        With ColumnMappingColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "ColumnMapping"
            .ColumnName = "ColumnMapping"
        End With

        'DateTimeMode column
        Dim DateTimeModeColumn As New DataColumn
        With DateTimeModeColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "DateTimeMode"
            .ColumnName = "DateTimeMode"
        End With

        'DefaultValue column
        Dim DefaultValueColumn As New DataColumn
        With DefaultValueColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "DefaultValue"
            .ColumnName = "DefaultValue"
        End With

        'Expression column
        Dim ExpressionColumn As New DataColumn
        With ExpressionColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Expression"
            .ColumnName = "Expression"
        End With

        'ExtendedProperties column
        'Dim ExtendedPropertiesColumn As New DataColumn
        'With ExtendedPropertiesColumn
        '    .DataType = System.Type.GetType("System.String")
        '    .Caption = "ExtendedProperties"
        '    .ColumnName = "ExtendedProperties"
        'End With

        'MaxLength column
        Dim MaxLengthColumn As New DataColumn
        With MaxLengthColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "MaxLength"
            .ColumnName = "MaxLength"
        End With

        'Table column
        Dim TableColumn As New DataColumn
        With TableColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Table"
            .ColumnName = "Table"
        End With

        'Unique column
        Dim UniqueColumn As New DataColumn
        With UniqueColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Unique"
            .ColumnName = "Unique"
        End With

        'add the columns to the datatable
        ColumnsDataTable.Columns.Add(ColumnNameColumn)
        ColumnsDataTable.Columns.Add(UnitsColumn)
        ColumnsDataTable.Columns.Add(CaptionColumn)
        ColumnsDataTable.Columns.Add(DataTypeColumn)

        ColumnsDataTable.Columns.Add(ColumnDescriptionColumn)

        ColumnsDataTable.Columns.Add(AllowDBNullColumn)
        ColumnsDataTable.Columns.Add(UniqueColumn)
        ColumnsDataTable.Columns.Add(DefaultValueColumn)
        ColumnsDataTable.Columns.Add(AutoIncrementColumn)
        ColumnsDataTable.Columns.Add(AutoIncrementSeedColumn)
        ColumnsDataTable.Columns.Add(AutoIncrementStepColumn)
        ColumnsDataTable.Columns.Add(ColumnMappingColumn)
        ColumnsDataTable.Columns.Add(ExpressionColumn)
        'ColumnsDataTable.Columns.Add(ExtendedPropertiesColumn)
        ColumnsDataTable.Columns.Add(MaxLengthColumn)
        ColumnsDataTable.Columns.Add(TableColumn)
        ColumnsDataTable.Columns.Add(DateTimeModeColumn)

        Try
            If Not InputDataTable Is Nothing Then
                'load the columns data table with info about the input data table
                For Each Column As DataColumn In InputDataTable.Columns
                    Dim NewRow As DataRow = ColumnsDataTable.NewRow
                    NewRow.Item("ColumnName") = Column.ColumnName
                    NewRow.Item("DataType") = Column.DataType.ToString.Replace("System.", "")
                    NewRow.Item("AllowDBNull") = Column.AllowDBNull
                    NewRow.Item("DefaultValue") = Column.DefaultValue
                    NewRow.Item("AutoIncrement") = Column.AutoIncrement
                    NewRow.Item("AutoIncrementSeed") = Column.AutoIncrementSeed
                    NewRow.Item("AutoIncrementStep") = Column.AutoIncrementStep
                    NewRow.Item("Caption") = Column.Caption
                    NewRow.Item("ColumnDescription") = Column.Caption
                    NewRow.Item("ColumnMapping") = Column.ColumnMapping
                    NewRow.Item("Expression") = Column.Expression
                    'NewRow.Item("ExtendedProperties") = Column.ExtendedProperties
                    NewRow.Item("MaxLength") = Column.MaxLength
                    NewRow.Item("Table") = Column.Table
                    NewRow.Item("Unique") = Column.Unique
                    NewRow.Item("DateTimeMode") = Column.DateTimeMode

                    'add the row to the table
                    ColumnsDataTable.Rows.Add(NewRow)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try

        'return the metadata datatable
        Return ColumnsDataTable
    End Function

    Public Function GetMappingsDataTable() As DataTable
        Dim MappingsDataTable As New DataTable("Mappings")

        'build a column names column
        Dim DestinationColumnName As New DataColumn
        With DestinationColumnName
            .DataType = System.Type.GetType("System.String")
            .Caption = "Destination column"
            .ColumnName = "DestinationColumnName"
        End With

        'build a source column name column
        Dim SourceColumnName As New DataColumn
        With SourceColumnName
            .DataType = System.Type.GetType("System.String")
            .Caption = "Source column"
            .ColumnName = "SourceColumnName"
        End With

        'build a default value column
        Dim DefaultValueColumn As New DataColumn
        With DefaultValueColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Default value"
            .ColumnName = "DefaultValueColumn"
        End With

        'build a boolean 'quoted' column
        Dim QuotedColumn As New DataColumn
        With QuotedColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Quoted"
            .ColumnName = "QuotedColumn"
            .DefaultValue = False
        End With

        'add the columns to the datatable
        MappingsDataTable.Columns.Add(DestinationColumnName)
        MappingsDataTable.Columns.Add(SourceColumnName)
        MappingsDataTable.Columns.Add(DefaultValueColumn)
        MappingsDataTable.Columns.Add(QuotedColumn)
        Return MappingsDataTable
    End Function

    Public Function GetSQLServerDatabaseTable(ConnectionString As String, Sql As String) As DataTable
        Dim MyDataTable As New DataTable
        Try
            'make a SqlConnection using the supplied ConnectionString 
            Dim MySqlConnection As New SqlConnection(ConnectionString)
            Using MySqlConnection
                'make a query using the supplied Sql
                Dim MySqlCommand As SqlCommand = New SqlCommand(Sql, MySqlConnection)

                'open the connection
                MySqlConnection.Open()

                'create a DataReader and execute the SqlCommand
                Dim MyDataReader As SqlDataReader = MySqlCommand.ExecuteReader()

                'load the reader into the datatable
                MyDataTable.Load(MyDataReader)

                'clean up
                MyDataReader.Close()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return MyDataTable
    End Function

    ''' <summary>
    ''' Accepts a ConnectionString and returns a DataTable of table names
    ''' </summary>
    ''' <param name="ConnectionString">ConnectionString</param>
    ''' <returns>DataTable</returns>
    Public Function GetDatabaseTables(ConnectionString As String) As DataTable
        Dim MyDataTable As New DataTable
        Try
            Dim MyConnection As New SqlConnection(ConnectionString)
            Using MyConnection
                MyConnection.Open()
                Dim schemaTable As DataTable = MyConnection.GetSchema("Tables")
                Return schemaTable
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        Return MyDataTable
    End Function
End Module

