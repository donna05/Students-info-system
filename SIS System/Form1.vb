Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Public Class admin
    '**************TAB CONTROL****************
    Private Sub TabControl1_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl1.DrawItem
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

        If TabControl1.SelectedIndex = e.Index Then 'Selected Tab
            g.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds) '<--- My modification
            'Else
            'This code is unnecessary because the control will automatically paint non-selected tabs
            '    g.FillRectangle(New SolidBrush(Color.LightBlue), e.Bounds)
        End If

        g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)
    End Sub
    '**************TAB CONTROL****************
    Private Sub TabControl2_DrawItem(sender As System.Object, e As System.Windows.Forms.DrawItemEventArgs) Handles TabControl2.DrawItem
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

        If TabControl1.SelectedIndex = e.Index Then 'Selected Tab
            g.FillRectangle(SystemBrushes.ControlLightLight, e.Bounds) '<--- My modification
            'Else
            'This code is unnecessary because the control will automatically paint non-selected tabs
            '    g.FillRectangle(New SolidBrush(Color.LightBlue), e.Bounds)
        End If

        g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)
    End Sub
    '**************COMMAND****************
    Dim con As SqlConnection = New SqlConnection("Data Source=ADMINW8\SQLEXPRESS; Database=db_sis; Trusted_Connection =yes;")
    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Friend name2 As String

    '*************CLEAR TEXTBOX*****************
    Public Sub studclear()
        txbx_uname.Clear()
        txbx_pass.Clear()
        txbx_lname.Clear()
        txbx_fname.Clear()
        txbx_mname.Clear()
        txbx_age.Clear()
        txbx_address.Clear()
        txbx_email.Clear()
        txbx_cstatus.Clear()
        txbx_father.Clear()
        txbx_foccupation.Clear()
        txbx_mother.Clear()
        txbx_moccupation.Clear()
        txbx_guardia.Clear()
        txbx_goccupation.Clear()
        txbx_guardrelation.Clear()
        txbx_siblings.Clear()

    End Sub

    '*************ADD STUDENT*****************
    Public Sub admin_addstud()
        cmd = New SqlCommand("Insert Into tbl_addstud1 (stud_uname, stud_pass, stud_lname, stud_fname, stud_mname, stud_bmonth, stud_bdate, stud_byear, stud_age, stud_gender, stud_address, stud_email, stud_cstatus, stud_father, stud_focc, stud_mother, stud_mocc, stud_guardian, stud_gocc, stud_grela, stud_siblings) Values ('" + txbx_uname.Text + "', '" + txbx_pass.Text + "','" + txbx_lname.Text + "', '" + txbx_fname.Text + "', '" + txbx_mname.Text + "', '" + ComboBox1.Text + "', '" + ComboBox2.Text + "', '" + ComboBox3.Text + "', '" + txbx_age.Text + "', '" + ComboBox4.Text + "', '" + txbx_address.Text + "', '" + txbx_email.Text + "', '" + txbx_cstatus.Text + "', '" + txbx_father.Text + "', '" + txbx_foccupation.Text + "', '" + txbx_mother.Text + "', '" + txbx_moccupation.Text + "', '" + txbx_guardia.Text + "', '" + txbx_goccupation.Text + "', '" + txbx_guardrelation.Text + "', '" + txbx_siblings.Text + "')", con)
        da = New SqlDataAdapter(cmd)
        ds = New DataSet()
        da.Fill(ds, "db_sis")
        MsgBox("Successful")
        studclear()
        TabPage1.Show()
    End Sub
    '**************STUDENT OUTPUT****************
    Public Sub student_output()
        Dim str As String = "Select * From tbl_addstud1"
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim TABLE As New DataTable
        Dim i As Integer

        With cmd
            .CommandText = str
            .Connection = con
        End With

        With da
            .SelectCommand = cmd
            .Fill(TABLE)
        End With

        ListView1.Items.Clear()

        For i = 0 To TABLE.Rows.Count - 1
            With ListView1
                .Items.Add(TABLE.Rows(i)("stud_id"))

                With .Items(.Items.Count - 1).SubItems
                    'Respondent Profile
                    .Add(TABLE.Rows(i)("stud_lname"))
                    .Add(TABLE.Rows(i)("stud_fname"))
                    .Add(TABLE.Rows(i)("stud_mname"))
                    .Add(TABLE.Rows(i)("stud_uname"))
                    .Add(TABLE.Rows(i)("stud_pass"))
                    .Add(TABLE.Rows(i)("stud_age"))
                    .Add(TABLE.Rows(i)("stud_address"))
                    .Add(TABLE.Rows(i)("stud_Gender"))
                End With
            End With
        Next

    End Sub
 
    '**************STUDENT DELETE****************
    Public Sub studdelete()
        If name2 = Nothing Then
            MsgBox("Please choose a record to delete.", MsgBoxStyle.Exclamation)
        Else
            Dim result As Integer = MessageBox.Show("Do you want to delete this column with Name: " + ListView1.SelectedItems(0).Text + "?", "caption", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                Try

                    Dim str As String = "DELETE from tbl_addstud1 where stud_id = '" + ListView1.SelectedItems(0).Text + "'"
                    Dim da As New SqlDataAdapter(str, con)
                    Dim ds As New DataSet
                    da.Fill(ds, "db_sis")
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
                MsgBox("Information Deleted!")
            End If

            student_output()

        End If
    End Sub
    '**************STUDENT NEW INFORMATION****************
    Public Sub studnewinfo()
        If txbx_euname.Text = "" Or
            txbx_epass.Text = "" Or
            txbx_elname.Text = "" Or
            txbx_efname.Text = "" Or
            txbx_emname.Text = "" Or
            cmbx_emonth.Text = "" Or
            cmbx_afbdate.Text = "" Or
            cmbx_afbyear.Text = "" Or
            txbx_eage.Text = "" Or
            cmbx_afgender.Text = "" Or
            txbx_eaddress.Text = "" Or
            txbx_eemail.Text = "" Or
            txbx_ecstatus.Text = "" Or
            txbx_efathern.Text = "" Or
            txbx_efocc.Text = "" Or
            txbx_emothern.Text = "" Or
            txbx_emocc.Text = "" Or
            txbx_eguard.Text = "" Or
            txbx_egocc.Text = "" Or
            txbx_egrela.Text = "" Or
            txbx_esiblings.Text = "" Then
            MsgBox("Fill up all info!")
        Else

            cmd = New SqlCommand("Update tbl_addstud1 Set stud_uname  = '" + txbx_euname.Text + "', stud_pass = '" + txbx_epass.Text + "', stud_lname = '" + txbx_elname.Text + "', stud_fname = '" + txbx_efname.Text + "', stud_mname = '" + txbx_emname.Text + "', stud_bmonth = '" + cmbx_emonth.Text + "', stud_bdate = '" + cmbx_afbdate.Text + "', stud_byear = '" + cmbx_afbyear.Text + "', stud_age = '" + txbx_eage.Text + "', stud_gender = '" + cmbx_afgender.Text + "', stud_address  = '" + txbx_eaddress.Text + "', stud_email = '" + txbx_eemail.Text + "', stud_cstatus  = '" + txbx_ecstatus.Text + "', stud_father = '" + txbx_efathern.Text + "', stud_focc = '" + txbx_efocc.Text + "', stud_mother = '" + txbx_emothern.Text + "', stud_mocc = '" + txbx_emocc.Text + "', stud_guardian  = '" + txbx_eguard.Text + "', stud_gocc = '" + txbx_egocc.Text + "', stud_grela = '" + txbx_egrela.Text + "', stud_siblings = '" + txbx_esiblings.Text + "'", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "db_sis")
            MsgBox("Save")
            student_output()
            studclear()
            TabPage5.Show()
        End If
    End Sub
    '**************STUDENT EDIT****************
    Public Sub studedit()
        If name2 = Nothing Then
            MsgBox("Please choose a record to edit.", MsgBoxStyle.Exclamation)
        Else
            TabControl1.SelectTab(0)
            TabControl2.SelectTab(1)
            TabPage6.Enabled = True
            Dim cmd As SqlCommand
            Dim query As String = "SELECT * FROM tbl_addstud1 WHERE stud_id='" + ListView1.SelectedItems(0).Text + "'"
            cmd = New SqlCommand(query, con)
            Try
                con.Open()
                Dim myreader As SqlDataReader = cmd.ExecuteReader()
                If myreader.Read() Then
                    lbl_sid.Text = myreader.GetValue(0)
                    txbx_euname.Text = myreader.GetValue(1)
                    txbx_epass.Text = myreader.GetValue(2)
                    txbx_elname.Text = myreader.GetValue(3)
                    txbx_efname.Text = myreader.GetValue(4)
                    txbx_emname.Text = myreader.GetValue(5)
                    cmbx_emonth.Text = myreader.GetValue(6)
                    cmbx_afbdate.Text = myreader.GetValue(7)
                    cmbx_afbyear.Text = myreader.GetValue(8)
                    txbx_eage.Text = myreader.GetValue(9)
                    cmbx_afgender.Text = myreader.GetValue(10)
                    txbx_eaddress.Text = myreader.GetValue(11)
                    txbx_eemail.Text = myreader.GetValue(12)
                    txbx_ecstatus.Text = myreader.GetValue(13)
                    txbx_efathern.Text = myreader.GetValue(14)
                    txbx_efocc.Text = myreader.GetValue(15)
                    txbx_emothern.Text = myreader.GetValue(16)
                    txbx_emocc.Text = myreader.GetValue(17)
                    txbx_eguard.Text = myreader.GetValue(18)
                    txbx_egocc.Text = myreader.GetValue(19)
                    txbx_egrela.Text = myreader.GetValue(20)
                    txbx_esiblings.Text = myreader.GetValue(21)
                End If
                myreader.Close()
            Catch ex As System.Exception
                MsgBox(ex.Message)
            End Try
            name2 = Nothing
        End If
        student_output()
        con.Close()
    End Sub
    '**************SEARCH STUDENT****************
    Public Sub studsearch()
        Dim itemcoll(100) As String

        ListView1.Clear()
        ListView1.View = View.Details

        Dim strQ As String = String.Empty
        'strQ = "SELECT * FROM tblInfo"
        strQ = "SELECT * FROM tbl_addstud1 where stud_uname LIKE '" & txbx_studsearch.Text & "%' or stud_pass LIKE '" & txbx_studsearch.Text & "%' or stud_lname LIKE '" & txbx_studsearch.Text & "%' or stud_fname LIKE '" & txbx_studsearch.Text & "%' or stud_mname LIKE '" & txbx_studsearch.Text & "%' or stud_address LIKE '" & txbx_studsearch.Text & "%' or stud_email LIKE '" & txbx_studsearch.Text & "%'"
        cmd = New SqlCommand(strQ, con)
        da = New SqlDataAdapter(cmd)
        ds = New DataSet
        da.Fill(ds, "db_sis")
        Dim i As Integer = 0
        Dim j As Integer = 0
        ' adding the columns in ListView
        For i = 0 To ds.Tables(0).Columns.Count - 1
            Me.ListView1.Columns.Add(ds.Tables(0).Columns(i).ColumnName.ToString())
        Next
        'Now adding the Items in Listview
        For i = 0 To ds.Tables(0).Rows.Count - 1
            For j = 0 To ds.Tables(0).Columns.Count - 1
                itemcoll(j) = ds.Tables(0).Rows(i)(j).ToString()
            Next
            Dim lvi As New ListViewItem(itemcoll)
            Me.ListView1.Items.Add(lvi)
        Next

        con.Close()
    End Sub
    '*************CLEAR TEXTBOX*****************
    Public Sub facclear()
        txbx_afuname.Clear()
        txbx_afpass.Clear()
        txbx_aflname.Clear()
        txbx_affname.Clear()
        txbx_afmname.Clear()
        txbx_afage.Clear()
        txbx_afaddress.Clear()
        txbx_afemail.Clear()
        txbx_afcstatus.Clear()
        txbx_religion.Clear()
    End Sub
    '*************ADD FACULTY*****************
    Public Sub admin_addfac()
            cmd = New SqlCommand("Insert Into tbl_addfac (fac_uname, fac_pass, fac_lname, fac_fname, fac_mname, fac_bmonth, fac_bdate, fac_byear, fac_age, fac_gender, fac_address, fac_email, fac_cstatus, fac_religion) Values ('" + txbx_afuname.Text + "', '" + txbx_afpass.Text + "','" + txbx_aflname.Text + "', '" + txbx_affname.Text + "', '" + txbx_afmname.Text + "', '" + cmbx_afbmonth.Text + "', '" + cmbx_afbdate.Text + "', '" + cmbx_afbyear.Text + "', '" + txbx_afage.Text + "', '" + cmbx_afgender.Text + "', '" + txbx_afaddress.Text + "', '" + txbx_afemail.Text + "', '" + txbx_afcstatus.Text + "', '" + txbx_religion.Text + "')", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "db_sis")
            MsgBox("Successful")
            facclear()
            TabPage2.Show()
    End Sub
    '**************FACULTY OUTPUT****************
    Public Sub faculty_output()
        Dim str As String = "Select * From tbl_addfac"
        Dim cmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim TABLE As New DataTable
        Dim i As Integer

        With cmd
            .CommandText = str
            .Connection = con
        End With

        With da
            .SelectCommand = cmd
            .Fill(TABLE)
        End With

        ListView2.Items.Clear()

        For i = 0 To TABLE.Rows.Count - 1
            With ListView2
                .Items.Add(TABLE.Rows(i)("fac_id"))

                With .Items(.Items.Count - 1).SubItems
                    'Respondent Profile
                    .Add(TABLE.Rows(i)("fac_lname"))
                    .Add(TABLE.Rows(i)("fac_fname"))
                    .Add(TABLE.Rows(i)("fac_mname"))
                    .Add(TABLE.Rows(i)("fac_uname"))
                    .Add(TABLE.Rows(i)("fac_pass"))
                    .Add(TABLE.Rows(i)("fac_age"))
                    .Add(TABLE.Rows(i)("fac_address"))
                    .Add(TABLE.Rows(i)("fac_Gender"))
                End With
            End With
        Next

    End Sub
    '**************FACULTY EDIT****************
    Public Sub facedit()
        TabControl1.SelectTab(1)
        TabControl3.SelectTab(1)
        If name2 = Nothing Then
            MsgBox("Please choose a record to edit.", MsgBoxStyle.Exclamation)
        Else
            TabPage7.Enabled = True
            Dim cmd As SqlCommand
            Dim query As String = "SELECT * FROM tbl_addfac WHERE fac_id='" + ListView2.SelectedItems(0).Text + "'"
            cmd = New SqlCommand(query, con)
            Try
                con.Open()
                Dim myreader As SqlDataReader = cmd.ExecuteReader()
                If myreader.Read() Then
                    lbl_facid.Text = myreader.GetValue(0)
                    txbx_feuname.Text = myreader.GetValue(1)
                    txbx_fepass.Text = myreader.GetValue(2)
                    txbx_felname.Text = myreader.GetValue(3)
                    txbx_fefname.Text = myreader.GetValue(4)
                    txbx_femname.Text = myreader.GetValue(5)
                    cmbx_femonth.Text = myreader.GetValue(6)
                    cmbx_fedate.Text = myreader.GetValue(7)
                    cmbx_feyear.Text = myreader.GetValue(8)
                    txbx_feage.Text = myreader.GetValue(9)
                    cmbx_fegender.Text = myreader.GetValue(10)
                    txbx_feaddress.Text = myreader.GetValue(11)
                    txbx_feemail.Text = myreader.GetValue(12)
                    txbx_fecstatus.Text = myreader.GetValue(13)
                    txbx_fereligion.Text = myreader.GetValue(14)
                End If
                myreader.Close()
            Catch ex As System.Exception
                MsgBox(ex.Message)
            End Try
            name2 = Nothing
        End If
        student_output()
        con.Close()
    End Sub
    '**************FACULTY NEW INFORMATION****************
    Public Sub facnewinfo()
        If txbx_euname.Text = "" Or
            txbx_fepass.Text = "" Or
            txbx_felname.Text = "" Or
            txbx_fefname.Text = "" Or
            txbx_femname.Text = "" Or
            cmbx_femonth.Text = "" Or
            cmbx_fedate.Text = "" Or
            cmbx_feyear.Text = "" Or
            txbx_feage.Text = "" Or
            cmbx_fegender.Text = "" Or
            txbx_feaddress.Text = "" Or
            txbx_feemail.Text = "" Or
            txbx_fecstatus.Text = "" Or
            txbx_fereligion.Text = "" Then
            MsgBox("Fill up all info!")
        Else

            cmd = New SqlCommand("Update tbl_addfac Set fac_uname  = '" + txbx_euname.Text + "', fac_pass  = '" + txbx_fepass.Text + "', fac_lname  = '" + txbx_felname.Text + "', fac_fname  = '" + txbx_fefname.Text + "', fac_mname  = '" + txbx_femname.Text + "', fac_bmonth  = '" + cmbx_femonth.Text + "', fac_bdate  = '" + cmbx_fedate.Text + "', fac_byear  = '" + cmbx_feyear.Text + "', fac_age  = '" + txbx_feage.Text + "', fac_gender  = '" + cmbx_fegender.Text + "', fac_address  = '" + txbx_feaddress.Text + "', fac_email = '" + txbx_feemail.Text + "', fac_cstatus = '" + txbx_fecstatus.Text + "', fac_religion  = '" + txbx_fereligion.Text + "'", con)
            da = New SqlDataAdapter(cmd)
            ds = New DataSet()
            da.Fill(ds, "db_sis")
            MsgBox("Save")
            student_output()
            studclear()
            TabPage5.Show()
        End If
    End Sub
    '**************DELETE FACULTY****************
    Public Sub facdelete()
        If name2 = Nothing Then
            MsgBox("Please choose a record to delete.", MsgBoxStyle.Exclamation)
        Else
            Dim result As Integer = MessageBox.Show("Do you want to delete this column with Name: " + ListView2.SelectedItems(0).Text + "?", "caption", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then

                Try

                    Dim str As String = "DELETE from tbl_addfac where fac_id = '" + ListView2.SelectedItems(0).Text + "'"
                    Dim da As New SqlDataAdapter(str, con)
                    Dim ds As New DataSet
                    da.Fill(ds, "db_sis")
                Catch ex As Exception
                    MsgBox(ex.Message)

                End Try
                MsgBox("Information Deleted!")
            End If

            faculty_output()

        End If
    End Sub
    '*************BUTTON ADD NEW STUDENT*****************
    Private Sub btn_submitstud_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_submitstud.Click
        admin_addstud()
        student_output()
    End Sub
    '**************PRINT STUDENT AND FACULTY ****************
    Private Sub admin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        student_output()
        faculty_output()
    End Sub
    '**************CLICK****************
    Private Sub ListView1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        name2 = ListView1.SelectedItems(0).Text
    End Sub
    '**************CLICK****************
    Private Sub ListView2_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        name2 = ListView2.SelectedItems(0).Text
    End Sub
    '*************BUTTON ADD NEW FACULTY*****************
    Private Sub btn_submitfac_Click_4(sender As System.Object, e As System.EventArgs) Handles btn_submitfac.Click
        admin_addfac()
        faculty_output()
    End Sub
    '**************SAVE STUDENT NEW INFORMATION****************
    Private Sub btn_esubmit_Click_1(sender As System.Object, e As System.EventArgs) Handles btn_esubmit.Click
        studnewinfo()
    End Sub
    '*************BUTTON EDIT INFORMATION*****************
    Private Sub btn_studedit_Click_1(sender As System.Object, e As System.EventArgs) Handles btn_studedit.Click
        studedit()
    End Sub
    '**************CLICK****************
    Private Sub ListView1_MouseClick_1(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseClick
        name2 = ListView1.SelectedItems(0).Text
    End Sub
    '**************DELETE STUDENT INFORMATION****************
    Private Sub btn_studdelete_Click(sender As System.Object, e As System.EventArgs) Handles btn_studdelete.Click
        studdelete()
    End Sub
    '**************DELETE FACULTY INFORMTION****************
    Private Sub btn_facdelete_Click_1(sender As System.Object, e As System.EventArgs) Handles btn_facdelete.Click
        facdelete()
    End Sub
    '**************EDIT FACULTY INFORMTION****************
    Private Sub btn_facedit_Click_2(sender As System.Object, e As System.EventArgs) Handles btn_facedit.Click
        facedit()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        TabControl2.Show()
        TabPage6.Hide()
    End Sub
    '**************SEARCH STUDENT****************
    Private Sub txbx_studsearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txbx_studsearch.TextChanged
        studsearch()
    End Sub
    '**************CLICK****************
    Private Sub ListView2_MouseClick_2(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles ListView2.MouseClick
        name2 = ListView2.SelectedItems(0).Text
    End Sub

    Private Sub txbx_facultys_TextChanged(sender As System.Object, e As System.EventArgs) Handles txbx_facultys.TextChanged

    End Sub

    Private Sub btn_studserch_Click(sender As System.Object, e As System.EventArgs) Handles btn_studserch.Click
        Form2.Show()
        Me.Hide()

    End Sub
End Class
