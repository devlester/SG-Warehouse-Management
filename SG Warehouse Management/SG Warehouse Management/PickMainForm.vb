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

                    Dim lines() As String = result.Split(ControlChars.Lf)

                    ListBox1.Items.Clear()

                    Dim isFirst As Boolean = True
                    Dim hasData As Boolean = False

                    For Each line As String In lines

                        line = line.Trim()

                        If line <> "" Then

                            Dim parts() As String = line.Split("|"c)

                            If parts.Length >= 9 Then

                                hasData = True

                                Dim store As String = parts(0)
                                Dim totalToPick As String = parts(1)
                                Dim totalPicked As String = parts(2)
                                Dim totalBoxed As String = parts(3)
                                Dim fromLoc As String = parts(4)
                                Dim style As String = parts(5)
                                Dim qty As String = parts(6)
                                Dim remainingToBox As String = parts(7)
                                Dim status As String = parts(8)

                                ' 👉 SET LABELS ONCE
                                If isFirst Then
                                    StoreLbl.Text = store
                                    TotalPickingLbl.Text = "Total to pick: " & totalToPick
                                    TotalPickeddLbl.Text = "Total Picked: " & totalPicked
                                    isFirst = False
                                End If

                                ' 👉 DISPLAY REMAINING



                                Dim qtyValue As Integer = Val(qty)

                                If qtyValue <= 0 Then
                                    Continue For
                                End If

                                If status = "COMPLETED" Then
                                    Continue For
                                End If

                                Dim display As String = fromLoc & " | " & style & " | " & qty
                                ListBox1.Items.Add(display)

                                ' Dim display As String = fromLoc & " | " & style & " | " & qty
                                'ListBox1.Items.Add(display)

                            End If

                        End If

                    Next

                    ' 👉 IF NO DATA (ALL PICKED)
                    If Not hasData Then
                        ' 👉 still show totals (call separate API or reuse last values)
                        ListBox1.Items.Add("✔ All items picked")

                        ' OPTIONAL: keep previous totals or set manually
                        ' (better if you create separate API for totals only)
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