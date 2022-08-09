<%@ Page Title="Hakkında" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2><%: Title %>.</h2>
        <h3>Bu uygulama diyagramlar arasında hızlı geçiş yapılması amacı ile geliştirilmiştir.</h3>
        <p>Uygulama drawio dosyalarını açabilmektedir. "mxGraph" paketini içerir.</p>
    </div>
</asp:Content>
