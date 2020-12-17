
function onsubmitComplex(event) {
    event.preventDefault();
    let date = event.target.elements.date.value;
    let descOrAsc = event.target.elements.descOrAsc.value;
    let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
    if (event.target.elements.orderBy.value == 'Gdp') {
        orderByGdp(convertedDate, descOrAsc)
    }
    else if (event.target.elements.orderBy.value == 'density') {
        orderByDensity(convertedDate, descOrAsc, event.target.elements.numberOfYears.value)
    }
    else if (event.target.elements.orderBy.value == 'Sick') {
        if (event.target.elements.count.value == 'perMillion') {
            perMillionOrderBySick(convertedDate, descOrAsc)
        }
    }
    //if (event.target.elements.count.value == 'perMillion') {
    //    if (event.target.elements.orderBy.value != 'density') {
    //        perMillionOrderBySickGdpDeath(convertedDate, event.target.elements.orderBy.value, descOrAsc);
    //    } else {
    //        perMillionOrderByDensity(convertedDate, descOrAsc, event.target.elements.numberOfYears.value)
    //    }
    //}
    
}


//perMillionOrderByDensity(date, descOrAsc, event.target.elements.numberOfYears.value) {
//    let url;
//    if (descOrAsc == 'desc') {
//        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date + "&desc=true";
//    } else {
//        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date;
//    }
//    $.ajax({
//        url: url,
//        type: "GET",
//        success: function (data) {
//            deleteTable();
//            let table = document.querySelector("table");
//            generateTableHead(table, Object.keys(data[0]));
//            generateTable(table, data);
//        },
//        error: function () {
//            alert("error");
//        }
//    });
//}


function perMillionOrderBySickGdpDeath(date, orderBy, descOrAsc) {
    let url;
    if (descOrAsc == 'desc') {
        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date;
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}

function perMillionOrderBySick(date, descOrAsc) {
    // check about date!!
    let url;
    if (descOrAsc == 'desc') {
        url = "/api/CountriesDeathsAndSickPreMillionBySick?desc=true";
    } else {
        url = "/api/CountriesDeathsAndSickPreMillionBySick";
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}


function orderByDensity(date, descOrAsc, numberOfYears) {
    let url;
    if (descOrAsc == 'desc') {
        url = "/api/CountrySickDeathsAndGdpByGdp?date=" + date + "&desc=true";
    } else {
        url = "/api/CountrySickDeathsAndGdpByGdp?date=" + date;
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}


function orderByGdp(date, descOrAsc) {
    let url;
    if (descOrAsc == 'desc') {
        url = "/api/CountrySickDeathsAndGdpByGdp?date=" + date + "&desc=true";
    } else {
        url = "/api/CountrySickDeathsAndGdpByGdp?date=" + date;
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}

function onSubmitNevigate(event) {
    window.location.href = "index.html";
}

function checkOrderBy(orderBy) {
    if (orderBy == 'density') {
        document.getElementById('numberOfYears').style.display = 'block';
        document.getElementById('labelForDensity').style.display = 'block';
    } else {
        document.getElementById('numberOfYears').style.display = 'none';
        document.getElementById('labelForDensity').style.display = 'none';
    }
}

function deleteTable() {
    var Table = document.getElementById("results");
    Table.innerHTML = "";
}

function generateTableHead(table, data) {
    let thead = table.createTHead();
    let row = thead.insertRow();
    for (let key of data) {
        let th = document.createElement("th");
        let text = document.createTextNode(key);
        th.appendChild(text);
        row.appendChild(th);
    }
}

function generateTable(table, data) {
    for (let element of data) {
        let row = table.insertRow();
        for (key in element) {
            let cell = row.insertCell();
            let text = document.createTextNode(element[key]);
            cell.appendChild(text);
        }
    }
}
