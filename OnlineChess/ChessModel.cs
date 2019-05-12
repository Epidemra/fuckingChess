using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineChess;

namespace OnlineChess {
    public enum figureType { Pawn , Queen , Rook , Bishop , King , Knight }

    public struct dStruct
    {
        public int dx;
        public int dy;

        public dStruct(int _dx, int _dy)
        {
            this.dx = _dx;
            this.dy = _dy;
        }
    }

    public class ChessModel{    

        private bool isMyTurn;
        public bool isGameOver;
        public figure[,] board = new figure[8 , 8];
        private figureColor color;
        private figure[,] pawnTransform = { {null, new Queen(0, 0, figureColor.Black), new Rook(0, 0,figureColor.Black),new Bishop(0, 0, figureColor.Black),null,new Knight(0, 0, figureColor.Black) },
                                            {null, new Queen(0, 0, figureColor.White), new Rook(0, 0,figureColor.White),new Bishop(0, 0, figureColor.White),null,new Knight(0, 0, figureColor.White) }};

        public ChessModel(figureColor color)
        {
            isMyTurn = color == figureColor.White ? true : false;
            this.color = color;
            this.isGameOver = false;

            createObjectArr();
        }

        public void changeTurn()
        {
            isMyTurn = !isMyTurn;
        }

        public bool getTurn()
        {
            return this.isMyTurn;
        }
        //попробовать через массивы
        public void makeMove(byte[] coord)
        {
            switch (coord[0])
            {
                case 1:
                    board[0, 5] = board[0, 7];
                    board[0, 5].x = 5;
                    board[0, 5].y = 0;
                    board[0, 5].isMoved = true;
                    board[0, 7] = null;
                    break;
                case 2:
                    board[0, 3] = board[0, 0];
                    board[0, 3].x = 3;
                    board[0, 3].y = 0;
                    board[0, 3].isMoved = true;
                    board[0, 0] = null;
                    break;
                case 3:
                    board[7, 5] = board[7, 7];
                    board[7, 5].x = 5;
                    board[7, 5].y = 5;
                    board[7, 5].isMoved = true;
                    board[7, 7] = null;
                    break;
                case 4:
                    board[7, 3] = board[7, 0];
                    board[7, 3].x = 3;
                    board[7, 3].y = 7;
                    board[7, 3].isMoved = true;
                    board[7, 0] = null;
                    break;
            }
            board[coord[4], coord[3]] = board[coord[2], coord[1]];
            board[coord[4], coord[3]].x = coord[3];
            board[coord[4], coord[3]].y = coord[4];
            board[coord[4], coord[3]].isMoved = true;
            board[coord[2], coord[1]] = null;
        }

        public void makePawnTransformMove(byte[] coord)
        {
            //0 - x1, 1 - y1, 2 - x2, 3 - y2, 4 - figType, 5 - color
            makeMove(new byte[] { 0, coord[0], coord[1], coord[2], coord[3] });
            board[coord[3], coord[2]] = pawnTransform[coord[5], coord[4]];
            board[coord[3], coord[2]].x = coord[2];
            board[coord[3], coord[2]].y = coord[3];            
        }

        private void createObjectArr()
        {
            //white figures
            for (byte i = 0; i <= 7; i++)
            {
                board[1, i] = new Pawn(i , 1 , figureColor.White);
            }

            board[0, 1] = new Knight(1, 0, figureColor.White);
            board[0, 6] = new Knight(6, 0, figureColor.White);

            board[0, 2] = new Bishop(2, 0, figureColor.White);
            board[0, 5] = new Bishop(5, 0, figureColor.White);

            board[0, 0] = new Rook(0, 0, figureColor.White);
            board[0, 7] = new Rook(7, 0, figureColor.White);

            board[0, 3] = new Queen(3, 0, figureColor.White);
            board[0, 4] = new King(4, 0, figureColor.White);

            //black figures
            for (byte i = 0; i <= 7; i++)
            {
                board[6, i] = new Pawn(i, 6, figureColor.Black);
            }

            board[7, 6] = new Knight(6, 7, figureColor.Black);
            board[7, 1] = new Knight(1, 7, figureColor.Black);

            board[7, 2] = new Bishop(2, 7, figureColor.Black);
            board[7, 5] = new Bishop(5, 7, figureColor.Black);

            board[7, 0] = new Rook(0, 7, figureColor.Black);
            board[7, 7] = new Rook(7, 7, figureColor.Black);

            board[7, 3] = new Queen(3, 7, figureColor.Black);
            board[7, 4] = new King(4, 7, figureColor.Black);

            for (byte i = 2; i <= 5; i++)
                for (byte j = 0; j <= 7; j++)
                    board[i, j] = null;            
        }

        public bool isValidMove_Ch(int x1 , int y1 , int x2 , int y2)
        {
            figure savedFigure = board[y2, x2];
            board[y2, x2] = board[y1, x1];
            board[y2, x2].x = x2;
            board[y2, x2].y = y2;
            board[y1, x1] = null;

            bool res = !isChecked();

            board[y1, x1] = board[y2, x2];
            board[y1, x1].y = y1;
            board[y1, x1].x = x1;
            board[y2, x2] = savedFigure;

            return res;
        }

        public bool isChecked()
        {
            bool res = checkForLong(getKing(this.color)) &&
                       checkForDia(getKing(this.color)) &&
                       checkForKnight(getKing(this.color)) &&
                       checkForPawn(getKing(this.color)) &&
                       checkForKing(getKing(this.color));
            return !res;
        }

        public bool isMovesExist()
        {
            List<dStruct> moves = new List<dStruct>();
            List<dStruct> tempMoves = new List<dStruct>();
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                    if (board[i, j] != null && board[i,j].getColor() == this.color)
                    {
                        tempMoves.AddRange(board[i, j].getPossibleMoves(board, this));
                        for (int f = 0; f <= tempMoves.Count - 1; f++)
                            if(!isValidMove_Ch(j, i, tempMoves[f].dx, tempMoves[f].dy))
                            {
                                tempMoves.RemoveAt(f);
                                f--;
                            }
                        moves.AddRange(tempMoves);
                        tempMoves.Clear();
                    }                        

            return moves.Count != 0;
        }

        private bool checkForLong(figure kingMy)
        {
            bool res = true;

            for (int i = 0; (i <= Rook.stepsForCheck.Length - 1) && res; i++)
            {
                int ix = kingMy.x + Rook.stepsForCheck[i].dx;
                int iy = kingMy.y + Rook.stepsForCheck[i].dy;
                while (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0)
                {
                    if (board[iy, ix] != null)
                    {
                        res = res && ((board[iy, ix].getColor() == kingMy.getColor()) ||
                                     (board[iy, ix].getName() != figureType.Queen && board[iy, ix].getName() != figureType.Rook));
                        break;
                    }
                    ix += Rook.stepsForCheck[i].dx;
                    iy += Rook.stepsForCheck[i].dy;
                }
            }

            return res;            
        }

        private bool checkForDia(figure kingMy)
        {
            bool res = true;

            for(int i = 0; (i <= Bishop.stepsForCheck.Length - 1) && res; i++)
            {
                int ix = kingMy.x + Bishop.stepsForCheck[i].dx;
                int iy = kingMy.y + Bishop.stepsForCheck[i].dy;
                while (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0)
                {
                    if (board[iy, ix] != null)
                    {
                        res = res && ((board[iy, ix].getColor() == kingMy.getColor()) ||
                                      (board[iy, ix].getName() != figureType.Queen && board[iy, ix].getName() != figureType.Bishop));
                        break;
                    }
                    ix += Bishop.stepsForCheck[i].dx;
                    iy += Bishop.stepsForCheck[i].dy;
                }
            }
            return res;            
        }

        private bool checkForKnight(figure kingMy)
        {           
            int ix;
            int iy;
            for(int i = 0; i <= Knight.stepsForCheck.Length - 1; i++)
            {
                ix = kingMy.x + Knight.stepsForCheck[i].dx;
                iy = kingMy.y + Knight.stepsForCheck[i].dy;
                if (ix <= 7 && iy <= 7 && ix >=0 && iy >= 0 && board[iy, ix] != null && board[iy, ix].getColor() != kingMy.getColor() && board[iy, ix].getName() == figureType.Knight)
                    return false;
            }
            return true;           
        }

        private bool checkForPawn(figure kingMy)
        {
            figureColor kingColor = kingMy.getColor();
            int ix;
            int iy;

            for(int i = 0; i <= Pawn.stepForCheck[kingColor].Length - 1; i++)
            {
                ix = kingMy.x + Pawn.stepForCheck[kingColor][i].dx;
                iy = kingMy.y + Pawn.stepForCheck[kingColor][i].dy;
                if (ix <= 7 && ix >= 0 && iy <= 7 && iy >= 0 && board[iy, ix] != null && board[iy, ix].getColor() != kingMy.getColor() && board[iy, ix].getName() == figureType.Pawn)
                    return false;
            }
            return true;            
        }

        private bool checkForKing(figure kingMy)
        {
            figureColor oppColor = kingMy.getColor() == figureColor.White ? figureColor.Black : figureColor.White;
            figure oppKing = getKing(oppColor);
            return (Math.Abs(kingMy.x - oppKing.x) > 1 || Math.Abs(kingMy.y - oppKing.y) > 1);
        }

        private figure getKing(figureColor _color)
        {
            for (int i = 0; i <= 7; i++)
                for (int j = 0; j <= 7; j++)
                    if (board[i, j] != null && board[i, j].getColor() == _color && board[i, j].getName() == figureType.King)
                        return board[i, j];        
            return null;
        }
    }
}