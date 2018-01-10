<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SkeeterDataTableControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SkeeterDataTableControl))
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.DataTableBindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorDeleteItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.DataTableDataGridView = New System.Windows.Forms.DataGridView()
        Me.MetadataDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataTableTabControl = New System.Windows.Forms.TabControl()
        Me.DataTabPage = New System.Windows.Forms.TabPage()
        Me.DestinationTabPage = New System.Windows.Forms.TabPage()
        Me.DestinationLabel = New System.Windows.Forms.Label()
        Me.ConnectionStringLabel = New System.Windows.Forms.Label()
        Me.ConnectionStringTextBox = New System.Windows.Forms.TextBox()
        Me.ColumnsMappingDataGridView = New System.Windows.Forms.DataGridView()
        Me.DestinationColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceColumnName = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DefaultValueColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QuotedColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatabaseConnectionPanel = New System.Windows.Forms.Panel()
        Me.ConnectButton = New System.Windows.Forms.Button()
        Me.TableLabel = New System.Windows.Forms.Label()
        Me.TableComboBox = New System.Windows.Forms.ComboBox()
        Me.DestinationDataGridView = New System.Windows.Forms.DataGridView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ExecuteSQLButton = New System.Windows.Forms.Button()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        CType(Me.DataTableBindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataTableBindingNavigator.SuspendLayout()
        CType(Me.DataTableDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MetadataDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DataTableTabControl.SuspendLayout()
        Me.DataTabPage.SuspendLayout()
        Me.DestinationTabPage.SuspendLayout()
        CType(Me.ColumnsMappingDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DatabaseConnectionPanel.SuspendLayout()
        CType(Me.DestinationDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.DataTableDataGridView)
        Me.SplitContainer.Panel1.Controls.Add(Me.DataTableBindingNavigator)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.MetadataDataGridView)
        Me.SplitContainer.Size = New System.Drawing.Size(1167, 604)
        Me.SplitContainer.SplitterDistance = 288
        Me.SplitContainer.TabIndex = 0
        '
        'DataTableBindingNavigator
        '
        Me.DataTableBindingNavigator.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.DataTableBindingNavigator.BindingSource = Me.DataTableBindingSource
        Me.DataTableBindingNavigator.CountItem = Me.BindingNavigatorCountItem
        Me.DataTableBindingNavigator.DeleteItem = Me.BindingNavigatorDeleteItem
        Me.DataTableBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataTableBindingNavigator.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.DataTableBindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BindingNavigatorDeleteItem})
        Me.DataTableBindingNavigator.Location = New System.Drawing.Point(0, 261)
        Me.DataTableBindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.DataTableBindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.DataTableBindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.DataTableBindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.DataTableBindingNavigator.Name = "DataTableBindingNavigator"
        Me.DataTableBindingNavigator.PositionItem = Me.BindingNavigatorPositionItem
        Me.DataTableBindingNavigator.Size = New System.Drawing.Size(1167, 27)
        Me.DataTableBindingNavigator.TabIndex = 1
        Me.DataTableBindingNavigator.Text = "BindingNavigator1"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorAddNewItem.Text = "Add new"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(45, 24)
        Me.BindingNavigatorCountItem.Text = "of {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Total number of items"
        '
        'BindingNavigatorDeleteItem
        '
        Me.BindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorDeleteItem.Image = CType(resources.GetObject("BindingNavigatorDeleteItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorDeleteItem.Name = "BindingNavigatorDeleteItem"
        Me.BindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorDeleteItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorDeleteItem.Text = "Delete"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 27)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Position"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 27)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Current position"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(24, 24)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'DataTableDataGridView
        '
        Me.DataTableDataGridView.AllowUserToAddRows = False
        Me.DataTableDataGridView.AllowUserToDeleteRows = False
        Me.DataTableDataGridView.AllowUserToOrderColumns = True
        Me.DataTableDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataTableDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTableDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DataTableDataGridView.Name = "DataTableDataGridView"
        Me.DataTableDataGridView.ReadOnly = True
        Me.DataTableDataGridView.RowTemplate.Height = 24
        Me.DataTableDataGridView.Size = New System.Drawing.Size(1167, 261)
        Me.DataTableDataGridView.TabIndex = 0
        '
        'MetadataDataGridView
        '
        Me.MetadataDataGridView.AllowUserToAddRows = False
        Me.MetadataDataGridView.AllowUserToDeleteRows = False
        Me.MetadataDataGridView.AllowUserToOrderColumns = True
        Me.MetadataDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.MetadataDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MetadataDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.MetadataDataGridView.Name = "MetadataDataGridView"
        Me.MetadataDataGridView.ReadOnly = True
        Me.MetadataDataGridView.RowTemplate.Height = 24
        Me.MetadataDataGridView.Size = New System.Drawing.Size(1167, 312)
        Me.MetadataDataGridView.TabIndex = 1
        '
        'DataTableTabControl
        '
        Me.DataTableTabControl.Controls.Add(Me.DataTabPage)
        Me.DataTableTabControl.Controls.Add(Me.DestinationTabPage)
        Me.DataTableTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataTableTabControl.Location = New System.Drawing.Point(0, 0)
        Me.DataTableTabControl.Name = "DataTableTabControl"
        Me.DataTableTabControl.SelectedIndex = 0
        Me.DataTableTabControl.Size = New System.Drawing.Size(1181, 639)
        Me.DataTableTabControl.TabIndex = 2
        '
        'DataTabPage
        '
        Me.DataTabPage.Controls.Add(Me.SplitContainer)
        Me.DataTabPage.Location = New System.Drawing.Point(4, 25)
        Me.DataTabPage.Name = "DataTabPage"
        Me.DataTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DataTabPage.Size = New System.Drawing.Size(1173, 610)
        Me.DataTabPage.TabIndex = 0
        Me.DataTabPage.Text = "Source data"
        Me.DataTabPage.UseVisualStyleBackColor = True
        '
        'DestinationTabPage
        '
        Me.DestinationTabPage.Controls.Add(Me.SplitContainer1)
        Me.DestinationTabPage.Controls.Add(Me.DatabaseConnectionPanel)
        Me.DestinationTabPage.Location = New System.Drawing.Point(4, 25)
        Me.DestinationTabPage.Name = "DestinationTabPage"
        Me.DestinationTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.DestinationTabPage.Size = New System.Drawing.Size(1173, 610)
        Me.DestinationTabPage.TabIndex = 1
        Me.DestinationTabPage.Text = "Destination table"
        Me.DestinationTabPage.UseVisualStyleBackColor = True
        '
        'DestinationLabel
        '
        Me.DestinationLabel.AutoSize = True
        Me.DestinationLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DestinationLabel.Location = New System.Drawing.Point(10, 12)
        Me.DestinationLabel.Name = "DestinationLabel"
        Me.DestinationLabel.Size = New System.Drawing.Size(193, 24)
        Me.DestinationLabel.TabIndex = 3
        Me.DestinationLabel.Text = "Destination dataset"
        '
        'ConnectionStringLabel
        '
        Me.ConnectionStringLabel.AutoSize = True
        Me.ConnectionStringLabel.Location = New System.Drawing.Point(11, 42)
        Me.ConnectionStringLabel.Name = "ConnectionStringLabel"
        Me.ConnectionStringLabel.Size = New System.Drawing.Size(122, 17)
        Me.ConnectionStringLabel.TabIndex = 2
        Me.ConnectionStringLabel.Text = "Connection string:"
        '
        'ConnectionStringTextBox
        '
        Me.ConnectionStringTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConnectionStringTextBox.Location = New System.Drawing.Point(139, 39)
        Me.ConnectionStringTextBox.Multiline = True
        Me.ConnectionStringTextBox.Name = "ConnectionStringTextBox"
        Me.ConnectionStringTextBox.Size = New System.Drawing.Size(903, 46)
        Me.ConnectionStringTextBox.TabIndex = 1
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
        Me.ColumnsMappingDataGridView.Location = New System.Drawing.Point(0, 48)
        Me.ColumnsMappingDataGridView.Name = "ColumnsMappingDataGridView"
        Me.ColumnsMappingDataGridView.RowTemplate.Height = 24
        Me.ColumnsMappingDataGridView.Size = New System.Drawing.Size(1167, 228)
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
        '
        'DatabaseConnectionPanel
        '
        Me.DatabaseConnectionPanel.Controls.Add(Me.ExecuteSQLButton)
        Me.DatabaseConnectionPanel.Controls.Add(Me.TableComboBox)
        Me.DatabaseConnectionPanel.Controls.Add(Me.TableLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ConnectButton)
        Me.DatabaseConnectionPanel.Controls.Add(Me.DestinationLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ConnectionStringLabel)
        Me.DatabaseConnectionPanel.Controls.Add(Me.ConnectionStringTextBox)
        Me.DatabaseConnectionPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.DatabaseConnectionPanel.Location = New System.Drawing.Point(3, 3)
        Me.DatabaseConnectionPanel.Name = "DatabaseConnectionPanel"
        Me.DatabaseConnectionPanel.Size = New System.Drawing.Size(1167, 138)
        Me.DatabaseConnectionPanel.TabIndex = 4
        '
        'ConnectButton
        '
        Me.ConnectButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ConnectButton.Enabled = False
        Me.ConnectButton.Location = New System.Drawing.Point(1048, 39)
        Me.ConnectButton.Name = "ConnectButton"
        Me.ConnectButton.Size = New System.Drawing.Size(75, 23)
        Me.ConnectButton.TabIndex = 4
        Me.ConnectButton.Text = "Connect"
        Me.ConnectButton.UseVisualStyleBackColor = True
        '
        'TableLabel
        '
        Me.TableLabel.AutoSize = True
        Me.TableLabel.Location = New System.Drawing.Point(11, 94)
        Me.TableLabel.Name = "TableLabel"
        Me.TableLabel.Size = New System.Drawing.Size(162, 17)
        Me.TableLabel.TabIndex = 6
        Me.TableLabel.Text = "Table: SELECT * FROM "
        '
        'TableComboBox
        '
        Me.TableComboBox.FormattingEnabled = True
        Me.TableComboBox.Location = New System.Drawing.Point(179, 91)
        Me.TableComboBox.Name = "TableComboBox"
        Me.TableComboBox.Size = New System.Drawing.Size(315, 24)
        Me.TableComboBox.TabIndex = 7
        '
        'DestinationDataGridView
        '
        Me.DestinationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DestinationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DestinationDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.DestinationDataGridView.Name = "DestinationDataGridView"
        Me.DestinationDataGridView.RowTemplate.Height = 24
        Me.DestinationDataGridView.Size = New System.Drawing.Size(1167, 186)
        Me.DestinationDataGridView.TabIndex = 5
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 141)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DestinationDataGridView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ColumnsMappingDataGridView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1167, 466)
        Me.SplitContainer1.SplitterDistance = 186
        Me.SplitContainer1.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1167, 48)
        Me.Panel1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(429, 24)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Map destination columns to source columns"
        '
        'ExecuteSQLButton
        '
        Me.ExecuteSQLButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExecuteSQLButton.Enabled = False
        Me.ExecuteSQLButton.Image = CType(resources.GetObject("ExecuteSQLButton.Image"), System.Drawing.Image)
        Me.ExecuteSQLButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ExecuteSQLButton.Location = New System.Drawing.Point(500, 91)
        Me.ExecuteSQLButton.Name = "ExecuteSQLButton"
        Me.ExecuteSQLButton.Size = New System.Drawing.Size(116, 23)
        Me.ExecuteSQLButton.TabIndex = 8
        Me.ExecuteSQLButton.Text = "Execute"
        Me.ExecuteSQLButton.UseVisualStyleBackColor = True
        '
        'SkeeterDataTableControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DataTableTabControl)
        Me.Name = "SkeeterDataTableControl"
        Me.Size = New System.Drawing.Size(1181, 639)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel1.PerformLayout()
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        CType(Me.DataTableBindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataTableBindingNavigator.ResumeLayout(False)
        Me.DataTableBindingNavigator.PerformLayout()
        CType(Me.DataTableDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MetadataDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DataTableTabControl.ResumeLayout(False)
        Me.DataTabPage.ResumeLayout(False)
        Me.DestinationTabPage.ResumeLayout(False)
        CType(Me.ColumnsMappingDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DatabaseConnectionPanel.ResumeLayout(False)
        Me.DatabaseConnectionPanel.PerformLayout()
        CType(Me.DestinationDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer As SplitContainer
    Friend WithEvents DataTableBindingNavigator As BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As ToolStripButton
    Friend WithEvents BindingNavigatorCountItem As ToolStripLabel
    Friend WithEvents BindingNavigatorDeleteItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem As ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As ToolStripSeparator
    Friend WithEvents DataTableDataGridView As DataGridView
    Friend WithEvents MetadataDataGridView As DataGridView
    Friend WithEvents DataTableBindingSource As BindingSource
    Friend WithEvents DataTableTabControl As TabControl
    Friend WithEvents DataTabPage As TabPage
    Friend WithEvents DestinationTabPage As TabPage
    Friend WithEvents ColumnsMappingDataGridView As DataGridView
    Friend WithEvents DestinationColumnName As DataGridViewTextBoxColumn
    Friend WithEvents SourceColumnName As DataGridViewComboBoxColumn
    Friend WithEvents DefaultValueColumn As DataGridViewTextBoxColumn
    Friend WithEvents QuotedColumn As DataGridViewTextBoxColumn
    Friend WithEvents DatabaseConnectionPanel As Panel
    Friend WithEvents ConnectButton As Button
    Friend WithEvents DestinationLabel As Label
    Friend WithEvents ConnectionStringLabel As Label
    Friend WithEvents ConnectionStringTextBox As TextBox
    Friend WithEvents TableComboBox As ComboBox
    Friend WithEvents TableLabel As Label
    Friend WithEvents DestinationDataGridView As DataGridView
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ExecuteSQLButton As Button
End Class
