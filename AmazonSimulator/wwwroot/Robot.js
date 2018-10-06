class Robot extends THREE.Group {

    init (){
        if (this._LoadState !=LoadStates.NOT_LOADING){
            return;
        }

        this._LoadState = LoadStates.LOADING;

        var rrobot = this;

        var rgeometry = new THREE.BoxGeometry(0.9, 0.3, 0.9);
        var rcubeMaterials = [
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_side.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_top.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshBasicMaterial({ map: new THREE.TextureLoader().load("textures/robot_front.png"), side: THREE.DoubleSide }), //BACK
        ];
        var rmaterial = new THREE.MeshFaceMaterial(rcubeMaterials);
        var meshrobot = new THREE.Mesh(rgeometry, rmaterial);
        meshrobot.position.set.y = 0.15;
        rrobot.add(meshrobot);    

        rrobot._LoadState = LoadStates.LOADED;
        
    }
            
    constructor(){
        super();

        this._LoadState = LoadStates.NOT_LOADING;
        this.init();
    }

}