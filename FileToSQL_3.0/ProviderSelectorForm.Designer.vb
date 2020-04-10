<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProviderSelectorForm
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
        Me.ProviderDataGridView = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SubmitButton = New System.Windows.Forms.Button()
        Me.ProviderLabel = New System.Windows.Forms.Label()
        CType(Me.ProviderDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProviderDataGridView
        '
        Me.ProviderDataGridView.AllowUserToAddRows = False
        Me.ProviderDataGridView.AllowUserToDeleteRows = False
        Me.ProviderDataGridView.AllowUserToOrderColumns = True
        Me.ProviderDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.ProviderDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ProviderDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProviderDataGridView.Location = New System.Drawing.Point(0, 58)
        Me.ProviderDataGridView.Name = "ProviderDataGridView"
        Me.ProviderDataGridView.ReadOnly = True
        Me.ProviderDataGridView.RowTemplate.Height = 24
        Me.ProviderDataGridView.Size = New System.Drawing.Size(1041, 403)
        Me.ProviderDataGridView.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ProviderLabel)
        Me.Panel1.Controls.Add(Me.SubmitButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 461)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1041, 49)
        Me.Panel1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1041, 58)
        Me.Panel2.TabIndex = 2
        '
        'SubmitButton
        '
        Me.SubmitButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SubmitButton.Location = New System.Drawing.Point(954, 14)
        Me.SubmitButton.Name = "SubmitButton"
        Me.SubmitButton.Size = New System.Drawing.Size(75, 23)
        Me.SubmitButton.TabIndex = 0
        Me.SubmitButton.Text = "Select"
        Me.SubmitButton.UseVisualStyleBackColor = True
        '
        'ProviderLabel
        '
        Me.ProviderLabel.Location = New System.Drawing.Point(13, 14)
        Me.ProviderLabel.Name = "ProviderLabel"
        Me.ProviderLabel.Size = New System.Drawing.Size(492, 23)
        Me.ProviderLabel.TabIndex = 1
        Me.ProviderLabel.Text = "Label1"
        '
        'ProviderSelectorForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1041, 510)
        Me.Controls.Add(Me.ProviderDataGridView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "ProviderSelectorForm"
        Me.Text = "Select a dataset connection provider"
        CType(Me.ProviderDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProviderDataGridView As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SubmitButton As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ProviderLabel As Label
End Class
