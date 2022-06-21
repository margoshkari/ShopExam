var tmp = JSON.parse(document.getElementById('tmp').innerHTML);
var popup = document.getElementById("pop-up");
var image = document.getElementById("image");
var productname = document.getElementById("name");
var closebtn = document.getElementById("closebtn");
var info = document.getElementById("info");
var price = document.getElementById("price");
var isLogin = document.getElementById('login').innerHTML;
var inputprod = document.getElementById('inputprod');

window.onload = funonload;
function funonload() {
    for (var i = 0; i < tmp.length; i++) {
        let element = document.createElement("div");
        element.setAttribute('class', `elem`);
        
        let image = document.createElement("img");
        image.setAttribute('src', `/img/${tmp[i]["ImageName"]}`);
        image.setAttribute('draggable', `false`);
        element.appendChild(image);

        let name = document.createElement("h2");
        let text = document.createTextNode(`${tmp[i]["ProductName"]}`);
        name.appendChild(text);
        element.appendChild(name);

        let container = document.getElementById("content");
        container.appendChild(element);
    }

    const elems = document.querySelectorAll('.elem');

    elems.forEach(elem => {
        elem.addEventListener('click', event => {
            PopUp(elem);
        });
    });

    closebtn.onclick = function () {
        popup.style = "visibility: hidden";
    };
    if (isLogin != "\n") {
        document.getElementById("loginbtn").style = "display: none";
    }
    else {
        document.getElementById("cartbtn").style = "display: none";
    }
}

function PopUp(elem) {
    image.src = elem.children[0].src;
    productname.innerHTML = elem.children[1].innerHTML;
    inputprod.value = productname.innerHTML;

    for (var i = 0; i < tmp.length; i++) {
        if (image.src.includes(tmp[i]["ImageName"])) {
            info.innerHTML = tmp[i]["Info"];
            price.innerHTML = tmp[i]["Price"] + "$";
            break;
        }
    }

    popup.style = "visibility: visible";
}