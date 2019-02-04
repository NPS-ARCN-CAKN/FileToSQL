﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SkeeterDataTableControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SkeeterDataTableControl))
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.DataTableGridEX = New Janus.Windows.GridEX.GridEX()
        Me.DataTableToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExportToCSVToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.AutosizeColumnsModeToolStripLabel = New System.Windows.Forms.ToolStripLabel()
        Me.AutosizeColumnsToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel5 = New System.Windows.Forms.ToolStripLabel()
        Me.ShowColumnTotalsToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.CreateTableQueryToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.MetadataDataGridView = New System.Windows.Forms.DataGridView()
        Me.MetadataToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel4 = New System.Windows.Forms.ToolStripLabel()
        Me.DataTableTabControl = New System.Windows.Forms.TabControl()
        Me.SourceDataTableTabPage = New System.Windows.Forms.TabPage()
        Me.DestinationTableTabPage = New System.Windows.Forms.TabPage()
        Me.DestinationDataGridView = New System.Windows.Forms.DataGridView()
        Me.DatabaseConnectionPanel = New System.Windows.Forms.Panel()
        Me.QueryTextBox = New System.Windows.Forms.TextBox()
        Me.ExecuteSQLButton = New System.Windows.Forms.Button()
        Me.TableLabel = New System.Windows.Forms.Label()
        Me.DestinationLabel = New System.Windows.Forms.Label()
        Me.ConnectionStringLabel = New System.Windows.Forms.Label()
        Me.ConnectionStringTextBox = New System.Windows.Forms.TextBox()
        Me.MappingTabPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.ColumnsMappingDataGridView = New System.Windows.Forms.DataGridView()
        Me.DestinationColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceColumnName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DefaultValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QuotedColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SqlTextBox = New System.Windows.Forms.TextBox()
        Me.SqlToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.WrapToolStripLabel = New System.Windows.Forms.ToolStripLabel()
        Me.WrapToolStripComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.NumberOfPreviewQueriesToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.MappingsPanel = New System.Windows.Forms.Panel()
        Me.MappingsHeaderLabel = New System.Windows.Forms.Label()
        Me.TreeNodesImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.DataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.DataTableGridEX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataTableToolStrip.SuspendLayout()
        CType(Me.MetadataDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MetadataToolStrip.SuspendLayout()
        Me.DataTableTabControl.SuspendLayout()
        Me.SourceDataTableTabPage.SuspendLayout()
        Me.DestinationTableTabPage.SuspendLayout()
        CType(Me.DestinationDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DatabaseConnectionPanel.SuspendLayout()
        Me.MappingTabPage.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.ColumnsMappingDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SqlToolStrip.SuspendLayout()
        Me.MappingsPanel.SuspendLayout()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(2, 2)
        Me.SplitContainer.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.DataTableGridEX)
        Me.SplitContainer.Panel1.Controls.Add(Me.DataTableToolStrip)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.MetadataDataGridView)
        Me.SplitContainer.Panel2.Controls.Add(Me.MetadataToolStrip)
        Me.SplitContainer.Size = New System.Drawing.Size(874, 489)
        Me.SplitContainer.SplitterDistance = 233
        Me.SplitContainer.SplitterWidth = 3
        Me.SplitContainer.TabIndex = 0
        '
        'DataTableGridEX
        '
        Me.DataTableGridEX.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.DataTableGridEX.AlternatingRowFormatStyle.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.DataTableGridEX.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell
        Me.DataTableGridEX.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells
        Me.DataTableGridEX.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTableGridEX.EmptyRows = True
        Me.DataTableGridEX.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.DataTableGridEX.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.DataTableGridEX.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.DataTableGridEX.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.DataTableGridEX.Location = New System.Drawing.Point(0, 25)
        Me.DataTableGridEX.Name = "DataTableGridEX"
        Me.DataTableGridEX.NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow
        Me.DataTableGridEX.RecordNavigator = True
        Me.DataTableGridEX.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.DataTableGridEX.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.DataTableGridEX.Size = New System.Drawing.Size(874, 208)
        Me.DataTableGridEX.TabIndex = 3
        Me.DataTableGridEX.TableHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.DataTableGridEX.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.DataTableGridEX.TotalRowFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.DataTableGridEX.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        '
        'DataTableToolStrip
        '
        Me.DataTableToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.DataTableToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel3, Me.ToolStripSeparator4, Me.ExportToCSVToolStripButton, Me.ToolStripSeparator3, Me.AutosizeColumnsModeToolStripLabel, Me.AutosizeColumnsToolStripComboBox, Me.ToolStripSeparator5, Me.ToolStripLabel5, Me.ShowColumnTotalsToolStripComboBox, Me.ToolStripSeparator6, Me.CreateTableQueryToolStripButton, Me.ToolStripSeparator7})
        Me.DataTableToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.DataTableToolStrip.Name = "DataTableToolStrip"
        Me.DataTableToolStrip.Size = New System.Drawing.Size(874, 25)
        Me.DataTableToolStrip.TabIndex = 2
        Me.DataTableToolStrip.Text = "ToolStrip1"
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(40, 22)
        Me.ToolStripLabel3.Text = "Data"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ExportToCSVToolStripButton
        '
        Me.ExportToCSVToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ExportToCSVToolStripButton.Image = CType(resources.GetObject("ExportToCSVToolStripButton.Image"), System.Drawing.Image)
        Me.ExportToCSVToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ExportToCSVToolStripButton.Name = "ExportToCSVToolStripButton"
        Me.ExportToCSVToolStripButton.Size = New System.Drawing.Size(82, 22)
        Me.ExportToCSVToolStripButton.Text = "Export to CSV"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'AutosizeColumnsModeToolStripLabel
        '
        Me.AutosizeColumnsModeToolStripLabel.Name = "AutosizeColumnsModeToolStripLabel"
        Me.AutosizeColumnsModeToolStripLabel.Size = New System.Drawing.Size(138, 22)
        Me.AutosizeColumnsModeToolStripLabel.Text = "Autosize columns mode:"
        '
        'AutosizeColumnsToolStripComboBox
        '
        Me.AutosizeColumnsToolStripComboBox.Items.AddRange(New Object() {"", "All cells", "All cells and header", "Column header", "Default", "Displayed cells", "Displayed cells and header"})
        Me.AutosizeColumnsToolStripComboBox.Name = "AutosizeColumnsToolStripComboBox"
        Me.AutosizeColumnsToolStripComboBox.Size = New System.Drawing.Size(151, 25)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel5
        '
        Me.ToolStripLabel5.Name = "ToolStripLabel5"
        Me.ToolStripLabel5.Size = New System.Drawing.Size(115, 22)
        Me.ToolStripLabel5.Text = "Show column totals:"
        '
        'ShowColumnTotalsToolStripComboBox
        '
        Me.ShowColumnTotalsToolStripComboBox.Items.AddRange(New Object() {"Avg", "Count", "Max", "Min", "Std. Dev.", "Sum", "Value count", "None", "Hide column totals"})
        Me.ShowColumnTotalsToolStripComboBox.Name = "ShowColumnTotalsToolStripComboBox"
        Me.ShowColumnTotalsToolStripComboBox.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'CreateTableQueryToolStripButton
        '
        Me.CreateTableQueryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CreateTableQueryToolStripButton.Image = CType(resources.GetObject("CreateTableQueryToolStripButton.Image"), System.Drawing.Image)
        Me.CreateTableQueryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CreateTableQueryToolStripButton.Name = "CreateTableQueryToolStripButton"
        Me.CreateTableQueryToolStripButton.Size = New System.Drawing.Size(116, 22)
        Me.CreateTableQueryToolStripButton.Text = "Create table query..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'MetadataDataGridView
        '
        Me.MetadataDataGridView.AllowUserToAddRows = False
        Me.MetadataDataGridView.AllowUserToDeleteRows = False
        Me.MetadataDataGridView.AllowUserToOrderColumns = True
        Me.MetadataDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetadataDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetadataDataGridView.Location = New System.Drawing.Point(0, 25)
        Me.MetadataDataGridView.Margin = New System.Windows.Forms.Padding(2)
        Me.MetadataDataGridView.Name = "MetadataDataGridView"
        Me.MetadataDataGridView.RowTemplate.Height = 24
        Me.MetadataDataGridView.Size = New System.Drawing.Size(874, 228)
        Me.MetadataDataGridView.TabIndex = 1
        '
        'MetadataToolStrip
        '
        Me.MetadataToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel4})
        Me.MetadataToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.MetadataToolStrip.Name = "MetadataToolStrip"
        Me.MetadataToolStrip.Size = New System.Drawing.Size(874, 25)
        Me.MetadataToolStrip.TabIndex = 2
        Me.MetadataToolStrip.Text = "ToolStrip1"
        '
        'ToolStripLabel4
        '
        Me.ToolStripLabel4.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel4.Name = "ToolStripLabel4"
        Me.ToolStripLabel4.Size = New System.Drawing.Size(73, 22)
        Me.ToolStripLabel4.Text = "Metadata"
        '
        'DataTableTabControl
        '
        Me.DataTableTabControl.Controls.Add(Me.SourceDataTableTabPage)
        Me.DataTableTabControl.Controls.Add(Me.DestinationTableTabPage)
        Me.DataTableTabControl.Controls.Add(Me.MappingTabPage)
        Me.DataTableTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTableTabControl.Location = New System.Drawing.Point(0, 0)
        Me.DataTableTabControl.Margin = New System.Windows.Forms.Padding(2)
        Me.DataTableTabControl.Name = "DataTableTabControl"
        Me.DataTableTabControl.SelectedIndex = 0
        Me.DataTableTabControl.Size = New System.Drawing.Size(886, 519)
        Me.DataTableTabControl.TabIndex = 2
        '
        'SourceDataTableTabPage
        '
        Me.SourceDataTableTabPage.Controls.Add(Me.SplitContainer)
        Me.SourceDataTableTabPage.Location = New System.Drawing.Point(4, 22)
        Me.SourceDataTableTabPage.Margin = New System.Windows.Forms.Padding(2)
        Me.SourceDataTableTabPage.Name = "SourceDataTableTabPage"
        Me.SourceDataTableTabPage.Padding = New System.Windows.Forms.Padding(2)
        Me.SourceDataTableTabPage.Size = New System.Drawing.Size(878, 493)
        Me.SourceDataTableTabPage.TabIndex = 0
        Me.SourceDataTableTabPage.Text = "Source data table"
        Me.SourceDataTableTabPage.UseVisualStyleBackColor = True
        '
        'DestinationTableTabPage
        '
        Me.DestinationTableTabPage.Controls.Add(Me.DestinationDataGridView)
        Me.DestinationTableTabPage.Controls.Add(Me.DatabaseConnectionPanel)
        Me.DestinationTableTabPage.Location = New System.Drawing.Point(4, 22)
        Me.DestinationTableTabPage.Margin = New System.Windows.Forms.Padding(2)
        Me.DestinationTableTabPage.Name = "DestinationTableTabPage"
        Me.DestinationTableTabPage.Padding = New System.Windows.Forms.Padding(2)
        Me.DestinationTableTabPage.Size = New System.Drawing.Size(878, 493)
        Me.DestinationTableTabPage.TabIndex = 1
        Me.DestinationTableTabPage.Text = "Destination data table"
        Me.DestinationTableTabPage.UseVisualStyleBackColor = True
        '
        'DestinationDataGridView
        '
        Me.DestinationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DestinationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DestinationDataGridView.Location = New System.Drawing.Point(2, 122)
        Me.DestinationDataGridView.Margin = New System.Windows.Forms.Padding(2)
        Me.DestinationDataGridView.Name = "DestinationDataGridView"
        Me.DestinationDataGridView.RowTemplate.Height = 24
        Me.DestinationDataGridView.Size = New System.Drawing.Size(874, 369)
        Me.DestinationDataGridView.TabIndex = 5
        '
        'DatabaseConnectionPanel
        '
        Me.DatabaseConnectionPanel.Controls.Add(Me.QueryTextBox)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ExecuteSQLButton)
        Me.DatabaseConnectionPanel.Controls.Add(Me.TableLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.DestinationLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ConnectionStringLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ConnectionStringTextBox)
        Me.DatabaseConnectionPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.DatabaseConnectionPanel.Location = New System.Drawing.Point(2, 2)
        Me.DatabaseConnectionPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.DatabaseConnectionPanel.Name = "DatabaseConnectionPanel"
        Me.DatabaseConnectionPanel.Size = New System.Drawing.Size(874, 120)
        Me.DatabaseConnectionPanel.TabIndex = 4
        '
        'QueryTextBox
        '
        Me.QueryTextBox.Location = New System.Drawing.Point(104, 76)
        Me.QueryTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.QueryTextBox.Multiline = True
        Me.QueryTextBox.Name = "QueryTextBox"
        Me.QueryTextBox.Size = New System.Drawing.Size(678, 32)
        Me.QueryTextBox.TabIndex = 9
        '
        'ExecuteSQLButton
        '
        Me.ExecuteSQLButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExecuteSQLButton.Image = CType(resources.GetObject("ExecuteSQLButton.Image"), System.Drawing.Image)
        Me.ExecuteSQLButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExecuteSQLButton.Location = New System.Drawing.Point(785, 76)
        Me.ExecuteSQLButton.Margin = New System.Windows.Forms.Padding(2)
        Me.ExecuteSQLButton.Name = "ExecuteSQLButton"
        Me.ExecuteSQLButton.Size = New System.Drawing.Size(74, 19)
        Me.ExecuteSQLButton.TabIndex = 8
        Me.ExecuteSQLButton.Text = "Execute"
        Me.ExecuteSQLButton.UseVisualStyleBackColor = True
        '
        'TableLabel
        '
        Me.TableLabel.AutoSize = True
        Me.TableLabel.Location = New System.Drawing.Point(8, 76)
        Me.TableLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TableLabel.Name = "TableLabel"
        Me.TableLabel.Size = New System.Drawing.Size(38, 13)
        Me.TableLabel.TabIndex = 6
        Me.TableLabel.Text = "Query:"
        '
        'DestinationLabel
        '
        Me.DestinationLabel.AutoSize = True
        Me.DestinationLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DestinationLabel.Location = New System.Drawing.Point(8, 10)
        Me.DestinationLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.DestinationLabel.Name = "DestinationLabel"
        Me.DestinationLabel.Size = New System.Drawing.Size(156, 19)
        Me.DestinationLabel.TabIndex = 3
        Me.DestinationLabel.Text = "Destination dataset"
        '
        'ConnectionStringLabel
        '
        Me.ConnectionStringLabel.AutoSize = True
        Me.ConnectionStringLabel.Location = New System.Drawing.Point(8, 34)
        Me.ConnectionStringLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ConnectionStringLabel.Name = "ConnectionStringLabel"
        Me.ConnectionStringLabel.Size = New System.Drawing.Size(92, 13)
        Me.ConnectionStringLabel.TabIndex = 2
        Me.ConnectionStringLabel.Text = "Connection string:"
        '
        'ConnectionStringTextBox
        '
        Me.ConnectionStringTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConnectionStringTextBox.Location = New System.Drawing.Point(104, 32)
        Me.ConnectionStringTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.ConnectionStringTextBox.Multiline = True
        Me.ConnectionStringTextBox.Name = "ConnectionStringTextBox"
        Me.ConnectionStringTextBox.Size = New System.Drawing.Size(677, 38)
        Me.ConnectionStringTextBox.TabIndex = 1
        '
        'MappingTabPage
        '
        Me.MappingTabPage.Controls.Add(Me.SplitContainer2)
        Me.MappingTabPage.Controls.Add(Me.MappingsPanel)
        Me.MappingTabPage.Location = New System.Drawing.Point(4, 22)
        Me.MappingTabPage.Margin = New System.Windows.Forms.Padding(2)
        Me.MappingTabPage.Name = "MappingTabPage"
        Me.MappingTabPage.Padding = New System.Windows.Forms.Padding(2)
        Me.MappingTabPage.Size = New System.Drawing.Size(878, 493)
        Me.MappingTabPage.TabIndex = 2
        Me.MappingTabPage.Text = "Map source data to destination"
        Me.MappingTabPage.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(2, 41)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.ColumnsMappingDataGridView)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.SqlTextBox)
        Me.SplitContainer2.Panel2.Controls.Add(Me.SqlToolStrip)
        Me.SplitContainer2.Size = New System.Drawing.Size(874, 450)
        Me.SplitContainer2.SplitterDistance = 189
        Me.SplitContainer2.SplitterWidth = 3
        Me.SplitContainer2.TabIndex = 6
        '
        'ColumnsMappingDataGridView
        '
        Me.ColumnsMappingDataGridView.AllowUserToAddRows = False
        Me.ColumnsMappingDataGridView.AllowUserToDeleteRows = False
        Me.ColumnsMappingDataGridView.AllowUserToOrderColumns = True
        Me.ColumnsMappingDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.ColumnsMappingDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ColumnsMappingDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DestinationColumnName, Me.SourceColumnName, Me.DefaultValueColumn, Me.QuotedColumn})
        Me.ColumnsMappingDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ColumnsMappingDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.ColumnsMappingDataGridView.Margin = New System.Windows.Forms.Padding(2)
        Me.ColumnsMappingDataGridView.Name = "ColumnsMappingDataGridView"
        Me.ColumnsMappingDataGridView.RowTemplate.Height = 24
        Me.ColumnsMappingDataGridView.Size = New System.Drawing.Size(874, 189)
        Me.ColumnsMappingDataGridView.TabIndex = 3
        '
        'DestinationColumnName
        '
        Me.DestinationColumnName.DataPropertyName = "DestinationColumnName"
        Me.DestinationColumnName.HeaderText = "Destination column"
        Me.DestinationColumnName.Name = "DestinationColumnName"
        '
        'SourceColumnName
        '
        Me.SourceColumnName.DataPropertyName = "SourceColumnName"
        Me.SourceColumnName.HeaderText = "Source column"
        Me.SourceColumnName.Name = "SourceColumnName"
        Me.SourceColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.SourceColumnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DefaultValueColumn
        '
        Me.DefaultValueColumn.DataPropertyName = "DefaultValueColumn"
        Me.DefaultValueColumn.HeaderText = "Default value"
        Me.DefaultValueColumn.Name = "DefaultValueColumn"
        '
        'QuotedColumn
        '
        Me.QuotedColumn.DataPropertyName = "QuotedColumn"
        Me.QuotedColumn.HeaderText = "Quote in SQL"
        Me.QuotedColumn.Name = "QuotedColumn"
        Me.QuotedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.QuotedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'SqlTextBox
        '
        Me.SqlTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SqlTextBox.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SqlTextBox.Location = New System.Drawing.Point(0, 25)
        Me.SqlTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.SqlTextBox.Multiline = True
        Me.SqlTextBox.Name = "SqlTextBox"
        Me.SqlTextBox.Size = New System.Drawing.Size(874, 233)
        Me.SqlTextBox.TabIndex = 0
        Me.SqlTextBox.WordWrap = False
        '
        'SqlToolStrip
        '
        Me.SqlToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.SqlToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.ToolStripSeparator1, Me.WrapToolStripLabel, Me.WrapToolStripComboBox, Me.ToolStripSeparator2, Me.ToolStripLabel2, Me.NumberOfPreviewQueriesToolStripTextBox})
        Me.SqlToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.SqlToolStrip.Name = "SqlToolStrip"
        Me.SqlToolStrip.Size = New System.Drawing.Size(874, 25)
        Me.SqlToolStrip.TabIndex = 1
        Me.SqlToolStrip.Text = "Insert queries script:"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(109, 22)
        Me.ToolStripLabel1.Text = "Insert queries script"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'WrapToolStripLabel
        '
        Me.WrapToolStripLabel.Name = "WrapToolStripLabel"
        Me.WrapToolStripLabel.Size = New System.Drawing.Size(35, 22)
        Me.WrapToolStripLabel.Text = "Wrap"
        '
        'WrapToolStripComboBox
        '
        Me.WrapToolStripComboBox.Items.AddRange(New Object() {"True", "False"})
        Me.WrapToolStripComboBox.Name = "WrapToolStripComboBox"
        Me.WrapToolStripComboBox.Size = New System.Drawing.Size(92, 25)
        Me.WrapToolStripComboBox.Text = "False"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(153, 22)
        Me.ToolStripLabel2.Text = "Number of preview queries:"
        '
        'NumberOfPreviewQueriesToolStripTextBox
        '
        Me.NumberOfPreviewQueriesToolStripTextBox.Name = "NumberOfPreviewQueriesToolStripTextBox"
        Me.NumberOfPreviewQueriesToolStripTextBox.Size = New System.Drawing.Size(76, 25)
        '
        'MappingsPanel
        '
        Me.MappingsPanel.BackColor = System.Drawing.Color.Transparent
        Me.MappingsPanel.Controls.Add(Me.MappingsHeaderLabel)
        Me.MappingsPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.MappingsPanel.Location = New System.Drawing.Point(2, 2)
        Me.MappingsPanel.Margin = New System.Windows.Forms.Padding(2)
        Me.MappingsPanel.Name = "MappingsPanel"
        Me.MappingsPanel.Size = New System.Drawing.Size(874, 39)
        Me.MappingsPanel.TabIndex = 5
        '
        'MappingsHeaderLabel
        '
        Me.MappingsHeaderLabel.AutoSize = True
        Me.MappingsHeaderLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MappingsHeaderLabel.Location = New System.Drawing.Point(8, 10)
        Me.MappingsHeaderLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.MappingsHeaderLabel.Name = "MappingsHeaderLabel"
        Me.MappingsHeaderLabel.Size = New System.Drawing.Size(346, 19)
        Me.MappingsHeaderLabel.TabIndex = 3
        Me.MappingsHeaderLabel.Text = "Map destination columns to source columns"
        '
        'TreeNodesImageList
        '
        Me.TreeNodesImageList.ImageStream = CType(resources.GetObject("TreeNodesImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.TreeNodesImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.TreeNodesImageList.Images.SetKeyName(0, "database.png")
        Me.TreeNodesImageList.Images.SetKeyName(1, "table.png")
        Me.TreeNodesImageList.Images.SetKeyName(2, "layout.png")
        Me.TreeNodesImageList.Images.SetKeyName(3, "brick.png")
        Me.TreeNodesImageList.Images.SetKeyName(4, "cog.png")
        Me.TreeNodesImageList.Images.SetKeyName(5, "bullet_wrench.png")
        Me.TreeNodesImageList.Images.SetKeyName(6, "page_excel.png")
        Me.TreeNodesImageList.Images.SetKeyName(7, "page_white_text.png")
        '
        'SkeeterDataTableControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DataTableTabControl)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "SkeeterDataTableControl"
        Me.Size = New System.Drawing.Size(886, 519)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel1.PerformLayout()
        Me.SplitContainer.Panel2.ResumeLayout(False)
        Me.SplitContainer.Panel2.PerformLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.DataTableGridEX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataTableToolStrip.ResumeLayout(False)
        Me.DataTableToolStrip.PerformLayout()
        CType(Me.MetadataDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MetadataToolStrip.ResumeLayout(False)
        Me.MetadataToolStrip.PerformLayout()
        Me.DataTableTabControl.ResumeLayout(False)
        Me.SourceDataTableTabPage.ResumeLayout(False)
        Me.DestinationTableTabPage.ResumeLayout(False)
        CType(Me.DestinationDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DatabaseConnectionPanel.ResumeLayout(False)
        Me.DatabaseConnectionPanel.PerformLayout()
        Me.MappingTabPage.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.ColumnsMappingDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SqlToolStrip.ResumeLayout(False)
        Me.SqlToolStrip.PerformLayout()
        Me.MappingsPanel.ResumeLayout(False)
        Me.MappingsPanel.PerformLayout()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer As SplitContainer
    Friend WithEvents MetadataDataGridView As DataGridView
    Friend WithEvents DataTableBindingSource As BindingSource
    Friend WithEvents DataTableTabControl As TabControl
    Friend WithEvents SourceDataTableTabPage As TabPage
    Friend WithEvents DestinationTableTabPage As TabPage
    Friend WithEvents ColumnsMappingDataGridView As DataGridView
    Friend WithEvents DatabaseConnectionPanel As Panel
    Friend WithEvents DestinationLabel As Label
    Friend WithEvents ConnectionStringLabel As Label
    Friend WithEvents ConnectionStringTextBox As TextBox
    Friend WithEvents TableLabel As Label
    Friend WithEvents DestinationDataGridView As DataGridView
    Friend WithEvents MappingsPanel As Panel
    Friend WithEvents MappingsHeaderLabel As Label
    Friend WithEvents ExecuteSQLButton As Button
    Friend WithEvents QueryTextBox As TextBox
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SqlTextBox As TextBox
    Friend WithEvents MappingTabPage As TabPage
    Friend WithEvents SqlToolStrip As ToolStrip
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TreeNodesImageList As ImageList
    Friend WithEvents DestinationColumnName As DataGridViewTextBoxColumn
    Friend WithEvents SourceColumnName As DataGridViewComboBoxColumn
    Friend WithEvents DefaultValueColumn As DataGridViewTextBoxColumn
    Friend WithEvents QuotedColumn As DataGridViewCheckBoxColumn
    Friend WithEvents WrapToolStripLabel As ToolStripLabel
    Friend WithEvents WrapToolStripComboBox As ToolStripComboBox
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents NumberOfPreviewQueriesToolStripTextBox As ToolStripTextBox
    Friend WithEvents DataTableToolStrip As ToolStrip
    Friend WithEvents ExportToCSVToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents AutosizeColumnsModeToolStripLabel As ToolStripLabel
    Friend WithEvents AutosizeColumnsToolStripComboBox As ToolStripComboBox
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents MetadataToolStrip As ToolStrip
    Friend WithEvents ToolStripLabel4 As ToolStripLabel
    Friend WithEvents DataTableGridEX As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripLabel5 As ToolStripLabel
    Friend WithEvents ShowColumnTotalsToolStripComboBox As ToolStripComboBox
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents CreateTableQueryToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
End Class
