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
            MsgBox(stats)
            Dim url As String = "http://192.168.1.122/stock-api/get_items.php?location=" & LocationCode & _
                               "&stylecode=" & StyleCode & _
                               "&barcode=" & scannedBarcode & _
                               "&stats=" & stats

            Dim request As WebRequest = WebRequest.Create(url)
            request.Method = "GET" ' ✅ FIX (you are using query string)
            request.Timeout = 10000 ' 🔥 increase timeout

            Using response As WebResponse = request.GetResponse()
                Using stream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(stream)

                        Dim result As String = reader.ReadToEnd().Trim()

                        ' 👉 DEBUG (optional)
                        'MessageBox.Show(result)

                        ' 👉 INVALID RESPONSE
                        If result = "INVALID" OrElse String.IsNullOrEmpty(result) Then
                            MsgBox("Invalid barcode!", MsgBoxStyle.OkOnly)

                            txt_barcode.Focus()
                            txt_barcode.SelectAll()
                            Exit Sub
                        End If

                        ' 👉 CLEAN RESPONSE
                        result = result.Replace("{", "").Replace("}", "").Replace("""", "")

                        Dim fields() As String = result.Split(",")

                        ' 👉 RESET (important para di mag halo old data)
                        Txt_from_location.Text = ""
                        txt_qty_to_pick.Text = ""
                        txt_picked_qty.Text = ""
                        txt_product_style.Text = ""
                        txt_product_name.Text = ""
                        txt_box_no.Text = "" ' optional

                        Dim pickedQty As Integer = 0
                        Dim boxedQty As Integer = 0
                        Dim remainingToBox As Integer = 0

                        For Each field In fields
                            Dim pair() As String = field.Split(":"c)

                            If pair.Length = 2 Then
                                Dim key As String = pair(0).Trim()
                                Dim value As String = pair(1).Trim()

                                Select Case key

                                    Case "product_location"
                                        Txt_from_location.Text = value

                                    Case "product_code"
                                        txt_barcode.Text = value

                                    Case "qty_to_pick"
                                        txt_qty_to_pick.Text = value

                                    Case "picked_qty"
                                        pickedQty = Val(value)

                                    Case "putaway_qty", "boxed_qty"
                                        boxedQty = Val(value)

                                    Case "remaining_to_box"
                                        remainingToBox = Val(value)

                                    Case "product_style"
                                        txt_product_style.Text = value

                                    Case "product_name"
                                        txt_product_name.Text = value

                                End Select
                            End If
                        Next

                        ' 👉 FINAL DISPLAY LOGIC

                        If stats = "fromPutaway" Then
                            ' 🔥 show remaining to box
                            txt_qty_to_pick.Text = pickedQty.ToString()
                            Label6.Text = "Remaining"
                            Label7.Text = "Qty to Put"

                        Else
                            ' 🔥 show picked qty

                            txt_picked_qty.Text = remainingToBox.ToString()
                        End If

                        ' 👉 VALIDATION (prevent negative / invalid)
                        'If Val(txt_picked_qty.Text) <= 0 Then
                        'MsgBox("No remaining quantity!", MsgBoxStyle.Exclamation)

                        'txt_barcode.Focus()
                        'txt_barcode.SelectAll()
                        'Exit Sub
                        'End If

                        ' 👉 MOVE TO QTY INPUT
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
            MsgBox(postData)
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

                    Dim parts() As String = result.Split("|"c)

                    If parts.Length = 0 Then
                        MessageBox.Show("Invalid response: " & result)
                        Exit Sub
                    End If

                    Dim status As String = parts(0)

                    If status = "SUCCESS" Then

                        Dim action As String = ""
                        If parts.Length > 1 Then action = parts(1)

                        If action = "UPDATED" Then
                            MessageBox.Show("Quantity updated!")
                            MessageBox.Show("Saved Successfully!")
                        ElseIf action = "INSERTED" Then
                            MessageBox.Show("New box saved!")
                            MessageBox.Show("Saved Successfully!")
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

                    ElseIf status = "ERROR" Then

                        Dim errorMsg As String = ""
                        If parts.Length > 1 Then errorMsg = parts(1)

                        MessageBox.Show("Server Error: " & errorMsg)

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