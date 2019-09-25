using CodeNationChalange.Enums;
using CodeNationChalange.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CodeNationChalange.Infra
{
    public static class WebService<T> where T : class
    {
        public static DefaultResponse Get()
        {
            return Request(ERequestType.GET);
        }

        public static DefaultResponse Post(byte[] file)
        {
            return Request(ERequestType.POST, file);
        }

        private static DefaultResponse Request(ERequestType requestType, byte[] file = null)
        {
            string responseString = "";

            string entityUrl = ParamConfig.URL_BASE + (file != null ? ParamConfig.PostUrl : ParamConfig.GetUrl) + ParamConfig.TOKEN;
            var httpWebRequest = WebRequest.Create(entityUrl);
            httpWebRequest.Method = requestType.ToString().ToUpper();
            
            if (file != null)
            {
                httpWebRequest.ContentType = "multipart/form-data";
                httpWebRequest.ContentLength = file.Length;
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(file, 0, file.Length);
                dataStream.Close();
            }
            else
                httpWebRequest.ContentType = "application/json";

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            try
            {
                using (var response = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            responseString = streamReader.ReadToEnd();
                            return new DefaultResponse()
                            {
                                status = EStatus.SUCCESS,
                                response = JsonConvert.DeserializeObject<ResponseObject>(responseString)
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is WebException webException)
                {
                    if (webException.Response != null)
                        using (var errorResponse = (HttpWebResponse)webException.Response)
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            return new DefaultResponse()
                            {
                                status = EStatus.ERROR,
                                response = JsonConvert.DeserializeObject<ResponseObject>(responseString)
                            };
                        }
                }
                throw e;
            }
        }

        //public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        //{
        //    log.Debug(string.Format("Uploading {0} to {1}", file, url));
        //    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        //    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        //    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        //    wr.ContentType = "multipart/form-data; boundary=" + boundary;
        //    wr.Method = "POST";
        //    wr.KeepAlive = true;
        //    wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        //    Stream rs = wr.GetRequestStream();

        //    string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        //    foreach (string key in nvc.Keys)
        //    {
        //        rs.Write(boundarybytes, 0, boundarybytes.Length);
        //        string formitem = string.Format(formdataTemplate, key, nvc[key]);
        //        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
        //        rs.Write(formitembytes, 0, formitembytes.Length);
        //    }
        //    rs.Write(boundarybytes, 0, boundarybytes.Length);

        //    string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        //    string header = string.Format(headerTemplate, paramName, file, contentType);
        //    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        //    rs.Write(headerbytes, 0, headerbytes.Length);

        //    FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        //    byte[] buffer = new byte[4096];
        //    int bytesRead = 0;
        //    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        //    {
        //        rs.Write(buffer, 0, bytesRead);
        //    }
        //    fileStream.Close();

        //    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        //    rs.Write(trailer, 0, trailer.Length);
        //    rs.Close();

        //    WebResponse wresp = null;
        //    try
        //    {
        //        wresp = wr.GetResponse();
        //        Stream stream2 = wresp.GetResponseStream();
        //        StreamReader reader2 = new StreamReader(stream2);
        //        log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error uploading file", ex);
        //        if (wresp != null)
        //        {
        //            wresp.Close();
        //            wresp = null;
        //        }
        //    }
        //    finally
        //    {
        //        wr = null;
        //    }
        //}

        //public static string UploadFilesToRemoteUrl(string url, string[] files, NameValueCollection formFields = null)
        //{
        //    string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.ContentType = "multipart/form-data; boundary=" +
        //                            boundary;
        //    request.Method = "POST";
        //    request.KeepAlive = true;

        //    Stream memStream = new System.IO.MemoryStream();

        //    var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
        //                                                            boundary + "\r\n");
        //    var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
        //                                                                boundary + "--");


        //    string formdataTemplate = "\r\n--" + boundary +
        //                                "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

        //    if (formFields != null)
        //    {
        //        foreach (string key in formFields.Keys)
        //        {
        //            string formitem = string.Format(formdataTemplate, key, formFields[key]);
        //            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
        //            memStream.Write(formitembytes, 0, formitembytes.Length);
        //        }
        //    }

        //    string headerTemplate =
        //        "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
        //        "Content-Type: application/octet-stream\r\n\r\n";

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        memStream.Write(boundarybytes, 0, boundarybytes.Length);
        //        var header = string.Format(headerTemplate, "uplTheFile", files[i]);
        //        var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

        //        memStream.Write(headerbytes, 0, headerbytes.Length);

        //        using (var fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read))
        //        {
        //            var buffer = new byte[1024];
        //            var bytesRead = 0;
        //            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                memStream.Write(buffer, 0, bytesRead);
        //            }
        //        }
        //    }

        //    memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
        //    request.ContentLength = memStream.Length;

        //    using (Stream requestStream = request.GetRequestStream())
        //    {
        //        memStream.Position = 0;
        //        byte[] tempBuffer = new byte[memStream.Length];
        //        memStream.Read(tempBuffer, 0, tempBuffer.Length);
        //        memStream.Close();
        //        requestStream.Write(tempBuffer, 0, tempBuffer.Length);
        //    }

        //    using (var response = request.GetResponse())
        //    {
        //        Stream stream2 = response.GetResponseStream();
        //        StreamReader reader2 = new StreamReader(stream2);
        //        return reader2.ReadToEnd();
        //    }
        //}
    }
}
