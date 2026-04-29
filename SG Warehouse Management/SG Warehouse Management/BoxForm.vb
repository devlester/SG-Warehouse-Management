Imports System.Net
Imports System.IO
Imports System.Windows.Forms




Public Class BoxForm

    Public PickNo As String = ""
    Public BoxNo As String = ""

    Private Sub BoxForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        ' 👉 Setup ListView
        With ListView1
            .View = View.Details
            .FullRowSelect = True



          
        End With


        ' 👉 Validate
        If PickNo = "" Then
            MessageBox.Show("No Pick Number received")
            Exit Sub
        End If

        ' 👉 Load data
        LoadBoxItems(BoxNo)

    End Sub


    


    Sub LoadBoxItems(ByVal boxNo As String)

        Try


            Dim url As String

            ' 👉 IF NO BOX → LOAD ALL
            If boxNo = "" Then
                url = "http://192.168.1.122/stock-api/get_box_items.php?pick_no=" & PickNo
            Else
                url = "http://192.168.1.122/stock-api/get_box_items.php?pick_no=" & PickNo & "&box_no=" & boxNo
            End If

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method = "GET"

            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(resp.GetResponseStream())

                    Dim result As String = reader.ReadToEnd()

                    ListView1.Items.Clear()

                    Dim boxItems As List(Of Dictionary(Of String, String)) = JsonGetObjectList(result, "items")

                    If boxItems.Count = 0 Then
                        MessageBox.Show("No items found")
                        Exit Sub
                    End If

                    For Each row As Dictionary(Of String, String) In boxItems
                        Dim qty As String = row("qty")
                        Dim lvi As New ListViewItem(row("box_no"))
                        lvi.SubItems.Add(row("style"))
                        lvi.SubItems.Add(row("name"))
                        lvi.SubItems.Add(qty)

                        If Val(qty) <= 1 Then lvi.BackColor = Color.LightPink

                        ListView1.Items.Add(lvi)
                    Next

                    ' 👉 AUTO RESIZE COLUMNS
                    For Each col As ColumnHeader In ListView1.Columns
                        col.Width = -2
                    Next

                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message)
        End Try

    End Sub


    ' 👉 OPTIONAL: DOUBLE CLICK ROW
    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles ListView1.DoubleClick

        If ListView1.Items.Count > 0 Then

            Dim box As String = ListView1.Items(0).Text
            Dim style As String = ListView1.Items(0).SubItems(1).Text

            MessageBox.Show("Box: " & box & vbCrLf & "Style: " & style)

        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class