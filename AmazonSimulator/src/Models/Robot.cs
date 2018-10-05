using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : C3model, IUpdatable, IBeweging {
        private Kast carryingKast;
        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }
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
                    //En nu voor de Y as
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
            carryingKast.NeerGezet();
            carryingKast = null;
        }
        public void GoCarryKast(Kast input) {
            carryingKast = input;
            actorStatus = "GoingToKast";
        }
        public override bool Update(int tick)
        {
            //Als we in het depot zijn. en we moeten een kast brengen na de opslag plaatsen
            //De begin positie(vrachtdepot) zit niet in de waypoints. Dus moeten we effe checken of we de kast hier al kunnen oppakken
            //ook VOORDAT we beginnen met bewegen!
            if (carryingKast != null){
                if((x == carryingKast.currentLocation.x && z == carryingKast.currentLocation.z && carryingKast.actorStatus == "WachtOpRobot") || carryingKast.currentLocation.nodeName == "vrachtdepot" && carryingKast.actorStatus == "WachtOpRobot"){
                    GrabKast();
                }
            }
            Movement();
            //Bestemming of waypoint berijkt.
            if((x==tX && z==tZ) && hraphTarget.Count != 0){

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
                    //we zijn er bijna.
                    if (carryingKast != null){
                        //als op aflever punt zijn van kast? droppen
                        if (carryingKast.destinationLocation.nodeName == hraphTarget[waypointNr].nodeName && carryingKast.actorStatus == "Opgepakt"){
                            DropKast();
                        }else if (carryingKast.currentLocation.nodeName == hraphTarget[waypointNr].nodeName && carryingKast.actorStatus == "WachtOpRobot"){
                            GrabKast();
                        }
                        
                    }
                    waypointNr++;
                    Target(hraphTarget[waypointNr].x,hraphTarget[waypointNr].y,hraphTarget[waypointNr].z);
                }
                
            }

            return base.Update(tick);
        }
    }
}