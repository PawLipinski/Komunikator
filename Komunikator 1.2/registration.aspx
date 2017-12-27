<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="IndexStyle.css" type="text/css"/>
    <script src="validateForm.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="return fi">
        <div>
            <div class="SSDiv" style="margin-top: 20%;">
                <h1>Podaj dane niezbędne do zarejestrowania!</h1>
                <div class="inputLine">
                    <h2 class="SSLabel" runat="server">Imię</h2>
                    <asp:TextBox ID="Imie" onclick="resetStyle(this.id);" class="inputText" runat="server" CssClass="loginInput"></asp:TextBox>
                </div>
                <div class="inputLine">
                    <h2  class="SSLabel" runat="server">Nazwisko</h2>
                    <asp:TextBox ID="Nazwisko" onclick="resetStyle(this.id);" runat="server" CssClass="loginInput"></asp:TextBox>
                </div>
                <div class="inputLine">
                    <h2 class="SSLabel" runat="server">Email</h2>
                    <asp:TextBox ID="Email" onclick="resetStyle(this.id);" runat="server" CssClass="loginInput"></asp:TextBox>
                </div>
                <div class="inputLine">
                    <h2 class="SSLabel" runat="server">Login</h2>
                    <asp:TextBox ID="Login" onclick="resetStyle(this.id);" CssClass="loginInput" runat="server"></asp:TextBox>
                </div>
                <div class="inputLine">
                    <h2 class="SSLabel" runat="server">Hasło</h2>
                    <asp:TextBox ID="Haslo" onclick="resetStyle(this.id);" CssClass="loginInput" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <div class="inputLine">
                    <h2 class="SSLabel" runat="server">Powtórz hasło</h2>
                    <asp:TextBox ID="powtorzoneHaslo" onclick="resetStyle(this.id);" CssClass="loginInput" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                
                    <asp:Button ID="loginButton" CssClass="SSButton" runat="server" OnClientClick="return validateRegistrationForm();" text="ZAREJESTRUJ" OnClick="loginButton_Click"/>
            </div>

        </div>
    </form>
</body>
</html>
