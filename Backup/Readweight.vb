Public Class Readweight

    Public Function readweight(ByVal Wtype As Integer, ByVal tempweightdata As String) As Double
        Dim read_result As Double = 0
        Dim wdata As String()
        Dim wdata1 As String '()
        If tempweightdata <> Nothing Then

            If Wtype = 1 Then
                Try
                    wdata = tempweightdata.Split(vbNewLine)

                    For H As Integer = 0 To wdata.Length - 1
                        If wdata(H).Contains("G") Then
                            wdata1 = wdata(H + 1).Replace("+", "")
                            wdata1 = wdata1.Replace("kg", "")
                            read_result = CDbl(formatinput(wdata1)) * 1000
                            Exit For
                        End If
                    Next
                    'If wdata.Length > 9 Then
                    '    If wdata(9).Contains("-") Then
                    '        read_result = "0"
                    '    Else
                    '        wdata1 = wdata(9).Replace("+", "")
                    '        wdata1 = wdata1.Replace("kg", "")
                    '        read_result = CDbl(formatinput(wdata1)) * 1000
                    '    End If
                    'Else
                    '    read_result = CDbl(formatinput(tempweightdata)) * 1000
                    'End If
                Catch ex As Exception
                    read_result = Nothing
                End Try

            ElseIf Wtype = 2 Then

                If tempweightdata.Contains("ST") = True And tempweightdata.Contains("GS") = True Then
                    Dim Value As String = tempweightdata.Replace("ST", "")
                    Value = Value.Replace("GS", "")
                    Value = Value.Replace(",", "")
                    Value = Value.Replace(" ", "")
                    Value = Value.Replace("KG", "")

                    read_result = Value
                ElseIf tempweightdata.Contains("+") Then
                    read_result = CDbl(formatinput(tempweightdata))
                Else
                    read_result = Nothing
                End If

            ElseIf Wtype = 3 Then
                read_result = tempweightdata
            End If

            tempweightdata = Nothing
        End If

        Return read_result
    End Function

    Private Function formatinput(ByVal stringint As String) As String
        Dim lngCount As Long
        Dim strOut As String = Nothing
        If Len(stringint) > 0 Then
            For lngCount = 1 To Len(stringint)
                If IsNumeric(Mid$(stringint, lngCount, 1)) Or Mid$(stringint, lngCount, 1) = "." Then
                    strOut = strOut & Mid$(stringint, lngCount, 1)
                End If
            Next lngCount
        End If

        Return strOut

    End Function

End Class
