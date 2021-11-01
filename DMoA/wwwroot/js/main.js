
/**
 * Checks if the current measurement of a given type is within its allowed threshold.
 * @param {any} data - array containing individual maps with measurement-data.
 * @param {string} valueType - name of the measurement value type.
 * @returns - array containing the current measurement value, the threshold check result,
 * and the name of the image which symbolises the current measurement value. 
 */
function checkCurrentValueAgainstThreshold(data, valueType) {
    let currentStates = valueType.includes("Temperature") ?
        ["Too Cold! Heating needed!", "Too Hot! Cooling needed!", "Perfect Temperature!"] :
        ["Very dark! Extra light needed!", "Very bright! No extra light needed!", "Perfect lighting!"];
    let imageNames = valueType.includes("Temperature") ?
        ["tempCold", "tempHot", "tempRoom"] :
        ["lightOvercast", "lightSunny", "lightCloudy"];

    let currentValue;
    let index;

    for (i = 0; i < data.length; i++) {
        if (data[i]["SensorType"].includes(valueType)) {
            currentValue = data[i]["MeasureValue"];

            if (data[i]["MinThreshold"] == "True" && parseFloat(currentValue) < parseFloat(data[i]["Threshold"])) {
                index = 0;
                break;
            }
            else if (data[i]["MaxThreshold"] == "True" && parseFloat(currentValue) > parseFloat(data[i]["Threshold"])) {
                index = 1;
                break;
            }
            else {
                index = 2;
            }
        }
    }

    return [currentValue, currentStates[index], imageNames[index]];
}


/**
 * Creates a HTML canvas-elements for each element in an array.
 * @param {any} identifiers - array with ids which are to be used in the DOM-identifiers.
 */
function createCanvasElements(identifiers) {
    identifiers.forEach(function (id) {
        let canvas = document.createElement("canvas");
        canvas.id = "canvas" + id;

        document.getElementById("data-container").appendChild(canvas);
    });
}


/**
 * Creates a line-graph for each identifier with the provided data.  
 * @param {any} measuringData - array containing individual maps with measurement-data.
 * @param {any} identifiers - array of identifiers.
 */
function createGraphs(measuringData, identifiers) {

    let colors = ["rgba(67,209,68,0.3)", "rgba(209,68,67,0.3)", "rgba(209,139,67,0.3)", "rgba(67,137,209,0.3)", "rgba(208,209,67,0.3)"]

    identifiers.forEach(function (id) {
        let xyValues = filterData(measuringData, id);
        let canvas = document.getElementById("canvas" + id).getContext("2d");

        new Chart(canvas, {
            type: "line",
            data: {
                labels: xyValues[0],
                datasets: [{
                    label: getGraphlabel(measuringData, id),
                    data: xyValues[1],
                    backgroundColor: colors[Math.floor(Math.random() * colors.length)]
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


/**
 * Creates all of the HTML elements containing the data about the Environment Controller LED's.
 * @param {any} LedInformation - array containing individual maps with the LED-data.
 */
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
        p2.innerHTML = "<strong>State:</strong> " + element["State"];

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


/**
 * Creates all of the HTML elements giving an overview over the current environment conditions.
 * @param {any} measuringData - array containing individual maps with measurement-data.
 * @param {any} overviewData - array containing meta-data in relation to the configuration of the overviews.
 */
function createOverviewDivsContent(measuringData, overviewData) {
    overviewData.forEach(function (entry) {
        let config = checkCurrentValueAgainstThreshold(measuringData, entry[0]);

        let img = document.createElement("img");
        img.src = "/assets/" + config[2] + ".png";

        let p1 = document.createElement("p");
        p1.innerHTML = "<strong>Current " + entry[0] + "</strong>: " + config[0] + " " + entry[1];

        let p2 = document.createElement("p");
        p2.innerHTML = "<strong>State:</strong> " + config[1];

        let table = document.createElement("table");
        let column1 = document.createElement("td");
        let row1 = document.createElement("tr");
        let column2 = document.createElement("td");

        document.getElementById("overviews-container").appendChild(table);
        table.appendChild(row1);
        row1.appendChild(column1);
        column1.append(img);
        row1.appendChild(column2);
        column2.appendChild(p1);
        column2.appendChild(p2);
    });
}


/**
 * Filters out only graph related data based on a given ID.
 * @param {any} measuringData - array containing individual maps with measurement-data.
 * @param {string} identifier - the sensorID.
 * @returns - 2D-array containing all of the timestamps and the corresponding measurement values.
 */
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


/**
 * Filters out all of the unique identifiers from an array containing dictionaries.
 * @param {any} measuringData - an array containing individual maps with measurement-data.
 * @returns - array containing all of the unique identifiers.
 */
function findAllUniqueIdentifiers(measuringData) {
    let uniqueIdentifiers = [];

    measuringData.forEach(function (entry) {
        if (!uniqueIdentifiers.includes(entry["SensorID"])) {
            uniqueIdentifiers.push(entry["SensorID"]);
        }
    });

    return uniqueIdentifiers;
}


/**
 * Finds the corresponding sensor type based on a given ID.
 * @param {any} measuringData - array containing individual maps with measurement-data.
 * @param {string} identifier - the sensorID.
 * @returns - the sensor type.
 */
function getGraphlabel(measuringData, identifier) {
    for (let entry of measuringData) {
        if (entry["SensorID"] == identifier) {
            return entry["SensorType"];
        }
    }
}