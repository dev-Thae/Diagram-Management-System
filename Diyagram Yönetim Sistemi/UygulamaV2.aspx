<%@ Page Title="UygulamaV2" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UygulamaV2.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.UygulamaV2" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">mxBasePath = '/Scripts/src/';</script>
	<script type="text/javascript" src="Scripts/src/js/mxClient.js"></script>
    <script type="text/javascript" src="mxgraph/grapheditor/www/sanitizer/sanitizer.min.js"></script>
    <script type="text/javascript" src="mxgraph/src/js/mxClient.js"></script>
    <script type="text/javascript" src="mxgraph/grapheditor/www/js/Graph.js"></script>
    <script type="text/javascript" src="mxgraph/grapheditor/www/js/Shapes.js"></script>

    
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Init.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/deflate/pako.min.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/deflate/base64.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/jscolor/jscolor.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/sanitizer/sanitizer.min.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/../../mxClient.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/EditorUi.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Editor.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Sidebar.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Graph.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Format.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Shapes.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Actions.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Menus.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Toolbar.js"></script>
	<script type="text/javascript" src="mxgraph/grapheditor/www/js/Dialogs.js"></script>

    <div class="container">

    <div class="row">
        <div class="col-lg-3 col-md-4" style="padding-left: 25px; overflow: auto; background-color: aliceblue;">
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
	    <div class="col-lg-9 col-md-8">
            <div id="graph" class="mx-auto" style="background: url('Scripts/src/images/grid.gif'); overflow: scroll; overflow-y: auto; overflow-x: auto"></div>
	    </div>
    </div>
    </div>
    
	<script type="text/javascript">
        var graph = new Graph(document.getElementById('graph'));
        graph.resizeContainer = true;
        graph.setEnabled(false);

        function show() {
            var xmlDoc = mxUtils.parseXml('<% = xml %>');
            var codec = new mxCodec(xmlDoc);
            codec.decode(xmlDoc.documentElement, graph.getModel());
        };
	</script>

    <div>
    </div>
</asp:Content>


