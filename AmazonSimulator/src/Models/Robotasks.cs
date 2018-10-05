using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robotasks {

        private Random rnd = new Random(); //alleen hier binnen gebruikt voor randomiseren
        GraphClass graphContent = new GraphClass(); //alleen hier gemaakt   
        private List<C3model> worldObjects = new List<C3model>(); //world create en update, in move check of kast
        private List<Kast> kastLijst = new List<Kast>(); //ook voor bewegen dumptruck
        public List<Hraph> HraphObjects = new List<Hraph>(); //ook voor bewegen dumptruck

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

        
    }
}