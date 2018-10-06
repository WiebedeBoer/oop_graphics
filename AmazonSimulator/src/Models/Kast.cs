using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    /// <summary>
    /// Kast(or cargo) is an actor which resembles packages in the simulator
    /// To be moved back and forth from the depot, being carried by Robots. And to be delivered and shipped off by the dumptruck.
    /// </summary>
    public class Kast : C3model, IUpdatable, IBeweging {
        //Properties unique to the Kast. the destination, current location and information on who(a Robot) is currently, or about to carry the kast.
        public Hraph destinationLocation;
        public Hraph currentLocation;
        private Robot carryingRobot;
        public Kast (double x, double y, double z, double rotationX, double rotationY, double rotationZ, Hraph huidigeLocatie) :base(x, y, z, rotationX, rotationY, rotationZ,"kast")
        {
            this.currentLocation = huidigeLocatie;
        }

        /// <summary>
        /// Movement gets called every update in Update(int tick) in the Kast class
        /// The only movement required for the kast is to hover above the Robot which is 'carrying' it.
        /// </summary>
        public void Movement(){
            if (actorStatus == "Opgepakt"){
                this.Move(carryingRobot.x,3.3,carryingRobot.z);
            }
        }
        /// <summary>
        /// Sets the destination property based on the paramater supplied
        /// </summary>
        /// <param name="destination"></param>
        public void ZetBestemming(Hraph destination) {
            destinationLocation = destination;
        }

        /// <summary>
        /// GetPutDown is called when the Robot carrying the Kast wants to drop the Kast
        /// Depending on the destinationLocation preoperty the kast moves itself to the place it is stupposed to be dropped off
        /// Which is exactly where the robot has carried him, hopefully.
        /// If the Kast is dropped off at the depot, it is treated as old cargo, and to be picked up next time the Dumptruck arrives.
        /// </summary>
        public void GetPutDown(){
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

        /// <summary>
        /// Makes the Kast class remember who is carrying the robot for future reference
        /// Also changes the Kast's status to wait for the robot, this way he cant get assigned to by anyone(Robot) else.
        /// </summary>
        /// <param name="input">The Robot which is to carry the kast</param>
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