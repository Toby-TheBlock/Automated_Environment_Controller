

function createLEDElements(LedInformation) {
    LedInformation.forEach(function (element) {
        let svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        svg.setAttribute("width", "100px");
        svg.setAttribute("height", "100px");

        let path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        path.setAttribute("fill", element["Color"]);
        path.setAttribute("d", "M 71.578125 35.328125 L 26.433594 35.328125 L 26.433594 22.570312 C 26.433594 10.105469 36.539062 0 49.007812 0 C 61.46875 0 71.578125 10.105469 71.578125 22.570312 Z M 21.773438 38.761719 L 21.773438 52.011719 L 78.203125 52.011719 L 78.203125 38.761719 Z M 58.816406 76.914062 L 60.476562 76.914062 L 60.476562 99.976562 L 63.789062 99.976562 L 63.789062 76.914062 L 65.445312 76.914062 L 65.445312 55.570312 L 58.816406 55.570312 Z M 32.136719 76.914062 L 33.792969 76.914062 L 33.792969 94.453125 L 37.105469 94.453125 L 37.105469 76.914062 L 38.765625 76.914062 L 38.765625 55.570312 L 32.136719 55.570312 Z M 32.136719 76.914062 ");

        let p1 = document.createElement("p");
        p1.innerHTML = "<strong>LED" + element["LED_ID"] + "</strong> - " + element["Description"];

        let p2 = document.createElement("p");
        p2.innerHTML = "State: " + element["State"];

        let table = document.createElement("table");
        let column1 = document.createElement("td");
        let row1 = document.createElement("tr");
        let column2 = document.createElement("td");

        document.getElementById("led-container").appendChild(table);
        table.appendChild(row1);
        row1.appendChild(column1);
        column1.append(svg);
        svg.appendChild(path);
        row1.appendChild(column2);
        column2.appendChild(p1);
        column2.appendChild(p2);
    });
}


function getGraphlabel(measuringData, identifier) {
    for (let entry of measuringData) {
        if (entry["SensorID"] == identifier) {
            return entry["SensorType"];
        }
    }
}


function filterData(measuringData, identifier) {
    let timestamps = [];
    let measureValues = [];

    measuringData.forEach(function (entry) {
        if (entry["SensorID"] == identifier) {
            timestamps.push(entry["Timestamp"]);
            measureValues.push(parseFloat(entry["MeasureValue"].replace(',', '.')));
        }
    });

    return [timestamps, measureValues];
}



function findAllUniqueIdentifiers(measuringData) {
    let uniqueIdentifiers = [];

    measuringData.forEach(function (entry) {
        if (!uniqueIdentifiers.includes(entry["SensorID"])) {
            uniqueIdentifiers.push(entry["SensorID"]);
        }
    });

    return uniqueIdentifiers;
}



function createCanvasElements(identifiers) {
    identifiers.forEach(function (id) {
        let canvas = document.createElement("canvas");
        canvas.id = "canvas" + id;

        document.getElementById("data-container").appendChild(canvas);
    });
}



function createGraphs(measuringData, identifiers) {
    identifiers.forEach(function (id) {
        let xyValues = filterData(measuringData, id);
        let canvas = document.getElementById("canvas" + id).getContext("2d");
        document.getElementById("canvas" + id).style.cssText = "margin: 20px; padding: 10px; background-color: #ffffff;";

        new Chart(canvas, {
            type: "line",
            data: {
                labels: xyValues[0],
                datasets: [{
                    label: getGraphlabel(measuringData, id),
                    data: xyValues[1]
                }],
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: "Measurements from Sensor_" + id
                },
                scales: {
                    yAxes: [{
                        display: true,
                        ticks: {
                            max: Math.max.apply(null, xyValues[1]) + 1,
                            min: Math.min.apply(null, xyValues[1]) - 1
                        }
                    }]
                }
            },
        })
    });
}