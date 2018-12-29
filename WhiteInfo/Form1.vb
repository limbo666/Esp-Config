Imports System.Threading

Public Class Form1
    Dim NumberOfPorts As Integer = 0
    Dim Delay As Integer = 50

    Dim PortStatus As String = ""
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Me.Top = GetSetting("ESP-Config", "Settings", "top", 100)
        Me.Left = GetSetting("ESP-Config", "Settings", "left", 200)
        If Me.Top < 0 Then
            Me.Top = 100
        End If

        If Me.Left < 0 Then
            Me.Left = 200
        End If
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size

        Timer1.Enabled = True
        GetSerialPortNames()
        ComboBox1.Text = GetSetting("ESP-Config", "Settings", "LastPort", "")
        TxtBxSSID.Text = GetSetting("ESP-Config", "Settings", "LastSSID", "")
        TxtBxPass.Text = GetSetting("ESP-Config", "Settings", "LastPass", "")
        TxtBxDeviceID.Text = GetSetting("ESP-Config", "Settings", "DeviceID", "")

        FieldTwo = GetSetting("ESP-Config", "Settings", "FieldTwo", False)
        FieldOne = GetSetting("ESP-Config", "Settings", "FieldOne", False)

        FieldTwoName = GetSetting("ESP-Config", "Settings", "FieldTwoName", "SomeVariable")
        FieldOneName = GetSetting("ESP-Config", "Settings", "FieldOneName", "AnotherVariable")
        AskToRestart = GetSetting("ESP-Config", "Settings", "AskToRestart", False)
        DisconnectAfterWrite = GetSetting("ESP-Config", "Settings", "DisconnectAfterWrite", False)

        TextBox1.Text = GetSetting("ESP-Config", "Settings", "FieldOneValue", "")
        TextBox2.Text = GetSetting("ESP-Config", "Settings", "FieldTwoValue", "")
        Label5.Visible = FieldOne
        Label6.Visible = FieldTwo
        Label5.Text = FieldOneName
        Label6.Text = FieldTwoName
        Baud = GetSetting("ESP-Config", "Settings", "Baud", 115200)
        ExtraFieldsMani()




    End Sub



    Sub ExtraFieldsMani()
        Label5.Visible = FieldOne
        Label6.Visible = FieldTwo
        Label5.Text = FieldOneName
        Label6.Text = FieldTwoName

        TextBox1.Visible = FieldOne
        TextBox2.Visible = FieldTwo

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        StlblResult.Text = ""
        Try
            If Trim(ComboBox1.Text) = "" Then
                Exit Sub
            Else
                SerialPort1.PortName = ComboBox1.Text
                SerialPort1.BaudRate = Baud
                SerialPort1.Encoding = System.Text.Encoding.ASCII  'change mode here
                SerialPort1.Open()
                Thread.Sleep(Delay)
                SerialPort1.WriteLine("")
                'LblTR.BackColor = Color.Blue
                'TmrTXLight.Enabled = True
                'If Trim(RichTextBox2.Text) <> Nothing Then
                'RichTextBox2.AppendText(vbNewLine)
                'End If
                'LblTR.BackColor = Color.Gray
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveSetting("ESP-Config", "Settings", "LastPort", ComboBox1.Text)

        SaveSetting("ESP-Config", "Settings", "LastSSID", TxtBxSSID.Text)
        SaveSetting("ESP-Config", "Settings", "LastPass", TxtBxPass.Text)
        SaveSetting("ESP-Config", "Settings", "DeviceID", TxtBxDeviceID.Text)
        SaveSetting("ESP-Config", "Settings", "top", Me.Top)
        SaveSetting("ESP-Config", "Settings", "left", Me.Left)
        SaveSetting("ESP-Config", "Settings", "FieldOneValue", TextBox1.Text)
        SaveSetting("ESP-Config", "Settings", "FieldTwoValue", TextBox2.Text)
        Try
            SerialPort1.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If SerialPort1.IsOpen Then
            StLblConnectionStatus.Text = "Connected"
            PortStatus = "Connected"
            StLblColor.BackColor = Color.Green
            BtnWrite.Enabled = True
            Button2.Visible = True
            Button1.Visible = False

        Else
            BtnWrite.Enabled = False
            StLblConnectionStatus.Text = "Idle"
            PortStatus = "Idle"
            StLblColor.BackColor = Color.Red
            Button2.Visible = False
            Button1.Visible = True
        End If
    End Sub



    Sub GetSerialPortNames()
        ' Show all available COM ports.

        ComboBox1.Items.Clear()
        NumberOfPorts = 0
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ' TextBox5.Text += sp & vbCrLf
            ComboBox1.Items.Add(sp)

            NumberOfPorts += 1
        Next
        '  NsTextBox2.Text = "Total ports found: " & NumberOfPorts
        '  TextBox5.BackColor = Me.BackColor
        '  TextBox5.ForeColor = Me.ForeColor
        '  Me.Refresh()
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        GetSerialPortNames()
    End Sub





    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If SerialPort1.IsOpen Then
            Try
                SerialPort1.Close()
            Catch ex As Exception

            End Try
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()

    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        FrmSettings.ShowDialog()

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        StlblResult.Text = ""
        Timer2.Enabled = False

    End Sub

    Private Sub BtnWrite_Click(sender As Object, e As EventArgs) Handles BtnWrite.Click
        StlblResult.Text = ""
        SerialPort1.Write("file.open('" & "Config.txt" & "',""w"")" & vbCr)
        SerialPort1.Write("file.writeline([[" & "DeviceID:" & TxtBxDeviceID.Text & "]])" & vbCr)
        Thread.Sleep(50)
        SerialPort1.Write("file.writeline([[" & "SSID:" & TxtBxSSID.Text & "]])" & vbCr)
        Thread.Sleep(50)
        SerialPort1.Write("file.writeline([[" & "PASS:" & TxtBxPass.Text & "]])" & vbCr)
        If FieldOne = True Then
            Thread.Sleep(50)
            SerialPort1.Write("file.writeline([[" & Label5.Text & ":" & TextBox1.Text & "]])" & vbCr)
        End If
        If FieldTwo = True Then
            Thread.Sleep(50)
            SerialPort1.Write("file.writeline([[" & Label6.Text & ":" & TextBox2.Text & "]])" & vbCr)
        End If
        Thread.Sleep(50)
        SerialPort1.Write("file.close()" & vbCr)
        Thread.Sleep(50)
        StlblResult.Text = "Written"
        If AskToRestart = True Then
            Dim ans = MsgBox("Done writing!" & vbNewLine & "Do you want to restart the module?", vbYesNo)
            If ans = vbYes Then
                SerialPort1.Write("node.restart()" & vbCr)
            End If
            StlblResult.Text = "Written - Restarted"
        End If
        If DisconnectAfterWrite = True Then
            If SerialPort1.IsOpen Then
                Try
                    SerialPort1.Close()
                Catch ex As Exception

                End Try
            End If
            StlblResult.Text = "Written - Disconnected"
        End If
        Timer2.Enabled = True

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        FrmAbout.ShowDialog()

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked

    End Sub

    Private Sub LinkLabel2_MouseDown(sender As Object, e As MouseEventArgs) Handles LinkLabel2.MouseDown
        TxtBxPass.UseSystemPasswordChar = False

    End Sub

    Private Sub LinkLabel2_MouseUp(sender As Object, e As MouseEventArgs) Handles LinkLabel2.MouseUp
        TxtBxPass.UseSystemPasswordChar = True
    End Sub
End Class
