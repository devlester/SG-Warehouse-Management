<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReceiveOrdersForm
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
        Me.Whs_cbox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.OrdDate_cbox = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.YearCollection_cbox = New System.Windows.Forms.ComboBox
        Me.OrdProceed_btn = New System.Windows.Forms.Button
        Me.OrdCancel_btn = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Whs_cbox
        '
        Me.Whs_cbox.Location = New System.Drawing.Point(26, 37)
        Me.Whs_cbox.Name = "Whs_cbox"
        Me.Whs_cbox.Size = New System.Drawing.Size(178, 23)
        Me.Whs_cbox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(26, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 20)
        Me.Label1.Text = "Warehouse"
        '
        'OrdDate_cbox
        '
        Me.OrdDate_cbox.Location = New System.Drawing.Point(26, 98)
        Me.OrdDate_cbox.Name = "OrdDate_cbox"
        Me.OrdDate_cbox.Size = New System.Drawing.Size(178, 24)
        Me.OrdDate_cbox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(26, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 20)
        Me.Label2.Text = "Date Receive"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(26, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(140, 20)
        Me.Label3.Text = "Shipment Collection"
        '
        'YearCollection_cbox
        '
        Me.YearCollection_cbox.Location = New System.Drawing.Point(26, 167)
        Me.YearCollection_cbox.Name = "YearCollection_cbox"
        Me.YearCollection_cbox.Size = New System.Drawing.Size(178, 23)
        Me.YearCollection_cbox.TabIndex = 6
        '
        'OrdProceed_btn
        '
        Me.OrdProceed_btn.Location = New System.Drawing.Point(26, 208)
        Me.OrdProceed_btn.Name = "OrdProceed_btn"
        Me.OrdProceed_btn.Size = New System.Drawing.Size(77, 35)
        Me.OrdProceed_btn.TabIndex = 8
        Me.OrdProceed_btn.Text = "Apply"
        '
        'OrdCancel_btn
        '
        Me.OrdCancel_btn.Location = New System.Drawing.Point(127, 208)
        Me.OrdCancel_btn.Name = "OrdCancel_btn"
        Me.OrdCancel_btn.Size = New System.Drawing.Size(77, 35)
        Me.OrdCancel_btn.TabIndex = 9
        Me.OrdCancel_btn.Text = "Cancel"
        '
        'ReceiveOrdersForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.OrdCancel_btn)
        Me.Controls.Add(Me.OrdProceed_btn)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.YearCollection_cbox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.OrdDate_cbox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Whs_cbox)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReceiveOrdersForm"
        Me.Text = "Receive Orders"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Whs_cbox As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OrdDate_cbox As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents YearCollection_cbox As System.Windows.Forms.ComboBox
    Friend WithEvents OrdProceed_btn As System.Windows.Forms.Button
    Friend WithEvents OrdCancel_btn As System.Windows.Forms.Button
End Class
