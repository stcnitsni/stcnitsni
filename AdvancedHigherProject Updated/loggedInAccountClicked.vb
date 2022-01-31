Public Class loggedInAccountClicked
    Dim back As Button
    Dim loggedIn As Button
    Dim loggedIn2 As Button

    'This subroutine displays one to two buttons, each with the name of the users signed in and a back button
    Sub loggedInAccounts(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides accountClicked form
        accountClicked.Hide()
        'Displays loggedInAccountClicked form
        Me.Show()
        'Sets width of form window
        Me.Width = Form1.formWidth
        'Sets height of form window
        Me.Height = Form1.formHeight

        'If 1 user is signed in then:
        If AccountLoginClicked.usersSignedIn <> 0 Then

            'Creates a button which sends user to ViewUserDetails subroutine in ViewUserDetailsClicked form (with sender.tag set to their username to allow for the recieving subroutine to determine which user has signed in)
            loggedIn = New Button
            Controls.Add(loggedIn)
            loggedIn.BackColor = Color.White
            loggedIn.Location = New Point(150, 225)
            loggedIn.Width = 100
            loggedIn.Height = 50
            loggedIn.Visible = True
            loggedIn.Text = "Log in " & AccountLoginClicked.users(0)
            loggedIn.Tag = AccountLoginClicked.users(0)
            AddHandler loggedIn.Click, AddressOf viewUserDetailsClicked.ViewUserDetails

            'If 2 users are signed in then: 
            If AccountLoginClicked.usersSignedIn > 1 Then

                'Creates a button which sends user to ViewUserDetails subroutine in ViewUserDetailsClicked form (with sender.tag set to their username to allow for the recieving subroutine to determine which user has signed in)
                loggedIn2 = New Button
                Controls.Add(loggedIn2)
                loggedIn2.BackColor = Color.White
                loggedIn2.Location = New Point(450, 225)
                loggedIn2.Width = 100
                loggedIn2.Height = 50
                loggedIn2.Visible = True
                loggedIn2.Text = "Log in " & AccountLoginClicked.users(1)
                loggedIn2.Tag = AccountLoginClicked.users(1)
                AddHandler loggedIn2.Click, AddressOf viewUserDetailsClicked.ViewUserDetails

            End If
        End If



        'Creates a button which sends user to accountclick subroutine in accountClicked form
        back = New Button
        Controls.Add(back)
        back.BackColor = Color.White
        back.Location = New Point(300, 300)
        back.Width = 100
        back.Height = 50
        back.Visible = True
        back.Tag = "loggedIn"
        back.Text = "Back"
        AddHandler back.Click, AddressOf accountClicked.accountclick



    End Sub

End Class