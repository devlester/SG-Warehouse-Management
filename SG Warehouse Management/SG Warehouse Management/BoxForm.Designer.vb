<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class BoxForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.CH_box = New System.Windows.Forms.ColumnHeader
        Me.CH_style = New System.Windows.Forms.ColumnHeader
        Me.CH_Desc = New System.Windows.Forms.ColumnHeader
        Me.CH_qty = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(220, 20)
        Me.Label1.Text = "Store Name:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 20)
        Me.Label2.Text = "Box # :"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(66, 35)
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(60, 24)
        Me.NumericUpDown1.TabIndex = 5
        Me.NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'RadioButton1
        '
        Me.RadioButton1.Location = New System.Drawing.Point(149, 39)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(63, 20)
        Me.RadioButton1.TabIndex = 6
        Me.RadioButton1.Text = "All"
        '
        'ListView1
        '
        Me.ListView1.Columns.Add(Me.CH_box)
        Me.ListView1.Columns.Add(Me.CH_style)
        Me.ListView1.Columns.Add(Me.CH_Desc)
        Me.ListView1.Columns.Add(Me.CH_qty)
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(8, 62)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(220, 200)
        Me.ListView1.TabIndex = 7
        '
        'CH_box
        '
        Me.CH_box.Text = "Box"
        Me.CH_box.Width = 40
        '
        'CH_style
        '
        Me.CH_style.Text = "Style"
        Me.CH_style.Width = 50
        '
        'CH_Desc
        '
        Me.CH_Desc.Text = "Description"
        Me.CH_Desc.Width = 60
        '
        'CH_qty
        '
        Me.CH_qty.Text = "Qty"
        Me.CH_qty.Width = 20
        '
        'BoxForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "BoxForm"
        Me.Text = "BoxForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents CH_box As System.Windows.Forms.ColumnHeader
    Friend WithEvents CH_style As System.Windows.Forms.ColumnHeader
    Friend WithEvents CH_Desc As System.Windows.Forms.ColumnHeader
    Friend WithEvents CH_qty As System.Windows.Forms.ColumnHeader
End Class
