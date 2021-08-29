<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Async="true" CodeBehind="UpdateStore.aspx.cs" Inherits="StoreApp.NetFramework.SubDir.UpdateStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/UpdateStore.css" rel="stylesheet"/>
    <section class="login">
        <label for="StoreName">
            <b>
                Store Name
            </b>
        </label>
        <input type="text" name="StoreName" id="StoreName" placeholder="Store name" style="width:250px" runat="server"/>
        <br><br>
        <label for="ProductCount">
            <b>
                Product Count
            </b>
        </label>
        <input type="number" name="ProductCount" id="ProductCount" runat="server"/>
        <input type="hidden" name="StoreId" id="StoreId" runat="server"/>
        <br><br>
        <input type="submit" name="Update" id="Update" value="Update" runat="server"/>
        <br><br>
    </section>
</asp:Content>