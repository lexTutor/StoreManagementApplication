<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserStores.aspx.cs" Inherits="StoreApp.NetFramework.SubDir.UserStores" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Editable table -->
    <link href="../lib/font-awesome/css/fontawesome.min.css" rel="stylesheet" />
    <link href="../lib/font-awesome/css/all.min.css" rel="stylesheet" />
    <section>
        <div class="card">
            <h3 class="card-header text-center font-weight-bold text-uppercase py-4">Your Stores
            </h3>

            <div class="card-body">
                <div id="table" class="table-editable">
                    <span class="table-add float-right mb-3 mr-2">
                        <a href="AddStore.aspx" class="text-success">
                            <i class="fas fa-plus fa-2x" aria-hidden="true" style="margin-left:97%; margin-bottom:10px"></i>
                        </a>
                    </span>
                    <table class="table table-bordered table-responsive-md table-striped text-center">
                        <thead>
                            <tr>
                                <th class="text-center">Name</th>
                                <th class="text-center">Number of Products</th>
                                <th class="text-center">View Store</th>
                                <th class="text-center">Update</th>
                                <th class="text-center">Remove</th>
                            </tr>
                        </thead>

                        <asp:ListView ID="UserStoresId" runat="server"
                            DataKeyNames="Id" GroupItemCount="1"
                            ItemType="StoreApp.Models.Store" SelectMethod="GetUserStores">

                            <EmptyDataTemplate>
                                <table>
                                    <tr>
                                        <td>You do not have any registered stores.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <EmptyItemTemplate>
                            </EmptyItemTemplate>
                            <ItemTemplate>
                                <tbody>
                                    <tr>
                                        <td class="pt-3-half" contenteditable="true" runat="server"><%#:Item.Name %></td>
                                        <td class="pt-3-half" contenteditable="true" id="StoreId" runat="server"><%#:Item.TotalNumberOfProducts %></td>
                                        <td class="pt-3-half">
                                            <span class="table-remove">
                                                <button type="button" class="btn btn-success btn-rounded btn-sm my-0">
                                                    <a href="<%#:GetRouteUrl("StoreById", new {storeId = Item.Id}) %>" style="color:white !important">
                                                        <%--<i class="fa fa fa-external-link-alt mr-1" style="color: white !important">Details </i>--%>
                                                        Details
                                                </button>
                                            </span>
                                        </td>
                                        <td class="pt-3-half">
                                            <span class="table-remove">
                                                <button type="button" class="btn btn-warning btn-rounded btn-sm my-0">
                                                    <a href="<%#:GetRouteUrl("UpdateStoreDetails", new {storeId = Item.Id}) %>"  style="color:white !important">
                                                        Update
                                                </button>
                                            </span>

                                        </td>
                                        <td>
                                            <span class="table-remove">
                                                <button type="button" class="btn btn-danger btn-rounded btn-sm my-0">
                                                    <a href="RemoveStore.aspx/?storeId=<%#:Item.Id %>"  style="color:white !important"/>
                                                    Delete
                                                </button>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </ItemTemplate>
                        </asp:ListView>

                    </table>
                </div>
            </div>
        </div>

    </section>
    <!-- Editable table -->
</asp:Content>
