using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Snow.Model
{   /// <summary>
    /// Handles operations connected with parsing data received from user 
    /// </summary>
    public class ParsingData
    {
        private List<string> receivedFromClient;
        private List<ChartBar> chartBarsList;


        /// <summary>
        /// Receives access to the DB 
        /// </summary>
        /// <param name="receivedFromClient">DB of the server</param>
        public ParsingData(List<string> receivedFromClient)
        {
            this.receivedFromClient = receivedFromClient;
        }

        /// <summary>
        /// Splits string based on delimiter
        /// </summary>
        /// <param name="jsonString">Input string received from the user</param>
        /// <returns>Array of elements without delimiters</returns>
        public String[] SplittingJsonString(string jsonString)
        {
            String[] singleElementsArray = jsonString.Split(new Char[] { '\n', '#', ':', ':', '\r', '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return singleElementsArray;
        }

        /// <summary>
        /// Creates objects of ChartBars from the input string from user
        /// </summary>
        /// <returns>List of all the objects received from the user input string</returns>
        private List<ChartBar> CleaningPOSTData()
        {
            string jsonString = receivedFromClient[0];
            String[] singleElementsArray = SplittingJsonString(jsonString);
            chartBarsList = new List<ChartBar>();

            int i = 0;
            if (IsValid(singleElementsArray))
            {
                while (i < singleElementsArray.Length)
                {
                    string name = singleElementsArray[i];
                    i++;
                    string color = singleElementsArray[i];
                    i++;
                    int value = Int32.Parse(singleElementsArray[i]);
                    i++;
                    chartBarsList.Add(new ChartBar(name, color, value));
                }
            }
            else
            {
                throw new Exception();
            }
            
            return chartBarsList;
        }

        /// <summary>
        /// Checks in data received from user are valid
        /// </summary>
        /// <param name="singleElement">Array of splited elements from the user</param>
        /// <returns>True if data is valid</returns>
        public bool IsValid(string[] singleElement)
        {
            bool flag = true;
            int number = 0;
            int i = 0;
            if (singleElement.Length != 0)
            {
                while (i < singleElement.Length)
                {
                    if (i < singleElement.Length && !(singleElement[i].All(Char.IsLetter)))
                    {
                        flag = false;
                    }

                    i++;
                    if (i < singleElement.Length && !(singleElement[i].All(Char.IsLetter)))
                    {
                        flag = false;
                    }

                    i++;
                    if (i < singleElement.Length && !(Int32.TryParse(singleElement[i], out number)))
                    {
                        flag = false;
                    }

                    i++;

                    if ((singleElement.Length % 3 != 0))
                    {
                        flag = false;
                    }
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// Creates JSON string which will be delivered back to the user
        /// </summary>
        /// <returns>Json string of parsed data </returns>
        public string CreateJsonChartBarsString()
        {
            CleaningPOSTData();
            ShuffleList();
            string jsonChartBarsString = JsonConvert.SerializeObject(chartBarsList, Formatting.Indented);
            return jsonChartBarsString;
        }

        /// <summary>
        /// Shuffle the data of elements reveived from user 
        /// </summary>
        private void ShuffleList()
        {
            int size = chartBarsList.Count;
            Random random = new Random();

            while (size > 0)
            {
                size--;
                int randomNumber = random.Next(size + 1);
                ChartBar temp = chartBarsList[randomNumber];
                chartBarsList[randomNumber] = chartBarsList[size];
                chartBarsList[size] = temp;
                //    System.Diagnostics.Debug.WriteLine(randomNumber);
            }
        }
    }
}