class Kast extends THREE.Group {
            
    constructor(){
        super();
        var kgeometry = new THREE.BoxGeometry(0.9, 6, 0.9);
        var cubeMaterials = [
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //LEFT
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //RIGHT
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //TOP
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //BOTTOM
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //FRONT
                new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/bricks.jpg"), side: THREE.DoubleSide }), //BACK
            ];
        var kmaterial = new THREE.MeshFaceMaterial(cubeMaterials);
        var mkast = new THREE.Mesh(kgeometry, kmaterial);
        mkast.position.y = 0.15;
        //var group = new THREE.Group();
        //group.add(kast);
        //scene.add(group);
        //worldObjects[command.parameters.guid] = group;
        this._LoadState = LoadStates.NOT_LOADING;
        //this.init();
    }

}