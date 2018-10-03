using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
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
            if(tZ<this.z) {
                this.Move(this.x,this.y,this.z-0.1);
                this._Cargo =false;
                return base.Update(tick);
                
            }
            else {
                this._Cargo =true;
                    return false;
            }

            //if(needsUpdate) {
            //    needsUpdate = false;
            //    return true;
            //}
            //return false;
        }
    }
}