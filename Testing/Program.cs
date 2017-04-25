using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Testing {
    class Program {
        static void Main(string[] args) {
            JObject obj = new JObject();
            obj["Name"] = "yolo";
            obj["Direction"] = "left";

            JObject obj2 = JObject.Parse(obj.ToString().Replace(Environment.NewLine, ""));
            Console.ReadKey();
        }
    }
}