<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Async="true" CodeBehind="AddStore.aspx.cs" Inherits="StoreApp.NetFramework.SubDir.AddStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/AddStore.css" rel="stylesheet" />
    <section class="AddStore">
        <label for="StoreName">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_StoreName" runat="server"
                ErrorMessage="Please enter the store name"
                ControlToValidate="StoreName" Display="Static"
                Font-Bold="true" ForeColor="Pink" CssClass="label">
            </asp:RequiredFieldValidator>
            <b>Store Name    
            </b>
        </label>
        <input type="text" name="StoreName" id="StoreName" placeholder="Store name" runat="server" />
        <br>
        <br>
        <label for="ProductCount">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_ProductCount" runat="server"
                ErrorMessage="Please enter the number of products"
                ControlToValidate="ProductCount" Display="Static"
                Font-Bold="true" ForeColor="Pink" CssClass="label">
            </asp:RequiredFieldValidator>
            <b>Product Count     
            </b>
        </label>
        <input type="number" name="ProductCount" id="ProductCount" runat="server" />
        <br>
        <br>

          <label for="FileUpload">
            <b>Store Image     
            </b>
        </label>
        <asp:FileUpload ID="FileUpload" runat="server" />

        <input type="submit" name="log" id="Add" value="Add" style="margin-left:80px;margin-top:20px" runat="server" />
        <br />
        <asp:Label ID="ErrorLbl" Text="" runat="server" Font-Bold="true"/>
        <br>
        <br>
    </section>
</asp:Content>
