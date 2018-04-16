<%@ Page Language="C#" AutoEventWireup="true" CodeFile="takesurvey.aspx.cs" Inherits="takesurvey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Personalized Surveys</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/MySyles1.css" rel="stylesheet" />
    <script src="js/bootstrap.js"></script>
</head>
<body>
    <form id="form1" class="vertical-center" runat="server" style="width:100%;height:100%;">
        <asp:Panel ID="SurveyPanel" CssClass="container col-6" runat="server" Style="border-radius:10px;padding-top:10px;width:100%;height:100%;">
            <div class="container-fluid col-auto" style="background-color:white;margin:0px 10px 10px 10px;border-radius:10px;">
                <div class="row" style="padding:10px 10px 10px 10px">
                    <div class="col-9">
                        <h3 style="margin-right:30px;">
                            <asp:Label ID="TitleLbl" runat="server" Text="Title"></asp:Label>
                        </h3>
                    </div>
                    <div class="col-1">
                        <asp:image ID="SurveyLogo" width="100px" runat="server"></asp:image>
                    </div>
                 </div>
            </div>
            
            <div class="col-10 container">
                    
                </div>
            <div class="jumbotron-fluid" style="background-color:white;margin:10px 10px 10px 10px;border-radius:5px;">
                <div class="col-12 container">
                    <div class="row">
                        <div class="col-12">
                            <asp:PlaceHolder ID="QuestionsPlaceholder" runat="server"></asp:PlaceHolder>
                            <asp:Label ID="Messagelbl" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                                                    
            </div>
            <div class="jumbotron-fluid" style="background-color:white;margin:10px 10px 10px 10px;border-radius:5px;">
                <div class="col-12 container">
                    <div class="row">
                        <div class="col-12" style="padding:10px 10px 10px 10px;">
                            <asp:Button ID="NextBtn" runat="server" Text="Next" CssClass="btn btn-secondary btn-sm" 
                                OnClick="NextBtn_Click" Visible="False"/>
                            <asp:Button ID="PrevBtn" runat="server" Text="Prev" CssClass="btn btn-secondary btn-sm" 
                                OnClick="NextBtn_Click" Visible="False"/>
                            <asp:Button ID="SubmitBtn" runat="server" Text="Submit" CssClass="btn btn-primary btn-sm"
                                OnClick="SumbitBtn_OnClick"/>
                            <asp:Label ID="Messagelbl2" runat="server" Visible="false"></asp:Label>

                        </div>
                    </div>
                </div>
                                                    
            </div>
        </asp:Panel>
    </form>
</body>
</html>
