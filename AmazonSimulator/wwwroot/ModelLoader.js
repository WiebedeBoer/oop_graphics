            //model loader
            function loadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
                new THREE.MTLLoader()
                    .setPath(texturePath)
                    .load(textureName, function (materials) {
                        materials.preload();
                        new THREE.OBJLoader()
                            .setPath(modelPath)
                            .setMaterials(materials)
                            .load(modelName, function (object) {
                                onload(object);
                            }, function () { }, function (e) { console.log("Error loading"); });
                    });
            }