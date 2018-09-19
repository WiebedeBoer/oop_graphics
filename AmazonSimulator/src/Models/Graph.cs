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
            Gnode vrachtdepot = CreateNode(0,0,0);
            Gnode RA = CreateNode(0,2,0);
            Gnode RB = CreateNode(0,4,0);
            Gnode RC = CreateNode(0,6,0);
            Gnode RD = CreateNode(0,8,0);
            Gnode LA = CreateNode(4,2,0);
            Gnode LB = CreateNode(4,4,0);
            Gnode LC = CreateNode(4,6,0);
            Gnode LD = CreateNode(4,8,0);
        }