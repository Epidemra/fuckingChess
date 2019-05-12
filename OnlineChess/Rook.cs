using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class Rook : figure
    {
        public static dStruct[] stepsForCheck = { new dStruct(1,0), new dStruct(-1,0), new dStruct(0,1), new dStruct(0,-1)};

        public Rook(int _x, int _y, figureColor _color) : base(_x, _y, _color, figureType.Rook)
        {
            isRepeat = true;
        }

        public override bool isValidMove(int _x, int _y, figure[,] _board)
        {
            if ((this.x != _x ^ this.y != _y) && isCorrectLong(_x, _y) && isEmptyRoadLong(_x, _y, _board))
                return true;

            return false;
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = new List<dStruct>();

            for (int i = 0; i <= Rook.stepsForCheck.Length - 1; i++)
            {
                int ix = this.x + Rook.stepsForCheck[i].dx;
                int iy = this.y + Rook.stepsForCheck[i].dy;
                while (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0)
                {
                    if(_board[iy, ix] == null)
                        possibleMoves.Add(new dStruct( ix , iy));
                    else
                    {
                        if (_board[iy, ix].getColor() != this.getColor())
                            possibleMoves.Add(new dStruct(ix, iy));
                        break;
                    }                                           

                    ix += Rook.stepsForCheck[i].dx;
                    iy += Rook.stepsForCheck[i].dy;
                }
            }
            return possibleMoves;
        }
    }
}
