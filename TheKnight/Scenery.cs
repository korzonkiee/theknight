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
                    if (HasWallNeighbour(i, j))
                    {
                        if (random.Next(2) == 0)
                            Board[i, j] = SceneryElement.Wall;
                        else
                        {
                            Board[i, j] = SceneryElement.Grass;
                            availablePositions.Add(new Position(i, j));
                        }
                    }
                    else
                    {
                        if (random.Next(5) == 0)
                            Board[i, j] = SceneryElement.Wall;
                        else
                        {
                            Board[i, j] = SceneryElement.Grass;
                            availablePositions.Add(new Position(i, j));
                        }
                    }
                }
            }

            ApplyRandomPosition(ref KnightSpawn);
            ApplyRandomPosition(ref KeySpawn);
            ApplyRandomPosition(ref DoorSpawn);
        }

        private bool HasWallNeighbour(int x, int y)
        {
            if (x < 0 || y < 0 || x >= BoardSize || y >= BoardSize)
            {
                if (Board[x + 1, y] == SceneryElement.Wall ||
                    Board[x - 1, y] == SceneryElement.Wall ||
                    Board[x, y + 1] == SceneryElement.Wall ||
                    Board[x, y - 1] == SceneryElement.Wall)
                    return true;
            }

            return false;
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
        
        public void SetDoorSpawn(Position position)
        {
            DoorSpawn = position;
        }

        public void SetKnightSpawn(Position position)
        {
            KnightSpawn = position;
        }

        public void SetKeySpawn(Position position)
        {
            KeySpawn = position;
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
