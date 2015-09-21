<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRework
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
        Me.rb_master = New System.Windows.Forms.RadioButton
        Me.rb_child = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.tb_masterserial = New System.Windows.Forms.TextBox
        Me.tb_remark = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btn_startRework = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'rb_master
        '
        Me.rb_master.AutoSize = True
        Me.rb_master.Location = New System.Drawing.Point(160, 48)
        Me.rb_master.Name = "rb_master"
        Me.rb_master.Size = New System.Drawing.Size(57, 17)
        Me.rb_master.TabIndex = 2
        Me.rb_master.Text = "Master"
        Me.rb_master.UseVisualStyleBackColor = True
        '
        'rb_child
        '
        Me.rb_child.AutoSize = True
        Me.rb_child.Checked = True
        Me.rb_child.Location = New System.Drawing.Point(106, 48)
        Me.rb_child.Name = "rb_child"
        Me.rb_child.Size = New System.Drawing.Size(48, 17)
        Me.rb_child.TabIndex = 3
        Me.rb_child.TabStop = True
        Me.rb_child.Text = "Child"
        Me.rb_child.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Master Serial"
        '
        'tb_masterserial
        '
        Me.tb_masterserial.Location = New System.Drawing.Point(106, 18)
        Me.tb_masterserial.Name = "tb_masterserial"
        Me.tb_masterserial.Size = New System.Drawing.Size(210, 20)
        Me.tb_masterserial.TabIndex = 1
        '
        'tb_remark
        '
        Me.tb_remark.Location = New System.Drawing.Point(106, 76)
        Me.tb_remark.Multiline = True
        Me.tb_remark.Name = "tb_remark"
        Me.tb_remark.Size = New System.Drawing.Size(210, 105)
        Me.tb_remark.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(27, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Master Carton"
        '
        'btn_startRework
        '
        Me.btn_startRework.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_startRework.Location = New System.Drawing.Point(342, 32)
        Me.btn_startRework.Name = "btn_startRework"
        Me.btn_startRework.Size = New System.Drawing.Size(84, 121)
        Me.btn_startRework.TabIndex = 7
        Me.btn_startRework.Text = "Start"
        Me.btn_startRework.UseVisualStyleBackColor = True
        '
        'frmRework
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 193)
        Me.Controls.Add(Me.btn_startRework)
        Me.Controls.Add(Me.tb_remark)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rb_child)
        Me.Controls.Add(Me.rb_master)
        Me.Controls.Add(Me.tb_masterserial)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmRework"
        Me.Text = "Rework"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rb_master As System.Windows.Forms.RadioButton
    Friend WithEvents rb_child As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tb_masterserial As System.Windows.Forms.TextBox
    Friend WithEvents tb_remark As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btn_startRework As System.Windows.Forms.Button
End Class
