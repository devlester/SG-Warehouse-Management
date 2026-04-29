Imports System.Net
Imports System.IO

''' <summary>
''' Downloads a pre-formatted HTML pick-list report from the server,
''' saves it to a temp file, and opens it in the system's default browser.
''' Works on both Windows CE (Compact Framework 3.5) and desktop Windows.
''' To print: use the browser's File → Print (or Ctrl+P on desktop).
''' To save as PDF: choose "Print to PDF" from the browser's print dialog.
''' </summary>
Module PrintPickList

    Public Sub PrintReport(ByVal pickNo As String, ByVal storeLocation As String)
        Try
            Dim url As String = "http://192.168.1.122/stock-api/get_print_data.php" &
                                "?pick_no=" & Uri.EscapeDataString(pickNo) &
                                "&store_location=" & Uri.EscapeDataString(storeLocation) &
                                "&format=html"

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method  = "GET"
            req.Timeout = 15000

            Dim html As String
            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(resp.GetResponseStream())
                    html = reader.ReadToEnd().Trim()
                End Using
            End Using

            If String.IsNullOrEmpty(html) OrElse html.StartsWith("{") Then
                MessageBox.Show("Could not load print data from server.")
                Exit Sub
            End If

            ' Save to temp file and open in browser
            Dim tempPath As String = Path.Combine(Path.GetTempPath(), "picklist_" & pickNo & ".html")
            File.WriteAllText(tempPath, html)

            System.Diagnostics.Process.Start(tempPath)

        Catch ex As WebException
            MessageBox.Show("Network error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message)
        End Try
    End Sub

End Module
