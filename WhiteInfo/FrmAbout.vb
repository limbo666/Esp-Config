Public Class FrmAbout
    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = Form1.Top + (Form1.Height - Me.Height) / 2
        Me.Left = Form1.Left + (Form1.Width - Me.Width) / 2
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.Icon = Form1.Icon
        PictureBox1.Image = Me.Icon.ToBitmap
        Label2.Text = "version " & Application.ProductVersion
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start("https://www.hackster.io/xerax/esp8266-easy-end-user-wifi-setup-117a89")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub
End Class