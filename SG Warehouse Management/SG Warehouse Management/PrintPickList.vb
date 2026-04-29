Imports System.Drawing.Printing
Imports System.Net
Imports System.IO

''' <summary>
''' Fetches pick-list data from the API and sends a formatted report
''' (pick items + discrepancies + box breakdown) directly to the system printer.
''' The report is landscape A4/Letter.  To save as PDF, choose
''' "Microsoft Print to PDF" in the print dialog.
''' </summary>
Module PrintPickList

    ' ── report data ──────────────────────────────────────────
    Private _store        As String
    Private _pickNo       As String
    Private _printedAt    As String
    Private _totalToPick  As Integer
    Private _totalPicked  As Integer
    Private _totalBoxed   As Integer
    Private _items        As List(Of Dictionary(Of String, String))
    Private _boxes        As List(Of Dictionary(Of String, String))

    ' ── pagination state (reset on each Print call) ───────────
    Private _phase   As Integer  ' 0=draw item header, 1=draw items, 2=draw box header, 3=draw boxes
    Private _itemIdx As Integer
    Private _boxIdx  As Integer
    Private _pageNum As Integer

    Private Const ROW_H As Single = 22
    Private Const HDR_H As Single = 22

    ' ─────────────────────────────────────────────────────────
    Public Sub PrintReport(ByVal pickNo As String, ByVal storeLocation As String)
        Try
            Dim url As String = "http://192.168.1.122/stock-api/get_print_data.php?pick_no=" &
                                Uri.EscapeDataString(pickNo) &
                                "&store_location=" & Uri.EscapeDataString(storeLocation)

            Dim req As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            req.Method  = "GET"
            req.Timeout = 15000

            Dim json As String
            Using resp As HttpWebResponse = CType(req.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(resp.GetResponseStream())
                    json = reader.ReadToEnd().Trim()
                End Using
            End Using

            If JsonGetStr(json, "status") <> "ok" Then
                MessageBox.Show("Could not load print data: " & JsonGetStr(json, "message"))
                Exit Sub
            End If

            _store       = JsonGetStr(json, "store")
            _pickNo      = JsonGetStr(json, "pick_no")
            _printedAt   = JsonGetStr(json, "printed_at")
            _totalToPick = CInt(Val(JsonGetStr(json, "total_to_pick")))
            _totalPicked = CInt(Val(JsonGetStr(json, "total_picked")))
            _totalBoxed  = CInt(Val(JsonGetStr(json, "total_boxed")))
            _items       = JsonGetObjectList(json, "items")
            _boxes       = JsonGetObjectList(json, "boxes")

            ' reset pagination
            _phase   = 0
            _itemIdx = 0
            _boxIdx  = 0
            _pageNum = 1

            Dim doc As New PrintDocument()
            doc.DocumentName                    = "PickList_" & _pickNo
            doc.DefaultPageSettings.Landscape   = True

            AddHandler doc.PrintPage, AddressOf OnPrintPage

            Dim dlg As New PrintDialog()
            dlg.Document = doc

            If dlg.ShowDialog() = DialogResult.OK Then
                doc.Print()
            End If

        Catch ex As WebException
            MessageBox.Show("Network error: " & ex.Message)
        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message)
        End Try
    End Sub

    ' ─────────────────────────────────────────────────────────
    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim g  As Graphics   = e.Graphics
        Dim mb As RectangleF = e.MarginBounds
        Dim x  As Single     = mb.Left
        Dim y  As Single     = mb.Top
        Dim w  As Single     = mb.Width

        ' ── resource allocation ───────────────────────────────
        Dim fTitle   As New Font("Arial", 15, FontStyle.Bold)
        Dim fSub     As New Font("Arial", 10, FontStyle.Bold)
        Dim fBold    As New Font("Arial", 9,  FontStyle.Bold)
        Dim fNormal  As New Font("Arial", 9)
        Dim fSmall   As New Font("Arial", 8)
        Dim fRedBold As New Font("Arial", 9,  FontStyle.Bold)

        Dim bNavy    As New SolidBrush(Color.FromArgb(21, 71, 132))
        Dim bGreen   As New SolidBrush(Color.FromArgb(34, 110, 55))
        Dim bGray    As New SolidBrush(Color.FromArgb(80, 80, 80))
        Dim bSilver  As New SolidBrush(Color.FromArgb(230, 235, 245))
        Dim bAlt     As New SolidBrush(Color.FromArgb(242, 246, 255))
        Dim bDisc    As New SolidBrush(Color.FromArgb(255, 210, 210))
        Dim bWhite   As New SolidBrush(Color.White)
        Dim bBlack   As New SolidBrush(Color.Black)
        Dim bRed     As New SolidBrush(Color.DarkRed)
        Dim pGrid    As New Pen(Color.FromArgb(200, 200, 200), 0.5)
        Dim pDark    As New Pen(Color.FromArgb(100, 100, 100), 1)

        Dim needsMore As Boolean = False

        Try
            ' ── page banner (every page) ─────────────────────
            g.FillRectangle(bNavy, x, y, w, 36)
            g.DrawString("PICK LIST REPORT", fTitle, bWhite, x + 10, y + 7)
            Dim pgStr As String = "Pick #: " & _pickNo & "     Page " & _pageNum
            Dim pgSz  As SizeF  = g.MeasureString(pgStr, fBold)
            g.DrawString(pgStr, fBold, bWhite, x + w - pgSz.Width - 10, y + 11)
            y += 42

            ' ── summary strip (page 1 only) ───────────────────
            If _pageNum = 1 Then
                g.DrawString("Store: " & _store, fSub, bBlack, x, y)
                Dim dtSz As SizeF = g.MeasureString("Printed: " & _printedAt, fSmall)
                g.DrawString("Printed: " & _printedAt, fSmall, bGray, x + w - dtSz.Width, y + 3)
                y += 22

                g.FillRectangle(bSilver, x, y, w, 26)
                g.DrawRectangle(pDark, x, y, w, 26)
                Dim col As Single = w / 3
                g.DrawString("Total to Pick:  " & _totalToPick, fBold, bBlack, x + col * 0 + 10, y + 6)
                g.DrawString("Total Picked:   " & _totalPicked, fBold, bBlack, x + col * 1 + 10, y + 6)
                g.DrawString("Total Boxed:    " & _totalBoxed,  fBold, bBlack, x + col * 2 + 10, y + 6)

                ' discrepancy callout
                Dim missing As Integer = _totalToPick - _totalPicked
                If missing > 0 Then
                    Dim warnStr As String = "  ⚠ " & missing & " unit(s) not yet picked"
                    g.DrawString(warnStr, fBold, bRed, x + col * 2 + 10, y + 6 + 14)
                End If
                y += 34
            Else
                g.DrawString("Store: " & _store, fSmall, bGray, x, y)
                y += 14
            End If

            ' ── section: PICK ITEMS ───────────────────────────
            If _phase = 0 Then
                y += 4
                g.DrawString("PICK ITEMS", fSub, bBlack, x, y)
                y += 16
                y = DrawItemHeader(g, x, y, w, bGray, bWhite, fBold, HDR_H, pGrid)
                _phase = 1
            End If

            If _phase = 1 Then
                While _itemIdx < _items.Count
                    If y + ROW_H > mb.Bottom Then
                        needsMore = True
                        Exit While
                    End If
                    Dim itm  As Dictionary(Of String, String) = _items(_itemIdx)
                    Dim disc As Integer = CInt(Val(itm("discrepancy")))
                    Dim rowBg As SolidBrush = If(disc > 0, bDisc, If(_itemIdx Mod 2 = 0, bAlt, bWhite))
                    DrawItemRow(g, x, y, w, itm, rowBg, bBlack, bRed, fNormal, fRedBold, pGrid, ROW_H)
                    y       += ROW_H
                    _itemIdx += 1
                End While
                If Not needsMore Then _phase = 2
            End If

            ' ── section: BOX BREAKDOWN ────────────────────────
            If Not needsMore AndAlso _phase = 2 Then
                y += 10
                If y + HDR_H + ROW_H > mb.Bottom Then
                    needsMore = True
                Else
                    g.DrawString("BOX BREAKDOWN", fSub, bBlack, x, y)
                    y += 16
                    y = DrawBoxHeader(g, x, y, w, bGreen, bWhite, fBold, HDR_H, pGrid)
                    _phase = 3
                End If
            End If

            If Not needsMore AndAlso _phase = 3 Then
                While _boxIdx < _boxes.Count
                    If y + ROW_H > mb.Bottom Then
                        needsMore = True
                        Exit While
                    End If
                    Dim box   As Dictionary(Of String, String) = _boxes(_boxIdx)
                    Dim rowBg As SolidBrush = If(_boxIdx Mod 2 = 0, bAlt, bWhite)
                    DrawBoxRow(g, x, y, w, box, rowBg, bBlack, fNormal, pGrid, ROW_H)
                    y      += ROW_H
                    _boxIdx += 1
                End While
            End If

            DrawFooter(g, mb, fSmall, bGray, pDark)

            If needsMore Then
                _pageNum     += 1
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If

        Finally
            ' always dispose GDI resources
            fTitle.Dispose()  : fSub.Dispose()    : fBold.Dispose()
            fNormal.Dispose() : fSmall.Dispose()  : fRedBold.Dispose()
            bNavy.Dispose()   : bGreen.Dispose()  : bGray.Dispose()
            bSilver.Dispose() : bAlt.Dispose()    : bDisc.Dispose()
            bWhite.Dispose()  : bBlack.Dispose()  : bRed.Dispose()
            pGrid.Dispose()   : pDark.Dispose()
        End Try
    End Sub

    ' ─────────────────────────────────────────────────────────
    ' PICK ITEMS TABLE
    ' Columns: Location | Style | Product Name | To Pick | Picked | Boxed | Discrepancy
    ' ─────────────────────────────────────────────────────────
    Private Function GetItemCols(ByVal x As Single, ByVal w As Single) As Single()
        Return New Single() {
            x,
            x + w * 0.09F,
            x + w * 0.22F,
            x + w * 0.60F,
            x + w * 0.69F,
            x + w * 0.78F,
            x + w * 0.87F
        }
    End Function

    Private Function DrawItemHeader(ByVal g As Graphics, ByVal x As Single, ByVal y As Single,
                                    ByVal w As Single, ByVal bg As SolidBrush, ByVal fg As SolidBrush,
                                    ByVal f As Font, ByVal h As Single, ByVal pen As Pen) As Single
        g.FillRectangle(bg, x, y, w, h)
        Dim c() As Single = GetItemCols(x, w)
        g.DrawString("Location",     f, fg, c(0)+3, y+4)
        g.DrawString("Style",        f, fg, c(1)+3, y+4)
        g.DrawString("Product Name", f, fg, c(2)+3, y+4)
        g.DrawString("To Pick",      f, fg, c(3)+3, y+4)
        g.DrawString("Picked",       f, fg, c(4)+3, y+4)
        g.DrawString("Boxed",        f, fg, c(5)+3, y+4)
        g.DrawString("Discrepancy",  f, fg, c(6)+3, y+4)
        g.DrawRectangle(New Pen(Color.DimGray, 0.8), x, y, w, h)
        Return y + h
    End Function

    Private Sub DrawItemRow(ByVal g As Graphics, ByVal x As Single, ByVal y As Single,
                            ByVal w As Single, ByVal item As Dictionary(Of String, String),
                            ByVal bg As SolidBrush, ByVal fg As SolidBrush, ByVal fgRed As SolidBrush,
                            ByVal fNorm As Font, ByVal fRed As Font, ByVal pen As Pen, ByVal h As Single)
        g.FillRectangle(bg, x, y, w, h)
        Dim c() As Single = GetItemCols(x, w)
        g.DrawString(item("location"),          fNorm, fg,    c(0)+3, y+4)
        g.DrawString(item("style"),             fNorm, fg,    c(1)+3, y+4)
        g.DrawString(Trunc(item("name"), 38),   fNorm, fg,    c(2)+3, y+4)
        g.DrawString(item("qty_to_pick"),       fNorm, fg,    c(3)+3, y+4)
        g.DrawString(item("qty_picked"),        fNorm, fg,    c(4)+3, y+4)
        g.DrawString(item("qty_boxed"),         fNorm, fg,    c(5)+3, y+4)
        Dim disc As String = item("discrepancy")
        If Val(disc) > 0 Then
            g.DrawString(disc, fRed, fgRed, c(6)+3, y+4)
        Else
            g.DrawString(disc, fNorm, fg,   c(6)+3, y+4)
        End If
        g.DrawLine(pen, x, y + h, x + w, y + h)
    End Sub

    ' ─────────────────────────────────────────────────────────
    ' BOX BREAKDOWN TABLE
    ' Columns: Box # | Style | Product Name | Qty
    ' ─────────────────────────────────────────────────────────
    Private Function GetBoxCols(ByVal x As Single, ByVal w As Single) As Single()
        Return New Single() {x, x + w * 0.10F, x + w * 0.25F, x + w * 0.88F}
    End Function

    Private Function DrawBoxHeader(ByVal g As Graphics, ByVal x As Single, ByVal y As Single,
                                   ByVal w As Single, ByVal bg As SolidBrush, ByVal fg As SolidBrush,
                                   ByVal f As Font, ByVal h As Single, ByVal pen As Pen) As Single
        g.FillRectangle(bg, x, y, w, h)
        Dim c() As Single = GetBoxCols(x, w)
        g.DrawString("Box #",        f, fg, c(0)+3, y+4)
        g.DrawString("Style",        f, fg, c(1)+3, y+4)
        g.DrawString("Product Name", f, fg, c(2)+3, y+4)
        g.DrawString("Qty",          f, fg, c(3)+3, y+4)
        g.DrawRectangle(New Pen(Color.DimGray, 0.8), x, y, w, h)
        Return y + h
    End Function

    Private Sub DrawBoxRow(ByVal g As Graphics, ByVal x As Single, ByVal y As Single,
                           ByVal w As Single, ByVal box As Dictionary(Of String, String),
                           ByVal bg As SolidBrush, ByVal fg As SolidBrush,
                           ByVal f As Font, ByVal pen As Pen, ByVal h As Single)
        g.FillRectangle(bg, x, y, w, h)
        Dim c() As Single = GetBoxCols(x, w)
        g.DrawString("Box #" & box("box_no"),  f, fg, c(0)+3, y+4)
        g.DrawString(box("style"),             f, fg, c(1)+3, y+4)
        g.DrawString(Trunc(box("name"), 52),   f, fg, c(2)+3, y+4)
        g.DrawString(box("qty"),               f, fg, c(3)+3, y+4)
        g.DrawLine(pen, x, y + h, x + w, y + h)
    End Sub

    ' ─────────────────────────────────────────────────────────
    Private Sub DrawFooter(ByVal g As Graphics, ByVal mb As RectangleF,
                           ByVal f As Font, ByVal fg As SolidBrush, ByVal pen As Pen)
        Dim fy As Single = mb.Bottom + 6
        g.DrawLine(pen, mb.Left, fy, mb.Right, fy)
        g.DrawString("SG Warehouse Management System", f, fg, mb.Left, fy + 4)
        Dim right As String = "Pick #: " & _pickNo & "     Page " & _pageNum
        Dim sz    As SizeF  = g.MeasureString(right, f)
        g.DrawString(right, f, fg, mb.Right - sz.Width, fy + 4)
    End Sub

    Private Function Trunc(ByVal s As String, ByVal maxLen As Integer) As String
        If String.IsNullOrEmpty(s) Then Return ""
        If s.Length <= maxLen Then Return s
        Return s.Substring(0, maxLen - 1) & "~"
    End Function

End Module
