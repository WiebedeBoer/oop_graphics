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
        
        Robot robot1;
        Robot robot2;
        Robot robot3;
        Kast kast1;
        Kast kast2;
        Kast kast3;
        Kast kast4;
        Kast kast5;
        Kast kast6;
        Kast kast7;
        Dumptruck dumptruck;
        GraphClass graphContent = new GraphClass();
        public List<Hraph> HraphObjects = new List<Hraph>();
        Random rnd = new Random();
        Robotasks robotasks;
        public World() {
            //Initialiseren van actoren binnen de simulatie en de paths.
            PopuleerGraph();
            CreerActoren();
            robotasks = new Robotasks(worldObjects,graphContent,HraphObjects);
        }

        //maakt robot
        private Robot CreateRobot(double x, double y, double z) {
            Robot r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            robotLijst.Add(r);
            return r;
        }

        //maakt dumptruck
        private Dumptruck CreateDumptruck(double x, double y, double z) {
            Dumptruck d = new Dumptruck(x,y,z,0,0,0);
            worldObjects.Add(d);
            return d;
        }

        //maakt kast
        private Kast CreateKast(double x, double y, double z, string type, bool cargoed) {
            Kast d = new Kast(x,y,z,0,0,0,null,false);
            worldObjects.Add(d);
            kastLijst.Add(d);
            return d;
        }

        //Word 1x aangeroept. creert en plaatst alle actoren
        private void CreerActoren() {
            //Alle 'Workers'
           robot1 = CreateRobot(0,0,0);
            robot1.Move(7,0,7);
            robot2 = CreateRobot(0,0,0);
            robot2.Move(7,0,7);
            robot3 = CreateRobot(0,0,0);
            robot3.Move(7,0,7);
            //Dumptruck
            dumptruck = CreateDumptruck(0,0,107);
            dumptruck.Target(0,0,7);

            //Alle kasten
            kast1 = CreateKast(0,1000,0,null, false);
            kast1.actorStatus = "hemel";
            kast2 = CreateKast(0,1000,0,null, false);
            kast2.actorStatus = "hemel";
            kast3 = CreateKast(0,1000,0,null, false);
            kast3.actorStatus = "hemel";
            kast4 = CreateKast(0,1000,0,null, false);
            kast4.actorStatus = "hemel";

            kast5 = CreateKast(7,0,7,"vrachtdepot", true);
            kast5.actorStatus = "InDepotNieuw";
            kast5.huidigeLocatie = new Hraph(7,0,7,"vrachtdepot", false);
            kast6 = CreateKast(7,0,7,"vrachtdepot", false);
            kast6.actorStatus = "InDepotNieuw";
            kast6.huidigeLocatie = new Hraph(7,0,7,"vrachtdepot", false);
            kast7 = CreateKast(7,0,7,"vrachtdepot", false);
            kast7.actorStatus = "InDepotNieuw";
            kast7.huidigeLocatie = new Hraph(7,0,7,"vrachtdepot", false);


        }
        
        //word 1x aangeroepen, maak nodes aan in wereld
        private void PopuleerGraph(){
            HraphObjects.Add(new Hraph(7,0,7,"vrachtdepot", false));
            HraphObjects.Add(new Hraph(7,0,5,"updepot", false));
            HraphObjects.Add(new Hraph(7,0,9,"downdepot", false));
            HraphObjects.Add(new Hraph(8,0,7,"idlevracht", false));
            HraphObjects.Add(new Hraph(8,0,5,"idleup", false));
            HraphObjects.Add(new Hraph(8,0,9,"idledown", false));
            HraphObjects.Add(new Hraph(11,0,7,"padepot", false));
            HraphObjects.Add(new Hraph(11,0,11,"pbdepot", false));
            HraphObjects.Add(new Hraph(15,0,11,"RA", false));
            HraphObjects.Add(new Hraph(19,0,11,"RB", false));
            HraphObjects.Add(new Hraph(23,0,11,"RC", false));
            HraphObjects.Add(new Hraph(23,0,15,"RD", false));
            HraphObjects.Add(new Hraph(11,0,15,"LA", false));
            HraphObjects.Add(new Hraph(11,0,19,"LB", false));
            HraphObjects.Add(new Hraph(11,0,23,"LC", false));
            HraphObjects.Add(new Hraph(15,0,23,"LD", false));
            HraphObjects.Add(new Hraph(15,0,12,"SRA", true));
            HraphObjects.Add(new Hraph(19,0,12,"SRB", true));
            HraphObjects.Add(new Hraph(22,0,15,"SRD", true));
            HraphObjects.Add(new Hraph(12,0,15,"SLA", true));
            HraphObjects.Add(new Hraph(12,0,19,"SLB", true));
            HraphObjects.Add(new Hraph(15,0,22,"SLD", true));
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
            
            robotasks.MoveKasten();
            
            dumptruck.MoveDumptruck(kastLijst,HraphObjects);
            
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