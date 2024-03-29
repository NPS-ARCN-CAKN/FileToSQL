﻿Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports ExcelDataReader

''' <summary>
''' Functions that convert data file formats to Datasets
''' </summary>
Module DatasetConverters

    ''' <summary>
    ''' Format is used in ConnectionStrings to determine if the data is delimited or fixed width
    ''' </summary>
    Enum Format
        Delimited = 0
        Fixed = 1
    End Enum


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
    ''' Converts a tab delimited text file to a DataTable
    ''' </summary>
    ''' <param name="TDVFileInfo">Tab delimited text file. FileInfo.</param>
    ''' <returns>DataTable</returns>
    Public Function GetDataTableFromTabDelimitedTextFile(TDVFileInfo As FileInfo) As DataTable
        Dim TDVDataTable As New DataTable(TDVFileInfo.Name)
        Try
            Dim MyTextFileParser As New FileIO.TextFieldParser(TDVFileInfo.FullName)
            MyTextFileParser.Delimiters = New String() {vbTab}
            TDVDataTable.Columns.AddRange(Array.ConvertAll(MyTextFileParser.ReadFields, Function(s) New DataColumn With {.Caption = s, .ColumnName = s}))
            Do While Not MyTextFileParser.EndOfData
                TDVDataTable.Rows.Add(MyTextFileParser.ReadFields)
            Loop
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return TDVDataTable
    End Function

    ''' <summary>
    ''' Converts an XML file into a Dataset
    ''' </summary>
    ''' <param name="XMLFileInfo">XML file</param>
    ''' <returns>Dataset</returns>
    Public Function GetDatasetFromXMLFile(XMLFileInfo As FileInfo) As DataSet
        Dim XMLDataset As New DataSet(XMLFileInfo.Name)
        Try
            XMLDataset.ReadXml(XMLFileInfo.FullName)
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return XMLDataset
    End Function

    Public Function GetDatasetFromTextFile(CSVFileInfo As FileInfo, Headers As Boolean, Format As Format) As DataSet
        Dim CSVDataset As New DataSet(CSVFileInfo.Name)
        'Dim FMT As String = ""
        'Select Case Format
        '    Case 0
        '        FMT = "Delimited"
        '    Case 1
        '        FMT = "Fixed"
        'End Select
        Try
            CSVDataset.Tables.Add(GetDataTableFromCSV(CSVFileInfo, Headers, Format))
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return CSVDataset
    End Function


    ''' <summary>
    ''' Converts the submitted CSV text file into a DataTable
    ''' </summary>
    ''' <param name="CSVFileInfo">Input CSV FileInfo</param>
    ''' <param name="Headers">Whether the file has headers or not</param>
    ''' <param name="Format"></param>
    ''' <returns>DataTable</returns>
    Public Function GetDataTableFromCSV(CSVFileInfo As FileInfo, Headers As Boolean, Format As Format) As DataTable
        Dim MyDataTable As New DataTable(CSVFileInfo.Name) 'this datatable will hold the imported data
        Dim FMT As String = ""
        Select Case Format
            Case 0
                FMT = "Delimited"
            Case 1
                FMT = "Fixed"
        End Select
        Try
            Dim CSVConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & CSVFileInfo.DirectoryName & ";Extended Properties=""text;HDR=" & Headers & ";FMT=" & FMT & """;"
            Using MyOleDBDataAdapter As New OleDbDataAdapter("SELECT * FROM [" & CSVFileInfo.Name & "]", CSVConnectionString)
                MyOleDBDataAdapter.Fill(MyDataTable)
            End Using
            MyDataTable.TableName = CSVFileInfo.Name
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return MyDataTable
    End Function

    ''' <summary>
    ''' Converts a DBF file into a Dataset
    ''' </summary>
    ''' <param name="DBFFileInfo">DBF File to convert</param>
    ''' <returns>Dataset</returns>
    Public Function GetDatasetFromDBF(DBFFileInfo As FileInfo) As DataSet
        Dim DBFDataset As New DataSet(DBFFileInfo.Name)
        Try
            DBFDataset.Tables.Add(GetDataTableFromDBF(DBFFileInfo))
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name & ")")
        End Try
        Return DBFDataset
    End Function

    ''' <summary>
    ''' Converts a DBF file into a DataTable
    ''' </summary>
    ''' <param name="DBFFileInfo">DBF FileInfo to convert</param>
    ''' <returns>DataTable</returns>
    Public Function GetDataTableFromDBF(DBFFileInfo As FileInfo) As DataTable
        Dim DBFDataTable As New DataTable(DBFFileInfo.Name)
        'dbf file
        Try
            'we'll need the dbf filename and path separately so isolate them here
            Dim DBFDirectory As String = DBFFileInfo.DirectoryName 'the dbf directory

            'NOTE: the dbf query won't work on long filenames so copy the dbf to a temporary file named zzzzzzzz.dbf and work with that
            Dim TemporaryDBFFilename As String = "zzzzzzzz.dbf"
            Dim TemporaryDBFFileFullName As String = DBFDirectory & "\" & TemporaryDBFFilename
            My.Computer.FileSystem.CopyFile(DBFFileInfo.FullName, TemporaryDBFFileFullName, True)

            'connect to the temporary dbf and open the contents into a datareader
            Dim MyConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBFDirectory & ";Extended Properties=dBASE IV;User ID=Admin;Password=;"
            Dim MyConnection As New OleDbConnection(MyConnectionString)
            MyConnection.Open()
            Dim Sql As String = "SELECT * FROM [" & TemporaryDBFFilename.Replace(".dbf", "") & "]"
            Dim MyCommand As New OleDbCommand(Sql, MyConnection)
            Dim MyDataReader As OleDbDataReader = MyCommand.ExecuteReader()

            'load the data from the datareader into the datatable
            DBFDataTable.Load(MyDataReader)

            'close the connection
            MyConnection.Close()

            'delete the temporary dbf file
            My.Computer.FileSystem.DeleteFile(TemporaryDBFFileFullName)
        Catch ex As Exception
            MsgBox("Could not import waypoints from DBF file. " & ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return DBFDataTable
    End Function

    ''' <summary>
    ''' Converts an Excel spreadsheet into a Dataset with each worksheet as a DataTable. Uses the ExcelDataReader library (https://github.com/ExcelDataReader/ExcelDataReader)
    ''' </summary>
    ''' <param name="ExcelFile"></param>
    ''' <returns></returns>
    Public Function GetDatasetFromExcel(ExcelFile As String) As DataSet
        Dim ExcelDataset As New DataSet
        Try
            If My.Computer.FileSystem.FileExists(ExcelFile) Then
                Dim Stream = File.Open(ExcelFile, FileMode.Open, FileAccess.Read)
                Dim Reader = ExcelDataReader.ExcelReaderFactory.CreateReader(Stream)
                ExcelDataset = Reader.AsDataSet(New ExcelDataReader.ExcelDataSetConfiguration() With {
                    .UseColumnDataType = True,
                    .FilterSheet = Function(tableReader, sheetIndex) True,
                    .ConfigureDataTable = Function(tableReader) New ExcelDataReader.ExcelDataTableConfiguration() With {
                        .EmptyColumnNamePrefix = "Column",
                        .UseHeaderRow = True,
                        .ReadHeaderRow = Function(rowReader)
                                             rowReader.Read()
                                         End Function,
                        .FilterRow = Function(rowReader) True,
                        .FilterColumn = Function(rowReader, columnIndex) True
                    }
                })
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " (" & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return ExcelDataset
    End Function

    Public Function GetDatasetFromExcelWorkbook(ExcelConnectionString As String) As DataSet
        Dim ExcelDataset As New DataSet
        Try
            Dim WorksheetsDataTable As DataTable = GetExcelWorksheets(ExcelConnectionString)
            For Each WorksheetRow As DataRow In WorksheetsDataTable.Rows
                Dim WorksheetName As String = WorksheetRow.Item("TABLE_NAME")
                Dim ExcelDataTable As New DataTable(WorksheetName)
                ExcelDataTable.TableName = WorksheetName.Trim
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

        'Average column
        Dim AverageColumn As New DataColumn
        With AverageColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Average"
            .ColumnName = "Average"
        End With

        'Maximum column
        Dim MaximumColumn As New DataColumn
        With MaximumColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Maximum"
            .ColumnName = "Maximum"
        End With

        'Minimum column
        Dim MinimumColumn As New DataColumn
        With MinimumColumn
            .DataType = System.Type.GetType("System.String")
            .Caption = "Minimum"
            .ColumnName = "Minimum"
        End With

        'NullCount column
        Dim NullCountColumn As New DataColumn
        With NullCountColumn
            .DataType = System.Type.GetType("System.Int32")
            .Caption = "Nulls"
            .ColumnName = "NullCount"
        End With

        'NotNullCount column
        Dim FilledCountColumn As New DataColumn
        With FilledCountColumn
            .DataType = System.Type.GetType("System.Int32")
            .Caption = "Not Nulls"
            .ColumnName = "FilledCount"
        End With

        'Blank Count column
        Dim BlankCountColumn As New DataColumn
        With BlankCountColumn
            .DataType = System.Type.GetType("System.Int32")
            .Caption = "Blanks"
            .ColumnName = "BlankCount"
        End With

        'Count column
        Dim CountColumn As New DataColumn
        With CountColumn
            .DataType = System.Type.GetType("System.Int32")
            .Caption = "Count"
            .ColumnName = "Count"
        End With

        'add the columns to the datatable
        With ColumnsDataTable.Columns
            .Add(ColumnNameColumn)
            .Add(UnitsColumn)
            .Add(CaptionColumn)
            .Add(DataTypeColumn)
            .Add(AverageColumn)
            .Add(MinimumColumn)
            .Add(MaximumColumn)
            .Add(CountColumn)
            .Add(NullCountColumn)
            .Add(FilledCountColumn)
            .Add(BlankCountColumn)
            .Add(ColumnDescriptionColumn)
            .Add(AllowDBNullColumn)
            .Add(UniqueColumn)
            .Add(DefaultValueColumn)
            .Add(AutoIncrementColumn)
            .Add(AutoIncrementSeedColumn)
            .Add(AutoIncrementStepColumn)
            .Add(ColumnMappingColumn)
            .Add(ExpressionColumn)
            '.Add(ExtendedPropertiesColumn)
            .Add(MaxLengthColumn)
            .Add(TableColumn)
            .Add(DateTimeModeColumn)
        End With

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

                    'set up the new row with metadata values
                    If Not Column.DataType.ToString = "System.Object" Then
                        'average fails on non-numeric types so check if numeric
                        If ColumnDataTypeIsNumeric(Column) = True Then
                            NewRow.Item("Average") = InputDataTable.Compute("Avg([" & Column.ColumnName & "])", "").ToString
                        End If

                        'ironically, max and min seem to work on text as well as numeric
                        NewRow.Item("Maximum") = InputDataTable.Compute("Max([" & Column.ColumnName & "])", "").ToString
                        NewRow.Item("Minimum") = InputDataTable.Compute("Min([" & Column.ColumnName & "])", "").ToString
                        NewRow.Item("Count") = InputDataTable.Compute("Count([" & Column.ColumnName & "])", "").ToString

                        Dim NullsFilter As String = "[" & Column.ColumnName & "] is NULL"
                        NewRow.Item("NullCount") = InputDataTable.Compute("Count([" & Column.ColumnName & "])", NullsFilter).ToString

                        Dim NotNullsFilter As String = "[" & Column.ColumnName & "] is not NULL"
                        NewRow.Item("FilledCount") = InputDataTable.Compute("Count([" & Column.ColumnName & "])", NotNullsFilter).ToString
                    End If


                        'count the blanks
                        Dim BlanksCount As Integer = 0
                    For Each InputRow As DataRow In InputDataTable.Rows
                        If InputRow.Item(Column.ColumnName).ToString.Trim = "" Then
                            BlanksCount = BlanksCount + 1
                        End If
                    Next
                    NewRow.Item("BlankCount") = BlanksCount

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

    'Public Function RemoveEmptyDataTableRows(InputDataTable As DataTable) As DataTable
    '    Dim RowIndex As Integer = 0
    '    For Each Row As DataRow In InputDataTable.Rows
    '        Dim EmptyCellsCount As Integer = 0
    '        For Each Col As DataColumn In InputDataTable.Columns
    '            If IsDBNull(Row.Item(Col.ColumnName)) = True Or Row.Item(Col.ColumnName).ToString.Trim = "" Then
    '                EmptyCellsCount = EmptyCellsCount + 1
    '            End If
    '        Next
    '        If EmptyCellsCount = InputDataTable.Columns.Count Then
    '            InputDataTable.Rows.Remove(Row)
    '        End If
    '        RowIndex = RowIndex + 1
    '    Next
    '    Return InputDataTable
    'End Function


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
                Using MySqlDataAdapter As New SqlDataAdapter(Sql, MySqlConnection)
                    MySqlDataAdapter.Fill(MyDataTable)
                End Using
                'create a DataReader and execute the SqlCommand
                'Dim MyDataReader As SqlDataReader = MySqlCommand.ExecuteReader()

                'load the reader into the datatable
                'MyDataTable.Load(MyDataReader)

                'clean up
                'MyDataReader.Close()
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



    ''' <summary>
    ''' Get a CREATE TABLE SQL query string based on the submitted DataView
    ''' </summary>
    ''' <param name="DataView"></param>
    ''' <param name="NewTableName"></param>
    ''' <returns>String</returns>
    Public Function GetCreateTableQuery(DataView As DataView, NewTableName As String) As String
        Dim Sql As String = "" & vbNewLine
        Try
            Sql = Sql & "--Best guess at columns and datatypes from the metadata available in the source dataset.  Examine and modify as needed" & vbNewLine
            Sql = Sql & "CREATE TABLE " & NewTableName.Trim & "(" & vbNewLine
            Dim CurrentDataTable As DataTable = DataView.ToTable
            For Each Col As DataColumn In CurrentDataTable.Columns
                Dim DataType As String = Col.DataType.ToString.Replace("System.", "")
                Dim SqlDataType As String = ""
                Select Case DataType
                    Case "Boolean"
                        SqlDataType = "Bit"
                    Case "Byte"
                        SqlDataType = "Binary"
                    Case "Char"
                        SqlDataType = "Char(" & Col.MaxLength & ")"
                    Case "DateTime"
                        SqlDataType = "Datetime"
                    Case "Date"
                        SqlDataType = "Date"
                    Case "Time"
                        SqlDataType = "Time"
                    Case "Decimal"
                        SqlDataType = "Decimal" & "(" & Col.MaxLength & ",2)"
                    Case "Double"
                        SqlDataType = "Float"
                    Case "Int16"
                        SqlDataType = "Int"
                    Case "Int32"
                        SqlDataType = "Int"
                    Case "Int64"
                        SqlDataType = "Int"
                    Case "Int"
                        SqlDataType = "Int"
                    Case "Integer"
                        SqlDataType = "Int"
                    Case "UInteger"
                        SqlDataType = "Int"
                    Case "UInt16"
                        SqlDataType = "Int"
                    Case "UInt32"
                        SqlDataType = "Int"
                    Case "UInt64"
                        SqlDataType = "Int"
                    Case "Long"
                        SqlDataType = "Int"
                    Case "Object"
                        SqlDataType = "Object()"
                    Case "SByte"
                        SqlDataType = "Binary"
                    Case "Short"
                        SqlDataType = "Int"
                    Case "Single"
                        SqlDataType = "Float"
                    Case "String"
                        SqlDataType = "Varchar(50)"
                    Case "UInteger"
                        SqlDataType = "Int"
                    Case "ULong"
                        SqlDataType = "Int"
                    Case "User-Defined"
                        SqlDataType = "User-Defined"
                    Case "UShort"
                        SqlDataType = "Int"
                    Case Else
                        SqlDataType = "Varchar(50)"
                End Select
                Debug.Print(Col.ColumnName & " " & DataType & " " & SqlDataType)
                Sql = Sql & "[" & Col.ColumnName & "] " & SqlDataType & "," & vbNewLine
            Next
            Sql = Sql.Substring(0, Sql.Trim.Length - 1) & ");"
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try
        Return Sql
    End Function

    ''' <summary>
    ''' Returns true if the DataColumn is a numeric type (Decimal,Double,Int16,Int32,Int64,Single,UInt16,UInt32,UInt64)
    ''' </summary>
    ''' <param name="DataColumn">DataColumn</param>
    ''' <returns>Boolean</returns>
    Public Function ColumnDataTypeIsNumeric(DataColumn As DataColumn) As Boolean
        Dim IsNumeric As Boolean = False
        Try
            Dim ColumnDataType As String = DataColumn.DataType.ToString.Trim.Replace("System.", "")
            Dim DataType As String = DataColumn.DataType.ToString.Trim.Replace("System.", "")

            Dim CSV As String = "Decimal,Double,Int16,Int32,Int64,Single,UInt16,UInt32,UInt64"
            For Each DataType In CSV.Split(",")
                If ColumnDataType = DataType Then
                    IsNumeric = True
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message & "  " & System.Reflection.MethodBase.GetCurrentMethod.Name)
        End Try

        Return IsNumeric
    End Function

End Module

