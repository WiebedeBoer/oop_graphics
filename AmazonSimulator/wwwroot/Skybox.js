class Skybox extends THREE.Group {
	init(){
				var mskyBox = this;
				var imagePrefix = "images/skybox";
	            var directions  = ["xpos", "xneg", "ypos", "yneg", "zpos", "zneg"];
	            var imageSuffix = ".png";
	            var skyGeometry = new THREE.CubeGeometry( 1000, 1000, 1000 );	
	
	            var materialArray = [];
	            for (var i = 0; i < 6; i++)
		            materialArray.push( new THREE.MeshBasicMaterial({
			        map: THREE.ImageUtils.loadTexture( imagePrefix + directions[i] + imageSuffix ),
			        side: THREE.BackSide
		            }));
	            var skyMaterial = new THREE.MeshFaceMaterial( materialArray );
				var skyBox = new THREE.Mesh( skyGeometry, skyMaterial );
				mskyBox.add(skyBox);
				//scene.add( skyBox );
			}

    
			constructor(){
				super();
				//this._LoadState = LoadStates.NOT_LOADING;
				this.init();
			}	
        }