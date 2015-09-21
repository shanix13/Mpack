Public Class frmRework
    Private Sub frmRework_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tb_masterserial.Text = Nothing
        tb_remark.Text = Nothing
        rb_child.Checked = True
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_startRework.Click
        If tb_masterserial.Text = Nothing Then
            MessageBox.Show("Please Scan Master Carton Serial No")
        ElseIf validateserialno(tb_masterserial.Text) = False Then

        Else
            frmMain.Panel1.BackColor = Color.Red()
            frmMain.btn_reworkmain.Text = "End Rework"
            processmode = "rework"
            Me.Close()
        End If
    End Sub
    Private Function validateserialno(ByVal scanserial As String) As Boolean
        If scanserial.Length < 10 Then
            MessageBox.Show("Incorrect Serial No")
            Return False
        Else
            Return True
        End If



    End Function

   
End Class