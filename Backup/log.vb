Imports System.Runtime.InteropServices
Imports System.Text
''' <summary>
''' Create a New INI file to store or load data
''' </summary>
''' <remarks></remarks>
Public Class LogFile

    Public path As String

    <DllImport("kernel32.dll")> Public Shared Function WritePrivateProfileString(ByVal section As String, ByVal key As String, ByVal val As String, ByVal filePath As String) As Long
    End Function
    <DllImport("kernel32.dll")> Public Shared Function GetPrivateProfileString(ByVal section As String, ByVal key As String, ByVal def As String, ByVal retVal As StringBuilder, ByVal size As Integer, ByVal filePath As String) As Integer
    End Function

    ''' <summary>
    ''' INIFile Constructor.
    ''' </summary>
    ''' <param name="INIPath"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal INIPath As String)
        path = INIPath
    End Sub

    ''' <summary>
    ''' Write Data to the INI File
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="Key"></param>
    ''' <param name="Value"></param>
    ''' <remarks></remarks>
    Public Sub WriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
        WritePrivateProfileString(Section, Key, Value, Me.path)
    End Sub

    ''' <summary>
    ''' Read Data Value From the Ini File
    ''' </summary>
    ''' <param name="Section"></param>
    ''' <param name="Key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadValue(ByVal Section As String, ByVal Key As String)
        Dim temp As StringBuilder = New StringBuilder(255)
        Dim i As Integer = GetPrivateProfileString(Section, Key, "", temp, 255, Me.path)
        Return temp.ToString()
    End Function

    Protected Overrides Sub Finalize()
    End Sub

End Class
