Imports System.Text.RegularExpressions

Module Functions

    ' ─────────────────────────────────────────────────────────────
    ' JSON HELPERS
    ' Works with .NET 3.5 — no extra assembly references required.
    ' Handles the simple JSON shapes our own PHP endpoints produce:
    '   flat objects, arrays of strings, arrays of flat objects.
    ' ─────────────────────────────────────────────────────────────

    ''' <summary>
    ''' Read one value (string or number) from a JSON object by key.
    ''' Returns "" when the key is absent or the value is null.
    ''' </summary>
    Public Function JsonGetStr(ByVal json As String, ByVal key As String) As String
        If String.IsNullOrEmpty(json) Then Return ""
        ' "key":"value"
        Dim m As Match = Regex.Match(json, """" & key & """\s*:\s*""([^""]*)""")
        If m.Success Then Return m.Groups(1).Value
        ' "key":number  (integer or decimal, optionally negative)
        m = Regex.Match(json, """" & key & """\s*:\s*(-?\d+(?:\.\d+)?)")
        If m.Success Then Return m.Groups(1).Value
        Return ""
    End Function

    ''' <summary>
    ''' Read a JSON array of strings: {"key":["a","b","c"]}
    ''' </summary>
    Public Function JsonGetStringList(ByVal json As String, ByVal key As String) As List(Of String)
        Dim result As New List(Of String)
        Dim arr As String = ExtractJsonArray(json, key)
        For Each m As Match In Regex.Matches(arr, """([^""]*)""")
            result.Add(m.Groups(1).Value)
        Next
        Return result
    End Function

    ''' <summary>
    ''' Read a JSON array of flat objects: {"key":[{"a":"1"},{"a":"2"}]}
    ''' Returns a List of Dictionary(Of String, String).
    ''' </summary>
    Public Function JsonGetObjectList(ByVal json As String, ByVal key As String) As List(Of Dictionary(Of String, String))
        Dim result As New List(Of Dictionary(Of String, String))
        Dim arr As String = ExtractJsonArray(json, key)
        Dim depth As Integer = 0
        Dim start As Integer = -1
        For i As Integer = 0 To arr.Length - 1
            Dim ch As Char = arr(i)
            If ch = "{"c Then
                If depth = 0 Then start = i
                depth += 1
            ElseIf ch = "}"c Then
                depth -= 1
                If depth = 0 AndAlso start >= 0 Then
                    result.Add(JsonParseFlat(arr.Substring(start, i - start + 1)))
                    start = -1
                End If
            End If
        Next
        Return result
    End Function

    ''' <summary>
    ''' Parse a flat JSON object into Dictionary(Of String, String).
    ''' Keys must be word characters (\w+). Nested objects/arrays are ignored.
    ''' </summary>
    Public Function JsonParseFlat(ByVal json As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        ' String values first
        For Each m As Match In Regex.Matches(json, """(\w+)""\s*:\s*""([^""]*)""")
            result(m.Groups(1).Value) = m.Groups(2).Value
        Next
        ' Numeric values (only if key not already captured as string)
        For Each m As Match In Regex.Matches(json, """(\w+)""\s*:\s*(-?\d+(?:\.\d+)?)")
            If Not result.ContainsKey(m.Groups(1).Value) Then
                result(m.Groups(1).Value) = m.Groups(2).Value
            End If
        Next
        Return result
    End Function

    ' ── private ──────────────────────────────────────────────────

    Private Function ExtractJsonArray(ByVal json As String, ByVal key As String) As String
        Dim keyIdx As Integer = json.IndexOf("""" & key & """")
        If keyIdx < 0 Then Return "[]"
        Dim arrStart As Integer = json.IndexOf("["c, keyIdx)
        If arrStart < 0 Then Return "[]"
        Dim depth As Integer = 0
        For i As Integer = arrStart To json.Length - 1
            If json(i) = "["c Then depth += 1
            If json(i) = "]"c Then
                depth -= 1
                If depth = 0 Then Return json.Substring(arrStart, i - arrStart + 1)
            End If
        Next
        Return "[]"
    End Function

End Module
