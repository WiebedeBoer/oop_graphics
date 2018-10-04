class Dumptruck extends THREE.Group {

    init (){
        if (this._LoadState !=LoadStates.NOT_LOADING){
            return;
        }

        this._LoadState = LoadStates.LOADING;

        var ddumptruck = this;
        loadOBJModel("models/","Dump_Truck.obj","models/","Dump_Truck.mtl", (mesh) => {
            ddumptruck.position.y = 1;
            ddumptruck.add(mesh);
            //addPointLight(ddumptruck,0x0000ff, 0, 17, 0, 4, 25); //extra light
            addPointLight(ddumptruck,0x0000ff, 1.906394, 3.613464, 5.096956, 4, 25); //extra light
            addSpotLight(ddumptruck,0x0000ff, 1.906394, 3.613464, 5.096956, 8, 25); //spot light
            addSpotLight(ddumptruck,0x0000ff, 1.906394, 3.613464, 5.096956, 8, 25); //spot light
            var g = new THREE.BoxGeometry(1,1,1);
            var m = new THREE.MeshBasicMaterial({color: 0xffffff});
            ddumptruck._meshtruck = new THREE.Mesh(g, m);
            ddumptruck._meshtruck.position.set(-25,10,4);
            ddumptruck._meshtruck.visible = false;
            ddumptruck.add(ddumptruck._meshtruck);
            ddumptruck._LoadState = LoadStates.LOADED;
        });
    }    
            
    constructor(){
        super();

        //var group = new THREE.Group();
        //group.add(kast);
        //scene.add(group);
        //worldObjects[command.parameters.guid] = group;
        this._LoadState = LoadStates.NOT_LOADING;
        this.init();
    }

}

/*

class dumptruck {
//dumptruck = new THREE.Group();
//scene.add(dumptruck);
//worldObjects[command.parameters.guid] = dumptruck;
constructor(){
    var dumptruck = this;
        loadOBJModel("models/","Dump_Truck.obj","models/","Dump_Truck.mtl", (mesh) => {
            dumptruck.position.y = 1;
            dumptruck.add(mesh);
            addPointLight(dumptruck,0xffffff, -5, 17, 0, 1, 10); //extra light
            //var g = new THREE.BoxGeometry(1,1,1);
            //var m = new THREE.MeshBasicMaterial({color: 0xffffff});
        });
    }
}
*/
