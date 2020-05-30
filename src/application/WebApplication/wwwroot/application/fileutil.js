const basePath = "/application/componenets/";
const displayLoadDiv = false;

function getSrcForHTML(f) {
    if (f && f.indexOf('.html') != -1) {
        return basePath + 'html/' + f + '?' + uuidv4();
    }
    return f;
}

function readHTML(fileName, success, failure, unauth) {
    var xhttp;
    var src = getSrcForHTML(fileName);
    if (src) {
        xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4) {
                if (this.status == 200) {
                    if (success)
                        success(fileName, this.responseText);
                }
                else {
                    if (this.status == 401) {
                        if (unauth)
                            unauth(fileName);
					}
                    else if (failure)
                        failure(fileName);
                }
            }
        }
        xhttp.open("GET", src, true);
        xhttp.send();
    }
}

function addHTMLIntoPublicDiv(fileName, success, failure, unauth) {
    var elmnt = document.createElement('div');
    if (!displayLoadDiv)
        elmnt.style.display = "none";
    readHTML(fileName, function (fileName,data) {
        elmnt.innerHTML = data;
        document.body.appendChild(elmnt);
        success(fileName, data);
    }, function (f) {
        if (failure)
            failure(f);
    }, function (f) {
        if (unauth)
            unauth(f);
    });
}

function addHTMLIntoAppDiv(fileName, success, failure, unauth) {
    var elmnt = document.getElementById(appName);
    readHTML(fileName, function (fileName, data) {
        elmnt.innerHTML += data;    
        success(fileName, data);
    }, function (f) {
        if (failure)
            failure(f);
    }, function (f) {
        if (unauth)
            unauth(f);
    });
}