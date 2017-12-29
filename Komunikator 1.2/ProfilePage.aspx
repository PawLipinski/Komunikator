<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link runat="server" id="pagestyle" rel="stylesheet" href="IndexStyle.css" type="text/css"/>
    <script src="interaction.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="ProfileHeader" class="SSDiv">
                <asp:Label ID="Powitanie" runat="server"></asp:Label><br />   
                <asp:Button runat="server" ID="logout" Text="Wyloguj się" OnClick="logout_Click"/>
                <asp:Button runat="server" ID="findFriends" Text="Szukaj znajomych" OnClick="findFriends_Click"/>
            </div>
            <div id="contDiv" class="SSDiv">
                <asp:Table ID="ContactsTable" runat="server" EnableViewState = "false">
                    <asp:TableHeaderRow>
                        <asp:TableCell>Login</asp:TableCell>
                        <asp:TableCell>Imię</asp:TableCell>
                        <asp:TableCell>Nazwisko</asp:TableCell>
                        <asp:TableCell>Status</asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                        <asp:TableCell></asp:TableCell>
                    </asp:TableHeaderRow>
                </asp:Table>
                <asp:TextBox ID="interlocutorLogin" runat="server" Visible="false" ClientIDMode="Static"></asp:TextBox>
            </div>
        </div>
    </form>
</body>
</html>
