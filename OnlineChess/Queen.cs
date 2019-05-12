using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class Queen : figure
    {
        public Queen(int _x, int _y, figureColor _color) : base(_x, _y, _color, figureType.Queen)
        {
            isRepeat = true;
        }

        public override bool isValidMove(int _x, int _y, figure[,] _board)
        {
            if (this.y != _y || this.x != _x)
            {
                if (Math.Abs(this.x - _x) == Math.Abs(this.y - _y))
                {
                    if (isEmptyRoadDia(_x, _y, _board))
                        return true;
                }

                if (isCorrectLong(_x, _y) && isEmptyRoadLong(_x, _y, _board))
                    return true;
            }

            return false;
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = getPossibleMovesDia(_board);
            possibleMoves.AddRange(getPossibleMovesLong(_board));
            return possibleMoves;
        }

        private List<dStruct> getPossibleMovesLong(figure[,] _board)
        {
            List<dStruct> possibleMoves = new List<dStruct>();

            for (int i = 0; i <= Rook.stepsForCheck.Length - 1; i++)
            {
                int ix = this.x + Rook.stepsForCheck[i].dx;
                int iy = this.y + Rook.stepsForCheck[i].dy;
                while (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0 && _board[iy, ix] == null)
                {
                    possibleMoves.Add(new dStruct(ix, iy));

                    ix += Rook.stepsForCheck[i].dx;
                    iy += Rook.stepsForCheck[i].dy;
                }

                if (ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0 && _board[iy, ix].getColor() != this.getColor())
                    possibleMoves.Add(new dStruct(ix, iy));
            }

            return possibleMoves;
        }

        private List<dStruct> getPossibleMovesDia(figure[,] _board)
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
