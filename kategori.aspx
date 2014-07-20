<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="kategori.aspx.cs" Inherits="kategori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <asp:Repeater ID="RepeaterProdukter" runat="server">

        <ItemTemplate>

            <div class="billede_right">
                <img src="../img/products/thumbs/<%# Eval ("productImg") %>" />
            </div>
            <!--billed_left slut-->

            <span class="producent"><%# Eval ("brandsName") %></span> - <span class="fed"><%# Eval ("productModel") %></span><br />
            <%# Eval ("productDescription") %><br />
            pris:<span class="fed"> <%# Eval ("productPrice") %>  kr</span>

            <div class="break"></div>

            <hr />

        </ItemTemplate>

    </asp:Repeater>

</asp:Content>

