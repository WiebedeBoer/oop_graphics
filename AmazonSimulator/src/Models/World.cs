using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<C3model> worldObjects = new List<C3model>();
        private List<Robot> robotLijst = new List<Robot>();
        private List<Kast> kastLijst = new List<Kast>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

        bool showGrid;
        
        private Robot robot1;
        private Robot robot2;
        private Robot robot3;
        private Kast kast1;
        private Kast kast2;
        private Kast kast3;
        private Kast kast4;
        private Kast kast5;
        private Kast kast6;
        private Kast kast7;
        private Dumptruck dumptruck;
        private GraphClass graphContent = new GraphClass();
        private List<Hraph> hraphObjects = new List<Hraph>();
        private Random rnd = new Random();
        private RoboTasks roboTasks;
        public World() {
            //Initializing actors and paths.
            PopuleerGraph();
            CreerActoren();
            roboTasks = new RoboTasks(worldObjects,graphContent,hraphObjects);
            showGrid = true;
        }

        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            robotLijst.Add(r);
            return r;
        }
        private Dumptruck CreateDumptruck(double x, double y, double z) {
            Dumptruck d = new Dumptruck(x,y,z,0,0,0);
            worldObjects.Add(d);
            return d;
        }
        private Kast CreateKast(double x, double y, double z, Hraph huidigeLocatie) {
            Kast d = new Kast(x,y,z,0,0,0,huidigeLocatie);
            worldObjects.Add(d);
            kastLijst.Add(d);
            return d;
        }

        /// <summary>
        /// Create all actors to populate the simulation
        /// A method is used  to make it more readable
        /// Note: is only called once! at the start of the simulation,
        /// </summary>
        private void CreerActoren() {
            //Alle 'Workers'
            robot1 = CreateRobot(7,0.15,7);
            robot2 = CreateRobot(7,0.15,7);
            robot3 = CreateRobot(7,0.15,7);
            //Dumptruck
            dumptruck = CreateDumptruck(0,0,107);
            dumptruck.Target(0,0,7);

            //Alle kasten
            kast1 = CreateKast(0,1000,0,new Hraph(7,3,7,"vrachtdepot",false));
            kast1.actorStatus = "hemel";
            kast2 = CreateKast(0,1000,0,new Hraph(7,3,7,"vrachtdepot",false));
            kast2.actorStatus = "hemel";
            kast3 = CreateKast(0,1000,0,new Hraph(7,3,7,"vrachtdepot",false));
            kast3.actorStatus = "hemel";
            kast4 = CreateKast(0,1000,0,new Hraph(7,3,7,"vrachtdepot",false));
            kast4.actorStatus = "hemel";

            kast5 = CreateKast(7,3,7,new Hraph(7,3,7,"vrachtdepot",false));
            kast5.actorStatus = "InDepotNieuw";
            kast6 = CreateKast(7,3,7,new Hraph(7,3,7,"vrachtdepot",false));
            kast6.actorStatus = "InDepotNieuw";
            kast7 = CreateKast(7,3,7,new Hraph(7,3,7,"vrachtdepot",false));
            kast7.actorStatus = "InDepotNieuw";


        }
        
        /// <summary>
        /// Create all waypoints for movement
        /// A Method is used to make it more readable
        /// Note: is only called once! at the start of the simulation,
        /// </summary>
        private void PopuleerGraph(){
            hraphObjects.Add(new Hraph(7,0,7,"vrachtdepot", false));
            hraphObjects.Add(new Hraph(7,0,5,"updepot", false));
            hraphObjects.Add(new Hraph(7,0,9,"downdepot", false));
            hraphObjects.Add(new Hraph(8,0,7,"idlevracht", false));
            hraphObjects.Add(new Hraph(8,0,5,"idleup", false));
            hraphObjects.Add(new Hraph(8,0,9,"idledown", false));
            hraphObjects.Add(new Hraph(11,0,7,"padepot", false));
            hraphObjects.Add(new Hraph(11,0,11,"pbdepot", false));
            hraphObjects.Add(new Hraph(15,0,11,"RA", false));
            hraphObjects.Add(new Hraph(19,0,11,"RB", false));
            hraphObjects.Add(new Hraph(23,0,11,"RC", false));
            hraphObjects.Add(new Hraph(23,0,15,"RD", false));
            hraphObjects.Add(new Hraph(11,0,15,"LA", false));
            hraphObjects.Add(new Hraph(11,0,19,"LB", false));
            hraphObjects.Add(new Hraph(11,0,23,"LC", false));
            hraphObjects.Add(new Hraph(15,0,23,"LD", false));
            hraphObjects.Add(new Hraph(15,0,12,"SRA", true));
            hraphObjects.Add(new Hraph(19,0,12,"SRB", true));
            hraphObjects.Add(new Hraph(22,0,15,"SRD", true));
            hraphObjects.Add(new Hraph(12,0,15,"SLA", true));
            hraphObjects.Add(new Hraph(12,0,19,"SLB", true));
            hraphObjects.Add(new Hraph(15,0,22,"SLD", true));
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
            foreach(C3model m3d in worldObjects) {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
            if (showGrid)
            {
                obs.OnNext(new ShowGridCommand(showGrid));
            }
        }
    
        public bool Update(int tick)
        {
            
            for(int i = 0; i < worldObjects.Count; i++) {
                C3model u = worldObjects[i];

                if(u is IUpdatable) {
                    bool needsCommand = ((IUpdatable)u).Update(tick);
                    if(needsCommand) {
                        SendCommandToObservers(new UpdateModel3DCommand(u));
                    }
                }
            }
            
            roboTasks.MoveKasten();
            
            dumptruck.MoveDumptruck(kastLijst,hraphObjects);
            
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