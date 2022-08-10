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

namespace Diyagram_Yönetim_Sistemi
{
    public partial class Uygulama : System.Web.UI.Page
    {
        protected string xml { get; set; }
        protected DrawioFile drawioFile = DrawioFile.Instance;
        protected string filePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack){
                DirectoryInfo rootInfo = new DirectoryInfo(@"D:\workspaces\github\Diagram-Management-System\Diyagram Yönetim Sistemi\Diagrams\");
                this.PopulateRacistTreeView(rootInfo, null);
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

        private void PopulateRacistTreeView(DirectoryInfo dirInfo, TreeNode treeNode){
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
                    if(file.Extension == ".drawio"){
                        TreeNode fileNode = new TreeNode{
                            Text = file.Name,
                            Value = file.FullName
                        };
                        directoryNode.ChildNodes.Add(fileNode);
                    }
                }
                PopulateRacistTreeView(directory, directoryNode);
            }
            TreeView.CollapseAll();
        }

        protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            printGraph(TreeView.SelectedNode.Value);
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "printGraph", "app.add_init(printGraph())", true);
            //var doc = mxUtils.ParseXml(xml);
        }

    }
}