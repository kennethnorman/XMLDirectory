using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace XMLDirectory
{
    public class SongList : SongBase
    {
        const String extension = ".pdf";
        const String pattern = "*" + extension;
        const Char ObsMarker = '+';
        List<KeyValuePair<String, String[]>> dnList = new List<KeyValuePair<String, String[]>>();

        public SongList(string OutputFileName, bool UseObsfucatedPdfSuffix) : 
            base(OutputFileName)
        {
            if (UseObsfucatedPdfSuffix)
            {
                ObsfucateFiles(dirPath, extension, pattern);
            }
            else
            {
                DeObsfucateFiles(dirPath, extension, pattern);
            }

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

                    track = TidyUpTrackName(artist, track);
                    artist = TidyUpArtistName(artist);
                    filename = TidyUpFileName(filename);

                    string[] fileData = new string[2];
                    fileData[0] = track;
                    fileData[1] = filename;

                    dnList.Add(new KeyValuePair<String, String[]>(artist, fileData));
                    FilesProcessed.Add(f.File);
                }
            }
        }

        private static string TidyUpFileName(string filename)
        {
            filename = filename.Replace("&", "&amp;");
            return filename;
        }

        private static string TidyUpTrackName(String artist, String track)
        {
            if (track.Contains(ObsMarker))
            {
                track = track.Substring(0, track.IndexOf(ObsMarker));
            }
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

        private static string TidyUpArtistName(String artist)
        {
            artist = artist.Replace("&&", "&amp;");
            artist = artist.Replace("\"", "&quot;");
            artist = artist.Replace("<", "&lt;");
            artist = artist.Replace(">", "&gt;");

            return artist;
        }

        private void WriteFileXml()
        {
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
        }

        private void WriteOuterOpenTag()
        {
            OutputFile.WriteLine("<SongList>");
        }

        private void WriteOuterClosingTag()
        {
            OutputFile.WriteLine("</SongList>");
        }

        private static void ObsfucateFiles(String dirPath, String extension, String pattern)
        {
            var files = from file in Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };

            Random rnd = new Random(Environment.TickCount);
            foreach (var f in files)
            {
                if (f.File.Contains(ObsMarker))
                {
                    continue;
                }

                int value = rnd.Next(10000000, 99999999);
                string nameOnly = f.File.Replace(extension, "");
                string obsFileName = nameOnly + ObsMarker + value;
                File.Move(f.File, obsFileName + extension);
            }
        }

        private static void DeObsfucateFiles(String dirPath, String extension, String pattern)
        {
            var files = from file in Directory.EnumerateFiles(dirPath, pattern, SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };

            foreach (var f in files)
            {
                if (f.File.Contains(ObsMarker))
                {
                    string nameOnly = f.File.Replace(extension, "");
                    if (nameOnly.Contains(ObsMarker))
                    {
                        nameOnly = nameOnly.Substring(0, nameOnly.IndexOf(ObsMarker));
                    }
                    File.Move(f.File, nameOnly + extension);
                }
            }
        }
    }
}