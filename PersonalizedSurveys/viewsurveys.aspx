<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="viewsurveys.aspx.cs" Inherits="viewsurveys" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <hr />
    <h4>View Surveys</h4>
    <hr />
    <div class="row">
        <div class="col-3">
            <div class="container"  style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                border-radius:5px;">
                <hr />
                <h5>Active Surveys</h5>
                <hr />
                <asp:PlaceHolder ID="SurveysPlaceholder" runat="server">
                    There are currently no surveys set active.
                </asp:PlaceHolder>
            </div>
        </div>
        <div class="col-9">
            <div class="container-fluid"  style="border:solid 1px black;border-left:solid 2px black; border-top:solid 2px black;
                border-radius:5px;">
                <hr />
                <h5>
                    <asp:Label ID="TitleLbl" runat="server" Text="View Surveys"></asp:Label>
                </h5>
                <hr />
                <asp:Label ID="ViewSurveyMessageLbl" runat="server" Visible ="false"></asp:Label>
                <asp:GridView ID="ViewSurveyGridView" runat="server" >
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="View Survey" ShowHeader="True" Text="View" />
                    </Columns>
                </asp:GridView>
                <hr />
                <asp:Label ID="Messagelbl" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

    <hr />

</asp:Content>

