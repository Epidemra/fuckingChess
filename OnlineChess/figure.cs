using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineChess
{
    public enum figureColor { Black, White }

    public abstract class figure
    {
        public int x;
        public int y;
        private figureColor color;
        private figureType name;
        public bool isMoved;
        public bool isRepeat;

        public figure(int _x, int _y, figureColor _color, figureType _name)
        {
            this.x = _x;
            this.y = _y;
            this.color = _color;
            this.name = _name;
            this.isMoved = false;            
        }

        public figureColor getColor()
        {
            return this.color;
        }

        public figureType getName()
        {
            return this.name;
        }

        //принимает коорд направления
        public abstract bool isValidMove(int _x, int _y, figure[,] _board);
        public abstract List<dStruct> getPossibleMoves(figure[,] _board, ChessModel _game);

        public bool isCorrectDia(int _x, int _y)
        {
            return (Math.Abs(this.x - _x) == Math.Abs(this.y - _y));
        }

        public bool isEmptyRoadDia(int _x, int _y, figure[,] _board)
        {
            if (_board[_y, _x] != null && (_board[_y, _x].getColor() == this.getColor() || _board[_y, _x].getName() == figureType.King))
                return false;

            int dx = _x - this.x > 0 ? 1 : -1;
            int dy = _y - this.y > 0 ? 1 : -1;

            int ix = this.x + dx;
            int iy = this.y + dy;

            while(ix != _x && iy != _y)
            {
                if (_board[iy, ix] != null)
                    return false;

                ix += dx;
                iy += dy;
            }

            return true;
        }

        public bool isCorrectLong(int _x, int _y)
        {
            return ((this.x == _x && this.y != _y) || (this.x != _x && this.y == _y));
        }

        public bool isEmptyRoadLong(int _x, int _y, figure[,] _board)
        {
            if (_board[_y, _x] != null && (_board[_y, _x].getColor() == this.getColor() || _board[_y, _x].getName() == figureType.King))
                return false;

            int dx;
            int dy;

            if (this.x == _x)
            {
                dy = _y - this.y > 0 ? 1 : -1;
                dx = 0;
            }
            else
            {
                dx = _x - this.x > 0 ? 1 : -1;
                dy = 0;
            }

            int i = (dx == 0 ? this.y : this.x) + dx + dy;
            int d = dx == 0 ? _y : _x;
            int ix = this.x + dx;
            int iy = this.y + dy;

            while(i != d)
            {
                if (_board[iy, ix] != null)
                    return false;

                ix += dx;
                iy += dy;
                i += dy + dx;
            }

            return true;
        }
    }
}
