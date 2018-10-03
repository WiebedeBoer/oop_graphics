class dumptruck extends THREE.Group {

    init (){
        if (this._loadState !=LoadStates.NOT_LOADING){
            return;
        }

        this._loadState = LoadStates.LOADING;

        var dumptruck = this;
        loadOBJModel("models/","Dump_Truck.obj","models/","Dump_Truck.mtl", (mesh) => {
            dumptruck.position.y = 1;
            dumptruck.add(mesh);
            addPointLight(dumptruck,0xffffff, -5, 17, 0, 1, 10); //extra light
            var g = new THREE.BoxGeometry(1,1,1);
            var m = new THREE.MeshBasicMaterial({color: 0xffffff});
            dumptruck._meshtruck = new THREE.Mesh(g, m);
            mkast._meshtruck.position.set.y = 0.15;
            dumptruck.add(dumptruck._meshtruck);
            dumptruck._loadState = LoadStates.LOADED;
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
