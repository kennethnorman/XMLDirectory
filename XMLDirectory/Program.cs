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
        static void Main(string[] args)
        {
            System.Console.WriteLine("XMLDirectory\n");

            String dirPath = ".";
            String outputFile = "XMLDirectory.xml";

            if (args.Length == 1)
            {
                outputFile = args[0];
            }


            String extension = ".pdf";
            String pattern = "*" + extension;
            String ignore = "_private";
            
            // open an output file
            StreamWriter OutputFile = new StreamWriter(@outputFile, false);
            try
            {
                // Write the XML Header
                OutputFile.WriteLine("<?xml version=\"1.0\"?>");
                OutputFile.WriteLine("<SongList>");

                var files = from file in Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                            select new
                            {
                                File = file,
                            };

                List<KeyValuePair<String, String>> dnList = new List<KeyValuePair<String, String>>();
                foreach (var f in files)
                {
                    String artist, track;

                    string[] artisttrack = f.File.Split('\\');
                    if (artisttrack.Length == 3)
                    {
                        artist = artisttrack[1];
                        track = artisttrack[2];

                        if (track.Contains(ignore))
                        {
                            continue;
                        }

                        track = track.Replace("_", " ");
                        track = track.Replace(artist, "");
                        track = track.Replace(extension, "");
                        track = track.Trim();

                        artist = artist.Replace("&&", "&amp;");
                        artist = artist.Replace("\"", "&quot;");
                        artist = artist.Replace("<", "&lt;");
                        artist = artist.Replace(">", "&gt;");

                        track = track.Replace("&", "&amp;");
                        track = track.Replace("\"", "&quot;");
                        track = track.Replace("<", "&lt;");
                        track = track.Replace(">", "&gt;");

                        dnList.Add(new KeyValuePair<String, String>(artist, track));
                        Console.WriteLine("{0}", f.File);
                    }
                }
                Console.WriteLine("{0} files found.", files.Count().ToString());

                foreach (var song in dnList)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("    <Song>\r\n");
                    sb.Append("        <Artist>" + song.Key + "</Artist>\r\n");
                    sb.Append("        <Track>" + song.Value + "</Track>\r\n");
                    sb.Append("    </Song>\r\n");
                    OutputFile.Write(sb);
                }
                OutputFile.WriteLine("</SongList>");

            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            finally
            {
                OutputFile.Close();
            }

            System.Console.WriteLine("Finished");
        }
    }
}
