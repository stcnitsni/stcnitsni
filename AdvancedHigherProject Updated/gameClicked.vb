Public Class gameClicked
    Dim back As Button

    'This subroutine asks both players which colour they would like to be and checks to see if two players are signed in
    Sub gameclick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Hides the forms that can send user to gameClicked form
        Form1.Hide()
        accountClicked.Hide()
        viewUserDetailsClicked.Hide()

        'Displays gameClicked form
        Me.Show()
        'Sets the width of the current form
        Me.Width = Form1.formWidth
        'Sets the height of the current form
        Me.Height = Form1.formHeight



        'If 2 users are signed in then:
        If AccountLoginClicked.usersSignedIn = 2 Then


            For numbers = 0 To 1

                'This block of code loops until the player enters black or white
                Do

                    'Only allows the first player to choose their colour
                    If numbers = 0 Then

                        chessGame.playerChoice = InputBox(AccountLoginClicked.users(numbers) & ", what colour would you like to be?")

                    End If


                    'If the first player didn't enter 'black' or 'white' then display an error message
                    If LCase(chessGame.playerChoice) <> "black" AndAlso LCase(chessGame.playerChoice) <> "white" Then

                        MsgBox("I'm sorry but " & LCase(chessGame.playerChoice) & " is not a viable option. Please try again!")

                    End If


                    'This forces the next player deciding to be the colour which is not yet assigned to a player
                    If chessGame.blackPlayer <> "" Then

                        chessGame.playerChoice = "white"


                    ElseIf chessGame.whitePlayer <> "" Then


                        chessGame.playerChoice = "black"

                    End If


                Loop Until LCase(chessGame.playerChoice) = "black" Or LCase(chessGame.playerChoice) = "white"

                If LCase(chessGame.playerChoice) = "black" Then

                    chessGame.blackPlayer = AccountLoginClicked.users(numbers)


                ElseIf LCase(chessGame.playerChoice) = "white" Then


                    chessGame.whitePlayer = AccountLoginClicked.users(numbers)

                End If

            Next
            MsgBox("white: " & chessGame.whitePlayer)
            MsgBox("black: " & chessGame.blackPlayer)


            'Sets colour of form window to saddle brown
            Me.BackColor = Color.SaddleBrown


            Dim nav As Button


            'Creates a button which sends user to createPieces subroutine
            nav = New Button
            Controls.Add(nav)
            nav.BackColor = Color.White
            nav.Location = New Point(468, 360)
            nav.Width = 150
            nav.Height = 50
            nav.Text = "Start Game"
            nav.Visible = True
            AddHandler nav.Click, AddressOf chessGame.createPieces




            'If less than 2 users are signed in then:
        Else


            MsgBox("You do not have two users signed in. Please navigate to the account login section and try again.")


            'Creates a button which sends user to accountclick subroutine so they can sign in more users
            back = New Button
            Controls.Add(back)
            back.BackColor = Color.White
            back.Width = 150
            back.Height = 100
            back.Location = New Point(450, 550)
            back.Text = "Back"
            back.Tag = "Register"
            back.Visible = True
            AddHandler back.Click, AddressOf accountClicked.accountclick


        End If



    End Sub

End Class