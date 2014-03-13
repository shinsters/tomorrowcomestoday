(function ($) {
    $(function () {
        var homepageMap = {
            init: function () {
                homepageMap.startMap();
                homepageMap.pullLocations();
            },
            startMap: function () {
                var $canvas = $(".homepage #map-canvas"),
                    $dateRange = $(".homepage #map-daterange"),
                    $listGroup = $(".homepage #map-listgroup");

                homepageMap.map = new google.maps.Map(
                    $canvas[0], 
                    {
                        center: new google.maps.LatLng(53.801576, -1.546903),
                        zoom: 5,
                        mapTypeId: google.maps.MapTypeId.ROADMAP
                    }
                );
            },
            pullLocations: function () {
                homepageMap.startLocation({
                    lat: 53.801576, 
                    long: -1.546903,
                    name: 'Leeds Hack',
                    start: '2013-08-11T12:00:00+0100',
                    slug: "leeds-hack",
                    description: "This is a description of the Meet. It might be quite long! Who knows.",
                    within: "thisweek"
                });
                homepageMap.startLocation({
                    lat: 53.478086, 
                    long: -2.244669,
                    name: 'Manchester Hack',
                    start: '2013-08-11T12:00:00+0100',
                    slug: "manchester-hack",
                    description: "This is a description of the Meet. It might be quite long! Who knows.",
                    within: "thisweek"
                });
                homepageMap.zoomToFit();
            },
            zoomToFit: function () {
                var i;
                
                var bounds = new google.maps.LatLngBounds();
                
                for (i = 0; i < homepageMap.markers.length; i++) {
                    var marker = homepageMap.markers[i];
                    if (marker.getVisible()) {
                        bounds.extend(marker.getPosition());
                    }
                }
                
                homepageMap.map.fitBounds(bounds);
            },
            startLocation: function (location) {
                var newInfo = new google.maps.InfoWindow({content: "<h3>" + location.name + "</h3><p>" + location.description + "</p><a href=\"#" + location.slug + "\">View Meet</a>"}),
                    newSide = $("<div class=\"list-group-item\"><strong>" + location.name + "</strong><em class=\"pull-right\"><abbr class=\"timeago\" title=\"" + location.start + "\">" + location.start + "</abbr></em></div>"),
                    newPin = new google.maps.Marker({
                        position: new google.maps.LatLng(location.lat, location.long),
                        map: homepageMap.map,
                        title: location.name
                    });
                    
                    reply = newSide.appendTo(".homepage #map-listgroup");
                    
                    console.log(newSide);
                    
                    $(".timeago").timeago();
                    
                    newSide.on("mouseover", function () {
                        console.log("Hover!");
                        newPin.setIcon("http://maps.google.com/mapfiles/ms/icons/green-dot.png");
                    });
                    newSide.on("mouseout", function () {
                        console.log("blur!");
                        newPin.setIcon("http://maps.google.com/mapfiles/ms/icons/red-dot.png");
                    });
                
                google.maps.event.addListener(newPin, 'click', function () {
                    newInfo.open(homepageMap.map, newPin);
                });
                google.maps.event.addListener(newPin, 'mouseover', function () {
                    newSide.addClass("text-primary");
                });
                google.maps.event.addListener(newPin, 'mouseout', function () {
                    newSide.removeClass("text-primary");
                });
                
                newPin.newSide = newSide;
                
                homepageMap.markers.push(newPin);
            },
            map: null,
            markers: []
        };
        
        homepageMap.init();
    });
})(jQuery);