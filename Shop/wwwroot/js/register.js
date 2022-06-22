var username = document.getElementById("username").value;
var email = document.getElementById("useremail").value;

window.onload = validateForm;
function validateForm() {
    if (username != "required" || email != "required") {
        if (username == "exist") {
            alert("Username exist");
            return false;
        }
        else if (email == "exist")
        {
            alert("Email exist");
            return false;
        }
        else if (username == "notexist" && email == "notexist"){
            window.location.href = '/Login';
        }
    }
}