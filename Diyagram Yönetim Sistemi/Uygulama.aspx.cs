using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.mxgraph;
using System.Text;
using System.IO;
using System.IO.Compression;
using zlib;

namespace Diyagram_Yönetim_Sistemi
{
    public partial class Uygulama : System.Web.UI.Page
    {
        public string filePath = @"D:\workspaces\visual_studio\Diyagram Yönetim Sistemi\Diyagram Yönetim Sistemi\Diagrams\d1Test.drawio";
        public string node = "mxfile/diagram";

        //Get the diagram node from xml file
        public string getNodeFromXml(string filePath, string node)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node_ = doc.SelectSingleNode(node);
            return node_.InnerText;
        }
        public static void DecompressData(byte[] inData, out byte[] outData)
        {
            using (MemoryStream outMemoryStream = new MemoryStream())
            using (ZOutputStream outZStream = new ZOutputStream(outMemoryStream))
            using (Stream inMemoryStream = new MemoryStream(inData))
            {
                CopyStream(inMemoryStream, outZStream);
                outZStream.finish();
                outData = outMemoryStream.ToArray();
            }
        }
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }

        protected string xml;

        protected void Page_Load(object sender, EventArgs e)
        {

            string data = getNodeFromXml(filePath, node);
            byte[] decoded = Convert.FromBase64String(data);
            using (MemoryStream resultStream = new MemoryStream(decoded))
            {
                using (DeflateStream compressionStream = new DeflateStream(resultStream, CompressionMode.Decompress))
                {
                    byte[] outBuffer = new byte[2048];   // need an estimate here
                    int length = compressionStream.Read(outBuffer, 0, outBuffer.Length);
                    Xml = Encoding.UTF8.GetString(outBuffer, 0, length);
                }

            }

         

            
            Xml = File.ReadAllText(@"D:\workspaces\visual_studio\Diyagram Yönetim Sistemi\Diyagram Yönetim Sistemi\Diagrams\d1Test.xml");
        }
        // Getter and setter for the XML variable.
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        public static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value));
        }

        public void metot (byte[] xmlBytes )
        {
            using (MemoryStream memStream = new MemoryStream(100))
            {
                // Write the first string to the stream.
                memStream.Write(xmlBytes, 0, xmlBytes.Length);

               
            }
        }
    }
}

/* solution with pako.js
 * data = atob(data);
 * data = pako.inflateRaw(Uint8Array.from(data, c => c.charCodeAt(0)), {to: 'string'});
 * data = decodeURIComponent(data);
*/

/* THIS PART IS THE OLD CODES THAT NEVER USEFUL
 * 
var asd = Encoding.UTF8.GetString(
    Convert.FromBase64String(
        getNodeFromXml(filePath, node)
    )
);

var decompressor = new GZipStream(memDecoded, CompressionMode.Decompress);
decompressor.CopyTo(memOut);

*/

/*
    // string str2 = Encoding.Default.GetString(result);
    //byte[] decode = Convert.FromBase64String(sifreliData);
    string str = Encoding.Default.GetString(decode);
    // string a = Encoding.Default.GetString(data);
    // string decodedString = Encoding.UTF8.GetString(data);



    //metot(data);
    MemoryStream compressed = GenerateStreamFromString(sifreliData);
    MemoryStream decompressed = new MemoryStream();
    DeflateStream deflateStream = new DeflateStream(compressed, CompressionMode.Decompress);

    try
    {
        deflateStream.CopyTo(decompressed);
    }
    catch(Exception ex)
    {
        string m = ex.Message;
    }
           
    Xml = decompressed.ToString();

    byte[] o;
    DecompressData(
        Convert.FromBase64String(
            getNodeFromXml(filePath, node)
            ), out o
        );
    Xml = o.ToString();
*/

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