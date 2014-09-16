using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;

namespace DeviceTestApplication.Utilities
{
    public class ArchiveHelper
    {
        public static bool ExtractTgz(string strFilename)
        {
            // TODO: May need to look at putting the extracted files in their own folder
            var fi = new FileInfo(strFilename); 
            return ExtractTgz(strFilename, fi.DirectoryName );
        }

        public static bool ExtractTgz(string strSourceFilename, string strDestFolder)
        {
            try
            {
                Stream inStream = File.OpenRead(strSourceFilename);
                Stream gzipStream = new GZipInputStream(inStream);

                TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
                tarArchive.ExtractContents(strDestFolder);
                tarArchive.CloseArchive();

                gzipStream.Close();
                inStream.Close();

                return true;
            } catch(Exception)
            {
                // Log error
                return false;
            }
        }
    }
}
