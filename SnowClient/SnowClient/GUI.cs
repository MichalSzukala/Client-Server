using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SnowClient
{

    /// <summary>
    /// GUI of the application
    /// </summary>
    public partial class GUI : Form
    {
        
        private string fileContent;
        private string url = "http://localhost:56214/api/snow/";
        private Timer timer;
        private ServerRequest server;

        public GUI()
        {
            InitializeComponent();
            server = new ServerRequest();
            timer = new Timer();

        }


        /// <summary>
        /// Opens and reads the content of the file 
        /// </summary>
        private void readFileContent_Click(object sender, EventArgs e)
        {

            string path = PathToFile();
            if (path != string.Empty)
            {
                fileContent = ReadContentOfFile(path);

            }
        }


        /// <summary>
        /// Chooses txt file
        /// <returns>path to the open file</returns>
        /// </summary>
        private string PathToFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            string path = string.Empty;

            openFile.InitialDirectory = "c:\\";
            openFile.Filter = "txt files (*.txt)|*.txt";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;
            
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                    try
                    {
                        path = openFile.FileName;
                        buttonSend.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not read file from disk ", "Error!!");
                    }
            }
            return path;
        }

        /// <summary>
        /// Reads content of the file
        /// <param name="path">Path to the open file</param> 
        /// <returns>Content of the file</returns>
        /// </summary>
        private string ReadContentOfFile(String path)
        {
            string contentOfFile = string.Empty;
            try
            {
                contentOfFile = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read file from disk", "Error!!");
            }
            return contentOfFile;
        }


        /// <summary>
        /// Exits the program
        /// </summary>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to close program?", "Confirmation Window", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Sends request with data to the server and receives message back from the server.  It will display
        /// the data on the chart or it will inform user about problem
        /// </summary>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (fileContent != null)
            {
                chart.Series["BarChart"].Points.Clear();
                timer.Stop();

                server.SendPOST(url, fileContent);
                string response = server.ReadGet(url);
                if (IsJSON(response))
                {
                    //MessageBox.Show(response, "Server response");
                    ShowChart(response);
                    buttonUpdate.Enabled = true;
                }else
                {
                    MessageBox.Show(response, "Server response");
                }
            }
        }


        /// <summary>
        /// Shows the chart with received data from the server
        /// </summary>
        /// <param name="serverResponse">Data with server response</param>
        private void ShowChart(string serverResponse)
        {
            List<ChartBar> chartBarsList = JsonConvert.DeserializeObject<List<ChartBar>>(serverResponse);
            chart.Series["BarChart"].Points.Clear();
            foreach (ChartBar x in chartBarsList)
            {
                chart.Series["BarChart"].Points.AddXY(x.Color, x.Value);
            }
        }

        /// <summary>
        /// Validate if reveiced data type from the server is a valid JSON type
        /// </summary>
        /// <param name="jsonString">Received JSON string from the server</param>
        /// <returns>True is string is valid JSON type</returns>
        private bool IsJSON(string jsonString)
        {
            try
            {
                JToken.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Data from server are updated on the chart every 3 secondes
        /// </summary>
        public void Timer()
        {
            timer.Tick += new EventHandler(UpdateChart);
            timer.Interval = 3000; // in miliseconds
            timer.Start();
        }

        /// <summary>
        /// Activates automatic update of the data on the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            Timer();
            buttonUpdate.Enabled = false;
        }

        /// <summary>
        /// Updates the chart with date from the server
        /// </summary>
        private void UpdateChart(object sender, EventArgs e)
        {
            string response = server.ReadGet(url);
            ShowChart(response);
        }
    }
}
