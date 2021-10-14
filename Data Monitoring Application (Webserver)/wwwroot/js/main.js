

function createLEDElements(LedData) {
    LedData.forEach(function (element) {
        let svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
        svg.setAttribute("width", "100px");
        svg.setAttribute("height", "100px");

        let path = document.createElementNS("http://www.w3.org/2000/svg", "path");
        path.setAttribute("fill", element.get("Color"));
        path.setAttribute("d", "M 71.578125 35.328125 L 26.433594 35.328125 L 26.433594 22.570312 C 26.433594 10.105469 36.539062 0 49.007812 0 C 61.46875 0 71.578125 10.105469 71.578125 22.570312 Z M 21.773438 38.761719 L 21.773438 52.011719 L 78.203125 52.011719 L 78.203125 38.761719 Z M 58.816406 76.914062 L 60.476562 76.914062 L 60.476562 99.976562 L 63.789062 99.976562 L 63.789062 76.914062 L 65.445312 76.914062 L 65.445312 55.570312 L 58.816406 55.570312 Z M 32.136719 76.914062 L 33.792969 76.914062 L 33.792969 94.453125 L 37.105469 94.453125 L 37.105469 76.914062 L 38.765625 76.914062 L 38.765625 55.570312 L 32.136719 55.570312 Z M 32.136719 76.914062 ");

        let h4 = document.createElement("h4");
        h4.innerHTML = element.get("SensorTyp") + element.get("ID");

        let p = document.createElement("p");
        p.innerHTML = "LED-State: " + element.get("State");

        let table = document.createElement("table");
        let column1 = document.createElement("td");
        let row1 = document.createElement("tr");
        let column2 = document.createElement("td");

        document.getElementById("svg-container").appendChild(table);
        table.appendChild(row1);
        row1.appendChild(column1);
        column1.append(svg);
        svg.appendChild(path);
        row1.appendChild(column2);
        column2.appendChild(h4);
        column2.appendChild(p);
    });
}
