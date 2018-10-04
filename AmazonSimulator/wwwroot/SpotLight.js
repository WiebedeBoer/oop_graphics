function addSpotLight (object,color,x,y,z,tx,ty,tz){
    var spotLight = new THREE.SpotLight(color,1,100,0.5,2,2);
    spotLight.position.set(x,y,z);
    spotLight.castShadow = true;
    object.add(spotLight);
    object.add(spotLight.target);
    spotLight.target.position.set(tx,ty,tz);
}