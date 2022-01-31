Public Class ViewDetailsClicked
    Dim returnBtn As Button
    Public userdetailsbox As RichTextBox
    Dim delete As Integer = 0

    'This subroutine displays the users forename, surname, age, username and password
    Sub ViewDetailsClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides viewUserDetailsClicked form
        viewUserDetailsClicked.Hide()
        'Displays viewDetailsClicked form
        Me.Show()
        Me.Width = Form1.formWidth
        Me.Height = Form1.formHeight

        Dim details(Form1.arrayLength - 1) As Form1.UserDetails
        'Reads everything from the project_user table
        Form1.readFromDatabase(details)

        AccountLoginClicked.values = 0


        'If this form has already been loaded:
        If delete = 1 Then

            'Dispose of richtextbox displaying user details
            userdetailsbox.Dispose()

        End If


        'Creates a richtextbox which displays all details about the user signed in
        userdetailsbox = New RichTextBox
        Controls.Add(userdetailsbox)
        userdetailsbox.Width = 150
        userdetailsbox.Height = 200
        userdetailsbox.Location = New Point(500, 100)
        userdetailsbox.Visible = True
        userdetailsbox.Text = ""
        userdetailsbox.AppendText("Forename: " & details(viewUserDetailsClicked.position).forename & vbNewLine & "Surname: " & details(viewUserDetailsClicked.position).surname & vbNewLine & "Age: " & details(viewUserDetailsClicked.position).age & vbNewLine & "Username: " & details(viewUserDetailsClicked.position).username & vbNewLine & "Password: " & details(viewUserDetailsClicked.position).password & vbNewLine)



        'Creates a button which sends user to viewDetailsClick subroutine
        returnBtn = New Button
        Controls.Add(returnBtn)
        returnBtn.BackColor = Color.White
        returnBtn.Location = New Point(550, 325)
        returnBtn.Text = "Back"
        returnBtn.Tag = "Clear Details"
        returnBtn.Visible = True
        AddHandler returnBtn.Click, AddressOf viewUserDetailsClicked.ViewUserDetails


        'This variable is increased by 1 to indicate that accountLoginClicked form has already been loaded
        delete = 1

    End Sub

End Class