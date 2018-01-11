<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatPage.aspx.cs" Inherits="ChatPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link runat="server" id="pagestyle" rel="stylesheet" href="IndexStyle.css" type="text/css" />
    <title></title>

    <script src="interaction.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        setInterval(function () {
            LoadMessages();
        }, 2000);

        function LoadMessages() {
            $.ajax({
                type: "POST",
                url: "ChatPage.aspx/LoadMessages",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: function () {

                    alert("Error");
                }
            });
        }

        function OnSuccess(response) {
            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var communicates = xml.find("Komunikaty");
            var row = $("[id*=chatTable] tr:last-child").clone(true);
            //$("[id*=chatTable] tr").not($("[id*=chatTable] tr:first-child")).remove();

            $.each(communicates, function () {
                var communicate = $(this);
                $("td", row).eq(0).html($(this).find("ID").text());
                $("td", row).eq(1).html($(this).find("nadawca").text());
                $("td", row).eq(2).html($(this).find("data_dodania").text());
                $("td", row).eq(3).html($(this).find("komunikat").text());
                if ($(this).find('ID').text() > $("[id*=chatTable] tr:last td:first").text()) {
                    $("[id*=chatTable]").append(row);
                    row = $("[id*=chatTable] tr:last-child").clone(true);
                    $('[id*=chatDiv]').scrollTop($('[id*=chatDiv]')[0].scrollHeight);
                }
            });
        };
        let parameter = $("[id*=textField]").val();

        function SendMessage() {
            $.ajax({
                type: "POST",
                url: "ChatPage.aspx/SendMessage",
                data: '{text: "' +  $("[id*=textField]").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function () {
                    $("[id*=textField]").val("");
                }
            });
        }

        function myfunction() {
            return $("[id*=textField]").val();
        }


    </script>

</head>
<body>


    <form id="form1" runat="server" DefaultButton="sendButton" style="position:relative;">
        <asp:Label runat="server" ID="etykieta"></asp:Label>
        <div id="container" style="position:absolute;">
            <asp:Button runat="server" ID="backToProfile" Text="Powrót" OnClick="backToProfile_Click" />
            <div id="tableWrapper" style="position:absolute;">
                <div class="SSDiv" id="chatDiv">

                    <asp:GridView runat="server" ID="chatTable" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="3%" DataField="ID" HeaderText="ID" />
                            <asp:BoundField ItemStyle-Width="3%" DataField="nadawca" HeaderText="Nadawca" />
                            <asp:BoundField ItemStyle-Width="6%" DataField="data_dodania" HeaderText="Czas" />
                            <asp:BoundField ItemStyle-Width="25%" DataField="komunikat" HeaderText="Wiadomość" />
                        </Columns>

                    </asp:GridView>

                </div>
                <div class="SSDiv" id="writeDiv">
                    <asp:TextBox runat="server" ID="textField" AutoComplete="off"></asp:TextBox>
                    <asp:Button runat="server" ID="sendButton" Text="Wyślij" OnClientClick="SendMessage(); return false; " />
                    <asp:Button runat="server" ID="loadButton" Text="Załaduj" OnClientClick="LoadMessages(); return false;" OnClick="loadButton_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
