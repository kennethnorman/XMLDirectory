using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLDirectory;

namespace XMLDirectoryTest
{
    [TestClass]
    public class MainProgram
    {
        [TestMethod]
        public void EmptyArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.NotSet);
            Assert.AreEqual(ap.OutputFile, ArgumentProcessor.DEFAULT_OUTPUT_FILE);
        }
        [TestMethod]
        public void HelpArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { "-?" });
            Assert.AreEqual(ap.ShowHelp, true);
        }
        [TestMethod]
        public void CommandOnlyArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { "songlist" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongList);
            Assert.AreEqual(ap.OutputFile, ArgumentProcessor.DEFAULT_OUTPUT_FILE);
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, false);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "songinfo" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongInfo);
            Assert.AreEqual(ap.OutputFile, ArgumentProcessor.DEFAULT_OUTPUT_FILE);
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, false);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "blah" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.NotSet);
            Assert.AreEqual(ap.ShowHelp, true);
        }
        [TestMethod]
        public void CommandAndDirArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { "Songlist", "directory" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongList);
            Assert.AreEqual(ap.OutputFile, "directory");
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, false);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "SongInfo", "directory" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongInfo);
            Assert.AreEqual(ap.OutputFile, "directory");
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, false);
            Assert.AreEqual(ap.ShowHelp, false);
        }
        [TestMethod]
        public void CommandAndDirAndOptionArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { "SongList", "directory", "-obs" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongList);
            Assert.AreEqual(ap.OutputFile, "directory");
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, true);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "SONGINFO", "directory", "-UseObsfucatedPdfSuffix" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongInfo);
            Assert.AreEqual(ap.OutputFile, "directory");
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, true);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "SONGINFO", "directory", "-blah" });
            Assert.AreEqual(ap.ShowHelp, true);
        }
        [TestMethod]
        public void CommandAndOptionArgumentList()
        {
            ArgumentProcessor ap = new ArgumentProcessor(new string[] { "SONGList", "-Obs" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongList);
            Assert.AreEqual(ap.OutputFile, ArgumentProcessor.DEFAULT_OUTPUT_FILE);
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, true);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "songinfo", "-UseObsfucatedPdfSuffix" });
            Assert.AreEqual(ap.Command, ArgumentProcessor.XMLDirCommand.SongInfo);
            Assert.AreEqual(ap.OutputFile, ArgumentProcessor.DEFAULT_OUTPUT_FILE);
            Assert.AreEqual(ap.UseObsfucatedPdfSuffix, true);
            Assert.AreEqual(ap.ShowHelp, false);

            ap = new ArgumentProcessor(new string[] { "SONGINFO", "-blah" });
            Assert.AreEqual(ap.ShowHelp, true);
        }
    }
}
