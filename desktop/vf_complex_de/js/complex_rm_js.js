

function ZoomSwipe(area){
	////*console.log('ZoomSwipeInstantiated');
	//TweenMax.set($('.touchRegion'), {scaleX:.5,scaleY:.5})
	TweenMax.set($('#touchArea'), {scale:.5})
	$('#touch'+area).click(function(){
		////*console.log('test');
		topRight.zoomAndOpen(area);	
	})
}

var topRight;
var animating;
var doorOpen = false;
var id;
var stopAllAdsBool = false;
var currentAd;
var wipeChanged = false;
var virusChanged = false;
var lockChanged = false;
var radarChanged = false;

ZoomSwipe.prototype.zoomAndOpen = function(area) {
//	//*console.log(area);
	for (var i = 0; i < 4;i++){
		////*console.log($('.touchRegion')[i].id);
		////*console.log($('.touchRegion')[i].id === 'touch'+area);

		if ($('.touchRegion')[i].id === 'touch'+area){
			id = $('.touchRegion')[i].id;
			//TweenMax.to($('#'+id), .75, {left:0, top:0, scale:1});
			////*console.log('here');
		} else {
			id = $('.touchRegion')[i].id;
			moveOtherAreas(area);
			//TweenMax.to($('#'+id), .75, {opacity:.5});
		}
	}
	//TweenMax.to($('.touchRegion'), 2, {scale:4,opacity:0});
	//TweenMax.to($('#touch'+area), 2, {left:0, top:0, width:600, height:800});
	//if (quadrant === 'TopLeft'){
		//selectTopLeft();
	//}

};




function initHome(){
	topRight = new ZoomSwipe('TopRight');
	topLeft = new ZoomSwipe('TopLeft');
	bottomRight = new ZoomSwipe('BottomRight');
	bottomLeft = new ZoomSwipe('BottomLeft');
	//*console.log('impression');
	admx.trackEvent('impression');
	TweenMax.set($('#doorHolderRadar'), {scale:1});
	TweenMax.set($('#doorHolderLock'), {scale:1});
	TweenMax.set($('#doorHolderWipe'), {scale:1});
	TweenMax.set($('#doorHolderVirus'), {scale:1});


	
	TweenMax.set($('#wrapperRadar'), {scale:.895});
	TweenMax.set($('#wrapperLock'), {scale:.895});
	TweenMax.set($('#wrapperVirus'), {scale:.895});
	TweenMax.set($('#wrapperWipe'), {scale:.895});
}

function moveOtherAreas(id){
	//stopAllAdsBool = false;
	if (!animating){
		if (id === 'TopRight'){
			TweenMax.to($('#touchArea'),.75, {scale:1, top:-254, left:-697, ease:Power2.easeInOut, overwrite:true, onComplete:TopRightDoor});
			//*console.log('on_homepage_top_right_area_clicked');
			admx.trackEvent('on_homepage_top_right_area_clicked');
			$('#radarCopy1').show();
			radarChanged = true;

		} else if (id === 'TopLeft'){
			TweenMax.to($('#touchArea'),.75, {scale:1, top:-254, left:-30, ease:Power2.easeInOut, overwrite:true, onComplete:TopLeftDoor});
			//*console.log('on_homepage_top_left_area_clicked');
			admx.trackEvent('on_homepage_top_left_area_clicked');
			wipeChanged = true;
			$('#wipeCopy').show();

		} else if (id === 'BottomRight'){
			TweenMax.to($('#touchArea'),.75, {scale:1, top:-1250, left:-697, ease:Power2.easeInOut, overwrite:true,  onComplete:BottomRightDoor});
			//*console.log('on_homepage_bottom_right_area_clicked');
			admx.trackEvent('on_homepage_bottom_right_area_clicked');
			$('#protectCopy').show();
			lockChanged = true;

		} else if (id === 'BottomLeft'){
			TweenMax.to($('#touchArea'),.75, {scale:1, top:-1250, left:-33, ease:Power2.easeInOut, overwrite:true, onComplete:BottomLeftDoor});
			//*console.log('on_homepage_bottom_left_area_clicked');
			admx.trackEvent('on_homepage_bottom_left_area_clicked');
			virusChanged = true;
			$('#virusCopy').show();
		}
		
		animating = true;
	}
	

}

function TopRightDoor(){
	doorOpen = true;
	TweenMax.to($('#doorRadar'),.75, {left:-600});
	TweenMax.to($('#backBtn'),.75,{opacity:1});
}

function BottomRightDoor(){
	doorOpen = true;
	TweenMax.to($('#doorLock'),.75, {left:-600});
	TweenMax.to($('#backBtn'),.75,{opacity:1});
}

function TopLeftDoor(){
	doorOpen = true;
	TweenMax.to($('#doorWipe'),.75, {left:-600});
	TweenMax.to($('#backBtn'),.75,{opacity:1});

}

function BottomLeftDoor(){
	moveVirusesAround();
	doorOpen = true;
	TweenMax.to($('#doorVirus'),.75, {left:-600});
	TweenMax.to($('#backBtn'),.75,{opacity:1});
}

function closeDoors(){
	doorOpen = false;
	TweenMax.to($('.door'),.75, {left:0, onComplete:stopAllAds});
	
}

function selectTopLeft(){
	TweenMax.to($('.touchRegion'), 2, {scale:4,opacity:0});
	TweenMax.to($('.touchRegion'), 2, {scale:4,opacity:0});
}




function stopAllAds () {
	console.log('STOP ALL ADS', new Date().getTime());
	//*console.log(performance.memory);
	stopAllAdsBool = true;
	

	if (wipeChanged) {
		initWipe();
		wipeChanged = false;
		stopAllAdsBool = false;
	}

	if (virusChanged) {
		hideCount = 0;
		TweenMax.killTweensOf($('#virus_1'));
		TweenMax.killTweensOf($('#virus_2'));
		TweenMax.killTweensOf($('#virus_3'));
		TweenMax.killTweensOf($('#virus_4'));
		TweenMax.killTweensOf($('#virus_5'));
		$('#virus_1').hide();
		$('#virus_2').hide();
		$('#virus_3').hide();
		$('#virus_4').hide();
		$('#virus_5').hide();
		virusChanged = false;
		stopAllAdsBool = false;
	}

	if (radarChanged){
		TweenMax.set($('#radarBar'),{rotation:0});
		$('#partOne').show();
		$('#partTwo').hide();
		$('#touchAreaThree').hide();
		$('#touchAreaOne').show();
		radarChanged = false;
		stopAllAdsBool = false;
	}
	
	//clearInterval(rotationTimer);
	//question: On lock, do we allow interrupt to go to bg, or do we make user continue with it
	if (lockChanged){
		TweenMax.set($('#arms'), {z:41, rotation:0});
		TweenMax.set($('#doorHolder'), {rotationY:-130, transformStyle:"preserve-3d", transformPerspective:2000, transformOrigin:"left"});
		lockChanged = false;
	}
}

$(function(){
	updateAdSize('mainWrapper');
	initHome();

	$('#backToHome').click(function(){
		//stopAllAds();
		$('.copy').hide();
		console.log('BACK LINK PRESSED', new Date().getTime());
		TweenMax.set($('#sliding_title2'),{opacity:1});
		TweenMax.to($('#sliding_title2'),1,{x:0, scale:.9, y:0, ease:Power1.easeOut});
		TweenMax.to($('#sliding_title1'),1,{x:0, scale:.9, y:0, ease:Power1.easeOut});
		$('#vodafoneInfo').hide();
		TweenMax.set($('#fadeInCopy'),{opacity:0});
		rotationCounter = 0;
		$('#touchAreaOne').show();
		$('#touchAreaTwo').hide();
		
		
		//initVirus();
		//positionDoor();
		animating = false;
		//*console.log('clicked_on_back_button_from_vodafone_final_screen_from_ad_'+currentAd);
		admx.trackEvent('clicked_on_back_button_from_vodafone_final_screen_from_ad_'+currentAd);
		
	})

	$('#backBtn').click(function(){
		$('.copy').hide();
		console.log('BACK BUTTON PRESSED', new Date().getTime());
		//stopAllAds();
		//stopAllAdsBool = false;
		if (animating){
			//*console.log('on_homepage_background_clicked_away_from_'+id);
			admx.trackEvent('on_homepage_background_clicked_away_from_'+id);
			animating = false;
		}
			TweenMax.to($('#touchArea'),.75, {scale:.5, top: -598, left: -383, ease:Power2.easeInOut, overwrite:true, onComplete:closeDoors});
			TweenMax.set($('#backBtn'),{opacity:0});
			
	//	}
	})
})

