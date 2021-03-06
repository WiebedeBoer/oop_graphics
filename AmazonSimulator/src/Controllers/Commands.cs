using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Newtonsoft.Json;

namespace Controllers {
    //all commands are being created here
    public abstract class Command {

        private string type;
        private Object parameters;

        public Command(string type, Object parameters) {
            this.type = type;
            this.parameters = parameters;
        }

        public string ToJson() {
            return JsonConvert.SerializeObject(new {
                command = type,
                parameters = parameters
            });
        }
    }

    public abstract class Model3DCommand : Command {

        public Model3DCommand(string type, C3model parameters) : base(type, parameters) {
        }
    }

    public class UpdateModel3DCommand : Model3DCommand {
        
        public UpdateModel3DCommand(C3model parameters) : base("update", parameters) {
        }
    }

    //Command ShowGrid sends a message to the client, telling it that it should show the grid.
    public class ShowGridCommand : Command {
        
        public ShowGridCommand(bool parameters) : base("grid", parameters) {
        }
    }
}