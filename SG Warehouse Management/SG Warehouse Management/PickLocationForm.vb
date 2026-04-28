Imports System.Net
Imports System.Text
Imports System.IO

Public Class PickLocationForm
   
    Dim pickData As New Dictionary(Of String, List(Of String))

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        MainMenu.Show()
        Me.Hide()
    End Sub


    Private Sub PickLocationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadLocations()
    End Sub


    Sub LoadLocations()

        Try

            Dim url As String = "http://192.168.1.122/stock-api/get_location.php"

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"
            req.Timeout = 5000
            req.AllowWriteStreamBuffering = True
            req.Expect = String.Empty

            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)

                Using reader As New StreamReader(resp.GetResponseStream())

                    Dim result As String = reader.ReadToEnd()

                    Dim lines() As String = result.Split(ControlChars.Lf)

                    StoreLocCBox.Items.Clear()
                    PickRefCBox.Items.Clear()
                    pickData.Clear()

                    For Each line As String In lines

                        line = line.Trim()

                        If line <> "" Then

                            Dim parts() As String = line.Split("|"c)

                            If parts.Length >= 2 Then

                                Dim store As String = parts(0).Trim()
                                Dim pickno As String = parts(1).Trim()

                                If Not pickData.ContainsKey(store) Then
                                    pickData(store) = New List(Of String)
                                    StoreLocCBox.Items.Add(store)
                                End If

                                pickData(store).Add(pickno)

                            End If

                        End If

                    Next

                End Using

            End Using

        Catch ex As Exception

            MessageBox.Show("Server Offline")

        End Try

    End Sub


    Private Sub StoreLocCBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles StoreLocCBox.SelectedIndexChanged

        PickRefCBox.Items.Clear()
        ProceedToPickBtn.Enabled = False
        Dim selectedStore As String = StoreLocCBox.SelectedItem.ToString()

        If pickData.ContainsKey(selectedStore) Then

            For Each pickno As String In pickData(selectedStore)
                PickRefCBox.Items.Add(pickno)
                ProceedToPickBtn.Enabled = True

            Next

        End If

    End Sub

    Private Sub PickRefCBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickRefCBox.SelectedIndexChanged

    End Sub

    Private Sub ProceedToPickBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProceedToPickBtn.Click

        If StoreLocCBox.SelectedItem Is Nothing OrElse PickRefCBox.SelectedItem Is Nothing Then
            MessageBox.Show("Please select Store and Pick Reference")
            Exit Sub
        End If

        ' 👉 PASS DATA
       
        Dim frm As New PickMainForm
        frm.StoreLocation = StoreLocCBox.SelectedItem.ToString
        frm.pick_no = PickRefCBox.SelectedItem.ToString

        frm.Show()
        Me.Hide()

    End Sub
End Class