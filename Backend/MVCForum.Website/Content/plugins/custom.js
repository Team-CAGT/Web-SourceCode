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
(function ($) {

  "use strict";


  // Preloader 
  function stylePreloader() {
    if ($('.preloader').length) {
      $('.preloader').delay(300).fadeOut(600);
    }
  }

  // Header Style

  if ($('.header-main').length) {
    $('.header-main').scrollToFixed();
  }

  $("#menuzord").menuzord({
    align: "left",
    indicatorFirstLevel: "",
    indicatorSecondLevel: ""
  });

  if ($('.toggle-menu').length) {
    $('.toggle-menu').jPushMenu();
  }

  $(function () {
    "use strict";
    var $onepage_nav = $('.sp-megamenu');
    var nav_height = $onepage_nav.outerHeight();
    var topoffset = nav_height;

    $('body').scrollspy({
      target: '.sp-megamenu',
      offset: topoffset
    });

    $('a.page-scroll[href*=#]:not([href=#])').click(function () {
      if (location.pathname.replace(/^\//, '') ===
        this.pathname.replace(/^\//, '') &&
        location.hostname === this.hostname) {
        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
        if (target.length) {
          $('html,body').animate({
            scrollTop: target.offset().top - topoffset + 2
          }, 1500, 'easeInOutExpo');
          return false;
        }
      }
    });
  });

  // Switcher Js

  if ($('#switcher-toggle').length) {
    $("#switcher-toggle").on('click', function (e) {
      e.preventDefault();
      $(".menu-switcher").toggleClass("switcher-show");
    });
  }

  // Boxed Layout   
  if ($('.btn-boxedlayout').length) {
    $(".btn-boxedlayout").on('click', function () {
      $('body').addClass('boxed-layout-style');
      $('body').removeClass('wide-layout-style');
    });
  }

  // Wide Layout
  if ($('.btn-widelayout').length) {
    $(".btn-widelayout").on('click', function () {
      $('body').addClass('wide-layout-style');
      $('body').removeClass('boxed-layout-style');
    });
  }

  // Switcher Background Image
  if ($('.switcher-bgi-solid').length) {
    $('.switcher-bgi-solid').on('click', function () {
      $('body').css('background-image', 'url(' + this.src + ')');
      $('body').addClass('solid-bgi-body');
      $('body').removeClass('pattern-bgi-body');
    });
  }

  // Switcher Background Pattern Image
  if ($('.switcher-bgi-pattern').length) {
    $('.switcher-bgi-pattern').on('click', function () {
      $('body').css('background-image', 'url(' + this.src + ')');
      $('body').addClass('pattern-bgi-body');
      $('body').removeClass('solid-bgi-body');
    });
  }

  // Side Menu Dropdown
  if ($('.sidemenu-dropdown').length) {
    $(".sidemenu-dropdown").on('click', function () {
      $('.sidemenu-dropdown-active').not($(this).children("ul")).hide();
      $(this).children(".sidemenu-dropdown-active").slideToggle('fast');
    });
  }

  // Owl Carousel
  if ($('.wrapper').length) {

    //client-slider
    $('.client-slider').owlCarousel({
      loop: true,
      margin: 60,
      dots: false,
      nav: true,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      smartSpeed: 500,
      autoplay: 2000,
      items: 6,
      responsiveClass: true,
      responsive: {
        0: {
          items: 2,
          loop: true
        },
        480: {
          items: 3,
          loop: true
        },
        600: {
          items: 4,
          loop: true
        },
        1000: {
          items: 5,
          loop: true
        },
        1200: {
          items: 6,
          loop: true
        },
      }
    });

    //testimonial-slider
    $('.testimonial-slider').owlCarousel({
      loop: true,
      margin: 15,
      dots: true,
      nav: false,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      smartSpeed: 500,
      autoplay: false,
      items: 3,
      responsiveClass: true,
      responsive: {
        0: {
          items: 1,
          loop: true
        },
        480: {
          items: 1,
          loop: true
        },
        600: {
          items: 2,
          loop: true
        },
        1000: {
          items: 2,
          loop: true
        },
        1200: {
          items: 3,
          loop: true
        },
      }
    });

    //gallery-carousel
    $('.gallery-carousel').owlCarousel({
      loop: true,
      margin: 3,
      dots: false,
      nav: true,
      autoplayHoverPause: false,
      autoplay: 5000,
      smartSpeed: 700,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      responsive: {
        0: {
          items: 1
        },
        480: {
          items: 1
        },
        600: {
          items: 2
        },
        800: {
          items: 2
        },
        1024: {
          items: 3
        },
        1200: {
          items: 4
        }
      }
    });

    //owl-Carousel-OneColumn
    $('.owl-column.owl-col-1').owlCarousel({
      loop: true,
      autoplay: 5000,
      autoplayHoverPause: false,
      smartSpeed: 700,
      items: 1,
      margin: 15,
      dots: true,
      nav: true,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ]
    });

    //owl-Carousel-TwoColumn
    $('.owl-column.owl-col-2').owlCarousel({
      loop: true,
      autoplay: 5000,
      autoplayHoverPause: false,
      smartSpeed: 700,
      items: 2,
      margin: 30,
      dots: true,
      nav: true,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      responsive: {
        0: {
          items: 1
        },
        480: {
          items: 1
        },
        600: {
          items: 2
        },
        800: {
          items: 2
        },
        1024: {
          items: 2
        },
        1200: {
          items: 2
        }
      }
    });

    //owl-Carousel-ThreeColumn
    $('.owl-column.owl-col-3').owlCarousel({
      autoplay: false,
      autoplayHoverPause: false,
      smartSpeed: 700,
      items: 3,
      margin: 30,
      dots: true,
      nav: true,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      responsive: {
        0: {
          items: 1
        },
        480: {
          items: 1
        },
        748: {
          items: 2
        },
        1000: {
          items: 2
        },
        1200: {
          items: 3
        },
      }
    });

    //owl-Carousel-FourColumn
    $('.owl-column.owl-col-4').owlCarousel({
      loop: true,
      autoplay: 5000,
      autoplayHoverPause: false,
      smartSpeed: 700,
      items: 4,
      margin: 30,
      dots: true,
      nav: true,
      navText: [
        '<i class="fa fa-angle-left"></i>',
        '<i class="fa fa-angle-right"></i>'
      ],
      responsive: {
        0: {
          items: 1,
          loop: true
        },
        480: {
          items: 1,
          loop: true
        },
        600: {
          items: 2,
          loop: true
        },
        1000: {
          items: 3,
          loop: true
        },
        1200: {
          items: 4,
          loop: true
        },
      }
    });

  }

  // Isotope-jquery
  // filter items on button click
  function isotopeGallery() {
    if ($('.isotopeFilter a').length) {
      $('.isotopeFilter a').on('click', function () {
        $('.isotopeFilter .current').removeClass('current');
        $(this).addClass('current');

        var selector = $(this).attr('data-filter');
        $container.isotope({
          filter: selector,
          animationOptions: {
            duration: 750,
            easing: 'linear',
            queue: false
          }
        });
        return false;
      });
    }

    // init Isotope
    if ($('.isotopeContainer').length) {
      var $container = $('.isotopeContainer');
      $container.isotope({
        filter: '*',
        animationOptions: {
          duration: 750,
          easing: 'linear',
          queue: false
        }
      });
    }
  }

  //LightBox / Fancybox
  if ($('.lightbox-image').length) {
    $('.lightbox-image').fancybox();
  }

  if ($('.lightbox-gallery').length) {
    $(".lightbox-gallery").fancybox({
      openEffect: 'none',
      closeEffect: 'none'
    });
  }




  if ($('.fancybox-media').length) {
    $('.fancybox-media').fancybox({
      openEffect: 'none',
      closeEffect: 'none',
      helpers: {
        media: {}
      }
    });
  }
  // datePicker
  if ($('.date-picker').length) {
    $('.date-picker').datepicker();
  }

  //Skill Progress Bar
  if ($('.progress-item').length) {
    $('.progress-item').each(function () {
      $(this).find('.bar-progress').animate({
        width: $(this).attr('data-percent')
      }, 2000);
    });
  }

  //Tabs Box
  if ($('.tabs-section').length) {
    $('.tabs-section .tab-btn').on('click', function (e) {
      e.preventDefault();
      var target = $($(this).attr('href'));
      $('.tabs-section .tab-btn').removeClass('active');
      $(this).addClass('active');
      $('.tabs-section .tab').fadeOut(0);
      $('.tabs-section .tab').removeClass('active-tab');
      $(target).fadeIn(300);
      $(target).addClass('active-tab');
      var windowWidth = $(window).width();
      if (windowWidth <= 700) {
        $('html, body').animate({
          scrollTop: $('.tabs-section .content-column').offset().top - 100
        }, 1000);
      }
    });

  }

  //Skill Progress Bar
  if ($('.skill-box .bar-fill').length) {
    $(".skill-box .bar-fill").each(function () {
      var progressWidth = $(this).attr('data-percent');
      $(this).css('width', progressWidth + '%');
      $(this).parents('.bar').children('.percent').html(progressWidth + '%');
    });
  }

  // CounterNumberChanger
  function counter_number() {
    var timer = $('.timer');
    if (timer.length) {
      timer.appear(function () {
        timer.countTo();
      });
    }
  }

  //Accordions
  if ($('.accordion-style').length) {
    $('.accordion-style .acc-title').on('click', function () {
      if ($(this).hasClass('active') !== true) {
        $('.accordion-style .acc-title').removeClass('active');
      }

      if ($(this).next('.acc-content').is(':visible')) {
        $(this).removeClass('active');
        $(this).next('.acc-content').slideUp(500);
      } else {
        $(this).addClass('active');
        $('.accordion-style .acc-content').slideUp(500);
        $(this).next('.acc-content').slideDown(500);
      }
    });
  }

  // fullScreen div style
  if ($('.fullScreen').length) {
    $('.fullScreen').css({
      'height': ($(window).height()) + 'px'
    });
    $(window).resize(function () {
      $('.fullScreen').css({
        'height': ($(window).height()) + 'px'
      });
    });
  }

  // Div Topfixed style 
  if ($('.div-topfixed').length) {
    var summaries = $('.div-topfixed');
    summaries.each(function (i) {
      var summary = $(summaries[i]);
      var next = summaries[i + 1];

      summary.scrollToFixed({
        marginTop: $('.header-main').outerHeight(true) + 10,
        limit: function () {
          var limit = 0;
          if (next) {
            limit = $(next).offset().top - $(this).outerHeight(true) - 10;
          }
          return limit;
        },
        zIndex: 999
      });
    });
  }

  // Scroll to top btn
  if ($('.scroll-to-top').length) {
    //Check to see if the window is top if not then display button
    $(window).scroll(function () {
      if ($(this).scrollTop() > 250) {
        $('.scroll-to-top').fadeIn();
      } else {
        $('.scroll-to-top').fadeOut();
      }
    });

    //Click event to scroll to top
    $('.scroll-to-top').on('click', function () {
      $('html, body').animate({
        scrollTop: 0
      }, 800);
      return false;
    });
  }

  // Elements Animation
  if ($('.wow').length) {
    var wow = new WOW({
      boxClass: 'wow',
      animateClass: 'animated',
      offset: 0,
      mobile: true,
      live: true
    });
    wow.init();
  }

  /* ==========================================================================
     When document is ready, do
     ========================================================================== */

  $(document).on('ready', function () {
    counter_number();
  });

  /* ==========================================================================
     When document is Scrollig, do
     ========================================================================== */

  $(window).on('scroll', function () {
    isotopeGallery();
  });

  /* ==========================================================================
     When document is loading, do
     ========================================================================== */

  $(window).on('load', function () {
    stylePreloader();
    isotopeGallery();
  });


  /* ==========================================================================
     When Window is resizing, do
     ========================================================================== */

  $(window).on('resize', function () {});

  $("#hero-slide").owlCarousel({
    loop: true,
    autoplay: true,
    autoplayTimeout: 4000,
    dots: false,
    margin: 0,
    nav: false,
    items: 1,
    animateOut: 'fadeOut',
    animateIn: 'fadeIn',
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false
  })



})(window.jQuery);