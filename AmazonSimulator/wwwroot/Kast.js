class Kast extends THREE.Group {

    init (){
        if (this._loadState !=LoadStates.NOT_LOADING){
            return;
        }

        this._loadState = LoadStates.LOADING;

        var mkast = this;

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
        meshkast._meshkast = new THREE.Mesh(kgeometry, kmaterial);
        mkast._meshkast.position.set.y = 0.15;
        mkast.add(mkast._meshkast);

        mkast._loadState = LoadStates.LOADED;
        
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