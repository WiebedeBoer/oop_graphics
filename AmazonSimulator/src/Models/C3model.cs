using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
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
        protected string actorStatus = "idle";

        public bool needsUpdate = true;

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
        //Target kan gebruikt worden om een robot handbatig na coordianten te sturen.
        //Ook word Target gebruikt door goTo als waypoint of destination.
        public void Target (double x, double y, double z){
            tX = x;
            tY = y;
            tZ = z;
        }
        //Move beweegt de acteur onmiddelijk zonder transitie of animatie.
        public virtual void Move(double x, double y, double z) {
            this._x = x;
            this._y = y;
            this._z = z;
            needsUpdate = true;
        }
        //goTo geeft een lijst met coordinaten waar de acteur heen moet gaan, in die volgorde.
        public void goTo (List<string> waypointName, List<Hraph> allWaypoints){
            //Wissel volgorde om van de lijst van waypoints, die staan namelijk niet goed.
            waypointName.Reverse();
            //Je bent weer terug bij af
            waypointNr = 0;
            foreach(var input in waypointName){
                foreach (var hraph in allWaypoints){
                    if(input == hraph.nodeName){
                        hraphTarget.Add( new Hraph(hraph.x,hraph.y,hraph.z,hraph.nodeName));
                    }
                }
            }
            actorStatus = "moving to waypoints";
            //Eerste waypoint/destination eerst...
            Target(hraphTarget[0].x,hraphTarget[0].y,hraphTarget[0].z);
        }
        //Rotate draait de acteur onmiddelijk zonder transitie of animatie.
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