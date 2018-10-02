using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class World : IObservable<Command>, IUpdatable
    {
        private List<C3model> worldObjects = new List<C3model>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        
        double xpos = 4.5; //global x
        double ypos = 0; //global y
        double zpos = 13; //global z

        Robot robot1;
        Robot robot2;
        Robot robot3;
        Kast kast1;
        Kast kast2;
        Kast kast3;
        Dumptruck dumptruck;
        GraphClass graphContent = new GraphClass();
        public List<Hraph> HraphObjects = new List<Hraph>();

        public World() {
            //Initialiseren van actoren binnen de simulatie en de paths.
            CreerActoren();
            PopuleerGraph();

            //Dumptruck die pakketen afleverd en ophaalt.
            dumptruck = CreateDumptruck(0,1000,0,"dumptruck");
            dumptruck.Move(1.6, 0, 45);
            dumptruck.Target(1.6, 0, 7);



            //dumptruck.Target(1.6,0,7);

            //TODO: Maak simulatie dat alle acteurs continu beweegt en simuleerd.
        }

        private Robot CreateRobot(double x, double y, double z, string type) {
            Robot r = new Robot(x,y,z,0,0,0);
            worldObjects.Add(r);
            return r;
        }

        private Dumptruck CreateDumptruck(double x, double y, double z, string type) {
            Dumptruck d = new Dumptruck(x,y,z,0,0,0);
            worldObjects.Add(d);
            return d;
        }

        private Kast CreateKast(double x, double y, double z, string type) {
            Kast d = new Kast(x,y,z,0,0,0);
            worldObjects.Add(d);
            return d;
        }

        //Word 1x aangeroept. creert en plaatst alle actoren
        private void CreerActoren() {
            //Alle 'Workers'
            robot1 = CreateRobot(0,0,0,"robot");
            robot1.Move(xpos, ypos, zpos);
            robot2 = CreateRobot(0,0,0,"robot");
            robot2.Move(4.6, 0, 11);
            robot3 = CreateRobot(0,0,0,"robot");
            robot3.Move(0, 0, 3);

            //Alle kasten
            kast1 = CreateKast(0,1000,0,"kast");
            kast2 = CreateKast(0,1000,0,"kast");
            kast3 = CreateKast(0,1000,0,"kast");            

        }

        //droppen van cargo
        private void CargoDropped(){
            kast1.Move(6.8, 3.25, 6.2);
            kast2.Move(6.8, 3.25, 6.6);
            kast3.Move(6.8, 3.25, 7.4);
        }

        //starten bewegen robots
        private void MoveAllRobots(){
            robot1.goTo(graphContent.GetPath("vrachtdepot","LC"),HraphObjects);
            robot2.goTo(graphContent.GetPath("vrachtdepot","LD"),HraphObjects);
            robot3.goTo(graphContent.GetPath("vrachtdepot","RD"),HraphObjects);
        }           


        //Er worden aangelegd
        private void PopuleerGraph(){
            HraphObjects.Add(new Hraph(7,0,7,"vrachtdepot"));
            HraphObjects.Add(new Hraph(22,0,5,"RA"));
            HraphObjects.Add(new Hraph(24,0,5,"RB"));
            HraphObjects.Add(new Hraph(26,0,5,"RC"));
            HraphObjects.Add(new Hraph(28,0,5,"RD"));
            HraphObjects.Add(new Hraph(22,0,9,"LA"));
            HraphObjects.Add(new Hraph(24,0,9,"LB"));
            HraphObjects.Add(new Hraph(26,0,9,"LC"));
            HraphObjects.Add(new Hraph(28,0,9,"LD"));
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

            if (dumptruck.Cargodrop ==true){
                CargoDropped();
                MoveAllRobots(); 
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