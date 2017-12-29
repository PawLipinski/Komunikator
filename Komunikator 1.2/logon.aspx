<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logon.aspx.cs" Inherits="logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="description" content =""/>
	<meta name="keywords" content= "film, kino, najlepsze filmy, top filmy"/>
	<meta http-equiv="X-UA-Compatible" content="IE=edge, chrome=1"/>
    <link runat="server" id="pagestyle" rel="stylesheet" href="IndexStyle.css" type="text/css"/>
    <title>Strona logowania</title>
    <script src="interaction.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        
        <div id="container">
                <asp:button runat="server" ID="LanguageChange" CssClass="SSButton" text="Zmień język!" cssstyle="clear: left;"/>
                <asp:button runat="server" ID="StyleChange" CssClass="SSButton" text="Zmień styl!"  cssstyle="clear: left;"  Onclick="StyleChange_Click"/>
        <div id="LogonField" class="SSDiv">
            <h1 id ="TekstPowitalny">Zaloguj się do <a href="index.aspx" class="indexLink">Sprechen-Sprechen</a>!</h1>
            <div class ="inputLine"><h2 style="display:inline-block; margin-right:30px;">Login:</h2><asp:textbox runat="server" type="text" class="loginInput" ID="login"/></div>
            <div class ="inputLine"><h2 style="display:inline-block; margin-right:30px;">Hasło:</h2><asp:textbox runat="server" type="password" class="loginInput" ID="haslo" /></div>
			<asp:button runat="server" ID="loginButton" CssClass="SSButton" type="submit" text="ZALOGUJ" OnClick="loginButton_Click"/>
            <hr />
            <p style="margin-bottom:0px;">Nie masz jeszcze konta? <a href="registration.aspx" class="indexLink" id="registerLink">Zarejestruj się</a>, by być w kontakcie ze znajomymi!</p>
        </div>

        </div>
        <div id="Footer">
            <a href="registration.aspx" class="footerLink">Zarejestruj się!</a>
            <a href="registration.aspx" class="footerLink">O stronie</a>
        </div>
    </form>
</body>
</html>
