<%@ WebHandler Language="C#" Class="Save" %>

using System;
using System.Web;
using System.IO;
using System.IO.Compression;

public class Save : IHttpHandler
{
    public string fileExtension = ".dygrm";
    public string savePath = @"../../../Diagrams/";
    public void ProcessRequest (HttpContext context)
    {
        // Response is always returned as text/plain
        context.Response.ContentType = "text/plain";
        string xml = HttpUtility.UrlDecode(context.Request.Params["xml"]);

        if (xml != null && xml.Length > 0)
        {
            context.Response.Write("Request received: " + xml);
        }
        else
        {
            context.Response.Write("Empty or missing request parameter.");
        }

        string filename = context.Request.Params["filename"];
        string doc = Convert.ToBase64String(
            Compress(
                System.Text.Encoding.UTF8.GetBytes(
                    context.Request.Params["xml"]
                )
            )
        );

        saveFileOnServer(filename, doc, context);

    }

    public bool IsReusable
    {
        // Return false in case your Managed Handler cannot be reused for another request.
        // Usually this would be false in case you have some state information preserved per request.
        get { return true; }
    }

    public static byte[] Compress(byte[] data)
    {
        MemoryStream output = new MemoryStream();
        using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
        {
            dstream.Write(data, 0, data.Length);
        }
        return output.ToArray();
    }

    public void saveFileOnServer(string filename, string doc, HttpContext context)
    {
        StreamWriter streamWriter = new StreamWriter(new FileStream(
            context.Request.MapPath(savePath + filename + fileExtension),
            FileMode.Create,
            FileAccess.Write));
        streamWriter.Write(doc);
        streamWriter.Close();
    }
}
