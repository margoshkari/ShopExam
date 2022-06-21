var isAdd = document.getElementById('isadd').innerHTML;

window.addEventListener('load', start, false);
function start() {
    if (isAdd != "\n") {
        if (isAdd.includes("True")) {
            document.getElementById("addform").style = "display:unset;";
        }
        else {
            document.getElementById("deleteform").style = "display:unset;";
        }
    }
}