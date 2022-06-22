var username = document.getElementById("username").value;

window.onload = validateForm;
function validateForm() {
    if (username != "required") {
        if (username == "exist") {
            alert("Username exist");
            return false;
        }
        else if (username == "notexist"){
            window.location.href = '/Login';
        }
    }
}