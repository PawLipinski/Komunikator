"use strict";

function validateRegistrationForm() {
    let fields = ["Imie", "Nazwisko", "Email", "Login", "Haslo", "powtorzoneHaslo"];

    let i, l = fields.length;
    let fieldname;

    let check = false;
    if (check === false) {
        check = true;
        for (i = 0; i < l; i++) {
            fieldname = fields[i];
            if (document.getElementById(fields[i]).value === "") {

                colourRed(fields[i]+"", "Należy wypełnić pole");
                check = false;
            }
        }
        if (check === false) return false;

        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!re.test(document.getElementById("Email").value)) {
            colourRed("Email", "Niewłaściwy format email.");
            return false;
        }  

        if (document.getElementById("Haslo").value === document.getElementById("powtorzoneHaslo").value) check = true;
        else {
            colourRed("powtorzoneHaslo", "Hasła nie zgadzają się.");
            check = false;
        if (check === false) return false;
        }

    }
}

function resetStyle(id) {
    document.getElementById(id).style.boxShadow = "none";
    document.getElementById(id).placeholder = "";
}

function colourRed(id,text) {
    document.getElementById(id).value = "";
    document.getElementById(id).placeholder = text;
    document.getElementById(id).style.boxShadow = "10px 10px 20px red";
}

function dbanswer(id, text,alertText) {
    colourRed(id, text);
    alert(alertText);
}

function registrationOK() {
    alert("Pomyślnie zarejestrowano nowego użytkownika!");
}