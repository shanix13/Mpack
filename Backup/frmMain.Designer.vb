<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.tb_scan = New System.Windows.Forms.TextBox
        Me.dgvChild = New System.Windows.Forms.DataGridView
        Me.clmSeq = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmItemPartNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmSerial = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.clmWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label4 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbl_msg = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lbl_prd = New System.Windows.Forms.Label
        Me.lbl_qc = New System.Windows.Forms.Label
        Me.lbl_ie = New System.Windows.Forms.Label
        Me.lbl_me = New System.Windows.Forms.Label
        Me.dgv_partnocheck = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lbl_dlot = New System.Windows.Forms.Label
        Me.tb_dlot = New System.Windows.Forms.TextBox
        Me.lbl_nextserial = New System.Windows.Forms.Label
        Me.tb_nextserial = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.tb_lineno = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.tb_startserial = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.tb_klotqty = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.tb_partcode = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tb_klotid = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.tsbl_msg = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lbl_bal = New System.Windows.Forms.Label
        Me.lbl_comp = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.btn_reworkmain = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.pb_guideimage = New System.Windows.Forms.PictureBox
        Me.pb_app1 = New System.Windows.Forms.PictureBox
        Me.pb_app4 = New System.Windows.Forms.PictureBox
        Me.pb_app2 = New System.Windows.Forms.PictureBox
        Me.pb_app3 = New System.Windows.Forms.PictureBox
        Me.btn_portsetting = New System.Windows.Forms.Button
        Me.btn_reset = New System.Windows.Forms.Button
        Me.timBuzzer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.dgvChild, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.dgv_partnocheck, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.pb_guideimage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_app1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_app4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_app2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_app3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tb_scan
        '
        Me.tb_scan.ForeColor = System.Drawing.Color.RoyalBlue
        Me.tb_scan.Location = New System.Drawing.Point(751, 13)
        Me.tb_scan.Margin = New System.Windows.Forms.Padding(4)
        Me.tb_scan.Multiline = True
        Me.tb_scan.Name = "tb_scan"
        Me.tb_scan.Size = New System.Drawing.Size(169, 22)
        Me.tb_scan.TabIndex = 3
        '
        'dgvChild
        '
        Me.dgvChild.AllowUserToAddRows = False
        Me.dgvChild.AllowUserToDeleteRows = False
        Me.dgvChild.BackgroundColor = System.Drawing.Color.Moccasin
        Me.dgvChild.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvChild.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvChild.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clmSeq, Me.clmItemPartNo, Me.clmSerial, Me.clmWeight})
        Me.dgvChild.Location = New System.Drawing.Point(402, 32)
        Me.dgvChild.Name = "dgvChild"
        Me.dgvChild.ReadOnly = True
        Me.dgvChild.RowHeadersVisible = False
        Me.dgvChild.Size = New System.Drawing.Size(325, 323)
        Me.dgvChild.TabIndex = 7
        '
        'clmSeq
        '
        Me.clmSeq.HeaderText = "Seq"
        Me.clmSeq.Name = "clmSeq"
        Me.clmSeq.ReadOnly = True
        Me.clmSeq.Width = 50
        '
        'clmItemPartNo
        '
        Me.clmItemPartNo.HeaderText = "Item Part No"
        Me.clmItemPartNo.Name = "clmItemPartNo"
        Me.clmItemPartNo.ReadOnly = True
        Me.clmItemPartNo.Visible = False
        Me.clmItemPartNo.Width = 110
        '
        'clmSerial
        '
        Me.clmSerial.HeaderText = "Serial"
        Me.clmSerial.Name = "clmSerial"
        Me.clmSerial.ReadOnly = True
        '
        'clmWeight
        '
        Me.clmWeight.HeaderText = "Weight"
        Me.clmWeight.Name = "clmWeight"
        Me.clmWeight.ReadOnly = True
        Me.clmWeight.Width = 60
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(399, 9)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Child"
        '
        'Timer1
        '
        Me.Timer1.Interval = 2000
        '
        'lbl_msg
        '
        Me.lbl_msg.AutoSize = True
        Me.lbl_msg.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_msg.ForeColor = System.Drawing.Color.Red
        Me.lbl_msg.Location = New System.Drawing.Point(504, 200)
        Me.lbl_msg.Name = "lbl_msg"
        Me.lbl_msg.Size = New System.Drawing.Size(0, 15)
        Me.lbl_msg.TabIndex = 13
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lbl_prd)
        Me.GroupBox4.Controls.Add(Me.lbl_qc)
        Me.GroupBox4.Controls.Add(Me.lbl_ie)
        Me.GroupBox4.Controls.Add(Me.lbl_me)
        Me.GroupBox4.Controls.Add(Me.pb_app1)
        Me.GroupBox4.Controls.Add(Me.pb_app4)
        Me.GroupBox4.Controls.Add(Me.pb_app2)
        Me.GroupBox4.Controls.Add(Me.pb_app3)
        Me.GroupBox4.Location = New System.Drawing.Point(748, 338)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(365, 107)
        Me.GroupBox4.TabIndex = 18
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Visible = False
        '
        'lbl_prd
        '
        Me.lbl_prd.AutoSize = True
        Me.lbl_prd.BackColor = System.Drawing.Color.White
        Me.lbl_prd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_prd.ForeColor = System.Drawing.Color.Blue
        Me.lbl_prd.Location = New System.Drawing.Point(279, 53)
        Me.lbl_prd.Name = "lbl_prd"
        Me.lbl_prd.Size = New System.Drawing.Size(45, 13)
        Me.lbl_prd.TabIndex = 21
        Me.lbl_prd.Text = "lbl_prd"
        Me.lbl_prd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_qc
        '
        Me.lbl_qc.AutoSize = True
        Me.lbl_qc.BackColor = System.Drawing.Color.White
        Me.lbl_qc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_qc.ForeColor = System.Drawing.Color.Blue
        Me.lbl_qc.Location = New System.Drawing.Point(196, 53)
        Me.lbl_qc.Name = "lbl_qc"
        Me.lbl_qc.Size = New System.Drawing.Size(41, 13)
        Me.lbl_qc.TabIndex = 20
        Me.lbl_qc.Text = "lbl_qc"
        Me.lbl_qc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_ie
        '
        Me.lbl_ie.AutoSize = True
        Me.lbl_ie.BackColor = System.Drawing.Color.White
        Me.lbl_ie.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ie.ForeColor = System.Drawing.Color.Blue
        Me.lbl_ie.Location = New System.Drawing.Point(113, 53)
        Me.lbl_ie.Name = "lbl_ie"
        Me.lbl_ie.Size = New System.Drawing.Size(37, 13)
        Me.lbl_ie.TabIndex = 19
        Me.lbl_ie.Text = "lbl_ie"
        Me.lbl_ie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_me
        '
        Me.lbl_me.AutoSize = True
        Me.lbl_me.BackColor = System.Drawing.Color.White
        Me.lbl_me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_me.ForeColor = System.Drawing.Color.Blue
        Me.lbl_me.Location = New System.Drawing.Point(26, 53)
        Me.lbl_me.Name = "lbl_me"
        Me.lbl_me.Size = New System.Drawing.Size(43, 13)
        Me.lbl_me.TabIndex = 18
        Me.lbl_me.Text = "lbl_me"
        Me.lbl_me.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dgv_partnocheck
        '
        Me.dgv_partnocheck.AllowUserToAddRows = False
        Me.dgv_partnocheck.AllowUserToDeleteRows = False
        Me.dgv_partnocheck.BackgroundColor = System.Drawing.Color.Moccasin
        Me.dgv_partnocheck.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv_partnocheck.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_partnocheck.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.dgv_partnocheck.Location = New System.Drawing.Point(12, 32)
        Me.dgv_partnocheck.Name = "dgv_partnocheck"
        Me.dgv_partnocheck.ReadOnly = True
        Me.dgv_partnocheck.RowHeadersVisible = False
        Me.dgv_partnocheck.Size = New System.Drawing.Size(384, 596)
        Me.dgv_partnocheck.TabIndex = 37
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Master"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 120
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Incoming Part"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "Scanned Data"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 120
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(13, 9)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(119, 20)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Part no check"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lbl_dlot)
        Me.GroupBox5.Controls.Add(Me.tb_dlot)
        Me.GroupBox5.Controls.Add(Me.lbl_nextserial)
        Me.GroupBox5.Controls.Add(Me.tb_nextserial)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.tb_lineno)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.tb_startserial)
        Me.GroupBox5.Controls.Add(Me.Label9)
        Me.GroupBox5.Controls.Add(Me.tb_klotqty)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.tb_partcode)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.tb_klotid)
        Me.GroupBox5.Location = New System.Drawing.Point(748, 215)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(365, 127)
        Me.GroupBox5.TabIndex = 39
        Me.GroupBox5.TabStop = False
        '
        'lbl_dlot
        '
        Me.lbl_dlot.AutoSize = True
        Me.lbl_dlot.Location = New System.Drawing.Point(7, 103)
        Me.lbl_dlot.Name = "lbl_dlot"
        Me.lbl_dlot.Size = New System.Drawing.Size(61, 16)
        Me.lbl_dlot.TabIndex = 13
        Me.lbl_dlot.Text = "D-Lot No"
        Me.lbl_dlot.Visible = False
        '
        'tb_dlot
        '
        Me.tb_dlot.Location = New System.Drawing.Point(73, 100)
        Me.tb_dlot.MaxLength = 11
        Me.tb_dlot.Name = "tb_dlot"
        Me.tb_dlot.Size = New System.Drawing.Size(100, 22)
        Me.tb_dlot.TabIndex = 12
        Me.tb_dlot.Visible = False
        '
        'lbl_nextserial
        '
        Me.lbl_nextserial.AutoSize = True
        Me.lbl_nextserial.Location = New System.Drawing.Point(179, 76)
        Me.lbl_nextserial.Name = "lbl_nextserial"
        Me.lbl_nextserial.Size = New System.Drawing.Size(73, 16)
        Me.lbl_nextserial.TabIndex = 11
        Me.lbl_nextserial.Text = "Next Serial"
        '
        'tb_nextserial
        '
        Me.tb_nextserial.Location = New System.Drawing.Point(255, 73)
        Me.tb_nextserial.Name = "tb_nextserial"
        Me.tb_nextserial.Size = New System.Drawing.Size(100, 22)
        Me.tb_nextserial.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(179, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 16)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Line no"
        '
        'tb_lineno
        '
        Me.tb_lineno.Location = New System.Drawing.Point(255, 45)
        Me.tb_lineno.Name = "tb_lineno"
        Me.tb_lineno.Size = New System.Drawing.Size(100, 22)
        Me.tb_lineno.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(179, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 16)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Start Serial"
        '
        'tb_startserial
        '
        Me.tb_startserial.Location = New System.Drawing.Point(255, 17)
        Me.tb_startserial.Name = "tb_startserial"
        Me.tb_startserial.Size = New System.Drawing.Size(100, 22)
        Me.tb_startserial.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(61, 16)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "K-Lot Qty"
        '
        'tb_klotqty
        '
        Me.tb_klotqty.Location = New System.Drawing.Point(73, 73)
        Me.tb_klotqty.Name = "tb_klotqty"
        Me.tb_klotqty.Size = New System.Drawing.Size(100, 22)
        Me.tb_klotqty.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 16)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "PartCode"
        '
        'tb_partcode
        '
        Me.tb_partcode.Location = New System.Drawing.Point(73, 17)
        Me.tb_partcode.Name = "tb_partcode"
        Me.tb_partcode.Size = New System.Drawing.Size(100, 22)
        Me.tb_partcode.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 16)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "K-Lot Id"
        '
        'tb_klotid
        '
        Me.tb_klotid.Location = New System.Drawing.Point(73, 45)
        Me.tb_klotid.Name = "tb_klotid"
        Me.tb_klotid.Size = New System.Drawing.Size(100, 22)
        Me.tb_klotid.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1046, 169)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(58, 44)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.Moccasin
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbl_msg, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 631)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1119, 22)
        Me.StatusStrip1.TabIndex = 40
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'tsbl_msg
        '
        Me.tsbl_msg.BackColor = System.Drawing.SystemColors.Control
        Me.tsbl_msg.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsbl_msg.Name = "tsbl_msg"
        Me.tsbl_msg.Size = New System.Drawing.Size(0, 17)
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'Timer2
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(604, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 16)
        Me.Label2.TabIndex = 42
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(402, 481)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(711, 147)
        Me.ListBox1.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(729, 159)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Qty: Comp / Bal"
        '
        'lbl_bal
        '
        Me.lbl_bal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_bal.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_bal.ForeColor = System.Drawing.Color.Blue
        Me.lbl_bal.Location = New System.Drawing.Point(837, 178)
        Me.lbl_bal.Name = "lbl_bal"
        Me.lbl_bal.Size = New System.Drawing.Size(80, 36)
        Me.lbl_bal.TabIndex = 46
        Me.lbl_bal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbl_comp
        '
        Me.lbl_comp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbl_comp.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_comp.ForeColor = System.Drawing.Color.Blue
        Me.lbl_comp.Location = New System.Drawing.Point(733, 178)
        Me.lbl_comp.Name = "lbl_comp"
        Me.lbl_comp.Size = New System.Drawing.Size(80, 36)
        Me.lbl_comp.TabIndex = 47
        Me.lbl_comp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(813, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(26, 37)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "/"
        '
        'btn_reworkmain
        '
        Me.btn_reworkmain.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_reworkmain.Location = New System.Drawing.Point(101, 10)
        Me.btn_reworkmain.Name = "btn_reworkmain"
        Me.btn_reworkmain.Size = New System.Drawing.Size(99, 59)
        Me.btn_reworkmain.TabIndex = 49
        Me.btn_reworkmain.Text = "Rework"
        Me.btn_reworkmain.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btn_reworkmain)
        Me.Panel1.Location = New System.Drawing.Point(402, 368)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(285, 75)
        Me.Panel1.TabIndex = 50
        '
        'TextBox1
        '
        Me.TextBox1.ForeColor = System.Drawing.Color.RoyalBlue
        Me.TextBox1.Location = New System.Drawing.Point(456, 5)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(169, 22)
        Me.TextBox1.TabIndex = 51
        '
        'pb_guideimage
        '
        Me.pb_guideimage.Location = New System.Drawing.Point(751, 8)
        Me.pb_guideimage.Name = "pb_guideimage"
        Me.pb_guideimage.Size = New System.Drawing.Size(343, 147)
        Me.pb_guideimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_guideimage.TabIndex = 8
        Me.pb_guideimage.TabStop = False
        '
        'pb_app1
        '
        Me.pb_app1.Location = New System.Drawing.Point(14, 18)
        Me.pb_app1.Name = "pb_app1"
        Me.pb_app1.Size = New System.Drawing.Size(80, 80)
        Me.pb_app1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_app1.TabIndex = 14
        Me.pb_app1.TabStop = False
        '
        'pb_app4
        '
        Me.pb_app4.Location = New System.Drawing.Point(271, 18)
        Me.pb_app4.Name = "pb_app4"
        Me.pb_app4.Size = New System.Drawing.Size(80, 80)
        Me.pb_app4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_app4.TabIndex = 17
        Me.pb_app4.TabStop = False
        '
        'pb_app2
        '
        Me.pb_app2.Location = New System.Drawing.Point(100, 18)
        Me.pb_app2.Name = "pb_app2"
        Me.pb_app2.Size = New System.Drawing.Size(80, 80)
        Me.pb_app2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_app2.TabIndex = 15
        Me.pb_app2.TabStop = False
        '
        'pb_app3
        '
        Me.pb_app3.Location = New System.Drawing.Point(185, 18)
        Me.pb_app3.Name = "pb_app3"
        Me.pb_app3.Size = New System.Drawing.Size(80, 80)
        Me.pb_app3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_app3.TabIndex = 16
        Me.pb_app3.TabStop = False
        '
        'btn_portsetting
        '
        Me.btn_portsetting.Image = CType(resources.GetObject("btn_portsetting.Image"), System.Drawing.Image)
        Me.btn_portsetting.Location = New System.Drawing.Point(921, 169)
        Me.btn_portsetting.Name = "btn_portsetting"
        Me.btn_portsetting.Size = New System.Drawing.Size(59, 44)
        Me.btn_portsetting.TabIndex = 11
        Me.btn_portsetting.UseVisualStyleBackColor = False
        '
        'btn_reset
        '
        Me.btn_reset.Image = Global.MasterPacking.My.Resources.Resources.Refresh_32x32
        Me.btn_reset.Location = New System.Drawing.Point(985, 169)
        Me.btn_reset.Name = "btn_reset"
        Me.btn_reset.Size = New System.Drawing.Size(58, 44)
        Me.btn_reset.TabIndex = 10
        Me.btn_reset.UseVisualStyleBackColor = False
        '
        'timBuzzer
        '
        Me.timBuzzer.Interval = 1000
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Moccasin
        Me.ClientSize = New System.Drawing.Size(1119, 653)
        Me.Controls.Add(Me.pb_guideimage)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lbl_comp)
        Me.Controls.Add(Me.lbl_bal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.dgv_partnocheck)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.lbl_msg)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btn_portsetting)
        Me.Controls.Add(Me.btn_reset)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dgvChild)
        Me.Controls.Add(Me.tb_scan)
        Me.Controls.Add(Me.Label6)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Master Packing"
        CType(Me.dgvChild, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.dgv_partnocheck, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.pb_guideimage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_app1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_app4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_app2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_app3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_scan As System.Windows.Forms.TextBox
    Friend WithEvents dgvChild As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pb_guideimage As System.Windows.Forms.PictureBox
    Friend WithEvents btn_reset As System.Windows.Forms.Button
    Friend WithEvents btn_portsetting As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lbl_msg As System.Windows.Forms.Label
    Friend WithEvents pb_app1 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_app2 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_app3 As System.Windows.Forms.PictureBox
    Friend WithEvents pb_app4 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_prd As System.Windows.Forms.Label
    Friend WithEvents lbl_qc As System.Windows.Forms.Label
    Friend WithEvents lbl_ie As System.Windows.Forms.Label
    Friend WithEvents lbl_me As System.Windows.Forms.Label
    Friend WithEvents dgv_partnocheck As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tb_klotid As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents tb_lineno As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tb_startserial As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tb_klotqty As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tb_partcode As System.Windows.Forms.TextBox
    Friend WithEvents lbl_nextserial As System.Windows.Forms.Label
    Friend WithEvents tb_nextserial As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsbl_msg As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbl_bal As System.Windows.Forms.Label
    Friend WithEvents lbl_comp As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_reworkmain As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lbl_dlot As System.Windows.Forms.Label
    Friend WithEvents tb_dlot As System.Windows.Forms.TextBox
    Friend WithEvents clmSeq As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmItemPartNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmSerial As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents clmWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents timBuzzer As System.Windows.Forms.Timer

End Class
