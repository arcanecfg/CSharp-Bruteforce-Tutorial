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
            //string mySrc = HttpMethods.Get("http://localhost/bwapp/login.php", "http://localhost/bwapp/login.php", myCookies);
            Console.Write("Enter the username: ");
            string username = Console.ReadLine();
            Console.Write("Enter the password: ");
            string password = Console.ReadLine();

            string postData = "login=" + username + "&password=" + password + "&security_level=0&form=submit";

            bool result = HttpMethods.Post("http://localhost/login.php", postData, "http://localhost/login.php", myCookies);
            if (result)
                Console.WriteLine("Valid!");
            else
                Console.WriteLine("Invalid!");

        }
    }
}
