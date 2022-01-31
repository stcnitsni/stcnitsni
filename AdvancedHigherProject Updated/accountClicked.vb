Public Class accountClicked
    Dim registerAccount As Button
    Dim AccountLogin As Button
    Dim back As Button
    Dim login As Button


    'This subroutine displays four buttons (registerAccount, accountLogin, back and login)
    Sub accountclick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides the forms that can send user to accountClicked form
        Form1.Hide()
        gameClicked.Hide()
        viewUserDetailsClicked.Hide()
        loggedInAccountClicked.Hide()
        registerAccountClicked.Hide()
        AccountLoginClicked.Hide()
        'Displays accountClick form
        Me.Show()
        'Sets width of form window
        Me.Width = Form1.formWidth
        'Sets height of form window
        Me.Height = Form1.formHeight


        'Disposes of button that sent user to this subroutine
        sender.dispose()
        'If a user is signing out of their account
        If sender.text = "Log Out" Then

            'Decrease otherpass and usersSignedIn by 1
            AccountLoginClicked.otherpass -= 1
            AccountLoginClicked.usersSignedIn -= 1
            'Set users(position) to blank to allow another user to sign into their account
            AccountLoginClicked.users(sender.tag) = ""

        End If


        'Creates a button which sends user to RegisterAccountClick subroutine in registerAccountClicked form
        registerAccount = New Button
        Controls.Add(registerAccount)
        registerAccount.BackColor = Color.White
        registerAccount.Location = New Point(150, 225)
        registerAccount.Width = 100
        registerAccount.Height = 50
        registerAccount.Visible = True
        registerAccount.Text = "Register Account"
        AddHandler registerAccount.Click, AddressOf registerAccountClicked.RegisterAccountClick


        'Creates a button which sends user to AccountLoginClick subroutine in accountLoginClicked form
        AccountLogin = New Button
        Controls.Add(AccountLogin)
        AccountLogin.BackColor = Color.White
        AccountLogin.Location = New Point(450, 225)
        AccountLogin.Width = 100
        AccountLogin.Height = 50
        AccountLogin.Visible = True
        AccountLogin.Text = "Log Into Account"
        AddHandler AccountLogin.Click, AddressOf AccountLoginClicked.AccountLoginClick


        'Creates a button which sends user to gameclick subroutine in gameClicked form
        back = New Button
        Controls.Add(back)
        back.BackColor = Color.White
        back.Width = 100
        back.Height = 50
        back.Location = New Point(150, 350)
        back.Text = "Game"
        back.Tag = "Game"
        back.Visible = True
        AddHandler back.Click, AddressOf gameClicked.gameclick


        'If there is a user signed in then:
        If AccountLoginClicked.usersSignedIn <> 0 Then

            'Creates a button which sends user to loggedInAccounts subroutine in loggedInAccountClicked form
            login = New Button
            Controls.Add(login)
            login.BackColor = Color.White
            login.Location = New Point(450, 350)
            login.Width = 100
            login.Height = 50
            login.Text = "View Logged In Accounts"
            login.Visible = True
            AddHandler login.Click, AddressOf loggedInAccountClicked.loggedInAccounts

        End If



    End Sub

End Class