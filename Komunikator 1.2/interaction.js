function interactWithContact(number) {
    var cbtn = $('#<%= loginCell'+number+' %>');
    if (cbtn != null) {
        cbtn.click();
    }
    window.location.href = "ChatPage.aspx";
}

function alertum() {
    let login = document.getElementById(interlocutorLogin);

}

