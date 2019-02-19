using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Diagnostics;
using System.IO;

namespace HostsDownloaderWindows
{
    static class Program
    {
        private static void Main(string[] args)
        {

            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);

                if(!isElevated)
                {
                    return;
                }

                if(!HostsFileRetriever.IsWebHostsFileAccessible())
                {
                    Console.Out.WriteLine("Cannot contact website hosting hosts file. Exitting.");
                    return;
                }

                String[] hostsFile = HostsFileRetriever.GetWebHostsFile().Split(Environment.NewLine.ToCharArray());

                String webLastUpdatedLine = "";
                foreach (String line in hostsFile)
                {
                    if (line.StartsWith("# Last updated:"))
                    {
                        webLastUpdatedLine = line;
                        break;
                    }
                }

                String webDateString = ExtractUpdatedDate(webLastUpdatedLine);




                if(String.IsNullOrEmpty(webDateString))
                {
                    Console.Out.WriteLine("Could not parse hosts file hosted at website.");
                    return;
                }

                DateTime webDate = DateTime.Parse(webDateString);
                Console.Out.WriteLine("Web file updated on " + webDate.ToLongDateString());

                String[] localHostsFile = HostsFileRetriever.GetLocalHostsFile().Split(Environment.NewLine.ToCharArray());

                String localDateUpdatedLine = "";
                foreach (String line in localHostsFile)
                {
                    if(line.StartsWith("# Last updated:"))
                    {
                        localDateUpdatedLine = line;
                        break;
                    }
                }



                String localDateString = ExtractUpdatedDate(localDateUpdatedLine);
                if (String.IsNullOrEmpty(localDateString) && !String.IsNullOrEmpty(webLastUpdatedLine))
                {
                    File.WriteAllText(HostsFileRetriever.GetHostsFilePath(),
                       string.Join(Environment.NewLine, hostsFile));
                    Console.Out.WriteLine("Successfully replaced hosts file with version hosted on web.");
                    return;
                }
                DateTime localDate = DateTime.Parse(localDateString);

                Console.Out.WriteLine("Local file updated on " + localDate.ToLongDateString());

                if (webDate.CompareTo(localDate) < 0)
                {
                    Console.Out.WriteLine("You are running a later version of the hosts file than the web version.");
                }

                else if (webDate.CompareTo(localDate) > 0)
                {
                    File.WriteAllText(HostsFileRetriever.GetHostsFilePath(),
                        string.Join(Environment.NewLine, hostsFile));
                    Console.Out.WriteLine("Successfully updated hosts file.");
                }
                else
                {
                    Console.Out.WriteLine("Your hosts file is already up to date.");
                }

                Console.Out.WriteLine("Press any key to quit.");
                Console.ReadKey();
            }
        }

        private static String ExtractUpdatedDate(String updateLine)
        {
            int dateStartIndex = updateLine.IndexOf(": ") + 1;
            int dateEndIndex = updateLine.IndexOf(" at");
            String dateString = updateLine.Substring(dateStartIndex, dateEndIndex - dateStartIndex).Trim();
            DateTime fileDate;
            bool worked = DateTime.TryParse(dateString, out fileDate);
            if(worked)
            {
                return dateString;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
