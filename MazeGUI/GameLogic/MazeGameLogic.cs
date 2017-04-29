using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.GameLogic {
    /// <summary>
    /// Implements a straight forward logic for maze game.
    /// </summary>
    /// <seealso cref="MazeGUI.GameLogic.IGameLogic" />
    class MazeGameLogic : IGameLogic {
        private Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGameLogic"/> class.
        /// </summary>
        /// <param name="maze">The maze.</param>
        public MazeGameLogic(Maze maze) {
            this.maze = maze;
        }

        /// <summary>
        /// Determines whether [is legit move] [the specified player position].
        /// </summary>
        /// <param name="playerPosition">The player position.</param>
        /// <param name="moveTo">The move to.</param>
        /// <returns>
        ///   <c>true</c> if [is legit move] [the specified player position]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLegitMove(Position playerPosition, Direction moveTo) {
            switch (moveTo) {
                case Direction.Down: {
                    if (playerPosition.Row + 1 >= 0 && playerPosition.Row + 1 <= maze.Rows - 1 &&
                        maze[playerPosition.Row + 1, playerPosition.Col] == CellType.Free) {
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
                case Direction.Left: {
                    if (playerPosition.Col - 1 >= 0 && playerPosition.Col - 1 <= maze.Cols &&
                        maze[playerPosition.Row, playerPosition.Col - 1] == CellType.Free) {
                        return true;
                    }
                }
                    break;
                case Direction.Up: {
                    if (playerPosition.Row - 1 >= 0 && playerPosition.Row - 1 <= maze.Rows - 1 &&
                        maze[playerPosition.Row - 1, playerPosition.Col] == CellType.Free) {
                        return true;
                    }
                }
                    break;
                case Direction.Unknown: {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Moves the specified player position.
        /// </summary>
        /// <param name="playerPosition">The player position.</param>
        /// <param name="moveToDirection">The move to direction.</param>
        /// <returns>
        /// The new position after the movement.
        /// </returns>
        public Position Move(Position playerPosition, Direction moveToDirection) {
            if (IsLegitMove(playerPosition, moveToDirection)) {
                switch (moveToDirection) {
                    case Direction.Down: {
                        return new Position(playerPosition.Row + 1, playerPosition.Col);
                    }
                        break;
                    case Direction.Right: {
                        return new Position(playerPosition.Row, playerPosition.Col + 1);
                    }
                        break;
                    case Direction.Left: {
                        return new Position(playerPosition.Row, playerPosition.Col - 1);
                    }
                        break;
                    case Direction.Up: {
                        return new Position(playerPosition.Row - 1, playerPosition.Col);
                    }
                        break;
                    case Direction.Unknown: {
                        return playerPosition;
                    }
                }
            }
            return playerPosition;
        }
    }
}