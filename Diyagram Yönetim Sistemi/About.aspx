<%@ Page Title="Hakkında" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%: Title %>.</h2>
        <h4>Bu uygulama diyagramlar arasında hızlı geçiş yapılması amacı ile geliştirilmiştir.</h4>
        <ul>
            <li>Uygulama .drawio ve .dygrm dosyalarını açabilmektedir.</li>
            <li>Kendi editörü .dygrm uzantısı ile dosya oluşturup düzanleyebilmektedir.</li>
            <li>"mxGraph" paketini içerir.</li>
        </ul>
    </div>
</asp:Content>
