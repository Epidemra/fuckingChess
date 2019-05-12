using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class Knight : figure
    {
        public static dStruct[] stepsForCheck = { new dStruct(2 , 1), new dStruct(2 , -1), new dStruct(-2, 1), new dStruct(-2 , -1),
                                                  new dStruct(1 , 2), new dStruct(-1 , 2), new dStruct(1, -2), new dStruct(-1 , -2) };

        public Knight(int _x, int _y, figureColor _color) : base(_x , _y , _color , figureType.Knight)
        {
            isRepeat = false;
        }

        public override bool isValidMove(int _x, int _y, figure[,] _board)
        {
            if (
                (
                 (Math.Abs(this.y - _y) == 2 && Math.Abs(this.x - _x) == 1) ||
                 (Math.Abs(this.y - _y) == 1 && Math.Abs(this.x - _x) == 2)
                ) &&
                (
                 _board[_y, _x] == null || (_board[_y, _x].getColor() != this.getColor() && _board[_y, _x].getName() != figureType.King)
                )
               )
            {
                return true;
            }
                
            return false;
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = new List<dStruct>();
            int ix;
            int iy;
            for (int i = 0; i <= Knight.stepsForCheck.Length - 1; i++)
            {
                ix = this.x + Knight.stepsForCheck[i].dx;
                iy = this.y + Knight.stepsForCheck[i].dy;
                if ((ix <= 7 && iy <= 7 && ix >= 0 && iy >= 0) && (_board[iy, ix] == null || (_board[iy, ix].getColor() != this.getColor())))
                    possibleMoves.Add(new dStruct(ix, iy));
            }
            return possibleMoves;
        }
    }
}
