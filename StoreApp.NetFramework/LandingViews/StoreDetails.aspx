<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StoreDetails.aspx.cs" 
    Inherits="StoreApp.NetFramework.LandingViews.StoreDetails" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/StoreDetails.css" rel="stylesheet" />
    <asp:FormView ID="StoreDetailsId" runat="server"
        ItemType="StoreApp.Models.Store" RenderOuterTable="false" SelectMethod="GetStore">
         <ItemTemplate>
            <div>
                <h1><%#:Item.Name %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="<%#:Item.Image %>" style="border:solid; height:300px" alt="<%#:Item.Name %>"/>
                    </td>
                    <td>&nbsp;</td>  
                    <td class="name-column">
                        <b>Store Name: <span class="sub-name"> #<%#:Item.Name %></span>
                        <br />
                        <span ><b>Number of products:</b><span class="product-column">&nbsp;<%#:String.Format("{0:N}", Item.TotalNumberOfProducts) %></span>
                        </span>
                    </td>
                </tr>
            </table>
        </ItemTemplate>

    </asp:FormView>
</asp:Content>
