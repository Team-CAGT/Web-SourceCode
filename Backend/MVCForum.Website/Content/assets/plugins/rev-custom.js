/**
=============================================================
[Custom Js]

Template:     eMedical
Version:      2.0
Created:      10.30.2016
Primary use:  Health & Medical Responsive HTML Template

@Author: icute_theme
=============================================================
**/
(function($) {
  
  "use strict";


  // Home Slider
  function revSlider() {
  if($('.home-slider .tp-banner').length){
    jQuery('.home-slider .tp-banner').show().revolution({
      delay:10000,
      startwidth:1200,
      startheight:620,
      hideThumbs:600,
  
      thumbWidth:80,
      thumbHeight:50,
      thumbAmount:5,
  
      navigationType:"bullet",
      navigationArrows:"0",
      navigationStyle:"preview4",
  
      touchenabled:"on",
      onHoverStop:"off",
  
      swipe_velocity: 0.7,
      swipe_min_touches: 1,
      swipe_max_touches: 1,
      drag_block_vertical: false,
  
      parallax:"mouse",
      parallaxBgFreeze:"on",
      parallaxLevels:[7,4,3,2,5,4,3,2,1,0],
  
      keyboardNavigation:"off",
  
      navigationHAlign:"center",
      navigationVAlign:"bottom",
      navigationHOffset:0,
      navigationVOffset:20,
  
      soloArrowLeftHalign:"left",
      soloArrowLeftValign:"center",
      soloArrowLeftHOffset:20,
      soloArrowLeftVOffset:0,
  
      soloArrowRightHalign:"right",
      soloArrowRightValign:"center",
      soloArrowRightHOffset:20,
      soloArrowRightVOffset:0,
  
      shadow:0,
      fullWidth:"on",
      fullScreen:"off",
  
      spinner:"spinner4",
  
      stopLoop:"off",
      stopAfterLoops:-1,
      stopAtSlide:-1,
  
      shuffle:"off",
  
      autoHeight:"off",
      forceFullWidth:"on",
  
      hideThumbsOnMobile:"on",
      hideNavDelayOnMobile:1500,
      hideBulletsOnMobile:"on",
      hideArrowsOnMobile:"on",
      hideThumbsUnderResolution:0,
  
      hideSliderAtLimit:0,
      hideCaptionAtLimit:0,
      hideAllCaptionAtLilmit:0,
      startWithSlide:0,
      videoJsPath:"",
      fullScreenOffsetContainer: ".home-slider"
    });    
  }
  }

$('.home-slider').resizable({
	start: function(event, ui) {
	
		console.log($('.home-slider').width());
	
	},
	stop: function(event, ui) {
	
		console.log($('.home-slider').width());
	
	}	
});




  
/* ==========================================================================
   When document is loading, do
   ========================================================================== */
  
  $(window).on('load', function() {
    revSlider();
  });
  

/* ==========================================================================
   When Window is resizing, do
   ========================================================================== */
  
  $(window).on('resize', function(){	  
    revSlider();
  });
  

})(window.jQuery);