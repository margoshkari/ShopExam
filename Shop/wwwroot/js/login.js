var isLogin = document.getElementById('login').innerHTML;
//var username = document.getElementById("username").value;

window.addEventListener('load', start, false);
//window.addEventListener('validateForm', start, false);
function start() {
    if (isLogin != "\n") {
        if (isLogin != "\nadmin") {
            document.getElementById("cartbtn").style = "display:unset;  background-image: url(../img/cart-outline.svg); background-repeat: no-repeat;background-position: center;color:transparent;";
        }
        document.getElementById("logoutbtn").style = "display:unset;";
        document.getElementById("logoutbtn").onclick = function () {
            document.cookie = `login=${isLogin};max-age=0`;
        }
    }
    else {
        document.getElementById("loginbtn").style = "display:unset";
    }
}

function validateForm() {
    if (username != "required") {
        if (username == "exist") {
            alert("Username exist");
            return false;
        }
        else if (username == "notexist") {
            window.location.href = '/Login';
        }
    }
}