/*
function Depot() {
    var dgeometry = new THREE.BoxGeometry(3.9, 13.3, 5.9);
    var dcubeMaterials = [
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //LEFT
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //RIGHT
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/bobdebouwer.png"), side: THREE.DoubleSide }), //TOP
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/bobdebouwer.png"), side: THREE.DoubleSide }), //FRONT
        new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BACK
    ];
    var dmaterial = new THREE.MeshPhongMaterial(dcubeMaterials);
    var depot = new THREE.Mesh(dgeometry, dmaterial);
    depot.position.x = 7;
    depot.position.y = 0;
    depot.position.z = 7;
    scene.add(depot);

}
*/

class Depot {

    init(){
        var mdepot = this;
        var dgeometry = new THREE.BoxGeometry(3.9, 13.3, 5.9);
        var dcubeMaterials = [
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/bobdebouwer.png"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/bobdebouwer.png"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/robot_bottom.png"), side: THREE.DoubleSide }), //BACK
            ];
        var dmaterial = new THREE.MeshPhongMaterial(dcubeMaterials);
        var ddepot = new THREE.Mesh(dgeometry, dmaterial);
        ddepot.position.x = 7;
        ddepot.position.y = 0;
        ddepot.position.z = 7;
        mdepot.add(ddepot);   
    }

    
    constructor(){
        //super();
        //this._LoadState = LoadStates.NOT_LOADING;
        this.init();
    }
    
    
}
