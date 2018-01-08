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
    <form id="form1" runat="server" defaultbutton="loginButton">
        
        <div id="container">
                <asp:button runat="server" ID="LanguageChange" CssClass="SSButton" text="<%$ Resources: langChange %>" cssstyle="clear: left;" OnClick="LanguageChange_Click"/>
                <asp:button runat="server" ID="StyleChange" CssClass="SSButton" text="<%$ Resources: stylChange %>"  cssstyle="clear: left;"  Onclick="StyleChange_Click"/>
        <div id="LogonField" class="SSDiv">
            <h1 id ="TekstPowitalny"><asp:Literal runat="server" Text="<%$ Resources: logText %>" /><a href="index.aspx" class="indexLink">Sprechen-Sprechen</a>!</h1>
            <div class ="inputLine"><h2 style="display:inline-block; margin-right:30px;">Login:</h2><asp:textbox runat="server" type="text" class="loginInput" ID="login"/></div>
            <div class ="inputLine"><h2 style="display:inline-block; margin-right:30px;"><asp:Literal runat="server" Text="<%$ Resources: logPass %>" /></h2><asp:textbox runat="server" type="password" class="loginInput" ID="haslo" /></div>
			<asp:button runat="server" ID="loginButton" CssClass="SSButton" type="submit" text="<%$ Resources: logButton %>" OnClick="loginButton_Click"/>
            <hr />
            <p style="margin-bottom:0px;"><asp:Literal runat="server" Text="<%$ Resources: botText %>" /> <a href="registration.aspx" class="indexLink" id="registerLink"><asp:Literal runat="server" Text="<%$ Resources: registerLink %>" /></a><asp:Literal runat="server" Text="<%$ Resources: registerRemainText %>" /></p>
        </div>

        </div>
        <div id="Footer">
            <a href="registration.aspx" class="footerLink"><asp:Literal runat="server" Text="<%$ Resources: registerLink %>" /></a>
            <a href="registration.aspx" class="footerLink"><asp:Literal runat="server" Text="<%$ Resources: aboutPage %>" /></a>
        </div>
    </form>
</body>
</html>