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
        
        public void MoveDumptruck(List<Kast> kastLijst, List<Hraph> HraphObjects){   
            if(this.x == 0 && this.z == 7){
                //We zijn bij depot
                this.depotTimer++;
                if(this.depotTimer == 20){
                    this.DropCargo(kastLijst,HraphObjects);
                }else if(this.depotTimer == 40){
                    this.PickupOldCargo(kastLijst,HraphObjects);
                }else if(this.depotTimer == 60){
                    this.Target(this.x,this.y,this.z-100);
                    this.depotTimer = 0;
                }
            }else if(this.z == -93){
                //terug bij af
                this.Move(0,0,107);
                this.Target(this.x,this.y,this.z-100);
            }
        }
        private void PickupOldCargo(List<Kast> alleKasten,List<Hraph> Hraph){
                
                    foreach(Kast kast in alleKasten){
                        if(kast.actorStatus == "InDepotOud"){
                            kast.Move(0,1000,0);
                            kast.actorStatus = "hemel";
                        }
                    }
            
        }
        private void DropCargo(List<Kast> alleKasten,List<Hraph> Hraph){
            foreach (Hraph hraph in Hraph)
            {
                if(hraph.nodeName == "vrachtdepot"){
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