using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snow.Model;

namespace UnitTestSnowServer.Model
{
    [TestClass]
    public class ParsingDataTests
    {
        [TestMethod]
        public void CleaningPOSTData_NoData_Exception()
        {

        }

        [TestMethod]
        public void IsValid_NoData_False()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = { };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsFalse(isValidToJson);
        }

        [TestMethod]
        public void IsValid_RightInput_True()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = {"aa", "aa", "4", "bb", "bb", "8" };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsTrue(isValidToJson);

        }

        [TestMethod]
        public void IsValid_OneElementTooMuch_False()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = { "aa", "aa", "4", "bb", "bb", "8", "cc" };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsFalse(isValidToJson);
        }

        [TestMethod]
        public void IsValid_TwoElementsTooMuch_False()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = { "aa", "aa", "4", "bb", "bb", "8", "cc", "cc" };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsFalse(isValidToJson);
        }

        [TestMethod]
        public void IsValid_StringIsNumber_False()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = { "aa", "aa", "4", "bb", "bb", "8", "cc", "3", "4" };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsFalse(isValidToJson);
        }

        [TestMethod]
        public void IsValid_NumberIsString_False()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string[] singleElemenArray = { "aa", "aa", "4", "bb", "bb", "5", "cc", "cc", "wrong" };

            bool isValidToJson = parsing.IsValid(singleElemenArray);

            Assert.IsFalse(isValidToJson);
        }

        [TestMethod]
        public void SplittingJsonString_HashSignsAdded_StringNoHash()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string inputString = "#sd#dd#23#grr#sdada#3";
            string validString = "sddd23grrsdada3";

            String[] ArrayNoHash = parsing.SplittingJsonString(inputString);
            string outputString = string.Join("", ArrayNoHash);

            Assert.AreEqual(validString, outputString);
        }

        [TestMethod]
        public void SplittingJsonString_ColonsAdded_StringNoColon()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string inputString = ":sd:dd:23:grr:sdada:3";
            string validString = "sddd23grrsdada3";

            String[] ArrayNoColon = parsing.SplittingJsonString(inputString);
            string outputString = string.Join("", ArrayNoColon);

            Assert.AreEqual(validString, outputString);
        }

        [TestMethod]
        public void SplittingJsonString_WhiteSpacesAdded_StringNoVariations()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string inputString = ":sd:dd    :2#3:grr    :sd##ada:3#";
            string validString = "sddd23grrsdada3";

            String[] ArrayNoVariation = parsing.SplittingJsonString(inputString);
            string outputString = string.Join("", ArrayNoVariation);

            Assert.AreEqual(validString, outputString);
        }

        [TestMethod]
        public void SplittingJsonString_DifferentSignsAdded_StringNoDifferentSigns()
        {
            List<string> testList = new List<string>();
            ParsingData parsing = new ParsingData(testList);
            string inputString = ":sd:dd    :2#3:grr    :sd##ada:3#";
            string validString = "sddd23grrsdada3";

            String[] ArrayNoVariation = parsing.SplittingJsonString(inputString);
            string outputString = string.Join("", ArrayNoVariation);

            Assert.AreEqual(validString, outputString);
        }
    }
}
