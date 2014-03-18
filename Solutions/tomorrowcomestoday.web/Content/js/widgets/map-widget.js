(function () {
    var mapWidget = {
        initialize: function () {
            mapWidget.findMapCanvases();
        },
        findMapCanvases: function () {
            $("[data-mapwidget-lat]").each(function (i, el) {
                
                var $el = $(el).clone(),
                    options = {},
                    lat,
                    long,
                    map,
                    pins;

                options.center = mapWidget.parseCenter($el);

                options.zoom = $el.data("mapwidget-zoom") ? $el.data("mapwidget-zoom") : 8;

                options.mapTypeId = google.maps.MapTypeId.ROADMAP;

                map = mapWidget.startMap(el, options);
                
                mapWidget.findMapPins($el, map);

            });
        },
        parseCenter: function ($el) {
            var lat = ($el.data("mapwidget-lat") ? $el.data("mapwidget-lat") : 53.801576),
                long = ($el.data("mapwidget-lng") ? $el.data("mapwidget-lng") : ($el.data("mapwidget-long") ? $el.data("mapwidget-long") : -1.546903));

            return new google.maps.LatLng(lat, long);
        },
        findMapPins: function ($canvas, map) {
            var pins = [];
            $canvas.find(".mapwidget-pin").each(function (i, el) {
                var newPin,
                    newInfo,
                    $el = $(el);
                
                newInfo = new google.maps.InfoWindow({content: $el.html()});
                
                newPin = new google.maps.Marker({
                    position: mapWidget.parseCenter($el),
                    map: map,
                    title: 'HALLO'
                });
                
                if ($el.hasClass("mapwidget-pin-alt")) {
                    newPin.setIcon("http://maps.google.com/mapfiles/ms/icons/green-dot.png");
                }
                
                google.maps.event.addListener(newPin, 'click', function () {
                    newInfo.open(map, newPin);
                });
            });
        },
        maps: [],
        startMap: function (el, options) {
            map = new google.maps.Map(el, options);
            mapWidget.maps.push(map);
            return map;
        }
    };

    google.maps.event.addDomListener(window, 'load', mapWidget.initialize);
    
    window.mapWidget = mapWidget;
})();