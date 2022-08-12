using System;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyagram_Yönetim_Sistemi
{
    /* Bu sınıf drawio dosya verilerini tutan tek objeli bir sınıftır.
     * İlk olarak objemizi oluşturalım => DrawioFile drawioDosyası = DrawioFile.Instance;
     * Oluşturduğumuz nesneye dosya dizini cümlesini gönderelim => drawioDosyası.setFilePath(dosyaDizini);
     * Son olarak çözümlenmiş xml verisini nesneden getirelim => xml = drawioFile.getXml();
     */

    /* This is a sigleton class that hold drawio file's data.
     * First, let construct our object => DrawioFile drawioFile = DrawioFile.Instance;
     * Set file path of our object => drawioFile.setFilePath(filePath);
     * Lastly, return the xml data from object => xml = drawioFile.getXml();
     */

    static class Constants{
        public const int defaultBufferSize = 0x100000;
        public const string defaultNode = "mxfile/diagram";
        public const short approximateCompressionMultiplier = 50;
    }

    public class DrawioFile{
        protected string filePath { get; set; }
        protected string fileName { get; set; }
        protected string node { get; set; }
        protected long bufferSize { get; set; }
        protected long fileSize { get; set; }
        protected string xml { get; set; }
        private DrawioFile(){
            node = Constants.defaultNode;
            bufferSize = Constants.defaultBufferSize;
            fileSize = Convert.ToInt64(Constants.defaultBufferSize / 10);
            filePath = null;
            fileName = null;
            xml = null;
        }
        private static DrawioFile instance = null;
        public static DrawioFile Instance{
            get{
                if(instance == null)
                    instance = new DrawioFile();
                return instance;
            }
        }

        public void setFilePath(string path) { filePath = path; }
        public string getFilePath() { return filePath; }

        public void setNode(string node) { this.node = node; }
        public string getNode() { return node; }

        public string getFileName() { return fileName; }
        public string getXml() { return xml; }

        public void getFileInfo(){
            try{
                if(filePath == null)
                    throw(new Exception("File path is null."));
                FileInfo fi = new FileInfo(filePath);
                fileName = fi.Name; //Path.GetFileName(filePath);
                fileSize = fi.Length;
                bufferSize = fileSize * Constants.approximateCompressionMultiplier;
                xml = HttpUtility.UrlDecode(
                    inflateByteArrayData(
                        decodeData(
                            getNodeFromXml())));
            }
            catch(Exception e){
                Console.WriteLine($"getFileInfo exception: {e.Message}");
            }
        }

        //Get the node from xml file
        private string getNodeFromXml(){
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node_ = doc.SelectSingleNode(node);
            return node_.InnerText;
        }

        private byte[] decodeData(string data){
            return Convert.FromBase64String(data);
        }

        private string inflateByteArrayData(byte[] data){
            byte[] outBuffer = new byte[bufferSize];   // need an estimate here
            int length;
            using (MemoryStream resultStream = new MemoryStream(data)){
                using (DeflateStream compressionStream = new DeflateStream(resultStream, CompressionMode.Decompress)){
                    length = compressionStream.Read(outBuffer, 0, outBuffer.Length);
                }
            }
            return System.Text.Encoding.UTF8.GetString(outBuffer, 0, length);
        }
    }

}