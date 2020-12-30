let marker;
let map;
let infos;
let sickOrDeath = '';

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


function addMarker(MyLatLong, country, number, state, date, string = 'on') {
    //let contentString;
    let infoWindow = new google.maps.InfoWindow();
    let contentString;
    //pop up window marker
    if (typeof(date) != "undefined") {
        contentString = "<h6>" + country + "</h6>" + "</br> <p> Number of " + state + string + date + " is </p> <h6>" + number + "</h6>";
    } else {
        contentString = "<h6>" + country + "</h6>" + "</br> <p> Number of " + state + " until today is </p>  <h6>" + number + "</h6>";
    }
    if (typeof(infos) != 'undefined') {
        infos.close();
    }
    infoWindow.setContent("<div style = 'width:200px;min-height:40px'>" + contentString + "</div>");
    infoWindow.open(map, marker);
    infos = infoWindow;
    marker.setPosition(MyLatLong);
    map.setCenter(MyLatLong);
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
            alert("sorry, we don't have this data");
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
        document.getElementById('Cumulative').style.display = 'none';
        document.getElementById('Cumulative').label = 'choose again!';
        document.getElementById('ThisDay').style.display = 'none';
        document.getElementById('ThisDay').label = 'choose again!';
        document.getElementById('untilToday').style.display = 'block';
        document.getElementById('untilToday').label = 'until today';
        document.getElementById('dateDownQuery').style.display = 'none';
        document.getElementById('Avg').style.display = 'none';
        document.getElementById('Avg').label = 'choose again!';
        document.getElementById('Div').style.display = 'none';
        document.getElementById('Div').label = 'choose again!';
    } else {
        document.getElementById('ThisDay').label = 'on date';
        document.getElementById('Cumulative').label = 'until date';
        document.getElementById('Cumulative').style.display = 'block';
        document.getElementById('ThisDay').style.display = 'block';
        document.getElementById('untilToday').label = 'choose again!';
        document.getElementById('untilToday').style.display = 'none';
        document.getElementById('dateDownQuery').style.display = 'block';
        if (sickOrDeath == 'sick') {
            document.getElementById('Avg').style.display = 'block';
            document.getElementById('Avg').label = 'each day in avg on the week of';
            document.getElementById('Div').style.display = 'block';
            document.getElementById('Div').label = 'each day relative to last week in avg on the week of';
        }

    }
}

function onChangeDate(event) {
    if (event.target.value != 'untilToday') {
        document.getElementById('dateDownQuery').style.display = 'block';
    } else {
        document.getElementById('dateDownQuery').style.display = 'none';
    }
}

function onChangeState(event) {
    if (event.target.value == 'Sick') {
        sickOrDeath = 'sick';
        document.getElementById('Avg').style.display = 'block';
        document.getElementById('Avg').label = 'each day in avg on the week of';
        document.getElementById('Div').style.display = 'block';
        document.getElementById('Div').label = 'each day relative to last week in avg on the week of';
        document.getElementById('dateDownQuery').style.display = 'none';
        //document.getElementById('dateDownQuery').style.display = 'block';
       
    } else { // deaths
        sickOrDeath = 'death';
        document.getElementById('Avg').style.display = 'none';
        document.getElementById('Avg').label = 'choose again!';
        document.getElementById('dateDownQuery').style.display = 'none';
        //document.getElementById('dateDownQuery').style.display = 'none';
        document.getElementById('Div').style.display = 'none';
        document.getElementById('Div').label = 'choose again!';
    }
}

function onSubmitSpecificCountrySpecificDateDeathOrSick(event) {
    event.preventDefault();
    untilOrThisDate = event.target.elements.untilOrThisDate.value;
    let date;
    let url;
    let country = event.target.elements.country.value;
    if (untilOrThisDate == 'Avg' || untilOrThisDate == 'Div') {
        date = event.target.elements.dateDownQuery.value;
        let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
        url = "/api/SpecificCountryAndDateAvgSick/?divOrAvg=" + untilOrThisDate + "&country=" + country + "&date=" + convertedDate;
    }

    else if (country == "allOver") {
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
            let number;
            let stringToSend;
            if ('sum' in data[0]) {
                number = data[0].sum;
                stringToSend = ' on '
            } else if ('avgSick' in data[0]) {
                number = data[0].avgSick;
                stringToSend = ' in average in each day in the week of '
            }
            var latLong = getLatLong(country);
            addMarker(latLong, country, number, event.target.elements.state.value, date, stringToSend);
            $("#contact100-form validate-form")[0].reset();
        },
        error: function (request) {
            alert("Sorry, we don't have this data");
        }
    });
}