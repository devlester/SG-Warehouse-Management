<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ReceiveScanForm
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
        Me.LocationTxt = New System.Windows.Forms.TextBox
        Me.barcodeTxt = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.stylecodeTxt = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.descriptionTxt = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.QTYTxt = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox6 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.ScanSaveBtn = New System.Windows.Forms.Button
        Me.ScanEndBtn = New System.Windows.Forms.Button
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(24, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 20)
        Me.Label1.Text = "Location"
        '
        'LocationTxt
        '
        Me.LocationTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocationTxt.Location = New System.Drawing.Point(95, 14)
        Me.LocationTxt.Name = "LocationTxt"
        Me.LocationTxt.Size = New System.Drawing.Size(114, 23)
        Me.LocationTxt.TabIndex = 1
        '
        'barcodeTxt
        '
        Me.barcodeTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.barcodeTxt.Location = New System.Drawing.Point(95, 44)
        Me.barcodeTxt.Name = "barcodeTxt"
        Me.barcodeTxt.Size = New System.Drawing.Size(114, 23)
        Me.barcodeTxt.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(24, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 20)
        Me.Label2.Text = "Barcode"
        '
        'stylecodeTxt
        '
        Me.stylecodeTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.stylecodeTxt.Enabled = False
        Me.stylecodeTxt.Location = New System.Drawing.Point(95, 76)
        Me.stylecodeTxt.Name = "stylecodeTxt"
        Me.stylecodeTxt.Size = New System.Drawing.Size(114, 23)
        Me.stylecodeTxt.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.Location = New System.Drawing.Point(24, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 20)
        Me.Label3.Text = "Stylecode"
        '
        'descriptionTxt
        '
        Me.descriptionTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.descriptionTxt.Enabled = False
        Me.descriptionTxt.Location = New System.Drawing.Point(95, 109)
        Me.descriptionTxt.Name = "descriptionTxt"
        Me.descriptionTxt.Size = New System.Drawing.Size(114, 23)
        Me.descriptionTxt.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.Location = New System.Drawing.Point(24, 112)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 20)
        Me.Label4.Text = "Description"
        '
        'QTYTxt
        '
        Me.QTYTxt.Location = New System.Drawing.Point(95, 142)
        Me.QTYTxt.Name = "QTYTxt"
        Me.QTYTxt.Size = New System.Drawing.Size(55, 23)
        Me.QTYTxt.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(24, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 20)
        Me.Label5.Text = "QTY"
        '
        'TextBox6
        '
        Me.TextBox6.Enabled = False
        Me.TextBox6.Location = New System.Drawing.Point(95, 174)
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(55, 23)
        Me.TextBox6.TabIndex = 15
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(24, 177)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 20)
        Me.Label6.Text = "Unit"
        '
        'ScanSaveBtn
        '
        Me.ScanSaveBtn.Location = New System.Drawing.Point(24, 214)
        Me.ScanSaveBtn.Name = "ScanSaveBtn"
        Me.ScanSaveBtn.Size = New System.Drawing.Size(84, 38)
        Me.ScanSaveBtn.TabIndex = 17
        Me.ScanSaveBtn.Text = "Save"
        '
        'ScanEndBtn
        '
        Me.ScanEndBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScanEndBtn.Location = New System.Drawing.Point(125, 214)
        Me.ScanEndBtn.Name = "ScanEndBtn"
        Me.ScanEndBtn.Size = New System.Drawing.Size(84, 38)
        Me.ScanEndBtn.TabIndex = 18
        Me.ScanEndBtn.Text = "End"
        '
        'CheckBox1
        '
        Me.CheckBox1.Location = New System.Drawing.Point(166, 145)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(28, 20)
        Me.CheckBox1.TabIndex = 25
        '
        'ReceiveScanForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(238, 295)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.ScanEndBtn)
        Me.Controls.Add(Me.ScanSaveBtn)
        Me.Controls.Add(Me.TextBox6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.QTYTxt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.descriptionTxt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.stylecodeTxt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.barcodeTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LocationTxt)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ReceiveScanForm"
        Me.Text = "ReceiveScanForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LocationTxt As System.Windows.Forms.TextBox
    Friend WithEvents barcodeTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents stylecodeTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents descriptionTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents QTYTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ScanSaveBtn As System.Windows.Forms.Button
    Friend WithEvents ScanEndBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
