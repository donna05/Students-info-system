Imports System.Data.SqlClient
Public Class LoginForm1
    Dim con As SqlConnection = New SqlConnection("Data Source=ADMINW8\SQLEXPRESS; Database=db_sis; Trusted_Connection =yes;")
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If txbx_adminu.Text = "" Or txbx_adminpass.Text = "" Then
            MsgBox("Enter your ID and Password")
        Else
            con.Close()
            Try
                con.Open()
            Catch myerror As SqlException
                MsgBox("Error Connecting to Database. Please Try again !")
            End Try
            cmd = New SqlCommand("Select * From tbl_admin where admin_user= '" + txbx_adminu.Text + "' and admin_pass='" + txbx_adminpass.Text + "'", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            cmd.Connection = con
            da.SelectCommand = cmd
            Dim mydata As SqlDataReader
            mydata = cmd.ExecuteReader
            If mydata.HasRows = 0 Then
                MsgBox("Invalid login")
            Else
                admin.Show()
                Me.Hide()
            End If
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Form2.Show()
        Me.Close()
    End Sub

    Private Sub LoginForm1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
