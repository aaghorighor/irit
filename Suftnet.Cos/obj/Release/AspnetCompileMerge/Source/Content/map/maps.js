var mapStyles = [{"featureType":"road","elementType":"geometry","stylers":[{"lightness":100},{"visibility":"simplified"}]},{"featureType":"water","elementType":"geometry","stylers":[{"visibility":"on"},{"color":"#C6E2FF"}]},{"featureType":"poi","elementType":"geometry.fill","stylers":[{"color":"#C5E3BF"}]},{"featureType":"road","elementType":"geometry.fill","stylers":[{"color":"#D1D1B8"}]}];

$(document).ready(function($) {
    "use strict";

 //  Map in item view --------------------------------------------------------------------------------------------------

    $(".item .mark-circle.map").on("click", function(){
        var _latitude = $(this).closest(".item").attr("data-map-latitude");
        var _longitude = $(this).closest(".item").attr("data-map-longitude");
        var _id =  $(this).closest(".item").attr("data-id");
        $(this).closest(".item").find(".map-wrapper").attr("id", "map"+_id);
        var _this = "map"+_id;
        simpleMap(_latitude,_longitude, _this);
        $(this).closest(".item").addClass("show-map");
        $(this).closest(".item").find(".btn-close").on("click", function(){
            $(this).closest(".item").removeClass("show-map");
        });
    });

});

// Simple map ----------------------------------------------------------------------------------------------------------

function simpleMap(_latitude,_longitude, element, markerDrag, makerIcon){
    if (!markerDrag){
        markerDrag = false;
    }
    var mapCenter = new google.maps.LatLng(_latitude,_longitude);
    var mapOptions = {
        zoom: 9,
        center: mapCenter,
        disableDefaultUI: true,
        scrollwheel: true,
        styles: mapStyles
    };
    var mapElement = document.getElementById(element);
    var map = new google.maps.Map(mapElement, mapOptions);

    var marker = new MarkerWithLabel({
        position: new google.maps.LatLng( _latitude,_longitude ),
        map: map,
        icon: makerIcon,
        labelAnchor: new google.maps.Point(50, 0),
        draggable: markerDrag
    });

    google.maps.event.addListener(marker, "mouseup", function (event) {
        var latitude = this.position.lat();
        var longitude = this.position.lng();
        $('#latitude').val( this.position.lat() );
        $('#longitude').val( this.position.lng() );
    });

    autoComplete(map, marker);  
}

//Autocomplete ---------------------------------------------------------------------------------------------------------

function autoComplete(map, marker)
{
    if ($("#CompleteAddress").length) {
        var input = /** @type {HTMLInputElement} */(document.getElementById('CompleteAddress'));
        var autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.bindTo('bounds', map);
        google.maps.event.addListener(autocomplete, 'place_changed', function() {
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

            if (marker)
            {              
                marker.setPosition(place.geometry.location);
                marker.setVisible(true);

                $('#Latitude').val(marker.getPosition().lat());
                $('#Longitude').val(marker.getPosition().lng());
            }
            var address = '';
            if (place.address_components)
            {               
                address = place.formatted_address;                              
                                           
                for (var i = 0; i < place.address_components.length; i++) {
                    var component = place.address_components[i];
                    var addressType = component.types[0];

                    switch (addressType) {
                        case 'street_number':
                            var streetNumber = component.long_name;                         
                            break;
                        case 'route':
                            var route = component.long_name;                       
                            break;
                        case 'postal_town':
                            $('#Town').val(component.long_name);
                            break;
                        case 'administrative_area_level_2':
                            $('#County').val(component.long_name);
                            break;
                        case 'postal_code':                        
                            $('#PostCode').val(component.long_name);
                            break;
                        case 'country':                        
                            $('#Country').val(component.long_name);
                            break;
                    }
                }

            }
         
        });

        function success(position) {
                       
            map.setCenter(new google.maps.LatLng(position.coords.latitude, position.coords.longitude));
            //initSubmitMap(position.coords.latitude, position.coords.longitude);
            //$('#Latitude').val(position.coords.latitude);
            //$('#Longitude').val(position.coords.longitude);

            //console.log(position.coords);
        }

        $('.geo-location').on("click", function() {
            if (navigator.geolocation) {
                $('#'+element).addClass('fade-map');
                navigator.geolocation.getCurrentPosition(success);
            } else {
                console.log('Geo Location is not supported');
            }
        });
    }
}


