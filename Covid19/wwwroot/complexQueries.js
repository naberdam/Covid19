
function onsubmitComplex(event) {
    event.preventDefault();
    let date = event.target.elements.date.value;
    //let descOrAsc = event.target.elements.descOrAsc.value;
    let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
    let orderBy = event.target.elements.orderBy.value;
    let column = event.target.elements.column.value;
    let descOrAsc = event.target.elements.descOrAsc.value;
    if (event.target.elements.count.value == 'PerMillion') {
        if (column == 'PopulationGrowth') {
            popGrowthPerMillion(convertedDate, descOrAsc, event.target.elements.numberOfYears.value, orderBy);
        }else if (column == 'Density') {
            densityPerMillion(convertedDate, descOrAsc);
        } else if (column == 'Gdp') {
            gdpPerMillion(convertedDate, descOrAsc, orderBy);
        }
    } else if (event.target.elements.count.value == 'TotalNumber') {
        if (column == 'PopulationGrowth') {
            popGrowthTotal(convertedDate, descOrAsc, event.target.elements.numberOfYears.value);
        } else if (column == 'Density') {
            densityTotal(convertedDate, descOrAsc);
        } else if (column == 'Gdp') {
            gdpTotal(convertedDate, descOrAsc, orderBy);
        }
    }  
}

function gdpTotal(date, descOrAsc, orderBy) { //working
    // from CountrySickDeathsAndGdpByGdpController

    if (descOrAsc == 'desc') {
        url = "/api/CountrySickDeathsAndGdpByGdp?gdpSickOrDeaths=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountrySickDeathsAndGdpByGdp?gdpSickOrDeaths=" + orderBy + "&date=" + date + "&desc=false";
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


function gdpPerMillion(date, descOrAsc, orderBy) { // working
    // from CountryDeathsSickPerMillionWithGdpController

    if (descOrAsc == 'desc') {
        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountryDeathsSickPerMillionWithGdp?deathsGdpSick=" + orderBy + "&date=" + date + "&desc=false";
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            //sorttable.makeSortable(table)
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}

function popGrowthPerMillion(date, descOrAsc, numYears, orderBy) { // working
    // from CountrySickAndDeathsOrDensityController

    if (descOrAsc == 'desc') {
        url = "/api/CountrySickAndDeathsOrDensity?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears+ "&desc=true";
    } else {
        url = "/api/CountrySickAndDeathsOrDensity?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears + "&desc=false";
    }
    $.ajax({
        url: url,
        type: "GET",
        success: function (data) {
            deleteTable();
            let table = document.querySelector("table");
            //sorttable.makeSortable(table)
            generateTableHead(table, Object.keys(data[0]));
            generateTable(table, data);
        },
        error: function () {
            alert("error");
        }
    });
}

function popGrowthTotal(date, descOrAsc, numberOfYears) {
    // from CountriesDeathsVsDensity2020Controller

    if (descOrAsc == 'desc') {
        url = "/api/CountriesDeathsVsDensity2020?densityOrDeath=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountriesDeathsVsDensity2020?densityOrDeath=" + orderBy + "&date=" + date + "&desc=false";
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

function densityTotal(date, descOrAsc, orderBy) {
    // from CountriesDeathsVsDensity2020Controller

    if (descOrAsc == 'desc') {
        url = "/api/CountrySickDeathsAndGdpByGdp?gdpSickOrDeaths=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountrySickDeathsAndGdpByGdp?gdpSickOrDeaths=" + orderBy + "&date=" + date + "&desc=false";
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

function densityPerMillion(date, descOrAsc, orderBy) {
    // from CountrySickAndDeathsOrDensityController

    if (descOrAsc == 'desc') {
        url = "/api/api/CountrySickAndDeathsOrDensity" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/api/CountrySickAndDeathsOrDensity" + orderBy + "&date=" + date;
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

function checkColumn(newCol) {
    if (newCol == 'Density') {
        document.getElementById('DensityOrder').style.display = 'block';
        document.getElementById('PopulationGrowthOrder').style.display = 'none';
        document.getElementById('GdpOrder').style.display = 'none';
        document.getElementById('numberOfYears').style.display = 'none';
        document.getElementById('labelForDensity').style.display = 'none';
    } else if (newCol == 'PopulationGrowth') {
        document.getElementById('PopulationGrowthOrder').style.display = 'block';
        document.getElementById('numberOfYears').style.display = 'block';
        document.getElementById('labelForDensity').style.display = 'block';
        document.getElementById('GdpOrder').style.display = 'none';
        document.getElementById('DensityOrder').style.display = 'none';
    } else if (newCol == 'Gdp') {
        document.getElementById('GdpOrder').style.display = 'block';
        document.getElementById('DensityOrder').style.display = 'none';
        document.getElementById('PopulationGrowthOrder').style.display = 'none';
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
