using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
        
        private double tX, tY, tZ;
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }

        public void Target (double x, double y, double z){
            tX = x;
            tY = y;
            tZ = z;

            if(tX!=this.x || tY!=this.y || tZ!=this.z) {
                Move(this.x,this.y,this.z-0.02);
                needsUpdate = true;
            }

        }
        public override bool Update(int tick)
        {
            if(tX!=this.x || tY!=this.y || tZ!=this.z) {
            this.Move(this.x,this.y,this.z-0.02);
            return base.Update(tick);
            }
            return false;
            
            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //}
            //return false;
        }
    }
}