<%@ Page Title="İletişim" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%: Title %>.</h2>
        <address>
            Ergazi, Fatih Sultan Mehmet Bulvarı<br />
            1817. Sokak 448/B, 06370 <br />
            Yenimahalle/Ankara<br />
            <abbr title="Telefon">Tel:</abbr>   <a href="tel:00000000000">(0000) 000 00 00</a>
        </address>

        <address>
            <strong>Destek:</strong>   <a href="mailto:abdullahtahaardogan@gmail.com">abdullahtahaardogan@gmail.com</a><br />
        </address>
    </div>
</asp:Content>
