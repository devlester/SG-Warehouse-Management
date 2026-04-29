Imports System.Net
Imports System.Text
Imports System.IO
Imports System.Windows.Forms


Public Class PickScanForm
    Public LocationCode As String
    Public StyleCode As String
    Public PickReference As String
    Public StoreLocation As String
    Public stats As String



    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles backBtn.Click

        PickLocationForm.Show()
        Me.Hide()
    End Sub

    Private Sub PickScanForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub txt_Barcode_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txt_barcode.KeyDown
        Dim isProcessing As Boolean = False

        If e.KeyCode = Keys.Enter Then

            If isProcessing Then Exit Sub

            isProcessing = True

            Dim scannedBarcode As String = txt_barcode.Text.Trim()

            If scannedBarcode <> "" Then
                ValidateBarcode(scannedBarcode)




                'MessageBox.Show("Scanned: " & barcode)

                'Call API dito
                'Validate barcode


                isProcessing = False
            End If

        End If

    End Sub


    Private Sub ValidateBarcode(ByVal scannedBarcode As String)

        Try
            Dim url As String = "http://192.168.1.122/stock-api/get_items.php?location=" & Uri.EscapeDataString(LocationCode) & _
                               "&stylecode=" & Uri.EscapeDataString(StyleCode) & _
                               "&barcode="   & Uri.EscapeDataString(scannedBarcode) & _
                               "&stats="     & Uri.EscapeDataString(stats)

            Dim request As WebRequest = WebRequest.Create(url)
            request.Method  = "GET"
            request.Timeout = 10000

            Using response As WebResponse = request.GetResponse()
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)

                        Dim result As String = reader.ReadToEnd().Trim()

                        If String.IsNullOrEmpty(result) OrElse JsonGetStr(result, "status") = "invalid" Then
                            MsgBox("Invalid barcode!", MsgBoxStyle.OkOnly)
                            txt_barcode.Focus()
                            txt_barcode.SelectAll()
                            Exit Sub
                        End If

                        ' Reset fields before populating
                        Txt_from_location.Text = ""
                        txt_qty_to_pick.Text   = ""
                        txt_picked_qty.Text    = ""
                        txt_product_style.Text = ""
                        txt_product_name.Text  = ""
                        txt_box_no.Text        = ""

                        Txt_from_location.Text = JsonGetStr(result, "product_location")
                        txt_barcode.Text       = JsonGetStr(result, "product_code")
                        txt_qty_to_pick.Text   = JsonGetStr(result, "qty_to_pick")
                        txt_product_style.Text = JsonGetStr(result, "product_style")
                        txt_product_name.Text  = JsonGetStr(result, "product_name")

                        Dim pickedQty As Integer = Val(JsonGetStr(result, "picked_qty"))

                        If stats = "fromPutaway" Then
                            txt_qty_to_pick.Text = pickedQty.ToString()
                            Label6.Text = "Remaining"
                            Label7.Text = "Qty to Put"
                        Else
                            txt_picked_qty.Text = pickedQty.ToString()
                        End If

                        txt_picked_qty.Focus()
                        txt_picked_qty.SelectAll()

                    End Using
                End Using
            End Using

        Catch ex As WebException

            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim resp As String = reader.ReadToEnd()
                    MessageBox.Show("Server Error: " & resp)
                End Using
            Else
                MessageBox.Show("Connection Error: " & ex.Message)
            End If

            txt_barcode.Focus()
            txt_barcode.SelectAll()

        Catch ex As Exception
            MessageBox.Show("Unexpected error: " & ex.Message)
        End Try

    End Sub




    Private Sub txt_picked_qty_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txt_picked_qty.KeyDown

        If e.KeyCode = Keys.Enter Then

            ' 👉 Validate input
            If txt_picked_qty.Text = "" OrElse Not IsNumeric(txt_picked_qty.Text) Then
                MessageBox.Show("Please enter valid quantity")
                txt_picked_qty.Focus()
                txt_picked_qty.SelectAll()
                Exit Sub
            End If

            ' 👉 Optional: check over picking
            If Val(txt_picked_qty.Text) > Val(txt_qty_to_pick.Text) Then
                MessageBox.Show("Quantity exceeds required!")
                txt_picked_qty.Focus()
                txt_picked_qty.SelectAll()
                Exit Sub
            End If

            ' 👉 Move to Box No
            txt_box_no.Focus()
            txt_box_no.SelectAll()

        End If

    End Sub

    Private Sub txt_box_no_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txt_box_no.KeyDown

        If e.KeyCode = Keys.Enter Then

            If txt_box_no.Text.Trim() = "" Then

                Dim resutBox As MsgBoxResult

                resutBox = MsgBox("Do you want input Box #?", vbYesNo + vbQuestion, "Confirmation")

                If resutBox = vbYes Then
                    txt_box_no.Focus()
                    Exit Sub

                Else
                    txt_box_no.Text = 0
                    SavePick()

                End If

            Else

                SavePick()

            End If


            ' 👉 CALL SAVE FUNCTION (API or DB)


            ' 👉 Reset for next scan



        End If

    End Sub

    Private Sub SavePick()

        Try
            ' ✅ VALIDATION
            If String.IsNullOrEmpty(Me.StoreLocation) OrElse String.IsNullOrEmpty(Me.PickReference) Then
                MessageBox.Show("Missing Store Location or Pick Reference")
                Exit Sub
            End If

            If String.IsNullOrEmpty(txt_barcode.Text) OrElse String.IsNullOrEmpty(txt_picked_qty.Text) Then
                MessageBox.Show("Please input barcode and quantity")
                Exit Sub
            End If

            Dim url As String = "http://192.168.1.122/stock-api/save_pick.php"

            ' ✅ SAFE VALUES
            Dim barcode As String = If(txt_barcode.Text, "")
            Dim pickedQty As String = If(txt_picked_qty.Text, "")
            Dim storeLoc As String = If(Me.StoreLocation, "")
            Dim pickNo As String = If(Me.PickReference, "")
            Dim boxNo As String = If(txt_box_no.Text, "")
            Dim fromLoc As String = If(Txt_from_location.Text, "")

            ' 👉 POST DATA
            Dim postData As String = "barcode=" & Uri.EscapeDataString(barcode) & _
                                     "&picked_qty=" & Uri.EscapeDataString(pickedQty) & _
                                     "&store_location=" & Uri.EscapeDataString(storeLoc) & _
                                     "&pick_no=" & Uri.EscapeDataString(pickNo) & _
                                     "&box_no=" & Uri.EscapeDataString(boxNo) & _
                                     "&from_location=" & Uri.EscapeDataString(fromLoc)
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(postData)

            ' 👉 REQUEST
            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "POST"
            req.ContentType = "application/x-www-form-urlencoded"
            req.ContentLength = bytes.Length
            req.Timeout = 15000
            req.KeepAlive = False

            Using reqStream As Stream = req.GetRequestStream()
                reqStream.Write(bytes, 0, bytes.Length)
            End Using

            ' 👉 RESPONSE
            Using response As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())

                    Dim result As String = reader.ReadToEnd().Trim()

                    If String.IsNullOrEmpty(result) Then
                        MessageBox.Show("Empty response from server")
                        Exit Sub
                    End If

                    Dim status As String = JsonGetStr(result, "status")
                    Dim action As String = JsonGetStr(result, "action")

                    If status = "success" Then

                        If action = "PICK_UPDATED" Then
                            MessageBox.Show("Quantity updated!" & vbCrLf & "Saved Successfully!")
                        ElseIf action = "PICK_INSERTED" Then
                            MessageBox.Show("New pick saved!" & vbCrLf & "Saved Successfully!")
                        ElseIf action = "PUTAWAY" Then
                            MessageBox.Show("You have successfully put in a box!")
                        End If

                        ' ✅ RETURN TO MAIN FORM (SAFE)
                        If stats = "fromPutaway" Then

                            If Me.Owner IsNot Nothing Then
                                Dim putFrm As Putaway = CType(Me.Owner, Putaway)
                                putFrm.Show()


                                ' or your method
                            Else
                                Dim putFrm As New Putaway
                                putFrm.Show()
                                putFrm.SelectStore.SelectedItem = storeLoc

                            End If

                        Else
                            ' 👉 DEFAULT PICK FLOW
                            If Me.Owner IsNot Nothing Then
                                Dim mainfrm As PickMainForm = CType(Me.Owner, PickMainForm)
                                mainfrm.Show()
                                mainfrm.LoadPickingItems()
                            Else
                                Dim mainfrm As New PickMainForm
                                mainfrm.StoreLocation = Me.StoreLocation
                                mainfrm.pick_no = Me.PickReference
                                mainfrm.Show()
                            End If
                        End If

                        Me.Close()

                    ElseIf status = "error" Then
                        MessageBox.Show("Server Error: " & JsonGetStr(result, "message"))

                    Else
                        MessageBox.Show("Unexpected response: " & result)
                    End If

                End Using
            End Using

        Catch ex As WebException

            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    MessageBox.Show("Web Error: " & reader.ReadToEnd())
                End Using
            Else
                MessageBox.Show("Network Error: " & ex.Message)
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.ToString())
        End Try

    End Sub
   
    Private Sub txt_box_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_box_no.TextChanged

    End Sub
End Class