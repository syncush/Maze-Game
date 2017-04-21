using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeLib;
using MazeGeneratorLib;
using Newtonsoft.Json.Linq;
using SearchAlgorithmsLib;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Concrete Model who handles the clients requests and holds all the info.
    /// </summary>
    /// <seealso cref="IModel" />
     class Model : IModel {
        /// <summary>
        /// The single player database holds the maze and it's solutions.
        /// </summary>
        private Dictionary<string, Tuple<Maze, Solution<MazeLib.Position>, Solution<MazeLib.Position>>>
            singlePlayerDB;

        /// <summary>
        /// The multi player database.
        /// </summary>
        private Dictionary<string, MultiPlayerInfoPackage> multiPlayerDB;

        /// <summary>
        /// The maze generator.
        /// </summary>
        IMazeGenerator mazeGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model() {
            this.singlePlayerDB =
                new Dictionary<string, Tuple<Maze, Solution<MazeLib.Position>, Solution<MazeLib.Position>>>();
            this.multiPlayerDB = new Dictionary<string, MultiPlayerInfoPackage>();
            mazeGenerator = new DFSMazeGenerator();
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>
        /// The maze by the properties.
        /// </returns>
        public Maze GenerateMaze(string name, int rows, int cols) {
            if (rows >= 0 && cols >= 0) {
                if (!singlePlayerDB.ContainsKey(name)) {
                    Maze maze = mazeGenerator.Generate(rows, cols);
                    maze.Name = name;
                    singlePlayerDB.Add(name,
                        new Tuple<Maze, Solution<MazeLib.Position>, Solution<MazeLib.Position>>(maze, null, null));
                    return maze;
                }
            }
            return null;
        }

        /// <summary>
        /// Solves the specified maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns>
        /// solves the maze and returns the solution
        /// </returns>
        /// <exception cref="System.Exception">Failed at Solve function in model class</exception>
        public Solution<Position> Solve(string name, Algortihm algorithm) {
            Tuple<Maze, Solution<MazeLib.Position>, Solution<MazeLib.Position>> mazeAndSol;
            // if the maze is exists
            if (singlePlayerDB.ContainsKey(name)) {
                mazeAndSol = singlePlayerDB[name];
                Solution<MazeLib.Position> sol = null;
                // turn the maze to a graph problem that bfs and dfs can solve.
                MazeAdapter mazeAdapter = new MazeAdapter(mazeAndSol.Item1);
                // swiches between the algoritms.
                switch (algorithm) {
                    case Algortihm.BFS: {
                        // check if there is a solution in cache
                        if (mazeAndSol.Item2 != null) {
                            sol = mazeAndSol.Item2;
                        }
                        else {
                            WeightForEdges<MazeLib.Position> we = new WeightsForShortestWay<MazeLib.Position>();
                            ISearcher<Position> bfs = new BFS<MazeLib.Position>(we);
                            // solve the maze with bfs.
                            sol = bfs.Search(mazeAdapter);
                            mazeAndSol.Item2 = sol;
                        }
                    }
                        break;
                    case Algortihm.DFS: {
                        // check if there is a solution in cache
                        if (mazeAndSol.Item3 != null) {
                            sol = mazeAndSol.Item3;
                        }
                        else {
                            ISearcher<Position> dfs = new DFS<Position>();
                            // solves the maze with dfs.
                            sol = dfs.Search(mazeAdapter);
                            mazeAndSol.Item3 = sol;
                        }
                    }
                        break;
                    default:
                        return null;
                }
                return sol;
            }
            return null;
        }


        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>
        /// the list of the free games
        /// </returns>
        public string[] List() {
            List<string> freeGamesList = new List<string>();
            foreach (string item in multiPlayerDB.Keys) {
                if (multiPlayerDB[item].Guest == null) {
                    freeGamesList.Add(item);
                }
            }
            return freeGamesList.ToArray();
        }

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="host">The host.</param>
        public Maze Start(string name, int rows, int cols, IPlayer host) {
            Maze maze = null;
            if (rows >= 0 && cols >= 0) {
                maze = this.GenerateMaze(name, rows, cols);
                this.multiPlayerDB.Add(name, new MultiPlayerInfoPackage(host, maze));
                /**
                while (multiPlayerDB[name].Guest == null) {
                    Thread.Sleep(10);
                }
                 **/
            }
            return maze;
        }

        /// <summary>
        /// Joins the game specified by the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="guesTcpClient"></param>
        /// <exception cref="System.Exception">Asking to Join a non existing game</exception>
        public MultiPlayerInfoPackage Join(string name, IPlayer guesTcpClient) {
            if (!multiPlayerDB.ContainsKey(name)) {
                return null;
            }
            multiPlayerDB[name].Guest = guesTcpClient;
            return multiPlayerDB[name];
        }

        /// <summary>
        /// Gets the other.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <returns></returns>
        public IPlayer GetEnemy(IPlayer first) {
            foreach (string item in multiPlayerDB.Keys) {
                if (multiPlayerDB[item].Host.Equals(first)) {
                    return multiPlayerDB[item].Guest;
                }
                if (multiPlayerDB[item].Guest.Equals(first)) {
                    return multiPlayerDB[item].Host;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the name of the maze.
        /// </summary>
        /// <param name="first">The first.</param>
        /// <returns>The maze where the client is playing</returns>
        private String GetGameName(IPlayer first) {
            foreach (string item in multiPlayerDB.Keys) {
                if (multiPlayerDB[item].Host.Equals(first)) {
                    return item;
                }
                if (multiPlayerDB[item].Guest.Equals(first)) {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// the game specified by the name
        /// </returns>
        /// <exception cref="System.Exception">No existing game</exception>
        public MultiPlayerInfoPackage GetGame(String name) {
            if (!this.multiPlayerDB.ContainsKey(name)) {
                throw new Exception("No existing game");
            }
            return this.multiPlayerDB[name];
        }


        /// <summary>
        /// Closes the specified name.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        public Boolean Close(string name) {
            if (multiPlayerDB.ContainsKey(name)) {
                multiPlayerDB[name].Clear();
                multiPlayerDB.Remove(name);
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>null if not found , InfoPackage of the player in the session.</returns>
        public MultiPlayerInfoPackage GetGame(IPlayer player) {
            foreach (string item in multiPlayerDB.Keys) {
                if (multiPlayerDB[item].Host.Equals(player)) {
                    return multiPlayerDB[item];
                }
                if (multiPlayerDB[item].Guest.Equals(player)) {
                    return multiPlayerDB[item];
                }
            }
            return null;
        }
    }
}