<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Putaway
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
        Me.PendingPutaway = New System.Windows.Forms.ListBox
        Me.SelectStore = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PendingPutaway
        '
        Me.PendingPutaway.Location = New System.Drawing.Point(16, 45)
        Me.PendingPutaway.Name = "PendingPutaway"
        Me.PendingPutaway.Size = New System.Drawing.Size(202, 162)
        Me.PendingPutaway.TabIndex = 0
        '
        'SelectStore
        '
        Me.SelectStore.Location = New System.Drawing.Point(16, 16)
        Me.SelectStore.Name = "SelectStore"
        Me.SelectStore.Size = New System.Drawing.Size(202, 23)
        Me.SelectStore.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(146, 213)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 37)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Back"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(16, 213)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 37)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "View Box"
        '
        'Putaway
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.SelectStore)
        Me.Controls.Add(Me.PendingPutaway)
        Me.Name = "Putaway"
        Me.Text = "Putaway"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PendingPutaway As System.Windows.Forms.ListBox
    Friend WithEvents SelectStore As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
