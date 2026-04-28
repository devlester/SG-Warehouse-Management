Imports System.Net
Imports System.Text
Imports System.IO

Public Class ReceiveScanForm


    Private Sub ReceiveScanForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub barcodeTxt_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles barcodeTxt.KeyDown
        If e.KeyCode = Keys.Enter Then
            CheckBarcode()
            QTYTxt.Focus()

        End If
    End Sub
    Private Sub CheckBarcode()

        Try
            Dim url As String = "http://192.168.1.122/stock-api/scan.php"
            Dim postData As String = "year_collection=" & Uri.EscapeDataString(ReceiveOrdersForm.YearCollection_cbox.Text) & _
                        "&product_code=" & Uri.EscapeDataString(barcodeTxt.Text)
            Dim bytes() As Byte = Encoding.UTF8.GetBytes(postData)
            MessageBox.Show(postData)
            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "POST"
            req.ContentType = "application/x-www-form-urlencoded"
            req.ContentLength = bytes.Length
            req.Timeout = 15000
            req.AllowWriteStreamBuffering = True
            req.Expect = String.Empty

            Using stream As Stream = req.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            Using response As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    Dim result As String = reader.ReadToEnd().Trim()

                    If result.StartsWith("FOUND|") Then
                        Dim parts() As String = result.Split("|"c)

                        stylecodeTxt.Text = parts(1)
                        descriptionTxt.Text = parts(2)

                    ElseIf result = "NOT_FOUND" Then
                        MessageBox.Show("❌ Product not found for this incoming")
                    Else
                        MessageBox.Show("⚠ Server error")
                    End If
                End Using
            End Using


        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try

    End Sub
End Class