using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robotasks {

        private Random rnd = new Random();
        GraphClass graphContent = new GraphClass();
        public List<Hraph> HraphObjects = new List<Hraph>();
        Dumptruck dumptruck;
        private List<C3model> worldObjects = new List<C3model>();
        private List<Kast> kastLijst = new List<Kast>();

        //kasten bewegen
        
        public void MoveKasten (){
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

                        if(kast.actorStatus == "opgeslagen" || kast.actorStatus == "InDepotNieuw"){
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
                                        int mogelijkheden = 3;
                                        int counter = 1;
                                        foreach (var result in HraphObjects){
                                            if(result.nodeName == "vrachtdepot" || result.nodeName == "updepot" || result.nodeName == "downdepot"){
                                                if (rnd.Next(mogelijkheden) <= counter){

                                                    var rijs = graphContent.GetPath("idlevracht",kast.huidigeLocatie.nodeName);
                                                    var terugReis = graphContent.GetPath(kast.huidigeLocatie.nodeName,result.nodeName);
                                                    //terugReis word bij rijs ingevoegd
                                                    rijs.AddRange(terugReis);

                                                    //Navigeer de robot na de locatie van de Kast.
                                                    robot.GoCarryKast(kast);
                                                    robot.goTo(rijs,HraphObjects);
                                                    kast.GetCarriedBy(robot);
                                                    //Eindbestemming is altijd hetzelfde voor een kast
                                                    kast.ZetBestemming(result);
                                                    //break de huidige robot foreach en zet de break boolean flag voor de kast for each
                                                    gaVerderFlag = true;
                                                    break;
                                                }
                                                counter++;
                                            }
                                        }
                                        
                                    }else if (kast.actorStatus == "InDepotNieuw"){
                                        //Of brengen kast uit depot
                                        //allemaal even kans voor elke opslag plaats in het depot.
                                        int counter = 1;
                                        //de vrachtwagen is een geen geldige optie. dus -1
                                        int countHraph = HraphObjects.Count-1;
                                        foreach (var result in HraphObjects){
                                            //TODO kans berekening klopt niet
                                            if (rnd.Next(countHraph) <= counter && result.Cargoplace == true){
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
        }

        //dumptruck bewegen
        public void MoveDumptruck(){   
            if(dumptruck.x == 0 && dumptruck.z == 7){
                //We zijn bij depot
                dumptruck.depotTimer++;
                if(dumptruck.depotTimer == 20){
                    dumptruck.DropCargo(kastLijst,HraphObjects);
                }else if(dumptruck.depotTimer == 40){
                    dumptruck.PickupOldCargo(kastLijst,HraphObjects);
                }else if(dumptruck.depotTimer == 60){
                    dumptruck.Target(dumptruck.x,dumptruck.y,dumptruck.z-100);
                    dumptruck.depotTimer = 0;
                }
            }else if(dumptruck.z == -93){
                dumptruck.Move(0,0,107);
                dumptruck.Target(dumptruck.x,dumptruck.y,dumptruck.z-100);
            }
        }
    }
}