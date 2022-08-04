using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.mxgraph;
using System.IO;

namespace Diyagram_Yönetim_Sistemi
{
    public partial class Uygulama : System.Web.UI.Page
    {
        public string filePath = "D:\\workspaces\\visual_studio\\Diyagram Yönetim Sistemi\\Diyagram Yönetim Sistemi\\Diagrams\\d1Test.drawio";
        public string node = "mxfile/diagram";
        mxGraph graph = new mxGraph();

        //Get the diagram node from xml file
        public string getNodeFromXml(string filePath, string node)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node_ = doc.SelectSingleNode(node);
            return node_.InnerText;
        }

        protected string xml;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the code from a drawio file
            //string code = getNodeFromXml(filePath, node);
            MemoryStream memOut = new MemoryStream();

            byte[] b64 = new byte[4160];
            b64 = Convert.FromBase64String(
                getNodeFromXml(filePath, node)
            );

            MemoryStream memDecoded = new MemoryStream(
                b64
            );

            var decompressor = new System.IO.Compression.DeflateStream(memDecoded, System.IO.Compression.CompressionMode.Decompress);
            decompressor.CopyTo(memOut);

            Xml= memOut.ToString();


            Xml = File.ReadAllText(@"D:\workspaces\visual_studio\Diyagram Yönetim Sistemi\Diyagram Yönetim Sistemi\Diagrams\d1Test.xml");


            /*
            XmlDocument doc = mxUtils.ParseXml(System.IO.File.ReadAllText(@"D:\workspaces\visual_studio\Diyagram Yönetim Sistemi\Diyagram Yönetim Sistemi\Diagrams\d1Test.xml")); //this should be mx xml
            mxCodec codec = new mxCodec(doc);
            codec.Decode(doc.DocumentElement, graph.Model);
            Xml = mxUtils.GetXml(doc.DocumentElement);
            */

            /*
            // Creates an instance of a graph to add vertices and edges. The instance can
            // then be used to create the corresponding XML using a codec. Note that this
            // is only required if a graph is programmatically created. If the XML for the
            // graph is already at hand, it can be sent directly here.
            mxGraph graph = new mxGraph();
            Object parent = graph.GetDefaultParent();

            // Adds vertices and edges to the graph.
            graph.Model.BeginUpdate();
            try
            {
                Object v1 = graph.InsertVertex(parent, null, "Hello,", 20, 20, 80, 30);
                Object v2 = graph.InsertVertex(parent, null, "World!", 200, 150, 80, 30);
                Object e1 = graph.InsertEdge(parent, null, "Edge", v1, v2);
            }
            finally
            {
                graph.Model.EndUpdate();
            }
            
            // Encodes the model into XML and passes the resulting XML string into a page
            // variable, so it can be read when the page is rendered on the server. Note
            // that the page instance is destroyed after the page was sent to the client.
            mxCodec codec = new mxCodec();
            Xml = mxUtils.GetXml(codec.Encode(graph.Model));
            */
        }

        // Getter and setter for the XML variable.
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }
    }
}