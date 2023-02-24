using System;
using System.Data;
using GenericParsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.BinaryUnitTests
{
    [TestClass]
    public class ParsingDataTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            DataSet dsResult;

            // Using an XML Config file. 
            using (GenericParserAdapter parser = new GenericParserAdapter(@"C:\Users\tuckchr01\Documents\myne\animals\core\Core.BinaryUnitTests\bin\Debug\personality\learn.aeon"))
            {
                parser.Load(@"C:\Users\tuckchr01\Documents\myne\animals\core\Core.BinaryUnitTests\bin\Debug\personality\learn.aeon");
                dsResult = parser.GetDataSet();
            }

            // Or... programmatically setting up the parser for TSV. 
            string strID, strName, strStatus;
            using (GenericParser parser = new GenericParser())
            {
                parser.SetDataSource("MyData.txt");

                parser.ColumnDelimiter = "\t".ToCharArray()[0];
                parser.FirstRowHasHeader = true;
                parser.SkipStartingDataRows = 10;
                parser.MaxBufferSize = 4096;
                parser.MaxRows = 500;
                parser.TextQualifier = '\"';

                while (parser.Read())
                {
                    strID = parser["ID"];
                    strName = parser["Name"];
                    strStatus = parser["Status"];

                    // Your code here ...
                }
            }

            // Or... programmatically setting up the parser for Fixed-width. 
            using (GenericParser parser = new GenericParser())
            {
                parser.SetDataSource("MyData.txt");

                parser.ColumnWidths = new int[4] { 10, 10, 10, 10 };
                parser.SkipStartingDataRows = 10;
                parser.MaxRows = 500;

                while (parser.Read())
                {
                    strID = parser["ID"];
                    strName = parser["Name"];
                    strStatus = parser["Status"];

                    // Your code here ...
                }
            }
        }
    }
}
