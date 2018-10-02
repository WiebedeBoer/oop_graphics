using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : C3model, IUpdatable {
        //Status van de robot
        string roboCommand;

        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }
        //RoboCommand geeft naast het bewegen, ook een commando.
        //Denk aan een kast op te pakken zodra meneer robot op zijn bestemming is.
        public void RoboCommand (){

        }
        
        public override bool Update(int tick)
        {
            //Beweging naar bestemming (Gaat veranderd worden voor correcte path vinding)
            if(tX>x && tX != -1){
                if (tX-x < 0.01){
                    this.Move(tX,y,z); 
                }else{ 
                this.Move(x+0.01,y,z);  
                }
            }else if(tX<x && tX != -1){
                if (tX-x > 0.01){
                    this.Move(tX,y,z); 
                }else{ 
                this.Move(x-0.01,y,z);  
                } 
                //En nu voor de Y as
            }else if(tZ>z && tX != -1){
                if (tZ-z < 0.01){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,z+0.01);  
                }  
            }else if(tZ<z && tX != -1){
                if (tZ-z > 0.01){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,tZ-0.01);
                } 
            }
            //Bestemming of waypoint berijkt.
            if((x==tX && y==tX && z==tZ) && actorStatus == "moving to waypoints"){

                if (hraphTarget.Count == waypointNr){
                    //we zijn er!
                    actorStatus = "idle";
                    hraphTarget.Clear();
                    waypointNr = 0;
                }else{
                    //we zijn er bijna.
                    actorStatus = "moving to waypoints";
                    waypointNr++;
                    Target(hraphTarget[waypointNr-1].x,hraphTarget[waypointNr-1].y,hraphTarget[waypointNr-1].z);
                }
                
            }
            //TODO: if positie != thuisbasis en status == idle then-> na huis jij

            return base.Update(tick);
        }
    }
}