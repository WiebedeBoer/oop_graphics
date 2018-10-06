using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    /// <summary>
    /// Robot is an actor which moves along the waypoint nodes to transport Kasten from and to the depot.
    /// </summary>
    public class Robot : C3model, IUpdatable, IBeweging {
        private Kast carryingKast;
        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }
        /// <summary>
        /// Movement gets called every update in Update(int tick) in the Robot class
        /// Robot moves to the target coordinates at a relatively slow speed of 0.1 per update.
        /// </summary>
        public void Movement(){
            if(tX != -1 && tY != -1 && tZ != -1){
                if(tX>x){
                    if (tX-x <= 0.1){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x+0.1,y,z);  
                    }
                }else if(tX<x){
                    if (tX-x >= -0.1){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x-0.1,y,z);  
                    } 
                    //And now for the Z coordinate
                }else if(tZ>z){
                    if (tZ-z <= 0.1){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z+0.1);  
                    }  
                }else if(tZ<z){
                    if (tZ-z >= -0.1){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z-0.1);
                    } 
                }
            }
        }
        
        private void GrabKast(){
            carryingKast.GetPickedUp();
        }
        private void DropKast(){
            carryingKast.GetPutDown();
            carryingKast = null;
        }
        public void GoCarryKast(Kast input) {
            carryingKast = input;
            actorStatus = "GoingToKast";
        }

        public override bool Update(int tick)
        {
            //Check if there is a kast that you have to carry, BEFORE you move toward your target and waypoints
            if (carryingKast != null){
                if((x == carryingKast.currentLocation.x && z == carryingKast.currentLocation.z && carryingKast.actorStatus == "WachtOpRobot") || carryingKast.currentLocation.nodeName == "vrachtdepot" && carryingKast.actorStatus == "WachtOpRobot"){
                    GrabKast();
                }
            }
            Movement();
            
            //We reached A waypoint
            if((x==tX && z==tZ) && hraphTarget.Count != 0){
                
                //We reached our final destination!
                //note: which is always the "vrachtdepot"
                if (hraphTarget.Count == waypointNr+1){
                    //we zijn er!
                    actorStatus = "idle";
                    hraphTarget.Clear();
                    waypointNr = 0;
                    //Als we terug zijn bij depot en we hebben een kast bij ons...
                    //Drop de kast
                    if (carryingKast != null){
                        DropKast();
                    }
                    tX = -1;
                    tY = -1;
                    tZ = -1;
                }else{
                    //We arent there just yet...
                    if (carryingKast != null){
                        //But this might be the place we either have to pickup or drop off our cargo
                        if (carryingKast.destinationLocation.nodeName == hraphTarget[waypointNr].nodeName && carryingKast.actorStatus == "Opgepakt"){
                            DropKast();
                        }else if (carryingKast.currentLocation.nodeName == hraphTarget[waypointNr].nodeName && carryingKast.actorStatus == "WachtOpRobot"){
                            GrabKast();
                        }
                        
                    }
                    //Since we havent reached our final desination, we have to keep going to the next.
                    waypointNr++;
                    Target(hraphTarget[waypointNr].x,hraphTarget[waypointNr].y,hraphTarget[waypointNr].z);
                }
                
            }

            return base.Update(tick);
        }
    }
}