using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.GameLogic
{
    class MazeGameLogic : IGameLogic {
        private Maze maze;

        public MazeGameLogic(Maze maze) {
            this.maze = maze;
        }
        public bool IsLegitMove(Position playerPosition, Direction moveTo) {
            switch (moveTo) {
                case Direction.Down: {
                    if (playerPosition.Row - 1 >= 0 && playerPosition.Row - 1 <= maze.Rows - 1 &&
                        maze[playerPosition.Row - 1, playerPosition.Col] == CellType.Free) {
                        return true;
                    }
                }
                    break;
                case Direction.Right: {
                    if (playerPosition.Col + 1 >= 0 && playerPosition.Col + 1 <= maze.Cols - 1 &&
                        maze[playerPosition.Row, playerPosition.Col + 1] == CellType.Free) {
                        return true;
                    }
                }
                    break;
                case Direction.Left:
                {

                }
                    break;
                case Direction.Up:
                {
                        

                }
                    break;
                case Direction.Unknown: {
                    return true;
                }
            }
            return false;
        }

        public Position Move(Position playerPosition, Direction moveToDirection)
        {
            throw new NotImplementedException();
        }
    }
}
