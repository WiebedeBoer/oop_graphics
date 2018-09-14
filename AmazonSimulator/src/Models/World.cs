using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<Robot> worldObjects = new List<Robot>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        
        double xpos = 4.5; //global x
        double ypos = 0; //global y
        double zpos = 13; //global z

        public World() {
            //Alle 'Workers'
            Robot robot1 = CreateRobot(0,0,0,"robot");
            robot1.Move(xpos, ypos, zpos);
            Robot robot2 = CreateRobot(0,0,0,"robot");
            robot2.Move(4.6, 0, 11);
            Robot robot3 = CreateRobot(0,0,0,"robot");
            robot3.Move(4.6, 0, 9);

            //Dumptruck die pakketen afleverd en ophaalt.
            Robot dumptruck = CreateRobot(0,0,0,"dumptruck");
            dumptruck.Move(4.6, 0, 15);
        }

        private Robot CreateRobot(double x, double y, double z, string type) {
            Robot r = new Robot(x,y,z,0,0,0,type);
            worldObjects.Add(r);
            return r;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            foreach(Robot m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public bool Update(int tick)
        {
            for(int i = 0; i < worldObjects.Count; i++) {
                Robot u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);
                    xpos = xpos + 0.05;
                    u.Move(xpos, ypos, zpos);
                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
