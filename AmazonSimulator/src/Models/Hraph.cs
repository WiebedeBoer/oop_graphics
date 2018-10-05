using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class Hraph
    {
        private double _x = 0;
        private double _y = 0;
        private double _z = 0;
        public string nodeName { get; }
        public double x { get { return _x; } }
        public double y { get { return _y; } }
        public double z { get { return _z; } }
        public bool cargoPlace;
        public Hraph(double x, double y, double z, string nodeName, bool cargoPlace) {
            _x = x;
            _y = y;
            _z = z;
            this.nodeName = nodeName;
            this.cargoPlace = cargoPlace;
        }

    }
}