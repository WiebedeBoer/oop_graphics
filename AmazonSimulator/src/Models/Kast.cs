using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Kast : C3model, IUpdatable, IBeweging {
        public Hraph opgeslagenLocatie;
        public Hraph huidigeLocatie;
        private Robot carryingRobot;
        public bool Cargoed;
        
        public Kast (double x, double y, double z, double rotationX, double rotationY, double rotationZ, Hraph huidigeLocatie) :base(x, y, z, rotationX, rotationY, rotationZ,"kast")
        {
            this.huidigeLocatie = huidigeLocatie;
        }
        public void Beweeg(){
            if (actorStatus == "Opgepakt"){
                this.Move(carryingRobot.x,3.3,carryingRobot.z);
            }
        }
        public void ZetBestemming(Hraph bestemming) {
            opgeslagenLocatie = bestemming;
        }
        public void NeerGezet(){
            this.Move(opgeslagenLocatie.x,3,opgeslagenLocatie.z);
            huidigeLocatie = new Hraph(opgeslagenLocatie.x,3,opgeslagenLocatie.z,opgeslagenLocatie.nodeName, true);
            if(opgeslagenLocatie.nodeName == "vrachtdepot"){
                actorStatus = "InDepotOud";
            } else {
                actorStatus = "opgeslagen";
            }
        }
        public void GetPickedUp(){
            actorStatus = "Opgepakt";
        }
        public void GetCarriedBy(Robot input){
            carryingRobot = input;
            actorStatus = "WachtOpRobot";
        }
        public override bool Update(int tick)
        {
            Beweeg();
            return base.Update(tick);
        }
    }
}