Imports System.Net
Imports System.IO

Public Class ReceiveOrdersForm

    Private Sub ReceiveOrdersForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadWarehouses()
        GetInvoice()
    End Sub
    Private Sub LoadWarehouses()
        Try
            Dim url As String = "http://192.168.1.122/stock-api/get_warehouse.php"

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"
            req.Timeout = 15000

            Dim resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(resp.GetResponseStream())
            Dim responseText As String = reader.ReadToEnd()

            reader.Close()
            resp.Close()

            Whs_cbox.Items.Clear()

            Dim lines() As String = responseText.Split(ControlChars.Lf)

            For Each line As String In lines
                If line.Trim() <> "" Then
                    Whs_cbox.Items.Add(line.Trim())
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error loading warehouses: " & ex.Message)
        End Try
    End Sub
    Private Sub GetInvoice()
        Try
            Dim url As String = "http://192.168.1.122/stock-api/get_inv.php"

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"
            req.Timeout = 15000

            Dim resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(resp.GetResponseStream())
            Dim responseText As String = reader.ReadToEnd()

            reader.Close()
            resp.Close()

            YearCollection_cbox.Items.Clear()

            Dim lines() As String = responseText.Split(ControlChars.Lf)

            For Each line As String In lines
                If line.Trim() <> "" Then
                    YearCollection_cbox.Items.Add(line.Trim())
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Error Getting Invoice #: " & ex.Message)
        End Try
    End Sub

    Private Sub OrdCancel_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdCancel_btn.Click
        Me.Hide()
        MainMenu.Show()

    End Sub

    Private Sub OrdProceed_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrdProceed_btn.Click
        ReceiveScanForm.Show()
        Me.Hide()

    End Sub
End Class