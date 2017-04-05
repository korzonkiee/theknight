using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKnight
{
    public class Scenery
    {
        private short defaultBoardSize = 8;
        public short BoardSize { get; set; }

        public SceneryElement[,] Board;

        public Scenery()
        {
            Board = new SceneryElement[defaultBoardSize, defaultBoardSize];
            BoardSize = defaultBoardSize;

            GenerateRandomColors();
        }

        public void SetBoardSize(short size)
        {
            Board = new SceneryElement[size, size];
            BoardSize = size;

            GenerateRandomColors();
        }

        public void GenerateRandomColors()
        {
            var random = new Random();
            var sceneryElements = Enum.GetValues(typeof(SceneryElement));

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var randomSceneryElement = (SceneryElement) sceneryElements.
                        GetValue(random.Next(sceneryElements.Length));

                    Board[i, j] = randomSceneryElement;
                }
            }
        }

        public bool NotAvailablePosition(Position position)
        {
            return Board[position.X, position.Y] == SceneryElement.Wall;
        }
    }

    public enum SceneryElement
    {
        Grass,
        Wall
    }
}
