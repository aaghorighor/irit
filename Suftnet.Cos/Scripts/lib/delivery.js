
var source, destination;
var locations = [];
var directionsDisplay;
var directionsService = new google.maps.DirectionsService();
var map;

$("#btnSubmitAddress").click(function ()
{  
    if ($('#DeliveryAddress').val() == '')
    {
        showError("Error, Delivery Address field is required", 5000);

        return false;
    }

    GetRoute();
});


setTimeout(function () {

    // initialise the location of the map on Chichester in England (ref lat and lng)
     map = new google.maps.Map(document.getElementById('deliveryMap'), {
        center: { lat: 50.834697, lng: -0.773792 },
        zoom: 13,
        mapTypeId: 'roadmap'
    });

    google.maps.event.addDomListener(window, 'load', function () {
        new google.maps.places.SearchBox(document.getElementById('DeliveryAddress'));
        new google.maps.places.SearchBox(document.getElementById('addressfrom'));
        directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
    });

}, 0);


var distanceService = {
       
    constant: 
    {
        kilometer: 2663,
        miles: 2664,
        pKilometer:2666,
        pMiles :2665
    },
    milesm : function(i)
    {
        return i * 1609.344;
    },
    metersm : function(i)
    {
        return i * 0.000621371192;
    },
    milesk : function(i)
    {
        return (i * 1.6093).toFixed(2);
    },
    kilometerm: function (i)
    {
        return (i * 0.621371).toFixed(2);
    }

}

function GetRoute() {

    directionsDisplay.setMap(map);

    source = document.getElementById("addressfrom").value;
    destination = document.getElementById("DeliveryAddress").value;

    var request = {
        origin: source,
        destination: destination,
        //optimizeWaypoints: true, //set to true if you want google to determine the shortest route or false to use the order specified.
        travelMode: google.maps.TravelMode.DRIVING
    };

    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsDisplay.setDirections(response);
        }
    });

    //*********DISTANCE AND DURATION**********************//
    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix({
        origins: [source],
        destinations: [destination],
        travelMode: google.maps.TravelMode.DRIVING,
        unitSystem: google.maps.UnitSystem.METRIC,
        avoidHighways: false,
        avoidTolls: false
    }, function (response, status) {

        if (status == google.maps.DistanceMatrixStatus.OK && response.rows[0].elements[0].status != "ZERO_RESULTS" && response.rows[0].elements[0].status != "NOT_FOUND")
        {           
            var distance = response.rows[0].elements[0].distance.text;
            var duration = response.rows[0].elements[0].duration.value;
            var deliveryRate = document.getElementById("deliveryRate").value;
          
            duration = parseFloat(duration / 60).toFixed(2);           

            var lasttwoChars = distance.substr(-2);            
           
            var miles = distanceService.metersm(response.rows[0].elements[0].distance.value);

            if (distanceService.constant.kilometer == document.getElementById("deliveryUnit").value)
            {
                distance = distanceService.milesk(miles);
                lasttwoChars = "km";     

            } else if (distanceService.constant.miles == document.getElementById("deliveryUnit").value)
            {
                distance = miles;
                lasttwoChars = "m";
            }
           
            var totalCost = parseFloat(distance) * parseFloat(deliveryRate);

            document.getElementById("DeliveryDuration").value = response.rows[0].elements[0].duration.text;
            document.getElementById("DeliveryDistance").value = parseFloat(distance).toFixed(2) + " " + lasttwoChars;
            document.getElementById("DeliveryCost").value = totalCost.toFixed(2);

        } else {
            alert("Unable to find the distance via road.");
        }
    });   
};