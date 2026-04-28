Imports System.Net
Imports System.Text
Imports System.IO
Public Class Putaway
    Public PA_StoreLocation As String
    Public PA_PickNo As String
    Public store_location As String
    Dim storeData As New Dictionary(Of String, String)
    Private Sub Putaway_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadStores()

    End Sub

    Sub LoadStores()

        Dim url As String = "http://192.168.1.122/stock-api/putaway.php"

        Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
        req.Method = "GET"

        Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(resp.GetResponseStream())

                Dim result As String = reader.ReadToEnd()
                Dim lines() As String = result.Split(ControlChars.Lf)

                SelectStore.Items.Clear()
                storeData.Clear()

                For Each line As String In lines
                    line = line.Trim()

                    If line.StartsWith("STORE|") Then
                        Dim parts() As String = line.Split("|"c)

                        If parts.Length >= 3 Then
                            Dim store As String = parts(1)
                            Dim pickno As String = parts(2)

                            SelectStore.Items.Add(store)

                            ' 👉 SAVE RELATION
                            storeData(store) = pickno
                        End If
                    End If
                Next

            End Using
        End Using

    End Sub

    Sub LoadProducts(ByVal selectedStore As String)

        ' 👉 GET PICKNO FROM DICTIONARY
        If Not storeData.ContainsKey(selectedStore) Then
            MessageBox.Show("Pick number not found!")
            Exit Sub
        End If

        PA_StoreLocation = selectedStore
        PA_PickNo = storeData(selectedStore)

        ' 👉 DEBUG (optional)
        'MessageBox.Show("Store: " & PA_StoreLocation & vbCrLf & "PickNo: " & PA_PickNo)

        Try
            Dim url As String = "http://192.168.1.122/stock-api/putaway.php?store_location=" & PA_StoreLocation

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"

            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(resp.GetResponseStream())

                    Dim result As String = reader.ReadToEnd()
                    Dim lines() As String = result.Split(ControlChars.Lf)

                    PendingPutaway.Items.Clear()

                    For Each line As String In lines
                        line = line.Trim()

                        If line.StartsWith("PRODUCT|") Then
                            Dim parts() As String = line.Split("|"c)

                            If parts.Length >= 4 Then
                                Dim location As String = parts(1)
                                Dim style As String = parts(2)
                                Dim qty As String = parts(3)

                                Dim display As String = location & " | " & style & " | " & qty
                                PendingPutaway.Items.Add(display)
                            End If
                        End If
                    Next

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading products: " & ex.Message)
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PickLocationForm.Show()
        Me.Hide()

    End Sub

    Private Sub SelectStore_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectStore.SelectedIndexChanged
        If SelectStore.SelectedItem IsNot Nothing Then

            Dim selectedStore As String = SelectStore.SelectedItem.ToString()

            ' 🔥 CALL YOUR FUNCTION HERE
            LoadProducts(selectedStore)

        End If
    End Sub


    Private Sub PendingPutaway_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PendingPutaway.SelectedIndexChanged

    End Sub
    Private Sub PendingPutaway_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles PendingPutaway.KeyDown

        If e.KeyCode = Keys.Enter Then

            If PendingPutaway.SelectedItem IsNot Nothing Then

                Dim textValue As String = PendingPutaway.SelectedItem.ToString()
                MsgBox(textValue)
                Dim parts() As String = textValue.Split("|"c)

                Dim location As String = parts(0).Trim()
                Dim styleCode As String = parts(1).Trim()

                Dim frm As New PickScanForm
                frm.stats = "fromPutaway"
                frm.LocationCode = location
                frm.StyleCode = styleCode
                frm.PickReference = PA_PickNo
                frm.StoreLocation = PA_StoreLocation
                frm.Show()
                Me.Hide()

                'MessageBox.Show("Location: " & location & vbCrLf & "StyleCode: " & styleCode)

            End If

        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim frm As New BoxForm

        ' 👉 PASS DATA
        frm.PickNo = PA_PickNo
        frm.BoxNo = ""
        ' 👉 IF MAY SELECTED BOX
        ' If txt_box_no.Text <> "" Then
        'frm.BoxNo = txt_box_no.Text
        'Else
        'frm.BoxNo = ""   ' 👉 will load ALL
        'End If

        frm.Show()
    End Sub
End Class