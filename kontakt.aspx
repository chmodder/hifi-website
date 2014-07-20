<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="kontakt.aspx.cs" Inherits="kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <asp:Table ID="kontaktForm" Width="450" CellSpacing="3" CellPadding="0" runat="server">

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">&nbsp;</asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">fornavn:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="fornavn" runat="server"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFornavn" runat="server" ErrorMessage="Du skal indtaste dit fornavn!" ControlToValidate="fornavn" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RegularExpressionValidator ID="RegularExpressionFornavn" runat="server" ErrorMessage="Dit fornavn skal være på mindst 2 bogstaver!" ControlToValidate="fornavn" ValidationExpression="^.{2,50}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">efternavn:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="efternavn" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredEfternavn" runat="server" ErrorMessage="Du skal indtaste dit efternavn!" ControlToValidate="efternavn" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RegularExpressionValidator ID="RegularExpressionefternavn" runat="server" ErrorMessage="Dit efternavn skal være mellem 2 og 50 bogstaver!" ControlToValidate="efternavn" ValidationExpression="^.{2,50}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">adresse:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="adresse" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredAdresse" runat="server" ErrorMessage="Du skal indtaste din adresse!" ControlToValidate="adresse" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RegularExpressionValidator ID="RegularExpressionAdresse" runat="server" ErrorMessage="Din adresse skal være mellem 3 og 100 bogstaver eller tal." ControlToValidate="adresse" ValidationExpression="^.{3,100}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">postnr.:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="postnr" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFieldPostnr" runat="server" ErrorMessage="Du skal indtaste dit postnummer!" ControlToValidate="postnr" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RangeValidator ID="RangePostnr" runat="server" ErrorMessage="Dit post nummer skal være et tal på mellem 1050 og 9990" ControlToValidate="postnr" MaximumValue="9990" MinimumValue="1050" Display="Dynamic" Type="Integer"></asp:RangeValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">By:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="bynavn" runat="server"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFieldBynavn" runat="server" ErrorMessage="Du skal indtaste navnet på din by!" ControlToValidate="bynavn" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <%--Mangler at lave regEx så den udelukker tal og tegn--%>
                <asp:RegularExpressionValidator ID="RegularExpressionBynavn" runat="server" ErrorMessage="Navnet på din by skal være på 3 og 50 bogstaver!" ControlToValidate="bynavn" ValidationExpression="^.{3,50}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">e-mail:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RequiredFieldValidator ID="RequiredFieldEmail" runat="server" ErrorMessage="Du skal indtaste din email!" ControlToValidate="email" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <%--Mangler at lave regEx så den udelukker tal og tegn--%>
                <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server" ErrorMessage="Det skal være en gyldig emailadresse!" ControlToValidate="email" ValidationExpression="^[-0-9a-zA-Z.+_]+@[-0-9a-zA-Z.+_]+\.[a-zA-Z]{2,5}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">evt. tlf:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="tlf" runat="server" TextMode="Phone"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>


        <asp:TableRow></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell VerticalAlign="Top">besked:</asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="besked" runat="server" Rows="5" Columns="30" TextMode="MultiLine"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:RegularExpressionValidator ID="RegularExpressionBesked" runat="server" ErrorMessage="Beskeden er for kort. Skriv mindst 20 karakterer!" ControlToValidate="besked" ValidationExpression="^.{20,1000}$" Display="Dynamic"></asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="button" runat="server" Text="Send" OnClick="button_Click" />
            </asp:TableCell>
            <asp:TableCell>
            </asp:TableCell>
            <asp:TableCell>
            </asp:TableCell>
        </asp:TableRow>

    </asp:Table>

    <asp:Label ID="Label_besked" runat="server" Text=""></asp:Label>

</asp:Content>

