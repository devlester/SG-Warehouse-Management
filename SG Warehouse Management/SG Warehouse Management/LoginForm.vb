Imports System.Net
Imports System.Text
Imports System.IO

Public Class LoginForm

    Private Sub loginBtn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles loginBtn.Click
        Login()
    End Sub

    Private Sub Login()
        Try
            ' Your PHP login URL on XAMPP
            Dim url As String = "http://192.168.1.122/stock-api/login.php"

            ' Prepare POST data
            Dim postData As String = "username=" & Uri.EscapeDataString(userIdTxt.Text) & _
                                     "&password=" & Uri.EscapeDataString(userPassTxt.Text)

            Dim bytes() As Byte = Encoding.UTF8.GetBytes(postData)

            ' Create request
            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "POST"
            req.ContentType = "application/x-www-form-urlencoded"
            req.ContentLength = bytes.Length
            req.Timeout = 15000
            req.ReadWriteTimeout = 15000
            req.KeepAlive = False

            ' ✅ Important fixes to prevent network error
            req.AllowWriteStreamBuffering = True       ' Buffer POST data
            req.AllowAutoRedirect = True               ' Follow redirects if any
            req.Expect = String.Empty                  ' Disable "Expect: 100-Continue"

            ' Send POST data
            Using reqStream As Stream = req.GetRequestStream()
                reqStream.Write(bytes, 0, bytes.Length)
            End Using

            ' Get response
            Using response As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim result As String = reader.ReadToEnd().Trim()

                    ' Check PHP response
                    If result.StartsWith("OK|") Then
                        Dim fullname As String = result.Split("|"c)(1)
                        MessageBox.Show("Welcome " & fullname)
                        MainMenu.Show()
                        Me.Hide()
                    ElseIf result.StartsWith("ERROR|") Then
                        MessageBox.Show("Server Error: " & result.Split("|"c)(1))
                    Else
                        MessageBox.Show("Invalid username or password")
                    End If
                End Using
            End Using

        Catch ex As WebException
            ' More detailed WebException handling
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim resp As String = reader.ReadToEnd()
                    MessageBox.Show("WebException Response: " & resp)
                End Using
            Else
                MessageBox.Show("Network Error: " & ex.Message)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckConnection()
        Try
            Dim url As String = "http://192.168.1.122/stock-api/ping.php"

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"
            req.Timeout = 5000
            req.AllowWriteStreamBuffering = True
            req.Expect = String.Empty

            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(resp.GetResponseStream())
                    Dim result As String = reader.ReadToEnd().Trim()

                    If result = "CONNECTED" Then
                        connectionBar.Text = "🟢 Connected to Database"
                    Else
                        connectionBar.Text = "🔴 No Database Connection"
                    End If
                End Using
            End Using

        Catch ex As Exception
            connectionBar.Text = "🔴 Server Offline"
        End Try
    End Sub


    Private Sub LoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckConnection()

    End Sub
End Class
