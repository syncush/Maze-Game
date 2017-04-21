using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Microsoft.SqlServer.Server;
using SearchAlgorithmsLib;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
namespace ServerLib {
    enum PlayerRequester
    {
        HOST,
        CLIENT
    };

    enum Algortihm
    {
        BFS = 0,
        DFS = 1,
        UNKNOWN = 2
    };
    interface IModel {
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns> The maze by the properties. </returns>
        Maze GenerateMaze(string name, int rows, int cols);

        /// <summary>
        /// Solves the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algorithm">The algorithm.</param>
        /// <returns>solves the maze and returns the solution</returns>
        Solution<Position> Solve(string name, Algortihm algorithm);

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="host">The host.</param>
        /// <returns>the maze the client plays</returns>
        Maze Start(string name, int rows, int cols, IPlayer host = null);

        /// <summary>
        /// List all the games that a player can join.
        /// </summary>
        /// <returns>all the names of the games.</returns>
        string[] List();

        /// <summary>
        /// Joins the game specified by the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="guesTcpClient">The gues TCP client.</param>
        /// <returns>the maze the client plays</returns>
        MultiPlayerInfoPackage Join(string name, IPlayer guesTcpClient = null);

        /// <summary>
        /// Closes the specified name.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <returns>true if succeeded else if not</returns>
        Boolean Close(string name);

        /// <summary>
        /// Gets the enemy.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        IPlayer GetEnemy(IPlayer other);

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>the game specified by the name</returns>
        MultiPlayerInfoPackage GetGame(String name);

        /// <summary>
        /// Gets the game.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns></returns>
        MultiPlayerInfoPackage GetGame(IPlayer player);
    }
}