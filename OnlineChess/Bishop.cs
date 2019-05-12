using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class Bishop : figure
    {
        public static dStruct[] stepsForCheck = { new dStruct(1, 1), new dStruct(1, -1), new dStruct(-1, 1), new dStruct(-1, -1) };

        public Bishop(int _x, int _y, figureColor _color) : base(_x , _y , _color , figureType.Bishop)
        {
            isRepeat = true;
        }

        public override bool isValidMove(int _x, int _y, figure[,] _board)
        {
            if (this.x != _x && this.y != _y && isCorrectDia(_x, _y) && isEmptyRoadDia(_x, _y, _board))
                return true;

            return false;
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = new List<dStruct>();

            for (int i = 0; i <= Bishop.stepsForCheck.Length - 1; i++)
            {
                int ix = this.x + Bishop.stepsForCheck[i].dx;
                int iy = this.y + Bishop.stepsForCheck[i].dy;
                while (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0)
                {
                    if (_board[iy, ix] == null)
                        possibleMoves.Add(new dStruct(ix, iy));
                    else
                    {
                        if (_board[iy, ix].getColor() != this.getColor())
                            possibleMoves.Add(new dStruct(ix, iy));
                        break;
                    }

                    ix += Bishop.stepsForCheck[i].dx;
                    iy += Bishop.stepsForCheck[i].dy;
                }
            }

            return possibleMoves;
        }
    }
}
