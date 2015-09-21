Imports System.IO
Imports System.IO.Ports
Public Class BarcodeScannerFrm
    Dim ini As New IniFile(GlobalInfo.SettingFileLoc)

    Private Sub BarcodeScannerFrm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        LoadSerialPortProperties()
        cb_scannertype.SelectedIndex = 0
        LoadSaveValues(0)
        GroupBox1.Visible = False

    End Sub

    Private Sub LoadSerialPortProperties()
        Dim portName As String
        cboComName.Items.Clear()
        For Each comName As String In SerialPort.GetPortNames
            Dim portNumberChars As Char() = comName.Substring(3).ToCharArray()
            portName = "COM"
            For Each portNumberChar As Char In portNumberChars
                If (Char.IsDigit(portNumberChar)) Then
                    portName += portNumberChar.ToString()
                End If
            Next
            cboComName.Items.Add(portName)
        Next

        cboParity.Items.Clear()
        For Each parity As String In [Enum].GetNames(GetType(Parity))
            cboParity.Items.Add(parity)
        Next

        cboStopbits.Items.Clear()
        For Each stopbits As String In [Enum].GetNames(GetType(StopBits))
            cboStopbits.Items.Add(stopbits)
        Next

        cboHandshake.Items.Clear()
        For Each handshake As String In [Enum].GetNames(GetType(Handshake))
            cboHandshake.Items.Add(handshake)
        Next
    End Sub

    Private Sub LoadSaveValues(ByVal selectedindex As Integer)

        Try

            tb_datafolder.Text = ini.ReadValue(Section.Commonsettings, Key.datafolder)
            tb_lineno.Text = ini.ReadValue(Section.Commonsettings, Key.lineno)
            childmode = ini.ReadValue(Section.Commonsettings, Key.childmode1)

            If Not childmode = "" Then
                If childmode = "Serial" Then
                    rb_childserial.Checked = True
                ElseIf childmode = "Partcode" Then
                    rb_childpartcode.Checked = True
                ElseIf childmode = "SkipPartNo" Then
                    rb_ChildSkipPN.Checked = True
                ElseIf childmode = "Serial_No_Prefix" Then 'beryls 20110816
                    rb_ChildNoPrefix.Checked = True
                ElseIf childmode = "Child_PN_SN" Then
                    rb_ChildPN_SN.Checked = True
                ElseIf childmode = "Child_PN_SN_NP" Then
                    rbPN_SN_NP.Checked = True
                End If
            Else
                rb_childserial.Checked = True
            End If


            If selectedindex = 0 Then
                'this is for scanner to scan out and accessories scanning
                cboComName.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.ComName)
                cboBoundrate.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.Boundrate)
                cboBitrate.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.DataBits)
                cboParity.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.Parity)
                cboStopbits.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.StopBits)
                cboHandshake.SelectedItem = ini.ReadValue(Section.Scanner1ComSetting, Key.Handshake)
                cb_enabled.Checked = ini.ReadValue(Section.Scanner1ComSetting, Key.Disabled)

            ElseIf selectedindex = 1 Then
                ' setting for accessories weight setting
                cboComName.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.ComName)
                cboBoundrate.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.Boundrate)
                cboBitrate.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.DataBits)
                cboParity.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.Parity)
                cboStopbits.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.StopBits)
                cboHandshake.SelectedItem = ini.ReadValue(Section.Scanner2ComSetting, Key.Handshake)
                cb_enabled.Checked = ini.ReadValue(Section.Scanner2ComSetting, Key.Disabled)

            ElseIf selectedindex = 2 Then
                ' setting for accessories set weight setting
                cboComName.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.ComName)
                cboBoundrate.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.Boundrate)
                cboBitrate.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.DataBits)
                cboParity.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.Parity)
                cboStopbits.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.StopBits)
                cboHandshake.SelectedItem = ini.ReadValue(Section.Scanner3ComSetting, Key.Handshake)
                cb_enabled.Checked = ini.ReadValue(Section.Scanner3ComSetting, Key.Disabled)
                Me.tb_lowlimit.Text = ini.ReadValue(Section.Commonsettings, Key.lowlimit)
                Me.tb_highlimit.Text = ini.ReadValue(Section.Commonsettings, Key.highlimit)

            ElseIf selectedindex = 3 Then
                'Setting for stopper
                cboComName.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.ComName)
                cboBoundrate.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.Boundrate)
                cboBitrate.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.DataBits)
                cboParity.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.Parity)
                cboStopbits.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.StopBits)
                cboHandshake.SelectedItem = ini.ReadValue(Section.Scanner4ComSetting, Key.Handshake)
                cb_enabled.Checked = ini.ReadValue(Section.Scanner4ComSetting, Key.Disabled)

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Function ValidateSaveValues() As Boolean

        If Me.cb_enabled.Checked = True Then Return True

        Return validateCboValues(cboComName) And validateCboValues(cboBoundrate) And validateCboValues(cboBitrate) _
        And validateCboValues(cboParity) And validateCboValues(cboStopbits) And validateCboValues(cboHandshake)

    End Function

    Private Function validateCboValues(ByVal cbo As ComboBox) As Boolean
        If (cbo.SelectedItem = Nothing) Then
            ErrTips.SetError(cbo, "This field can not be blank.")
            Return False
        Else
            Return True
        End If
    End Function


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ErrTips.Clear()

        If (ValidateSaveValues() = False) Then 'Or (ValidateSaveValues1() = False) Or (ValidateSaveValues2() = False) Or validateCboValuesPort() = False Then
            MessageBox.Show("Can not save settings.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim selectedindex As Integer = cb_scannertype.SelectedIndex

        If selectedindex = 0 Then
            ini.WriteValue(Section.Scanner1ComSetting, Key.ComName, cboComName.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.Boundrate, cboBoundrate.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.DataBits, cboBitrate.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.Parity, cboParity.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.StopBits, cboStopbits.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.Handshake, cboHandshake.SelectedItem)
            ini.WriteValue(Section.Scanner1ComSetting, Key.Disabled, cb_enabled.Checked)

        ElseIf selectedindex = 1 Then
            ini.WriteValue(Section.Scanner2ComSetting, Key.ComName, cboComName.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.Boundrate, cboBoundrate.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.DataBits, cboBitrate.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.Parity, cboParity.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.StopBits, cboStopbits.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.Handshake, cboHandshake.SelectedItem)
            ini.WriteValue(Section.Scanner2ComSetting, Key.Disabled, cb_enabled.Checked)

        ElseIf selectedindex = 2 Then
            ini.WriteValue(Section.Scanner3ComSetting, Key.ComName, cboComName.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.Boundrate, cboBoundrate.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.DataBits, cboBitrate.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.Parity, cboParity.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.StopBits, cboStopbits.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.Handshake, cboHandshake.SelectedItem)
            ini.WriteValue(Section.Scanner3ComSetting, Key.Disabled, cb_enabled.Checked)

            ini.WriteValue(Section.Commonsettings, Key.lowlimit, Me.tb_lowlimit.Text)
            ini.WriteValue(Section.Commonsettings, Key.highlimit, Me.tb_highlimit.Text)


        ElseIf selectedindex = 3 Then
            ini.WriteValue(Section.Scanner4ComSetting, Key.ComName, cboComName.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.Boundrate, cboBoundrate.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.DataBits, cboBitrate.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.Parity, cboParity.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.StopBits, cboStopbits.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.Handshake, cboHandshake.SelectedItem)
            ini.WriteValue(Section.Scanner4ComSetting, Key.Disabled, cb_enabled.Checked)

        End If

        ini.WriteValue(Section.Commonsettings, Key.lineno, Me.tb_lineno.Text)
        ini.WriteValue(Section.Commonsettings, Key.datafolder, Me.tb_datafolder.Text)

        If rb_childpartcode.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "Partcode")
            childmode = "Partcode"
        End If
        If rb_childserial.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "Serial")
            childmode = "Serial"
        End If
        If rb_ChildSkipPN.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "SkipPartNo")
            childmode = "SkipPartNo"
        End If
        If rb_ChildNoPrefix.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "Serial_No_Prefix")
            'childmode = "SkipPartNo"
            childmode = "Serial_No_Prefix" 'beryls 20110816
        End If
        If rb_ChildPN_SN.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "Child_PN_SN")
            'childmode = "SkipPartNo"
            childmode = "Child_PN_SN" 'yyy 20111028
        End If
        If rbPN_SN_NP.Checked = True Then
            ini.WriteValue(Section.Commonsettings, Key.childmode1, "Child_PN_SN_NP")
            'childmode = "SkipPartNo"
            childmode = "Child_PN_SN_NP" 'beryls 20120228
        End If

        MessageBox.Show("Save successful", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        DialogResult = MessageBox.Show("Restart Application?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If DialogResult = Windows.Forms.DialogResult.Yes Then
            Application.Restart()
        Else
            Me.Close()
            Me.Dispose()
        End If

    End Sub

#Region "Scanner Type selector"

    Private Sub cb_scannertype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_scannertype.SelectedIndexChanged

        Me.cboBitrate.SelectedIndex = -1
        Me.cboBoundrate.SelectedIndex = -1
        Me.cboComName.SelectedIndex = -1
        Me.cboHandshake.SelectedIndex = -1
        Me.cboParity.SelectedIndex = -1
        Me.cboStopbits.SelectedIndex = -1

        LoadSaveValues(cb_scannertype.SelectedIndex)

        If cb_scannertype.SelectedIndex = 2 Then

            GroupBox1.Visible = True
        Else
            GroupBox1.Visible = False

        End If

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim bf As New FolderBrowserDialog
        bf.ShowDialog()
        Me.tb_datafolder.Text = bf.SelectedPath
    End Sub

    Private Sub btnSetupMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetupMaster.Click
        Dim IsScanner2Open As Boolean = False
        If frmMain.SPort2.IsOpen = True Then
            IsScanner2Open = True
            frmMain.SPort2.Close()
        End If

        Dim frmSetupMaster As New SetupMasterFrm
        frmSetupMaster.ShowDialog()

        If IsScanner2Open = True Then
            frmMain.SPort2.Open()
        End If
    End Sub
End Class