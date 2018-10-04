using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models {
    public class Dumptruck : C3model, IUpdatable {
        private bool _Cargo = false;
        public bool Cargodrop {get{ return _Cargo; }}
        private List<Kast> opGepakteKasten;
        private int depotTimer;
        public Dumptruck (double x, double y, double z, double rotationX, double rotationY, double rotationZ) :base(x, y, z, rotationX, rotationY, rotationZ,"dumptruck")
        {
            
        }
        private void PickupOldCargo(){
            //Oude cargo weer op pakken.
            //oproepen get oldcargo van World?

        }
        private void DropCargo(){
            //For each kast in opGepakteKasten. dumpen jij.
            //List leeg maken aant einde.
        }

        public override bool Update(int tick)
        {
            if(tX != -1 && tY != -1 && tZ != -1){
                if(tX>x){
                    if (tX-x <= 0.1){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x+0.1,y,z);  
                    }
                }else if(tX<x){
                    if (tX-x >= -0.1){
                        this.Move(tX,y,z); 
                    }else{ 
                        this.Move(x-0.1,y,z);  
                    } 
                    //En nu voor de Y as
                }else if(tZ>z){
                    if (tZ-z <= 0.1){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z+0.1);  
                    }  
                }else if(tZ<z){
                    if (tZ-z >= -0.1){
                        this.Move(x,y,tZ); 
                    }else{ 
                        this.Move(x,y,z-0.1);
                    } 
                }
            }
            if(x == 0 && z == 7){
                //We zijn bij depot
                depotTimer++;
                if(depotTimer == 20){
                    DropCargo();
                }else if(depotTimer == 40){
                    PickupOldCargo();
                }else if(depotTimer == 60){
                    this.Target(x,y,z-100);
                }
                
                //nieuw pakketen of ouwe weg brengen?
            }else if(z == -93){
                this.Move(0,0,107);
                this.Target(x,y,z-100);
            }
            return base.Update(tick);
        }
    }
}