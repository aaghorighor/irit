jQuery(document).ready(function ($) {

    if ($("#Term").length) {
        var input = /** @type {HTMLInputElement} */(document.getElementById('Term'));
        var autocomplete = new google.maps.places.Autocomplete(input);
        //autocomplete.bindTo('bounds', map);
        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            var place = autocomplete.getPlace();
            if (!place.geometry) {
                return;
            }
            if (place.geometry.viewport) {
                map.fitBounds(place.geometry.viewport);
            } else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);
            }

            if (marker) {
                marker.setPosition(place.geometry.location);
                marker.setVisible(true);

                $('#Latitude').val(marker.getPosition().lat());
                $('#Longitude').val(marker.getPosition().lng());

            }
            var address = '';
            if (place.address_components) {
                address = [
                    (place.address_components[0] && place.address_components[0].long_name || ''),
                    (place.address_components[1] && place.address_components[1].long_name || ''),
                    //(place.address_components[2] && place.address_components[2].long_name || ''),
                    (place.address_components[3] && place.address_components[3].long_name || ''),
                    (place.address_components[4] && place.address_components[4].long_name || ''),
                    (place.address_components[5] && place.address_components[5].long_name || '')
                ].join(' ');
            }
        });

        function success(position) {

            map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
            //initSubmitMap(position.coords.latitude, position.coords.longitude);
            $('#Latitude').val(position.coords.latitude);
            $('#Longitude').val(position.coords.longitude);
        }

        $('.geo-location').on("click", function () {
            if (navigator.geolocation) {
                $('#' + element).addClass('fade-map');
                navigator.geolocation.getCurrentPosition(success);
            } else {
                console.log('Geo Location is not supported');
            }
        });
    }

});

