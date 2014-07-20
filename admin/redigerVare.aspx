<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="redigerVare.aspx.cs" Inherits="admin_redigerVare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="Server">

    <%--<asp:Button ID="tilbageBtn" runat="server" Text="Gå tilbage" OnClick="tilbageBtn_Click" />--%>

    <hr />

    <div class="billede_right">
        <%--<img src=<%= thumb() %> />--%>


        <asp:Image ID="productImg" runat="server" />
        <br />
        <asp:Label CssClass="fed" Text="Billednavn:" runat="server"></asp:Label>
        <asp:Label ID="labelProductImgName" runat="server"></asp:Label>
        <hr />
        <asp:FileUpload ID="billedUpload" runat="server" />

    </div>
    <!--billed_left slut-->

    <asp:SqlDataSource ID="SqlDataSourceBrands" runat="server" ConnectionString='<%$ ConnectionStrings:ConnectionString %>' SelectCommand="SELECT DISTINCT * FROM [Brands]"></asp:SqlDataSource>


    <asp:DropDownList
        ID="DropDownListBrandName"
        runat="server"
        DataSourceID="SqlDataSourceBrands"
        DataTextField="BrandsName"
        DataValueField="BrandsId"
        CssClass="producent">
    </asp:DropDownList>


    - 
                <asp:TextBox ID="TextBoxProductModel" CssClass="fed" runat="server"></asp:TextBox>
    <br />
    <asp:TextBox ID="TextBoxProductDescription" runat="server" TextMode="MultiLine" Rows="5" Width="47%"></asp:TextBox><br />
    pris:
    <asp:TextBox ID="TextBoxProductPrice" CssClass="fed" runat="server"></asp:TextBox><span class="fed">  kr</span>

    <div class="break"></div>



    <%--<a href="redigerVare.aspx?id=<%# Eval("productId") %>">Rediger varen</a>

            <a href="SletVare.aspx?id=<%# Eval("productId") %>">Slet varen</a>--%>

    <hr />

    <asp:Button ID="gemBtn" runat="server" Text="Gem" OnClick="gemBtn_Click" />
    <br />
    <asp:Label ID="Label_besked" runat="server" Text=""></asp:Label>

    <asp:RequiredFieldValidator ID="RequiredProductModel" runat="server" ControlToValidate="TextBoxProductModel" ErrorMessage="Du skal skrive et modelnavn" Display="Dynamic">
    </asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionAdresse" runat="server" ErrorMessage="Modelnavnet skal være mellem 1 og 50 bogstaver eller tal." ControlToValidate="TextBoxProductModel" ValidationExpression="^.{1,50}$" Display="Dynamic"></asp:RegularExpressionValidator>
    <br />
    <asp:RequiredFieldValidator ID="RequiredDescription" runat="server" ControlToValidate="TextBoxProductDescription" ErrorMessage="Du skal skrive en produktbeskrivelse" Display="Dynamic"></asp:RequiredFieldValidator>
    <br />
    <asp:RequiredFieldValidator ID="RequiredProductPrice" runat="server" ControlToValidate="TextBoxProductPrice" ErrorMessage="Du skal skrive en pris" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionProductPrice" runat="server" ErrorMessage="Prisen skal være et tal på minimum 1, og må ikke indeholde andre tegn end et komma" ValidationExpression="^\s*(?=.*[1-9])\d*(?:\,\d{1,2})?\s*$" Display="Dynamic" ControlToValidate="TextBoxProductPrice"></asp:RegularExpressionValidator>

    <%--
RegEx forklaring
^            # Start of string
\s*          # Optional whitespace
(?=.*[1-9])  # Assert that at least one digit > 0 is present in the string
\d*          # integer part (optional)
(?:          # decimal part:
 \,          # komma
 \d{1,2}     # plus one or two decimal digits
)?           # (optional)
\s*          # Optional whitespace
$            # End of string--%>
</asp:Content>



