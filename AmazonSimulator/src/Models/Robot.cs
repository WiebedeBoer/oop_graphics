using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : C3model, IUpdatable {
        //Target coordinaten
        private double tX = -1, tY = -1, tZ =-1;
        //Status


        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }

        public void Target (double x, double y, double z){
            tX = x;
            tY = y;
            tZ = z;
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
                needsUpdate = true;    
            }else if(tX<x && tX != -1){
                if (tX-x > 0.1){
                    this.Move(tX,y,z); 
                }else{ 
                this.Move(x-0.1,y,z);  
                } 
                needsUpdate = true; 
                //En nu voor de Y as
            }else if(tZ>z && tX != -1){
                if (tZ-z < 0.1){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,z+0.1);  
                }
                needsUpdate = true;    
            }else if(tZ<z && tX != -1){
                if (tZ-z > 0.1){
                    this.Move(x,y,tZ); 
                }else{ 
                this.Move(x,y,tZ-0.1);  
                } 
                needsUpdate = true; 
            }
            
            return base.Update(tick);
            
            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //}
            //return false;
        }
    }
}