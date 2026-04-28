<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class PickScanForm
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
        Me.Txt_from_location = New System.Windows.Forms.TextBox
        Me.txt_box_no = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txt_barcode = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txt_product_style = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_product_name = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txt_qty_to_pick = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txt_picked_qty = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.doneBtn = New System.Windows.Forms.Button
        Me.backBtn = New System.Windows.Forms.Button
        Me.viewBtn = New System.Windows.Forms.Button
        Me.mainBtn = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.Text = "From Location"
        '
        'Txt_from_location
        '
        Me.Txt_from_location.Enabled = False
        Me.Txt_from_location.Location = New System.Drawing.Point(13, 23)
        Me.Txt_from_location.Name = "Txt_from_location"
        Me.Txt_from_location.Size = New System.Drawing.Size(100, 23)
        Me.Txt_from_location.TabIndex = 3
        '
        'txt_box_no
        '
        Me.txt_box_no.Location = New System.Drawing.Point(119, 23)
        Me.txt_box_no.Name = "txt_box_no"
        Me.txt_box_no.Size = New System.Drawing.Size(100, 23)
        Me.txt_box_no.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(119, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 20)
        Me.Label2.Text = "to Box#"
        '
        'txt_barcode
        '
        Me.txt_barcode.Location = New System.Drawing.Point(13, 65)
        Me.txt_barcode.MaxLength = 13
        Me.txt_barcode.Name = "txt_barcode"
        Me.txt_barcode.Size = New System.Drawing.Size(206, 23)
        Me.txt_barcode.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(13, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.Text = "Barcode"
        '
        'txt_product_style
        '
        Me.txt_product_style.Enabled = False
        Me.txt_product_style.Location = New System.Drawing.Point(13, 107)
        Me.txt_product_style.Name = "txt_product_style"
        Me.txt_product_style.Size = New System.Drawing.Size(206, 23)
        Me.txt_product_style.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(13, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 20)
        Me.Label4.Text = "Stylecode"
        '
        'txt_product_name
        '
        Me.txt_product_name.Enabled = False
        Me.txt_product_name.Location = New System.Drawing.Point(13, 152)
        Me.txt_product_name.Name = "txt_product_name"
        Me.txt_product_name.Size = New System.Drawing.Size(206, 23)
        Me.txt_product_name.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(13, 135)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(100, 20)
        Me.Label5.Text = "Description"
        '
        'txt_qty_to_pick
        '
        Me.txt_qty_to_pick.Enabled = False
        Me.txt_qty_to_pick.Location = New System.Drawing.Point(13, 196)
        Me.txt_qty_to_pick.Name = "txt_qty_to_pick"
        Me.txt_qty_to_pick.Size = New System.Drawing.Size(100, 23)
        Me.txt_qty_to_pick.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(13, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 20)
        Me.Label6.Text = "QTY to Pick"
        '
        'txt_picked_qty
        '
        Me.txt_picked_qty.Location = New System.Drawing.Point(119, 196)
        Me.txt_picked_qty.Name = "txt_picked_qty"
        Me.txt_picked_qty.Size = New System.Drawing.Size(100, 23)
        Me.txt_picked_qty.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(119, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 20)
        Me.Label7.Text = "Picked QTY"
        '
        'doneBtn
        '
        Me.doneBtn.Location = New System.Drawing.Point(13, 225)
        Me.doneBtn.Name = "doneBtn"
        Me.doneBtn.Size = New System.Drawing.Size(100, 29)
        Me.doneBtn.TabIndex = 7
        Me.doneBtn.Text = "DONE"
        '
        'backBtn
        '
        Me.backBtn.Location = New System.Drawing.Point(13, 260)
        Me.backBtn.Name = "backBtn"
        Me.backBtn.Size = New System.Drawing.Size(100, 29)
        Me.backBtn.TabIndex = 9
        Me.backBtn.Text = "BACK"
        '
        'viewBtn
        '
        Me.viewBtn.Location = New System.Drawing.Point(119, 225)
        Me.viewBtn.Name = "viewBtn"
        Me.viewBtn.Size = New System.Drawing.Size(100, 29)
        Me.viewBtn.TabIndex = 8
        Me.viewBtn.Text = "VIEW"
        '
        'mainBtn
        '
        Me.mainBtn.Location = New System.Drawing.Point(119, 260)
        Me.mainBtn.Name = "mainBtn"
        Me.mainBtn.Size = New System.Drawing.Size(100, 29)
        Me.mainBtn.TabIndex = 10
        Me.mainBtn.Text = "MAIN"
        '
        'PickScanForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.mainBtn)
        Me.Controls.Add(Me.viewBtn)
        Me.Controls.Add(Me.backBtn)
        Me.Controls.Add(Me.doneBtn)
        Me.Controls.Add(Me.txt_picked_qty)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txt_qty_to_pick)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txt_product_name)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txt_product_style)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txt_barcode)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txt_box_no)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Txt_from_location)
        Me.Controls.Add(Me.Label1)
        Me.Name = "PickScanForm"
        Me.Text = "PickScanForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Txt_from_location As System.Windows.Forms.TextBox
    Friend WithEvents txt_box_no As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_barcode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt_product_style As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txt_product_name As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt_qty_to_pick As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txt_picked_qty As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents doneBtn As System.Windows.Forms.Button
    Friend WithEvents backBtn As System.Windows.Forms.Button
    Friend WithEvents viewBtn As System.Windows.Forms.Button
    Friend WithEvents mainBtn As System.Windows.Forms.Button
End Class
