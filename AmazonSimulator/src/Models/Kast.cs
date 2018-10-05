using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Kast : C3model, IUpdatable, IBeweging {
        public Hraph destinationLocation;
        public Hraph currentLocation;
        private Robot carryingRobot;
        public Kast (double x, double y, double z, double rotationX, double rotationY, double rotationZ, Hraph huidigeLocatie) :base(x, y, z, rotationX, rotationY, rotationZ,"kast")
        {
            this.currentLocation = huidigeLocatie;
        }
        public void Movement(){
            if (actorStatus == "Opgepakt"){
                this.Move(carryingRobot.x,3.3,carryingRobot.z);
            }
        }
        public void ZetBestemming(Hraph destination) {
            destinationLocation = destination;
        }
        public void NeerGezet(){
            this.Move(destinationLocation.x,3,destinationLocation.z);
            currentLocation = new Hraph(destinationLocation.x,3,destinationLocation.z,destinationLocation.nodeName, true);
            if(destinationLocation.nodeName == "vrachtdepot"){
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
            Movement();
            return base.Update(tick);
        }
    }
}