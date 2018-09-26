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
            if(tX>x && tX != -1){
                this.Move(x+0.1,y,z);        
                needsUpdate = true;    
            }else if(tX<x && tX != -1){
                this.Move(x-0.1,y,z); 
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