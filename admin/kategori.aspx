<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="kategori.aspx.cs" Inherits="admin_kategori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" Runat="Server">
    
    
    <asp:Button ID="opretProduktBtn" runat="server" Text="Opret nyt produkt" OnClick="opretProduktBtn_Click" />

    <hr />

    Sorter Efter <asp:Button ID="sortProducentBtn" runat="server" Text="producent" OnClick="sortProducentBtn_Click" />

    <asp:Button ID="sortModelnameBtn" runat="server" Text="modelnavn" OnClick="sortModelnameBtn_Click" />
    
    <asp:Button ID="sortPriceBtn" runat="server" Text="pris" OnClick="sortPriceBtn_Click" />

    <hr />

    <asp:Repeater ID="RepeaterRedProd" runat="server">

        <ItemTemplate>

            <div class="billede_right">
                <img alt="intet billede" src="../img/products/thumbs/<%# Eval ("productImg") %>" />
            </div>
            <!--billed_left slut-->

            <span class="producent"><%# Eval ("brandsName") %></span> - <span class="fed"><%# Eval ("productModel") %></span><br />
            <%# Eval ("productDescription") %><br />
            pris:<span class="fed"> <%# string.Format("{0:n2}", Eval ("productPrice")) %>  kr</span>

            <div class="break"></div>

            <%--<asp:Button ID="redProdBtn" runat="server" Text="rediger produkt" />

            <asp:Button ID="sletProdBtn" runat="server" Text="Slet produkt" />--%>

            <a href="redigerVare.aspx?id=<%# Eval("productId") %>&overskrift=<%# Eval("fkCategoryId") %>">Rediger varen</a>

            <a href="SletVare.aspx?id=<%# Eval("productId") %>">Slet varen</a>

            <hr />

        </ItemTemplate>

    </asp:Repeater>




</asp:Content>

