using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class Graph
    {
        private List<Gnode> graphObjects = new List<Gnode>();
        
        Gnode vrachtdepot;
        Gnode RA;
        Gnode RB;
        Gnode RC;
        Gnode RD;
        Gnode LA;
        Gnode LB;
        Gnode LC;
        Gnode LD;

        public Graph() {
            //Alle 'nodes'
            Gnode vrachtdepot = CreateNode(0,0,3, "vrachtdepot");
            Gnode RA = CreateNode(0,0,2,"RA");
            Gnode RB = CreateNode(0,0,4,"RB");
            Gnode RC = CreateNode(0,0,6,"RC");
            Gnode RD = CreateNode(0,0,8,"RD");
            Gnode LA = CreateNode(4,0,2,"LA");
            Gnode LB = CreateNode(4,0,4,"LB");
            Gnode LC = CreateNode(4,0,6,"LC");
            Gnode LD = CreateNode(4,0,8,"LD");
        }

        private Gnode CreateNode(double x, double y, double z, string nodename) {
            Gnode g = new Gnode(0,0,0,nodename);
            graphObjects.Add(g);
            return g;
        }

        public class Gnode {

            private double _x = 0;
            private double _y = 0;
            private double _z = 0;

            public string nodename { get; }
            public double x { get { return _x; } }
            public double y { get { return _y; } }
            public double z { get { return _z; } }

            public Gnode(double x, double y, double z, string nodename) {
            //type = "robot" of "dumptruck"
            this.nodename = nodename;
            
            this._x = x;
            this._y = y;
            this._z = z;
            }
           

        }

    }
}