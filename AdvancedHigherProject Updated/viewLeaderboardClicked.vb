Public Class ViewLeaderboardClick
    Dim returnBtn As Button
    Dim delete As Integer = 0
    Public gamesPlayed As RichTextBox
    Public gamesWon As RichTextBox
    Public gamesLost As RichTextBox
    Public username As RichTextBox

    'This subroutine sorts the data stored in the leaderboard array in descending order and displays it
    Sub ViewLeaderboardClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides viewUserDetailsClicked form
        viewUserDetailsClicked.Hide()
        'Displays viewLeaderboardClick form
        'Me.Show()
        'Me.Width = Form1.formWidth
        'Me.Height = Form1.formHeight

        Dim leaderboard(Form1.arrayLength - 1) As Form1.leaderboardDetails
        Form1.readLeaderboard(leaderboard)

        AccountLoginClicked.values = 0



        'Variable are used to store lower index of each record
        Dim currentGamesWon As Integer
        Dim currentGamesLost As Integer
        Dim currentGamesPlayed As Integer
        Dim currentUsername As String
        Dim sortposition As Integer


        MsgBox((leaderboard.Length))


        'Leaderboard data is sorted in descending order (based on gamesWon) through the use of insertion sort algorithm
        For numbers = 1 To (leaderboard.Length) - 1
            currentGamesWon = leaderboard(numbers - 1).gamesWon
            currentGamesLost = leaderboard(numbers - 1).gamesLost
            currentGamesPlayed = leaderboard(numbers - 1).gamesPlayed
            currentUsername = leaderboard(numbers - 1).username
            sortposition = numbers

            'Variable are used to store lower index of each record
            While sortposition > 0 AndAlso currentGamesWon > leaderboard(sortposition - 1).gamesWon


                'Records are swapped and sortposition is decreased by 1
                leaderboard(sortposition - 1).gamesWon = leaderboard(sortposition).gamesWon
                leaderboard(sortposition - 1).gamesLost = leaderboard(sortposition).gamesLost
                leaderboard(sortposition - 1).gamesPlayed = leaderboard(sortposition).gamesPlayed
                leaderboard(sortposition - 1).username = leaderboard(sortposition).username
                sortposition = sortposition - 1

            End While

            leaderboard(sortposition - 1).gamesWon = currentGamesWon
            leaderboard(sortposition - 1).gamesLost = currentGamesLost
            leaderboard(sortposition - 1).gamesPlayed = currentGamesPlayed
            leaderboard(sortposition - 1).username = currentUsername

        Next


        'Creates a richtextbox which displays the number of games played by each user
        gamesPlayed = New RichTextBox
        'Controls.Add(gamesPlayed)
        gamesPlayed.Visible = True
        gamesPlayed.Location = New Point(200, 100)
        gamesPlayed.Width = 100
        gamesPlayed.Height = 200
        gamesPlayed.Clear()
        gamesPlayed.AppendText("games played" & vbNewLine)
        gamesPlayed.AppendText("-------------------- " & vbNewLine)


        'Creates a richtextbox which displays the number of games won by each user
        gamesWon = New RichTextBox
        'Controls.Add(gamesWon)
        gamesWon.Visible = True
        gamesWon.Location = New Point(325, 100)
        gamesWon.Width = 100
        gamesWon.Height = 200
        gamesWon.Clear()
        gamesWon.AppendText("games won" & vbNewLine)
        gamesWon.AppendText("-------------------- " & vbNewLine)


        'Creates a richtextbox which displays the number of games lost by each user
        gamesLost = New RichTextBox
        'Controls.Add(gamesLost)
        gamesLost.Visible = True
        gamesLost.Location = New Point(450, 100)
        gamesLost.Width = 100
        gamesLost.Height = 200
        gamesLost.Clear()
        gamesLost.AppendText("games lost" & vbNewLine)
        gamesLost.AppendText("-------------------- " & vbNewLine)


        'Creates a richtextbox which displays the username of each user
        username = New RichTextBox
        'Controls.Add(username)
        username.Visible = True
        username.Location = New Point(575, 100)
        username.Width = 100
        username.Height = 200
        username.Clear()
        username.AppendText("username" & vbNewLine)
        username.AppendText("-------------------- " & vbNewLine)


        'This block of code is repeated for the length of the array
        For numbers = 0 To (Form1.arrayLength - 1)

            gamesPlayed.AppendText(leaderboard(numbers).gamesPlayed & vbNewLine)
            gamesWon.AppendText(leaderboard(numbers).gamesWon & vbNewLine)
            gamesLost.AppendText(leaderboard(numbers).gamesLost & vbNewLine)
            username.AppendText(leaderboard(numbers).username & vbNewLine)

        Next

        'Creates a button which sends user to ViewUserDetails subroutine in viewUserDetailsClicked form
        returnBtn = New Button
        'Controls.Add(returnBtn)
        returnBtn.BackColor = Color.White
        returnBtn.Location = New Point(375, 325)
        returnBtn.Text = "Back"
        returnBtn.Tag = "Clear Leaderboard"
        returnBtn.Visible = True
        AddHandler returnBtn.Click, AddressOf viewUserDetailsClicked.ViewUserDetails

    End Sub
End Class