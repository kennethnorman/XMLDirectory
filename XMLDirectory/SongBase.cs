using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XMLDirectory
{
    public class SongBase
    {
        protected String dirPath = ".";
        protected String ignore = "_private";

        public SongBase(string outputFileName)
        {
            OutputFile = new StreamWriter(@outputFileName, false);
            OutputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        }

        public virtual void Close()
        {
            OutputFile.Close();
        }

        public int FilesProcessedCount
        {
            get { return m_filesProcessed.Count; }
        }

        public List<string> FilesProcessed
        {
            get { return m_filesProcessed; }
            protected set { m_filesProcessed.AddRange(value); }
        }
        List<string> m_filesProcessed = new List<string>();

        protected StreamWriter OutputFile = null;
    }
}

