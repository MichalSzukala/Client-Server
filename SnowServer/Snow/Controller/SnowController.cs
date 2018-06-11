using Snow.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Snow.Controller
{
    public class SnowController : ApiController
    {
        private ParsingData parsingData;


        public SnowController()
        {
            parsingData = new ParsingData(DB.DB.ReceivedFromClient);
        }

        /// <summary>
        /// User perform GET request and receives parsed data which were delivered to server by user
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                if (DB.DB.ReceivedFromClient[0] != null)
                {
                    string jsonChartBarsString = parsingData.CreateJsonChartBarsString();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(jsonChartBarsString, Encoding.UTF8, "application/json");
                    return response;
                }else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Need to POST some data");
                }

            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Provided data is wrong format");
            }
        }
        
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "Snow Server";
        }

        /// <summary>
        /// Data received from user are saved in DataBase
        /// </summary>
        /// <param name="value">Input data from user</param>
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            DB.DB.ReceivedFromClient.Clear();
            DB.DB.ReceivedFromClient.Add(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            DB.DB.ReceivedFromClient.Clear();
            DB.DB.ReceivedFromClient.Add(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            DB.DB.ReceivedFromClient.Clear();
        }
    }
}