function addPointLight(object,color,x,y,z,intensity,distance){
    var light = new THREE.PointLight(color, intensity, distance);
    light.position.set(x,y,z);
    object.add(light);
}