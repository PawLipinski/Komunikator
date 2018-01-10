<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatPage.aspx.cs" Inherits="ChatPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link runat="server" id="pagestyle" rel="stylesheet" href="IndexStyle.css" type="text/css"/>
    <title></title>
    <script src="interaction.js"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function LoadMessages() {
        $.ajax({
            type: "POST",
            url: "ChatPage.aspx/LoadMessages",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            error: function(){

                alert("Error");
        }
        });
    }
 
    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var communicates = xml.find("Komunikaty");
        var row = $("[id*=chatTable] tr:last-child").clone(true);
        $("[id*=chatTable] tr").not($("[id*=chatTable] tr:first-child")).remove();
        $.each(communicates, function () {
            var communicate = $(this);
            $("td", row).eq(0).html($(this).find("nadawca").text());
            $("td", row).eq(1).html($(this).find("data_dodania").text());
            $("td", row).eq(2).html($(this).find("komunikat").text());
            $("[id*=chatTable]").append(row);
            row = $("[id*=gvCustomers] tr:last-child").clone(true);
        });
    };
</script>

</head>
<body>

<%--<script type="text/javascript">

    //setInterval(
    //    $("sendButton").click(function(){
    //        $("#chatTable").load('WebForm1.aspx #container');   
    //    }, 5000));

    $('#chatDiv').scrollTop($('#ChatDiv')[0].scrollHeight);

</script>--%>

<%--<script type="text/javascript">
function PerformSearchViaPageMethod()
{
    //var searchKeyWord = document.getElementById('searchKeywordBox').value;
    PageMethods.GetSearchResult
    (OnPerformSearchViaPageMethodCallComplete);
    return false;
}
function OnPerformSearchViaPageMethodCallComplete(asyncResult)
{
    var searchResultsContainer = document.getElementById('chatDiv');
    searchResultsContainer.innerHTML = asyncResult;
}
</script>--%>
    
    
        <form id="form1" runat="server">
        <asp:Label runat="server" ID="etykieta"></asp:Label>
        <div id="container">
                <asp:Button runat="server" ID="backToProfile" Text="Powrót" OnClick="backToProfile_Click"/>
                <div id="tableWrapper">
            <div class="SSDiv" id="chatDiv">

                
<%--                        <asp:UpdatePanel runat="server" UpdateMode="Always">
                            <Triggers>
                            <asp:AsyncPostBackTrigger controlid="ReloadButton" eventname="Click" />--%>
<%--                            </Triggers>
                                <ContentTemplate>--%>
<%--                                
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>

<%--                <asp:TextBox runat="server" ID="textField"></asp:TextBox>
                <asp:Button runat="server" ID="sendButton" Text="Wyślij" OnClick="sendButton_Click"/>--%>

                 <%--<asp:Table runat="server" ID="chatTable"></asp:Table>--%>

                <asp:GridView runat="server" ID="chatTable" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField ItemStyle-Width="150px" DataField="nadawca" HeaderText="Nadawca" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="data_dodania" HeaderText="Czas" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="komunikat" HeaderText="Wiadomość" />
                    </Columns>
                    
                </asp:GridView>

            </div>
            <div class="SSDiv" id="writeDiv">
                <asp:TextBox runat="server" ID="textField" AutoComplete="off"></asp:TextBox>
                <asp:Button runat="server" ID="sendButton" Text="Wyślij" OnClick="sendButton_Click"/>
                <asp:Button runat="server"  ID="loadButton" Text="Załaduj" OnClientClick="LoadMessages(); return false;" OnClick="loadButton_Click"/>
            </div>
          </div>
        </div>
      <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
                    
    </form>
</body>
</html>
