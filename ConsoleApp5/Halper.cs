using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;
using System.Globalization;

namespace Progect
{



    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
    }

    public class Halper
    {       
        public static void Greeting()
        {
            System.TimeSpan str = DateTime.Now.TimeOfDay;
            int hours = str.Hours;
            if (hours >= 9 && hours < 12)
                Console.WriteLine("Good morning, what is your name?");
            if (hours >= 12 && hours < 15)
                Console.WriteLine("Good day, what is your name?");
            if (hours >= 15 && hours < 22)
                Console.WriteLine("Good evening, what is your name?");
            if (hours >= 22 && hours < 9)
                Console.WriteLine("Good night, what is your name?");
        }
        

        
    }
}
