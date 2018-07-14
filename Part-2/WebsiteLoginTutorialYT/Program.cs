using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteLoginTutorialYT
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.CookieContainer myCookies = new System.Net.CookieContainer();
            string myStr = HttpMethods.Get("http://localhost/dvwapp/login.php", "http://localhost/dvwapp/login.php", ref myCookies);
            string token;
            Console.WriteLine("Token: " + (token = GetBetween(myStr, "name='user_token' value='", "' />")));

            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            string postData = "username=" + username + "&password=" + password + "&Login=Login&user_token=" + token;

            bool result = HttpMethods.Post("http://localhost/dvwapp/login.php", postData, "http://localhost/dvwapp/login.php", myCookies);
            Console.WriteLine(result.ToString());
            Console.ReadKey();
        }

        static string GetBetween(string message, string start, string end)
        {
            int startIndex = message.IndexOf(start) + start.Length;
            int stopIndex = message.IndexOf(end);
            return message.Substring(startIndex, stopIndex - startIndex);
        }
    }
}
