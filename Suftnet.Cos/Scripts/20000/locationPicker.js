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
                
                var originalDistance = 0;
                var expectedDistance = 0;
                var duration = 0.00;
                var lat = 0;
                var lng = 0;
                var totalCost = 0.00
                var startAddress = "";
                var endAddress = "";
                var unit = "";
                var costNote = "Delivery cost is calculated using Flat Rate";
                var isFlatRate = document.getElementById("isFlatRate").value;
                var deliveryRate = document.getElementById("deliveryRate").value;
                var deliveryUnit = document.getElementById("deliveryUnit").value;
                var flatRate = document.getElementById("flatRate").value;

                response.routes[0].legs.forEach(function (item, index)
                {
                    if (index < response.routes[0].legs.length - 1)
                    {
                        originalDistance = originalDistance + parseInt(item.distance.text);
                        duration = parseFloat(duration) + parseFloat(item.duration.value / 60);

                        startAddress = source;
                        endAddress = item.end_address;

                        lat = response.routes[0].legs[response.routes[0].legs.length - 1].end_location.lat();
                        lng = response.routes[0].legs[response.routes[0].legs.length - 1].end_location.lng();  

                        if (!isNaN(flatRate)) {
                            totalCost = parseFloat(flatRate);
                        }    

                        var miles = locationPicker.conveter.meterToMiles(originalDistance);

                        if (constants.matrix.kilomater == deliveryUnit) {
                            expectedDistance = locationPicker.conveter.milesToKilometer(miles);
                            unit = "Kilometer";
                        } else if (constants.matrix.miles == deliveryUnit) {
                            expectedDistance = miles;
                            unit = "Miles";
                        }                              

                        $("#edistance").val(expectedDistance.toFixed(2));
                        $("#eduration").val(duration.toFixed(2));
                        $("#Distance").val(expectedDistance.toFixed(2));
                        $("#Duration").val(duration.toFixed(2));
                        $("#Latitude").val(lat);
                        $("#Logitude").val(lng);
                        $("#AddressLine").val(endAddress);   
                        $("#unit").text(unit);
                        $("#costNote").text(costNote);
                        $("#DeliveryCost").val(totalCost);                        
                        $("#cost").val(totalCost.toFixed(2));                     
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
    }, 
    conveter: {
        kilometerToMiles: function (i) {

            if (i == null) {
                return 0;
            };

            return (i * 0.621371).toFixed(2);
        },
        milesToKilometer: function (i) {

            if (i == null) {
                return 0;
            };

            return (i * 1.6093).toFixed(2);
        },
        meterToMiles: function (i) {

            if (i == null) {
                return 0;
            };

            return i * 0.000621371192;
        },
        milesToMeter: function (i) {

            if (i == null) {
                return 0;
            };

            return i * 1609.344;
        }
        
    }
}


//if (isFlatRate === "false")
                        //{                          
                        //    costNote = "Delivery cost is calculated using distance per " + unit;
                        //    totalCost = parseFloat(expectedDistance) * parseFloat(deliveryRate);
                        //}  