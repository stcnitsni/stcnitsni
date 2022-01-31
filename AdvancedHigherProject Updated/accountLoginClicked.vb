Public Class AccountLoginClicked
    Dim username As TextBox
    Dim password As TextBox
    Dim usernameLabel As Label
    Dim passwordLabel As Label
    Dim delete As Integer = 0
    Public users(1) As String
    Public usersSignedIn As Integer = 0
    Public otherpass As Integer = -1
    Public values As Integer = 0
    Public enteredusername As String = ""

    'This subroutine determines whether less than 2 users are signed in and displays 2 textboxes and labels. It also compares the entered username to all usernames stored and checks if the password entered is correct 
    Sub AccountLoginClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides accountClicked form
        accountClicked.Hide()
        'Shows accountLoginClicked form
        Me.Show()
        Me.Width = Form1.formWidth
        Me.Height = Form1.formHeight



        'If this form has already been loaded then:
        If delete = 1 Then

            'Dispose of textboxes and labels
            username.Dispose()
            password.Dispose()
            usernameLabel.Dispose()
            passwordLabel.Dispose()

        End If


        'if less than 2 users are signed in then:
        If usersSignedIn < 2 Then

            Dim details(Form1.arrayLength - 1) As Form1.UserDetails
            Form1.readFromDatabase(details)

            'Displays login textboxes and labels
            username = New TextBox
            Controls.Add(username)
            username.BackColor = Color.White
            username.Location = New Point(300, 150)
            username.Visible = True

            password = New TextBox
            Controls.Add(password)
            password.BackColor = Color.White
            password.Location = New Point(300, 300)
            password.Visible = True

            usernameLabel = New Label
            Controls.Add(usernameLabel)
            usernameLabel.Location = New Point(300, 130)
            usernameLabel.Text = "Username:"
            usernameLabel.Visible = True

            passwordLabel = New Label
            Controls.Add(passwordLabel)
            passwordLabel.Location = New Point(300, 280)
            passwordLabel.Text = "Password:"
            passwordLabel.Visible = True


            'Variables used to determine if username entered exists and if password entered belongs to that username
            Dim enteredpassword As String = ""
            Dim counter As Integer = 0
            Dim outercounter As Integer = 0
            Dim accepted As Boolean = False
            Dim found As Boolean = False


            'This block of code is repeated until found is true orElse the code has checked entered username against all usernames in the table
            Do

                username.Text = InputBox("Please enter your username (case sensitive).")

                'This block of code is repeated until found is true orElse the code has checked entered username against all usernames in the table
                Do

                    'If username entered by user exists then:
                    If username.Text = details(counter).username Then

                        'Found is set to true and the position in which the username exists is stored to position variable (for password checker)
                        found = True
                        viewUserDetailsClicked.position = counter

                    End If
                    'Increments for loop
                    counter += 1

                Loop Until counter = Form1.arrayLength OrElse found = True
                counter = 0
                outercounter += 1

                'if username isn't found then:
                If found = False Then

                    'Error message
                    MsgBox("I'm sorry, but there is no account linked to that username. Please try again")

                    'if username is found then:
                Else

                    'Set username textbox to entered username
                    enteredusername = username.Text

                End If

            Loop Until outercounter = Form1.arrayLength OrElse found = True


            'if username exists then:
            If found = True Then



                'This block of code is repeated until password is found or until counter is equal to arraylength
                Do

                    'Password textbox is set to what user enters
                    password.Text = InputBox("Please enter your password (case sensitive).")


                    'If entered password is found at same location of entered username then:
                    If password.Text = details(viewUserDetailsClicked.position).password Then

                        'Set accepted to true and display message letting user know that the password they entered was correct
                        MsgBox("Password Accepted")
                        accepted = True
                        enteredpassword = password.Text

                        'If entered password is not found at same location of entered username then:
                    Else

                        'Displays error message and clears password textbox
                        enteredpassword = " "
                        MsgBox("Error! Incorrect password")

                    End If

                    counter += 1
                Loop Until accepted = True Or counter = Form1.arrayLength


                'If entered password is found then:
                If accepted = True Then

                    'If enteredusername is the same as a user already signed in then:
                    If enteredusername = users(0) Or enteredusername = users(1) Then

                        'Display message letting the user know that they are already signed in
                        MsgBox("You are already signed in!")


                        'Creates a button which sends user to accountclick subroutine in accountClicked form
                        Dim back As Button
                        back = New Button
                        Controls.Add(back)
                        back.BackColor = Color.White
                        back.Location = New Point(500, 250)
                        back.Text = "Back"
                        back.Tag = "Back"
                        back.Visible = True
                        AddHandler back.Click, AddressOf accountClicked.accountclick

                    Else
                        'Sets values variable to 1 and increases otherpass variable by 1
                        'Variable values is used to determine whether the user has signed in (came from accountLoginClick) or is already signed in (came from loggedInAccounts)
                        values = 1
                        'Variable otherpass is used to dictate which user is user(0) and which is user(1)
                        otherpass += 1


                        'Creates a button which sends user to viewUserDetails subroutine in viewUserDetailsClicked form
                        Dim login As Button
                        login = New Button
                        Controls.Add(login)
                        login.BackColor = Color.White
                        login.Location = New Point(500, 250)
                        login.Text = "Login"
                        login.Tag = enteredusername
                        login.Visible = True
                        AddHandler login.Click, AddressOf viewUserDetailsClicked.ViewUserDetails


                    End If

                    'If entered password hasn't been found then:
                Else

                    'Display error message
                    MsgBox("You have failed to enter the correct password, please return to the accounts menu and try again.")



                    'Creates a button which sends user to accountclick subroutine in accountClicked form
                    Dim back As Button
                    back = New Button
                    back.BackColor = Color.White
                    Controls.Add(back)
                    back.Location = New Point(500, 250)
                    back.Text = "Back"
                    back.Visible = True
                    AddHandler back.Click, AddressOf accountClicked.accountclick

                End If
            End If


            'if 2 users are signed in then:
        Else

            'Display message telling user that 2 accounts are already signed in
            MsgBox("There is already 2 users signed in, please sign out of an account and try again")

            'Creates a button which sends user to accountclick subroutine in accountClicked form
            Dim back As Button
            back = New Button
            back.BackColor = Color.White
            Controls.Add(back)
            back.Location = New Point(500, 250)
            back.Text = "Back"
            back.Visible = True
            AddHandler back.Click, AddressOf accountClicked.accountclick

        End If


        'This variable is increased by 1 to indicate that accountLoginClicked form has already been loaded
        delete = 1

    End Sub
End Class
