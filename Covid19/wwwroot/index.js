let marker;
let map;

// Initialize and add the map
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
    //pop up window marker
    const contentString = "<b>" + country + "</b>" +"</br>Number of " + state + " on " + date + " is <b>" + number + "</b>";
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

function onSubmitLeastOrMostSpecificDate(event) {
    event.preventDefault();
    const url = "/api/CountriesSickOrDeathsThisDay/?sickOrDeath=" + event.target.elements.state.value + "&dateReported=" + event.target.elements.fromDate.value;
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            const country = data[0].country;
            const number = data[0].newCases;
            var latLong = getLatLong(country);
            addMarker(latLong, country, number, event.target.elements.state.value, event.target.elements.fromDate.value);

        },
        error: function (request) {
            alert("error");
        }
    });
};

function getLatLong(country) {
    const url = "/api/LngLtdOfCountry?country=" + country;
    var request = new XMLHttpRequest();
    request.open('GET', url, false);  // `false` makes the request synchronous
    request.send(null);
    let lat = request.responseText.split('latitude":')[1].split(",")[0];
    let long = request.responseText.split('longitude":')[1].split(",")[0].split("}")[0];
    return new google.maps.LatLng(parseFloat(lat), parseFloat(long));
    //return MyLatLong;
    //alert(request.responseText);
    //$.ajax({
    //    url: url,
    //    type: "GET",
    //    success: function (data) {
    //        const myLatLng = new google.maps.LatLng(data[0].latitude, data[0].longitude);
    //        return myLatLong;
    //    },
    //    error: function (request) {
    //        alert("error");
    //    }
    //});
}

function onSubmitSpecificCountrySpecificDateDeathOrSick(event) {
    alert("sending query");
    const url = "/api/CountryDatas?country=" + event.target.elements.country.value + "?fromDate" + event.target.elements.fromDate.value +
        "?toDate" + event.target.elements.toDate.value + "?state" + event.target.elements.state.value;
    alert(url);

    $.ajax({
        url: url,
        success:
            alert("success"),
        function(countriesData) {
            alert("success");
            alert(countriesData);
            //delete all markers in the map
            for (i = 0; i < allMarker.length; i++) {
                allMarker[i].setMap(null);
            }
            allMarker = [];

            let newCountriesNames = [];

            // insert new country to tables
            for (const countryData of countriesData) {
                if (countryData === null) {
                    continue;
                }

                newCountries.push(countryData.name);
                // draw route
                const myLatLng = new google.maps.LatLng(countryData.latitude, countryData.longitude);
                addMarker(myLatLng, countryData);
            }

            // remove deleted country from tables
            for (const countryName of countryName) {
                if (!newCountriesNames.includes(countryName)) {
                    //remove marker on the map
                    for (let i = 0; i < allMarker.length; i++) {
                        if (allMarker[i].get('store_id') === countryName) {
                            allMarker[i].setMap(null);
                            //remove from array
                            allMarker.splice(i, 1);
                            //remove from array color
                            const index = array.indexOf(countryName);
                            markersColor.splice(index, 1);
                        }
                    }
                }
            }

            countryName = newCountriesNames;
        },
        error: function (request) {
            alert("error");
        }
    });
}