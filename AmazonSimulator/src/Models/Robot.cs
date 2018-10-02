using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : C3model, IUpdatable {
        //Status van de robot
        public string roboStatus = "idle";

        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }
        //roboCommand geeft naast het bewegen, ook een commando.
        //Denk aan een kast op te pakken zodra meneer robot op zijn bestemming is.
        public void roboCommand (){

        }
        
        public override bool Update(int tick)
        {
            //Beweging na bestemming (Gaat veranderd worden voor correcte path vinding)
            if(tX>x && tX != -1){
                if (tX-x < 0.1){
                    this.Move(tX,y,z); 
                }else{ 
                this.Move(x+0.1,y,z);  
                }
            }else if(tX<x && tX != -1){
                if (tX-x > 0.1){
                    this.Move(tX,y,z); 
                }else{ 
                this.Move(x-0.1,y,z);  
                } 
                //En nu voor de Y as
            }else if(tZ>z && tX != -1){
                if (tZ-z < 0.1){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,z+0.1);  
                }  
            }else if(tZ<z && tX != -1){
                if (tZ-z > 0.1){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,tZ-0.1);
                } 
            }
            //Bestemming of waypoint berijkt.
            if(x==tX && y==tX && z==tZ || (tZ-z < 0.1 && tX-x < 0.1 && tX-x < 0.1)){

                if (hraphTarget[waypointNr+1] == null){
                    //we zijn er!
                    roboStatus = "idale";
                }else{
                    //we zijn er bijna.
                    roboStatus = "idaley";
                    waypointNr++;
                    Target(hraphTarget[waypointNr].x,hraphTarget[waypointNr].y,hraphTarget[waypointNr].z);
                }
                
            }
            //TODO: if positie != thuisbasis en status == idle then-> na huis jij

            return base.Update(tick);
        }
    }
}