<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BarcodeScannerFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BarcodeScannerFrm))
        Me.gboxComPort = New System.Windows.Forms.GroupBox
        Me.cb_enabled = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.cb_scannertype = New System.Windows.Forms.ComboBox
        Me.cboHandshake = New System.Windows.Forms.ComboBox
        Me.cboComName = New System.Windows.Forms.ComboBox
        Me.cboBitrate = New System.Windows.Forms.ComboBox
        Me.cboStopbits = New System.Windows.Forms.ComboBox
        Me.cboParity = New System.Windows.Forms.ComboBox
        Me.cboBoundrate = New System.Windows.Forms.ComboBox
        Me.label4 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblStopbits = New System.Windows.Forms.Label
        Me.ErrTips = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.tb_highlimit = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tb_lowlimit = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rb_ChildPN_SN = New System.Windows.Forms.RadioButton
        Me.rb_ChildNoPrefix = New System.Windows.Forms.RadioButton
        Me.rb_ChildSkipPN = New System.Windows.Forms.RadioButton
        Me.rb_childpartcode = New System.Windows.Forms.RadioButton
        Me.rb_childserial = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.tb_lineno = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.tb_datafolder = New System.Windows.Forms.TextBox
        Me.btnSetupMaster = New System.Windows.Forms.Button
        Me.rbPN_SN_NP = New System.Windows.Forms.RadioButton
        Me.gboxComPort.SuspendLayout()
        CType(Me.ErrTips, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboxComPort
        '
        Me.gboxComPort.Controls.Add(Me.cb_enabled)
        Me.gboxComPort.Controls.Add(Me.Label1)
        Me.gboxComPort.Controls.Add(Me.Label19)
        Me.gboxComPort.Controls.Add(Me.cb_scannertype)
        Me.gboxComPort.Controls.Add(Me.cboHandshake)
        Me.gboxComPort.Controls.Add(Me.cboComName)
        Me.gboxComPort.Controls.Add(Me.cboBitrate)
        Me.gboxComPort.Controls.Add(Me.cboStopbits)
        Me.gboxComPort.Controls.Add(Me.cboParity)
        Me.gboxComPort.Controls.Add(Me.cboBoundrate)
        Me.gboxComPort.Controls.Add(Me.label4)
        Me.gboxComPort.Controls.Add(Me.label8)
        Me.gboxComPort.Controls.Add(Me.Label3)
        Me.gboxComPort.Controls.Add(Me.Label6)
        Me.gboxComPort.Controls.Add(Me.lblStopbits)
        Me.gboxComPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gboxComPort.Location = New System.Drawing.Point(9, 12)
        Me.gboxComPort.Name = "gboxComPort"
        Me.gboxComPort.Size = New System.Drawing.Size(306, 304)
        Me.gboxComPort.TabIndex = 4
        Me.gboxComPort.TabStop = False
        Me.gboxComPort.Text = "Port setting"
        '
        'cb_enabled
        '
        Me.cb_enabled.AutoSize = True
        Me.cb_enabled.Location = New System.Drawing.Point(20, 270)
        Me.cb_enabled.Name = "cb_enabled"
        Me.cb_enabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cb_enabled.Size = New System.Drawing.Size(122, 20)
        Me.cb_enabled.TabIndex = 39
        Me.cb_enabled.Text = "                Disable"
        Me.cb_enabled.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 16)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "COM Port"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(17, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(74, 16)
        Me.Label19.TabIndex = 36
        Me.Label19.Text = "Scanner ID"
        '
        'cb_scannertype
        '
        Me.cb_scannertype.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cb_scannertype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cb_scannertype.FormattingEnabled = True
        Me.cb_scannertype.Items.AddRange(New Object() {"Scanner 1", "Scanner 2", "Weight MC"})
        Me.cb_scannertype.Location = New System.Drawing.Point(126, 25)
        Me.cb_scannertype.Name = "cb_scannertype"
        Me.cb_scannertype.Size = New System.Drawing.Size(155, 24)
        Me.cb_scannertype.TabIndex = 34
        '
        'cboHandshake
        '
        Me.cboHandshake.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHandshake.FormattingEnabled = True
        Me.cboHandshake.Location = New System.Drawing.Point(126, 231)
        Me.cboHandshake.Name = "cboHandshake"
        Me.cboHandshake.Size = New System.Drawing.Size(155, 24)
        Me.cboHandshake.TabIndex = 5
        '
        'cboComName
        '
        Me.cboComName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboComName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComName.FormattingEnabled = True
        Me.cboComName.Location = New System.Drawing.Point(126, 59)
        Me.cboComName.Name = "cboComName"
        Me.cboComName.Size = New System.Drawing.Size(90, 24)
        Me.cboComName.TabIndex = 0
        '
        'cboBitrate
        '
        Me.cboBitrate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboBitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBitrate.FormattingEnabled = True
        Me.cboBitrate.Items.AddRange(New Object() {"5", "6", "7", "8"})
        Me.cboBitrate.Location = New System.Drawing.Point(126, 127)
        Me.cboBitrate.Name = "cboBitrate"
        Me.cboBitrate.Size = New System.Drawing.Size(90, 24)
        Me.cboBitrate.TabIndex = 2
        '
        'cboStopbits
        '
        Me.cboStopbits.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboStopbits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStopbits.FormattingEnabled = True
        Me.cboStopbits.Location = New System.Drawing.Point(126, 196)
        Me.cboStopbits.Name = "cboStopbits"
        Me.cboStopbits.Size = New System.Drawing.Size(90, 24)
        Me.cboStopbits.TabIndex = 4
        '
        'cboParity
        '
        Me.cboParity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboParity.FormattingEnabled = True
        Me.cboParity.Location = New System.Drawing.Point(126, 162)
        Me.cboParity.Name = "cboParity"
        Me.cboParity.Size = New System.Drawing.Size(90, 24)
        Me.cboParity.TabIndex = 3
        '
        'cboBoundrate
        '
        Me.cboBoundrate.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.cboBoundrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBoundrate.FormattingEnabled = True
        Me.cboBoundrate.Items.AddRange(New Object() {"110", "300", "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400", "460800", "921600"})
        Me.cboBoundrate.Location = New System.Drawing.Point(126, 93)
        Me.cboBoundrate.Name = "cboBoundrate"
        Me.cboBoundrate.Size = New System.Drawing.Size(90, 24)
        Me.cboBoundrate.TabIndex = 1
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(17, 234)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(102, 23)
        Me.label4.TabIndex = 33
        Me.label4.Text = "Handshake"
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(17, 130)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(102, 23)
        Me.label8.TabIndex = 31
        Me.label8.Text = "Bit Rate"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(17, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 23)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Bound Rate"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(17, 165)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 23)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Parity"
        '
        'lblStopbits
        '
        Me.lblStopbits.Location = New System.Drawing.Point(17, 199)
        Me.lblStopbits.Name = "lblStopbits"
        Me.lblStopbits.Size = New System.Drawing.Size(102, 23)
        Me.lblStopbits.TabIndex = 25
        Me.lblStopbits.Text = "Stop bits"
        '
        'ErrTips
        '
        Me.ErrTips.ContainerControl = Me
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.Location = New System.Drawing.Point(317, 400)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(109, 35)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "Exit"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.Location = New System.Drawing.Point(193, 400)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(109, 35)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tb_highlimit)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.tb_lowlimit)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(321, 241)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(153, 75)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Weight"
        '
        'tb_highlimit
        '
        Me.tb_highlimit.Location = New System.Drawing.Point(66, 43)
        Me.tb_highlimit.Name = "tb_highlimit"
        Me.tb_highlimit.Size = New System.Drawing.Size(81, 20)
        Me.tb_highlimit.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "High Limit"
        '
        'tb_lowlimit
        '
        Me.tb_lowlimit.Location = New System.Drawing.Point(66, 18)
        Me.tb_lowlimit.Name = "tb_lowlimit"
        Me.tb_lowlimit.Size = New System.Drawing.Size(81, 20)
        Me.tb_lowlimit.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Low Limit"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbPN_SN_NP)
        Me.GroupBox2.Controls.Add(Me.rb_ChildPN_SN)
        Me.GroupBox2.Controls.Add(Me.rb_ChildNoPrefix)
        Me.GroupBox2.Controls.Add(Me.rb_ChildSkipPN)
        Me.GroupBox2.Controls.Add(Me.rb_childpartcode)
        Me.GroupBox2.Controls.Add(Me.rb_childserial)
        Me.GroupBox2.Location = New System.Drawing.Point(321, 67)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(153, 168)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Child Scan"
        '
        'rb_ChildPN_SN
        '
        Me.rb_ChildPN_SN.Location = New System.Drawing.Point(6, 105)
        Me.rb_ChildPN_SN.Name = "rb_ChildPN_SN"
        Me.rb_ChildPN_SN.Size = New System.Drawing.Size(143, 33)
        Me.rb_ChildPN_SN.TabIndex = 4
        Me.rb_ChildPN_SN.TabStop = True
        Me.rb_ChildPN_SN.Text = "Child Part No && Serial No"
        Me.rb_ChildPN_SN.UseVisualStyleBackColor = True
        '
        'rb_ChildNoPrefix
        '
        Me.rb_ChildNoPrefix.AutoSize = True
        Me.rb_ChildNoPrefix.Location = New System.Drawing.Point(6, 84)
        Me.rb_ChildNoPrefix.Name = "rb_ChildNoPrefix"
        Me.rb_ChildNoPrefix.Size = New System.Drawing.Size(120, 17)
        Me.rb_ChildNoPrefix.TabIndex = 3
        Me.rb_ChildNoPrefix.TabStop = True
        Me.rb_ChildNoPrefix.Text = "Serial No (no prefix) "
        Me.rb_ChildNoPrefix.UseVisualStyleBackColor = True
        '
        'rb_ChildSkipPN
        '
        Me.rb_ChildSkipPN.AutoSize = True
        Me.rb_ChildSkipPN.Location = New System.Drawing.Point(6, 61)
        Me.rb_ChildSkipPN.Name = "rb_ChildSkipPN"
        Me.rb_ChildSkipPN.Size = New System.Drawing.Size(111, 17)
        Me.rb_ChildSkipPN.TabIndex = 2
        Me.rb_ChildSkipPN.TabStop = True
        Me.rb_ChildSkipPN.Text = "Skip Child Part No"
        Me.rb_ChildSkipPN.UseVisualStyleBackColor = True
        '
        'rb_childpartcode
        '
        Me.rb_childpartcode.AutoSize = True
        Me.rb_childpartcode.Location = New System.Drawing.Point(6, 38)
        Me.rb_childpartcode.Name = "rb_childpartcode"
        Me.rb_childpartcode.Size = New System.Drawing.Size(98, 17)
        Me.rb_childpartcode.TabIndex = 1
        Me.rb_childpartcode.TabStop = True
        Me.rb_childpartcode.Text = "Child Part Code"
        Me.rb_childpartcode.UseVisualStyleBackColor = True
        '
        'rb_childserial
        '
        Me.rb_childserial.AutoSize = True
        Me.rb_childserial.Location = New System.Drawing.Point(6, 15)
        Me.rb_childserial.Name = "rb_childserial"
        Me.rb_childserial.Size = New System.Drawing.Size(94, 17)
        Me.rb_childserial.TabIndex = 0
        Me.rb_childserial.TabStop = True
        Me.rb_childserial.Text = "Serial no (S01)"
        Me.rb_childserial.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.tb_lineno)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(323, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(153, 49)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "General"
        '
        'tb_lineno
        '
        Me.tb_lineno.Location = New System.Drawing.Point(64, 19)
        Me.tb_lineno.Name = "tb_lineno"
        Me.tb_lineno.Size = New System.Drawing.Size(81, 20)
        Me.tb_lineno.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Line"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Controls.Add(Me.tb_datafolder)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 322)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(467, 56)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Data Path"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(384, 23)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tb_datafolder
        '
        Me.tb_datafolder.Location = New System.Drawing.Point(14, 25)
        Me.tb_datafolder.Name = "tb_datafolder"
        Me.tb_datafolder.Size = New System.Drawing.Size(360, 20)
        Me.tb_datafolder.TabIndex = 3
        '
        'btnSetupMaster
        '
        Me.btnSetupMaster.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSetupMaster.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSetupMaster.Location = New System.Drawing.Point(68, 400)
        Me.btnSetupMaster.Name = "btnSetupMaster"
        Me.btnSetupMaster.Size = New System.Drawing.Size(109, 35)
        Me.btnSetupMaster.TabIndex = 9
        Me.btnSetupMaster.Text = "Setup Master"
        Me.btnSetupMaster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSetupMaster.UseVisualStyleBackColor = True
        '
        'rbPN_SN_NP
        '
        Me.rbPN_SN_NP.Location = New System.Drawing.Point(6, 134)
        Me.rbPN_SN_NP.Name = "rbPN_SN_NP"
        Me.rbPN_SN_NP.Size = New System.Drawing.Size(143, 31)
        Me.rbPN_SN_NP.TabIndex = 6
        Me.rbPN_SN_NP.TabStop = True
        Me.rbPN_SN_NP.Text = "Child Part No && Serial No (no prefix)"
        Me.rbPN_SN_NP.UseVisualStyleBackColor = True
        '
        'BarcodeScannerFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Moccasin
        Me.ClientSize = New System.Drawing.Size(483, 447)
        Me.Controls.Add(Me.btnSetupMaster)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.gboxComPort)
        Me.Controls.Add(Me.btnSave)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "BarcodeScannerFrm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Scanner Port Setting"
        Me.gboxComPort.ResumeLayout(False)
        Me.gboxComPort.PerformLayout()
        CType(Me.ErrTips, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents gboxComPort As System.Windows.Forms.GroupBox
    Private WithEvents cboHandshake As System.Windows.Forms.ComboBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents cboComName As System.Windows.Forms.ComboBox
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents cboBitrate As System.Windows.Forms.ComboBox
    Private WithEvents Label3 As System.Windows.Forms.Label
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents cboStopbits As System.Windows.Forms.ComboBox
    Private WithEvents cboParity As System.Windows.Forms.ComboBox
    Private WithEvents lblStopbits As System.Windows.Forms.Label
    Private WithEvents cboBoundrate As System.Windows.Forms.ComboBox
    Friend WithEvents ErrTips As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Private WithEvents cb_scannertype As System.Windows.Forms.ComboBox
    Friend WithEvents cb_enabled As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tb_lowlimit As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tb_highlimit As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rb_childpartcode As System.Windows.Forms.RadioButton
    Friend WithEvents rb_childserial As System.Windows.Forms.RadioButton
    Friend WithEvents tb_lineno As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents tb_datafolder As System.Windows.Forms.TextBox
    Friend WithEvents rb_ChildSkipPN As System.Windows.Forms.RadioButton
    Private WithEvents btnSetupMaster As System.Windows.Forms.Button
    Friend WithEvents rb_ChildNoPrefix As System.Windows.Forms.RadioButton
    Friend WithEvents rb_ChildPN_SN As System.Windows.Forms.RadioButton
    Friend WithEvents rbPN_SN_NP As System.Windows.Forms.RadioButton
End Class
