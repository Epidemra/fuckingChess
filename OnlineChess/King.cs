using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class King : figure
    {
        public static dStruct[] stepsForCheck = { new dStruct(0, 1), new dStruct(1, 1), new dStruct(1, 0), new dStruct(1, -1),
                                                  new dStruct(0, -1), new dStruct(-1, -1), new dStruct(-1, 0), new dStruct(-1, 1)};

        public King(int _x, int _y, figureColor _color) : base(_x, _y, _color, figureType.King)
        {
            isRepeat = false;
        }

        public override bool isValidMove(int _x, int _y, figure[,] _board)
        {
            if ((this.y != _y || this.x != _x) && (Math.Abs(this.x - _x) <= 1 && Math.Abs(this.y - _y) <= 1) && (_board[_y , _x] == null || _board[_y , _x].getColor() != this.getColor()))
                return true;

            return false;
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = new List<dStruct>();
            int ix;
            int iy;
            for (int i = 0; i <= King.stepsForCheck.Length - 1; i++)
            {
                ix = this.x + King.stepsForCheck[i].dx;
                iy = this.y + King.stepsForCheck[i].dy;
                if ((ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0) && (_board[iy, ix] == null || (_board[iy, ix].getColor() != this.getColor())))
                    possibleMoves.Add(new dStruct(ix, iy));
            }
            ///длинная рокиррвка для белых
            if (!isMoved && _board[0, 0] != null && !_board[0, 0].isMoved && _board[0,1] == null && _board[0, 2] == null && _board[0, 3] == null &&
               !_game.isChecked() && _game.isValidMove_Ch(x, y, 3, 0) && _game.isValidMove_Ch(x, y, 2, 0) && getColor() == figureColor.White)
                possibleMoves.Add(new dStruct(2,0));

            //короткая для белых
            if (!isMoved && _board[0, 7] != null && !_board[0, 7].isMoved && _board[0, 5] == null && _board[0, 6] == null &&
               !_game.isChecked() && _game.isValidMove_Ch(x, y, 6, 0) && _game.isValidMove_Ch(x, y, 5, 0) && getColor() == figureColor.White)
                possibleMoves.Add(new dStruct(6, 0));

            //длинная для черных
            if (!isMoved && _board[7, 0] != null && !_board[7, 0].isMoved && _board[7, 1] == null && _board[7, 2] == null && _board[7, 3] == null &&
               !_game.isChecked() && _game.isValidMove_Ch(x, y, 3, 7) && _game.isValidMove_Ch(x, y, 2, 7) && getColor() == figureColor.Black)
                possibleMoves.Add(new dStruct(2, 7));

            //короткая для черных
            if (!isMoved && _board[7, 7] != null && !_board[7, 7].isMoved && _board[7, 5] == null && _board[7, 6] == null &&
               !_game.isChecked() && _game.isValidMove_Ch(x, y, 6, 7) && _game.isValidMove_Ch(x, y, 5, 7) && getColor() == figureColor.Black)
                possibleMoves.Add(new dStruct(6, 7));


            return possibleMoves;
        }
    }
}
