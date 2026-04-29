Imports System.Net
Imports System.Text
Imports System.IO



Public Class PickMainForm
    Public StoreLocation As String
    Public pick_no As String

 

    Private Sub PickMainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadPickingItems()
    End Sub


    Sub LoadPickingItems()

        Try

            Dim url As String = "http://192.168.1.122/stock-api/get_picking_items.php?store_location=" & StoreLocation & "&pick_no=" & pick_no

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"
            req.Timeout = 5000
            req.AllowWriteStreamBuffering = True
            req.Expect = String.Empty

            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)

                Using reader As New StreamReader(resp.GetResponseStream())

                    Dim result As String = reader.ReadToEnd()

                    ListBox1.Items.Clear()

                    StoreLbl.Text        = Me.StoreLocation
                    TotalPickingLbl.Text = "Total to pick: " & JsonGetStr(result, "total_to_pick")
                    TotalPickeddLbl.Text = "Total Picked: "  & JsonGetStr(result, "total_picked")

                    Dim items As List(Of Dictionary(Of String, String)) = JsonGetObjectList(result, "items")

                    If items.Count = 0 Then
                        ListBox1.Items.Add("✔ All items picked")
                    Else
                        For Each item As Dictionary(Of String, String) In items
                            Dim qty As Integer    = Val(item("remaining_to_pick"))
                            Dim status As String  = item("status")

                            If qty <= 0 OrElse status = "COMPLETED" Then Continue For

                            ListBox1.Items.Add(item("product_location") & " | " & item("product_style") & " | " & qty.ToString())
                        Next
                    End If

                End Using

            End Using

        Catch ex As Exception

            MessageBox.Show("Server Offline")

        End Try

    End Sub

    Private Sub ListBox1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ListBox1.KeyDown

        If e.KeyCode = Keys.Enter Then

            If ListBox1.SelectedItem IsNot Nothing Then

                Dim textValue As String = ListBox1.SelectedItem.ToString()

                Dim parts() As String = textValue.Split("|"c)

                Dim location As String = parts(0).Trim()
                Dim styleCode As String = parts(1).Trim()

                Dim frm As New PickScanForm
                frm.stats = "fromMain"
                frm.LocationCode = location
                frm.StyleCode = styleCode
                frm.PickReference = Me.pick_no
                frm.StoreLocation = Me.StoreLocation
                frm.Show()
                Me.Hide()

                'MessageBox.Show("Location: " & location & vbCrLf & "StyleCode: " & styleCode)

            End If

        End If

    End Sub
    
   
    Private Sub TotalPickeddLbl_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TotalPickeddLbl.ParentChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PickLocationForm.Show()

        Me.Hide()

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click


        Dim frm As New Putaway

        frm.Show()
        frm.SelectStore.SelectedItem = Me.StoreLocation
        Me.Hide()

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
End Class