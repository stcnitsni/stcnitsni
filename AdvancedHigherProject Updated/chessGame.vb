Public Class chessGame
    Dim pieces(7, 7) As PictureBox
    Dim box(7, 7) As PictureBox
    Dim board(7, 7) As ChessBoard
    Dim piece(7, 7) As ChessPieces
    Dim possiblemoves(29) As String
    Dim kingmoves As Integer = 1
    Dim availableMoves As Integer = 0
    Dim PieceLocation As String
    Dim Boardchecker As Integer = 0
    Public validateChoice As Color = Color.White
    Dim counters As Integer = 0
    Public lockGame As Integer = 0
    Dim kingCounter As Integer = 0
    Dim BlackKingLocation As String
    Dim WhiteKingLocation As String
    Public blackPlayer As String
    Public whitePlayer As String
    Public playerChoice As String
    Dim turn As TextBox
    Dim pieceLocationText As TextBox
    Dim boardLocationText As TextBox
    Dim pieceLocationLabel As Label
    Dim boardLocationLabel As Label
    Dim resetGameBtn As Button
    Dim deleteBoard As Integer = 0

    Structure movementCalculator

        Dim IncreasingVertically As Integer
        Dim IncreasingHorizontally As Integer
        Dim DecreasingVertically As Integer
        Dim DecreasingHorizontally As Integer

    End Structure

    'This subroutine displays all chess pieces and assigns values to the piece and board objects
    Sub createPieces(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Upon createPieces subroutine first being called:
        If deleteBoard = 0 Then
            'Hides the forms that can send user to accountClicked form
            Form1.Hide()
            viewUserDetailsClicked.Hide()
            gameClicked.Hide()
            'Shows chessgame form
            Me.Show()
            Me.Width = Form1.formWidth
            Me.Height = Form1.formHeight
        End If

        'If user clicks resetGame button then:
        If deleteBoard = 1 Then

            'Call resetGame subroutine
            resetGame()

        End If


        For row = 0 To 7
            For column = 0 To 7

                'Initalise 2D array of pictureboxes and 2D array of objects
                board(row, column) = New ChessBoard
                piece(row, column) = New ChessPieces
                pieces(row, column) = New PictureBox
                box(row, column) = New PictureBox

            Next
        Next


        'Creates black chess pieces
        For row = 0 To 1
            For column = 0 To 7

                pieces(row, column) = New PictureBox
                Controls.Add(pieces(row, column))
                pieces(row, column).Location = New Point(100 * (column + 0.075), 100 * (row + 0.075))
                pieces(row, column).Width = 75
                pieces(row, column).Height = 75
                pieces(row, column).Visible = True
                pieces(row, column).Tag = row & column


                'Set piece colour attribute to black
                piece(row, column).setPieceColour(Color.Black)
                'Set moved attribute to false
                piece(row, column).setMoved(False)
                'Set occupied attribute to true
                board(row, column).setOccupied(True)


                'If chess piece is in row 1
                If row = 1 Then

                    'Set value attribute to -1 (value for black pawn)
                    piece(row, column).setValue(-1)
                    'Set picturebox image to a black pawn
                    pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_pawn.png")

                    'If chess piece is in row 0
                ElseIf row = 0 Then


                    'If chess piece is in column 0 or 7
                    If column = 0 Or column = 7 Then

                        'Set value attribute to -5 (value for black rook)
                        piece(row, column).setValue(-5)
                        'Set picturebox image to a black rook
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_rook.png")

                        'If chess piece is in column 1 or 6
                    ElseIf column = 1 Or column = 6 Then

                        'Set value attribute to -3 (value for black knight)
                        piece(row, column).setValue(-3)
                        'Set picturebox image to a black knight
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_knight.png")

                        'If chess piece is in column 2 or 5
                    ElseIf column = 2 Or column = 5 Then

                        'Set value attribute to -6 (value for black bishop)
                        piece(row, column).setValue(-6)
                        'Set picturebox image to a black bishop
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_bishop.png")

                        'If chess piece is in column 3
                    ElseIf column = 3 Then

                        'Set value attribute to -10 (value for black queen)
                        piece(row, column).setValue(-10)
                        'Set picturebox image to a black queen
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_queen.png")

                        'If chess piece is in column 4
                    Else

                        'Set value attribute to -15 (value for black king)
                        piece(row, column).setValue(-15)
                        'Set picturebox image to a black king
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\black_king.png")

                    End If

                End If

                'Creates an addhandler which when clicked calls highlightAvailableMoves subroutine
                AddHandler pieces(row, column).Click, AddressOf highlightAvailableMoves

            Next
        Next




        'Creates white chess pieces
        For row = 6 To 7
            For column = 0 To 7

                pieces(row, column) = New PictureBox
                Controls.Add(pieces(row, column))
                pieces(row, column).Location = New Point(100 * (column + 0.075), 100 * (row + 0.075))
                pieces(row, column).Width = 75
                pieces(row, column).Height = 75
                pieces(row, column).Visible = True
                pieces(row, column).Tag = row & column


                'Set piece colour attribute to white
                piece(row, column).setPieceColour(Color.White)
                'Set moved attribute to false
                piece(row, column).setMoved(False)
                'Set occupied attribute to true
                board(row, column).setOccupied(True)


                'If chess piece is in row 6
                If row = 6 Then

                    'Set value attribute to 1 (value for white pawn)
                    piece(row, column).setValue(1)
                    'Set picturebox image to a white pawn
                    pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_pawn.png")

                    'If chess piece is in row 7
                ElseIf row = 7 Then


                    'If chess piece is in column 0 or 7
                    If column = 0 Or column = 7 Then

                        'Set value attribute to 5 (value for white rook)
                        piece(row, column).setValue(5)
                        'Set picturebox image to a white rook
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_rook.png")

                        'If chess piece is in column 1 or 6
                    ElseIf column = 1 Or column = 6 Then

                        'Set value attribute to 3 (value for white knight)
                        piece(row, column).setValue(3)
                        'Set picturebox image to a white knight
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_knight.png")

                        'If chess piece is in column 2 or 5
                    ElseIf column = 2 Or column = 5 Then

                        'Set value attribute to 6 (value for white bishop)
                        piece(row, column).setValue(6)
                        'Set picturebox image to a white bishop
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_bishop.png")

                        'If chess piece is in column 3
                    ElseIf column = 3 Then

                        'Set value attribute to 10 (value for white queen)
                        piece(row, column).setValue(10)
                        'Set picturebox image to a white queen
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_queen.png")

                        'If chess piece is in column 4
                    Else

                        'Set value attribute to 15 (value for white king)
                        piece(row, column).setValue(15)
                        'Set picturebox image to a white king
                        pieces(row, column).Image = Image.FromFile("C:\Users\LeeA1\Documents\ProjectPiecePictures\white_king.png")

                    End If

                End If

                'Creates an addhandler which when clicked calls highlightAvailableMoves subroutine
                AddHandler pieces(row, column).Click, AddressOf highlightAvailableMoves

            Next
        Next



        'Calls createBoard subroutine
        createBoard()
        'Variable used to indicate that this subroutine has already been loaded, used in combination with the resetgame button 
        deleteBoard = 1

    End Sub



    'This subroutine displays the chess board and further assigns values to the board object
    Sub createBoard()
        Me.BackColor = Color.SaddleBrown

        For row = 0 To 7    'creates dimensions of grid
            For column = 0 To 7

                box(row, column) = New PictureBox
                Controls.Add(box(row, column))
                'Sets the board square to white
                box(row, column).BackColor = Color.White
                box(row, column).Location = New Point(100 * column, 100 * row)
                box(row, column).Width = 90
                box(row, column).Height = 90
                box(row, column).Visible = True
                box(row, column).Tag = row & column
                'Sets the colour attribute to white (used to change highlighted squares back to their normal colour)
                board(row, column).setColour(Color.White)
                pieces(row, column).BackColor = Color.White


                If row = 1 Or row = 3 Or row = 5 Or row = 7 Then
                    If column = 1 Or column = 3 Or column = 5 Or column = 7 Then

                        'Sets the board square to grey
                        box(row, column).BackColor = Color.Gray
                        'Sets the colour attribute to grey (used to change highlighted squares back to their normal colour)
                        board(row, column).setColour(Color.Gray)
                        pieces(row, column).BackColor = Color.Gray

                    End If
                End If


                If row = 0 Or row = 2 Or row = 4 Or row = 6 Then
                    If column = 0 Or column = 2 Or column = 4 Or column = 6 Then

                        box(row, column).BackColor = Color.Gray
                        board(row, column).setColour(Color.Gray)
                        pieces(row, column).BackColor = Color.Gray

                    End If
                End If


                'Creates an addhandler which when clicked calls validateMove subroutine
                AddHandler box(row, column).Click, AddressOf validateMove

            Next
        Next



        'Creates a button which calls the createPieces subroutine (which will call the resetGame subroutine as delete = 1) and results in the enter game being reset
        resetGameBtn = New Button
        Controls.Add(resetGameBtn)
        resetGameBtn.BackColor = Color.White
        resetGameBtn.Location = New Point(1100, 200)
        resetGameBtn.Width = 150
        resetGameBtn.Height = 50
        resetGameBtn.Text = "Reset Game"
        resetGameBtn.Visible = True
        AddHandler resetGameBtn.Click, AddressOf Me.createPieces


        Dim quitGameBtn As Button

        'Creates a button which calls the Form1_Load subroutine in form1 
        quitGameBtn = New Button
        Controls.Add(quitGameBtn)
        quitGameBtn.BackColor = Color.White
        quitGameBtn.Location = New Point(1100, 400)
        quitGameBtn.Width = 150
        quitGameBtn.Height = 50
        quitGameBtn.Text = "Quit Game"
        quitGameBtn.Visible = True
        AddHandler quitGameBtn.Click, AddressOf Form1.Form1_Load



        'Creates label for piece location textbox
        pieceLocationLabel = New Label
        Controls.Add(pieceLocationLabel)
        pieceLocationLabel.BackColor = Color.SaddleBrown
        pieceLocationLabel.ForeColor = Color.White
        pieceLocationLabel.Location = New Point(800, 50)
        pieceLocationLabel.Width = 100
        pieceLocationLabel.Height = 50
        pieceLocationLabel.Text = "Piece Location:"
        pieceLocationLabel.Visible = True


        'Creates label for board location textbox
        boardLocationLabel = New Label
        Controls.Add(boardLocationLabel)
        boardLocationLabel.BackColor = Color.SaddleBrown
        boardLocationLabel.ForeColor = Color.White
        boardLocationLabel.Location = New Point(800, 150)
        boardLocationLabel.Width = 100
        boardLocationLabel.Height = 50
        boardLocationLabel.Text = "Board Location:"
        boardLocationLabel.Visible = True


        'Creates piece location textbox which displays location of chess piece clicked
        pieceLocationText = New TextBox
        Controls.Add(pieceLocationText)
        pieceLocationText.BackColor = Color.White
        pieceLocationText.ForeColor = Color.SaddleBrown
        pieceLocationText.Location = New Point(900, 50)
        pieceLocationText.Width = 100
        pieceLocationText.Height = 50
        pieceLocationText.Visible = True


        'Creates board location textbox which displays location of board square clicked
        boardLocationText = New TextBox
        Controls.Add(boardLocationText)
        boardLocationText.BackColor = Color.White
        boardLocationText.ForeColor = Color.SaddleBrown
        boardLocationText.Location = New Point(900, 150)
        boardLocationText.Width = 100
        boardLocationText.Height = 50
        boardLocationText.Visible = True


        'Creates textbox which displays which users turn it is
        turn = New TextBox
        Controls.Add(turn)
        turn.BackColor = Color.SaddleBrown
        turn.ForeColor = Color.White
        turn.Location = New Point(1100, 0)
        turn.Text = whitePlayer
        turn.Width = 100
        turn.Height = 50
        turn.Visible = True


    End Sub



    'This subroutine checks what player's turn it is and....
    Sub highlightAvailableMoves(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim x As Integer
        Dim y As Integer
        Dim choice As Color
        Dim resetColourCounter As Integer = 0
        Dim movement As movementCalculator


        'If chess piece picturebox clicked is white then:
        If piece(Strings.Left(sender.tag, 1), Strings.Right(sender.tag, 1)).getPieceColour() = Color.White Then

            'Choice is set to white
            choice = Color.White

            'If chess piece picturebox clicked is black then:
        ElseIf piece(Strings.Left(sender.tag, 1), Strings.Right(sender.tag, 1)).getPieceColour() = Color.Black Then

            'Choice is set to black
            choice = Color.Black

        End If


        If lockGame = 0 Then


            'If this subroutine has already been called (highlighting available moves) then:
            If Boardchecker = 1 Then
                'Repeat for the number of board squares that are light blue
                Do

                    'If the value of array is not empty then:
                    If possiblemoves(resetColourCounter) <> "" Then


                        'x and y coordinates are split and set equal to separate variables
                        x = Strings.Right(possiblemoves(resetColourCounter), 1)
                        y = Strings.Left(possiblemoves(resetColourCounter), 1)
                        'variables are then used with box 2D array to revert each box square which is light blue back to it's original colour
                        box(y, x).BackColor = board(y, x).getColour()

                        'Increments loop
                        resetColourCounter += 1


                    End If

                Loop Until resetColourCounter = availableMoves
            End If



            'If choice is equal to validate choice then:
            If choice = validateChoice Then



                'Split x and y coordinates
                y = Strings.Left(sender.tag, 1)
                x = Strings.Right(sender.tag, 1)


                'Location of chess piece is passed into pieceLocation
                PieceLocation = sender.tag
                'pieceLocation textbox displays location of chess piece
                pieceLocationText.Text = PieceLocation

                'variables are all set to 0
                movement.IncreasingVertically = 0
                movement.IncreasingHorizontally = 0
                movement.DecreasingVertically = 0
                movement.DecreasingHorizontally = 0
                counters = 0

                'Pawn (if piece clicked has value 1 or -1) 
                If piece(y, x).getValue() = 1 Or piece(y, x).getValue() = -1 Then

                    pawnMovements(movement, x, y)

                End If


                'Knight (if piece clicked has value 3 or -3) 
                If piece(y, x).getValue() = 3 Or piece(y, x).getValue() = -3 Then

                    KnightMovements(movement, x, y)

                End If


                'Rook (if piece clicked has value 5 or -5) 
                If piece(y, x).getValue() = 5 Or piece(y, x).getValue() = -5 Then

                    RookMovements(movement, x, y)

                End If


                'Bishop (if piece clicked has value 6 or -6) 
                If piece(y, x).getValue() = 6 Or piece(y, x).getValue() = -6 Then

                    BishopMovements(movement, x, y)

                End If


                'Queen (if piece clicked has value 10 or -10) 
                If piece(y, x).getValue() = 10 Or piece(y, x).getValue() = -10 Then

                    RookMovements(movement, x, y)
                    BishopMovements(movement, x, y)

                End If


                'King (if piece clicked has value 15 or -15) 
                If piece(y, x).getValue() = 15 Or piece(y, x).getValue() = -15 Then

                    KingMovements(movement, x, y)

                End If


                'This variable is increased by 1 to indicate that chess board has shown available moves
                Boardchecker = 1

            Else

                MsgBox("It is not your turn!")

            End If

        Else

            If choice = Color.White Then

                MsgBox("The game is over " & whitePlayer)

            ElseIf choice = Color.Black Then

                MsgBox("The game is over " & blackPlayer)

            End If

        End If
    End Sub



    'This subroutine identifies and displays the available moves that a pawn can make
    Sub pawnMovements(ByVal movement As movementCalculator, ByVal x As Integer, ByVal y As Integer)
        'This variable dictates how many places a pawn can move forwards
        Dim limit As Integer = 2

        If piece(y, x).getMoved() = True Then

            limit = 1

        End If


        'If pawn is white then:
        If piece(y, x).getValue() = 1 Then


            'If y location is above or equal to 2 then:
            If y >= 2 Then

                'Repeat code until IncreasingVertically equals limit orElse the next board square is occupied
                Do
                    'Increase variable by 1
                    movement.IncreasingVertically += 1

                    'If board square is not occupied then:
                    If board(y - movement.IncreasingVertically, x).getOccupied() = False Then


                        'Increasing Vertically
                        'Change board square colour to light blue and write location to array
                        box(y - movement.IncreasingVertically, x).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y - movement.IncreasingVertically) & x


                        'Increment loop
                        counters += 1
                    End If

                Loop Until movement.IncreasingVertically = limit OrElse board(y - movement.IncreasingVertically, x).getOccupied() = True
                movement.IncreasingVertically = 0


                'If location is not above or equal to 2 then:
            Else


                'Repeat until IncreasingVertically is equal to 0 or IncreasingVertically is equal to limit and the next board square is occupied
                Do
                    movement.IncreasingVertically += 1

                    If board(y - movement.IncreasingVertically, x).getOccupied() = False Then


                        'Increasing Vertically
                        box(y - movement.IncreasingVertically, x).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y - movement.IncreasingVertically) & x


                        counters += 1
                    End If

                Loop Until ((y - movement.IncreasingVertically) = 0 Or movement.IncreasingVertically = limit) OrElse board(y - movement.IncreasingVertically, x).getOccupied() = True
                movement.IncreasingVertically = 0

            End If



            'If y is above or equal to 1 then:
            If y >= 1 Then
                'If x is above or equal to 1 then:
                If x >= 1 Then
                    'If next board square is occupied by a black piece then:
                    If board(y - 1, x - 1).getOccupied() = True And (piece(y - 1, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Then


                        'Change board square colour to light blue and write location to array
                        box(y - 1, x - 1).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y - 1) & (x - 1)


                        counters += 1
                    End If
                End If


                'If x is below or equal to 6 then:
                If x <= 6 Then
                    If board(y - 1, x + 1).getOccupied() = True And (piece(y - 1, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Then


                        box(y - 1, x + 1).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y - 1) & (x + 1)


                        counters += 1
                    End If
                End If
            End If


            'If pawn is black then:
        ElseIf piece(y, x).getValue() = -1 Then



            'If y is below or equal to 5 then:
            If y <= 5 Then

                'Repeat code until DecreasingVertically equals limit orElse the next board square is occupied
                Do
                    'Increase variable by 1
                    movement.DecreasingVertically += 1

                    'If board square is not occupied then:
                    If board(y + movement.DecreasingVertically, x).getOccupied() = False Then


                        'Decreasing Vertically
                        'Change board square colour to light blue and write location to array
                        box(y + movement.DecreasingVertically, x).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y + movement.DecreasingVertically) & x


                        'Increment loop
                        counters += 1
                    End If

                Loop Until movement.DecreasingVertically = limit OrElse board((y + movement.DecreasingVertically), x).getOccupied() = True


                'If location is not below or equal to 5 then:
            Else


                'Repeat until DecreasingVertically is equal to 7 or DecreasingVertically is equal to limit and the next board square is occupied
                Do
                    movement.DecreasingVertically += 1

                    If board(y + movement.DecreasingVertically, x).getOccupied() = False Then


                        'Decreasing Vertically
                        box(y + movement.DecreasingVertically, x).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y + movement.DecreasingVertically) & x


                        counters += 1
                    End If

                Loop Until ((y + movement.DecreasingVertically) = 7 Or movement.DecreasingVertically = limit) OrElse board((y + movement.DecreasingVertically), x).getOccupied() = True

            End If
            movement.DecreasingVertically = 0



            'If y is below or equal to 6 then:
            If y <= 6 Then
                'If x is above or equal to 1 then:
                If x >= 1 Then
                    'If next board square is occupied by a white piece then:
                    If board(y + 1, x - 1).getOccupied() = True And (piece(y + 1, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                        'Change board square colour to light blue and write location to array
                        box(y + 1, x - 1).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y + 1) & (x - 1)


                        counters += 1
                    End If
                End If


                'If x is below or equal to 6 then:
                If x <= 6 Then
                    If board(y + 1, x + 1).getOccupied() = True And (piece(y + 1, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                        box(y + 1, x + 1).BackColor = Color.LightBlue
                        possiblemoves(counters) = (y + 1) & (x + 1)


                        counters += 1
                    End If
                End If
            End If



        End If


        'Size of array is set to number of squares highlighted
        availableMoves = counters

    End Sub



    'This subroutine identifies and displays the available moves that a knight can make
    Sub KnightMovements(ByVal movement As movementCalculator, ByVal x As Integer, ByVal y As Integer)


        If y >= 2 Then
            If x >= 1 Then
                If board(y - 2, x - 1).getOccupied() = False Or (piece(y - 2, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 2, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 2, x - 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 2) & (x - 1)


                    counters += 1
                End If
            End If

            If x <= 6 Then
                If board(y - 2, x + 1).getOccupied() = False Or (piece(y - 2, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 2, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 2, x + 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 2) & (x + 1)


                    counters += 1
                End If
            End If
        End If



        If y <= 5 Then
            If x >= 1 Then
                If board(y + 2, x - 1).getOccupied() = False Or (piece(y + 2, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 2, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 2, x - 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 2) & (x - 1)


                    counters += 1
                End If
            End If

            If x <= 6 Then
                If board(y + 2, x + 1).getOccupied() = False Or (piece(y + 2, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 2, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 2, x + 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 2) & (x + 1)


                    counters += 1
                End If
            End If
        End If



        If x >= 2 Then
            If y >= 1 Then
                If board(y - 1, x - 2).getOccupied() = False Or (piece(y - 1, x - 2).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 1, x - 2).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 1, x - 2).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 1) & (x - 2)


                    counters += 1
                End If
            End If

            If y <= 6 Then
                If board(y + 1, x - 2).getOccupied() = False Or (piece(y + 1, x - 2).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 1, x - 2).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 1, x - 2).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 1) & (x - 2)


                    counters += 1
                End If
            End If
        End If



        If x <= 5 Then
            If y >= 1 Then
                If board(y - 1, x + 2).getOccupied() = False Or (piece(y - 1, x + 2).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 1, x + 2).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 1, x + 2).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 1) & (x + 2)


                    counters += 1
                End If
            End If

            If y <= 6 Then
                If board(y + 1, x + 2).getOccupied() = False Or (piece(y + 1, x + 2).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 1, x + 2).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 1, x + 2).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 1) & (x + 2)


                    counters += 1
                End If
            End If
        End If

        availableMoves = counters

    End Sub



    'This subroutine identifies and displays the available moves that a rook can make
    Sub RookMovements(ByVal movement As movementCalculator, ByVal x As Integer, ByVal y As Integer)

        If y <> 0 Then

            Do


                movement.IncreasingVertically += 1
                If board(y - movement.IncreasingVertically, x).getOccupied() = False Or (piece(y - movement.IncreasingVertically, x).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - movement.IncreasingVertically, x).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'increasing vertically
                    box(y - movement.IncreasingVertically, x).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - movement.IncreasingVertically) & x


                    counters += 1
                End If


            Loop Until (y - movement.IncreasingVertically) = 0 OrElse board(y - movement.IncreasingVertically, x).getOccupied() = True

        End If


        If x <> 0 Then

            Do


                movement.IncreasingHorizontally += 1
                If board(y, x - movement.IncreasingHorizontally).getOccupied() = False Or (piece(y, x - movement.IncreasingHorizontally).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y, x - movement.IncreasingHorizontally).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'increasing horizontally
                    box(y, x - movement.IncreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = y & (x - movement.IncreasingHorizontally)


                    counters += 1
                End If


            Loop Until (x - movement.IncreasingHorizontally) = 0 OrElse board(y, x - movement.IncreasingHorizontally).getOccupied() = True

        End If


        If y <> 7 Then

            Do


                movement.DecreasingVertically += 1
                If board(y + movement.DecreasingVertically, x).getOccupied() = False Or (piece(y + movement.DecreasingVertically, x).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + movement.DecreasingVertically, x).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'decreasing vertically
                    box(y + movement.DecreasingVertically, x).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + movement.DecreasingVertically) & x


                    counters += 1
                End If


            Loop Until (y + movement.DecreasingVertically) = 7 OrElse board(y + movement.DecreasingVertically, x).getOccupied() = True

        End If


        If x <> 7 Then

            Do


                movement.DecreasingHorizontally += 1
                If board(y, x + movement.DecreasingHorizontally).getOccupied() = False Or (piece(y, x + movement.DecreasingHorizontally).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y, x + movement.DecreasingHorizontally).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'decreasing horizontally
                    box(y, x + movement.DecreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = y & (x + movement.DecreasingHorizontally)


                    counters += 1
                End If


            Loop Until (x + movement.DecreasingHorizontally) = 7 OrElse board(y, x + movement.DecreasingHorizontally).getOccupied() = True

        End If

        availableMoves = counters


    End Sub



    'This subroutine identifies and displays the available moves that a bishop can make
    'This subroutine is combined with the RookMovements subroutine to identify and display the available moves that a queen can make
    Sub BishopMovements(ByVal movement As movementCalculator, ByVal x As Integer, ByVal y As Integer)


        If y <> 0 And x <> 0 Then

            Do


                movement.IncreasingHorizontally += 1
                movement.IncreasingVertically += 1
                If board(y - movement.IncreasingVertically, x - movement.IncreasingHorizontally).getOccupied() = False Or (piece(y - movement.IncreasingVertically, x - movement.IncreasingHorizontally).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - movement.IncreasingVertically, x - movement.IncreasingHorizontally).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'increasing horizontally and vertically
                    box(y - movement.IncreasingVertically, x - movement.IncreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - movement.IncreasingVertically) & (x - movement.IncreasingHorizontally)


                    counters += 1
                End If


            Loop Until (y - movement.IncreasingVertically = 0 Or x - movement.IncreasingHorizontally = 0) OrElse board(y - movement.IncreasingVertically, x - movement.IncreasingHorizontally).getOccupied() = True

            movement.IncreasingHorizontally = 0
            movement.IncreasingVertically = 0
        End If


        If y <> 7 And x <> 7 Then

            Do


                movement.DecreasingHorizontally += 1
                movement.DecreasingVertically += 1
                If board(y + movement.DecreasingVertically, x + movement.DecreasingVertically).getOccupied() = False Or (piece(y + movement.DecreasingVertically, x + movement.DecreasingVertically).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + movement.DecreasingVertically, x + movement.DecreasingVertically).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'decreasing horizontally and vertically
                    box(y + movement.DecreasingVertically, x + movement.DecreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + movement.DecreasingVertically) & (x + movement.DecreasingHorizontally)


                    counters += 1
                End If


            Loop Until (y + movement.DecreasingVertically = 7 Or x + movement.DecreasingHorizontally = 7) OrElse board(y + movement.DecreasingVertically, x + movement.DecreasingHorizontally).getOccupied() = True

            movement.DecreasingHorizontally = 0
            movement.DecreasingVertically = 0
        End If


        If y <> 7 And x <> 0 Then

            Do


                movement.IncreasingHorizontally += 1
                movement.DecreasingVertically += 1
                If board(y + movement.DecreasingVertically, x - movement.IncreasingHorizontally).getOccupied() = False Or (piece(y + movement.DecreasingVertically, x - movement.IncreasingHorizontally).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + movement.DecreasingVertically, x - movement.IncreasingHorizontally).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'increasing horizontally and decreasing vertically
                    box(y + movement.DecreasingVertically, x - movement.IncreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + movement.DecreasingVertically) & (x - movement.IncreasingHorizontally)


                    counters += 1
                End If


            Loop Until (y + movement.DecreasingVertically = 7 Or x - movement.IncreasingHorizontally = 0) OrElse board(y + movement.DecreasingVertically, x - movement.IncreasingHorizontally).getOccupied() = True

            movement.IncreasingHorizontally = 0
            movement.DecreasingVertically = 0
        End If


        If y <> 0 And x <> 7 Then

            Do


                movement.DecreasingHorizontally += 1
                movement.IncreasingVertically += 1
                If board(y - movement.IncreasingVertically, x + movement.DecreasingHorizontally).getOccupied() = False Or (piece(y - movement.IncreasingVertically, x + movement.DecreasingHorizontally).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - movement.IncreasingVertically, x + movement.DecreasingHorizontally).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    'increasing vertically and decreasing horizontally
                    box(y - movement.IncreasingVertically, x + movement.DecreasingHorizontally).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - movement.IncreasingVertically) & (x + movement.DecreasingHorizontally)


                    counters += 1
                End If


            Loop Until (y - movement.IncreasingVertically = 0 Or x + movement.DecreasingHorizontally = 7) OrElse board(y - movement.IncreasingVertically, x + movement.DecreasingHorizontally).getOccupied() = True

            movement.DecreasingHorizontally = 0
            movement.IncreasingVertically = 0
        End If

        availableMoves = counters

    End Sub



    'This subroutine identifies and displays the available moves that a king can make
    Sub KingMovements(ByVal movement As movementCalculator, ByVal x As Integer, ByVal y As Integer)

        If y <> 0 Then
            If board(y - 1, x).getOccupied() = False Or (piece(y - 1, x).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 1, x).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                box(y - 1, x).BackColor = Color.LightBlue
                possiblemoves(counters) = (y - 1) & x


                counters += 1
            End If

            If x <> 0 Then
                If board(y - 1, x - 1).getOccupied() = False Or (piece(y - 1, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 1, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 1, x - 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 1) & (x - 1)


                    counters += 1
                End If
            End If
        End If



        If y <> 7 Then
            If board(y + 1, x).getOccupied() = False Or (piece(y + 1, x).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 1, x).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                box(y + 1, x).BackColor = Color.LightBlue
                possiblemoves(counters) = (y + 1) & x


                counters += 1
            End If

            If x <> 7 Then
                If board(y + 1, x + 1).getOccupied() = False Or (piece(y + 1, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 1, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 1, x + 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 1) & (x + 1)


                    counters += 1
                End If
            End If
        End If



        If x <> 0 Then
            If board(y, x - 1).getOccupied() = False Or (piece(y, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                box(y, x - 1).BackColor = Color.LightBlue
                possiblemoves(counters) = y & (x - 1)

                counters += 1
            End If

            If y <> 7 Then
                If board(y + 1, x - 1).getOccupied() = False Or (piece(y + 1, x - 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y + 1, x - 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y + 1, x - 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y + 1) & (x - 1)


                    counters += 1
                End If
            End If
        End If



        If x <> 7 Then
            If board(y, x + 1).getOccupied() = False Or (piece(y, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                box(y, x + 1).BackColor = Color.LightBlue
                possiblemoves(counters) = y & (x + 1)


                counters += 1
            End If

            If y <> 0 Then
                If board(y - 1, x + 1).getOccupied() = False Or (piece(y - 1, x + 1).getPieceColour() = Color.Black And piece(y, x).getPieceColour() = Color.White) Or (piece(y - 1, x + 1).getPieceColour() = Color.White And piece(y, x).getPieceColour() = Color.Black) Then


                    box(y - 1, x + 1).BackColor = Color.LightBlue
                    possiblemoves(counters) = (y - 1) & (x + 1)


                    counters += 1
                End If
            End If
        End If

        availableMoves = counters



    End Sub



    'This subroutine determines whether the move chosen by the player is legal
    Sub validateMove(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If a chess piece is clicked on first then:
        If Boardchecker = 1 Then


            Dim legal As Boolean = False
            Dim x As Integer
            Dim y As Integer
            Dim GridLocationSelected As String = sender.tag
            boardLocationText.Text = GridLocationSelected


            counters = 0
            'Repeat until legal is equal to true or counters is equal to number of board squares highlighted
            Do


                'If the board square clicked on is an available move then:
                If sender.tag = possiblemoves(counters) Then

                    'Legal is set to true
                    legal = True

                End If

                'Increment loop
                counters += 1
            Loop Until legal = True OrElse counters = availableMoves


            counters = 0
            'Repeat until counters is equal to number of board squares highlighted
            Do

                'x and y coordinates are split and set equal to separate variables
                x = Strings.Right(possiblemoves(counters), 1)
                y = Strings.Left(possiblemoves(counters), 1)
                'variables are then used with box 2D array to revert each box square which is light blue back to it's original colour
                box(y, x).BackColor = board(y, x).getColour()

                'Increments loop
                counters += 1
            Loop Until counters = availableMoves



            'If legal is true:
            If legal = True Then

                'Display message box telling the user that the move they chose is legal
                MsgBox("Legal move")
                'Call movePiece subroutine and pass in location of board square clicked
                movePiece(GridLocationSelected)


                'If legal is not true:
            Else


                'Display message box telling the user that the move they chose is illegal
                MsgBox("Illegal move")

            End If


            'Set boardchecker to 0 as to prevent execution error from occuring in highlightAvailableMoves subroutine (where board square colours are changed back to their original colours)
            Boardchecker = 0



            'Calls endgame subroutine
            endgame(legal)

        End If
    End Sub



    'If the move chosen by the player is legal, then this subroutine updates the location of the piece moved (and if an opposing piece is being taken it is disposed of)
    Sub movePiece(ByVal GridLocationSelected As String)

        'Set variable equal to x position of board square
        Dim xx As String = Strings.Right(GridLocationSelected, 1)
        'Set variable equal to y position of board square
        Dim yy As String = Strings.Left(GridLocationSelected, 1)

        'Set variable equal to x position of chess piece
        Dim move_x As String = Strings.Right(PieceLocation, 1)
        'Set variable equal to y position of chess piece
        Dim move_y As String = Strings.Left(PieceLocation, 1)



        'if piece clicked is white then:
        If piece(move_y, move_x).getPieceColour() = Color.White Then

            'it becomes black players turn
            validateChoice = Color.Black
            'turn textbox displays black players username
            turn.Text = blackPlayer

            'if piece clicked is black then:
        ElseIf piece(move_y, move_x).getPieceColour() = Color.Black Then

            'it becomes white players turn
            validateChoice = Color.White
            'turn textbox displays white players username
            turn.Text = whitePlayer

        End If


        'If board square chosen is occupied then:
        If board(yy, xx).getOccupied() = True Then

            'Piece on chosen square inherits all properties of chosen piece
            box(yy, xx).BackColor = board(yy, xx).getColour()
            piece(yy, xx).setValue(piece(move_y, move_x).getValue())
            piece(yy, xx).setMoved(True)
            piece(yy, xx).setPieceColour(piece(move_y, move_x).getPieceColour())
            board(yy, xx).setOccupied(True)
            pieces(yy, xx).Tag = yy & xx
            pieces(yy, xx).Image = pieces(move_y, move_x).Image

            'Disposes of chosen piece (as replaced) and frees board space
            pieces(move_y, move_x).Dispose()
            board(move_y, move_x).setOccupied(False)
            piece(move_y, move_x).setPieceColour(Color.Gray)


            'If board square chosen is not occupied occupied then:
        Else


            'Dispose of board square so that chess piece picturebox is visible
            box(yy, xx).Dispose()


            'Recreates chess piece at chosen board square
            pieces(yy, xx) = New PictureBox
            Controls.Add(pieces(yy, xx))
            pieces(yy, xx).Location = New Point(100 * (xx + 0.075), 100 * (yy + 0.075))
            pieces(yy, xx).Width = 75
            pieces(yy, xx).Height = 75
            pieces(yy, xx).Visible = True
            pieces(yy, xx).Tag = yy & "" & xx

            'Set moved attribute to true
            piece(yy, xx).setMoved(True)
            'Set piece colour attribute to that of the chosen chess piece
            piece(yy, xx).setPieceColour(piece(move_y, move_x).getPieceColour())
            'Set value attribute to that of the chosen chess piece
            piece(yy, xx).setValue(piece(move_y, move_x).getValue())
            'Set picturebox image to that of the chosen chess piece
            pieces(yy, xx).Image = pieces(move_y, move_x).Image
            'Set occupied attribute of board to true
            board(yy, xx).setOccupied(True)
            'Create a clickhandler for chess piece
            AddHandler pieces(yy, xx).Click, AddressOf highlightAvailableMoves


            'Recreate board square
            box(yy, xx) = New PictureBox
            Controls.Add(box(yy, xx))
            box(yy, xx).BackColor = Color.White
            box(yy, xx).Location = New Point(100 * xx, 100 * yy)
            box(yy, xx).Width = 90
            box(yy, xx).Height = 90
            box(yy, xx).Visible = True
            box(yy, xx).Tag = yy & xx
            board(yy, xx).setColour(Color.White)
            pieces(yy, xx).BackColor = Color.White

            If yy = 1 Or yy = 3 Or yy = 5 Or yy = 7 Then
                If xx = 1 Or xx = 3 Or xx = 5 Or xx = 7 Then

                    box(yy, xx).BackColor = Color.Gray
                    board(yy, xx).setColour(Color.Gray)
                    pieces(yy, xx).BackColor = Color.Gray

                End If
            End If


            If yy = 0 Or yy = 2 Or yy = 4 Or yy = 6 Then
                If xx = 0 Or xx = 2 Or xx = 4 Or xx = 6 Then

                    box(yy, xx).BackColor = Color.Gray
                    board(yy, xx).setColour(Color.Gray)
                    pieces(yy, xx).BackColor = Color.Gray

                End If
            End If

            AddHandler box(yy, xx).Click, AddressOf validateMove


            'Disposes of chosen piece (as replaced) and frees board space
            pieces(move_y, move_x).Dispose()
            board(move_y, move_x).setOccupied(False)
            piece(move_y, move_x).setPieceColour(Color.Gray)

        End If


        'If the chess piece chosen is a white or black pawn then:
        If piece(yy, xx).getValue() = 1 Or piece(yy, xx).getValue() = -1 Then

            'Call pawnTransformation subroutine
            pawnTransformation(xx, yy)

        End If


        'Set piece location to the board square that the chess piece has moved to
        PieceLocation = (yy & xx)
        counters = 0

    End Sub



    'This subroutine allows the player to change their pawn into another piece if it has reached the opposite end of the chess board 
    Sub pawnTransformation(ByVal xx As Integer, ByVal yy As Integer)
        Dim pieceChoice As String
        Dim transformationOccured As Boolean = False


        'If piece colour is white and piece is at the top of the board then:
        If piece(yy, xx).getPieceColour() = Color.White And yy = 0 Then

            'Repeat until pawn has changed into another piece type
            Do

                'Get piece type choice from user
                pieceChoice = InputBox("Which piece would you like to turn your pawn into?")


                'If player chooses knight then: 
                If LCase(pieceChoice) = "knight" Then

                    'Set value attribute to 3 (value of white knight)
                    piece(yy, xx).setValue(3)
                    'Set picturebox image to white knight
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\white_knight.png")
                    'Set transformationOccured to true so that conditonal loop may end
                    transformationOccured = True

                    'If player chooses rook then: 
                ElseIf LCase(pieceChoice) = "rook" Then

                    'Set value attribute to 5 (value of white rook)
                    piece(yy, xx).setValue(5)
                    'Set picturebox image to white rook
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\white_rook.png")
                    transformationOccured = True

                    'If player chooses bishop then: 
                ElseIf LCase(pieceChoice) = "bishop" Then

                    'Set value attribute to 6 (value of white bishop)
                    piece(yy, xx).setValue(6)
                    'Set picturebox image to white bishop
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\white_bishop.png")
                    transformationOccured = True

                    'If player chooses queen then: 
                ElseIf LCase(pieceChoice) = "queen" Then

                    'Set value attribute to 10 (value of white queen)
                    piece(yy, xx).setValue(10)
                    'Set picturebox image to white queen
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\white_queen.png")
                    transformationOccured = True

                    'If user does not enter a string matching any of the available piece types then
                Else

                    'Display error message telling user that their choice isn't possible
                    MsgBox("I'm sorry but " & pieceChoice & " is not a viable option")

                End If


            Loop Until transformationOccured = True


            'If piece colour is white and piece is at the bottom of the board then:
        ElseIf piece(yy, xx).getPieceColour() = Color.Black And yy = 7 Then


            Do

                pieceChoice = InputBox("Which piece would you like to turn your pawn into?")


                'If player chooses knight then: 
                If LCase(pieceChoice) = "knight" Then


                    'Set value attribute to -3 (value of black knight)
                    piece(yy, xx).setValue(-3)
                    'Set picturebox image to black knight
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\black_knight.png")
                    'Set transformationOccured to true so that conditonal loop may end
                    transformationOccured = True

                    'If player chooses rook then: 
                ElseIf LCase(pieceChoice) = "rook" Then

                    'Set value attribute to -5 (value of black rook)
                    piece(yy, xx).setValue(-5)
                    'Set picturebox image to black rook
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\black_rook.png")
                    transformationOccured = True

                    'If player chooses bishop then: 
                ElseIf LCase(pieceChoice) = "bishop" Then

                    'Set value attribute to -6 (value of black bishop)
                    piece(yy, xx).setValue(-6)
                    'Set picturebox image to black bishop
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\black_bishop.png")
                    transformationOccured = True

                    'If player chooses queen then: 
                ElseIf LCase(pieceChoice) = "queen" Then

                    'Set value attribute to -10 (value of black queen)
                    piece(yy, xx).setValue(-10)
                    'Set picturebox image to black queen
                    pieces(yy, xx).Image = Image.FromFile("c:\users\leea1\documents\projectpiecepictures\black_queen.png")
                    transformationOccured = True

                    'If user does not enter a string matching any of the available piece types then
                Else

                    'Display error message telling user that their choice isn't possible
                    MsgBox("I'm sorry but " & pieceChoice & " is not a viable option")

                End If


            Loop Until transformationOccured = True

        End If

    End Sub



    'This subroutine determines if a king piece is missing and which player has lost a king
    Sub endgame(ByVal legal As Boolean)
        If legal = True Then

            Dim turnColour As Color
            Dim coloury As Integer = Strings.Left(PieceLocation, 1)
            Dim colourx As Integer = Strings.Right(PieceLocation, 1)
            Dim kingCount As Integer = 0

            'If colour of piece clicked is white then:
            If piece(coloury, colourx).getPieceColour() = Color.White Then

                'Set turnColour to white
                turnColour = Color.White

                'If colour of piece clicked is black then:
            ElseIf piece(coloury, colourx).getPieceColour() = Color.Black Then

                'Set turnColour to black
                turnColour = Color.Black

            End If


            'Repeat for all chess piece locations
            For rows = 0 To 7
                For columns = 0 To 7


                    'If value attribute is 15 or -15 (white or black king) then:
                    If piece(rows, columns).getValue() = 15 Or piece(rows, columns).getValue() = -15 Then

                        'Increase kingCount variable by 1
                        kingCount += 1

                    End If


                Next
            Next


            Dim leaderboard(Form1.arrayLength - 1) As Form1.leaderboardDetails
            Dim decider As Integer
            Dim leaderboardCount As Integer = 0
            Dim userlocation As Integer

            'If there is only 1 king alive and it's white's turn then:
            If kingCount = 1 And turnColour = Color.White Then

                'Display message box declaring winner
                MsgBox("White wins!")
                'Set variable lockGame to 1 which prevents users from moving any more pieces
                lockGame = 1
                'Call readLeaderboard subroutine and read details into leaderboard array of records
                Form1.readLeaderboard(leaderboard)
                'Change resetGameBtn text to "New Game"
                resetGameBtn.Text = "New Game"


                'For the number of users (2)
                For numbers = 0 To 1


                    leaderboardCount = 0
                    'Repeat for the length of array of records
                    Do


                        'If username is equal to any username in leaderboard table then:
                        If AccountLoginClicked.users(numbers) = leaderboard(leaderboardCount).username Then

                            'Set decider to numbers so that user can be correctly identified in updateLeaderboard subroutine  
                            decider = numbers
                            'Set userlocation to location that username was found in leaderboard array so that it can be used to update users details in leaderboard table
                            userlocation = leaderboardCount
                            'Call updateLeaderboard subroutine whilst passing in values
                            Form1.updateLeaderboard(leaderboard, decider, userlocation, turnColour)

                        End If


                        'Increment conditional loop
                        leaderboardCount += 1
                    Loop Until leaderboardCount = Form1.arrayLength


                Next


                'If there is only 1 king alive and it's black's turn then:
            ElseIf kingCount = 1 And turnColour = Color.Black Then

                'Do the exact same as when white wins but for blackplayer
                MsgBox("Black wins!")
                lockGame = 1
                Form1.readLeaderboard(leaderboard)
                resetGameBtn.Text = "New Game"


                For numbers = 0 To 1


                    Do


                        If AccountLoginClicked.users(numbers) = leaderboard(leaderboardCount).username Then

                            decider = numbers
                            userlocation = leaderboardCount
                            Form1.updateLeaderboard(leaderboard, decider, userlocation, turnColour)

                        End If


                        leaderboardCount += 1
                    Loop Until leaderboardCount = Form1.arrayLength


                Next
            End If
        End If

    End Sub



    'This subroutine disposes of the chess board and its pieces, as well as setting certain global variables to their initial values
    Sub resetGame()


        'Repeat for every available position
        For row = 0 To 7
            For column = 0 To 7

                'Set moved attribute to false
                piece(row, column).setMoved(False)
                'Set occupied attribute to false
                board(row, column).setOccupied(False)
                'Dispose of all chess piece pictureboxes
                pieces(row, column).Dispose()
                'Dispose of all board square pictureboxes
                box(row, column).Dispose()


            Next
        Next

        'Set global variables back to their original values
        availableMoves = 0
        PieceLocation = ""
        Boardchecker = 0
        validateChoice = Color.White
        counters = 0
        lockGame = 0

        'Dispose of all labels and textboxes
        pieceLocationLabel.Dispose()
        boardLocationLabel.Dispose()
        pieceLocationText.Dispose()
        boardLocationText.Dispose()
        turn.Dispose()


    End Sub
End Class










Public Class ChessBoard

    Private occupied As Boolean
    Private colour As Color


    'Setters
    'Set occupied
    Sub setOccupied(ByVal occupied As Boolean)

        Me.occupied = occupied

    End Sub


    'Set colour
    Sub setColour(ByVal colour As Color)

        Me.colour = colour

    End Sub



    'Getters
    'Get occupied
    Function getOccupied()

        Return Me.occupied

    End Function



    'Get colour
    Function getColour()

        Return Me.colour

    End Function


End Class






Public Class ChessPieces

    Private Piececolour As Color
    Private value As Integer
    Private position As Integer
    Private moved As Boolean

    'Setters
    'Set Piececolour
    Sub setPieceColour(ByVal Piececolour As Color)

        Me.Piececolour = Piececolour

    End Sub


    'Set value
    Sub setValue(ByVal value As Integer)

        Me.value = value

    End Sub


    'Set position
    Sub setposition(ByVal position As Integer)

        Me.position = position

    End Sub


    'Set moved
    Sub setMoved(ByVal moved As Boolean)

        Me.moved = moved

    End Sub



    'Getters
    'Get Piececolour
    Function getPieceColour()

        Return Me.Piececolour

    End Function


    'Get value
    Function getValue()

        Return Me.value

    End Function


    'Get Position
    Function getPosition()

        Return Me.position

    End Function


    'Get Position
    Function getMoved()

        Return Me.moved

    End Function

End Class