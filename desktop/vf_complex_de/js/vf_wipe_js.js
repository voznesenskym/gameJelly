window.addEventListener("load",function() {
		  // Set a timeout...
		  setTimeout(function(){
			window.scrollTo(0, 1);
		  }, 0);
		});
		
		var stage,
			toolPointX,
			toolPointY;
			
		var columns = 13;
		var rows = 16;
		var tileW = 55;
		var tileH = 55;
		var tileDic = {};
		var percentArray = [];
		var sawNoise;
		var ctaUp = true;
		var cta;
		var transition = false;
		var wiped0 = false;
		var wiped25 = false;
		var wiped50 = false;
	
		
        function initWipe() {
        	
			
        	//console.log('here');

			doorHolderWipe.addEventListener('touchstart', touchStart);
			doorHolderWipe.addEventListener('mousedown', touchStart);

			function touchStart(evt){
				evt.preventDefault();
				doorHolderWipe.addEventListener('touchmove', touchMove );
				doorHolderWipe.addEventListener('mousemove', touchMove );
				
			}

			function touchMove(evt){
				evt.preventDefault();
				
				clickBehavior(evt);
				doorHolderWipe.addEventListener('touchend', touchEnd);
				doorHolderWipe.addEventListener('mouseup', touchEnd);
				
			}
			//sawNoise = new Audio("game/saw.wav"); // buffers automatically when created
			
			//sawNoise.addEventListener('ended',function(event){
				//sawNoise.play();
			//},true);

		    canvas = document.getElementById("demoCanvas");
			stage = new createjs.Stage(canvas);
			createjs.Touch.enable(stage);
			
			
					  
			
			
			
			
			
			
			
			
			
			
			var count = 0;
			for (var y = 0; y < rows; y++) {
				for (var x = 0; x < columns; x++) {
					var number = Math.floor(Math.random()*10);
					//console.log(number);
					if (number % 2){
						var tile_img = new Image();
						tile_img.src = "style/media_vf_wipe/wipe_zero.png";
						var texture = new createjs.Bitmap(tile_img);
					} else {
						var tile_img = new Image();
						tile_img.src = "style/media_vf_wipe/wipe_one.png";
						var texture = new createjs.Bitmap(tile_img);
					}
					
			
			
					
					
				
					
					var tile = new createjs.Container();
					tile.addChild(texture);
					
					tile.regX = tileW/2;
					tile.regY = tileH/2;
			
					// set up velocities:
					var speed = 1.8;
					var a = Math.PI*2*Math.random();
					var v = (Math.random()-0.5)*30*speed;
					tile.vX = Math.cos(a)*v;
					tile.vY = Math.sin(a)*v;
					tile.vS = (Math.random()-0.5)*0.2; // scale
					//tile.vA = -Math.random()*0.05-0.01; // alpha
					tile.rot = 180 * Math.random()|0;
		
					tile.x = (x * tileW);
					tile.y = (y * tileH);
					
					
					var tileObj = {};
					tileObj.shape = tile;
					tileObj.active = true;
					
					tileDic[count] = tileObj;
					
					count += 1;
					
					stage.addChild(tile);
					

				}
			}
		
			//stage.addChild(saw, logo, cta);
		
		
			
		

			
			function clickBehavior(evt) {
				//console.log(evt.pageY);
				evt.preventDefault();
				if (ctaUp){
					stage.removeChild(cta);
					ctaUp = false;
				}
				
				
				
				var offset = {x:evt.pageX, y:evt.pageY};
				//console.log(evt)
				// add a handler to the event object's onMouseMove callback
				// this will be active until the user releases the mouse button:
				
					//console.log(evt.pageX);
					if (evt.pageX<700){
						var columnCount = Math.floor((evt.pageX-100) / tileW);
						var rowCount =  Math.floor((evt.pageY-155) / tileH);
					}
					var tileNum = columnCount + (columns * rowCount);
					//console.log(evt.pageX-100/20);
					//console.log(columnCount, rowCount, tileNum);
					//console.log(ev.stageY,ev.stageX);
					//console.log(tileNum);
					
					var currentTileObj = tileDic[tileNum];
					//if (currentTileObj.active){
						var currentTile = currentTileObj.shape;
						//console.log(percentArray.indexOf(currentTile));

						if (percentArray.indexOf(currentTile)===-1){
							percentArray.push(currentTile);
						//	console.log(percentArray.length);
						}
						if (percentArray.length > 0){
							if (!wiped0){
								wiped0 = true;
								console.log('on_wipe_ad_started_interacting_and_wiped_one_number');
								admx.trackEvent('on_wipe_ad_started_interacting_and_wiped_one_number');
							}
						}
						if (percentArray.length > 5){
							if (!wiped25){
								wiped25 = true;
								console.log('on_wipe_ad_started_wiped_5_numbers');
								admx.trackEvent('on_wipe_ad_started_wiped_5_numbers');
							}
						}
						if (percentArray.length > 15){
							if (!wiped50){
								wiped50 = true;
								console.log('on_wipe_ad_started_wiped_15_numbers');
								admx.trackEvent('on_wipe_ad_started_wiped_15_numbers');
							}
						}
						if (percentArray.length > 25){
							if (!transition){
								transition = true;
								currentAd = 'wipe';
								moveOnToLastPage();
								console.log('on_wipe_ad_started_wiped_25_numbers_and_transitioned_to_info_page');
								admx.trackEvent('on_wipe_ad_started_wiped_25_numbers_and_transitioned_to_info_page');

								wiped0 = false;
								wiped25 = false;
								wiped50 = false;
								percentArray = [];
							}
						}
						
						stage.addChild(currentTile);
						currentTileObj.active = false;
						
						
						currentTile.onTick = function() {
							// apply gravity and friction
							currentTile.vY += 2;
							//currentTile.vX *= 1;

							// update position, scale, and alpha:
							currentTile.x += currentTile.vX;
							currentTile.y += currentTile.vY;
							currentTile.rotation += currentTile.rot;
							//currentTile.scaleX = currentTile.scaleY = currentTile.scaleX + currentTile.vS;
							//currentTile.alpha += -.01;

							//remove sparkles that are off screen or not invisble
							if (currentTile.alpha <= 0 || currentTile.y > stage.height) {
								stage.removeChild(currentTile);
								
							}
						}
						//currentTile.alpha = 0;
						//console.log(currentTileObj.shape);
					//}
					
				
			}
				

			function touchEnd(evt){
			
				doorHolderWipe.removeEventListener('touchmove', touchMove );
				doorHolderWipe.removeEventListener('mousemove', touchMove );

				doorHolderWipe.removeEventListener('touchend', touchEnd );
				doorHolderWipe.removeEventListener('mouseup', touchEnd );
			
			}
	
			
			createjs.Ticker.addListener(stage);
			createjs.Ticker.setFPS(20);
			//stage.update();
        }
		
	
	
	$(function(){
		initWipe();
	})

		
		
		
		