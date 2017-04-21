using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MazeLib;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// adapt the searchable problem to maze problem.
    /// </summary>
    /// <seealso cref="Position" />
    public class MazeAdapter : ISearcheble<MazeLib.Position> {
        Maze maze;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeAdapter"/> class.
        /// </summary>
        /// <param name="m">The m.</param>
        public MazeAdapter(Maze m) {
            maze = m;
        }

        /// <summary>
        /// Gets the final stateItem.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetFinalState() {
            return State<Position>.StatePool.GetState(this.maze.GoalPos);
        }

        /// <summary>
        /// Gets the initial stateItem.
        /// </summary>
        /// <returns></returns>
        public State<Position> GetInitialState() {
            return State<Position>.StatePool.GetState(this.maze.InitialPos);
        }

        /// <summary>
        /// Gets the list of n's neighbors
        /// </summary>
        /// <param name="n">The vertex .</param>
        /// <returns></returns>
        public List<State<Position>> GetListStates(State<Position> n) {
            List<State<Position>> neighbors = new List<State<Position>>();
            int cols = maze.Cols - 1;
            int rows = maze.Rows - 1;
            int x = n.stateItem.Col;
            int y = n.stateItem.Row;
            CellType free = CellType.Free;
            //up
            if (y + 1 <= rows && maze[y + 1, x] == free) {
                neighbors.Add(State<Position>.StatePool.GetState(new Position(y + 1, x)));
            }
            //right
            if (x + 1 <= cols && maze[y, x + 1] == free) {
                neighbors.Add(State<Position>.StatePool.GetState(new Position(y, x + 1)));
            }
            //down
            if (y - 1 >= 0 && maze[y - 1, x] == free) {
                neighbors.Add(State<Position>.StatePool.GetState(new Position(y - 1, x)));
            }
            //left
            if (x - 1 >= 0 && maze[y, x - 1] == free) {
                neighbors.Add(State<Position>.StatePool.GetState(new Position(y, x - 1)));
            }
            return neighbors;
        }

        /// <summary>
        /// adapts the generic solution to maze solution.
        /// </summary>
        public static class AdaptSolution {
            /// <summary>
            /// return a string that represents the directions of the solution
            /// </summary>
            /// <param name="sol">The sol.</param>
            /// <returns>the directions.</returns>
            public static string ToDirection(Solution<Position> sol) {
                // if there is no solution.
                if (sol == null) {
                    return "there is no solution";
                }
                List<Position> pos = sol.ChippestPath;
                // number of position.
                int n = pos.Count;
                StringBuilder directions = new StringBuilder("");
                // add to the string of the solution the directions
                for (int i = 1; i < n; i++) {
                    directions.Append(GetDirectionBetweenTwoPoints(pos[i - 1], pos[i]));
                }
                return directions.ToString();
            }

            /// <summary>
            /// Gets the direction between two points.
            /// </summary>
            /// <param name="source">The source.</param>
            /// <param name="dest">The dest.</param>
            /// <returns></returns>
            private static string GetDirectionBetweenTwoPoints(Position source, Position dest) {
                // if the points are in the same col
                if (source.Col == dest.Col) {
                    // down
                    if (source.Row < dest.Row) {
                        return "3";
                    }
                    // up
                    else {
                        return "2";
                    }
                }
                // if the points are in the same row
                else {
                    // right
                    if (source.Col < dest.Col) {
                        return "1";
                    }
                    // left
                    else {
                        return "0";
                    }
                }
            }
        }
    }
}