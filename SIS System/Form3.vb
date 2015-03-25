Imports System.Data.SqlClient
Public Class student
    Dim con As SqlConnection = New SqlConnection("Data Source=ADMINW8\SQLEXPRESS; Database=db_sis; Trusted_Connection =yes;")
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Friend name2 As String
    Public Sub loading()
        If txbx_suname.Text = "" Or txbx_suname.Text = "" Then
            MsgBox("Enter your ID and Password")
        Else
            con.Close()
            Try
                con.Open()
            Catch myerror As SqlException
                MsgBox("Error Connecting to Database. Please Try again !")
            End Try
            cmd = New SqlCommand("Select * From tbl_addstud1 where stud_uname= '" + txbx_suname.Text + "' and stud_pass ='" + txbx_spass.Text + "'", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            cmd.Connection = con
            da.SelectCommand = cmd
            Dim mydata As SqlDataReader
            mydata = cmd.ExecuteReader

            If mydata.Read() Then
                lbl_studname.Text = mydata.GetValue(4)
            End If
            If mydata.HasRows = 0 Then
                MsgBox("Invalid login")
            Else
                Panel2.Hide()
                Panel1.Show()
                student1.Show()
            End If
        End If
    End Sub

    Private Sub student_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Panel2.Show()
        Panel1.Hide()
        student1.Hide()
    End Sub
    Private Sub TabControl1_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles student1.DrawItem
        Dim g As Graphics
        Dim sText As String
        Dim iX As Integer
        Dim iY As Integer
        Dim sizeText As SizeF
        Dim ctlTab As TabControl

        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

        ctlTab = CType(sender, TabControl)

        g = e.Graphics
        sText = ctlTab.TabPages(e.Index).Text
        sizeText = g.MeasureString(sText, ctlTab.Font)
        iX = e.Bounds.Left + 6
        iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2

        If student1.SelectedIndex = e.Index Then 'Selected Tab
            g.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds) '<--- My modification
            'Else
            'This code is unnecessary because the control will automatically paint non-selected tabs
            '    g.FillRectangle(New SolidBrush(Color.LightBlue), e.Bounds)
        End If

        g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)
    End Sub

    Private Sub txbx_slogin_Click(sender As System.Object, e As System.EventArgs) Handles txbx_slogin.Click
        If txbx_suname.Text = "" Or txbx_suname.Text = "" Then
            MsgBox("Enter your ID and Password")
        Else
            con.Close()
            Try
                con.Open()
            Catch myerror As SqlException
                MsgBox("Error Connecting to Database. Please Try again !")
            End Try
            cmd = New SqlCommand("Select * From tbl_addstud1 where stud_uname= '" + txbx_suname.Text + "' and stud_pass ='" + txbx_spass.Text + "'", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            cmd.Connection = con
            da.SelectCommand = cmd
            Dim mydata As SqlDataReader
            mydata = cmd.ExecuteReader

            If mydata.Read() Then
                lbl_studname.Text = mydata.GetValue(4)
            End If
            If mydata.HasRows = 0 Then
                MsgBox("Invalid login")
            Else
                Panel2.Hide()
                Panel1.Show()
                student1.Show()
            End If
        End If
    End Sub

    Private Sub txbx_scancel_Click(sender As System.Object, e As System.EventArgs) Handles txbx_scancel.Click
        Form2.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Form2.Show()
        Me.Hide()

    End Sub
End Class