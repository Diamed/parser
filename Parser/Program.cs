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
            Console.WriteLine("Enter the number that you want to parse:\n" +
                                "1)Aimp skins\n" +
                                "2)Certificates on Geekbrains\n");
            switch(Console.Read()){
                case (1):
                    Console.WriteLine(DownloadSkinsForAimp());
                    break;
                case (2):
                    break;
                default:
                    Console.WriteLine("You specify an invalid command. Please try again.");
                    break;
            }

            
        }

        static string DownloadSkinsForAimp()
        {
            Console.WriteLine("Downloading began");
            WebClient wc = new WebClient();
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
    }
}
