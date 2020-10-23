var source, destination;
var locations = [];
var directionsDisplay;
var directionsService = new google.maps.DirectionsService();

var locationPicker = {

    pageInit: function () {
       
        google.maps.event.addDomListener(window, 'load', function () {
            new google.maps.places.SearchBox(document.getElementById('pickUp'));
            new google.maps.places.SearchBox(document.getElementById('dropOff'));
            directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
        });

        locationPicker.listener();
        locationPicker.map();
    },

    map: function () {

        var map = new google.maps.Map(document.getElementById('dvMap'), {
            center: { lat: 50.834697, lng: -0.773792 },
            zoom: 13,
            mapTypeId: 'roadmap'

        });

        return map;
    },

    setDestination: function () {
        destination = document.getElementById("dropOff").value;
        locations.push(destination);       
    },   

    getRoute: function () {

        directionsDisplay.setMap(locationPicker.map());

        source = document.getElementById("pickUp").value;
        destination = document.getElementById("dropOff").value;

        var waypoints = [];
        for (var i = 0; i < locations.length; i++) {
            var address = locations[i];
            if (address !== "") {
                waypoints.push({
                    location: address,
                    stopover: true
                });
            }
        }

        var request = {
            origin: source,
            destination: waypoints[0].location,
            waypoints: waypoints, //an array of waypoints
            optimizeWaypoints: true, //set to true if you want google to determine the shortest route or false to use the order specified.
            travelMode: google.maps.DirectionsTravelMode.DRIVING
        };

        directionsService.route(request, function (response, status) {
            if (status == google.maps.DirectionsStatus.OK) {
                
                var distance = 0;
                var minute = 0.00;
                var lat = 0;
                var lng = 0;
                var startAddress = ""
                var endAddress = ""

                response.routes[0].legs.forEach(function (item, index) {
                    if (index < response.routes[0].legs.length - 1) {
                        distance = distance + parseInt(item.distance.text);
                        minute = parseFloat(minute) + parseFloat(item.duration.value / 60);

                        startAddress = source;
                        endAddress = item.end_address;

                        lat = response.routes[0].legs[response.routes[0].legs.length - 1].end_location.lat();
                        lng = response.routes[0].legs[response.routes[0].legs.length - 1].end_location.lng();  

                        $("#edistance").val(distance);
                        $("#eduration").val(minute.toFixed(2));
                        $("#Distance").val(distance);
                        $("#Duration").val(minute.toFixed(2));
                        $("#Latitude").val(lat);
                        $("#Logitude").val(lng);
                        $("#AddressLine").val(endAddress);   
                     
                    }
                });

                directionsDisplay.setDirections(response);
            }
            else {
                //handle error
            }
        });
    }
    ,
    listener: function () {

        $(document).on("click", "#getRoute", function () {
            locationPicker.setDestination();
            locationPicker.getRoute();          
        });  

        $(document).on("click", "#btnContinue", function () {
            suftnet.tab(1);
        }); 

    }    
}