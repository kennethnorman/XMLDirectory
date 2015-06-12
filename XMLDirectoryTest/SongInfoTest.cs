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
        [TestMethod]
        public void InfoNoFiles()
        {
            Directory.SetCurrentDirectory("NoFiles");
            SongInfo si = new SongInfo(DefaultTestFileName);
            si.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

        [TestMethod]
        public void InfoSomeFiles()
        {
            Directory.SetCurrentDirectory("SomeFiles");
            SongInfo sl = new SongInfo(DefaultTestFileName);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

        [TestMethod]
        public void InfoObsSomeFiles()
        {
            Directory.SetCurrentDirectory("SomeFilesObs");
            SongInfo sl = new SongInfo(DefaultTestFileName);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }
    }
}
