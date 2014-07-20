<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="sletVare.aspx.cs" Inherits="admin_sletVare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <asp:Repeater ID="RepeaterSletProd" runat="server">

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

    <asp:Label ID="Label_besked" runat="server" Text="Er du sikker på at du vil slette dette produkt?"></asp:Label>

    <asp:Button ID="sletProdBtn" runat="server" Text="Ja, slet produkt" OnClick="sletProdBtn_Click" />

</asp:Content>

