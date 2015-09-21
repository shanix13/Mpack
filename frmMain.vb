Imports System.IO.Ports
Imports Microsoft.Win32
Imports System.IO
Imports SoftwareLocker

'process flow: master - master - S015000xxxY - 12345 - weight - 5000xxx

'Modification by yyy 20110617
'**Incomingscan function
'-replace correct variable to filter " "
'**tb_scan text change event
'-add replace " " to "" for part no scan
'-comment at tb_nextserial.Text = (CInt(tb_nextserial.Text) + 1).ToString, In Part No mode, child serial no won't increase

'Modification by yyy 20110626
'**Scanner 2 Data Received function
'-add function to run 1 scanner (ScanMe) only if user select child mode is part no

'Modification by yyy 20110712
'**Scanner 1 Scan D-lot, when rework mode
'incorrect qty bal

'Modification by yyy 20110727
'** incom_row = 0 in txtscan_changed after master part no scan finish

'Modification by yyy 20110803 (V3.15)
'** Enlarge main form screen, view 20 qty part no in a list 

'Modification by yyy 20110807
'** increate part no to 50 qty in master setup list

'Modification by yyy 20110816 V3.15
'** child serial number no prefix(with "S01")

'Modification by yyy 20111028 V3.18
'** Add item part no in child carton



'Modification by Thiru 20111117 V3.19
'** Add write to file for all error message

'Modification by Thiru 20111117 V3.20
'** Add SupPIC and Next Serial in error msg

'Modification by Beryl 20120315 V3.21
'** Add child PN + SN no prefix

'mod by shahrul 20150918
'** Error sound coninue to beep until auth
'**Incoming,scaning part column is cleared if NG

'Scan State
'Stage 0 -  setup Pcode 
'Stage 10 - Setup Mserial
'Stage 1 - scan klot
'Stage 2 = scan klot qty
'Stage 20 = scan master part no
'Stage 21 = master carton serial no
'Stage 3 = scan Dlot
'Stage 4 = Incomming stage
Public Class frmMain

    Dim Stage As Integer = 0
    Dim incoming_check As Boolean = False
    Dim incom_row As Integer = 0
    Dim startcycle As Boolean = False

    Dim weightread_index As Integer = 0
    Dim Measureweight As Boolean = False

    Dim Pseq As Integer = 0
    Dim errorstr As String = Nothing
    Dim childqty As Integer = 0
    Dim pchildqty As Integer = 0
    Dim childstart As String = Nothing
    Dim childnext As String = Nothing
    Dim _cSerial As String = Nothing
    Dim masterprefix As String = Nothing
    Dim masternext As String = Nothing
    Dim SPort1 As New SerialPort
    Public SPort2 As New SerialPort
    Dim SPort3 As New SerialPort
    Dim ini As New IniFile(GlobalInfo.SettingFileLoc)

    Dim App_me As String = Nothing
    Dim App_ie As String = Nothing
    Dim App_qc As String = Nothing
    Dim App_prd As String = Nothing
    Dim App_me_name As String = Nothing
    Dim App_ie_name As String = Nothing
    Dim App_qc_name As String = Nothing
    Dim App_prd_name As String = Nothing
    Dim Aprovalstatus As Boolean = True
    Dim weighcheck As Boolean = False
    Dim currentversion As String = "v3.22"
    Dim previousSerialNo As String = ""
    Dim endSerialNo As Integer = 0
    Dim itemPartNo As String = Nothing
    Dim Child_PN_SN As String = Nothing
    Dim IsItemPN_Scan As Boolean = False


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim t As New TrialMaker("Master Packing", "MAPAC")
            Dim RT As TrialMaker.RunTypes = t.ShowDialog()
            Dim is_trial As Boolean = False

            If RT <> TrialMaker.RunTypes.Expired Then
                'If (RT <> TrialMaker.RunTypes.Full And RT <> TrialMaker.RunTypes.Trial) Then
                If (RT = TrialMaker.RunTypes.Expired Or RT = TrialMaker.RunTypes.UnKnown) Then
                    Me.Close()
                End If
            Else
                Me.Close()
            End If

            ToolTip1.SetToolTip(btn_reset, "New K-lot")
            ToolTip1.SetToolTip(btn_portsetting, "Setup")
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try

        AddHandler SPort1.DataReceived, AddressOf DataReceviedHandler1
        AddHandler SPort2.DataReceived, AddressOf DataReceviedHandler2
        AddHandler SPort3.DataReceived, AddressOf DataReceviedHandler3
        OpenAllports()

        checkregistry()

        If App_me = "1" Then
            Me.pb_app1.Image = My.Resources.OK_1
            Me.lbl_me.Text = App_me_name
        Else
            Me.pb_app1.Image = My.Resources.NG_1
            Me.lbl_me.Text = Nothing
            Aprovalstatus = False
        End If
        If App_ie = "1" Then
            Me.pb_app2.Image = My.Resources.OK_2
            Me.lbl_ie.Text = App_ie_name
        Else
            Me.pb_app2.Image = My.Resources.NG_2
            Me.lbl_ie.Text = Nothing
            Aprovalstatus = False
        End If
        If App_qc = "1" Then
            Me.pb_app3.Image = My.Resources.OK_3
            Me.lbl_qc.Text = App_qc_name
        Else
            Me.pb_app3.Image = My.Resources.NG_3
            Me.lbl_qc.Text = Nothing
            Aprovalstatus = False
        End If
        If App_prd = "1" Then
            Me.pb_app4.Image = My.Resources.OK_4
            Me.lbl_prd.Text = App_prd_name
        Else
            Me.pb_app4.Image = My.Resources.NG_4
            Me.lbl_prd.Text = Nothing
            Aprovalstatus = False
        End If


        'temproray make it true because no E-chop
        Aprovalstatus = True
        If Aprovalstatus = False Then
            Me.Text = "Master Packing " & " [Pending Approval] " & currentversion
        Else
            Me.Text = "Master Packing " & currentversion
        End If


        'Initial process mode is norma
        processmodeflag = "normal"


        'check to load previous data
        Dim _Partcode As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.Partcode)
        Dim _Startserial As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.Startserial)
        Dim _Nextserial As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.Nextserial)
        Dim _lineno As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.lineno)
        Dim _klotqty As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.klotqty)
        Dim _klotid As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.klotid)
        Dim _qtybal As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.qtybal)
        Dim _dlotno As String = ini.ReadValue(Sectionlog.Datarecord, Keylog.dlotno)
        processmodeflag = ini.ReadValue(Sectionlog.Datarecord, Keylog.ProcessMode)
        childmode = ini.ReadValue(Section.Commonsettings, Key.childmode1)
        If childmode <> "Child_PN_SN" And childmode <> "Child_PN_SN_NP" Then
            dgvChild.Columns("clmSerial").HeaderText = childmode
        End If

        Dim loadprev As Boolean = False
        If Not _klotqty = Nothing Then
            If CInt(_klotqty) > 0 Then
                DialogResult = MessageBox.Show("Do you want to load previous record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If DialogResult = Windows.Forms.DialogResult.Yes Then
                    tb_partcode.Text = _Partcode
                    tb_klotid.Text = _klotid
                    tb_klotqty.Text = _klotqty
                    lbl_bal.Text = _qtybal
                    lbl_comp.Text = "0"
                    If Not _klotqty = Nothing And Not _qtybal = Nothing Then
                        lbl_comp.Text = CInt(_klotqty) - CInt(_qtybal)
                    End If
                    tb_nextserial.Text = _Nextserial
                    tb_startserial.Text = _Startserial
                    tb_lineno.Text = _lineno
                    tb_dlot.Text = _dlotno
                    Getfromtextfile(_Partcode)
                    Pseq = 0
                    Stage = 4
                    GroupBox5.Enabled = False
                    Me.btnSave.Enabled = False
                    tsbl_msg.Text = "Scan Incoming Check"
                    pb_guideimage.Image = My.Resources.Scanning_Incoming
                    loadprev = True

                    If processmodeflag = "rework" Then
                        Label9.Text = "D-Lot Qty"
                        setreworkmode(True)
                    Else
                        Label9.Text = "K-Lot Qty"
                        setreworkmode(False)
                    End If
                Else
                    processmodeflag = "normal"
                    Me.pb_guideimage.Image = My.Resources.setup
                    tb_lineno.Text = ini.ReadValue(Section.Commonsettings, Key.lineno)

                End If
            End If
        Else
            tb_lineno.Text = ini.ReadValue(Section.Commonsettings, Key.lineno)
        End If

        If childmode = "Partcode" Then
            If child_partcode = Nothing Then
                Me.ListBox1.Items.Insert(0, "PartCode not found in pcode text file")
            End If
        End If

        'edit by yyy
        'no child barcode scan and weight
        If childmode = "SkipPartNo" Then
            Label2.Visible = False
            Label4.Visible = False
            dgvChild.Visible = False
        End If

        If childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP" Then
            dgvChild.Columns("clmItemPartNo").Visible = True
        Else
            dgvChild.Columns("clmItemPartNo").Visible = False
        End If

        If loadprev = False Then
            Stage = 0
            Me.pb_guideimage.Image = My.Resources.setup_scanpartcode
            tsbl_msg.Text = "Scan Part Code and Start Serial"
            Me.ListBox1.Items.Insert(0, "Stage 0")
            Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
        End If


    End Sub
    Private Sub OpenAllports()

        If Not ini.ReadValue(Section.Scanner1ComSetting, Key.Disabled) = Nothing Then
            If ini.ReadValue(Section.Scanner1ComSetting, Key.Disabled) = False Then
                Try
                    If SPort1.IsOpen = False Then
                        OpenPort1()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Open Port 1")
                End Try
            End If
        End If

        If Not ini.ReadValue(Section.Scanner2ComSetting, Key.Disabled) = Nothing Then
            If ini.ReadValue(Section.Scanner2ComSetting, Key.Disabled) = False Then
                Try
                    If SPort2.IsOpen = False Then
                        OpenPort2()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If

        If Not ini.ReadValue(Section.Scanner3ComSetting, Key.Disabled) = Nothing Then
            If ini.ReadValue(Section.Scanner3ComSetting, Key.Disabled) = False Then
                Try
                    If SPort3.IsOpen = False Then
                        OpenPort3()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
        End If

    End Sub
#Region "Scanner 1"
    Private Sub DataReceviedHandler1(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        System.Threading.Thread.Sleep(150)
        FromFirstScanner = True
        Me.Invoke(New EventHandler(AddressOf DoUpdate12))
    End Sub

    'Private Sub DoUpdate1(ByVal s As Object, ByVal e As EventArgs)
    '    System.Threading.Thread.Sleep(500)
    '    Dim rLine As String = Nothing
    '    rLine = SPort1.ReadExisting
    '    Me.ListBox1.Items.Insert(0, rLine.Replace(Chr(13), " "))
    '    rLine = rLine & Chr(13)

    '    'if fault window up
    '    If faultscanflag = True Or Loginscanflag = True Then
    '        FaultScan.txtScanPIC.Text = rLine
    '        Exit Sub
    '    End If

    '    Dim HJ As String()
    '    '   Label1.Text = rLine
    '    If rLine.Contains(Chr(13)) Then
    '        HJ = rLine.Split(Chr(13))
    '        '   rLine = rLine.Replace(Chr(13), "")
    '        ' rLine = rLine.Replace(Chr(10), "")
    '    Else
    '        Exit Sub
    '    End If

    '    ' Stage = 1
    '    Dim Mserial As String = Nothing
    '    Dim Pcode As String = Nothing

    '    Try
    '        If HJ.Length < 4 Then
    '            If Stage = 0 Then Exit Sub
    '        Else
    '            If Not HJ(0) = Nothing Then
    '                If HJ(0).Substring(0, 1) = "S" Then
    '                    Mserial = HJ(0).Trim
    '                ElseIf HJ(0).Substring(0, 1) = "P" Then
    '                    Pcode = HJ(0).Trim
    '                End If
    '                If HJ(1).Substring(0, 1) = "S" Then
    '                    Mserial = HJ(1).Trim
    '                ElseIf HJ(1).Substring(0, 1) = "P" Then
    '                    Pcode = HJ(1).Trim
    '                End If
    '            End If
    '        End If

    '        If (Mserial = Nothing Or Pcode = Nothing) And Stage = 0 Then
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "scan")
    '    End Try

    '    Me.ListBox1.Items.Insert(0, "Partcode: " & Pcode)
    '    Me.ListBox1.Items.Insert(0, "Start Serial: " & Mserial)


    '    If Stage = 0 Then
    '        initialSeq(Mserial, Pcode)
    '        tsbl_msg.Text = "Scan K-Lot Id"
    '        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)

    '    ElseIf Stage = 1 Then
    '        tb_klotid.Text = rLine
    '        Stage = 2
    '        tsbl_msg.Text = "Scan K-Lot Quantity"
    '        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
    '        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
    '        ini.WriteValue(Sectionlog.Datarecord, Keylog.klotid, tb_klotid.Text)

    '    ElseIf Stage = 2 Then
    '        '     rLine = "10"
    '        tb_klotqty.Text = rLine
    '        lbl_bal.Text = rLine
    '        Stage = 3
    '        tsbl_msg.Text = "Scan Line No"
    '        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
    '        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
    '        ini.WriteValue(Sectionlog.Datarecord, Keylog.klotqty, tb_klotqty.Text)
    '        ini.WriteValue(Sectionlog.Datarecord, Keylog.qtybal, lbl_bal.Text)

    '    ElseIf Stage = 3 Then
    '        '   rLine = "HW01"
    '        tb_lineno.Text = rLine
    '        Stage = 4
    '        GroupBox5.Enabled = False
    '        tsbl_msg.Text = "Scan Incoming Check"
    '        pb_guideimage.Image = My.Resources.Scanning_Incoming
    '        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
    '        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
    '        Me.ListBox1.Items.Insert(0, "Scan Master Carton Path code Using scaner 2")

    '        ini.WriteValue(Sectionlog.Datarecord, Keylog.lineno, tb_lineno.Text)

    '        'ElseIf Stage = 4 And incoming_check = False Then  'incomming check
    '        '    '   rLine = "P-17808180-F"
    '        '    If Incomingscan(rLine.Trim) = True Then
    '        '        Stage = 5
    '        '        Timer2.Start()
    '        '        pb_guideimage.Image = My.Resources.Scan_mSerial
    '        '        tsbl_msg.Text = "Start, Scan Master Packing"
    '        '    End If
    '        'ElseIf Stage = 5 Then  'scan master partcode and serial
    '        '    '  rLine = "P-17808180-F|S01-5591897-U" & Chr(13)
    '        '    tb_scan.Text = rLine
    '    ElseIf Stage = 6 Then
    '        tb_scan.Text = rLine
    '    End If


    'End Sub

    Private Sub DoUpdate12(ByVal s As Object, ByVal e As EventArgs)
        'Private Sub DoUpdate12(value As String)
        Dim rLine As String = Nothing
        'data scanner from first or second(ScanMe) scanner 

        If FromFirstScanner = True Then
            rLine = SPort1.ReadExisting
            'rLine = TextBox1.Text.Trim
        Else
            rLine = SPort2.ReadExisting
        End If

        'rLine = value

        Me.ListBox1.Items.Insert(0, rLine.Replace(Chr(13), " "))
        If rLine = Nothing Then Exit Sub
        rLine = rLine & Chr(13)


        'if fault window up
        If faultscanflag = True Or Loginscanflag = True Then
            FaultScan.txtScanPIC.Text = rLine
            Exit Sub
        End If


        Dim HJ As String()
        '   Label1.Text = rLine
        If rLine.Contains(Chr(13)) Then
            HJ = rLine.Split(Chr(13))
            '   rLine = rLine.Replace(Chr(13), "")
            ' rLine = rLine.Replace(Chr(10), "")
        Else
            Exit Sub
        End If
        ' Stage = 1
        Dim Mserial As String = Nothing
        Dim Pcode As String = Nothing

        Try

            'check the scan in is Pcode or Mserial
            If Stage = 0 Then

                For J As Integer = 0 To HJ.Length - 1
                    If HJ(J) = Nothing Then Continue For
                    If HJ(J).Substring(0, 1) = "P" Then
                        'Pcode = HJ(J).Trim()
                        Pcode = HJ(J)
                        Pcode = Pcode.Substring(0, Pcode.Length - 1)
                    End If
                Next
                Dim strRtn As Boolean = False
                If Not Pcode = Nothing Then
                    'load file
                    strRtn = Getfromtextfile(Pcode)
                End If
                If strRtn = True Then

                    tb_partcode.Text = Pcode

                    If processmodeflag = "normal" Then
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Partcode, tb_partcode.Text)
                        savetotext("****************************************", Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("START : " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("P-Code:" & Pcode, Now().ToString("yyyyMMdd") & "_log.txt")
                    Else
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Partcode, tb_partcode.Text)
                        savetotext("****************************************", Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("START REWORK : " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("P-Code:" & Pcode, Now().ToString("yyyyMMdd") & "_log.txt")
                    End If

                    Me.pb_guideimage.Image = My.Resources.setup_scanserialno
                    Stage = 10
                End If
            End If

            If Stage = 10 Then
                For J As Integer = 0 To HJ.Length - 1
                    If HJ(J) = Nothing Then Continue For
                    If HJ(J).Substring(0, 1) = "S" Then
                        Mserial = HJ(J)
                        'Mserial = HJ(J).Trim()
                    End If
                Next
                If Not Mserial = Nothing Then
                    If processmodeflag = "normal" Then
                        tb_startserial.Text = Mserial.Substring(3, Mserial.Length - 4)
                        tb_nextserial.Text = Mserial.Substring(3, Mserial.Length - 4)
                        Me.ListBox1.Items.Insert(0, "Start Serial " & tb_startserial.Text)
                        Me.ListBox1.Items.Insert(0, "Next Serial " & tb_nextserial.Text)
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Startserial, tb_startserial.Text)
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Nextserial, tb_nextserial.Text)
                        tsbl_msg.Text = "Scan K-Lot Id"
                        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                        Stage = 1
                        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                        savetotext("Start Serial No: " & tb_startserial.Text, Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("Next Serial No: " & tb_nextserial.Text, Now().ToString("yyyyMMdd") & "_log.txt")

                        Me.pb_guideimage.Image = My.Resources.setup_scanklotid
                        Exit Sub

                    ElseIf processmodeflag = "rework" Then
                        'edit by yyy 
                        'prevent start serial no increase at textbox(uncomment it, test only)
                        tb_startserial.Text = Mserial.Substring(3, Mserial.Length - 4)
                        tb_nextserial.Text = Mserial.Substring(3, Mserial.Length - 4)
                        Me.ListBox1.Items.Insert(0, "Start Serial " & tb_startserial.Text)
                        Me.ListBox1.Items.Insert(0, "Next Serial " & tb_nextserial.Text)
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Startserial, tb_startserial.Text)
                        ini.WriteValue(Sectionlog.Datarecord, Keylog.Nextserial, tb_nextserial.Text)
                        tsbl_msg.Text = "Scan K-Lot Id"
                        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                        Stage = 1
                        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                        savetotext("Start Serial No: " & tb_startserial.Text, Now().ToString("yyyyMMdd") & "_log.txt")
                        '   savetotext("Next Serial No: " & tb_nextserial.Text, Now().ToString("yyyyMMdd") & "_log.txt")

                        Me.pb_guideimage.Image = My.Resources.setup_scanklotid
                        Exit Sub


                    End If

                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "scan")
        End Try


        If Stage = 1 Then
            If rLine.ToString.Trim.Length <> 8 Then
                tsbl_msg.Text = "Invalid K-Lot ID"
                Exit Sub
            End If
            tb_klotid.Text = rLine
            Stage = 2
            tsbl_msg.Text = "Scan K-Lot Quantity"
            Me.ListBox1.Items.Insert(0, "Stage " & Stage)
            Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
            ini.WriteValue(Sectionlog.Datarecord, Keylog.klotid, tb_klotid.Text.Trim)
            savetotext("Klot ID: " & tb_klotid.Text, Now().ToString("yyyyMMdd") & "_log.txt")
            Me.pb_guideimage.Image = My.Resources.setup_scanklotqty
        ElseIf Stage = 2 Then

            tb_klotqty.Text = rLine
            lbl_comp.Text = "0"
            ' Stage = 3
            If processmodeflag = "normal" Then
                Stage = 4
                lbl_bal.Text = tb_klotqty.Text
                GroupBox5.Enabled = False
                Me.btnSave.Enabled = False
                tsbl_msg.Text = "Scan Incoming Check"
                pb_guideimage.Image = My.Resources.Scanning_Incoming
                Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                Me.ListBox1.Items.Insert(0, "Scan Master Carton Path code Using scanner 2")

            ElseIf processmodeflag = "rework" Then
                Stage = 3
                tsbl_msg.Text = "Scan D-Lot"
                Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                Me.pb_guideimage.Image = My.Resources.setup_scandlotid
            End If

            ini.WriteValue(Sectionlog.Datarecord, Keylog.klotid, tb_klotid.Text)
            ini.WriteValue(Sectionlog.Datarecord, Keylog.klotqty, tb_klotqty.Text)
            savetotext("Klot Qty: " & tb_klotqty.Text, Now().ToString("yyyyMMdd") & "_log.txt")
            savetotext("Line No: " & tb_lineno.Text, Now().ToString("yyyyMMdd") & "_log.txt")


        ElseIf Stage = 3 Then
            '   rLine = "HW01"
            ' tb_lineno.Text = rLine
            tb_dlot.Text = rLine
            Stage = 4
            lbl_bal.Text = tb_klotqty.Text
            GroupBox5.Enabled = False
            Me.btnSave.Enabled = False
            tsbl_msg.Text = "Scan Incoming Check"
            pb_guideimage.Image = My.Resources.Scanning_Incoming
            Me.ListBox1.Items.Insert(0, "Stage " & Stage)
            Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
            Me.ListBox1.Items.Insert(0, "Scan Master Carton Path code Using scanner 2")
            ini.WriteValue(Sectionlog.Datarecord, Keylog.dlotno, tb_dlot.Text)
            'edit by yyy 20110712
            'incorrect qty bal
            ini.WriteValue(Sectionlog.Datarecord, Keylog.qtybal, tb_klotqty.Text)
            ' savetotext("Line No: " & tb_lineno.Text, Now().ToString("yyyyMMdd") & "_log.txt")
            savetotext("D-Lot No: " & tb_dlot.Text, Now().ToString("yyyyMMdd") & "_log.txt")

        ElseIf Stage = 21 And Pseq = 1 And processmodeflag = "normal" Then
            tb_scan.Text = rLine
        ElseIf Stage = 21 And Pseq = 1 And processmodeflag = "rework" Then
            'validate dlot
            If tb_dlot.Text = "" Then
                Me.ListBox1.Items.Insert(0, "Please enter D-lot number")
                Exit Sub
            Else
                If tb_dlot.Text.Length <> 11 Then
                    Me.ListBox1.Items.Insert(0, "Invalid D-lot number")
                    Exit Sub
                Else
                    tb_scan.Text = rLine
                End If
            End If
        End If

        If Stage = 4 And incoming_check = False Then
            If Incomingscan(rLine.Trim) = True Then
                Stage = 20
                Timer2.Start()
                pb_guideimage.Image = My.Resources.Scan_ProductCode
                tsbl_msg.Text = "Cycle Start. Scan Master Part Number and Serial No A"
                Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                startcycle = True
                savetotext("Start Production Cycle " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
            End If
            'ElseIf Stage = 5 Then
            '    Stage = 6
            '    Me.ListBox1.Items.Insert(0, "Stage " & Stage)
            '    tb_scan.Text = rLine.Trim


        Else
            Me.ListBox1.Items.Insert(0, "startcycle " & startcycle)
            Me.ListBox1.Items.Insert(0, "incoming_check " & incoming_check)
            Me.ListBox1.Items.Insert(0, "Stage " & Stage)

            If incoming_check = True And startcycle = True Then
                'part number scan
                If Stage = 20 Then

                    tb_scan.Text = rLine.Trim
                ElseIf Stage = 21 Then

                    tb_scan.Text = rLine.Trim

                End If
            Else
                Me.ListBox1.Items.Insert(0, "Incorrect Stage")
            End If
        End If
    End Sub
    Private Sub OpenPort1()

        If (SPort1.IsOpen = True) Then
            SPort1.Close()
        End If

        SPort1.PortName = ini.ReadValue(Section.Scanner1ComSetting, Key.ComName)
        SPort1.BaudRate = CInt(ini.ReadValue(Section.Scanner1ComSetting, Key.Boundrate))
        SPort1.DataBits = CInt(ini.ReadValue(Section.Scanner1ComSetting, Key.DataBits))
        Dim strParity = ini.ReadValue(Section.Scanner1ComSetting, Key.Parity)
        SPort1.Parity = DirectCast([Enum].Parse(GetType(Parity), strParity), Parity)
        Dim strStopBits = ini.ReadValue(Section.Scanner1ComSetting, Key.StopBits)
        SPort1.StopBits = DirectCast([Enum].Parse(GetType(StopBits), strStopBits), StopBits)
        Dim strHandshake = ini.ReadValue(Section.Scanner1ComSetting, Key.Handshake)
        SPort1.Handshake = DirectCast([Enum].Parse(GetType(Handshake), strHandshake), Handshake)

        If (SPort1.IsOpen = False) Then
            SPort1.Open()
        End If
    End Sub

    Private Sub ClosePort1()
        Try
            If (SPort1.IsOpen = True) Then
                SPort1.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region


    Private Sub initialSeq(ByVal mpserial As String, ByVal mpcode As String)

        'Dim mpcode As String = Nothing
        'Dim mpserial As String = Nothing


        'assume parcode and serial separator as "|"
        'Dim strdata As String()
        'strdata = scandata.Split("|")
        'If strdata.Length >= 2 Then
        '    mpcode = strdata(0)
        '    mpserial = strdata(1).Split("-")(1)

        tb_partcode.Text = mpcode
        tb_startserial.Text = mpserial.Substring(3, mpserial.Length - 4)
        tb_nextserial.Text = mpserial.Substring(3, mpserial.Length - 4)
        Me.ListBox1.Items.Insert(0, "Start Serial " & tb_startserial.Text)
        Me.ListBox1.Items.Insert(0, "Next Serial " & tb_nextserial.Text)

        ini.WriteValue(Sectionlog.Datarecord, Keylog.Partcode, tb_partcode.Text)
        ini.WriteValue(Sectionlog.Datarecord, Keylog.Startserial, tb_startserial.Text)
        ini.WriteValue(Sectionlog.Datarecord, Keylog.Nextserial, tb_nextserial.Text)


        'load from textfile
        Getfromtextfile(mpcode)
        savetotext("Start : " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
        savetotext("P-Code:" & mpcode, Now().ToString("yyyyMMdd") & "_log.txt")
        Stage = 1
        Me.ListBox1.Items.Insert(0, "Stage " & Stage)
        'Else
        'MessageBox.Show("Scan Data error")
        'End If



    End Sub

    Private Function Getfromtextfile(ByVal mpcode As String) As Boolean

        Dim strRtn As Boolean = False

        mpcode = mpcode.Replace("-", "")

        Dim filepath As String = ini.ReadValue(Section.Commonsettings, Key.datafolder)
        'Dim Pcode As String = cboVersion.SelectedItem.ToString
        If IsNumeric(mpcode.Substring(0, 1)) = False Then
            mpcode = mpcode.Substring(1)
        End If

        If IsNumeric(mpcode.Substring(mpcode.Length - 1)) = False Then
            mpcode = mpcode.Substring(0, mpcode.Length - 1)
        End If

        ' here Dim filename As String = filepath & "\pcode\" & mpcode.Substring(0, mpcode.Length - 2) & "00.dat"
        'filepath = "C:\Users\sha_x\Desktop\Project\MasterPacking 20110817 V3.18.1\MasterPacking\bin\Debug"
        Dim filename As String = filepath & "\pcode\" & mpcode.Substring(0, mpcode.Length - 2) & "00.dat"
        Me.ListBox1.Items.Insert(0, "Load file " & filename)
        If My.Computer.FileSystem.FileExists(filename) = False Then
            MessageBox.Show("Part list data file not found" & vbNewLine & filename, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            strRtn = False
        Else
            readdatafile(filename, mpcode)
            incoming_check = False
            strRtn = True
        End If

        Return strRtn
    End Function

    Private Sub readdatafile(ByVal filename As String, ByVal Pcode As String)
        dgv_partnocheck.Rows.Clear()
        Dim matchfound As Boolean = False
        Dim iniData As New IniFile(filename)

        'here
        Pcode = "P" & Pcode
        'Pcode = "P17808101"
        itemPartNo = iniData.ReadValue(Pcode, "Child_PN_SN")
        child_partcode = iniData.ReadValue(Pcode, "CPart")
        Dim totalPart As Integer = Convert.ToInt32(iniData.ReadValue(Pcode, "TOTAL_PART"))

        For i As Integer = 0 To totalPart - 1
            dgv_partnocheck.Rows.Add()
            dgv_partnocheck.Rows(i).Cells(0).Value() = iniData.ReadValue(Pcode, "PART_" & (i + 1).ToString)
        Next


        'Using fs As New FileStream(filename, FileMode.Open)
        '    Using sr As New StreamReader(fs)
        '        sr.BaseStream.Seek(0, SeekOrigin.Begin)
        '        While sr.Peek() > -1
        '            Dim dataline As String = sr.ReadLine
        '            If dataline = Nothing Then
        '                Continue While
        '            End If
        '            If dataline.Contains("[") And dataline.Contains("]") And matchfound = False Then
        '                Dim datavalue As String = dataline.Replace("[", "").Replace("]", "")
        '                If IsNumeric(datavalue.Substring(0, 1)) = False Then
        '                    datavalue = datavalue.Substring(1)
        '                End If
        '                If datavalue = Pcode Then
        '                    matchfound = True
        '                End If
        '            Else
        '                If matchfound = True Then
        '                    '  Me.ListBox1.Items.Insert(0, matchfound)
        '                    Dim rowcount As Integer = dgv_partnocheck.RowCount
        '                    If Not dataline.Contains("[") And Not dataline.Contains("]") Then
        '                        If Not dataline.ToUpper.Contains("TOTAL_PART") And _
        '                        Not dataline.ToUpper.Contains("MODEL") And _
        '                       Not dataline.ToUpper.Contains("VERSION") And _
        '                       Not dataline = Nothing Then
        '                            If dataline.ToUpper.Contains("CPART") Then
        '                                child_partcode = dataline.Split("=")(1)
        '                            ElseIf dataline.ToUpper.Contains("ItemPart") Then
        '                                itemPartNo = dataline.Split("=")(1)
        '                            Else
        '                                dgv_partnocheck.Rows.Add()
        '                                dgv_partnocheck.Rows(rowcount).Cells(0).Value = dataline.Split("=")(1)
        '                            End If
        '                        End If
        '                    Else
        '                        matchfound = False
        '                        Exit While
        '                    End If
        '                End If
        '            End If
        '        End While
        '    End Using
        'End Using
    End Sub

    Private Function Incomingscan(ByVal scandata As String) As Boolean
        Dim checkresult As Boolean = False
        scandata = scandata.Replace(Chr(13), "")
        scandata = scandata.Replace(Chr(10), "")

        Dim _scandata As String

        _scandata = scandata.Replace("-", "")
        'add by yyy
        'wrong variable to filter " "
        '_scandata = scandata.Replace(" ", "")
        _scandata = _scandata.Replace(" ", "")
        If _scandata.Length > 9 Then _scandata = _scandata.Substring(0, 9)

        Me.ListBox1.Items.Insert(0, "Scan data " & _scandata)


        Dim storedata As String = dgv_partnocheck.Rows(incom_row).Cells(0).Value
        storedata = storedata.Replace("-", "")
        storedata = storedata.Replace(" ", "")
        If storedata.Length > 9 Then storedata = storedata.Substring(0, 9)

        Me.ListBox1.Items.Insert(0, "Store data " & storedata)

        If storedata = _scandata Then
            'If masterPartNo = scandata Then
            dgv_partnocheck.Rows(incom_row).Cells(1).Value = scandata
            dgv_partnocheck.Rows(incom_row).Cells(1).Style.BackColor = Color.Yellow
            checkresult = True
        Else
            'here
            For Each dgvr As DataGridViewRow In Me.dgv_partnocheck.Rows
                dgvr.Cells(1).Value = String.Empty
                dgvr.Cells(1).Style.BackColor = Color.White
                'dgv_partnocheck.Rows(incom_row).Cells(1).Style.BackColor = Color.White
            Next
            incom_row = 0
            Me.pb_guideimage.Image = My.Resources.NG
        End If

        If checkresult = True Then
            If Not dgv_partnocheck.RowCount - 1 = incom_row Then
                incom_row += 1
            Else
                'indicate incoming check complete
                incoming_check = True
                incom_row = 0
            End If
            Me.pb_guideimage.Image = My.Resources.OK
            savetotext("Incomming Check OK", Now().ToString("yyyyMMdd") & "_log.txt")

        End If

        Return incoming_check

    End Function

#Region "Scanner 2"
    Private Sub DataReceviedHandler2(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        System.Threading.Thread.Sleep(100)
        'verify if childmode running part no?
        'if childmode is partno/SkipPartNo, user may use 1 scanner (scanMe) only
        If (Stage = 0 Or Stage = 10 Or Stage = 1 Or Stage = 2 Or Stage = 3 Or Stage = 4 Or Stage = 20 Or Stage = 21) _
        And ((ini.ReadValue(Section.Commonsettings, Key.childmode1) = "Partcode") _
        Or (ini.ReadValue(Section.Commonsettings, Key.childmode1) = "SkipPartNo")) Then
            'If (Stage = 0 Or Stage = 10 Or Stage = 1 Or Stage = 2 Or Stage = 3) Then 'for testing purpose include serial no, part no, skippartno can use 1 scanner only
            FromFirstScanner = False
            Me.Invoke(New EventHandler(AddressOf DoUpdate12))
        Else
            Me.Invoke(New EventHandler(AddressOf DoUpdate2))
        End If


    End Sub

    Private Sub DoUpdate2(ByVal s As Object, ByVal e As EventArgs)
        ''OpenPort3()
        'If Not ini.ReadValue(Section.Scanner3ComSetting, Key.Disabled) = Nothing Then
        '    If ini.ReadValue(Section.Scanner3ComSetting, Key.Disabled) = False Then
        '        Try
        '            If SPort3.IsOpen = False Then
        '                OpenPort3()
        '            End If
        '        Catch ex As Exception
        '            MessageBox.Show(ex.Message)
        '        End Try
        '    End If
        'End If

        Dim rLine As String = Nothing


        'rLine = SPort2.ReadExisting
        rLine = TextBox1.Text.Trim
        If rLine = Nothing Then Exit Sub
        'rLine = "4-208-979-01"

        ' rLine = "display"
        rLine = rLine & Chr(13)

        'manualweighttest()
        'Exit Sub


        'if fault window up
        If faultscanflag = True Or Loginscanflag = True Then
            FaultScan.txtScanPIC.Text = rLine
            Exit Sub
        End If


        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
        '      startcycle = True
        Me.ListBox1.Items.Insert(0, "Stage C " & Stage)
        'incomming stage
        If Stage = 4 And incoming_check = False Then
            If Incomingscan(rLine.Trim) = True Then
                Stage = 20
                Timer2.Start()
                pb_guideimage.Image = My.Resources.Scan_ProductCode
                tsbl_msg.Text = "Cycle Start. Scan Master Part Number and Serial No A"
                Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                startcycle = True
                savetotext("Start Production Cycle " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
            End If
            'ElseIf Stage = 5 Then
            '    Stage = 6
            '    Me.ListBox1.Items.Insert(0, "Stage " & Stage)
            '    tb_scan.Text = rLine.Trim


        Else
            Me.ListBox1.Items.Insert(0, "startcycle " & startcycle)
            Me.ListBox1.Items.Insert(0, "incoming_check " & incoming_check)
            Me.ListBox1.Items.Insert(0, "Stage " & Stage)

            If incoming_check = True And startcycle = True Then
                'part number scan
                If Stage = 20 Then

                    tb_scan.Text = rLine.Trim
                ElseIf Stage = 21 Then

                    tb_scan.Text = rLine.Trim

                End If
            Else
                Me.ListBox1.Items.Insert(0, "Incorrect Stage")
            End If
        End If


    End Sub
    Private Sub OpenPort2()

        If (SPort2.IsOpen = True) Then
            SPort2.Close()
        End If

        SPort2.PortName = ini.ReadValue(Section.Scanner2ComSetting, Key.ComName)
        SPort2.BaudRate = CInt(ini.ReadValue(Section.Scanner2ComSetting, Key.Boundrate))
        SPort2.DataBits = CInt(ini.ReadValue(Section.Scanner2ComSetting, Key.DataBits))
        Dim strParity = ini.ReadValue(Section.Scanner2ComSetting, Key.Parity)
        SPort2.Parity = DirectCast([Enum].Parse(GetType(Parity), strParity), Parity)
        Dim strStopBits = ini.ReadValue(Section.Scanner2ComSetting, Key.StopBits)
        SPort2.StopBits = DirectCast([Enum].Parse(GetType(StopBits), strStopBits), StopBits)
        Dim strHandshake = ini.ReadValue(Section.Scanner2ComSetting, Key.Handshake)
        SPort2.Handshake = DirectCast([Enum].Parse(GetType(Handshake), strHandshake), Handshake)

        If (SPort2.IsOpen = False) Then
            SPort2.Open()
        End If
    End Sub

    Private Sub ClosePort2()
        Try
            If (SPort2.IsOpen = True) Then
                SPort2.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Scanner 3 - Weight "
    Private Sub DataReceviedHandler3(ByVal sender As Object, ByVal e As SerialDataReceivedEventArgs)
        System.Threading.Thread.Sleep(200)
        Me.Invoke(New EventHandler(AddressOf DoUpdate3))
    End Sub

    Private Sub DoUpdate3(ByVal s As Object, ByVal e As EventArgs)
        Dim rLine As String = Nothing
        rLine = SPort3.ReadExisting & vbCrLf
        'rLine = TextBox1.Text.Trim

        Me.ListBox1.Items.Insert(0, rLine)
        Me.ListBox1.Items.Insert(0, Measureweight)

        'if fault window up
        If faultscanflag = True Or Loginscanflag = True Then
            Exit Sub
        End If


        Try
            If Measureweight = True Then
                Dim wd As New Readweight
                Dim weightvalue As Double = wd.readweight(1, rLine) 'TextBox1.Text.Trim
                tb_scan.Text = weightvalue
                '  processweight(weightvalue)
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub manualweighttest()
        If Measureweight = True Then
            Dim wd As New Readweight
            Dim weightvalue As Double = wd.readweight(1, "G" & vbNewLine & "+0.150kg")
            tb_scan.Text = weightvalue
            '  processweight(weightvalue)
        End If

    End Sub
    Private Sub OpenPort3()

        If (SPort3.IsOpen = True) Then
            SPort3.Close()
        End If

        SPort3.PortName = ini.ReadValue(Section.Scanner3ComSetting, Key.ComName)
        SPort3.BaudRate = CInt(ini.ReadValue(Section.Scanner3ComSetting, Key.Boundrate))
        SPort3.DataBits = CInt(ini.ReadValue(Section.Scanner3ComSetting, Key.DataBits))
        Dim strParity = ini.ReadValue(Section.Scanner3ComSetting, Key.Parity)
        SPort3.Parity = DirectCast([Enum].Parse(GetType(Parity), strParity), Parity)
        Dim strStopBits = ini.ReadValue(Section.Scanner3ComSetting, Key.StopBits)
        SPort3.StopBits = DirectCast([Enum].Parse(GetType(StopBits), strStopBits), StopBits)
        Dim strHandshake = ini.ReadValue(Section.Scanner3ComSetting, Key.Handshake)
        SPort3.Handshake = DirectCast([Enum].Parse(GetType(Handshake), strHandshake), Handshake)

        If (SPort3.IsOpen = False) Then
            SPort3.Open()
        End If
    End Sub

    Private Sub ClosePort3()
        Try
            If (SPort3.IsOpen = True) Then
                SPort3.Close()
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region



    Private Sub tb_scan_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tb_scan.TextChanged

        Dim scanvalue As String = tb_scan.Text
        'MessageBox.Show("here" + scanvalue)
        If scanvalue = Nothing Then Exit Sub
        ' If Not Stage = 5 Then Exit Sub
        '  Me.ListBox1.Items.Insert(0, "Text Change detected")
        Me.ListBox1.Items.Insert(0, "Pseq " & Pseq)
        Me.ListBox1.Items.Insert(0, "TCstartcycle " & startcycle)
        Me.ListBox1.Items.Insert(0, "TCincoming_check " & incoming_check)

        scanvalue = scanvalue.Replace(Chr(13), "")
        scanvalue = scanvalue.Replace(Chr(10), "")
        'edit by yyy
        scanvalue = scanvalue.Replace(" ", "")
        scanvalue = scanvalue.Trim
        If scanvalue.Trim = Nothing Then
            tb_scan.Text = Nothing
            Exit Sub
        End If
        Dim _scandata As String = scanvalue.Replace("-", "")
        If _scandata.Length > 9 Then _scandata = _scandata.Substring(0, 9)

        Dim storedata As String = dgv_partnocheck.Rows(incom_row).Cells(0).Value.ToString.Trim
        storedata = storedata.Replace("-", "")
        storedata = storedata.Replace(" ", "")
        If storedata.Length > 9 Then storedata = storedata.Substring(0, 9)

        If Stage = 20 Then
            'part no check
            Dim regpart As String = dgv_partnocheck.Rows(incom_row).Cells(1).Value
            '   scanvalue = scanvalue.Replace("-", " ")
            If storedata = _scandata Then
                dgv_partnocheck.Rows(incom_row).Cells(2).Value = regpart
                dgv_partnocheck.Rows(incom_row).Cells(2).Style.BackColor = Color.GreenYellow
                'edit by yyy
                'part no scan more than 1
                'Stage = 21
                tb_scan.Text = Nothing
                'pb_guideimage.Image = My.Resources.Scan_mSerial
                savetotext("Part Code Scan Match : " & scanvalue & " " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
                'tb_scan.Text = Nothing

                If Not dgv_partnocheck.RowCount - 1 = incom_row Then
                    incom_row += 1
                Else
                    If childmode = "SkipPartNo" Then
                        SendBeepOK()
                        pchildqty += 1
                        childqty = 1
                        incom_row = 0
                    Else
                        '' edit by yyy add incom_row = 0 to intial master part no scan
                        incom_row = 0
                        Stage = 21
                        pb_guideimage.Image = My.Resources.Scan_mSerial
                    End If
                End If
                Exit Sub
            Else

                'here
                incom_row = 0
                For Each dgvr As DataGridViewRow In Me.dgv_partnocheck.Rows
                    dgvr.Cells(2).Value = String.Empty
                    dgvr.Cells(2).Style.BackColor = Color.White
                    'dgv_partnocheck.Rows(incom_row).Cells(1).Style.BackColor = Color.White
                Next
                pb_guideimage.Image = My.Resources.NG
                FaultScan.lbl_msg.Text = "Part Code not Match"
                StartfaultScan(FaultScan.lbl_msg.Text & "|" & storedata & "|" & _scandata)
                'SaveErrorText(FaultScan.lbl_msg.Text & "|" & storedata & "|" & _scandata, Now().ToString("yyyyMMdd") & "_errlog.txt")

                Me.ListBox1.Items.Insert(0, "Part Code not Match")
                Me.ListBox1.Items.Insert(0, "Store data : '" & storedata & "' <> Scan Data : '" & _scandata & "' ")
                tb_scan.Text = Nothing
            End If



        ElseIf Stage = 21 Then

            If Pseq = 0 Then
                tsbl_msg.Text = scanvalue
                'split scanned data to partcode and serial
                Dim mpcode As String = Nothing
                Dim mpserial As String = Nothing
                Dim mpserialno As String = Nothing
                'assume parcode and serial separator as "|"
                '  Dim strdata As String()

                Try
                    'strdata = scanvalue.Split("|")
                    ' If strdata.Length >= 2 Then
                    mpcode = Me.tb_partcode.Text  'strdata(0)

                    Dim strdata As String()
                    strdata = scanvalue.Split(Chr(13))
                    Me.ListBox1.Items.Insert(0, "Master Scan Data count " & strdata.Length)

                    If strdata.Length >= 1 Then
                        Dim matchfound As Boolean = False
                        'just check for corect Master serial
                        For T As Integer = 0 To strdata.Length - 1
                            If strdata(T) = Nothing Then Continue For

                            If strdata(T).Substring(0, 1).ToUpper = "S" Then
                                matchfound = True
                                mpserial = strdata(T).Trim
                                Exit For
                            End If
                        Next

                        'if scan value not start with "S", rescan again
                        If matchfound = False Then
                            pb_guideimage.Image = My.Resources.NG
                            'SendBeepNG()
                            tsbl_msg.Text = "Invalid master serial no format, please rescan"
                            tb_scan.Text = Nothing
                            Exit Sub
                        End If
                    End If

                    'mpserial = scanvalue.Trim 'strdata(1).Split("-")(1)
                    'mpserial = scanvalue
                    mpserial = mpserial.Replace(Chr(13), "")
                    mpserial = mpserial.Replace(Chr(10), "")

                    If mpserial.Length = 11 Then
                        mpserialno = mpserial.Substring(3, mpserial.Length - 4)
                    ElseIf mpserial.Length = 10 Then
                        mpserialno = mpserial.Substring(3, mpserial.Length - 3)
                    Else
                        mpserialno = "9999"
                    End If
                    'mpserialno = mpserial.Substring(3, mpserial.Length - 4)

                    Me.ListBox1.Items.Insert(0, "mpcode " & mpcode)
                    Me.ListBox1.Items.Insert(0, "mpserial " & mpserial)
                    Me.ListBox1.Items.Insert(0, "mpserialno " & mpserialno)


                    childqty = mpserial.Substring(1, 2) 'strdata(1).Split("-")(0).Substring(1)
                    ' Label4.Text = "Child =" & childqty.ToString
                    Me.ListBox1.Items.Insert(0, "childqty " & childqty)
                    '     mpserialno = "5591898"

                    'edit by yyy 20110704
                    If (mpserialno = tb_nextserial.Text And processmodeflag = "normal") _
                    Or (processmodeflag = "rework") Then
                        'If (mpserialno = tb_nextserial.Text And processmodeflag = "normal") _
                        'Or (mpserialno >= tb_startserial.Text And processmodeflag = "rework") Then
                        tb_nextserial.Text = mpserialno
                        endSerialNo = CInt(tb_startserial.Text) + CInt(tb_klotqty.Text)
                        If (CInt(mpserialno) < CInt(tb_startserial.Text) Or CInt(mpserialno) > endSerialNo - 1) Then
                            pb_guideimage.Image = My.Resources.NG
                            tsbl_msg.Text = "Master serial no not in the range"
                            FaultScan.lbl_msg.Text = tsbl_msg.Text

                            StartfaultScan(FaultScan.lbl_msg.Text & "|" & tb_startserial.Text)
                            'SaveErrorText(FaultScan.lbl_msg.Text & "|" & tb_startserial.Text, Now().ToString("yyyyMMdd") & "_errlog.txt")

                            Me.ListBox1.Items.Insert(0, "Start serial no : " & tb_startserial.Text)
                            Me.ListBox1.Items.Insert(0, "End serial no : " & " <> " & endSerialNo)
                            Me.ListBox1.Items.Insert(0, "Scan serial no : " & " <> " & mpserialno)
                            Exit Sub
                        End If

                        Label4.Text = "Master Serial:" & mpserialno
                        'Label4.Text = "Master Serial:" & mpserial
                        ' dgv_partnocheck.Rows(incom_row).Cells(2).Value = mpserial
                        Pseq = 1
                        If childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP" Then
                            pb_guideimage.Image = My.Resources.Scan_ProductCode
                        Else
                            pb_guideimage.Image = My.Resources.measureweight
                        End If
                        tsbl_msg.Text = "Start Child Serial Scan Using Scanner 1"

                        Me.ListBox1.Items.Insert(0, "Pseq " & Pseq)
                        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                        Me.ListBox1.Items.Insert(0, "partno and serial scan together using scanner 1")

                        weighcheck = False
                        Measureweight = True

                        savetotext("Master Serial : " & mpserial & " " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")
                        savetotext("Child Qty : " & childqty, Now().ToString("yyyyMMdd") & "_log.txt")

                    Else
                        pb_guideimage.Image = My.Resources.NG
                        FaultScan.lbl_msg.Text = "Master Serial No not match with next serial"
                        StartfaultScan(FaultScan.lbl_msg.Text & "|" & mpserialno & "|" & tb_nextserial.Text)
                        'SaveErrorText(FaultScan.lbl_msg.Text & "|" & mpserialno & "|" & tb_nextserial.Text, Now().ToString("yyyyMMdd") & "_errlog.txt")
                        tsbl_msg.Text = "Master Serial No not match with next serial"
                        Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                    End If


                Catch ex As Exception
                    MessageBox.Show("Scan Data error", "Error")
                End Try

            ElseIf Pseq = 1 And weighcheck = False Then
                'edit by yyy 20111028 because of Child_PN_SN
                If (childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP") And IsItemPN_Scan = False Then
                    'product code capture
                    If scanvalue.ToUpper.StartsWith("P") Or scanvalue.ToUpper.StartsWith("S01") Then
                        Exit Sub
                    End If

                    If itemPartNo = scanvalue Then
                        IsItemPN_Scan = True
                        Me.dgvChild.Rows.Add()
                        Dim currrow As Integer = Me.dgvChild.RowCount - 1
                        Me.dgvChild.Rows(currrow).Cells("clmItemPartNo").Value = scanvalue
                        Me.ListBox1.Items.Insert(0, "Item part no scanned : " & scanvalue)
                        Exit Sub
                    Else
                        pb_guideimage.Image = My.Resources.NG
                        tsbl_msg.Text = "Item Part No Not Match"

                        FaultScan.lbl_msg.Text = tsbl_msg.Text
                        Me.ListBox1.Items.Insert(0, "Scanned value : " & scanvalue & " doesn't same with item part no : " & itemPartNo)
                        StartfaultScan(FaultScan.lbl_msg.Text & "|" & itemPartNo & "|" & scanvalue)
                        ' SaveErrorText(FaultScan.lbl_msg.Text & "|" & itemPartNo & "|" & scanvalue, Now().ToString("yyyyMMdd") & "_errlog.txt")

                    End If
                End If
                'check spec
                ' weightread_index = 0
                If Measureweight = True Then
                    Try
                        Dim T As Double = CDbl(scanvalue)
                    Catch ex As Exception
                        tb_scan.Text = Nothing
                        'pb_guideimage.Image = My.Resources.NG
                        'tsbl_msg.Text = "Weight format wrong"
                        'FaultScan.lbl_msg.Text = tsbl_msg.Text
                        'StartfaultScan()

                        Exit Sub
                    End Try
                End If

                If processweight(scanvalue) = True Then
                    'edit by yyy 20111028 because of Child_PN_SN
                    If childmode <> "Child_PN_SN" And childmode <> "Child_PN_SN_NP" Then
                        'add to grid
                        Me.dgvChild.Rows.Add()
                    End If
                    IsItemPN_Scan = False
                    Dim currrow As Integer = Me.dgvChild.RowCount - 1
                    Me.dgvChild.Rows(currrow).Cells("clmSeq").Value = currrow + 1
                    Me.dgvChild.Rows(currrow).Cells("clmWeight").Value = scanvalue.Trim
                    Me.dgvChild.Rows(currrow).Cells("clmWeight").Style.BackColor = Color.LightGreen
                    weighcheck = True

                    Measureweight = False
                    weightread_index = currrow + 1
                    If childmode = "Serial" Then pb_guideimage.Image = My.Resources.Scan_cSerial
                    If childmode = "Partcode" Then pb_guideimage.Image = My.Resources.Scan_ProductCode

                    tsbl_msg.Text = Nothing
                    savetotext("Child Weight " & weightread_index & ": " & scanvalue & " " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")

                Else
                    Try
                        If Not CInt(scanvalue) = 0 Then
                            pb_guideimage.Image = My.Resources.NG
                            tsbl_msg.Text = "Weight out of specification"
                            FaultScan.lbl_msg.Text = tsbl_msg.Text
                            StartfaultScan(FaultScan.lbl_msg.Text & "|" & scanvalue)
                            ' SaveErrorText(FaultScan.lbl_msg.Text & "|" & scanvalue, Now().ToString("yyyyMMdd") & "_errlog.txt")


                        End If
                    Catch ex As Exception

                    End Try

                End If
            ElseIf Pseq = 1 And weighcheck = True Then

                'this is for child scan 
                Me.ListBox1.Items.Insert(0, "Current Child Count " & pchildqty)
                If pchildqty < childqty Then

                    'split scanned data to partcode and serial
                    Dim cpcode As String = Nothing
                    Dim cpserial As String = Nothing
                    Dim cpserialno As String = Nothing
                    'assume parcode and serial separator as "|"
                    Dim strdata As String()

                    Try

                        scanvalue = scanvalue.Replace("-", "")
                        scanvalue = scanvalue.Replace(" ", "")
                        strdata = scanvalue.Split(Chr(13))

                        Me.ListBox1.Items.Insert(0, "Data count " & strdata.Length)

                        If strdata.Length >= 1 Then
                            Dim matchfound As Boolean = False
                            'just check for corect child serial
                            For T As Integer = 0 To strdata.Length - 1
                                If strdata(T) = Nothing Then Continue For
                                If childmode = "Serial" Or childmode = "Serial_No_Prefix" Or childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP" Then
                                    If strdata(T).Substring(0, 3).ToUpper = "S01" Then
                                        matchfound = True
                                        cpserial = strdata(T).Trim
                                        Exit For
                                        'End If
                                        'edit by yyy 20110815
                                    ElseIf childmode = "Serial_No_Prefix" Or childmode = "Child_PN_SN_NP" Then
                                        matchfound = True
                                        cpserial = strdata(T).Trim
                                        Exit For
                                    End If
                                ElseIf childmode = "Partcode" Then
                                    'If strdata(T).Substring(0, 1).ToUpper = "P" Then
                                    '    matchfound = True
                                    '    cpcode = strdata(T).Trim
                                    '    'remove check digit
                                    '    If cpcode.Length > 9 Then
                                    '        cpcode = cpcode.Substring(0, 9)
                                    '    End If
                                    '    Exit For
                                    'End If
                                    If Not strdata(T).Substring(0, 3).ToUpper = "S01" Then
                                        matchfound = True
                                        cpcode = strdata(T).Trim
                                        Exit For
                                    End If
                                End If

                            Next

                            'if scan value not start with "S01", rescan again
                            If matchfound = False Then
                                pb_guideimage.Image = My.Resources.NG
                                'SendBeepNG()
                                tsbl_msg.Text = "Invalid serial no format, please rescan"
                                tb_scan.Text = Nothing
                                Exit Sub

                                'pb_guideimage.Image = My.Resources.NG
                                'tsbl_msg.Text = "Invalid child partcode or serial no"
                                'FaultScan.lbl_msg.Text = tsbl_msg.Text
                                'StartfaultScan()
                                '' Me.ListBox1.Items.Insert(0, cpserial.Substring(0, 3).ToString & "<> S01")
                                'Exit Sub
                            End If

                            'cpcode = strdata(0)
                            'cpserial = strdata(1)

                            'edit by yyy 20110815 add Serial_No_Prefix to substring serial no
                            'If childmode = "Serial" then
                            'If childmode = "Serial" Or childmode = "Serial_No_Prefix" Then
                            If (childmode = "Serial" Or childmode = "Child_PN_SN") Or childmode = "Serial_No_Prefix" Or childmode = "Child_PN_SN_NP" Then
                                If childmode = "Serial" Or childmode = "Child_PN_SN" Then
                                    If cpserial.Length = 11 Then
                                        cpserialno = cpserial.Substring(3, cpserial.Length - 4)
                                    ElseIf cpserial.Length = 10 Then
                                        cpserialno = cpserial.Substring(3, cpserial.Length - 3)
                                    Else
                                        cpserialno = "9999"
                                    End If
                                ElseIf childmode = "Serial_No_Prefix" Or childmode = "Child_PN_SN_NP" Then
                                    If cpserial.Length = 8 Then
                                        cpserialno = cpserial.Substring(0, cpserial.Length - 1)
                                    ElseIf cpserial.Length = 7 Then
                                        cpserialno = cpserial
                                    Else
                                        cpserialno = "9999"
                                    End If
                                End If
                                Me.ListBox1.Items.Insert(0, "cpserial " & cpserial)
                                Me.ListBox1.Items.Insert(0, "cpserialno " & cpserialno)
                                '            cpserialno = (CInt("5591898") + Me.DataGridView1.RowCount - 1).ToString
                                'If cpserial.Substring(0, 3) = "S01" Then

                                'check duplicate serial no
                                If (previousSerialNo = cpserialno) Then
                                    pb_guideimage.Image = My.Resources.NG
                                    tsbl_msg.Text = "Duplicate child serial no"
                                    FaultScan.lbl_msg.Text = tsbl_msg.Text
                                    StartfaultScan(FaultScan.lbl_msg.Text & "|" & cpserialno)
                                    'SaveErrorText(FaultScan.lbl_msg.Text & "|" & cpserialno, Now().ToString("yyyyMMdd") & "_errlog.txt")

                                    Me.ListBox1.Items.Insert(0, "Previous serial no : " & previousSerialNo)
                                    Me.ListBox1.Items.Insert(0, "Current serial no : " & " <> " & cpserialno)
                                    Exit Sub
                                End If


                                endSerialNo = CInt(tb_startserial.Text) + CInt(tb_klotqty.Text)
                                If (CInt(cpserialno) < CInt(tb_startserial.Text) Or CInt(cpserialno) > endSerialNo - 1) Then
                                    pb_guideimage.Image = My.Resources.NG
                                    tsbl_msg.Text = "Child serial no not in the range"
                                    FaultScan.lbl_msg.Text = tsbl_msg.Text
                                    StartfaultScan(FaultScan.lbl_msg.Text & "|" & tb_startserial.Text)
                                    ' SaveErrorText(FaultScan.lbl_msg.Text & "|" & tb_startserial.Text, Now().ToString("yyyyMMdd") & "_errlog.txt")

                                    Me.ListBox1.Items.Insert(0, "Start serial no : " & tb_startserial.Text)
                                    Me.ListBox1.Items.Insert(0, "End serial no : " & " <> " & endSerialNo)
                                    Me.ListBox1.Items.Insert(0, "Scan serial no : " & " <> " & cpserialno)
                                    Exit Sub
                                End If
                                'edit by yyy
                                'If (cpserialno = tb_nextserial.Text And processmodeflag = "normal") Or processmodeflag = "rework" Then
                                If (cpserialno = tb_nextserial.Text And processmodeflag = "normal") Or (cpserialno = tb_nextserial.Text And processmodeflag = "rework") Then
                                    previousSerialNo = cpserialno
                                    pchildqty += 1
                                    'increase nextserial
                                    'edit by yyy
                                    'increase next serial for normal and rework mode
                                    'If processmodeflag = "normal" Then
                                    tb_nextserial.Text = (CInt(tb_nextserial.Text) + 1).ToString
                                    'End If
                                    'Me.DataGridView1.Rows.Add()
                                    'Dim currrow As Integer = Me.DataGridView1.RowCount - 1
                                    'Me.DataGridView1.Rows(currrow).Cells(0).Value = currrow + 1
                                    '  Me.DataGridView1.Rows(currrow).Cells(1).Value = strdata(1)
                                    dgvChild.Rows(weightread_index - 1).Cells("clmSerial").Value = cpserial
                                    weighcheck = False
                                    Measureweight = True

                                    savetotext("Child Serial " & weightread_index & ": " & cpserial & " " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")

                                    'set wightindext and weight for weight measurement
                                    'Measureweight = True
                                    'weightread_index = currrow + 1

                                    If Not pchildqty = childqty Then
                                        If childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP" Then
                                            pb_guideimage.Image = My.Resources.Scan_ProductCode
                                        Else
                                            pb_guideimage.Image = My.Resources.measureweight
                                        End If
                                    Else
                                        pb_guideimage.Image = My.Resources.OK

                                        'when round to 10 complete, sound OK buzzer 2 times
                                        If Not lbl_comp.Text = Nothing Then
                                            Dim Ccount As Integer = CInt(lbl_comp.Text)
                                            Dim Cdouble As Double = Ccount / 10
                                            If Cdouble.ToString.Contains(".") = False Then
                                                SendBeepOK()
                                            End If
                                            SendBeepOK()
                                        End If
                                    End If
                                    tsbl_msg.Text = Nothing
                                Else
                                    pb_guideimage.Image = My.Resources.NG
                                    tsbl_msg.Text = "Invalid child partcode or serial no"
                                    FaultScan.lbl_msg.Text = tsbl_msg.Text
                                    StartfaultScan(FaultScan.lbl_msg.Text & "|" & cpserialno & "|" & tb_nextserial.Text)
                                    'SaveErrorText(FaultScan.lbl_msg.Text & "|" & cpserialno & "|" & tb_nextserial.Text, Now().ToString("yyyyMMdd") & "_errlog.txt")


                                    Me.ListBox1.Items.Insert(0, cpserial.Substring(0, 3).ToString & "<> S01")
                                    Me.ListBox1.Items.Insert(0, cpserialno & " <> " & tb_nextserial.Text)
                                End If


                            ElseIf childmode = "Partcode" Then

                                If cpcode = "" Then Exit Sub
                                'cpcode = cpcode.Replace("-", "").Trim

                                'If cpcode.Length >= 9 Then
                                '    cpcode = cpcode.Substring(0, 9)
                                'End If

                                Me.ListBox1.Items.Insert(0, "cpcodel " & cpcode)
                                '  Dim masterpcode As String = tb_partcode.Text ' dgv_partnocheck.Rows(0).Cells(0).Value

                                'masterpcode = masterpcode.Replace("-", "").Trim
                                'If masterpcode.Length >= 9 Then
                                '    masterpcode = masterpcode.Substring(0, 9)
                                'End If

                                '  If cpcode = masterpcode Then
                                If cpcode = child_partcode Then
                                    pchildqty += 1

                                    If processmodeflag = "normal" Then
                                        'increase nextserial
                                        'comment by yyy
                                        'Part No mode, child serial no won't increase
                                        'tb_nextserial.Text = (CInt(tb_nextserial.Text) + 1).ToString
                                    End If


                                    dgvChild.Rows(weightread_index - 1).Cells("clmSerial").Value = cpcode
                                    weighcheck = False
                                    Measureweight = True

                                    savetotext("Child Partcode " & weightread_index & ": " & cpcode & " " & Now().ToString, Now().ToString("yyyyMMdd") & "_log.txt")

                                    If Not pchildqty = childqty Then
                                        If childmode = "Child_PN_SN" Or childmode = "Child_PN_SN_NP" Then
                                            pb_guideimage.Image = My.Resources.Scan_ProductCode
                                        Else
                                            pb_guideimage.Image = My.Resources.measureweight
                                        End If

                                    Else
                                        pb_guideimage.Image = My.Resources.OK

                                        'when round to 10 complete, sound OK buzzer 2 times
                                        If Not lbl_comp.Text = Nothing Then
                                            Dim Ccount As Integer = CInt(lbl_comp.Text)
                                            Dim Cdouble As Double = Ccount / 10
                                            If Cdouble.ToString.Contains(".") = False Then
                                                SendBeepOK()
                                            End If
                                            SendBeepOK()
                                        End If
                                    End If
                                    tsbl_msg.Text = Nothing
                                Else
                                    pb_guideimage.Image = My.Resources.NG
                                    tsbl_msg.Text = "Invalid child partcode"
                                    FaultScan.lbl_msg.Text = tsbl_msg.Text
                                    StartfaultScan(FaultScan.lbl_msg.Text & "|" & cpcode & "|" & child_partcode)
                                    'SaveErrorText(FaultScan.lbl_msg.Text & "|" & cpcode & "|" & child_partcode, Now().ToString("yyyyMMdd") & "_errlog.txt")


                                    Me.ListBox1.Items.Insert(0, cpcode)
                                    Me.ListBox1.Items.Insert(0, cpcode & " <> " & child_partcode)
                                End If


                            End If



                            'Else
                            '    pb_guideimage.Image = My.Resources.NG
                            '    tsbl_msg.Text = "Invalid serial Prefix"
                            'End If
                        Else
                            tsbl_msg.Text = "Invalid scanning " & scanvalue
                        End If
                    Catch ex As Exception
                        MessageBox.Show("Scan Data error", "Error")
                    End Try

                ElseIf Measureweight = True Then
                    tsbl_msg.Text = "Measure weight to continue"
                End If

            End If


        End If

        tb_scan.Text = Nothing
    End Sub


    Private Function processweight(ByVal Value As Double) As Boolean

        Dim restr As Boolean = False
        Try

            If weightread_index <= dgvChild.RowCount Then
                'check limit
                Dim lowlimit As Double = ini.ReadValue(Section.Commonsettings, Key.lowlimit)
                Dim highlimit As Double = ini.ReadValue(Section.Commonsettings, Key.highlimit)


                Me.ListBox1.Items.Insert(0, "lowlimit " & lowlimit)
                Me.ListBox1.Items.Insert(0, "highlimit " & highlimit)
                Me.ListBox1.Items.Insert(0, "Value " & Value)


                If Value >= highlimit Or Value <= lowlimit Then
                    restr = False
                Else
                    restr = True

                End If
            End If
        Catch ex As Exception

        End Try

        Return restr

    End Function

    'Private Function ValidateMasterCarton(ByVal mSerial As String) As Boolean

    '    Try
    '        If mSerial.Substring(0, 1).ToUpper = "S" Then

    '            'check if the last char is no, if not trim it
    '            '  Dim lastchar As String = childstart.Substring(childstart.Length - 1)
    '            Dim lastchar_asc As Integer = Asc(mSerial.Substring(mSerial.Length - 1))
    '            If lastchar_asc < 47 Or lastchar_asc > 57 Then
    '                mSerial = mSerial.Substring(0, mSerial.Length - 1)
    '            End If
    '            If Not tb_nextchild.Text = Nothing Then
    '                If mSerial = tb_nextchild.Text Then
    '                    childqty = CInt(mSerial.Substring(1, 2))
    '                    childstart = mSerial.Substring(3)
    '                    Return True
    '                Else
    '                    errorstr = "Invalid Master Carton Serial"

    '                End If
    '            Else
    '                childqty = CInt(mSerial.Substring(1, 2))
    '                childstart = mSerial.Substring(3)
    '                Return True
    '            End If

    '        Else

    '            errorstr = "Invalid Master Carton Serial"

    '        End If
    '    Catch ex As Exception
    '        errorstr = "Invalid Master Carton Serial"

    '    End Try

    'End Function

    'Private Function ValidateChildCarton(ByVal cSerial As String) As Boolean

    '    Try
    '        If cSerial.Substring(0, 3).ToUpper = "S01" Then
    '            _cSerial = cSerial.Substring(3)
    '            'check if the last char is no, if not trim it
    '            Dim lastchar_asc As Integer = Asc(_cSerial.Substring(_cSerial.Length - 1))
    '            If lastchar_asc < 47 Or lastchar_asc > 57 Then
    '                _cSerial = _cSerial.Substring(0, _cSerial.Length - 1)
    '            End If
    '            If _cSerial = childnext Then
    '                Return True
    '            Else
    '                errorstr = "Invalid Child Carton Serial"

    '            End If
    '        Else
    '            errorstr = "Invalid Child Carton Serial"

    '        End If
    '    Catch ex As Exception
    '        errorstr = "Invalid Child Carton Serial"

    '    End Try

    'End Function

    'Private Function Validateweight(ByVal cWeight As String) As Boolean
    '    Dim _cWeight As Double = 0
    '    Try
    '        _cWeight = CDbl(cWeight)
    '        Return True
    '    Catch ex As Exception
    '        errorstr = "Invalid Child Carton Weight"
    '    End Try

    'End Function




    'Private Sub nextmaster()
    '    'set for next master
    '    Dim currSerial As String = tb_nextchild.Text.Substring(3)
    '    Dim curr As Integer = CInt(currSerial)
    '    childnext = (curr + 1).ToString.PadLeft(currSerial.Length, "0"c)
    '    masternext = masterprefix & childnext

    'End Sub

#Region "reset"
    Private Sub btn_reset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        Timer2.Stop()
        resetform()
        processmodeflag = "normal"
        setreworkmode(False)
    End Sub
    Private Sub resetform()
        'Timer2.Stop()
        Pseq = 0
        Stage = 0
        pchildqty = 0
        childqty = 0
        dgvChild.Rows.Clear()
        dgv_partnocheck.Rows.Clear()
        tb_klotid.Text = Nothing
        tb_klotqty.Text = Nothing
        tb_dlot.Text = Nothing
        lbl_bal.Text = Nothing
        lbl_comp.Text = Nothing
        tb_lineno.Text = ini.ReadValue(Section.Commonsettings, Key.lineno)
        tb_partcode.Text = Nothing
        tb_nextserial.Text = Nothing
        tb_startserial.Text = Nothing
        GroupBox5.Enabled = True
        startcycle = False
        Me.Label4.Text = ""
        weightread_index = 0
        Me.pb_guideimage.Image = My.Resources.setup_scanpartcode
        Me.btnSave.Enabled = True


    End Sub
#End Region

#Region "Guide"


#End Region

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Timer2.Stop()
        resetform()
    End Sub
#Region "Menu"
    Private Sub btn_portsetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_portsetting.Click
        BarcodeScannerFrm.ShowDialog()
    End Sub
    Private Sub btn_setting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmSetting.ShowDialog()
    End Sub
#End Region

#Region "Check Registry"

    Private Sub checkregistry()

        'check registry
        Dim regKey As RegistryKey
        Try
            regKey = Registry.LocalMachine.OpenSubKey("Software\SOEM\MasterPacking1.00", True)
            App_me = regKey.GetValue("me", "0")
            App_ie = regKey.GetValue("ie", "0")
            App_qc = regKey.GetValue("qc", "0")
            App_prd = regKey.GetValue("prd", "0")
            App_me_name = regKey.GetValue("me_name", "")
            App_ie_name = regKey.GetValue("ie_name", "")
            App_qc_name = regKey.GetValue("qc_name", "")
            App_prd_name = regKey.GetValue("prd_name", "")
            regKey.Close()

        Catch ex As Exception

        End Try

    End Sub




#End Region

    'end of child cycle
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If pchildqty >= childqty And childqty <> 0 Then
            Timer2.Stop()
            Me.ListBox1.Items.Insert(0, "Child complete")

            'reduce klot
            '   tb_klotqty.Text = (CInt(tb_klotqty.Text) - 1).ToString

            Dim balqty As Integer = (CInt(lbl_bal.Text) - childqty).ToString
            Dim compqty As Integer = CInt(tb_klotqty.Text) - balqty

            lbl_bal.Text = balqty.ToString
            lbl_comp.Text = compqty.ToString

            ini.WriteValue(Sectionlog.Datarecord, Keylog.qtybal, lbl_bal.Text.Trim)
            ini.WriteValue(Sectionlog.Datarecord, Keylog.Nextserial, tb_nextserial.Text.Trim)

            '  savetotext("Balance Klot : " & tb_klotqty.Text, Now().ToString("yyyyMMdd") & "_log.txt")
            savetotext("Balance Klot : " & lbl_bal.Text, Now().ToString("yyyyMMdd") & "_log.txt")

            'If tb_klotqty.Text = "0" Then
            '    DialogResult = MessageBox.Show("Klot Comleted" & vbNewLine & "Reset form now?", "Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            '    If DialogResult = Windows.Forms.DialogResult.Yes Then

            '        resetform()
            '    End If
            '    Exit Sub
            'End If

            If lbl_bal.Text = "0" Then
                DialogResult = MessageBox.Show("Klot Comleted" & vbNewLine & "Reset form now.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                If DialogResult = Windows.Forms.DialogResult.OK Then
                    resetform()
                End If
                Exit Sub
            End If


            'go for next
            Me.dgvChild.Rows.Clear()
            For G As Integer = 0 To dgv_partnocheck.RowCount - 1
                dgv_partnocheck.Rows(G).Cells(2).Value = Nothing
                dgv_partnocheck.Rows(G).Cells(2).Style.BackColor = Color.White
                Pseq = 0
                pchildqty = 0
                Stage = 20
                weightread_index = 0
                Me.Label4.Text = ""
            Next
            pb_guideimage.Image = My.Resources.OK
            tsbl_msg.Text = "Next Cycle"
            savetotext("****************************************", Now().ToString("yyyyMMdd") & "_log.txt")
            Timer2.Start()
        End If
    End Sub
    'manual enter klot qty and line
    ' request by bala 2011_02_28
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Not tb_klotid.Text = Nothing Then
            If tb_klotqty.Text <> Nothing And tb_lineno.Text <> Nothing _
            And (processmodeflag = "normal") Or (processmodeflag = "rework" And tb_dlot.Text <> "") Then

                Stage = 4
                GroupBox5.Enabled = False
                Me.btnSave.Enabled = False
                lbl_bal.Text = tb_klotqty.Text
                lbl_comp.Text = "0"
                tsbl_msg.Text = "Scan Incoming Check"
                pb_guideimage.Image = My.Resources.Scanning_Incoming
                Me.ListBox1.Items.Insert(0, "Stage " & Stage)
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
                Me.ListBox1.Items.Insert(0, "Scan Master Carton Path code Using scaner 2")

                'savetotext("Klot ID: " & tb_klotid.Text, Now().ToString("yyyyMMdd") & "_log.txt")
                savetotext("Klot Qty: " & tb_klotqty.Text, Now().ToString("yyyyMMdd") & "_log.txt")
                savetotext("Line No: " & tb_lineno.Text, Now().ToString("yyyyMMdd") & "_log.txt")

                ini.WriteValue(Sectionlog.Datarecord, Keylog.klotid, tb_klotid.Text.Trim)
                ini.WriteValue(Sectionlog.Datarecord, Keylog.klotqty, tb_klotqty.Text.Trim)
                ini.WriteValue(Sectionlog.Datarecord, Keylog.lineno, tb_lineno.Text.Trim)
                ini.WriteValue(Sectionlog.Datarecord, Keylog.qtybal, tb_klotqty.Text.Trim)

                If processmodeflag = "rework" Then
                    savetotext("DLot No: " & tb_dlot.Text, Now().ToString("yyyyMMdd") & "_log.txt")
                    ini.WriteValue(Sectionlog.Datarecord, Keylog.dlotno, tb_dlot.Text)
                End If

            Else
                If processmodeflag = "rework" Then
                    If tb_klotqty.Text = Nothing Then
                        tsbl_msg.Text = "Missing Klot Qty"
                    ElseIf tb_lineno.Text = Nothing Then
                        tsbl_msg.Text = "Missing  Line no"
                    ElseIf tb_dlot.Text = Nothing Then
                        tsbl_msg.Text = "Missing D-Lot"
                    End If
                ElseIf processmodeflag = "normal" Then
                    If tb_klotqty.Text = Nothing Then
                        tsbl_msg.Text = "Missing Klot Qty"
                    ElseIf tb_lineno.Text = Nothing Then
                        tsbl_msg.Text = "Missing  Line no"
                    End If
                End If
                Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
            End If
        Else
            tsbl_msg.Text = "Scan Klot id first"
            Me.ListBox1.Items.Insert(0, tsbl_msg.Text)
        End If

    End Sub

    Private Sub savetotext(ByVal value As String, ByVal FILE_NAME As String)

        Try

            Dim fs As FileStream = Nothing
            Dim foldername As String = ""

            If processmodeflag = "normal" Then
                foldername = ini.ReadValue(Section.Commonsettings, Key.datafolder) & "\savedata\"
            Else
                foldername = ini.ReadValue(Section.Commonsettings, Key.datafolder) & "\reworkdata\"
            End If

            If My.Computer.FileSystem.DirectoryExists(foldername) = False Then
                My.Computer.FileSystem.CreateDirectory(foldername)
            End If
            If (Not File.Exists(foldername & FILE_NAME)) Then
                fs = File.Create(foldername & FILE_NAME)
                fs.Close()
            End If

            If System.IO.File.Exists(foldername & FILE_NAME) = True Then
                Dim objWriter As New System.IO.StreamWriter(foldername & FILE_NAME, True)
                objWriter.Write(value & vbNewLine)
                objWriter.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Public Sub SaveErrorText(ByVal value As String, ByVal FILE_NAME As String)

        Try

            Dim fs As FileStream = Nothing
            Dim foldername As String = ""

            foldername = ini.ReadValue(Section.Commonsettings, Key.datafolder) & "\errorrec\"

            If My.Computer.FileSystem.DirectoryExists(foldername) = False Then
                My.Computer.FileSystem.CreateDirectory(foldername)
            End If
            If (Not File.Exists(foldername & FILE_NAME)) Then
                fs = File.Create(foldername & FILE_NAME)
                fs.Close()
            End If

            If System.IO.File.Exists(foldername & FILE_NAME) = True Then
                Dim objWriter As New System.IO.StreamWriter(foldername & FILE_NAME, True)
                objWriter.Write(Now().ToString("yyyy-MM-dd HH:mm:ss") & " " & value & "|Next SN:" & tb_nextserial.Text & "|Sup_PIC:" & SupPIC & vbNewLine)
                objWriter.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'manual enter data
    Public Sub SendBeepNG()
        System.Threading.Thread.Sleep(100)
        For i As Integer = 0 To 11
            System.Threading.Thread.Sleep(100)
            SPort2.WriteLine(Chr(1) & Chr(13))
        Next
    End Sub
    Public Sub SendBeepOK()
        System.Threading.Thread.Sleep(100)
        For i As Integer = 0 To 1
            System.Threading.Thread.Sleep(200)
            SPort2.WriteLine(Chr(1) & Chr(13))
        Next
    End Sub



    Private Sub StartfaultScan(_errMsg As String)

        Try
            timBuzzer.Enabled = True
            SendBeepNG()
            faultscanflag = True
            fsdialogshow = False
            FaultScan.txtScanPIC.Text = Nothing
            FaultScan.errMsgLog = _errMsg
            Fault_login_scan_mode = "fault"
            FaultScan.Show()
            fsdialogshow = True

        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reworkmain.Click
        'frmRework.ShowDialog()
        If btn_reworkmain.Text = "Rework" Then
            DialogResult = MessageBox.Show("Change to Rework Mode", "Change Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If DialogResult = Windows.Forms.DialogResult.Yes Then
                Label9.Text = "D-Lot Qty"
                setreworkmode(True)
                resetform()
            Else
                setreworkmode(False)
            End If
        Else

            DialogResult = MessageBox.Show("Confirm End Rework Mode", "Change Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If DialogResult = Windows.Forms.DialogResult.Yes Then
                Label9.Text = "K-Lot Qty"
                setreworkmode(False)
                resetform()
            End If
        End If
    End Sub
    Private Sub setreworkmode(ByVal mode As Boolean)
        If mode = True Then
            processmodeflag = "rework"
            Me.Panel1.BackColor = Color.Red
            Me.btn_reworkmain.Text = "End Rework"
            lbl_dlot.Visible = True
            tb_dlot.Visible = True
            tb_nextserial.Text = ""
            lbl_nextserial.Enabled = False
            tb_nextserial.Enabled = False
            ini.WriteValue(Sectionlog.Datarecord, Keylog.ProcessMode, "rework")
        Else
            processmodeflag = "normal"
            Me.Panel1.BackColor = Color.Moccasin
            Me.btn_reworkmain.Text = "Rework"
            lbl_dlot.Visible = False
            tb_dlot.Visible = False
            lbl_nextserial.Enabled = True
            tb_nextserial.Enabled = True
            ini.WriteValue(Sectionlog.Datarecord, Keylog.ProcessMode, "normal")
        End If


    End Sub

    'Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
    '    DoUpdate2(sender, e)
    'End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DoUpdate2(sender, e)
    End Sub

    Dim buzzerCount As Integer = 0
    Private Sub timBuzzer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timBuzzer.Tick
      
            PlayBackgroundSoundResource()
        

    End Sub
    Sub PlayBackgroundSoundResource()
        My.Computer.Audio.Play(My.Resources.buzzer, _
            AudioPlayMode.Background)
    End Sub

    Private Sub txtSP1_TextChanged(sender As Object, e As EventArgs) Handles txtSP1.TextChanged
        If txtSP1.Text.Contains(Chr(13)) Then
            txtSP1.Text = txtSP1.Text.Replace(Chr(13), "")
            'DoUpdate12(txtSP1.Text)
        End If


    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Incomingscan(txtSP1.Text)
        tb_scan.Text = txtSP1.Text
        dgv_partnocheck.Rows.Clear()
        Dim matchfound As Boolean = False


        For i As Integer = 0 To 5 - 1
            dgv_partnocheck.Rows.Add()
            dgv_partnocheck.Rows(i).Cells(0).Value() = i.ToString

        Next
        'timBuzzer.Enabled = True
        'FaultScan.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim storedata As String
        Dim _scandata As String
        Dim checkresult As Boolean = False

        'DoUpdate12(txtSP1.Text)
        'Incomingscan(txtSP1.Text)


        'storedata = txtSP1.Text
        '_scandata = "1"
        'storedata = storedata.Replace(Chr(13), "")
        'storedata = storedata.Replace(Chr(10), "")

        'If storedata = _scandata Then
        '    'If masterPartNo = scandata Then
        '    dgv_partnocheck.Rows(incom_row).Cells(1).Value = _scandata
        '    dgv_partnocheck.Rows(incom_row).Cells(1).Style.BackColor = Color.Yellow
        '    checkresult = True
        'Else
        '    Me.pb_guideimage.Image = My.Resources.NG
        'End If

        'If checkresult = True Then
        '    If Not dgv_partnocheck.RowCount - 1 = incom_row Then
        '        incom_row += 1
        '    Else
        '        'indicate incoming check complete
        '        incoming_check = True
        '        incom_row = 0
        '    End If
        '    Me.pb_guideimage.Image = My.Resources.OK

        'End If

        'Dim regpart As String = dgv_partnocheck.Rows(incom_row).Cells(1).Value


        'MessageBox.Show(checkresult.ToString)


        ' dgv_partnocheck.Rows(incom_row).Cells(1).Value = "2"
        'dgv_partnocheck.Rows(incom_row).Cells(2).Value = "3"
    End Sub
End Class
