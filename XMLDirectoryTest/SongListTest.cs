using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDirectory;

namespace XMLDirectoryTest
{
    [TestClass]
    public class SongListTest
    {
        const string DefaultTestFileName = "testlist.xml";
        [TestMethod]
        public void ListNoFiles()
        {
            Directory.SetCurrentDirectory("NoFiles");
            SongList sl = new SongList(DefaultTestFileName, false);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

        [TestMethod]
        public void ListSomeFiles()
        {
            Directory.SetCurrentDirectory("SomeFiles");
            SongList sl = new SongList(DefaultTestFileName, false);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader(DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

        [TestMethod]
        public void ListObsNoFiles()
        {
            Directory.SetCurrentDirectory("NoFiles");
            SongList sl = new SongList("obs" + DefaultTestFileName, true);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader("obs" + DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

        [TestMethod]
        public void ListObsSomeFiles()
        {
            Directory.SetCurrentDirectory("SomeFilesObs");
            SongList sl = new SongList("obs" + DefaultTestFileName, true);
            sl.Close();

            StreamReader ExpectedResult = new StreamReader("expected_" + "obs" + DefaultTestFileName);
            StreamReader ActualResult = new StreamReader("obs" + DefaultTestFileName);

            CommonTest.CompareResult(ExpectedResult, ActualResult);

            Directory.SetCurrentDirectory("..");
        }

    }
}
