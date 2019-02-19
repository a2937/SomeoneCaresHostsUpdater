using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace HostsDownloaderWindows
{
    public static class HostsFileRetriever
    {
        private static string url = @"https://someonewhocares.org/hosts/ipv6/hosts";

        /// <summary>
        /// Gets the someonewhocares ipV6 hosts file.
        /// </summary>
        /// <returns>A string containing the hosts file</returns>
        public static string GetWebHostsFile()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Determines whether [is web hosts file accessible.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is web hosts file accessible]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWebHostsFileAccessible()
        {
            try
            {
                var myRequest = (HttpWebRequest)WebRequest.Create(url);

                var response = (HttpWebResponse)myRequest.GetResponse();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception)
            {
                //  not available at all, for some reason
                return false;
            }
        }


        /// <summary>
        /// Gets the local hosts file for the current operating system.
        /// </summary>
        /// <returns></returns>
        public static string GetLocalHostsFile()
        {
            string path = "";
            string hostfile = "";
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
               path = "hosts";
               hostfile = Path.Combine("etc", path);
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) )
            {
                path = "system32\\drivers\\etc\\hosts";
                hostfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), path);
            }

            if (File.Exists(hostfile))
            {
                return File.ReadAllText(hostfile);
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets the hosts file path.
        /// </summary>
        /// <returns></returns>
        public static String GetHostsFilePath()
        {
            string path = "";
            string hostfile = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = "hosts";
                hostfile = Path.Combine("etc", path);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = "system32\\drivers\\etc\\hosts";
                hostfile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), path);
            }

            if (File.Exists(hostfile))
            {
                return hostfile;
            }
            else
            {
                return String.Empty;
            }
        }

    }
}
