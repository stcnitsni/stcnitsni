Imports MySql.Data.MySqlClient
Public Class Form1
    ReadOnly connectionString As String = "server=localhost;user=root;password=;database=project;"
    ReadOnly connection As MySqlConnection = New MySqlConnection(connectionString)
    Dim command As MySqlCommand
    Dim leaderboardCommand As MySqlCommand
    Public arrayLength As Integer
    Dim game As Button
    Dim account As Button
    Dim createAccount As Button
    Dim back As Button
    Dim forename As TextBox
    Dim surname As TextBox
    Dim age As TextBox
    Dim username As TextBox
    Dim password As TextBox
    Dim returnBtn As Button
    Public formHeight As Integer = Screen.PrimaryScreen.Bounds.Height
    Public formWidth As Integer = Screen.PrimaryScreen.Bounds.Width

    Structure UserDetails

        Dim forename As String
        Dim surname As String
        Dim age As Integer
        Dim username As String
        Dim password As String

    End Structure

    Structure leaderboardDetails

        Dim username As String
        Dim gamesPlayed As Integer
        Dim gamesWon As Integer
        Dim gamesLost As Integer

    End Structure

    'This subroutine displays 2 buttons (game and account)
    Public Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        'Sets the width of the current form
        Me.Width = formWidth
        'Sets the height of the current form
        Me.Height = formHeight


        'Creates a button which sends user to gameclick subroutine in gameClicked form
        game = New Button
        Controls.Add(game)
        game.BackColor = Color.White
        game.Location = New Point(150, 225)
        game.Width = 100
        game.Height = 50
        game.Visible = True
        game.Text = "Game"
        AddHandler game.Click, AddressOf gameClicked.gameclick


        'Creates a button which sends user to accountClick subroutine in accountClicked form
        account = New Button
        Controls.Add(account)
        account.BackColor = Color.White
        account.Location = New Point(450, 225)
        account.Width = 100
        account.Height = 50
        account.Visible = True
        account.Text = "Account"
        AddHandler account.Click, AddressOf accountClicked.accountclick


        determineSize()



        'This block of code determines which player forfeit the game
        If sender.text = "Quit Game" Then
            'Hides the chess game form
            chessGame.Hide()

            Dim leaderboard(arrayLength - 1) As Form1.leaderboardDetails
            readLeaderboard(leaderboard)
            Dim decider As Integer
            Dim leaderboardCount As Integer = 0
            Dim userlocation As Integer
            Dim turnColour As Color = chessGame.validateChoice


            For numbers = 0 To 1

                leaderboardCount = 0
                Do

                    If AccountLoginClicked.users(numbers) = leaderboard(leaderboardCount).username Then

                        decider = numbers
                        userlocation = leaderboardCount
                        updateLeaderboard(leaderboard, decider, userlocation, turnColour)

                    End If


                    leaderboardCount += 1
                Loop Until leaderboardCount = arrayLength OrElse AccountLoginClicked.users(numbers) = leaderboard(leaderboardCount).username


            Next

        End If


    End Sub


    'This subroutine finds the highest value of userID which allows the program to set the size of any array, involving user details, correctly to prevent any errors occuring
    Sub determineSize()

        'A checker which tells user if the program is having an issue opening a connection to the database
        Try
            connection.Open()
            MsgBox("Connection Opened (determineSize)")

        Catch ex As Exception

            MsgBox("Error opening connection!")
        End Try



        Try
            'This query selects the max userID from the project user table
            command = New MySqlCommand("SELECT MAX(userID) FROM project_user;") With {
                .connection = connection}

            MsgBox("Command Completed! (determineSize)")
            'Variable arrayLength is set to the integer value produced by the query
            arrayLength = command.ExecuteScalar()


        Catch ex As Exception

            MsgBox("Command error!")
        End Try

        'Closes connection between program and database
        connection.Close()


    End Sub



    'This subroutine reads all user data into the array named details
    Sub readFromDatabase(ByRef details() As UserDetails)
        'This variable is used to store the output from the query and is parsed to split each element
        Dim reader(arrayLength - 1) As MySqlDataReader


        'This block of code is repeated a for all users in the table to read all user details into details array of variables
        For numbers = 0 To (arrayLength - 1)
            connection.Open()


            'reads users details from database into reader at index position numbers
            command = New MySqlCommand("SELECT forename, surname, age, username, password FROM project_user WHERE userID = " & (numbers + 1) & ";") With {
                .connection = connection}

            'Output is read into variable reader
            reader(numbers) = command.ExecuteReader()


            'Whilst reader is reading from the database:
            While reader(numbers).Read()

                'forename is parsed from reader and stored at this index position for details.forename
                details(numbers).forename = (reader(numbers).GetString("forename"))


                'surname is parsed from reader and stored at this index position for details.surname
                details(numbers).surname = (reader(numbers).GetString("surname"))


                'age is parsed from reader and stored at this index position for details.age
                details(numbers).age = (reader(numbers).GetInt16("age"))


                'username is parsed from reader and stored at this index position for details.username
                details(numbers).username = (reader(numbers).GetString("username"))


                'password is parsed from reader and stored at this index position for details.password
                details(numbers).password = (reader(numbers).GetString("password"))

            End While

            connection.Close()

        Next

    End Sub



    'This subroutine reads all leaderboard data into the array named leaderboard
    Sub readLeaderboard(ByRef leaderboard() As leaderboardDetails)
        Dim reader(arrayLength - 1) As MySqlDataReader


        For numbers = 0 To (arrayLength - 1)


            connection.Open()

            'reads leaderboard details from database into reader at index position numbers
            command = New MySqlCommand("SELECT username, gamesPlayed, gamesWon, gamesLost FROM leaderboard WHERE userID = " & (numbers + 1) & ";") With {
            .connection = connection}

            reader(numbers) = command.ExecuteReader()


            'Whilst reader is reading from the database:
            While reader(numbers).Read()

                'username is parsed from reader and stored at this index position for leaderboard.username
                leaderboard(numbers).username = (reader(numbers).GetString("username"))


                'gamesPlayed is parsed from reader and stored at this index position for leaderboard.gamesPlayed
                leaderboard(numbers).gamesPlayed = (reader(numbers).GetInt16("gamesPlayed"))


                'gamesWon is parsed from reader and stored at this index position for leaderboard.gamesWon
                leaderboard(numbers).gamesWon = (reader(numbers).GetInt16("gamesWon"))


                'gamesLost is parsed from reader and stored at this index position for leaderboard.gamesLost
                leaderboard(numbers).gamesLost = (reader(numbers).GetInt16("gamesLost"))

            End While

            connection.Close()

        Next

    End Sub



    'This subroutine updates the leaderboard table, altering gamesPlayed, gamesWon and gamesLost
    Sub updateLeaderboard(ByVal leaderboard() As leaderboardDetails, ByVal decider As Integer, ByVal userlocation As Integer, ByVal turncolour As Color)


        Try
            connection.Open()
            MsgBox("Connection Opened (updateLeaderboard)")

        Catch ex As Exception

            MsgBox("Error opening connection!")
        End Try


        Try
            'This query updates the leaderboard table and increases gamesPlayed by 1 for each user playing
            leaderboardCommand = New MySqlCommand("UPDATE `leaderboard` SET gamesPlayed = '" & leaderboard(userlocation).gamesPlayed + 1 & "'  WHERE userID = " & (userlocation + 1) & ";") With {
            .connection = connection}

            'This function executes the query
            leaderboardCommand.ExecuteNonQuery()

            'This indicates whether the query is successful
            MsgBox("Command completed!")

        Catch ex As Exception

            MsgBox("Command error!")
        End Try

        connection.Close()


        'If game is finished
        If chessGame.lockGame = 1 Then

            'If player is white and it's white's turn (or vice versa) then:
            If (turncolour = Color.White And AccountLoginClicked.users(decider) = chessGame.whitePlayer) Or (turncolour = Color.Black And AccountLoginClicked.users(decider) = chessGame.blackPlayer) Then

                Try
                    connection.Open()
                    MsgBox("Connection Opened (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Error opening connection!")
                End Try


                Try
                    'This query updates the leaderboard table and increases gamesWon by 1 for the player who won (which is determined in the endgame subroutine)
                    leaderboardCommand = New MySqlCommand("UPDATE `leaderboard` SET gamesWon = '" & leaderboard(userlocation).gamesWon + 1 & "'  WHERE userID = " & (userlocation + 1) & ";") With {
                    .connection = connection}

                    leaderboardCommand.ExecuteNonQuery()

                    MsgBox("Command completed! (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Command error!")
                End Try

                connection.Close()

            End If


            'If player is white and it's black's turn (or vice versa) then:
            If (turncolour = Color.White And AccountLoginClicked.users(decider) = chessGame.blackPlayer) Or (turncolour = Color.Black And AccountLoginClicked.users(decider) = chessGame.whitePlayer) Then

                Try
                    connection.Open()
                    MsgBox("Connection Opened (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Error opening connection!")
                End Try


                Try
                    'This query updates the leaderboard table and increases gamesLost by 1 for the player who lost (which is also determined in the endgame subroutine)
                    leaderboardCommand = New MySqlCommand("UPDATE `leaderboard` SET gamesLost = '" & leaderboard(userlocation).gamesLost + 1 & "'  WHERE userID = " & (userlocation + 1) & ";") With {
                    .connection = connection}

                    leaderboardCommand.ExecuteNonQuery()

                    MsgBox("Command completed! (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Command error!")
                End Try

                connection.Close()

            End If


            'If a player forfeit game
        ElseIf chessGame.lockGame = 0 Then


            'If player is white and it's white's turn (or vice versa) then:
            If (turncolour = Color.White And AccountLoginClicked.users(decider) = chessGame.whitePlayer) Or (turncolour = Color.Black And AccountLoginClicked.users(decider) = chessGame.blackPlayer) Then

                Try
                    connection.Open()
                    MsgBox("Connection Opened (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Error opening connection!")
                End Try


                Try
                    leaderboardCommand = New MySqlCommand("UPDATE `leaderboard` SET gamesLost = '" & leaderboard(userlocation).gamesWon + 1 & "'  WHERE userID = " & (userlocation + 1) & ";") With {
                    .connection = connection}

                    leaderboardCommand.ExecuteNonQuery()

                    MsgBox("Command completed! (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Command error!")
                End Try

                connection.Close()

            End If



            'If player is white and it's black's turn (or vice versa) then:
            If (turncolour = Color.White And AccountLoginClicked.users(decider) = chessGame.blackPlayer) Or (turncolour = Color.Black And AccountLoginClicked.users(decider) = chessGame.whitePlayer) Then

                Try
                    connection.Open()
                    MsgBox("Connection Opened (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Error opening connection!")
                End Try


                Try
                    leaderboardCommand = New MySqlCommand("UPDATE `leaderboard` SET gamesWon = '" & leaderboard(userlocation).gamesLost + 1 & "'  WHERE userID = " & (userlocation + 1) & ";") With {
                    .connection = connection}

                    leaderboardCommand.ExecuteNonQuery()

                    MsgBox("Command completed! (updateLeaderboard)")

                Catch ex As Exception

                    MsgBox("Command error!")
                End Try

                connection.Close()

            End If

        End If





    End Sub




    'This subroutine creates a new row in both project_user and leaderboard tables using the information entered by the user in the RegisterAccountClick subroutine
    Sub UpdateDatabase(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sender.dispose()

        Dim details(0) As UserDetails
        'Variable userID is used to provide the details entered a unique identifier in the table
        Dim userID As Integer = 0
        'Variable dummy is used to set all fields in leaderboard (except username and userID) to 0
        Dim dummyVar As Integer = 0

        'userID of new account is set to max userID + 1
        userID = arrayLength + 1

        'Details being written to the database are set to values entered in the registerAccountClick subroutine
        details(0).forename = registerAccountClicked.forename.Text
        details(0).surname = registerAccountClicked.surname.Text
        details(0).age = registerAccountClicked.age.Text
        details(0).username = registerAccountClicked.username.Text
        details(0).password = registerAccountClicked.password.Text



        Try
            connection.Open()
            MsgBox("Connection Opened (UpdateDatabase)")

        Catch ex As Exception

            MsgBox("Error opening connection!")
        End Try

        'Creates row in project_user table
        Try
            'This query inserts the new values into the project_user table
            command = New MySqlCommand("INSERT INTO project_user VALUES('" & userID & "', '" & details(0).forename & "' , '" & details(0).surname & "' , '" & details(0).age & "' , '" & details(0).username & "' , '" & details(0).password & "')") With {
                .connection = connection}

            command.ExecuteNonQuery()

            MsgBox("Command completed! (UpdateDatabase)")

        Catch ex As Exception

            MsgBox("Command error!")
        End Try



        'Creates row in leaderboard table
        Try
            'This query creates a new line in the leaderboard table connected to new the new line in the project_line table
            leaderboardCommand = New MySqlCommand("INSERT INTO leaderboard VALUES('" & details(0).username & "', '" & dummyVar & "', '" & dummyVar & "', '" & dummyVar & "',  '" & userID & "')") With {
                .connection = connection}

            leaderboardCommand.ExecuteNonQuery()

            MsgBox("Command completed! (UpdateDatabase)")

        Catch ex As Exception

            MsgBox("Command error!")
        End Try


        connection.Close()


        'Creates a button which sends user to accountclick subroutine
        createAccount = New Button
        Controls.Add(createAccount)
        createAccount.BackColor = Color.White
        createAccount.Location = New Point(200, 200)
        createAccount.Text = "Log in"
        createAccount.Visible = True
        AddHandler createAccount.Click, AddressOf accountClicked.accountclick

    End Sub



    'This subroutine asks the user to enter the name of a field and what they would like to change the value stored to
    Sub updateField(ByVal sender As System.Object, ByVal e As System.EventArgs)
        viewUserDetailsClicked.Hide()
        Me.Show()
        Me.Width = formWidth
        Me.Height = formHeight
        game.Visible = False
        account.Visible = False


        'Variable choice is used to allow the user to choose which field they would like to update
        Dim choice As String
        'Variable correct determines whether the string entered into choice is the same as changeable fields
        Dim correct As Boolean = False
        'Variable details is used to store the new value stored in field chosen
        Dim details As String = ""
        'Variable fieldName is used to carry the users choice to the SQL query
        Dim fieldName As String

        AccountLoginClicked.values = 0


        'this block of code repeats until the field name entered exists or the user enters back
        Do

            'takes users choice
            choice = InputBox("which field would you like to update? (if you did not intend to click this button, please type in: 'back')")


            'if user chooses to update forename field then:
            If LCase(choice) = "forename" Then

                'details is set to what the user wants to change field to, correct is set to true and fieldName is set to user's choice
                fieldName = choice
                details = InputBox("what are you changing " & choice & " to?")
                correct = True

                'if user chooses to update surname field then:
            ElseIf LCase(choice) = "surname" Then

                fieldName = choice
                details = InputBox("what are you changing " & choice & " to?")
                correct = True

                'if user chooses to update age field then:
            ElseIf LCase(choice) = "age" Then

                fieldName = choice
                details = InputBox("what are you changing " & choice & " to?")


                'if the value entered is a number then:
                If IsNumeric(details) Then

                    correct = True

                    'if the value entered isn't a number then:
                Else

                    'Displays error message
                    MsgBox("Please enter an integer into the inputbox, rather than a string")

                End If

                'if user chooses to update password field then:
            ElseIf LCase(choice) = "password" Then

                fieldName = choice
                details = InputBox("what are you changing " & LCase(choice) & " to?")
                correct = True

                'if user doesn't want to update a field then:
            ElseIf LCase(choice) = "back" Then

                MsgBox("Returning to User Details page now")

                'if user chooses a field name that doesn't exist then:
            Else

                'Display error message
                MsgBox("I'm sorry but " & choice & " is not a viable option")

            End If


        Loop Until correct = True Or LCase(choice) = "back"



        'If the user chooses back, this block of code is skipped
        If correct = True Then

            Dim userID As Integer = 0
            userID = arrayLength + 1


            Try
                connection.Open()
                MsgBox("Connection Opened (updateField)")

            Catch ex As Exception

                MsgBox("Error opening connection!")
            End Try


            Try
                command = New MySqlCommand("UPDATE `project_user` SET `" & LCase(choice) & "` = ('" & details & "')  WHERE userID = " & (viewUserDetailsClicked.position + 1) & ";") With {
                    .connection = connection}

                command.ExecuteNonQuery()

                MsgBox("Command completed! (updateField)")

            Catch ex As Exception

                MsgBox("Command error!")
            End Try

            connection.Close()

        End If



        'Creates a button which sends user to ViewUserDetails subroutine in viewUserDetailsClicked form
        returnBtn = New Button
        Controls.Add(returnBtn)
        returnBtn.BackColor = Color.White
        returnBtn.Location = New Point(500, 250)
        returnBtn.Text = "Back"
        returnBtn.Visible = True
        AddHandler returnBtn.Click, AddressOf viewUserDetailsClicked.ViewUserDetails

    End Sub

End Class
