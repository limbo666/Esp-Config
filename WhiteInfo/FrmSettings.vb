Public Class FrmSettings
    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = Form1.Top + (Form1.Height - Me.Height) / 2
        Me.Left = Form1.Left + (Form1.Width - Me.Width) / 2
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.Icon = Form1.Icon

        TextBox1.Text = FieldOneName
        TextBox2.Text = FieldTwoName
        CheckBox1.Checked = FieldOne
        CheckBox2.Checked = FieldTwo
        ComboBox1.Text = Baud
        Call CheckBox1_CheckedChanged(Nothing, Nothing)
        Call CheckBox2_CheckedChanged(Nothing, Nothing)


        CheckBox3.Checked = AskToRestart
        CheckBox4.Checked = DisconnectAfterWrite
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        TextBox1.Enabled = CheckBox1.Checked

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        TextBox2.Enabled = CheckBox2.Checked

    End Sub

    Private Sub FrmSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FieldOne = CheckBox1.Checked
        FieldTwo = CheckBox2.Checked
        SaveSetting("ESP-Config", "Settings", "FieldTwo", FieldTwo)
        SaveSetting("ESP-Config", "Settings", "FieldOne", FieldOne)
        FieldOneName = TextBox1.Text
        FieldTwoName = TextBox2.Text
        SaveSetting("ESP-Config", "Settings", "FieldTwoName", FieldTwoName)
        SaveSetting("ESP-Config", "Settings", "FieldOneName", FieldOneName)


        AskToRestart = CheckBox3.Checked
        DisconnectAfterWrite = CheckBox4.Checked
        SaveSetting("ESP-Config", "Settings", "AskToRestart", AskToRestart)
        SaveSetting("ESP-Config", "Settings", "DisconnectAfterWrite", DisconnectAfterWrite)
        Form1.ExtraFieldsMani()

        Baud = ComboBox1.Text
        SaveSetting("ESP-Config", "Settings", "Baud", Baud)
        Me.Close()

    End Sub
End Class