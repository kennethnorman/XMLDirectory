using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XMLDirectory
{
    public class ArgumentProcessor
    {
        public const string DEFAULT_OUTPUT_FILE = "XMLDirectory.xml";

        public enum XMLDirCommand
        {
            NotSet,
            SongList,
            SongInfo
        };

        public ArgumentProcessor(string[] args)
        {
            if (args.Length == 0)
            {
                m_showHelp = true;
            }
            else
            {
                ProcessFirstArgument(args);
                ProcessRemainingArguments(args);
            }
        }

        public XMLDirCommand Command
        {
            get { return m_command; }
        }
        private XMLDirCommand m_command;

        public String OutputFile
        {
            get { return m_outputFile; }
        }
        private String m_outputFile = DEFAULT_OUTPUT_FILE;

        public bool UseObsfucatedPdfSuffix
        {
            get { return m_useObsfucatedPdfSuffix; }
        }
        private bool m_useObsfucatedPdfSuffix = false;

        public bool ShowHelp
        {
            get { return m_showHelp; }
        }
        private bool m_showHelp = false;

        private void ProcessFirstArgument(string[] args)
        {
            if (args.Length >= 1)
            {
                switch (args[0].ToLower())
                {
                    case "songlist":
                        m_command = XMLDirCommand.SongList;
                        break;
                    case "songinfo":
                        m_command = XMLDirCommand.SongInfo;
                        break;
                    default:
                        m_showHelp = true;
                        break;
                }
            }
        }

        private void ProcessRemainingArguments(string[] args)
        {
            for (int i = 1; args.Length > i; i++)
            {
                ProcessOption(args, i);
            }
        }

        private void ProcessOption(string[] args, int index)
        {
            switch (args[index].ToLower())
            {
                case "-useobsfucatedpdfsuffix":
                case "-obs":
                    m_useObsfucatedPdfSuffix = true;
                    break;
                default:
                    if (index == 1)
                    {
                        if (!IsOption(args, index))
                        {
                            m_outputFile = args[index];
                        }
                        else
                        {
                            m_showHelp = true;
                        }
                    }
                    else
                    {
                        m_showHelp = true;
                    }
                    break;
            }
        }

        private static bool IsOption(string[] args, int index)
        {
            return args[index][0] == '-';
        }
    }
}