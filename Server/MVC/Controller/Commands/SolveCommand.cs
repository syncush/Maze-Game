using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
using System.IO;
using MazeLib;
using Newtonsoft.Json.Linq;
using Server;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Describes a solve command.
    /// </summary>
    /// <seealso cref="ServerLib.Command" />
    class SolveCommand : Command {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolveCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SolveCommand(IModel model) : base(model) {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// JSON result of the command
        /// </returns>
        public override string ExecuteCommand(string[] args, IPlayer player) {
            lock (this.lockRaceCondition) {
                int typeOfAlgo;
                string name = args[0];
                //try to convert args.
                try {
                    typeOfAlgo = int.Parse(args[1]);
                }
                catch (Exception e) { //Throw bad arguments to a integer field.
                    throw new GameException("Failed converting arguments at solve command.", true);
                }
                //Convert the integer to the algorihm enum.
                Algortihm algo = typeOfAlgo == 1
                    ? Algortihm.DFS
                    : typeOfAlgo == 0
                        ? Algortihm.BFS
                        : throw new GameException("Bad algorithm arg", true);
                //Get the solution from the model.
                Solution<Position> solution = this.model.Solve(name, algo);
                //If model failed to solve , throw exception.
                if (solution == null) {
                    throw new GameException(String.Format("Failed solving {0}, please check parameters given!"),
                        true);
                }
                string directions = MazeAdapter.AdaptSolution.ToDirection(solution);
                // add to the string the number of nods evaluated.
                JObject mazeObj = new JObject();
                mazeObj["Name"] = name;
                mazeObj["Solution"] = directions.ToString();
                mazeObj["NodesEvaluated"] = solution.EvaluatedNodes;
                player.SendMessage(mazeObj.ToString());
                player.CloseConnection();
                return "shutdown";
            }
        }
    }
}