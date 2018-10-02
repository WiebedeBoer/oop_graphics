class dumptruck{
dumptruck = new THREE.Group();
scene.add(dumptruck);
worldObjects[command.parameters.guid] = dumptruck;
var dumptruck = this;
    loadOBJModel("models/","Dump_Truck.obj","models/","Dump_Truck.mtl", (mesh) => {
        dumptruck.position.y = 1;
        dumptruck.add(mesh);
        addPointLight(dumptruck,0xffffff, -5, 17, 0, 1, 10);
        //var g = new THREE.BoxGeometry(1,1,1);
        //var m = new THREE.MeshBasicMaterial({color: 0xffffff});
    });
}
