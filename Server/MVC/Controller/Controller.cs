using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Describes a controller.
    /// </summary>
    /// <seealso cref="ServerLib.IController" />
     class Controller : IController {
        /// <summary>
        /// The dictionary of functions and their names.
        /// </summary>
        private Dictionary<string, Command> dictionary;

        /// <summary>
        /// The model who is gonna handle the requests by users.
        /// </summary>
        private IModel model;

        public Controller() {
            this.model = new Model();
            this.dictionary = new Dictionary<string, Command>();
            //Add all the supported commands.
            this.dictionary.Add("generate", new MazeGeneratorCommand(this.model));
            this.dictionary.Add("solve", new SolveCommand(this.model));
            this.dictionary.Add("start", new StartCommand(this.model));
            this.dictionary.Add("join", new JoinCommand(this.model));
            this.dictionary.Add("list", new ListCommand(this.model));
            this.dictionary.Add("play", new PlayCommand(this.model));
            this.dictionary.Add("close", new CloseCommand(this.model));
        }

        /// <summary>
        /// Executes the command requested by user
        /// </summary>
        /// <param name="arg">The arguments of the command and the command name.</param>
        /// <param name="client">The client who sent the request.</param>
        /// <returns></returns>
        public string ExecuteCommand(string arg, Player player = null, TcpClient client = null) {
            try {
                if (arg != null) {
                    string[] args = arg.Trim().Replace(" ", "()").Replace(")(", "").Replace("()", " ").Split(' ');
                    string command = args[0].ToLower();
                    if (!dictionary.ContainsKey(command)) {
                        //Command  not found.
                        throw new GameException("Error,Unknown command , please try again !", true);
                    }
                    //Command found , import from dictionary and execute it.
                    ICommand commandExectunior = dictionary[command];
                    return commandExectunior.ExecuteCommand(args.Skip(1).ToArray(), player);
                }
                return "";
            }
            catch (GameException exception) { // if caught and exception throw it to the client handler.
                throw exception;
            }
        }
    }
}