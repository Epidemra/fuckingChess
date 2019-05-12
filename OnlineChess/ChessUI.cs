using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OnlineChess {

    public enum signalType { ordinaryMove , castlingNearWhite , castlingFarWhite, castlingNearBlack, castlingFarBlack, pawnTransfom , mat , pat , disconnect, newGame};

    public partial class ChessUI : Form
    {
        private Color colorWhite = Color.White;
        private Color colorBlack = Color.Indigo;
        private Color clickedButtonColor;
        private MyButton clickedButton = null;
        private NetworkStream stream;
        private ChessModel game;
        private MyButton[,] buttons = new MyButton[8 , 8];
        private List<dStruct> possibleMoves = new List<dStruct>();
        char[] chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private figureColor color;
        bool isClicked = false;
        private Image[,] pawnTransform;
        private Button btn_newGame;

        public ChessUI(NetworkStream stream, figureColor color)
        {
            InitializeComponent();
            this.stream = stream;
            this.color = color;
            btn_light.BackColor = color == figureColor.White ? Color.Green : Color.Red; 
            generateButtonArray(color);
            setFiguresOnStart();
            pawnTransform = new Image[,] { { null, imageList1.Images[4], imageList1.Images[5], imageList1.Images[0], null, imageList1.Images[2]},
                                           { null, imageList1.Images[10], imageList1.Images[11], imageList1.Images[6], null, imageList1.Images[8]} };
            game = new ChessModel(color);
            if (color == figureColor.White)
            {
                Text = "Chess - Host";
                InfoLabel.Text = "You are the first";
            }
            else
            {
                Text = "Chess - Client";
                InfoLabel.Text = "Your opponent is the first";
            }
            WaitForResponse();
        }

        public void generateButtonArray(figureColor color)
        {
            int btnWidth = 50, horizOffset = 40, vertOffset = 100;
            if(color == figureColor.White)
            {
                btn_newGame = new Button();
                btn_newGame.Location = new Point(450, 200);
                btn_newGame.Text = "New game";
                btn_newGame.Size = new Size(100, 50);
                btn_newGame.Click += new EventHandler(btn_newGame_Click);
                btn_newGame.Enabled = false;
                Controls.Add(btn_newGame);


                for (byte i = 8; i >= 1; i--)
                {
                    leftLabel.Text += "\n\n" + i.ToString() + "\n\n";
                    downLabel.Text += "        " + chars[8 - i] + "       ";
                }                    

                bool isWhite = true;
                for (int i = 7; i >= 0; i--)
                {
                    for (byte c = 0; c <= 7; c++)
                    {
                        buttons[i, c] = new MyButton();
                        buttons[i, c].Location = new Point(horizOffset + btnWidth * c, vertOffset + (7 - i) * btnWidth);
                        buttons[i, c].Name = "btn" + i.ToString() + c.ToString();
                        buttons[i, c].Size = new Size(btnWidth, btnWidth);
                        buttons[i, c].Text = buttons[i, c].Name;
                        buttons[i, c].BackColor = isWhite ? colorWhite : colorBlack;
                        buttons[i, c].Click += new EventHandler(Button_Click);
                        buttons[i, c].x = c;
                        buttons[i, c].y = i;
                        Controls.Add(buttons[i, c]);
                        isWhite = !isWhite;
                    }
                    isWhite = !isWhite;
                }
            }
            else
            {
                for (int i = 1; i <= 8; i++)
                {
                    leftLabel.Text += "\n\n" + i.ToString() + "\n\n";
                    downLabel.Text += "        " + chars[8 - i] + "       ";
                }                    
                
                bool isWhite = false;
                for (byte i = 0; i <= 7; i++)
                {
                    for (byte c = 0; c <= 7; c++)
                    {
                        buttons[i, c] = new MyButton();
                        buttons[i, c].Location = new Point(horizOffset + btnWidth * (7 - c), vertOffset + i * btnWidth);
                        buttons[i, c].Name = "btn"  + i.ToString() + c.ToString();
                        buttons[i, c].Size = new Size(btnWidth, btnWidth);
                        buttons[i, c].Text = buttons[i, c].Name;
                        buttons[i, c].BackColor = isWhite ? colorWhite : colorBlack;
                        buttons[i, c].Click += new EventHandler(Button_Click);
                        buttons[i, c].x = c;
                        buttons[i, c].y = i;
                        Controls.Add(buttons[i, c]);
                        isWhite = !isWhite;
                    }
                    isWhite = !isWhite;
                }
            }           
        }

        public void setFiguresOnStart()
        {
            for(byte i = 0; i <= 7; i++)
            {
                buttons[1, i].Image = imageList1.Images[9];
            }

            buttons[0, 0].Image = buttons[0, 7].Image = imageList1.Images[11];
            buttons[0, 1].Image = buttons[0, 6].Image = imageList1.Images[8];
            buttons[0, 2].Image = buttons[0, 5].Image = imageList1.Images[6];
            buttons[0, 3].Image = imageList1.Images[10];
            buttons[0, 4].Image = imageList1.Images[7];

            for (byte i = 0; i <= 7; i++)
            {
                buttons[6, i].Image = imageList1.Images[3];
            }

            buttons[7, 0].Image = buttons[7, 7].Image = imageList1.Images[5];
            buttons[7, 1].Image = buttons[7, 6].Image = imageList1.Images[2];
            buttons[7, 2].Image = buttons[7, 5].Image = imageList1.Images[0];
            buttons[7, 3].Image = imageList1.Images[4];
            buttons[7, 4].Image = imageList1.Images[1];

            for (byte i = 2; i <= 5; i++)
                for (byte j = 0; j <= 7; j++)
                    buttons[i, j].Image = null;
        }

        public void UpdateBoard(byte[] coord)
        {
            switch (coord[0])
            {
                case 1:
                    buttons[0, 5].Image = buttons[0, 7].Image;
                    buttons[0, 7].Image = null;
                    break;
                case 2:
                    buttons[0, 3].Image = buttons[0, 0].Image;
                    buttons[0, 0].Image = null;
                    break;
                case 3:
                    buttons[7, 5].Image = buttons[7, 7].Image;
                    buttons[7, 7].Image = null;
                    break;
                case 4:
                    buttons[7, 3].Image = buttons[7, 0].Image;
                    buttons[7, 0].Image = null;
                    break;
            }
            buttons[coord[4], coord[3]].Image = buttons[coord[2], coord[1]].Image;
            buttons[coord[2], coord[1]].Image = null;
        }

        public void UpdateBoardPawnTransfom(byte[] coord)
        {
            //0 - x1, 1 - y1, 2 - x2, 3 - y2, 4 - figType, 5 - color
            UpdateBoard(new byte[] { 0, coord[0], coord[1], coord[2], coord[3] });
            buttons[coord[3], coord[2]].Image = pawnTransform[coord[5],coord[4]];            
        }

        private async void WaitForResponse()
        {
            try
            {
                while (true)
                {
                    if (stream.CanRead)
                    {
                        string move;
                        byte[] bytes = new byte[256];
                        await stream.ReadAsync(bytes, 0, bytes.Length);

                        if (bytes[0] == (byte)signalType.disconnect)
                        {
                            InfoLabel.Text = "Disconnected";
                            game.isGameOver = true;
                            break;
                        }
                        else
                        {
                            switch (bytes[0])
                            {
                                case (byte)signalType.ordinaryMove:
                                case (byte)signalType.castlingNearWhite:
                                case (byte)signalType.castlingFarWhite:
                                case (byte)signalType.castlingNearBlack:
                                case (byte)signalType.castlingFarBlack:
                                    //обработка пришедшего хода
                                    move = game.board[bytes[2], bytes[1]].getColor().ToString() + ' ' +
                                           game.board[bytes[2], bytes[1]].getName().ToString() + " (" +
                                           chars[bytes[1]] + (bytes[2] + 1).ToString() + ")->(" +
                                           chars[bytes[3]] + (bytes[4] + 1).ToString() + ')';
                                    lastMove.Text = move;
                                    UpdateBoard(bytes);
                                    game.makeMove(bytes);
                                    game.changeTurn();
                                    btn_light.BackColor = Color.Green;

                                    if (!game.isMovesExist() && !game.isChecked())
                                    {
                                        lblLoseWin.Text = "Pat";
                                        WriteMessage(new byte[] { (byte)signalType.pat });
                                        if (color == figureColor.White)
                                            btn_newGame.Enabled = true;
                                    }
                                    else
                                    if (game.isChecked() && !game.isMovesExist())
                                    {
                                        lblLoseWin.Text = "Mat. You lose";
                                        WriteMessage(new byte[] { (byte)signalType.mat });
                                        if (color == figureColor.White)
                                            btn_newGame.Enabled = true;
                                    }
                                    else
                                    if (game.isChecked() && game.isMovesExist())
                                        lblLoseWin.Text = "Checked";
                                    else
                                        lblLoseWin.Text = "";
                                    break;
                                case (byte)signalType.pawnTransfom:
                                    move = game.board[bytes[2], bytes[1]].getColor().ToString() + ' ' +
                                           game.board[bytes[2], bytes[1]].getName().ToString() + " (" +
                                           chars[bytes[1]] + (bytes[2] + 1).ToString() + ")->(" +
                                           chars[bytes[3]] + (bytes[4] + 1).ToString() + ')';
                                    lastMove.Text = move;
                                    UpdateBoardPawnTransfom(new byte[] { bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6] });
                                    game.makePawnTransformMove(new byte[] { bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6] });
                                    game.changeTurn();
                                    btn_light.BackColor = Color.Green;

                                    if (!game.isMovesExist() && !game.isChecked())
                                    {
                                        lblLoseWin.Text = "Pat";
                                        WriteMessage(new byte[] { (byte)signalType.pat });
                                        if (color == figureColor.White)
                                            btn_newGame.Enabled = true;
                                    }
                                    else
                                    if (game.isChecked() && !game.isMovesExist())
                                    {
                                        lblLoseWin.Text = "Mat. You lose";
                                        WriteMessage(new byte[] { (byte)signalType.mat });
                                        if (color == figureColor.White)
                                            btn_newGame.Enabled = true;
                                    }
                                    else
                                    if (game.isChecked() && game.isMovesExist())
                                        lblLoseWin.Text = "Checked";
                                    else
                                        lblLoseWin.Text = "";
                                    break;
                                case (byte)signalType.mat:
                                    if (color == figureColor.White)
                                        btn_newGame.Enabled = true;
                                    lblLoseWin.Text = "Mat.You win";
                                    break;
                                case (byte)signalType.pat:
                                    if (color == figureColor.White)
                                        btn_newGame.Enabled = true;
                                    lblLoseWin.Text = "Pat";
                                    break;
                                case (byte)signalType.newGame:
                                    btn_light.BackColor = Color.Red;
                                    game = new ChessModel(color);
                                    setFiguresOnStart();
                                    break;
                            }                              /* else
                                /* */                             
                        }
                    }                                      
                }                
            }
            catch (ObjectDisposedException err)
            {
                InfoLabel.Text = "Opponent disconnected. You won!";
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }        

        /*
         * Sends a string of data through the network stream
         */
        private void WriteMessage(byte[] data)
        {
            if(stream.CanRead)
                stream.Write(data, 0, data.Length);
        }

        private void Button_Click(object sender, EventArgs e)
        {//сделать ход в gameModel
            if (game.getTurn())
            {
                if (isClicked)
                {
                    MyButton _sender = (MyButton)sender;
                    if(possibleMoves.IndexOf(new dStruct(_sender.x , _sender.y)) != -1)
                    {
                        string move;
                        move = game.board[clickedButton.y, clickedButton.x].getColor().ToString() + ' ' +
                               game.board[clickedButton.y, clickedButton.x].getName().ToString() + " (" +
                               chars[clickedButton.x] + (clickedButton.y + 1).ToString() + ")->(" +
                               chars[_sender.x] + (_sender.y + 1).ToString() + ')';
                        lastMove.Text = move;

                        if ((game.board[clickedButton.y, clickedButton.x].getName() == figureType.Pawn &&
                            game.board[clickedButton.y, clickedButton.x].getColor() == figureColor.White && 
                            clickedButton.y == 6) ||
                            (game.board[clickedButton.y, clickedButton.x].getName() == figureType.Pawn &&
                            game.board[clickedButton.y, clickedButton.x].getColor() == figureColor.Black &&
                            clickedButton.y == 1))
                        {
                            byte choseFigure;
                            var pawnTransform = new PawnTransform();
                            if (pawnTransform.ShowDialog() == DialogResult.OK)
                            {
                                choseFigure = pawnTransform.chosenFigure;
                                byte[] bytes = new byte[] { (byte)clickedButton.x, (byte)clickedButton.y,
                                                            (byte)_sender.x, (byte)_sender.y,
                                                            choseFigure , (byte)color};
                                game.makePawnTransformMove(bytes);
                                UpdateBoardPawnTransfom(bytes);
                                btn_light.BackColor = Color.Red;
                                WriteMessage(new byte[] { 5, bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5] });
                                game.changeTurn();
                            }
                            pawnTransform.Dispose();                                
                        }
                        else
                        {
                            byte moveType;
                            if (clickedButton.x == 4 && clickedButton.y == 0 && _sender.x == 2 && _sender.y == 0)
                                moveType = 2;
                            else
                            if (clickedButton.x == 4 && clickedButton.y == 0 && _sender.x == 6 && _sender.y == 0)
                                moveType = 1;
                            else
                            if (clickedButton.x == 4 && clickedButton.y == 7 && _sender.x == 2 && _sender.y == 7)
                                moveType = 4;
                            else
                            if (clickedButton.x == 4 && clickedButton.y == 7 && _sender.x == 6 && _sender.y == 7)
                                moveType = 3;
                            else
                            {
                                moveType = 0;
                            }
                            byte[] data = new byte[] {moveType, (byte)clickedButton.x , (byte)clickedButton.y,
                                                                (byte)_sender.x, (byte)_sender.y};
                            //отправка хода противнику
                            WriteMessage(data);
                            //обработка хода у меня
                            UpdateBoard(data);
                            game.makeMove(data);
                            game.changeTurn();
                            btn_light.BackColor = Color.Red;
                        }                       

                        //отладка
                        textBox1.Text = "";
                        for (int i = 0; i <= 7; i++)
                        {
                            for (int j = 0; j <= 7; j++)
                            {
                                if (game.board[i, j] == null)
                                    textBox1.Text += i.ToString() + ' ' + j.ToString() + "null";
                                else
                                    textBox1.Text += i.ToString() + ' ' + j.ToString() + game.board[i, j].getName() + game.board[i, j].getColor();
                            }
                            textBox1.Text += "\n\n";
                        }
                    }
                    else
                        InfoLabel.Text = "Invalid move";

                    isClicked = false;
                    clickedButton.BackColor = (clickedButton.x + clickedButton.y) % 2 != 0 ? colorWhite : colorBlack;
                    for (int i = 0; i <= possibleMoves.Count - 1; i++)
                        buttons[possibleMoves[i].dy, possibleMoves[i].dx].BackColor = (possibleMoves[i].dy + possibleMoves[i].dx) % 2 != 0 ? colorWhite : colorBlack;
                    possibleMoves.Clear();
                    clickedButton = null;                    
                }
                else
                {
                    MyButton _sender = (MyButton)sender;
                    if(!game.isGameOver)
                    if (game.board[_sender.y, _sender.x] != null)
                    {
                        if (game.board[_sender.y, _sender.x].getColor() == this.color)
                        {
                            isClicked = true;
                            clickedButton = (MyButton)sender;
                            clickedButtonColor = clickedButton.BackColor;
                            clickedButton.BackColor = Color.YellowGreen;
                            possibleMoves = game.board[_sender.y, _sender.x].getPossibleMoves(game.board, game);
                            for (int i = 0; i <= possibleMoves.Count - 1; i++)
                                if (!game.isValidMove_Ch(_sender.x, _sender.y, possibleMoves[i].dx, possibleMoves[i].dy))
                                {
                                    possibleMoves.RemoveAt(i);
                                    i--;
                                }
                            for(int i = 0; i <= possibleMoves.Count - 1; i++)
                                buttons[possibleMoves[i].dy, possibleMoves[i].dx].BackColor = Color.YellowGreen;
                        }
                        else
                            InfoLabel.Text = "It's not your figure";
                    }
                    else
                        InfoLabel.Text = "Empty";
                }
            }
            else
                InfoLabel.Text = "It's not your turn";                                           
        }

        private void btn_newGame_Click(object sender, EventArgs e)
        {
            game = new ChessModel(color);
            setFiguresOnStart();
            WriteMessage(new byte[] { (byte)signalType.newGame});
            ((Button)sender).Enabled = false;
            btn_light.BackColor = Color.Green;
        }
    }
}
