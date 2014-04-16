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

            String command = "";
            String outputFile = "XMLDirectory.xml";

            if (args.Length >= 1)
            {
                command = args[0];
            }

            if (args.Length >= 2)
            {
                outputFile = args[1];
            }

            command= command.ToLower();

            // open an output file
            StreamWriter OutputFile = null;
            try
            {
                OutputFile = new StreamWriter(@outputFile, false);
                // Write the XML Header
                OutputFile.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                
                switch (command)
                {
                    case "songlist":
                        {
                            SongList(ref OutputFile);
                        }
                        break;
                    case "songinfo":
                        {
                            SongInfo(ref OutputFile);
                        }
                        break;
                    default:
                        break;
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
            finally
            {
                OutputFile.Close();
            }

            System.Console.WriteLine("Finished");
        }

        static void SongList(ref StreamWriter OutputFile)
        {
            String dirPath = ".";
            String extension = ".pdf";
            String pattern = "*" + extension;
            String ignore = "_private";

            OutputFile.WriteLine("<SongList>");

            var files = from file in Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };

            List<KeyValuePair<String, String[]>> dnList = new List<KeyValuePair<String, String[]>>();
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

                    string filename = track;

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

                    filename = filename.Replace("&", "&amp;");

                    string[] fileData = new string[2];
                    fileData[0]= track;
                    fileData[1]= filename;

                    dnList.Add(new KeyValuePair<String, String[]>(artist, fileData));
                    Console.WriteLine("{0}", f.File);
                }
            }
            Console.WriteLine("{0} files found.", files.Count().ToString());

            foreach (var song in dnList)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("    <Song>\r\n");
                sb.Append("        <Artist>" + song.Key + "</Artist>\r\n");
                sb.Append("        <Track>" + song.Value[0] + "</Track>\r\n");
                sb.Append("        <Filename>" + song.Value[1] + "</Filename>\r\n");
                sb.Append("    </Song>\r\n");
                OutputFile.Write(sb);
            }
            OutputFile.WriteLine("</SongList>");
        }


        static void SongInfo(ref StreamWriter OutputFile)
        {
            String dirPath = ".";
            String extension = ".info.xml";
            String pattern = "*" + extension;
            String ignore = "_private";

            OutputFile.WriteLine("<SongInfo>");

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

                    StringBuilder xmlFrag = new StringBuilder();
                    xmlFrag.Append("    <Song>\r\n");
                    xmlFrag.Append("        <Artist>" + artist + "</Artist>\r\n");
                    xmlFrag.Append("        <Track>" + track + "</Track>\r\n");

                    StreamReader InputFile = null;
                    try
                    {
                        // open the info file
                        InputFile = new StreamReader(f.File);
                        while (!InputFile.EndOfStream)
                        {
                            String sLine = InputFile.ReadLine();
                            if (sLine.Length == 0 || sLine.Contains("?xml"))
                                continue;
                            xmlFrag.Append("        " + sLine + "\r\n");
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
                    finally
                    {
                        InputFile.Close();
                    }
                    
                    
                    xmlFrag.Append("    </Song>\r\n");


                    dnList.Add(new KeyValuePair<String, String>(artist, xmlFrag.ToString()));
                    Console.WriteLine("{0}", f.File);
                }
            }
            Console.WriteLine("{0} files found.", files.Count().ToString());

            foreach (var entry in dnList)
            {
                OutputFile.Write(entry.Value);
            }
            OutputFile.WriteLine("</SongInfo>");
        }
    }
}
