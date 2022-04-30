using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace IndianArmyWeb.Infrastructure.Constants
{
    public class CustomConst
    {
        public static string GetUploadDirectory(string FileName)
        {
            var mimeType = MimeMapping.GetMimeMapping(FileName);
            string path = ServerRootConsts.MEDIA_ROOT;
            if (mimeType.Length > 0)
            {
                if (mimeType.ToString() == "image/jpeg" || mimeType.ToString() == "image/png" || mimeType.ToString() == "image/gif")
                {
                    path = path + "images/";
                }
                else if (mimeType.ToString() == "application/msword" || mimeType.ToString() == "application/pdf" || mimeType.ToString() == "application/vnd.ms-excel" || mimeType.ToString() == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || mimeType.ToString() == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    path = path + "documents/";
                }
                else if (mimeType.ToString() == "video/mp4" || mimeType.ToString() == "application/x-mpegURL" || mimeType.ToString() == "video/x-ms-wmv" || mimeType.ToString() == "video/quicktime" || mimeType.ToString() == "video/quicktime")
                {
                    path = path + "videos/";
                }
                else
                {
                    path = "InvalidFile";
                }
            }
            return path;
        }
        //public static string CheckFile(HttpPostedFileBase file)
        //{
        //    string ext = null;
        //    string msg1 = "";
        //    string fpath = ServerRootConsts.MEDIA_ROOT;
        //    var mimeType = MimeMapping.GetMimeMapping(file.FileName);
        //    string[] file_hexa_signature = { "25-50-44-46-2D-31-2E", "50-4B-03-04-14-00-06", "D0-CF-11-E0-A1-B1-1A", "47-49-46-38-39-61-20", "FF-D8-FF-E0-00-10-4A", "89-50-4E-47-0D-0A-1A" };
        //    try
        //    {
        //        ext = System.IO.Path.GetExtension(file.FileName).ToLower();
        //    }
        //    catch (Exception ex)
        //    {
        //        msg1 = ex.Message;
        //    }

        //    if (ext == null)
        //    {
        //        msg1 = "Please select pdf, word, excel,jpg, png or gif files only";
        //    }


        //    if (file != null && file.ContentLength > 0)
        //    {

        //        string str = Path.GetFileName(file.FileName);
        //        string path = Path.Combine(Server.MapPath("~/Docs"), Path.GetFileName(file.FileName));
        //        file.SaveAs(path);
        //        BinaryReader reader = new BinaryReader(new FileStream(Convert.ToString(path), FileMode.Open, FileAccess.Read, FileShare.None));
        //        reader.BaseStream.Position = 0x0;     // The offset you are reading the data from
        //        byte[] data = reader.ReadBytes(0x10); // Read 16 bytes into an array         
        //        string data_as_hex = BitConverter.ToString(data);
        //        reader.Close();

        //        // substring to select first 11 characters from hexadecimal array
        //        string my = data_as_hex.Substring(0, 20);
        //        string output = null;

        //        switch (my)
        //        {
        //            case "25-50-44-46-2D-31-2E":
        //                output = "pdf";
        //                break;
        //            case "50-4B-03-04-14-00-06":
        //                output = "word-excel-ppt";
        //                break;
        //            case "D0-CF-11-E0-A1-B1-1A":
        //                output = "doc-xls-ppt";
        //                break;
        //            case "47-49-46-38-39-61-20":
        //                output = "gif";
        //                break;
        //            case "FF-D8-FF-E0-00-10-4A":
        //                output = "jpeg-jpg";
        //                break;
        //            case "89-50-4E-47-0D-0A-1A":
        //                output = "png";
        //                break;
        //            case null:
        //                output = "notmatched";
        //                break;
        //        }

        //        ViewBag.Message = data_as_hex;
        //        ViewBag.signature = my;
        //        ViewBag.out_put = output;
        //    }
        //    return View();
        //}

    }
    public static class AppSettingsKeyConsts
    {
        public const string PUBLIC_ROOT_KEY = "PublicRoot";
        public const string MEDIA_ROOT_KEY = "MediaRoot";
        //public const string ErrorLogRootKey = "ErrorLogRoot";
    }
    public static class ServerRootConsts
    {
        public const string PUBLIC_ROOT = "~/writereaddata/";
        public const string MEDIA_ROOT = "~/writereaddata/media/";
        

        public const string PAGE_CONTENT_ROOT = "~/writereaddata/";
        public const string NEWS_CONTENT_ROOT = "~/writereaddata/";
    }
}