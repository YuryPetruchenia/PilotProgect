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
using System.Threading;
using System.Text.RegularExpressions;

namespace Progect
{
    public class Program
    {
        static void  Main(string[] args)
        {
            Logger.InitLogger();
            Logger.Log.Info("Logs worck");
            string specCult = "ru";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(specCult);
                        
            double bill = 0;
            string name;
            string email;

            #region Работа с меню
            Item menu = new Item();
            menu.items = new List<Sushi>();
            menu.items.Add(new Sushi { Name = "Syake maki", Price = 4.50 });
            menu.items.Add(new Sushi { Name = "Mango syake maki", Price = 10.40 });
            menu.items.Add(new Sushi { Name = "California maki", Price = 8.90 });
            menu.items.Add(new Sushi { Name = "Hokkaido maki", Price = 9.20 });
            menu.items.Add(new Sushi { Name = "Nigiri with roast tuna", Price = 2.90 });
            menu.items.Add(new Sushi { Name = "Nigiri with roast basse", Price = 1.70 });
            menu.items.Add(new Sushi { Name = "Ebi", Price = 2.90 });
            menu.items.Add(new Sushi { Name = "Maguro", Price = 2.90 });
            string json = JsonConvert.SerializeObject(menu, Formatting.Indented);
            File.WriteAllText(@"C:\Users\YuryPetruchenia\source\repos\Project\menu.txt", json);
            #endregion

            Halper.Greeting();
            name = Console.ReadLine();
            Console.WriteLine($"Gald to meet you: {name}. What sushi are you chois?");
            Console.WriteLine("This is our menu:");

            #region Вывод меню
            var obj = JsonConvert.DeserializeObject<Item>
                (File.ReadAllText(@"C:\Users\YuryPetruchenia\source\repos\Project\menu.txt"));

            for (int i = 0; i < obj.items.Count; i++)
            {
                Console.WriteLine($"{i+1}:{obj.items[i].Name}, price: {obj.items[i].Price} BYN");
            }
            #endregion

            #region Проверка вариантов выбора меню
            Console.WriteLine("Enter the number of the option you like:");

            string ver;
            do
            {
                ver = Console.ReadLine().ToLower();
                int numb = 0;
                bool ansver = Int32.TryParse(ver, out numb);

                if (ansver == true && numb <= obj.items.Count)
                {
                    numb = Convert.ToInt32(ver) - 1;
                    using (System.IO.StreamWriter file = new StreamWriter
                        (@"C:\Users\YuryPetruchenia\source\repos\Project\order.txt", true, Encoding.Default))
                    {
                        file.WriteLine($"{obj.items[numb].Name}.....{obj.items[numb].Price} BYN");
                    }
                    bill += obj.items[numb].Price;
                    Console.WriteLine($"{obj.items[numb].Name} added on your order. \t Total:{bill} BYN.");
                    Console.WriteLine("Somesthing more? To exit press 'Q'.");
                }
                else if (ansver == false)
                {                    
                    for (int i = 0; i < obj.items.Count; i++)
                    {
                        if (obj.items[i].Name.ToLower().Equals(ver))
                        {
                            using (System.IO.StreamWriter file = new StreamWriter
                                 (@"C:\Users\YuryPetruchenia\source\repos\Project\order.txt", true, Encoding.Default))
                                file.WriteLine($"{obj.items[i].Name}.....{obj.items[i].Price} BYN");

                            bill += obj.items[i].Price;
                            Console.WriteLine($"{obj.items[i].Name} added on your order. \t Total:{bill} BYN.");
                            Console.WriteLine("Somesthing more? To exit press 'Q'.");
                        }
                    }
                }                
            } while (ver != "q" && ver != "Q");

            using (System.IO.StreamWriter file = new StreamWriter
                                 (@"C:\Users\YuryPetruchenia\source\repos\Project\order.txt", true, Encoding.Default))
                file.WriteLine($"\t\t Bill: {bill} BYN");
            #endregion

            #region Проверка email
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            while (true)
            {
                Console.WriteLine("Now pass your email:");
                email = Console.ReadLine();

                if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Email passed");
                    break;
                }
                else
                {
                    Console.WriteLine("Uncorrect email");
                }
            }
            #endregion          

            SendEmailAsync(email).GetAwaiter();
            File.WriteAllText(@"C:\Users\YuryPetruchenia\source\repos\Project\order.txt", string.Empty);
            Console.WriteLine($"Thanks for your order,{name}. Check sanded on your email.");
            Console.Read();
        }
        
        private static async Task SendEmailAsync(string mail)
        {            
            MailAddress from = new MailAddress("yourockpet@gmail.com", "Юрий Петрученя");
            MailAddress to = new MailAddress( mail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Your order on YouRockSushi";
            m.Body = File.ReadAllText(@"C:\Users\YuryPetruchenia\source\repos\Project\order.txt");
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("yourockpet@gmail.com", "obe8xn1p");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            Console.WriteLine("Well done!");
        }
    }
}
