using System;
using System.IO;
using System.Net.FtpClient;
using log4net;

namespace DeviceTestApplication
{
    public class RemoteFileManager
    {
        #region Fields

        protected static ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        /// <summary>
        /// Get remote file. Only FTP supported at present
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool DownloadFile(Uri file, string destination)
        {
            try
            {
                string[] userInfo = file.UserInfo.Split(new[] { ':' });
                if (userInfo.Length != 2)
                {
                    Logger.Warn("No login information in URL!");
                    return false;
                }

                using (var cl = new FtpClient(userInfo[0], userInfo[1], file.Host))
                {
                    string remoteFile = file.PathAndQuery;

                    // Check for existence
                    if (!cl.FileExists(remoteFile))
                        return false;

                    long size = cl.GetFileSize(remoteFile);

                    using (FtpDataStream chan = cl.OpenRead(remoteFile))
                    {
                        using (var stream = new FileStream(destination, FileMode.Create))
                        {
                            using (var writer = new BinaryWriter(stream))
                            {
                                var buf = new byte[cl.ReceiveBufferSize];
                                int read;
                                long total = 0;

                                while ((read = chan.Read(buf, 0, buf.Length)) > 0)
                                {
                                    total += read;

                                    writer.Write(buf, 0, read);

                                    Logger.DebugFormat("\rDownloaded: {0}/{1} {2:p2}",
                                                        total, size, (total / (double)size));
                                }
                            }
                        }
                        // when Dispose() is called on the chan object, the data channel
                        // stream will automatically be closed
                    }
                    // when Dispose() is called on the cl object, a logout will
                    // automatically be performed and the socket will be closed.
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Exception downloading file:" + e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Send file to remote. Only FTP supported at present
        /// </summary>
        /// <param name="file"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool UploadFile(Uri file, string source)
        {
            try
            {
                string[] userInfo = file.UserInfo.Split(new[] { ':' });
                if (userInfo.Length != 2)
                {
                    Logger.Warn("No login information in URL!");
                    return false;
                }

                using (var cl = new FtpClient(userInfo[0], userInfo[1], file.Host))
                {
                    string remoteFile = file.PathAndQuery;

                    // Check for existence
                    if (!File.Exists(source))
                        return false;

                    long size = new FileInfo(source).Length;

                    using (FtpDataStream chan = cl.OpenWrite(remoteFile))
                    {
                        using (var stream = new FileStream(source, FileMode.Open))
                        {
                            using (var reader = new BinaryReader(stream))
                            {
                                var buf = new byte[cl.SendBufferSize];
                                int read;
                                long total = 0;

                                while ((read = reader.Read(buf, 0, buf.Length)) > 0)
                                {
                                    total += read;

                                    chan.Write(buf, 0, read);

                                    Logger.DebugFormat("\rUploaded: {0}/{1} {2:p2}",
                                                        total, size, (total / (double)size));
                                }
                            }
                        }
                        // when Dispose() is called on the chan object, the data channel
                        // stream will automatically be closed
                    }
                    // when Dispose() is called on the cl object, a logout will
                    // automatically be performed and the socket will be closed.
                }
            }
            catch (Exception e)
            {
                Logger.Warn("Exception uploading file:" + e.Message);
                return false;
            }
            return true;
        }
    }
}
