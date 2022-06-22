var products = JSON.parse(document.getElementById('products').innerHTML);
var categories = JSON.parse(document.getElementById('categories').innerHTML);
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
    AddCard();
    AddCategory();

    const elems = document.querySelectorAll('.elem');
    elems.forEach(elem => {
        elem.addEventListener('click', event => {
            PopUp(elem);
        });
    });

    closebtn.onclick = function () {
        document.body.style.overflowY = 'visible';
        popup.style = "visibility: hidden";
    };
}

function AddCard() {
    for (var i = 0; i < products.length; i++) {
        let element = document.createElement("div");
        element.setAttribute('class', `elem`);

        let image = document.createElement("img");
        image.setAttribute('src', `/img/${products[i]["ImageName"]}`);
        image.setAttribute('draggable', `false`);
        element.appendChild(image);

        let name = document.createElement("h2");
        let text = document.createTextNode(`${products[i]["ProductName"]}`);
        name.appendChild(text);
        element.appendChild(name);

        let container = document.getElementById("content");
        container.appendChild(element);
    }
}

function AddCategory() {
    for (var i = 0; i < categories.length; i++) {
        let li = document.createElement("li");
        li.style = "height: 90px; display: flex; align-items: center; list-style: none;";

        //Кнопка категории
        let btn = document.createElement("div");
        btn.setAttribute('class', `categorybtn`);
        btn.setAttribute('name', `${categories[i]["idCategory"]}`);
        btn.innerHTML = `${categories[i]["CategoryName"]}`;
        btn.style = "cursor:pointer; font-size: 30px; text-decoration: none; width: 100%;" +
            "word-wrap: break-word; background-color: white; text-align: left;";
        li.appendChild(btn);

        let container = document.getElementById("categoryid");
        container.appendChild(li);
    }
    $('.categorybtn').on('click', function () {
        categoryinput.value = $(this).attr('name');
        submitbtn.click();
    });
}

function PopUp(elem) {
    document.body.style.overflowY = 'hidden';
    image.src = elem.children[0].src;
    productname.innerHTML = elem.children[1].innerHTML;
    inputprod.value = productname.innerHTML;

    for (var i = 0; i < products.length; i++) {
        if (image.src.includes(products[i]["ImageName"])) {
            info.innerHTML = products[i]["Info"];
            price.innerHTML = products[i]["Price"] + "$";
            break;
        }
    }

    popup.style = "visibility: visible";
}