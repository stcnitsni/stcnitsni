Public Class RegisterAccountClicked
    Dim createAccount As Button
    Dim back As Button
    Dim delete As Integer = 0
    Public forename As TextBox
    Public surname As TextBox
    Public age As TextBox
    Public username As TextBox
    Public password As TextBox

    'This subroutine displays 5 textboxes and labels. It also checks whether the user has entered an integer into the age textbox and if the username entered doesn't already exist
    Sub RegisterAccountClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides accountClicked form
        accountClicked.Hide()
        'Displays registerAccountClicked form
        Me.Show()
        Me.Width = Form1.formWidth
        Me.Height = Form1.formHeight


        'Creates labels to identify each text box
        Dim forenameLabel As Label
        Dim surnameLabel As Label
        Dim ageLabel As Label
        Dim usernameLabel As Label
        Dim passwordLabel As Label
        'Creates a variable which determines whether the variable entered into age textbox is an integer
        Dim isInteger As Boolean = False



        'If this form has already been loaded then:
        If delete = 1 Then

            'Dispose of all textboxes
            forename.Dispose()
            surname.Dispose()
            age.Dispose()
            username.Dispose()
            password.Dispose()

        End If


        'Displays register textboxes
        forename = New TextBox
        Controls.Add(forename)
        forename.BackColor = Color.White
        forename.Location = New Point(10, 30)
        forename.Visible = True

        surname = New TextBox
        Controls.Add(surname)
        surname.BackColor = Color.White
        surname.Location = New Point(10, 80)
        surname.Visible = True

        age = New TextBox
        Controls.Add(age)
        age.BackColor = Color.White
        age.Location = New Point(10, 130)
        age.Visible = True

        username = New TextBox
        Controls.Add(username)
        username.BackColor = Color.White
        username.Location = New Point(10, 180)
        username.Visible = True

        password = New TextBox
        Controls.Add(password)
        password.BackColor = Color.White
        password.Location = New Point(10, 230)
        password.Visible = True


        'Displays register labels
        forenameLabel = New Label
        Controls.Add(forenameLabel)
        forenameLabel.Location = New Point(10, 15)
        forenameLabel.Text = "Forename:"
        forenameLabel.Visible = True

        surnameLabel = New Label
        Controls.Add(surnameLabel)
        surnameLabel.Location = New Point(10, 65)
        surnameLabel.Text = "Surname:"
        surnameLabel.Visible = True

        ageLabel = New Label
        Controls.Add(ageLabel)
        ageLabel.Location = New Point(10, 115)
        ageLabel.Text = "Age:"
        ageLabel.Visible = True

        usernameLabel = New Label
        Controls.Add(usernameLabel)
        usernameLabel.Location = New Point(10, 165)
        usernameLabel.Text = "Username:"
        usernameLabel.Visible = True


        passwordLabel = New Label
        Controls.Add(passwordLabel)
        passwordLabel.Location = New Point(10, 215)
        passwordLabel.Text = "Password:"
        passwordLabel.Visible = True



        Dim prompt As String

        'Loops Inputbox until user enters Y or N
        Do

            'Asks user if they would like to register an account
            prompt = InputBox("Would you like to register an account? (Y/N)")

            If UCase(prompt) <> "Y" AndAlso UCase(prompt) <> "N" Then

                MsgBox("I'm sorry but " & prompt & " is not a viable option. Please try again!")

            End If

        Loop Until UCase(prompt) = "Y" Or UCase(prompt) = "N"


        If UCase(prompt) = "Y" Then

            'Calls determineSize subroutine in form1 to determine the size of the array of records called details
            Form1.determineSize()


            'Textboxes are set to the values entered by the user into inputboxes
            forename.Text = InputBox("Please enter your forename")
            surname.Text = InputBox("Please enter your surname")

            'Loops until value entered into age textbox is an integer 
            Do


                age.Text = InputBox("Please enter your age")

                'If value entered is a number then:
                If IsNumeric(age.Text) Then

                    'boolean is set to true
                    isInteger = True

                    'If value entered isn't a number then:
                Else

                    'Display error message
                    MsgBox("You have entered your age incorrectly, please enter a number between 1 and 100 and try again")

                End If



            Loop Until isInteger = True


            username.Text = InputBox("Please enter your username")


            Dim check7 As Integer
            Dim count7 As Integer
            Dim countouter7 As Integer = 0
            Dim details(Form1.arrayLength - 1) As Form1.UserDetails
            'Reads user details into details array of records
            Form1.readFromDatabase(details)

            'This block of code loops until variable countouter7 is equal to arraylength
            Do


                'If the username entered is the same as a username which already exists then:
                If username.Text = details(countouter7).username Then

                    'Display error message
                    username.Text = InputBox("I'm sorry but someone already has the username " & username.Text & ". Please enter another username")

                    'Initialise variables check7 and count7
                    check7 = 0
                    count7 = 0

                    'This block of code loops until count7 is equal to arraylength (when the username entered has been checked against all usernames in project_user table)
                    Do

                        'If the new username entered isn't equal to any pre existing username then:
                        If username.Text <> details(count7).username Then

                            'Check7 is increased by 1
                            check7 += 1

                        End If

                        count7 += 1
                    Loop Until count7 = (Form1.arrayLength)

                End If

                countouter7 += 1
            Loop Until countouter7 = (Form1.arrayLength)


            'This if statement is checking if: A, if the username entered was originally the same as another but now is unique. B, If the username entered was originally unique 
            If check7 = Form1.arrayLength Or check7 = 0 Then

                password.Text = InputBox("Please enter your password")

                'Creates a button which sends user to UpdateDatabase subroutine
                createAccount = New Button
                Controls.Add(createAccount)
                createAccount.BackColor = Color.White
                createAccount.Location = New Point(200, 200)
                createAccount.Text = "Create Account:"
                createAccount.Visible = True
                AddHandler createAccount.Click, AddressOf Form1.UpdateDatabase


                'This statement is checking if the username entered both times was the same as another
            ElseIf check7 >= 1 And check7 <= (Form1.arrayLength - 1) Then

                MsgBox("You have failed to enter a unique username, please return to the page prior and try again")

                'Creates a button which sends user to accountclick subroutine
                back = New Button
                Controls.Add(back)
                back.BackColor = Color.White
                back.Location = New Point(200, 200)
                back.Text = "Back"
                back.Tag = "Register"
                back.Visible = True
                AddHandler back.Click, AddressOf accountClicked.accountclick


            End If


        Else

            MsgBox("Returning you to the accounts page now")

            'Creates a button which sends user to accountclick subroutine
            back = New Button
            Controls.Add(back)
            back.BackColor = Color.White
            back.Location = New Point(200, 200)
            back.Text = "Back"
            back.Tag = "Register"
            back.Visible = True
            AddHandler back.Click, AddressOf accountClicked.accountclick


        End If


        'This variable is increased by 1 to indicate that registerAccountClicked form has already been loaded
        delete = 1

    End Sub
End Class