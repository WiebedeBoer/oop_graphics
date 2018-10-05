function Plane(){
                var geometry = new THREE.PlaneGeometry(300, 300, 320);
                var material = new THREE.MeshBasicMaterial({ color: 0x00ff00, side: THREE.DoubleSide });
                var plane = new THREE.Mesh(geometry, material);
                plane.rotation.x = Math.PI / 2.0;
                plane.position.x = 15;
                plane.position.z = 15;
                scene.add(plane);
}