using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
        public int depotTimer;
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }
        public void PickupOldCargo(List<Kast> alleKasten,List<Hraph> Hraph){
                
                    foreach(Kast kast in alleKasten){
                        if(kast.actorStatus == "InDepotOud"){
                            kast.Move(0,1000,0);
                            kast.actorStatus = "hemel";
                        }
                    }
            
        }
        public void DropCargo(List<Kast> alleKasten,List<Hraph> Hraph){
            foreach (Hraph hraph in Hraph)
            {
                if(hraph.nodeName == "vrachtdepot" || hraph.nodeName == "updepot" || hraph.nodeName == "downdepot"){
                    foreach(Kast kast in alleKasten){
                        if(kast.actorStatus == "hemel"){
                            kast.Move(hraph.x,3,hraph.z);
                            kast.actorStatus = "InDepotNieuw";
                            kast.huidigeLocatie = hraph;
                            break;
                        }
                    }
                }else{
                    break;
                }
            }
        }

        public override bool Update(int tick)
        {
            if(tX != -1 && tY != -1 && tZ != -1){
                if(tX>x){
                    if (tX-x <= 0.3){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x+0.3,y,z);  
                    }
                }else if(tX<x){
                    if (tX-x >= -0.3){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x-0.3,y,z);  
                    } 
                    //En nu voor de Y as
                }else if(tZ>z){
                    if (tZ-z <= 0.3){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z+0.3);  
                    }  
                }else if(tZ<z){
                    if (tZ-z >= -0.3){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z-0.3);
                    } 
                }
            }
            
            return base.Update(tick);
        }
    }
}