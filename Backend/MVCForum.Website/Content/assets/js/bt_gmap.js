function bt_gmap_init(id, lat, lng, zoom, icon, primary_color, secondary_color, water_color, custom_style) {

    var myLatLng = new google.maps.LatLng(lat, lng);
    var mapOptions = {
        zoom: zoom,
        center: myLatLng,
        scrollwheel: false,
        scaleControl: true,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL,
            position: google.maps.ControlPosition.RIGHT_CENTER
        },
        streetViewControl: true,
        mapTypeControl: true
    }
    var map = new google.maps.Map(id, mapOptions);

    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        icon: icon
    });

    if ((primary_color != '' && secondary_color != '' && water_color != '') || custom_style != '') {

        var style_array = [
			{
			    featureType: "all",
			    stylers: [
					{ hue: primary_color },
					{ saturation: 100 }
			    ]
			}, {
			    featureType: "road",
			    elementType: "geometry",
			    stylers: [
					{ hue: secondary_color },
					{ saturation: 0 }
			    ]
			}, {
			    featureType: "water",
			    elementType: 'all',
			    stylers: [
					{ color: water_color },
					{ saturation: 0 }
			    ]
			}, {
			    featureType: "poi.business",
			    elementType: "labels",
			    stylers: [
					{ visibility: "off" }
			    ]
			}
        ];

        if (custom_style != '') {
            style_array = JSON.parse(atob(custom_style));
        }

        var customMapType = new google.maps.StyledMapType(style_array, {
            name: 'Custom Style'
        });

        var customMapTypeId = 'custom_style';
        map.mapTypes.set(customMapTypeId, customMapType);
        map.setMapTypeId(customMapTypeId);
    }

      function toggleBounce() {

          if (marker.getAnimation() !== null) {
              marker.setAnimation(null);
          } else {
              marker.setAnimation(google.maps.Animation.BOUNCE);
          }
      }
     
    //google.maps.event.addDomListener(window, 'load', function () {
    //    var bvndgd_123333 = document.getElementById("map");
    //    bt_gmap_init(bvndgd_123333, 10.8049606, 106.6854161, 14, "/Content/assets/images/icon-green.png", "", "", "", "W3siZmVhdHVyZVR5cGUiOiJsYW5kc2NhcGUiLCJzdHlsZXJzIjpbeyJodWUiOiIjRkZCQjAwIn0seyJzYXR1cmF0aW9uIjo0My40MDAwMDAwMDAwMDAwMDZ9LHsibGlnaHRuZXNzIjozNy41OTk5OTk5OTk5OTk5OTR9LHsiZ2FtbWEiOjF9XX0seyJmZWF0dXJlVHlwZSI6InJvYWQuaGlnaHdheSIsInN0eWxlcnMiOlt7Imh1ZSI6IiNGRkMyMDAifSx7InNhdHVyYXRpb24iOi02MS44fSx7ImxpZ2h0bmVzcyI6NDUuNTk5OTk5OTk5OTk5OTk0fSx7ImdhbW1hIjoxfV19LHsiZmVhdHVyZVR5cGUiOiJyb2FkLmFydGVyaWFsIiwic3R5bGVycyI6W3siaHVlIjoiI0ZGMDMwMCJ9LHsic2F0dXJhdGlvbiI6LTEwMH0seyJsaWdodG5lc3MiOjUxLjE5OTk5OTk5OTk5OTk5fSx7ImdhbW1hIjoxfV19LHsiZmVhdHVyZVR5cGUiOiJyb2FkLmxvY2FsIiwic3R5bGVycyI6W3siaHVlIjoiI0ZGMDMwMCJ9LHsic2F0dXJhdGlvbiI6LTEwMH0seyJsaWdodG5lc3MiOjUyfSx7ImdhbW1hIjoxfV19LHsiZmVhdHVyZVR5cGUiOiJ3YXRlciIsInN0eWxlcnMiOlt7Imh1ZSI6IiMwMDc4RkYifSx7InNhdHVyYXRpb24iOi0xMy4yMDAwMDAwMDAwMDAwMDN9LHsibGlnaHRuZXNzIjoyLjQwMDAwMDAwMDAwMDAwNTd9LHsiZ2FtbWEiOjF9XX0seyJmZWF0dXJlVHlwZSI6InBvaSIsInN0eWxlcnMiOlt7Imh1ZSI6IiMwMEZGNkEifSx7InNhdHVyYXRpb24iOi0xLjA5ODkwMTA5ODkwMTEyMzR9LHsibGlnaHRuZXNzIjoxMS4yMDAwMDAwMDAwMDAwMTd9LHsiZ2FtbWEiOjF9XX1d");
    //});



}