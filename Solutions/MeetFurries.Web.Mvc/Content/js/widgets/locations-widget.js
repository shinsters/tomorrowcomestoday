(function () {
    var locations = {
        index: 0,
        init: function () {
            $("#locations #addLocation").on("click", function () {
                locations.addLocation();
            });
            
            $("#locations .list-group-item.clearfix").each(function (i, el) {
                locations.startLocation(el);
            });
            
            $("#locations").on("click", ".btn-danger", function (event) {
                var $el = $(event.target);
                console.log($el.parents(".list-group-item"));
                $el.parents(".list-group-item").first().remove();
            });
            
            $("#locations .form-group").on("keypress keydown keyup", "[name=\"search\"]", function (event) {
                if (event.keyCode === 13) {
                    event.preventDefault();
                    return false;
                }
            });
        },
        resetLocation: function (el) {
            var $el = $(el);
        
            $el.find("input, textarea").not("[type='checkbox']").val("");
            $el.find("input[type='checkbox']").prop("checked", false);
        },
        startLocation: function (el) {
            var $el = $(el),
                options = {},
                initialLat,
                initialLong,
                map,
                marker,
                $locationLat = $el.find("input[name=\"locationLat[]\"]"),
                $locationLong = $el.find("input[name=\"locationLong[]\"]"),
                $search = $el.find("input[name=\"search\"]"),
                $locationName = $el.find("input[name=\"locationName[]\"]"),
                locationName = $locationName.val(),
                places,
                center;

            initialLat = ($locationLat.val() ? $locationLat.val() : 53.801576);
            initialLong = ($locationLong.val() ? $locationLong.val() : -1.546903);

            center = options.center = new google.maps.LatLng(initialLat, initialLong);

            options.zoom = $el.data("mapwidget-zoom") ? $el.data("mapwidget-zoom") : 8;

            options.mapTypeId = google.maps.MapTypeId.ROADMAP;

            map = new google.maps.Map($(el).find(".map-canvas")[0], options);

            marker = new google.maps.Marker({
                position: options.center,
                map: map,
                title: '',
                draggable: true
            });

            places = new google.maps.places.PlacesService(map);

            google.maps.event.addListener(marker, 'dragend', function(a) {
                $locationLat.val(a.latLng.lat().toFixed(8));
                $locationLong.val(a.latLng.lng().toFixed(8));
            });

            var timeout = null;

            $search.on("keyup", function () {
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    places.textSearch({query: $search.val(), location: center, radius: 10000}, function (results) {
                        console.log(results);
                        /**/
                        center = results[0].geometry.location;

                        if ($locationName.val() === locationName) {
                            var newName = results[0].name;
                            $locationName.val(newName);
                            locationName = newName;
                        }

                        map.setCenter(center);
                        map.setZoom(14);
                        marker.setPosition(center);
                        /**/
                    })
                }, 500);
            });
        },
        addLocation: function () {
            var last = $("#locations .list-group-item.clearfix").last(),
                    clone = last.clone();
            
            clone.find("input, textarea").each(function (i, el) {
                var $el = $(el);
                
                $el.attr("name", $el.attr("name").replace(locationsWidget.index, ++locationsWidget.index));
            });
                    
            clone.insertAfter(last);
            
            locations.resetLocation(clone);
            locations.startLocation(clone);
        }
    };
    
    window.locationsWidget = locations;
    
    locations.init();
})();