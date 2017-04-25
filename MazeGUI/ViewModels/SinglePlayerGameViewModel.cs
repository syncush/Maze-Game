using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels
{
    class SinglePlayerGameViewModel : INotifyPropertyChanged {
        #region DataMembers
        private Maze mazeGame;
        private TcpClient serverConnection;
        private Position playerPosition;
        private Boolean stop;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public SinglePlayerGameViewModel(Maze maze, TcpClient client)
        {
            this.mazeGame = maze;
            this.playerPosition = maze.InitialPos;
            this.serverConnection = client;
            Task t = new Task(() => {
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;
                try
                {
                    while (!stop) {
                        string answer = reader.ReadLine();
                        JObject obj = JObject.Parse(answer);


                    }
                }
                catch (IOException)
                {
                    throw new Exception("Thread failed listening to games");
                }
            });
        }

        

        #region Funcs

        

       
        public void RestartGame() {
            this.playerPosition = mazeGame.InitialPos;
        }

        public void Move(string Direction) {
            
        }
        #endregion

        #region Properties

        public string GameName {
            set { }
            get { return this.GameName; }
        }

        public Maze Game {
            get { return this.mazeGame; }
        }
        

        #endregion



    }
}
