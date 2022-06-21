var tmp = JSON.parse(document.getElementById('tmp').innerHTML);
var inputprod = document.getElementById('inputprod');
var submitbtn = document.getElementById('submitbtn');

window.onload = funonload;
function funonload() {
    for (var i = 0; i < tmp.length; i++) {
        let element = document.createElement("div");
        element.setAttribute('class', `elem`);

        //Картинка товара
        let image = document.createElement("img");
        image.setAttribute('src', `/img/${tmp[i]["ImageName"]}`);
        image.setAttribute('draggable', `false`);
        element.appendChild(image);

        //Название товара
        let name = document.createElement("h2");
        let text = document.createTextNode(`${tmp[i]["ProductName"]}`);
        name.appendChild(text);
        element.appendChild(name);

        //Кол-во товара
        let count = document.createElement("h3");
        let textcount = document.createTextNode(`Count: ${tmp[i]["Count"]}   Price: ${tmp[i]["Price"]}$`);
        count.appendChild(textcount);
        element.appendChild(count);

        //Кнопка "удалить"
        let deletebtn = document.createElement("div");
        deletebtn.setAttribute('class', `deletebtn`);
        deletebtn.setAttribute('name', `${tmp[i]["ProductName"]}`);
        deletebtn.innerHTML = `Delete`;
        deletebtn.style = "cursor:pointer; font-size: 15px;";
        element.appendChild(deletebtn);

        let container = document.getElementById("form-content");
        container.appendChild(element);
    }

    $('.deletebtn').on('click', function () {
        inputprod.value = $(this).attr('name');
        submitbtn.click();
    });
}