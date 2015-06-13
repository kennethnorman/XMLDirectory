using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDirectory;

namespace XMLDirectoryTest
{
    [TestClass]
    public class SongInfoTest
    {
        const string DefaultTestFileName = "testinfo.xml";
        const string TestSrc = "..\\..\\Test\\";
        const string BackTo = "..\\..\\Bin\\Release";


        [TestMethod]
        public void InfoNoFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "NoFiles");
            SongInfo si = new SongInfo(DefaultTestFileName);
            si.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

        [TestMethod]
        public void InfoSomeFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "SomeFiles");
            SongInfo sl = new SongInfo(DefaultTestFileName);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

        [TestMethod]
        public void InfoObsSomeFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "SomeFilesObs");
            SongInfo sl = new SongInfo(DefaultTestFileName);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }
    }
}
