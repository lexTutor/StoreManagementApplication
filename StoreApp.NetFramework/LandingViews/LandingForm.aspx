<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LandingForm.aspx.cs" 
    Inherits="StoreApp.NetFramework.LandingViews.LandingForm" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/LandingPage.css" rel="stylesheet" />
     <section>
        <div>
            <hgroup>
                <h2><%: Page.Title%></h2>
            </hgroup>

            <div style="text-align: center; font-family:Candara;font-weight:bold;font-size:xx-large" >
                <p>Welcome to the Kingdom store App</p>
                <p style="font-size:large">Visit a store.</p>
                <br />
            </div>

            <div class="grid-container">
                <asp:ListView ID="StoreList" runat="server"
                    DataKeyNames="Id" GroupItemCount="1"
                    ItemType="StoreApp.Models.Store" SelectMethod="GetStores">

                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td>There are no available stores please check back later.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <EmptyItemTemplate>
                        <td />
                    </EmptyItemTemplate>
                    <GroupTemplate>
                        <tr id="itemPlaceholderContainer" runat="server">
                            <td id="itemPlaceholder" runat="server"></td>
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <div class="flip-card">
                            <div class="flip-card-inner">
                                <div class="flip-card-front">
                                    <a href="<%#:GetRouteUrl("StoreById", new {storeId = Item.Id }) %>">
                                        <img src="<%#:Item.Image %>" alt="<%#:Item.Name %>" style="width: 370px; height: 120px;">
                                    </a>
                                    <h4 class="p2"><b><%#:Item.Name %></b></h4>
                                    <p class="p1">Number of products: <%#: Item.TotalNumberOfProducts %></p>
                                </div>
                                <div class="flip-card-back">
                                    <a href="<%#:GetRouteUrl("StoreById", new {storeId = Item.Id }) %>">
                                        <img src="<%#:Item.Image %>" alt="<%#:Item.Name %>" style="width: 370px; height: 180px;">
                                    </a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </section>
</asp:Content>
