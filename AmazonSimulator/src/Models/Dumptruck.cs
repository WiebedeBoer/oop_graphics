using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    /// <summary>
    /// Dumptruck is an actor in the simulation that takes the role of routinely picking up and delivering cargo to the depot.
    /// This is its sole purpose
    /// </summary>
    public class Dumptruck : C3model, IUpdatable, IBeweging {
        private sbyte depotTimer;
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }
        /// <summary>
        /// Movement gets called every update in Update(int tick) in the Dumptruck class
        /// Movement navigates toward the target coordinates in its own way
        /// </summary>
        public void Movement(){
            if(tX != -1 && tY != -1 && tZ != -1){
                if(tX>x){
                    if (tX-x <= 0.3){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x+0.3,y,z);  
                    }
                }else if(tX<x){
                    if (tX-x >= -0.3){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x-0.3,y,z);  
                    } 
                    //En nu voor de Y as
                }else if(tZ>z){
                    if (tZ-z <= 0.3){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z+0.3);  
                    }  
                }else if(tZ<z){
                    if (tZ-z >= -0.3){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z-0.3);
                    } 
                }
            }
        }
        /// <summary>
        /// Gets called every Update(int tick) in the World class
        /// Based on the current position of the truck, it stops and drops and picks up old cargo from the depot.
        /// </summary>
        /// <param name="kastLijst">List of all kasts</param>
        /// <param name="HraphObjects">List of all waypoints</param>
        public void MoveDumptruck(List<Kast> kastLijst, List<Hraph> HraphObjects){
            if(x == 0 && z == 7){
                //We are now at the depot
                depotTimer++;
                if(depotTimer == 20){
                    DropCargo(kastLijst,HraphObjects);
                }else if(depotTimer == 40){
                PickupOldCargo(kastLijst);
                }else if(depotTimer == 60){
                    Target(x,y,z-100);
                    depotTimer = 0;
                }
            }else if(this.z == -93){
                //And back the beginning
                Move(0,0,107);
                Target(x,y,z-100);
            }
        }
        /// <summary>
        /// 'Picks up' all cargo that has the status "InDepotOud" to be shipped off.
        /// </summary>
        /// <param name="alleKasten">All kasten</param>
        private void PickupOldCargo(List<Kast> alleKasten){
                
                    foreach(Kast kast in alleKasten){
                        if(kast.actorStatus == "InDepotOud"){
                            kast.Move(0,1000,0);
                            kast.actorStatus = "hemel";
                        }
                    }
            
        }
        /// <summary>
        /// Drops off all cargo that had previously been picked up. All dropped cargo is dropped inside the depot and marked ready to be used
        /// </summary>
        /// <param name="alleKasten">All kasten</param>
        /// <param name="hraph">All waypoints to find the depot coordinates</param>
        private void DropCargo(List<Kast> alleKasten,List<Hraph> hraph){
            foreach (Hraph hraphresult in hraph)
            {
                if(hraphresult.nodeName == "vrachtdepot"){
                    foreach(Kast kast in alleKasten){
                        if(kast.actorStatus == "hemel"){
                            kast.Move(hraphresult.x,3,hraphresult.z);
                            kast.actorStatus = "InDepotNieuw";
                            kast.currentLocation = hraphresult;
                            break;
                        }
                    }
                }else{
                    break;
                }
            }
        }

        public override bool Update(int tick)
        {
            Movement();            
            return base.Update(tick);
        }
    }
}