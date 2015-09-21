<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FaultScan
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
        Me.txtScanPIC = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblProdFautyMsg = New System.Windows.Forms.Label
        Me.ErrMsg = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbl_msg = New System.Windows.Forms.Label
        CType(Me.ErrMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtScanPIC
        '
        Me.txtScanPIC.Location = New System.Drawing.Point(124, 109)
        Me.txtScanPIC.MaxLength = 20
        Me.txtScanPIC.Multiline = True
        Me.txtScanPIC.Name = "txtScanPIC"
        Me.txtScanPIC.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtScanPIC.ReadOnly = True
        Me.txtScanPIC.Size = New System.Drawing.Size(142, 20)
        Me.txtScanPIC.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(55, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "PIC ID: "
        '
        'lblProdFautyMsg
        '
        Me.lblProdFautyMsg.AutoSize = True
        Me.lblProdFautyMsg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProdFautyMsg.ForeColor = System.Drawing.Color.White
        Me.lblProdFautyMsg.Location = New System.Drawing.Point(22, 64)
        Me.lblProdFautyMsg.Name = "lblProdFautyMsg"
        Me.lblProdFautyMsg.Size = New System.Drawing.Size(301, 20)
        Me.lblProdFautyMsg.TabIndex = 8
        Me.lblProdFautyMsg.Text = "Please scan authorize ID to proceed"
        '
        'ErrMsg
        '
        Me.ErrMsg.ContainerControl = Me
        '
        'Timer1
        '
        '
        'btn_cancel
        '
        Me.btn_cancel.Location = New System.Drawing.Point(269, 197)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_cancel.TabIndex = 9
        Me.btn_cancel.Text = "Cancel"
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Global.MasterPacking.My.Resources.Resources.NG
        Me.PictureBox1.Location = New System.Drawing.Point(8, 20)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(399, 224)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lbl_msg)
        Me.Panel1.Controls.Add(Me.btn_cancel)
        Me.Panel1.Controls.Add(Me.lblProdFautyMsg)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtScanPIC)
        Me.Panel1.Location = New System.Drawing.Point(418, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(362, 234)
        Me.Panel1.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Yellow
        Me.Label2.Location = New System.Drawing.Point(17, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(327, 56)
        Me.Label2.TabIndex = 11
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_msg
        '
        Me.lbl_msg.AutoSize = True
        Me.lbl_msg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_msg.ForeColor = System.Drawing.Color.Yellow
        Me.lbl_msg.Location = New System.Drawing.Point(97, 148)
        Me.lbl_msg.Name = "lbl_msg"
        Me.lbl_msg.Size = New System.Drawing.Size(0, 20)
        Me.lbl_msg.TabIndex = 10
        '
        'FaultScan
        '
        Me.AcceptButton = Me.btn_cancel
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Crimson
        Me.ClientSize = New System.Drawing.Size(792, 266)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "FaultScan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Fault Register"
        Me.TopMost = True
        CType(Me.ErrMsg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtScanPIC As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ErrMsg As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblProdFautyMsg As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_msg As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
