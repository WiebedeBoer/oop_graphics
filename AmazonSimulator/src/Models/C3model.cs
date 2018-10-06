using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    //C3model serves as a base class to Robot.cs, Kast.cs and Dumptruck.cs
    //C3model contains all the barebones properties and methods neccesary for any actor within the simulation
    public class C3model : IUpdatable {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        private double _rX = 0;
        private double _rY = 0;
        private double _rZ = 0;

        public string type { get; }
        public Guid guid { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public double rotationX { get { return _rX; } }
        public double rotationY { get { return _rY; } }
        public double rotationZ { get { return _rZ; } }

        protected List<Hraph> hraphTarget = new List<Hraph>();
        protected double tX = -1, tY = -1, tZ =-1;
        protected int waypointNr = 0;
        public string actorStatus = "idle";
        private bool needsUpdate = true;

        public C3model(double x, double y, double z, double rotationX, double rotationY, double rotationZ, string type) {
            this.type = type;
            this.guid = Guid.NewGuid();

            this._x = x;
            this._y = y;
            this._z = z;

            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;
        }
        /// <summary>
        /// Target method changes the target coordinates of the respective actor
        /// Depending on the way the actor moves, the actor navigates towards the target.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        public void Target (double x, double y, double z){
            tX = x;
            tY = y;
            tZ = z;
        }
        /// <summary>
        /// Move method moves the actor instantly upon the next available update to the supplied coordinates
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="z">z coordinate</param>
        public virtual void Move(double x, double y, double z) {
            this._x = x;
            this._y = y;
            this._z = z;
            needsUpdate = true;
        }
        /// <summary>
        /// GoTo changes the HraphTarget variable and sets off a Target method to go to the first coordiante
        /// Using all available waypoints and the list of waypoint names given to navigate to, GoTo obtains the coordinates and names and add this to the hraphTarget variable
        /// Which will used to navigate from waypoint to waypoint using Target
        /// </summary>
        /// <param name="waypointName">List of strings of all the locations the actor needs to visit</param>
        /// <param name="allWaypoints">Hraph list of all available waypoints in the simulation. Needed to compare .nodeName to waypointName and obtain coordinates</param>
        public void GoTo (List<string> waypointName, List<Hraph> allWaypoints){
            foreach(var input in waypointName){
                foreach (var hraph in allWaypoints){
                    if(input == hraph.nodeName){
                        hraphTarget.Add( new Hraph(hraph.x,hraph.y,hraph.z,hraph.nodeName, true));
                    }
                }
            }
            //Immediatly target the first coordinate.
            Target(hraphTarget[0].x,hraphTarget[0].y,hraphTarget[0].z);
        }
        /// <summary>
        /// Rotate instantly rotates the actor upon next available update to the supplied paramaters.
        /// </summary>
        /// <param name="rotationX">Roll</param>
        /// <param name="rotationY">Jaw</param>
        /// <param name="rotationZ">Pitch</param>
        public virtual void Rotate(double rotationX, double rotationY, double rotationZ) {
            this._rX = rotationX;
            this._rY = rotationY;
            this._rZ = rotationZ;

            needsUpdate = true;
        }

        public virtual bool Update(int tick)
        {
            if(needsUpdate) {
                needsUpdate = false;
                return true;
            }
            return false;
        }
    }
}