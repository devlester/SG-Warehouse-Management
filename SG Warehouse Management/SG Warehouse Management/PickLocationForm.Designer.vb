<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PickLocationForm
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
        Me.StoreLocCBox = New System.Windows.Forms.ComboBox
        Me.ProceedToPickBtn = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.PickRefCBox = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.Text = "Store Location"
        '
        'StoreLocCBox
        '
        Me.StoreLocCBox.Location = New System.Drawing.Point(16, 47)
        Me.StoreLocCBox.Name = "StoreLocCBox"
        Me.StoreLocCBox.Size = New System.Drawing.Size(206, 23)
        Me.StoreLocCBox.TabIndex = 0
        '
        'ProceedToPickBtn
        '
        Me.ProceedToPickBtn.Enabled = False
        Me.ProceedToPickBtn.Location = New System.Drawing.Point(16, 143)
        Me.ProceedToPickBtn.Name = "ProceedToPickBtn"
        Me.ProceedToPickBtn.Size = New System.Drawing.Size(206, 51)
        Me.ProceedToPickBtn.TabIndex = 2
        Me.ProceedToPickBtn.Text = "Proceed to Pick"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(16, 200)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(206, 51)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Back"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 20)
        Me.Label2.Text = "Picking Reference"
        '
        'PickRefCBox
        '
        Me.PickRefCBox.Location = New System.Drawing.Point(16, 104)
        Me.PickRefCBox.Name = "PickRefCBox"
        Me.PickRefCBox.Size = New System.Drawing.Size(206, 23)
        Me.PickRefCBox.TabIndex = 1
        '
        'PickLocationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.ControlBox = False
        Me.Controls.Add(Me.PickRefCBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ProceedToPickBtn)
        Me.Controls.Add(Me.StoreLocCBox)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PickLocationForm"
        Me.Text = "PickLocationForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StoreLocCBox As System.Windows.Forms.ComboBox
    Friend WithEvents ProceedToPickBtn As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PickRefCBox As System.Windows.Forms.ComboBox
End Class
