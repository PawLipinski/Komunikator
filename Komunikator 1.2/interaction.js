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

function changeStyle(sheet) {
    document.getElementById('pagestyle').setAttribute('href', sheet);
}

function changeStyle() {
    document.getElementById('pagestyle').setAttribute('href', document.cookie);
    //document.cookie = "style=IndexStyle.css";
}
