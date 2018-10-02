using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
        
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }

        public override bool Update(int tick)
        {
            if(tX!=this.x || tY!=this.y || tZ!=this.z) {
            this.Move(this.x,this.y,this.z-0.02);
            return base.Update(tick);
            }
            return false;
        }
    }
}