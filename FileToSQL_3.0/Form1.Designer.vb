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
        Me.DatasetTreeView = New System.Windows.Forms.TreeView()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SkeeterDataTableControl = New FileToSQL_3._0.SkeeterDataTableControl()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel1.SuspendLayout()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'DatasetTreeView
        '
        Me.DatasetTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DatasetTreeView.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DatasetTreeView.Location = New System.Drawing.Point(0, 0)
        Me.DatasetTreeView.Name = "DatasetTreeView"
        Me.DatasetTreeView.Size = New System.Drawing.Size(173, 606)
        Me.DatasetTreeView.TabIndex = 0
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer.Name = "SplitContainer"
        '
        'SplitContainer.Panel1
        '
        Me.SplitContainer.Panel1.Controls.Add(Me.DatasetTreeView)
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SkeeterDataTableControl)
        Me.SplitContainer.Size = New System.Drawing.Size(935, 606)
        Me.SplitContainer.SplitterDistance = 173
        Me.SplitContainer.TabIndex = 1
        '
        'SkeeterDataTableControl
        '
        Me.SkeeterDataTableControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SkeeterDataTableControl.Location = New System.Drawing.Point(0, 0)
        Me.SkeeterDataTableControl.Name = "SkeeterDataTableControl"
        Me.SkeeterDataTableControl.Size = New System.Drawing.Size(758, 606)
        Me.SkeeterDataTableControl.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(935, 606)
        Me.Controls.Add(Me.SplitContainer)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer.Panel1.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DatasetTreeView As TreeView
    Friend WithEvents SplitContainer As SplitContainer
    Friend WithEvents SkeeterDataTableControl As SkeeterDataTableControl
End Class
