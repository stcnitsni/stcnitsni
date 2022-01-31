Public Class ViewUserDetailsClicked
    Dim viewDetails As Button
    Dim viewLeaderboard As Button
    Dim updateField As Button
    Dim logInAccount As Button
    Dim logOut As Button
    Dim launchGame As Button
    Public position As Integer = 0


    'This subroutine increase the number of signed in users by 1 and displays 6 buttons (viewDetails, viewLeaderboard, updateField, logInAccount, logOut and launchGame)
    Sub ViewUserDetails(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides the forms that can send user to ViewUserDetailsClicked form
        AccountLoginClicked.Hide()
        ViewDetailsClicked.Hide()
        ViewLeaderboardClick.Hide()
        loggedInAccountClicked.Hide()
        'Displays ViewUserDetailsClicked form
        Me.Show()
        Me.Width = Form1.formWidth
        Me.Height = Form1.formHeight


        'Disposes of button which sent user to viewUserDetailsClick subroutine
        sender.dispose()


        'If the user came from the viewDetailsClick subroutine in the viewDetailsClicked form then:
        If sender.tag = "Clear Details" Then

            'Dispose of the user details richtextbox
            ViewDetailsClicked.userdetailsbox.Dispose()


            'If the user came from the viewLeaderboardClick subroutine in the viewLeaderboardClicked form then:
        ElseIf sender.tag = "Clear Leaderboard" Then

            'Dispose of all the leaderboard richtextboxes
            ViewLeaderboardClick.gamesPlayed.Dispose()
            ViewLeaderboardClick.gamesWon.Dispose()
            ViewLeaderboardClick.gamesLost.Dispose()
            ViewLeaderboardClick.username.Dispose()

        End If



        Dim details(Form1.arrayLength - 1) As Form1.UserDetails
        Form1.readFromDatabase(details)


        'If the user signed in through AccountLoginClick then:
        If AccountLoginClicked.values = 1 Then

            'Users signed in is increased by 1 and users is set to sender.tag
            AccountLoginClicked.usersSignedIn += 1
            AccountLoginClicked.users(AccountLoginClicked.otherpass) = AccountLoginClicked.enteredusername

        End If



        'Repeats block of code for both users
        For numbers = 0 To 1
            'If enteredusername is equal to one of the usernames then:
            If AccountLoginClicked.enteredusername = AccountLoginClicked.users(numbers) Then

                'Repeats block of code until users variable has been compared to every username
                For detailLocation = 0 To (Form1.arrayLength - 1)
                    'If username is equal to a username in project_user table then:
                    If AccountLoginClicked.users(numbers) = details(detailLocation).username Then

                        'Location of username is stored in position variable
                        position = detailLocation

                    End If
                Next


            End If
        Next
        AccountLoginClicked.values = 0


        'Creates a button which sends user to viewDetailsClick subroutine
        viewDetails = New Button
        Controls.Add(viewDetails)
        viewDetails.BackColor = Color.White
        viewDetails.Width = 150
        viewDetails.Height = 150
        viewDetails.Location = New Point(150, 250)
        viewDetails.Text = "Details"
        viewDetails.Visible = True
        AddHandler viewDetails.Click, AddressOf ViewDetailsClicked.ViewDetailsClick

        'Creates a button which sends user to viewLeaderboardClick subroutine
        viewLeaderboard = New Button
        Controls.Add(viewLeaderboard)
        viewLeaderboard.BackColor = Color.White
        viewLeaderboard.Width = 150
        viewLeaderboard.Height = 150
        viewLeaderboard.Location = New Point(450, 250)
        viewLeaderboard.Text = "Leaderboard"
        viewLeaderboard.Visible = True
        AddHandler viewLeaderboard.Click, AddressOf ViewLeaderboardClick.ViewLeaderboardClick

        'Creates a button which sends user to updateField subroutine
        updateField = New Button
        Controls.Add(updateField)
        updateField.BackColor = Color.White
        updateField.Width = 150
        updateField.Height = 150
        updateField.Location = New Point(750, 250)
        updateField.Text = "Update Account"
        updateField.Visible = True
        AddHandler updateField.Click, AddressOf Form1.updateField

        'Creates a button which sends user to accountclick subroutine
        logInAccount = New Button
        Controls.Add(logInAccount)
        logInAccount.BackColor = Color.White
        logInAccount.Width = 150
        logInAccount.Height = 150
        logInAccount.Location = New Point(150, 550)
        logInAccount.Text = "Log in another account"
        logInAccount.Visible = True
        AddHandler logInAccount.Click, AddressOf accountClicked.accountclick

        'Creates a button which sends user to accountclick subroutine
        logOut = New Button
        Controls.Add(logOut)
        logOut.BackColor = Color.White
        logOut.Width = 150
        logOut.Height = 150
        logOut.Location = New Point(450, 550)
        logOut.Text = "Log Out"
        logOut.Tag = (AccountLoginClicked.usersSignedIn - 1)
        logOut.Visible = True
        AddHandler logOut.Click, AddressOf accountClicked.accountclick


        'If 2 users are signed in then:
        If AccountLoginClicked.usersSignedIn = 2 Then


            'Creates a button which sends user to gameclick subroutine
            launchGame = New Button
            Controls.Add(launchGame)
            launchGame.BackColor = Color.White
            launchGame.Width = 150
            launchGame.Height = 150
            launchGame.Location = New Point(750, 550)
            launchGame.Text = "Launch Game"
            launchGame.Visible = True
            AddHandler launchGame.Click, AddressOf gameClicked.gameclick


        End If

    End Sub

End Class