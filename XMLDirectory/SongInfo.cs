using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XMLDirectory
{
    public class SongInfo : SongBase
    {
        const String extension = ".info.xml";
        const String pattern = "*" + extension;
        List<KeyValuePair<String, String>> dnList = new List<KeyValuePair<String, String>>();

        public SongInfo(string OutputFileName) :
            base(OutputFileName)
        {
            WriteOuterOpenTag();
            GetFileData();
            WriteFileXml();
            WriteOuterClosingTag();
        }

        private void GetFileData()
        {
            var files = from file in Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };

            foreach (var f in files)
            {
                String artist, track, file;
                file = f.File;

                string[] artisttrack = f.File.Split('\\');
                if (artisttrack.Length == 3)
                {
                    artist = artisttrack[1];
                    track = artisttrack[2];

                    if (track.Contains(ignore))
                    {
                        continue;
                    }

                    track = TidyUpTrack(artist, track);
                    artist = TidyUpArtist(artist);

                    StringBuilder xmlFrag = ConstructSongXml(artist, track, file);

                    dnList.Add(new KeyValuePair<String, String>(artist, xmlFrag.ToString()));
                    FilesProcessed.Add(f.File);
                }
            }
        }

        private static StringBuilder ConstructSongXml(String artist, String track, String file)
        {
            StringBuilder xmlFrag = new StringBuilder();
            xmlFrag.Append("    <Song>\r\n");
            xmlFrag.Append("        <Artist>" + artist + "</Artist>\r\n");
            xmlFrag.Append("        <Track>" + track + "</Track>\r\n");

            StreamReader InputFile = null;
            try
            {
                // open the info file
                InputFile = new StreamReader(file);
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

            return xmlFrag;
        }

        private static string TidyUpArtist(String artist)
        {
            artist = artist.Replace("&&", "&amp;");
            artist = artist.Replace("\"", "&quot;");
            artist = artist.Replace("<", "&lt;");
            artist = artist.Replace(">", "&gt;");
            return artist;
        }

        private static string TidyUpTrack(String artist, String track)
        {
            track = track.Replace("_", " ");
            track = track.Replace(artist, "");
            track = track.Replace(extension, "");
            track = track.Trim();
            track = track.Replace("&", "&amp;");
            track = track.Replace("\"", "&quot;");
            track = track.Replace("<", "&lt;");
            track = track.Replace(">", "&gt;");

            return track;
        }

        private void WriteFileXml()
        {
            foreach (var entry in dnList)
            {
                OutputFile.Write(entry.Value);
            }
        }

        private void WriteOuterOpenTag()
        {
            OutputFile.WriteLine("<SongInfo>");
        }

        private void WriteOuterClosingTag()
        {
            OutputFile.WriteLine("</SongInfo>");
        }

    }
}
