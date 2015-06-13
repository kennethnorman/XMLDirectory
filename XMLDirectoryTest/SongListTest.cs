using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDirectory;

namespace XMLDirectoryTest
{
    [TestClass]
    public class SongListTest
    {
        const string TestSrc = "..\\..\\Test\\";
        const string BackTo = "..\\..\\Bin\\Release";
        
        const string DefaultTestFileName = "testlist.xml";
        [TestMethod]
        public void ListNoFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "NoFiles");
            SongList sl = new SongList(DefaultTestFileName, false);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

        [TestMethod]
        public void ListSomeFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "SomeFiles");
            SongList sl = new SongList(DefaultTestFileName, false);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

        [TestMethod]
        public void ListObsNoFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "NoFiles");
            SongList sl = new SongList("obs" + DefaultTestFileName, true);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader("obs" + DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

        [TestMethod]
        public void ListObsSomeFiles()
        {
            Directory.SetCurrentDirectory(TestSrc + "SomeFilesObs");
            SongList sl = new SongList("obs" + DefaultTestFileName, true);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + "obs" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader("obs" + DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory(BackTo);
        }

    }
}
