using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Kast : C3model, IUpdatable {
        public string opgeslagenLocatie;
        public bool Cargoed;
        
        public Kast (double x, double y, double z, double rotationX, double rotationY, double rotationZ, string opgeslagenLocatie, bool Cargoed) :base(x, y, z, rotationX, rotationY, rotationZ,"kast")
        {
            this.opgeslagenLocatie = opgeslagenLocatie;
            this.Cargoed = Cargoed;
        }
        public override bool Update(int tick)
        {
            if(needsUpdate) {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}