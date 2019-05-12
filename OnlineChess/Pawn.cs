using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    class Pawn : figure
    {
        public static Dictionary<figureColor, dStruct[]> stepForCheck = new Dictionary<figureColor, dStruct[]>()
        {
            { figureColor.Black, new dStruct[]{new dStruct(1, -1) , new dStruct(-1, -1) } },
            { figureColor.White, new dStruct[]{new dStruct(1, 1) , new dStruct(-1, 1)} }
        };

        public static Dictionary<figureColor, dStruct[]> stepForChecks = new Dictionary<figureColor, dStruct[]>()
        {
            { figureColor.Black, new dStruct[]{new dStruct(0, -1) , new dStruct(0, -2) } },
            { figureColor.White, new dStruct[]{new dStruct(0, 1) , new dStruct(0, 2)} }
        };

        public Pawn(int _x , int _y , figureColor _color) : base(_x , _y , _color , figureType.Pawn)
        {
            isRepeat = false;
        }

        public override bool isValidMove(int _x , int _y, figure[,] _board)
        {
            if(this.getColor() == figureColor.White)
            {
                if (_y - this.y == 1 && Math.Abs(this.x - _x) == 1 && _board[_y , _x] != null && _board[_y , _x].getColor() == figureColor.Black && _board[_y, _x].getName() != figureType.King)
                    return true;
                    
                if (_board[_y , _x] == null && this.x == _x && _y - this.y == 1 )
                    return true;

                if (!isMoved && this.x == _x && _y - this.y == 2 && _board[_y , _x] == null && _board[_y - 1 , _x] == null)
                    return true;

                return false;
            }
            else
            {
                if (this.y - _y == 1 && Math.Abs(this.x - _x) == 1 && _board[_y, _x] != null && _board[_y, _x].getColor() == figureColor.White && _board[_y, _x].getName() != figureType.King)
                    return true;

                if (_board[_y, _x] == null && this.x == _x && this.y - _y == 1 )
                    return true;

                if (!isMoved && this.x == _x && this.y - _y == 2 && _board[_y, _x] == null && _board[_y + 1, _x] == null )
                    return true;

                return false;
            }
        }

        public override List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game)
        {
            List<dStruct> possibleMoves = getPossibleMovesFire(_board);
            possibleMoves.AddRange(getPossibleMovesNotFire(_board));
            return possibleMoves;
        }

        private List<dStruct> getPossibleMovesFire(figure[,] _board)
        {
            List<dStruct> possibleMoves = new List<dStruct>();

            figureColor Color = this.getColor();
            int ix;
            int iy;
            
            for (int i = 0; i <= stepForCheck[Color].Length - 1; i++)
            {
                ix = this.x + stepForCheck[Color][i].dx;
                iy = this.y + stepForCheck[Color][i].dy;
                if (ix <= 7 && ix >= 0 && iy <= 7 && iy >= 0 && _board[iy, ix] != null && _board[iy, ix].getColor() != Color)
                    possibleMoves.Add(new dStruct(ix , iy));
            }
            return possibleMoves;
        }

        private List<dStruct> getPossibleMovesNotFire(figure[,] _board)
        {
            List<dStruct> possibleMoves = new List<dStruct>();
            figureColor Color = this.getColor();
            int ix;
            int iy;

            ix = this.x + Pawn.stepForChecks[Color][0].dx;
            iy = this.y + Pawn.stepForChecks[Color][0].dy;
            if (ix <= 7 && ix >= 0 && iy <= 7 && iy >= 0 && _board[iy, ix] == null)
                possibleMoves.Add(new dStruct(ix, iy));

            ix = this.x + Pawn.stepForChecks[Color][1].dx;
            iy = this.y + Pawn.stepForChecks[Color][1].dy;
            if (ix <= 7 && ix >= 0 && iy <= 7 && iy >= 0 && _board[iy, ix] == null && _board[iy - Pawn.stepForChecks[Color][0].dy, ix - Pawn.stepForChecks[Color][0].dx] == null && !this.isMoved )
                possibleMoves.Add(new dStruct(ix, iy));

            return possibleMoves;
        }
    }
}
