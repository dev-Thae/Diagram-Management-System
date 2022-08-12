<%@ Page Title="Uygulama" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Uygulama.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.Uygulama" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">mxBasePath = '/Scripts/src/';</script>
	<script type="text/javascript" src="Scripts/src/js/mxClient.js"></script>

    <div class="row">
        <div class="col-lg-3 col-md-4" style="padding-left: 25px">
            <h3>Diagrams Directories</h3>
            <hr />
            <asp:TreeView ID="TreeView" runat="server" ImageSet="WindowsHelp" OnSelectedNodeChanged="TreeView_SelectedNodeChanged">
                <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="1px"></NodeStyle>
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
        <div class="col-lg-9 col-md-8" style="padding-right: 25px;">
            <div id="graphContainer"
                style="overflow: scroll; background: url('Scripts/src/images/grid.gif')">
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        function printGraph(sender, args) {
            if (!mxClient.isBrowserSupported())
                mxUtils.error('Tarayıcı desteklenmiyor!', 200, false);
            else {
                var graphContainer = $get('graphContainer');
                var graphControl = $create(aspnet.GraphControl, null, null, null, graphContainer);
                var doc = mxUtils.parseXml('<% = xml %>');
                //console.log(doc.documentElement);
                graphControl.decode(doc.documentElement);
            }
        }
        var app = Sys.Application;
        app.add_init();
    </script>

    <div>
    </div>
</asp:Content>


