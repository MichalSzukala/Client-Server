using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SnowClient
{
    class ServerRequest
    {
        /// <summary>
        /// Sends data to the server with POST
        /// </summary>
        /// <param name="url">Url of the server API</param>
        /// <param name="data">Data send to the server</param>
        public void SendPOST(String url, string data)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] bytes = encoding.GetBytes(jsonData);
            request.ContentLength = bytes.Length;
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    // Send the data.
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Server id down try later", "Error!!");
            }
        }

        /// <summary>
        /// Receives data back from the server
        /// </summary>
        /// <param name="url">Url of the server API</param>
        /// <returns>Parsed data from the server or error message if parsing was not possible</returns>
        public string ReadGet(String url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex)
            {
                return "Error while parsing";
            }
        }




    }
}
