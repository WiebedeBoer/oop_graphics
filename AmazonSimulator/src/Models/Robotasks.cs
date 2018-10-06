using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    /// <summary>
    /// Complicated RobotTasks that deserve their own class and method.
    /// </summary>
    public class RoboTasks {

        private Random rnd = new Random();
        private GraphClass graphContent; 
        private List<C3model> worldObjects;
        private List<Hraph> HraphObjects;

        public RoboTasks (List<C3model> worldObjects,GraphClass graphContent,List<Hraph> HraphObjects){
            this.worldObjects = worldObjects;
            this.graphContent = graphContent;
            this.HraphObjects = HraphObjects;
        }
        /// <summary>
        /// This big method gets called for in every Update(int tick) in the World class.
        /// Once it is called it immediatly starts a chance calculation with a low chance of success.
        /// If it succseeds we start to foreach trough all Kasts and Robots.
        /// And then it either picks up an already stored Kast, to be brought back to the depot.
        /// Or it brings a Kast from the depot to an unoccupied storage position.
        /// </summary>
        public void MoveKasten (){
            //20 updates per second, 0,5% chancer per update to make things happen.
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

                                        //Is there storage available?
                                        Boolean storageAvailable = true;
                                        foreach (var result in HraphObjects){
                                            if (result.cargoPlace == true){
                                                    foreach (C3model t in worldObjects){
                                                        Kast checkKast;
                                                        if(t is Kast) {
                                                            checkKast = (Kast)t;
                                                        } else {
                                                            continue;
                                                        }
                                                        if(checkKast.destinationLocation != null && checkKast.currentLocation != null){
                                                            if(checkKast.currentLocation.nodeName == result.nodeName || checkKast.destinationLocation.nodeName == result.nodeName){
                                                                //no, this particular storage place is already occupied
                                                                storageAvailable = false;
                                                                break;
                                                            }else {
                                                                storageAvailable = true;
                                                            }
                                                        }
                                                    }
                                                    //If a storage space is abilable-> set it as our destination!
                                                    if(storageAvailable == true){
                                                        kast.ZetBestemming(result);
                                                        break;
                                                    }
                                                    //If not? we continue our search for a storage space
                                            }
                                        }
                                        if (storageAvailable == true){
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
}