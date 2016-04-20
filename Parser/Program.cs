using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;   //need to run process
using System.Net;           //need to work with web
using System.Threading;     //need to wait

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
                    break;
                case (2):
                    break;
                default:
                    Console.WriteLine("You specify an invalid command. Please try again.");
                    break;
            }

            
        }
    }
}
