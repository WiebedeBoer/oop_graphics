using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class RoboTasks {

        private Random rnd = new Random(); //alleen hier binnen gebruikt voor randomiseren
        private GraphClass graphContent; //alleen hier gemaakt   
        private List<C3model> worldObjects; //world create en update, in move check of kast
        private List<Hraph> HraphObjects; //ook voor bewegen dumptruck

        //kasten bewegen
        public RoboTasks (List<C3model> worldObjects,GraphClass graphContent,List<Hraph> HraphObjects){
            this.worldObjects = worldObjects;
            this.graphContent = graphContent;
            this.HraphObjects = HraphObjects;
        }
        public void MoveKasten (){
            //20 updates per seconde. 20x 0,5% kans per seconde om een actie te laten gebeuren.
            if (rnd.Next(10000) <= 50){
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
                                    if(rnd.Next(100) >= 70 && kast.actorStatus == "opgeslagen"){
                                        foreach (var result in HraphObjects){
                                            if(result.nodeName == "vrachtdepot"){
                                                var rijs = graphContent.GetPath("vrachtdepot",kast.currentLocation.nodeName);
                                                var terugReis = graphContent.GetPath(kast.currentLocation.nodeName,result.nodeName);
                                                //terugReis word bij rijs ingevoegd
                                                rijs.AddRange(terugReis);

                                                //Navigeer de robot na de locatie van de Kast.
                                                robot.GoCarryKast(kast);
                                                robot.GoTo(rijs,HraphObjects);
                                                kast.GetCarriedBy(robot);
                                                //Eindbestemming is altijd hetzelfde voor een kast
                                                kast.ZetBestemming(result);
                                                //break de huidige robot foreach en zet de break boolean flag voor de kast for each
                                                gaVerderFlag = true;
                                                break;
                                            }
                                        }
                                        
                                    }else if (kast.actorStatus == "InDepotNieuw"){
                                        //Of brengen kast uit depot
                                        //allemaal even kans voor elke opslag plaats in het depot.
                                        int counter = 1;
                                        int countHraph = 6;
                                        foreach (var result in HraphObjects){
                                            if (result.cargoPlace == true){
                                                if (rnd.Next(countHraph) <= counter){
                                                    kast.ZetBestemming(result);
                                                    break;
                                                }
                                                counter++;
                                            }
                                        }
                                        var rijs = graphContent.GetPath("vrachtdepot",kast.destinationLocation.nodeName);
                                        var terugReis = graphContent.GetPath(kast.destinationLocation.nodeName,"vrachtdepot");
                                        rijs.AddRange(terugReis);
                                        
                                        robot.GoCarryKast(kast);
                                        robot.GoTo(rijs,HraphObjects);
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

        
    }
}