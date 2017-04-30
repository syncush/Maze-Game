using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeLib;
using MazeGUI.MazeUserControl;

namespace MazeGUI.Utilities {
    /// <summary>
    /// Converts premitive type to object
    /// </summary>
    static class Converter {
        /// <summary>
        /// Algoes to int.
        /// </summary>
        /// <param name="algo">The algo.</param>
        /// <returns></returns>
        public static int AlgoToInt(Algorithm algo) {
            switch (algo) {
                case Algorithm.BFS: {
                    return 1;
                }
                    break;
                case Algorithm.DFS: {
                    return 0;
                }
                    break;
            }
            return -1;
        }

        /// <summary>
        /// Froms the direction to new position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="direct">The direct.</param>
        /// <returns></returns>
        public static Position FromDirectionToNewPosition(Position pos, Direction direct) {
            switch (direct) {
                case Direction.Up: {
                    return new Position(pos.Row - 1, pos.Col);
                }
                    break;
                case Direction.Down: {
                    return new Position(pos.Row + 1, pos.Col);
                }
                    break;
                case Direction.Right: {
                    return new Position(pos.Row, pos.Col + 1);
                }
                    break;
                case Direction.Left: {
                    return new Position(pos.Row, pos.Col - 1);
                }
                    break;
                case Direction.Unknown: {
                    return pos;
                }
                    break;
            }
            return pos;
        }


        /// <summary>
        /// Strings to direction.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static Direction StringToDirection(string str) {
            Direction parseDirection;
            switch (str) {
                case "Up":
                case "up": {
                    parseDirection = Direction.Up;
                }
                    break;
                case "Down":
                case "down": {
                    parseDirection = Direction.Down;
                }
                    break;
                case "Left":
                case "left": {
                    parseDirection = Direction.Left;
                }
                    break;
                case "Right":
                case "right": {
                    parseDirection = Direction.Right;
                }
                    break;
                default: {
                    parseDirection = Direction.Unknown;
                }
                    break;
            }
            return parseDirection;
        }

        /// <summary>
        /// Characters to position.
        /// </summary>
        /// <param name="prev">The previous.</param>
        /// <param name="num">The number.</param>
        /// <returns></returns>
        public static Position CharToPosition(Position prev, char num) {
            Direction direct;
            switch (num) {
                case '0': {
                    direct = Direction.Left;
                }
                    break;

                case '1': {
                    direct = Direction.Right;
                }
                    break;

                case '2': {
                    direct = Direction.Up;
                }
                    break;

                case '3': {
                    direct = Direction.Down;
                }
                    break;
                default: {
                    direct = Direction.Unknown;
                }
                    break;
            }
            return Converter.FromDirectionToNewPosition(prev, direct);
        }

        /// <summary>
        /// Mazes to representation.
        /// </summary>
        /// <param name="convert">The convert.</param>
        /// <param name="maze">The maze.</param>
        /// <param name="solList">The sol list.</param>
        /// <param name="playerPosition">The player position.</param>
        /// <returns></returns>
        public static string MazeToRepresentation(String[,] array, Maze maze, List<Position> solList, Position playerPosition) {
            //string[,] stringMaze = array;

            for (int i = 0; i < maze.Rows; i++) {
                for (int j = 0; j < maze.Cols; j++) {
                    array[i, j] = maze[i, j] == CellType.Free ? MazeBoard.FreeSpaceRep.ToString() : MazeBoard.WallRep.ToString();
                }
            }
            if (solList != null) {
                foreach (Position pos in solList) {
                    array[pos.Row, pos.Col] = MazeBoard.SolutionBrickRep.ToString();
                }
            }
            array[playerPosition.Row, playerPosition.Col] = MazeBoard.PlayerRep.ToString();
            array[maze.GoalPos.Row, maze.GoalPos.Col] = MazeBoard.GoalRep.ToString();
            string b = "";
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            for (int i = 0; i < rows; ++i) {
                for (int j = 0; j < cols; ++j) {
                    b += array[i, j];
                }
                b += ',';
            }
            b = b.Substring(0, b.Length - 1);
            return b;
        }
    }
}