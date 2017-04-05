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
        private List<Position> availablePositions = new List<Position>();

        private Position KeySpawn;
        private Position DoorSpawn;
        private Position KnightSpawn;

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
            availablePositions.Clear();

            var random = new Random();
            var sceneryElements = Enum.GetValues(typeof(SceneryElement));

            for (short i = 0; i < BoardSize; i++)
            {
                for (short j = 0; j < BoardSize; j++)
                {
                    var randomSceneryElement = (SceneryElement) sceneryElements.
                        GetValue(random.Next(sceneryElements.Length));

                    if (randomSceneryElement == SceneryElement.Grass)
                        availablePositions.Add(new Position(i, j));

                    Board[i, j] = randomSceneryElement;
                }
            }

            ApplyRandomPosition(ref KnightSpawn);
            ApplyRandomPosition(ref KeySpawn);
            ApplyRandomPosition(ref DoorSpawn);
        }

        public Position GetKnightSpawn()
        {
            return KnightSpawn;
        }

        public Position GetKeySpawn()
        {
            return KeySpawn;
        }

        public Position GetDoorSpawn()
        {
            return DoorSpawn;
        }

        public bool NotAvailablePosition(Position position)
        {
            return Board[position.X, position.Y] == SceneryElement.Wall;
        }

        private void ApplyRandomPosition(ref Position position)
        {
            var random = new Random();
            var randomIndex = random.Next(availablePositions.Count);

            var randomPosition = availablePositions
                .ElementAt(randomIndex);

            availablePositions.RemoveAt(randomIndex);

            position = randomPosition;
        }
    }

    public enum SceneryElement
    {
        Grass,
        Wall
    }
}
