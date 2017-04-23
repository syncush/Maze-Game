using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Server;

namespace ServerLib {
     class ListCommand : Command {
        public ListCommand(IModel model) : base(model) {
        }
        public override string ExecuteCommand(string[] args, IPlayer client = null) {
            lock (this.lockRaceCondition) {
                string[] games = this.model.List();
                JArray jobJArray = new JArray();
                foreach (string item in games) {
                    jobJArray.Add(item);
                }
                client.SendMessage(jobJArray.ToString().Replace("\r\n", ""));
                return "keep";
            }
        }
    }
}