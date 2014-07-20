<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <asp:Label ID="forsideOverskrift" CssClass="h2" runat="server" Text="Produktnyheder"></asp:Label>

    <hr />

    <asp:SqlDataSource ID="SqlDataSource1" SelectCommand="
        
        SELECT TOP 3 p.*, c.*, b.* 
        FROM Products p INNER JOIN Categories c ON p.fkCategoryId = c.categoriesId 
        INNER JOIN Brands b ON p.fkBrandId = b.BrandsId  
        ORDER BY productId DESC"
        runat="server" ConnectionString='<%$ ConnectionStrings:ConnectionString %>'></asp:SqlDataSource>

    <asp:Repeater ID="RepeaterProdukter" runat="server" DataSourceID="SqlDataSource1">

        <ItemTemplate>

            Kategori: <span class="h3"><%# Eval ("categoriesName") %></span>
            <hr />
            <div class="billede_right">
                <img src="../img/products/thumbs/<%# Eval ("productImg") %>" />
            </div>
            <!--billed_left slut-->

            <span class="producent"><%# Eval ("brandsName") %></span> - <span class="fed"><%# Eval ("productModel") %></span>
            <br />
             <%# Eval ("productDescription") %>
            <br />
             pris:<span class="fed"> <%# Eval ("productPrice") %>  kr</span>
            <br />
            
            <div class="break"></div>

            <hr />

        </ItemTemplate>

    </asp:Repeater>

</asp:Content>

