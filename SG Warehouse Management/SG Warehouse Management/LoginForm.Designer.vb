<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class LoginForm
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
        Me.loginBtn = New System.Windows.Forms.Button
        Me.userIdTxt = New System.Windows.Forms.TextBox
        Me.userPassTxt = New System.Windows.Forms.TextBox
        Me.useridLbl = New System.Windows.Forms.Label
        Me.userpassLbl = New System.Windows.Forms.Label
        Me.connectionBar = New System.Windows.Forms.StatusBar
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'loginBtn
        '
        Me.loginBtn.Location = New System.Drawing.Point(21, 115)
        Me.loginBtn.Name = "loginBtn"
        Me.loginBtn.Size = New System.Drawing.Size(195, 39)
        Me.loginBtn.TabIndex = 2
        Me.loginBtn.Text = "Login"
        '
        'userIdTxt
        '
        Me.userIdTxt.Location = New System.Drawing.Point(99, 34)
        Me.userIdTxt.Name = "userIdTxt"
        Me.userIdTxt.Size = New System.Drawing.Size(117, 23)
        Me.userIdTxt.TabIndex = 0
        '
        'userPassTxt
        '
        Me.userPassTxt.Location = New System.Drawing.Point(99, 73)
        Me.userPassTxt.Name = "userPassTxt"
        Me.userPassTxt.Size = New System.Drawing.Size(117, 23)
        Me.userPassTxt.TabIndex = 1
        '
        'useridLbl
        '
        Me.useridLbl.Location = New System.Drawing.Point(32, 38)
        Me.useridLbl.Name = "useridLbl"
        Me.useridLbl.Size = New System.Drawing.Size(65, 17)
        Me.useridLbl.Text = "User ID"
        '
        'userpassLbl
        '
        Me.userpassLbl.Location = New System.Drawing.Point(21, 77)
        Me.userpassLbl.Name = "userpassLbl"
        Me.userpassLbl.Size = New System.Drawing.Size(76, 17)
        Me.userpassLbl.Text = "Password"
        '
        'connectionBar
        '
        Me.connectionBar.Location = New System.Drawing.Point(0, 246)
        Me.connectionBar.Name = "connectionBar"
        Me.connectionBar.Size = New System.Drawing.Size(238, 24)
        Me.connectionBar.Text = "StatusBar1"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(155, 171)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(61, 20)
        Me.LinkLabel1.TabIndex = 3
        Me.LinkLabel1.Text = "Register"
        '
        'LoginForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 270)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.connectionBar)
        Me.Controls.Add(Me.userpassLbl)
        Me.Controls.Add(Me.useridLbl)
        Me.Controls.Add(Me.userPassTxt)
        Me.Controls.Add(Me.userIdTxt)
        Me.Controls.Add(Me.loginBtn)
        Me.Name = "LoginForm"
        Me.Text = "Login"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents loginBtn As System.Windows.Forms.Button
    Friend WithEvents userIdTxt As System.Windows.Forms.TextBox
    Friend WithEvents userPassTxt As System.Windows.Forms.TextBox
    Friend WithEvents useridLbl As System.Windows.Forms.Label
    Friend WithEvents userpassLbl As System.Windows.Forms.Label
    Friend WithEvents connectionBar As System.Windows.Forms.StatusBar
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel

End Class
