<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PickMainForm
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
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.StoreLbl = New System.Windows.Forms.Label
        Me.TotalPickingLbl = New System.Windows.Forms.Label
        Me.TotalPickeddLbl = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.Location = New System.Drawing.Point(12, 68)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(205, 130)
        Me.ListBox1.TabIndex = 0
        '
        'StoreLbl
        '
        Me.StoreLbl.Location = New System.Drawing.Point(12, 7)
        Me.StoreLbl.Name = "StoreLbl"
        Me.StoreLbl.Size = New System.Drawing.Size(205, 20)
        Me.StoreLbl.Text = "Store"
        '
        'TotalPickingLbl
        '
        Me.TotalPickingLbl.Location = New System.Drawing.Point(12, 27)
        Me.TotalPickingLbl.Name = "TotalPickingLbl"
        Me.TotalPickingLbl.Size = New System.Drawing.Size(205, 20)
        Me.TotalPickingLbl.Text = "Total QTY for Picking"
        '
        'TotalPickeddLbl
        '
        Me.TotalPickeddLbl.Location = New System.Drawing.Point(12, 46)
        Me.TotalPickeddLbl.Name = "TotalPickeddLbl"
        Me.TotalPickeddLbl.Size = New System.Drawing.Size(205, 20)
        Me.TotalPickeddLbl.Text = "Total QTY Picked"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(127, 202)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 30)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Back"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(12, 202)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(89, 30)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Putaway"
        '
        'PickMainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.TotalPickeddLbl)
        Me.Controls.Add(Me.TotalPickingLbl)
        Me.Controls.Add(Me.StoreLbl)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "PickMainForm"
        Me.Text = "PickMainForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents StoreLbl As System.Windows.Forms.Label
    Friend WithEvents TotalPickingLbl As System.Windows.Forms.Label
    Friend WithEvents TotalPickeddLbl As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
