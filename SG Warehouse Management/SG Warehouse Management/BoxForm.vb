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
                    Dim lines() As String = result.Split(ControlChars.Lf)

                    ListView1.Items.Clear()

                    If result.Trim() = "NO_DATA" Then
                        MessageBox.Show("No items found")
                        Exit Sub
                    End If

                    For Each line As String In lines

                        line = line.Trim()
                        MsgBox(line & "-")
                        If line <> "" Then

                            Dim parts() As String = line.Split("|"c)

                            If parts.Length >= 5 Then

                                Dim box As String = parts(0)
                                Dim style As String = parts(2)
                                Dim name As String = parts(3)
                                Dim qty As String = parts(4)

                                Dim item As New ListViewItem(parts(0))

                                item.SubItems.Add(parts(2))
                                item.SubItems.Add(parts(3))
                                item.SubItems.Add(parts(4))


                                ' 👉 OPTIONAL: highlight low qty
                                If Val(qty) <= 1 Then
                                    item.BackColor = Color.LightPink
                                End If

                                ListView1.Items.Add(item)

                            End If

                        End If

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