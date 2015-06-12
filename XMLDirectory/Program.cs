using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XMLDirectory
{
    class Program
    {
        public const string VERSION = "1.1";

        static void Main(string[] args)
        {
            System.Console.WriteLine("XMLDirectory, v"+VERSION+"\n");

            ArgumentProcessor ap = new ArgumentProcessor(args);
            ArgumentProcessor.XMLDirCommand command = ap.Command;
            String outputFile = ap.OutputFile;

            if (ap.ShowHelp)
            {
                ShowHelp();
                return;
            }

            try
            {
                SongBase sb = null;
                if (command == ArgumentProcessor.XMLDirCommand.SongList)
                {
                    SongList sl = new SongList(outputFile, ap.UseObsfucatedPdfSuffix);
                    sl.Close();
                    sb = sl;
                }
                else if (command == ArgumentProcessor.XMLDirCommand.SongInfo)
                {
                    SongInfo si = new SongInfo(outputFile);
                    si.Close();
                    sb= si;
                }
                if (sb != null)
                {
                    foreach (string file in sb.FilesProcessed)
                    {
                        Console.WriteLine("{0}", file);
                    }
                    Console.WriteLine("{0} files found.", sb.FilesProcessedCount.ToString());
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

            System.Console.WriteLine("Finished");
        }

        static void ShowHelp()
        {
            System.Console.WriteLine("Syntax: xmldirectory command [outputdirectory options]");
            System.Console.WriteLine("where command is:  songlist, songinfo");
            System.Console.WriteLine("the default outputdirectory is: " + ArgumentProcessor.DEFAULT_OUTPUT_FILE);
            System.Console.WriteLine("valid options are:  -useobsfucatedpdfsuffix (or -obs)");
        }
    }
}
