<%@ Page Title="Uygulama" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Uygulama.aspx.cs" Inherits="Diyagram_Yönetim_Sistemi.Uygulama" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<script type="text/javascript">
	    mxBasePath = '/Scripts/src/';
	</script>
	<script type="text/javascript" src="Scripts/src/js/mxClient.js"></script>
    <div id="graphContainer"
        style="overflow: hidden; background: url('Scripts/src/images/grid.gif')">
    </div>
    <button id="saveButton" type="button">Save</button>
    <script type="text/javascript">
        var app = Sys.Application;
        app.add_init(function (sender, args) {
            // Program starts here. Gets the DOM elements for the respective IDs so things can be
            // created and wired-up.
            var graphContainer = $get('graphContainer');
            var saveButton = $get('saveButton');

            if (!mxClient.isBrowserSupported()) {
                // Displays an error message if the browser is not supported.
                mxUtils.error('Tarayıcı desteklenmiyor!', 200, false);
            }
            else {
                // Creates an instance of the graph control, passing the graphContainer element to the
                // mxGraph constructor. The $create function is part of ASP.NET. It can take an ID for
                // creating objects so the new instances can later be found using the $find function.
                var graphControl = $create(aspnet.GraphControl, null, null, null, graphContainer);

                // Saves graph by posting the XML to the generic handler SaveHandler.ashx. This code
                // only registers the event handler in the static button, which in turn invokes the
                // method to post the XML on the client control, passing the URL and param name.
                mxEvent.addListener(saveButton, 'click', function (evt) {
                    // Posts the XML representation for the graph model to the given URL under the
                    // given request parameter and prints the server response to the console.
                    var xml = encodeURIComponent(mxUtils.getXml(graphControl.encode()));
                    mxUtils.post('Save.ashx', 'xml=' + xml,
                        // Asynchronous callback for successfull requests. Depending on the application
                        // you may have to parse a custom response such as a new or modified graph.
                        function (req) {
                            mxLog.show();
                            mxLog.debug(req.getText());
                        }
                    );
                });

                // Reads the initial graph from a member variable in the page. The variable is an XML
                // string which is replaced on the server-side using the expression below. The string
                // is then parsed using mxUtils.parseXml on the client-side and the resulting DOM is
                // passed to the decode method for reading the graph into the current graph model.
                var doc = mxUtils.parseXml('<% = xml %>');
                console.log(doc.documentElement);
                graphControl.decode(doc.documentElement);
            }
        });
    </script>
    <div>
    </div>
</asp:Content>
