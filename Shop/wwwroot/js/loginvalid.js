var username = document.getElementById("username").value;

window.addEventListener('load', validateForm, false);

function validateForm() {
    if (username != "required") {
        if (username == "uncorrect") {
            alert("Uncorrect username or password");
            return false;
        }
        else if (username == "correct") {
            window.location.href = '/Index';
        }
    }
}