Imports System.IO
Imports System.IO.Ports

Public Class SetupMasterFrm
    Dim SPort As New SerialPort
    Dim scanState As Integer = -1
    Dim ini As New IniFile(GlobalInfo.SettingFileLoc)
    Dim arrTextbox As New ArrayList
    Dim maxPartNo As Integer = 50
    Dim Part As String = "PART_"
    Dim TOTAL_PART As String = "TOTAL_PART"
    Dim Model As String = "MODEL"
    Dim Version As String = "VERSION"
    Dim CPart As String = "CPart"
    Dim Child_PN_SN As String = "Child_PN_SN"
    Private Sub SetupMasterFrm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ClosePort()
    End Sub

    Private Sub SetupMasterFrm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For i As Integer = 0 To maxPartNo
            arrTextbox.Add(New TextBox)
        Next
        assignTextBoxToArray()

        AddHandler SPort.DataReceived, AddressOf DataReceviedHandler
        OpenPort()
    End Sub

    Private Sub assignTextBoxToArray()
        arrTextbox(0) = txtPcode
        arrTextbox(1) = txt1
        arrTextbox(2) = txt2
        arrTextbox(3) = txt3
        arrTextbox(4) = txt4
        arrTextbox(5) = txt5
        arrTextbox(6) = txt6
        arrTextbox(7) = txt7
        arrTextbox(8) = txt8
        arrTextbox(9) = txt9
        arrTextbox(10) = txt10
        arrTextbox(11) = txt11
        arrTextbox(12) = txt12
        arrTextbox(13) = txt13
        arrTextbox(14) = txt14
        arrTextbox(15) = txt15
        arrTextbox(16) = txt16
        arrTextbox(17) = txt17
        arrTextbox(18) = txt18
        arrTextbox(19) = txt19
        arrTextbox(20) = txt20

    End Sub

    Dim ScaninValue As String = Nothing
    Dim rLine As String
    Private Sub DataReceviedHandler(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        System.Threading.Thread.Sleep(50)
        Me.Invoke(New EventHandler(AddressOf DoUpdate))
    End Sub

    Private Sub DoUpdate(ByVal s As Object, ByVal e As EventArgs)
        rLine = SPort.ReadExisting

        If Not rLine = Nothing Then
            If rLine.Contains(Chr(13)) = False Then
                ScaninValue = ScaninValue & rLine
            Else
                ScaninValue = ScaninValue & rLine
                ScaninValue = ScaninValue.Replace(Chr(13), "")
                ScaninValue = ScaninValue.Replace(Chr(10), "")
                ' 20110614 replace "-' also
                ScaninValue = ScaninValue.Replace("-", "")
                ' fix to 9 chars
                If ScaninValue.Length >= 9 Then
                    ScaninValue = ScaninValue.Substring(0, 9)
                End If
                AssignScanIn(ScaninValue)
            End If
            ScaninValue = Nothing
        End If
    End Sub

    Private Sub AssignScanIn(ByVal value As String)
        'Select Case scanState
        '    Case 0 'Pcode
        '        txtPcode.Text = value
        '        txtPcode.BackColor = Color.Yellow
        '        LoadPartNoByPartCode(value)
        '        'part no 1 to 20
        '    Case 1
        '        txt1.Text = value
        '        txt1.BackColor = Color.Yellow
        '    Case 2
        '        txt2.Text = value
        '        txt2.BackColor = Color.Yellow
        '    Case 3
        '        txt3.Text = value
        '        txt3.BackColor = Color.Yellow
        '    Case 4
        '        txt4.Text = value
        '        txt4.BackColor = Color.Yellow
        '    Case 5
        '        txt5.Text = value
        '        txt5.BackColor = Color.Yellow
        '    Case 6
        '        txt6.Text = value
        '        txt6.BackColor = Color.Yellow
        '    Case 7
        '        txt7.Text = value
        '        txt7.BackColor = Color.Yellow
        '    Case 8
        '        txt8.Text = value
        '        txt8.BackColor = Color.Yellow
        '    Case 9
        '        txt9.Text = value
        '        txt9.BackColor = Color.Yellow
        '    Case 10
        '        txt10.Text = value
        '        txt10.BackColor = Color.Yellow
        '    Case 11
        '        txt11.Text = value
        '        txt11.BackColor = Color.Yellow
        '    Case 12
        '        txt12.Text = value
        '        txt12.BackColor = Color.Yellow
        '    Case 13
        '        txt13.Text = value
        '        txt13.BackColor = Color.Yellow
        '    Case 14
        '        txt14.Text = value
        '        txt14.BackColor = Color.Yellow
        '    Case 15
        '        txt15.Text = value
        '        txt15.BackColor = Color.Yellow
        '    Case 16
        '        txt16.Text = value
        '        txt16.BackColor = Color.Yellow
        '    Case 17
        '        txt17.Text = value
        '        txt17.BackColor = Color.Yellow
        '    Case 18
        '        txt18.Text = value
        '        txt18.BackColor = Color.Yellow
        '    Case 19
        '        txt19.Text = value
        '        txt19.BackColor = Color.Yellow
        '    Case 20
        '        txt20.Text = value
        '        txt20.BackColor = Color.Yellow
        'End Select

        StatusLbl.Text = ""

        scanState += 1
        If scanState > maxPartNo Then
            Dim dr As New DialogResult
            dr = MessageBox.Show("All fields are fill with data. Do you want to reset all data?", "Data Full", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If dr = Windows.Forms.DialogResult.Yes Then
                ClearTxtBox()
                scanState = 0
            Else
                Exit Sub
            End If
        End If

        Dim txtBox As TextBox
        If scanState = 0 Then 'scan pcode
            If value.Length < 7 Then
                StatusLbl.Text = "Pcode format incorrect"
                Exit Sub
            End If
            txtBox = arrTextbox(0)
            txtBox.Text = value
            txtBox.BackColor = Color.Yellow
        ElseIf scanState > 0 And scanState <= maxPartNo Then 'scan part no
            txtBox = arrTextbox(scanState)
            txtBox.Text = value
            txtBox.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub ClearTxtBox()
        'clear all text and reset color
        For Each txtbox As TextBox In arrTextbox
            txtbox.Text = ""
            txtbox.BackColor = Color.White
        Next

        scanState = -1

        txtModel.Text = ""
        txtVersion.Text = ""
        txtChildAccPN.Text = ""
        ''clear all text
        'txtPcode.Text = ""
        'txt1.Text = ""
        'txt2.Text = ""
        'txt3.Text = ""
        'txt4.Text = ""
        'txt5.Text = ""
        'txt6.Text = ""
        'txt7.Text = ""
        'txt8.Text = ""
        'txt9.Text = ""
        'txt10.Text = ""
        'txt11.Text = ""
        'txt12.Text = ""
        'txt13.Text = ""
        'txt14.Text = ""
        'txt15.Text = ""
        'txt16.Text = ""
        'txt17.Text = ""
        'txt18.Text = ""
        'txt19.Text = ""
        'txt20.Text = ""

        ''reset color
        'txtPcode.BackColor = Color.White
        'txt1.BackColor = Color.White
        'txt2.BackColor = Color.White
        'txt3.BackColor = Color.White
        'txt4.BackColor = Color.White
        'txt5.BackColor = Color.White
        'txt6.BackColor = Color.White
        'txt7.BackColor = Color.White
        'txt8.BackColor = Color.White
        'txt9.BackColor = Color.White
        'txt10.BackColor = Color.White
        'txt11.BackColor = Color.White
        'txt12.BackColor = Color.White
        'txt13.BackColor = Color.White
        'txt14.BackColor = Color.White
        'txt15.BackColor = Color.White
        'txt16.BackColor = Color.White
        'txt17.BackColor = Color.White
        'txt18.BackColor = Color.White
        'txt19.BackColor = Color.White
        'txt20.BackColor = Color.White
    End Sub

    Private Sub OpenPort()

        If (SPort.IsOpen = True) Then
            SPort.Close()
        End If

        SPort.PortName = ini.ReadValue(Section.Scanner2ComSetting, Key.ComName)
        SPort.BaudRate = CInt(ini.ReadValue(Section.Scanner2ComSetting, Key.Boundrate))
        SPort.DataBits = CInt(ini.ReadValue(Section.Scanner2ComSetting, Key.DataBits))
        Dim strParity = ini.ReadValue(Section.Scanner2ComSetting, Key.Parity)
        SPort.Parity = DirectCast([Enum].Parse(GetType(Parity), strParity), Parity)
        Dim strStopBits = ini.ReadValue(Section.Scanner2ComSetting, Key.StopBits)
        SPort.StopBits = DirectCast([Enum].Parse(GetType(StopBits), strStopBits), StopBits)
        Dim strHandshake = ini.ReadValue(Section.Scanner2ComSetting, Key.Handshake)
        SPort.Handshake = DirectCast([Enum].Parse(GetType(Handshake), strHandshake), Handshake)

        If (SPort.IsOpen = False) Then
            SPort.Open()
        End If
    End Sub

    Private Sub ClosePort()
        Try
            If (SPort.IsOpen = True) Then
                SPort.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearTxtBox()
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        StatusLbl.Text = ""
        Try
            If SaveInDatFile() = True Then

                StatusLbl.Text = "Save successful"
            Else
                StatusLbl.Text = "Save unsuccessful"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Function SaveInDatFile() As Boolean
        Err.Clear()
        Dim result As Boolean = True
        Dim Pcode As String = txtPcode.Text

        If Pcode = Nothing Then
            Throw New Exception("Pcode can not be blank")
            Exit Function
        End If

        If txtModel.Text = Nothing Then
            Throw New Exception("Model can not be blank")
            Exit Function
        End If

        If txtVersion.Text = Nothing Then
            Throw New Exception("Version can not be blank")
            Exit Function
        End If

        Dim pcode1 As String = Nothing
        Dim txtBox As TextBox
        Try

            'Get Dat file name
            If Pcode.Length > 6 Then
                pcode1 = (Pcode.Substring(1)).Substring(0, 6)
            Else
                Throw New Exception("Pcode format incorrect")
                'Return False
            End If

            'check existing of file
            'Dim filename As String = Application.StartupPath & "\pcode\" & pcode1 & "00.dat"
            Dim filepath As String = ini.ReadValue(Section.Commonsettings, Key.datafolder)
            Dim filename As String = filepath & "\pcode\" & pcode1 & "00.dat"

            Dim pcodeIni As New IniFile(filename)
            'save model & version
            pcodeIni.WriteValue(Pcode, Model, txtModel.Text)
            pcodeIni.WriteValue(Pcode, Version, txtVersion.Text)
            pcodeIni.WriteValue(Pcode, CPart, txtChildPartNo.Text)
            pcodeIni.WriteValue(Pcode, Child_PN_SN, txtChildAccPN.Text)
            'count no of part no
            pcodeIni.WriteValue(Pcode, TOTAL_PART, scanState.ToString)
            Dim i, j As Integer
            For i = 1 To scanState
                txtBox = arrTextbox(i)
                pcodeIni.WriteValue(Pcode, Part & i, txtBox.Text)
            Next

            For j = i To maxPartNo
                pcodeIni.WriteValue(Pcode, Part & j, Nothing)
            Next


            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "LoadPartNoByPartCode")
            Return False
        End Try

        'Return result
    End Function

    'Private Function LoadPartNoByPartCode(ByVal Pcode As String) As Boolean

    '    Dim result As Boolean = False
    '    If Pcode = Nothing Then Exit Function
    '    Dim pcode1 As String = Nothing
    '    Dim txtBox As TextBox
    '    Try

    '        'remove first and last char
    '        '   Pcode = (Pcode.Substring(1)).Substring(0, Pcode.Length - 2)

    '        'Get Dat file name
    '        If Pcode.Length > 6 Then
    '            pcode1 = (Pcode.Substring(1)).Substring(0, 6)
    '        Else
    '            Return False
    '        End If

    '        'check existing of file
    '        Dim filename As String = Application.StartupPath & "\pcode\" & pcode1 & "00.dat"
    '        If My.Computer.FileSystem.FileExists(filename) = False Then
    '            File.Create(filename)
    '        Else
    '            Dim pcodeIni As New IniFile(filename)
    '            Dim qtyPartNo As Integer = Convert.ToInt32(pcodeIni.ReadValue(Pcode, TOTAL_PART))
    '            For i As Integer = 1 To qtyPartNo
    '                txtBox = arrTextbox(i)
    '                txtBox.Text = pcodeIni.ReadValue(Pcode, Part & i).ToString()
    '                'txtBox.BackColor = Color.Yellow
    '            Next
    '        End If

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "LoadPartNoByPartCode")
    '    End Try

    '    Return result
    'End Function
End Class