function initVirus () {
				
				prepTouchArea();
			}
			bezierValues = [];
			var positionTop1;
			var positionLeft1;
			var positionTop2;
			var positionLeft2;
			var positionTop3;
			var positionLeft3;
			var hideCount = 0;

			for (var i = 0; i < 150; i ++){
				var valueX = (Math.random()*300);
				var valueY = (Math.random()*700);
				var valueObject = {left: valueX, top: valueY};
				bezierValues.push(valueObject);
				//console.log(bezierValues);
				var virusOnePointValues = bezierValues.slice(0, 30);
				var virusTwoPointValues = bezierValues.slice(31, 60);
				var virusThreePointValues = bezierValues.slice(61, 90);
				var virusFourPointValues = bezierValues.slice(91, 120);
				var virusFivePointValues = bezierValues.slice(121, 149);

			}

			function moveVirusesAround () {
				console.log('moveVirusesAround');
					$('#virus_1').show();
					$('#virus_2').show();
					$('#virus_3').show();
					$('#virus_4').show();
					$('#virus_5').show();
					myTween1 = TweenMax.to( $('#virus_1'), 50, {
					     zIndex:0, rotation: 45,
					     bezier:{curviness:1,values:virusOnePointValues}});					
					myTween1.repeat(-1).yoyo(true);
					TweenMax.to( $('#virus_1'), 3, {scale:.5,repeat:-1, yoyo:true});

					myTween2 = TweenMax.to( $('#virus_2'), 50, {
					     zIndex:0, rotation: 45,
					     bezier:{curviness:1,values:virusTwoPointValues}});					
					myTween2.repeat(-1).yoyo(true);
					TweenMax.to( $('#virus_2'), 5, {scale:1.2,repeat:-1, yoyo:true});

					myTween3 = TweenMax.to( $('#virus_3'), 50, {
					     zIndex:0, rotation: 45,
					     bezier:{curviness:1,values:virusThreePointValues}});					
					myTween3.repeat(-1).yoyo(true);
					TweenMax.to( $('#virus_3'), 4, {scale:.8,repeat:-1, yoyo:true});


					myTween1 = TweenMax.to( $('#virus_4'), 50, {
					     zIndex:0, rotation: 45,
					     bezier:{curviness:1,values:virusFourPointValues}});					
					myTween1.repeat(-1).yoyo(true);
					TweenMax.to( $('#virus_4'), 8, {scale:.3,repeat:-1, yoyo:true});


					myTween1 = TweenMax.to( $('#virus_5'), 50, {
					     zIndex:0, rotation: 45,
					     bezier:{curviness:1,values:virusFivePointValues}});					
					myTween1.repeat(-1).yoyo(true);
					TweenMax.to( $('#virus_5'), 3.5, {scale:1.1,repeat:-1, yoyo:true});
					
				}

			function prepTouchArea () {
			
				//	maskVirus.addEventListener('touchstart', touchStart);
					maskVirus.addEventListener('mousedown', touchStart);
				
			}
			
			function touchStart (evt){
				maskVirus.addEventListener('touchmove', touchMove );
				maskVirus.addEventListener('mousemove', touchMove );
				if (doorOpen){
					hideCount +=1;
					/*
					positionTop3 = $('#virus_3')[0].offsetTop+174;
					positionLeft3 = $('#virus_3')[0].offsetLeft+120;
					
					if (positionTop3+30 > evt.y && positionTop3-30< evt.y){
						$('#virus_3').hide();
						hideCount += 1;
						if (hideCount > 2){
							moveOnToLastPage();
						}
					}

					positionTop1 = $('#virus_1')[0].offsetTop+174;
					positionLeft1 = $('#virus_1')[0].offsetLeft+120;
					
					if (positionTop1+30 > evt.y && positionTop1-30< evt.y){
						$('#virus_1').hide();
						hideCount += 1;
						if (hideCount > 2){
							moveOnToLastPage();
						}
					}

					positionTop2 = $('#virus_2')[0].offsetTop+174;
					positionLeft2 = $('#virus_2')[0].offsetLeft+120;
					
					if (positionTop2+30 > evt.y && positionTop2-30< evt.y){
						$('#virus_2').hide();
						hideCount += 1;
						if (hideCount > 2){
							moveOnToLastPage();
						}
					}	*/
					if (hideCount === 1){
						$('#virus_1').hide();
						
					//	console.log(hideCount);
						console.log('clicked_to_hide_virus_1');
						admx.trackEvent('clicked_to_hide_virus_2');
					} 

					if (hideCount === 2){
						$('#virus_2').hide();
						
					//	console.log(hideCount);
						console.log('clicked_to_hide_virus_2');
						admx.trackEvent('clicked_to_hide_virus_2');
					} 


					if (hideCount === 3){
						$('#virus_3').hide();
						
					//	console.log(hideCount);
						console.log('clicked_to_hide_virus_3');
						admx.trackEvent('clicked_to_hide_virus_3');
					} 
					if (hideCount === 4){
						$('#virus_4').hide();
						
					//	console.log(hideCount);
						console.log('clicked_to_hide_virus_4');
						admx.trackEvent('clicked_to_hide_virus_4');
					}
					if (hideCount === 5){
						$('#virus_5').hide();
						currentAd = 'virus';
						moveOnToLastPage();
					//	console.log(hideCount);
						
						console.log('clicked_to_hide_virus_5');
						admx.trackEvent('clicked_to_hide_virus_5');
					}
					
					maskVirus.addEventListener('touchend', touchEnd);
					maskVirus.addEventListener('mouseup', touchEnd);
				}
			}

			function touchMove (evt){
				
			}
			function touchEnd(){
				maskVirus.removeEventListener('touchmove', touchMove );
				maskVirus.removeEventListener('mousemove', touchMove );

				maskVirus.removeEventListener('touchend', touchEnd );
				maskVirus.removeEventListener('mouseup', touchEnd );
			}

			$(function () {
				initVirus();
			})