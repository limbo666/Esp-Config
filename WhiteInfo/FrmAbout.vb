Public Class FrmAbout
    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Top = Form1.Top + (Form1.Height - Me.Height) / 2
        Me.Left = Form1.Left + (Form1.Width - Me.Width) / 2
        Me.MaximumSize = Me.Size
        Me.MinimumSize = Me.Size
        Me.Icon = Form1.Icon
        PictureBox1.Image = Me.Icon.ToBitmap
    End Sub
End Class