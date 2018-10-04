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
        
        //double xpos = 4.5; //global x
        //double ypos = 0; //global y
        //double zpos = 13; //global z

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

        private bool Cargocarry;


        public World() {
            //Initialiseren van actoren binnen de simulatie en de paths.
            CreerActoren();
            PopuleerGraph();

            //Dumptruck die pakketen afleverd en ophaalt.
            dumptruck = CreateDumptruck(0,1000,0);
            dumptruck.Move(1.6, 0, 45);
            dumptruck.Target(1.6, 0, 7);

            //TODO: Maak simulatie dat alle acteurs continu beweegt en simuleerd.
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

            //Alle kasten
            kast1 = CreateKast(0,1000,0,null, false);
            kast2 = CreateKast(0,1000,0,null, false);
            kast3 = CreateKast(0,1000,0,null, false);

            kast4 = CreateKast(26,3,5,null, false);
            kast4.actorStatus = "opgeslagen";
            kast4.huidigeLocatie = new Hraph(26,0,5,"RC");
            kast5 = CreateKast(28,3,5,null, false);
            kast5.actorStatus = "opgeslagen";
            kast5.huidigeLocatie = new Hraph(28,0,5,"RD");
            kast6 = CreateKast(26,3,9,null, false);
            kast6.actorStatus = "opgeslagen";
            kast6.huidigeLocatie = new Hraph(26,0,9,"LC");
            kast7 = CreateKast(28,3,9,null, false);
            kast7.actorStatus = "opgeslagen";
            kast7.huidigeLocatie = new Hraph(28,0,9,"LD");


        }

        //droppen van cargo
        private void CargoDropped(){
            kast1.Move(6.8, 3.25, 6.2);
            kast2.Move(6.8, 3.25, 6.6);
            kast3.Move(6.8, 3.25, 7.4);
        }

        //oppakken van cargo
        private void CargoPicked(){
            dumptruck.Target(1.6, 0, -45);
        }

        public void PopuleerGraph(){
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

            //20 updates per seconde. 20x 1% kans per seconde om een actie te laten gebeuren.
            if (rnd.Next(10000) <= 100){
                Boolean gaVerderFlag = false;
                //Ophalen van een pakket
                foreach (C3model o in worldObjects){
                    //Een flag voor als een robot is gevonden voor een kast
                    if (gaVerderFlag == true){break;}

                    //ben jij een kast? nee? volgende candidaat
                    Kast kast;
                    if(o is Kast) {
                        kast = (Kast)o;
                    } else {
                        continue;
                    }

                        if(kast.actorStatus == "opgeslagen" || kast.actorStatus == "InDepot"){
                            foreach (C3model p in worldObjects)
                            {
                                Robot robot;
                                if(p is Robot) {
                                    robot = (Robot)p;
                                } else {
                                    continue;
                                }
                                if(robot.actorStatus == "idle"){
                                    //Ophalen kast
                                    if(rnd.Next(100) <= 60 && kast.actorStatus == "opgeslagen"){
                                        var rijs = graphContent.GetPath("vrachtdepot",kast.huidigeLocatie.nodeName);
                                        var terugReis = graphContent.GetPath(kast.huidigeLocatie.nodeName,"vrachtdepot");
                                        //terugReis word bij rijs ingevoegd
                                        rijs.AddRange(terugReis);

                                        //Navigeer de robot na de locatie van de Kast.
                                        robot.GoCarryKast(kast);
                                        robot.goTo(rijs,HraphObjects);
                                        kast.GetCarriedBy(robot);
                                        //Eindbestemming is altijd hetzelfde voor een kast
                                        kast.ZetBestemming(new Hraph(7,0,7,"vrachtdepot"));
                                        //break de huidige robot foreach en zet de break boolean flag voor de kast for each
                                        gaVerderFlag = true;
                                        break;
                                    }else if (kast.actorStatus == "InDepot"){
                                    //Of brengen kast uit depot
                                        //allemaal even kans voor elke opslag plaats in het depot.
                                        int counter = 0;
                                        //de vrachtwagen is een geen geldige optie. dus -1
                                        int countHraph = HraphObjects.Count-1;
                                        foreach (var result in HraphObjects){
                                            if (rnd.Next(countHraph) <= counter && result.nodeName != "vrachtdepot"){
                                                kast.ZetBestemming(result);
                                                break;
                                            }
                                            counter++;
                                        }
                                        var rijs = graphContent.GetPath("vrachtdepot",kast.opgeslagenLocatie.nodeName);
                                        var terugReis = graphContent.GetPath(kast.opgeslagenLocatie.nodeName,"vrachtdepot");
                                        rijs.AddRange(terugReis);

                                        robot.GoCarryKast(kast);
                                        robot.goTo(rijs,HraphObjects);
                                        kast.GetCarriedBy(robot);

                                        //break de huidige robot foreach en zet de break boolean flag voor de kast for each
                                        gaVerderFlag = true;
                                        break;
                                    }
                                }
                            }
                        }
                }
            }

            foreach (Robot robot in robotLijst){
                //als robot opzelfde locatie is als kast? en hij hoort de kast op te pakken?
                // Dan pak hem op!
            } 
            if (dumptruck.Cargodrop == true){
                //CargoDropped();
            }

            if (Cargocarry ==true){
                CargoPicked();
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