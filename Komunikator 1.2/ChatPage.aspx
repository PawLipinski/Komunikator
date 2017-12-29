<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChatPage.aspx.cs" Inherits="ChatPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link runat="server" id="pagestyle" rel="stylesheet" href="IndexStyle.css" type="text/css"/>
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script> 
    <script src="interaction.js"></script>

</head>
<body>

<%--<script type="text/javascript">

    //setInterval(
    //    $("sendButton").click(function(){
    //        $("#chatTable").load('WebForm1.aspx #container');   
    //    }, 5000));

    $('#chatDiv').scrollTop($('#ChatDiv')[0].scrollHeight);

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
                 <asp:Table runat="server" ID="chatTable"></asp:Table>

            </div>
            <div class="SSDiv" id="writeDiv">
                <asp:TextBox runat="server" ID="textField" AutoComplete="off"></asp:TextBox>
                <asp:Button runat="server" ID="sendButton" Text="Wyślij" OnClick="sendButton_Click"/>
            </div>
          </div>
        </div>
      <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
                    
    </form>
</body>
</html>
