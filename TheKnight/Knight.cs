using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheKnight.Properties;

namespace TheKnight
{
    public class Knight
    {
        public Position Position;

        public Bitmap Image { get; private set; }

        public bool HasKey { get; set; } = false;

        public Knight()
        {
            Image = Resources.knight_right;
            Position = new Position(0, 0);
        }

        public Knight(Position initialPosition)
        {
            Image = Resources.knight_right;
            Position = initialPosition;
        }

        public void SetPosition(Position position, Walk walk = Walk.None)
        {
            Position = position;

            switch (walk)
            {
                case Walk.Right:
                Image = Resources.knight_right;
                break;

                case Walk.Left:
                Image = Resources.knight_left;
                break;

                default:
                break;
            }
        }
    }

    public class Position
    {
        public short X { get; set; }
        public short Y { get; set; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public static Position Create (Position position, Walk walk)
        {
            var newPosition = new Position(position.X, position.Y);

            switch (walk)
            {
                case Walk.Up:
                newPosition.Y--;
                break;

                case Walk.Right:
                newPosition.X++;
                break;

                case Walk.Down:
                newPosition.Y++;
                break;

                case Walk.Left:
                newPosition.X--;
                break;

                default:
                break;
            }

            return newPosition;
        }

        public static bool operator ==(Position p1, Position p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Position p1, Position p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }
    }
}
