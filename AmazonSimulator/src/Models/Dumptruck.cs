using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
        
        private double tX, tY, tZ;
        private bool _Cargo = false;
        public bool Cargodrop {get{ return _Cargo; }}
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }

        public void Target (double x, double y, double z){
            tX = x;
            tY = y;
            tZ = z;

            if(tX!=this.x || tY!=this.y || tZ!=this.z) {
                Move(this.x,this.y,this.z-0.1);
                needsUpdate = true;
            }

        }
        public override bool Update(int tick)
        {
            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //if(x==tX && y==tX && z==tZ){
            if(tZ<this.z) {
                this.Move(this.x,this.y,this.z-0.1);
                this._Cargo =true;
                return base.Update(tick);
                
            }
            else {
                this._Cargo =true;
                    return false;
            }
            
            //}
            

            /*
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
            if(x==tX && y==tX && z==tZ){
                this._Cargo =true;                
            }
            return base.Update(tick);
            */

            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //}
            //return false;
        }
    }
}