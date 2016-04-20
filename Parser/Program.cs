using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;   // Need to run process
using System.Net;           // Need to work with web
using System.Threading;     // Need to wait

namespace Parser
{
    class Program
    {
        static void Main()
        {
            WebClient wc = new WebClient();

            int answer;

            do
            {
                Console.WriteLine("Enter the number that you want to parse:\n" +
                                "1)Aimp skins\n" +
                                "2)Certificates on Geekbrains\n"+
                                "3)Exit\n");
                answer = int.Parse(Console.ReadLine());
                switch (answer)
                {
                    case (1):
                        Console.WriteLine(DownloadSkinsForAimp(wc));
                        break;
                    case (2):
                        Console.WriteLine(DownloadCertificates(wc));
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("You specify an invalid command. Please try again.");
                        break;
                }

            } while (answer != 3);



        }


        /// <summary>
        /// Method to download skins from Aimp.ru
        /// </summary>
        /// <param name="wc"></param>
        /// <returns>String with description of result</returns>
        static string DownloadSkinsForAimp(WebClient wc)
        {
            Console.WriteLine("Downloading began");
            try
            {
                for (int i = 0; i <= 9; i++)
                {
                    string id = "79" + i;
                    // Path where we will download
                    string path = "aimp.ru/index.php?do=download&sub=catalog&id=" + id;
                                       
                    // All skins will be download in the folder specified in you browser settings
                    try
                    {
                        // Launch the browser and pass as an argument the path
                        Process.Start("chrome.exe", path);

                        Console.WriteLine("Download " + GetNameOfSkin(wc, id) + " is succesfull!");

                        // Wait for 5 seconds to avoid stack overflow
                        Thread.Sleep(5000);
                    }
                    catch
                    {
                        Console.WriteLine("Download" + GetNameOfSkin(wc, id) + "failed");
                    }
                }
            }
            catch
            {
                return "Something went is wrong";
            }
            return "Downloading complete";
            
        }

        /// <summary>
        /// Method to parse aimp skin name
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="id">ID of skin</param>
        /// <returns>String with name of skin</returns>
        static string GetNameOfSkin(WebClient wc, string id)
        {
            // Get html page
            string html = wc.DownloadString("http://www.aimp.ru/index.php?do=catalog&rec_id=" + id);
            // Find current id and remove left part of html
            string rightPartOfHtml = html.Substring(html.IndexOf(id) + 5);
            // Find the end of the name, and delete the rest of the line
            string name = rightPartOfHtml.Substring(0, rightPartOfHtml.IndexOf("<")).Replace(" ", "_");

            return name;
        }

        /// <summary>
        /// Method of downloading certificates
        /// </summary>
        /// <param name="wc"></param>
        /// <returns>String description of result</returns>
        static string DownloadCertificates(WebClient wc)
        {
            string currentUser = Environment.UserName;

            Console.WriteLine("Downloading began");

            try
            {
                for (int i = 0; i <= 9; i++)
                {
                    try
                    {
                        // All certificates on this site are stored in '.pdf' format
                        wc.DownloadFile("https://geekbrains.ru//certificates//7075" + i + ".pdf", "c:\\users\\" + currentUser + "\\download\\7075" + i + ".pdf");

                        Console.WriteLine("Download certificate №7075" + i + " is succesfull");
                    }
                    catch
                    {
                        Console.WriteLine("Download certificate №7075" + i + " is failed");
                    }
                }
                return "Downloading certificates are complite!";
            }
            catch
            {
                return "Something went is wrong";
            }
        }
    }
}
