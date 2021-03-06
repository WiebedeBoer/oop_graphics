class Skyshader extends THREE.Group {

	
	
	init(){	
		var msphere = this;
		//var sky;

		//var sky, sunSphere;
			//function initSky() {
				
				// Add Sky
				var sky = new THREE.Sky();
				sky.scale.setScalar( 4500 );
				//scene.add( sky );
				// Add Sun Helper
				

				var sunSphere = new THREE.Mesh(
					new THREE.SphereBufferGeometry( 20, 16, 8 ),
					new THREE.MeshBasicMaterial( { color: 0xff0000 } )
				);
				sunSphere.position.y = 70;
				sunSphere.visible = false;
				msphere.add( sunSphere );
				msphere.add( sky );
				/// GUI
				var effectController  = {
					turbidity: 10,
					rayleigh: 2,
					mieCoefficient: 0.051,
					mieDirectionalG: 0.8,
					luminance: 0.5,
					inclination: 0.49, // elevation / inclination
					azimuth: 0.25, // Facing front,
					sun: ! true
				};
				var distance = 400;
				
				//function guiChanged() {
					var uniforms = sky.material.uniforms;
					uniforms.turbidity.value = effectController.turbidity;
					uniforms.rayleigh.value = effectController.rayleigh;
					uniforms.luminance.value = effectController.luminance;
					uniforms.mieCoefficient.value = effectController.mieCoefficient;
					uniforms.mieDirectionalG.value = effectController.mieDirectionalG;
					var theta = Math.PI * ( effectController.inclination - 0.5 );
					var phi = 2 * Math.PI * ( effectController.azimuth - 0.5 );
					sunSphere.position.x = distance * Math.cos( phi );
					sunSphere.position.y = distance * Math.sin( phi ) * Math.sin( theta );
					sunSphere.position.z = distance * Math.sin( phi ) * Math.cos( theta );
					sunSphere.visible = effectController.sun;
					uniforms.sunPosition.value.copy( sunSphere.position );
					//renderer.render( scene, camera );
				//}
				/*
				var gui = new dat.GUI();
				gui.add( effectController, "turbidity", 1.0, 20.0, 0.1 ).onChange( guiChanged );
				gui.add( effectController, "rayleigh", 0.0, 4, 0.001 ).onChange( guiChanged );
				gui.add( effectController, "mieCoefficient", 0.0, 0.1, 0.001 ).onChange( guiChanged );
				gui.add( effectController, "mieDirectionalG", 0.0, 1, 0.001 ).onChange( guiChanged );
				gui.add( effectController, "luminance", 0.0, 2 ).onChange( guiChanged );
				gui.add( effectController, "inclination", 0, 1, 0.0001 ).onChange( guiChanged );
				gui.add( effectController, "azimuth", 0, 1, 0.0001 ).onChange( guiChanged );
				gui.add( effectController, "sun" ).onChange( guiChanged );
				*/
				//guiChanged();

			//}
			
			//initSky();
		}


    
			constructor(){
				super();
				//guiChanged();
				//initSky();
				//this._LoadState = LoadStates.NOT_LOADING;
				this.init();
			}	
    }