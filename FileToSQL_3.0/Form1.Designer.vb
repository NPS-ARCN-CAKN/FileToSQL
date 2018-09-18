<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.DatasetTreeView = New System.Windows.Forms.TreeView()
        Me.TreeViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveDatasetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveAllDatasetsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TreeNodesImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.MainMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainToolStrip = New System.Windows.Forms.ToolStrip()
        Me.OpenDataFileToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenSourceFileToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SkeeterDataTableControl = New FileToSQL_3._0.SkeeterDataTableControl()
        Me.TreeViewContextMenuStrip.SuspendLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.MainMenuStrip.SuspendLayout()
        Me.MainToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'DatasetTreeView
        '
        Me.DatasetTreeView.ContextMenuStrip = Me.TreeViewContextMenuStrip
        Me.DatasetTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DatasetTreeView.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DatasetTreeView.ImageIndex = 0
        Me.DatasetTreeView.ImageList = Me.TreeNodesImageList
        Me.DatasetTreeView.Location = New System.Drawing.Point(0, 0)
        Me.DatasetTreeView.Margin = New System.Windows.Forms.Padding(2)
        Me.DatasetTreeView.Name = "DatasetTreeView"
        Me.DatasetTreeView.SelectedImageIndex = 0
        Me.DatasetTreeView.Size = New System.Drawing.Size(185, 680)
        Me.DatasetTreeView.TabIndex = 0
        '
        'TreeViewContextMenuStrip
        '
        Me.TreeViewContextMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.TreeViewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveDatasetToolStripMenuItem, Me.RemoveAllDatasetsToolStripMenuItem})
        Me.TreeViewContextMenuStrip.Name = "TreeViewContextMenuStrip"
        Me.TreeViewContextMenuStrip.Size = New System.Drawing.Size(205, 48)
        '
        'RemoveDatasetToolStripMenuItem
        '
        Me.RemoveDatasetToolStripMenuItem.Name = "RemoveDatasetToolStripMenuItem"
        Me.RemoveDatasetToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.RemoveDatasetToolStripMenuItem.Text = "Remove selected dataset"
        '
        'RemoveAllDatasetsToolStripMenuItem
        '
        Me.RemoveAllDatasetsToolStripMenuItem.Name = "RemoveAllDatasetsToolStripMenuItem"
        Me.RemoveAllDatasetsToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.RemoveAllDatasetsToolStripMenuItem.Text = "Remove all datasets"
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
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(0, 49)
        Me.SplitContainer.Margin = New System.Windows.Forms.Padding(2)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.DatasetTreeView)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SkeeterDataTableControl)
        Me.SplitContainer.Size = New System.Drawing.Size(1008, 680)
        Me.SplitContainer.SplitterDistance = 185
        Me.SplitContainer.SplitterWidth = 3
        Me.SplitContainer.TabIndex = 1
        '
        'MainMenuStrip
        '
        Me.MainMenuStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem})
        Me.MainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MainMenuStrip.Name = "MainMenuStrip"
        Me.MainMenuStrip.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MainMenuStrip.Size = New System.Drawing.Size(1008, 24)
        Me.MainMenuStrip.TabIndex = 1
        Me.MainMenuStrip.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.CloseToolStripMenuItem, Me.ExtToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.OpenToolStripMenuItem.Text = "Open..."
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'ExtToolStripMenuItem
        '
        Me.ExtToolStripMenuItem.Name = "ExtToolStripMenuItem"
        Me.ExtToolStripMenuItem.Size = New System.Drawing.Size(112, 22)
        Me.ExtToolStripMenuItem.Text = "Exit"
        '
        'MainToolStrip
        '
        Me.MainToolStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MainToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenDataFileToolStripButton, Me.ToolStripSeparator1, Me.OpenSourceFileToolStripButton})
        Me.MainToolStrip.Location = New System.Drawing.Point(0, 24)
        Me.MainToolStrip.Name = "MainToolStrip"
        Me.MainToolStrip.Size = New System.Drawing.Size(1008, 25)
        Me.MainToolStrip.TabIndex = 2
        Me.MainToolStrip.Text = "ToolStrip1"
        '
        'OpenDataFileToolStripButton
        '
        Me.OpenDataFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OpenDataFileToolStripButton.Image = CType(resources.GetObject("OpenDataFileToolStripButton.Image"), System.Drawing.Image)
        Me.OpenDataFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenDataFileToolStripButton.Name = "OpenDataFileToolStripButton"
        Me.OpenDataFileToolStripButton.Size = New System.Drawing.Size(94, 22)
        Me.OpenDataFileToolStripButton.Text = "Open data file..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'OpenSourceFileToolStripButton
        '
        Me.OpenSourceFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.OpenSourceFileToolStripButton.Image = CType(resources.GetObject("OpenSourceFileToolStripButton.Image"), System.Drawing.Image)
        Me.OpenSourceFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenSourceFileToolStripButton.Name = "OpenSourceFileToolStripButton"
        Me.OpenSourceFileToolStripButton.Size = New System.Drawing.Size(198, 22)
        Me.OpenSourceFileToolStripButton.Text = "Open using Windows default editor"
        '
        'SkeeterDataTableControl
        '
        Me.SkeeterDataTableControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SkeeterDataTableControl.Location = New System.Drawing.Point(0, 0)
        Me.SkeeterDataTableControl.Margin = New System.Windows.Forms.Padding(2)
        Me.SkeeterDataTableControl.Name = "SkeeterDataTableControl"
        Me.SkeeterDataTableControl.Size = New System.Drawing.Size(820, 680)
        Me.SkeeterDataTableControl.SkeeterDatasetTreeNode = Nothing
        Me.SkeeterDataTableControl.TabIndex = 0
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.SplitContainer)
        Me.Controls.Add(Me.MainToolStrip)
        Me.Controls.Add(Me.MainMenuStrip)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "Form1"
        Me.Text = "Data file to SQL 3.0"
        Me.TreeViewContextMenuStrip.ResumeLayout(False)
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.MainMenuStrip.ResumeLayout(False)
        Me.MainMenuStrip.PerformLayout()
        Me.MainToolStrip.ResumeLayout(False)
        Me.MainToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DatasetTreeView As TreeView
    Friend WithEvents SplitContainer As SplitContainer
    Friend WithEvents MainMenuStrip As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TreeNodesImageList As ImageList
    Friend WithEvents MainToolStrip As ToolStrip
    Friend WithEvents OpenSourceFileToolStripButton As ToolStripButton
    Friend WithEvents OpenDataFileToolStripButton As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents TreeViewContextMenuStrip As ContextMenuStrip
    Friend WithEvents RemoveDatasetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RemoveAllDatasetsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SkeeterDataTableControl As SkeeterDataTableControl
End Class
