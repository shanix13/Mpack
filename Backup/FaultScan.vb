'Imports System.Data
'Imports System.Data.SqlClient
Imports System.IO.Ports
Imports System.IO

Public Class FaultScan
    Public Login As Boolean = False
    Public Username As String
    Public Password As String
    Public ProductionFauty As Boolean = False
    Dim SqlStr As String
    Dim FPort As New SerialPort
    Dim ini As New IniFile(GlobalInfo.SettingFileLoc)
    Dim scanflag As Boolean = False

    Private Sub FaultScan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Fault_login_scan_mode = "login" Then
            Me.BackColor = Color.Green
            Me.Text = "Login"
            logincancel = False
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.btn_cancel.Visible = True
            Me.PictureBox1.Visible = False
            Me.Panel1.Location = New System.Drawing.Point(8, 20)
            Me.Size = New System.Drawing.Size(400, 300)
            Me.Label2.Text = Nothing

        ElseIf Fault_login_scan_mode = "fault" Then
            Me.BackColor = Color.Red
            Me.Text = "Fault Scan"
            Me.btn_cancel.Visible = False
            fsdialogshow = True
            Me.PictureBox1.Visible = True
            Me.PictureBox1.Image = My.Resources.NG
            Me.Panel1.Location = New System.Drawing.Point(418, 20)
            Me.Size = New System.Drawing.Size(800, 300)
            'Dim Screenloc As New SQLTransClass()
            'Screenloc.SetLoc(Me, 1, SQLTransClass.LCS.Bottom_Right)
            '      Me.Label2.Text = NGscanvalue

        End If


    End Sub

    Private Sub FaultScan_Disposed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Disposed

        fsdialogshow = False
        Loginscanflag = False
    End Sub
    'Private Sub DataReceviedHandler(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
    '    Me.Invoke(New EventHandler(AddressOf DoUpdate))
    'End Sub

    'Private Sub DoUpdate(ByVal s As Object, ByVal e As EventArgs)
    '    scanflag = True
    '    Timer1.Start()
    'End Sub
    'Private Sub OpenPort()
    '    Try
    '        FPort.PortName = ini.ReadValue(Section.ScannerComSetting, Key.ComName)
    '        FPort.BaudRate = CInt(ini.ReadValue(Section.ScannerComSetting, Key.Boundrate))
    '        FPort.DataBits = CInt(ini.ReadValue(Section.ScannerComSetting, Key.DataBits))
    '        Dim strParity = ini.ReadValue(Section.ScannerComSetting, Key.Parity)
    '        FPort.Parity = DirectCast([Enum].Parse(GetType(Parity), strParity), Parity)
    '        Dim strStopBits = ini.ReadValue(Section.ScannerComSetting, Key.StopBits)
    '        FPort.StopBits = DirectCast([Enum].Parse(GetType(StopBits), strStopBits), StopBits)
    '        Dim strHandshake = ini.ReadValue(Section.ScannerComSetting, Key.Handshake)
    '        FPort.Handshake = DirectCast([Enum].Parse(GetType(Handshake), strHandshake), Handshake)

    '        If (FPort.IsOpen = False) Then
    '            FPort.Open()
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show("Wrong Port setting, Authorization bypassed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Loginscanflag = False
    '        Me.Close()
    '        Exit Sub
    '    End Try

    'End Sub

    'Private Sub ClosePort()
    '    Try
    '        If (FPort.IsOpen = True) Then
    '            FPort.Close()
    '        End If
    '    Catch ex As Exception

    '    End Try

    'End Sub
    'Private Function LoadUserList() As Boolean
    '    Dim Conn As New SqlConnection(My.Settings.SQL_Conn)
    '    Dim SqlCmd As SqlCommand
    '    Dim dr As SqlDataReader
    '    Dim Fresult As Boolean = False

    '    Try
    '        ErrMsg.Clear()
    '        Dim errorDesc As String = ""

    '        If (Conn.State = ConnectionState.Closed) Then
    '            Conn.Open()
    '        End If

    '        SqlStr = "Exec spValidatePIC  @PIC = '" & txtScanPIC.Text.Trim & "' "
    '        SqlCmd = New SqlCommand(SqlStr, Conn)
    '        dr = SqlCmd.ExecuteReader()

    '        Dim dtUser As New DataTable()
    '        dtUser.Load(dr)

    '        If (dtUser.Rows.Count > 0) Then
    '            Fresult = True
    '        Else
    '            Fresult = False
    '            '    Throw New Exception("Unauthorise PIC. " & vbCrLf & "Please try again.")
    '        End If
    '        dr.Close()
    '        SqlCmd.Dispose()

    '    Catch ex As Exception
    '        '  MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '            Conn.Dispose()
    '        End If

    '    End Try

    '    Return fresult
    'End Function

    Private Sub txtScanPIC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtScanPIC.TextChanged
        checklogin(txtScanPIC.Text)
    End Sub

    Public Sub checklogin(ByVal txtScanPIC As String)

        Dim loginapp As Boolean = False

        If txtScanPIC.Contains(Chr(13)) Then
            Me.lbl_msg.Text = Nothing
        
            If txtScanPIC.Trim.ToLower = "display" Then
                loginapp = True
            ElseIf CheckUserAuth(txtScanPIC.Trim.ToLower) = True Then
                loginapp = True
            End If

            If loginapp = True Then
                faultscanflag = False
                Loginscanflag = False
                'clearparts = True
                'If Fault_login_scan_mode = "fault" Then
                '      ResetDgvPreset()
                'End If
                SupPIC = txtScanPIC
                Me.Close()
                txtScanPIC = Nothing
                Exit Sub
            Else
                Me.lbl_msg.Text = "Unauthorise PIC. " & vbCrLf & "Please scan again."
                txtScanPIC = ""
            End If


        End If
    End Sub

    'check user from text file
    Private Function CheckUserAuth(ByVal scanvalue As String) As Boolean

        Dim matchfound As Boolean = False

        If My.Computer.FileSystem.FileExists("Auth.txt") = False Then
            MessageBox.Show("Auth file not found", "Error")
            Return False
        End If

        Using fs As New FileStream("Auth.txt", FileMode.Open)
            Using sr As New StreamReader(fs)
                sr.BaseStream.Seek(0, SeekOrigin.Begin)
                While sr.Peek() > -1
                    Dim dataline As String = sr.ReadLine
                    If dataline = Nothing Then
                        Continue While
                    End If
                    If dataline.ToLower.Trim = scanvalue Then
                        matchfound = True
                        Exit While
                    End If
                End While
            End Using
        End Using

        Return matchfound

    End Function

    'Private Sub ResetDgvPreset()
    '    For Each row As DataGridViewRow In MainFrm.dgvPreSet.Rows
    '        row.Cells("clmIsTrue").Value = "F"
    '        row.Cells("clmScanned").Value = My.Resources.nok ' Image.FromFile(Application.StartupPath & "\Resource\nok.png")
    '        row.Cells("ScannedData").Value = ""
    '        row.DefaultCellStyle.BackColor = Color.White
    '        row.DefaultCellStyle.SelectionBackColor = Color.White
    '    Next
    'End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Dim rLine As String
        System.Threading.Thread.Sleep(200)
        rLine = FPort.ReadExisting & vbCrLf
        If faultscanflag = True Or Loginscanflag = True Then
            txtScanPIC.Text = rLine & Chr(13)
        End If
        'Timer1.Start()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        '  Me.txtScanPIC.Text = "display"

        logincancel = True
        FPort.Close()
        Me.Close()
    End Sub


End Class