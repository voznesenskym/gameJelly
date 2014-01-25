function initLock () {
				positionDoor();
				
			}

			function positionDoor(){
				TweenMax.set($('#logo'), {z:42});
				TweenMax.set($('#arms'), {z:41, rotation:0});
				TweenMax.set($('#door'), {z:40});
				TweenMax.set($('#door_back'), {z:00});
				
				
				TweenMax.set($('#doorHolder'), {rotationY:-130, transformStyle:"preserve-3d", transformPerspective:2000, transformOrigin:"left"});
				setUpClicksRadar();
			}

			function setUpClicksRadar () {
				$('#vaultBackTouchArea').click(function(){
					if (doorOpen){
						stopAllAdsBool = false;
						console.log('wheel_locked_transition_to_vodafone_info');
						admx.trackEvent('wheel_locked_transition_to_vodafone_info');
						TweenMax.to($('#doorHolder'), 1, {rotationY:0, transformStyle:"preserve-3d", left:100,transformPerspective:2000, transformOrigin:"left", onComplete:spinWheel})
					}
				})
			}

			function spinWheel(){
				TweenMax.to($('#arms'), 2, {rotation:-360, ease:Power1.easeOut, transformOrigin:"center", z:41, onComplete:moveOnToLastPage});
			}
			$(function () {
				initLock();
			})