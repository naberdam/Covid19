let marker;
let map;

//// Initialize and add the map
function initMap() {
    // The location of Uluru
    const uluru = { lat: -25.344, lng: 131.036 };
    // The map, centered at Uluru
    map = new google.maps.Map(document.getElementById("map"), {
        zoom: 4,
        center: uluru,
    });
    // The marker, positioned at Uluru
    marker = new google.maps.Marker({
        position: uluru,
        map: map,
    });
    //google.maps.event.addListener(map, 'click', function (event) {
    //    alert('Lat: ' + event.latLng.lat() + ' and Longitude is: ' + event.latLng.lng());
    //});
}

function onSubmitNevigate(event) {
    window.location.href = "complexQueries.html";
}


function addMarker(MyLatLong, country, number, state, date) {
    //let contentString;
    let infoWindow = new google.maps.InfoWindow();
    let contentString;
    //pop up window marker
    if (typeof(date) != "undefined") {
        contentString = "<b>" + country + "</b>" + "</br>Number of " + state + " on " + date + " is <b>" + number + "</b>";
    } else {
        contentString = "<b>" + country + "</b>" + "</br>Number of " + state + " until today is <b>" + number + "</b>";
    }
   
    //if (markersColor.includes(country)) {
    //    marker = new google.maps.Marker({
    //        map: map,
    //        position: myLatLng,
    //        store_id: country,
    //        icon: {
    //            url: "https://www.google.com/mapfiles/marker_green.png"
    //        }
    //    });
    //    infoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + contentString + "</div>");
    //    infoWindow.open(map, marker);
    //}
    //if the marker click
   // else {
    //marker = new google.maps.Marker({
    //    map: map,
    //    position: MyLatLong,
    //    store_id: country,
    //    icon: {
    //        url: "https://www.google.com/mapfiles/marker_green.png"
    //    }
    //});
    infoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + contentString + "</div>");
    infoWindow.open(map, marker);
    marker.setPosition(MyLatLong);
    map.setCenter(MyLatLong);
    //allMarker.push(marker);

    (function (marker, country) {
        google.maps.event.addListener(marker, "click", function (e) {
            if (infoWindow) infoWindow.close();
            //delete all route 
            for (let [key, value] of flightPaths.entries()) {
                value.setMap(null);
            }
            //reset all the markers and there color
            for (i = 0; i < allMarker.length; i++) {
                allMarker[i].setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');

            }
            markersColor = [];
            //Change the marker icon
            marker.setIcon('https://www.google.com/mapfiles/marker_green.png');
            markersColor.push(country);
            //Wrap the content inside an HTML DIV in order to set height and width of InfoWindow.
            infoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + contentString + "</div>");
            infoWindow.open(map, marker);
            updateFlightInfo(country)
            polyline(country, map, country)
            flightBoldMap(country)
        });
        //Attach click event to the map.
        google.maps.event.addDomListenerOnce(map, "click", function (e) {
            // deleting bolded row?
            if (flightIsBold(country)) {
                flightUnbold(country)
            }
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/red-dot.png');
            markersColor.pop(country);
            if (infoWindow) infoWindow.close();
            emptyFlightInfo()
            for (let [key, value] of flightPaths.entries()) {
                if (key === marker.get('store_id')) {
                    value.setMap(null);
                }
            }
        });
    })(marker, country);
}

function onSubmitSpecificDate(event) {
    event.preventDefault();
    date = event.target.elements.dateUpQuery.value;
    const url = "/api/CountriesSickOrDeathsThisDay?sickOrDeath=" + event.target.elements.state.value + "&dateReported=" + date;
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            let number = data[0].newCases;
            let country = data[0].country
            var latLong = getLatLong(country);
            addMarker(latLong, country, number, event.target.elements.state.value,date);

        },
        error: function (request) {
            alert("error");
        }
    });
}

function getLatLong(country) {
    if (country == "all over the world") {
        country = "Israel";
    }
    const url = "/api/LngLtdOfCountry?country=" + country;
    var request = new XMLHttpRequest();
    request.open('GET', url, false);  // `false` makes the request synchronous
    request.send(null);
    let lat = request.responseText.split('latitude":')[1].split(",")[0];
    let long = request.responseText.split('longitude":')[1].split(",")[0].split("}")[0];
    return new google.maps.LatLng(parseFloat(lat), parseFloat(long));
}

function onChangeCountry(event) {
    if (event.target.value == 'allOver') {
        document.getElementById('untilOrThisDate').style.display = 'none';
        document.getElementById('untilTodayLabel').style.display = 'block';
        document.getElementById('dateDownQuery').style.display = 'none';
    } else {
        document.getElementById('dateDownQuery').style.display = 'block';
        document.getElementById('untilOrThisDate').style.display = 'block';
        document.getElementById('untilTodayLabel').style.display = 'none';
    }
}

function onSubmitSpecificCountrySpecificDateDeathOrSick(event) {
    event.preventDefault();
    untilOrThisDate = event.target.elements.untilOrThisDate.value;
    let date;
    let url;
    let country = event.target.elements.country.value;
    if (country == "allOver") {
        url = "/api/OneIntVariable/?deathsOrSick=" + event.target.elements.state.value;
        country = 'all over the world';
       
    } else {
        date = event.target.elements.dateDownQuery.value;
        let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
        url = "/api/OneIntVariable/?deathsOrSick=" + event.target.elements.untilOrThisDate.value + event.target.elements.state.value + "&date=" + convertedDate + "&country=" + country;
    }
    
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            let number = data[0].sum;
            var latLong = getLatLong(country);
            addMarker(latLong, country, number, event.target.elements.state.value, date);
            $("#contact100-form validate-form")[0].reset();
        },
        error: function (request) {
            alert("Sorry, we don't have this data");
        }
    });
}