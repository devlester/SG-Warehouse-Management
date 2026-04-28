<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class MainMenu
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
        Me.rcv_btn = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.pickingBtn = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'rcv_btn
        '
        Me.rcv_btn.Location = New System.Drawing.Point(34, 9)
        Me.rcv_btn.Name = "rcv_btn"
        Me.rcv_btn.Size = New System.Drawing.Size(170, 57)
        Me.rcv_btn.TabIndex = 0
        Me.rcv_btn.Text = "RECEIVE SHIPMENT"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(34, 198)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(170, 57)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "BACK TO MENU"
        '
        'pickingBtn
        '
        Me.pickingBtn.Location = New System.Drawing.Point(34, 72)
        Me.pickingBtn.Name = "pickingBtn"
        Me.pickingBtn.Size = New System.Drawing.Size(170, 57)
        Me.pickingBtn.TabIndex = 1
        Me.pickingBtn.Text = "PICKING ITEMS"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(34, 135)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(170, 57)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "PUTTING IN LOCATION"
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.pickingBtn)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.rcv_btn)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainMenu"
        Me.Text = "MainMenu"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rcv_btn As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents pickingBtn As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
