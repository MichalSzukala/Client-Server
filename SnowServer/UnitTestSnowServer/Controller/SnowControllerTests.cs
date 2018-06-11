using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snow.Controller;
using Snow.DB;
using System;
using System.Collections.Generic;

namespace UnitTestSnowServer.Controller
{
    [TestClass]
    public class SnowControllerTests
    {
        [TestMethod]
        public void GetTest_WrongFormatData_Exception()
        {
            SnowController controler = new SnowController();
            string ValidJsonString = "#A:RED:5:4";

            DB.ReceivedFromClient.Add(ValidJsonString);
            try
            {   
                controler.Get();
                Assert.Fail(); 
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void GetTest_NoData_Exception()
        {
            SnowController controler = new SnowController();
            string ValidJsonString = "";

            DB.ReceivedFromClient.Add(ValidJsonString);

            try
            {
                controler.Get();
                Assert.Fail(); 
            }
            catch (Exception) { }
        }
    }
}