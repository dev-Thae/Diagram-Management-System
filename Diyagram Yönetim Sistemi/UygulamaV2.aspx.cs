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
using System.Windows;


namespace Diyagram_Yönetim_Sistemi
{

    public partial class UygulamaV2 : System.Web.UI.Page
    {
        protected string xml { get; set; }
        protected DrawioFile drawioFile = DrawioFile.Instance;
        protected string filePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack){
                DirectoryInfo rootInfo = new DirectoryInfo(@"D:\workspaces\github\Diagram-Management-System\Diyagram Yönetim Sistemi\Diagrams\");
                this.PopulateRacistTreeView(rootInfo, null, ".drawio", ".dygrm");
            }
        }

        protected void printGraph(string filePath)
        {
            drawioFile.setFilePath(filePath);
            drawioFile.getFileInfo();
            xml = drawioFile.getXml();
        }
        
        private void PopulateTreeView(DirectoryInfo dirInfo, TreeNode treeNode){
            foreach (DirectoryInfo directory in dirInfo.GetDirectories()){
                TreeNode directoryNode = new TreeNode{
                    Text = directory.Name,
                    Value = directory.FullName
                };

                if (treeNode == null){
                    //If Root Node, add to TreeView.
                    TreeView.Nodes.Add(directoryNode);
                }else{
                    //If Child Node, add to Parent Node.
                    treeNode.ChildNodes.Add(directoryNode);
                }

                //Get all files in the Directory.
                foreach(FileInfo file in directory.GetFiles()){
                    //Add each file as Child Node.
                    TreeNode fileNode = new TreeNode{
                        Text = file.Name,
                        Value = file.FullName,
                        //Target = "_blank",
                        //NavigateUrl = (new Uri(Server.MapPath("~/"))).MakeRelativeUri(new Uri(file.FullName)).ToString()
                    };
                    directoryNode.ChildNodes.Add(fileNode);
                }

                PopulateTreeView(directory, directoryNode);
            }
            TreeView.CollapseAll();
        }

        private void PopulateRacistTreeView(DirectoryInfo dirInfo, TreeNode treeNode, params string[] races){
            foreach (DirectoryInfo directory in dirInfo.GetDirectories()){
                TreeNode directoryNode = new TreeNode{
                    Text = directory.Name,
                    Value = directory.FullName
                };

                if (treeNode == null){
                    //If Root Node, add to TreeView.
                    TreeView.Nodes.Add(directoryNode);
                }else{
                    //If Child Node, add to Parent Node.
                    treeNode.ChildNodes.Add(directoryNode);
                }

                //Get all files in the Directory.
                foreach (FileInfo file in directory.GetFiles()){
                    if(races.Contains(file.Extension)){
                        TreeNode fileNode = new TreeNode{
                            Text = file.Name,
                            Value = file.FullName
                        };
                        directoryNode.ChildNodes.Add(fileNode);
                    }
                }
                PopulateRacistTreeView(directory, directoryNode, races);
            }
            TreeView.CollapseAll();
        }


        private string inflateDecodeBase64StringData(string data)
        {
            byte[] outBuffer = new byte[0x100000];   // need an estimate here
            int length;
            using (MemoryStream resultStream = new MemoryStream(Convert.FromBase64String(data)))
            {
                using (DeflateStream compressionStream = new DeflateStream(resultStream, CompressionMode.Decompress))
                {
                    length = compressionStream.Read(outBuffer, 0, outBuffer.Length);
                }
            }
            return HttpUtility.UrlDecode(Encoding.UTF8.GetString(outBuffer, 0, length));
        }

        protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (TreeView.SelectedNode.Value.GetLast(6) == ".dygrm") //TreeView.SelectedNode.Value.Substring(TreeView.SelectedNode.Value.Length - 6)
            {
                xml = inflateDecodeBase64StringData(File.ReadAllText(TreeView.SelectedNode.Value));
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "printGraph", "show()", true);
                return;
            }
            printGraph(TreeView.SelectedNode.Value);
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "printGraph", "show()", true);
        }

        // Parses the mxGraph XML file format
        /*
        private void read(mxGraph graph, string filePath)
        {
            drawioFile.setFilePath(filePath);
            drawioFile.getFileInfo();
            var doc = mxUtils.ParseXml(drawioFile.getXml());
            var root = req.getDocumentElement();
            var dec = new mxCodec(root.ownerDocument);

            dec.decode(root, graph.getModel());
        }
        */
        /*
        private void printGraph(string filePath, bool none)
        {
            drawioFile.setFilePath(filePath);
            drawioFile.getFileInfo();
            mxGraph graph = new mxGraph(graphContainer);
            XmlDocument doc = mxUtils.ParseXml(drawioFile.getXml());
            var enc = new mxCodec(doc.DocumentElement.OwnerDocument);
            enc.Decode(doc, graph.Model);
        }
        */
    }
}