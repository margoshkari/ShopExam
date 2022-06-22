var isLogin = document.getElementById('login').innerHTML;

window.addEventListener('load', start, false);
function start() {
    if (isLogin != "\n") {
        document.getElementById("logoutbtn").style = "display:unset;";
        document.getElementById("logoutbtn").onclick = function () {
            document.cookie = `login=${isLogin};max-age=0`;
        }
        if (isLogin != "\nadmin") {
            document.getElementById("cartbtn").style = "display:unset;  background-image: url(../img/cart-outline.svg); background-repeat: no-repeat;background-position: center;color:transparent;";
        }
    }
    else {
        document.getElementById("loginbtn").style = "display:unset";
    }
}