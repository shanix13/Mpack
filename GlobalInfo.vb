Module GlobalInfo
    Public SettingFileLoc As String = Environment.CurrentDirectory & "\Setting.ini"
End Module

Module Section
    Public Commonsettings As String = "Commonsettings"
    Public Scanner1ComSetting As String = "Scanner1ComSetting"
    Public Scanner2ComSetting As String = "Scanner2ComSetting"
    Public Scanner3ComSetting As String = "Scanner3ComSetting"
    Public Scanner4ComSetting As String = "Scanner4ComSetting"
End Module

Module Key
    Public ComName As String = "ComName"
    Public Boundrate As String = "Boundrate"
    Public DataBits As String = "DataBits"
    Public Parity As String = "Parity"
    Public StopBits As String = "StopBits"
    Public Handshake As String = "Handshake"
    Public Disabled As String = "Disabled"

    Public sqlconnstr As String = "sqlconnstr"
    Public lowlimit As String = "lowlimit"
    Public highlimit As String = "highlimit"
    Public lineno As String = "lineno"
    Public childmode1 As String = "childmode"
    Public datafolder As String = "datafolder"

End Module

Module LotNoStatus
    Public Pending As String = "Pending"
    Public Finish As String = "Finish"
    Public Pause As String = "Pause"
End Module

Module User
    Public Username As String
    Public Password As String
End Module


