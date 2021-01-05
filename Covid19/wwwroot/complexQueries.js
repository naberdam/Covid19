


function onChangeColumn(event) {
    if (event.target.value == 'Gdp') {
        document.getElementById('orderBy').style.display = 'none';
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


function onsubmitComplex(event) {
    event.preventDefault();
    let date = event.target.elements.date.value;
    //let descOrAsc = event.target.elements.descOrAsc.value;
    let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
    let orderBy = event.target.elements.orderBy.value;
    let column = event.target.elements.column.value;
    let descOrAsc = event.target.elements.descOrAsc.value;
    if (event.target.elements.count.value == 'PerMillion') {
        if (column == 'PopulationGrowth') { // working
            popGrowthPerMillion(convertedDate, descOrAsc, event.target.elements.numberOfYears.value, orderBy);
        }else if (column == 'Density') {
            densityPerMillion(convertedDate, descOrAsc, orderBy);
        } else if (column == 'Gdp') { // working
            gdpPerMillion(convertedDate, descOrAsc, orderBy);
        }
    } else if (event.target.elements.count.value == 'TotalNumber') {
        if (column == 'PopulationGrowth') { // working
            popGrowthTotal(convertedDate, descOrAsc, event.target.elements.numberOfYears.value, orderBy);
        } else if (column == 'Density') {
            densityTotal(convertedDate, descOrAsc, orderBy);
        } else if (column == 'Gdp') { // working
            gdpTotal(convertedDate, descOrAsc, orderBy);
        }
    }  
}

function onsubmitAvg(event) {
     // from SpecificCountryAndDateAvgSick
    event.preventDefault();
    let date = event.target.elements.dateDownQuery.value;
    let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
    let descOrAsc = event.target.elements.descOrAscDownQuery.value;

    if (descOrAsc == 'desc') {
        url = "/api/SpecificCountryAndDateAvgSick/?divOrAvg=Avg&date=" + convertedDate + "&desc=true";
    } else {
        url = "/api/SpecificCountryAndDateAvgSick/?divOrAvg=Avg&date=" + convertedDate + "&desc=false";
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
            alert("sorry, we don't have this data");
        }
    });

}


function onsubmitAvgDiv(event) {
    // from SpecificCountryAndDateAvgSick
    event.preventDefault();
    let date = event.target.elements.dateDownQuery.value;
    let convertedDate = "" + date[8] + date[9] + "/" + date[5] + date[6] + "/" + date[0] + date[1] + date[2] + date[3];
    let descOrAsc = event.target.elements.descOrAscDownQuery.value;

    if (descOrAsc == 'desc') {
        url = "/api/SpecificCountryAndDateAvgSick/?divOrAvg=Div&date=" + convertedDate + "&desc=true";
    } else {
        url = "/api/SpecificCountryAndDateAvgSick/?divOrAvg=Div&date=" + convertedDate + "&desc=false";
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
            alert("sorry, we don't have this data");
        }
    });

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
            alert("sorry, we don't have this data");
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
            alert("sorry, we don't have this data");
        }
    });
}

function popGrowthPerMillion(date, descOrAsc, numYears, orderBy) { // working
    // from CountrySickAndDeathsPerMillionAndGrowthController

    if (descOrAsc == 'desc') {
        url = "/api/CountrySickAndDeathsPerMillionAndGrowth?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears+ "&desc=true";
    } else {
        url = "/api/CountrySickAndDeathsPerMillionAndGrowth?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears + "&desc=false";
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
            alert("sorry, we don't have this data");
        }
    });
}

function popGrowthTotal(date, descOrAsc, numYears, orderBy) {
    // from CountrySickAndDeathsAndGrowthController
    if (descOrAsc == 'desc') {
        url = "/api/CountrySickAndDeathsAndGrowth?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears + "&desc=true";
    } else {
        url = "/api/CountrySickAndDeathsAndGrowth?date=" + date + "&orderBySickDeathGrowth=" + orderBy + "&numYears=" + numYears + "&desc=false";
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
            alert("sorry, we don't have this data");
        }
    });
}

function densityTotal(date, descOrAsc, orderBy) {
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
            alert("sorry, we don't have this data");
        }
    });
}

function densityPerMillion(date, descOrAsc, orderBy) {
    // from CountriesDeathsVsDensity2020PerMillionController
    if (descOrAsc == 'desc') {
        url = "/api/CountriesDeathsVsDensity2020PerMillion?densityOrDeath=" + orderBy + "&date=" + date + "&desc=true";
    } else {
        url = "/api/CountriesDeathsVsDensity2020PerMillion?densityOrDeath=" + orderBy + "&date=" + date + "&desc=false";
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
            alert("sorry, we don't have this data");
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
//            alert("sorry, we don't have this data");
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
            alert("sorry, we don't have this data");
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
            alert("sorry, we don't have this data");
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
            alert("sorry, we don't have this data");
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
        document.getElementById('DensityOrder').label = 'density today';
        document.getElementById('PopulationGrowthOrder').style.display = 'none';
        document.getElementById('PopulationGrowthOrder').label = 'choose again!';
        document.getElementById('GdpOrder').style.display = 'none';
        document.getElementById('GdpOrder').label = 'choose again!';
        document.getElementById('numberOfYears').style.display = 'none';
        document.getElementById('labelForDensity').style.display = 'none';
    } else if (newCol == 'PopulationGrowth') {
        document.getElementById('PopulationGrowthOrder').style.display = 'block';
        document.getElementById('PopulationGrowthOrder').label = 'population growth';
        document.getElementById('numberOfYears').style.display = 'block';
        document.getElementById('labelForDensity').style.display = 'block';
        document.getElementById('GdpOrder').style.display = 'none';
        document.getElementById('DensityOrder').style.display = 'none';
        document.getElementById('GdpOrder').label = 'choose again!';
        document.getElementById('DensityOrder').label = 'choose again!';
    } else if (newCol == 'Gdp') {
        document.getElementById('GdpOrder').style.display = 'block';
        document.getElementById('GdpOrder').label = 'gdp';
        document.getElementById('DensityOrder').style.display = 'none';
        document.getElementById('PopulationGrowthOrder').style.display = 'none';
        document.getElementById('DensityOrder').label = 'choose again!';
        document.getElementById('PopulationGrowthOrder').label = 'choose again!';
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
