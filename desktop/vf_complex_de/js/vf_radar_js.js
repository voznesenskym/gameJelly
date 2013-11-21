function initRadar(){
				
				setUpDivs();
				
				////console.log(audioTag);
			}
			
			
			var trackPlaying = false;
			var audioLoaded = false;
			

		
			function setUpDivs () {
				TweenMax.set($('#sliding_title1'), {scale:.9});
				//TweenMax.set($('#fadeInCopy'), {scale:1.05});
				TweenMax.set($('#sliding_title2'), {scale:.9});

				$('#touchAreaThree').click(function(){
					clearInterval(rotationTimer);
					console.log('touched_screen_to_transition_to_vodafone_info');
					admx.trackEvent('touched_screen_to_transition_to_vodafone_info');
					currentAd = 'radar';
					moveOnToLastPage();
					console.log(stopAllAdsBool);
				})

				

				$('#touchAreaOne').click(function(e){
					//stopAllAdsBool = false;
					//$('#partOne').hide();
					//$('#partTwo').show();
					if (doorOpen){
					$('#touchAreaOne').hide();
					
						$('#partOne').hide();
						$('#partTwo').show();
						
						$('#touchAreaThree').show();
						//console.log('HERE');
						startRadar();
						console.log('tapped_on_screen_to_start_radar');
						admx.trackEvent('tapped_on_screen_to_start_radar');
					//sonarTimer = setInterval(function(){playSonar();},1000);	
					}
				})

				$('#click_to_store').click(function(){
					window.open("https://play.google.com/store/apps/details?id=com.wsandroid.suite.vodaemea&hl=en","_blank");
					console.log('clicked_out_to_store');
					admx.trackEvent('clicked_out_to_store');
				})

				
			}

			

			function moveOnToLastPage(){
				if (!stopAllAdsBool){
					console.log('TRANSITION TO LAST PAGE', new Date().getTime());
					////console.log(stopAllAdsBool);
					////console.log('click');
					TweenMax.to($('#backBtn'),.75,{opacity:0});
					TweenMax.to($('#fadeInCopy'),2,{opacity:1,});
					TweenMax.set($('#sliding_title2'),{opacity:1});
					TweenMax.to($('#sliding_title2'),1,{x:530, scale:1, y:100, ease:Power1.easeOut});
					$('#vodafoneInfo').show();
					transition = false;

					TweenMax.to($('#touchArea'),.75, {scale:.5, top: -598, left: -383, ease:Power2.easeInOut, overwrite:true, onComplete:closeDoors});
					animating = true;
					
				}
			}


			function startRadar () {
				TweenMax.to($('#radarBar'),2,{rotation:"360_cw", ease:Linear.easeNone, repeat:-1, });
				//TweenMax.to($('#radarHolder'),2,{rotation:"360_cw", ease:Linear.easeNone, repeat:-1,});

			
			   rotationTimer = setInterval(function(){monitorTransform();}, 100);
		   
			}

	

			function monitorTransform(){
				var transform = $("#radarBar")[0]._gsTransform;
			            var x = transform.x;
			            var rotation = transform.rotation;
			    ////console.log(rotation);
				checkRotation(rotation);
			}
			var rotationCounter = 0;
			function checkRotation(rotation){
				console.log(rotationCounter);
				if (rotation > 5.2 && rotation < 5.7){

					//audioTag.play();
					TweenMax.to($('#radarBox'),.1,{opacity:1});
					//console.log(rotation);
					
				} else {
					TweenMax.to($('#radarBox'),.8,{opacity:0});
					bleepPlayed = false;
				}

				if (rotation > 6){
					rotationCounter++;
				//	//console.log(rotationCounter);
				}

				////console.log(rotation);

				if (rotationCounter > 3){
					clearInterval(rotationTimer);
					console.log('automatically_after_3_spins_transition_to_vodafone_info');
					admx.trackEvent('automatically_after_3_spins_transition_to_vodafone_info');
					currentAd = 'radar';
					moveOnToLastPage();
					
				}
			}

			$(function () {
				initRadar();
			})