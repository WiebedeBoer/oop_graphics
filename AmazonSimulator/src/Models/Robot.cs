using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Robot : C3model, IUpdatable {
        
        private double tX, tY, tZ;

        public Robot (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"robot")
        {
            
        }

        public void Target (double x, double y, double z){
            //while(x!=this.x && y!=this.y && z!=this.z){
                if(x>this.x) {
                    Move(this.x+0.1,this.y,this.z);
                    needsUpdate = true;
                }
            //}
        }
        public override bool Update(int tick)
        {
            this.Target(this.x+0.1,this.y,this.z);
            return base.Update(tick);
            
            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //}
            //return false;
        }
    }
}