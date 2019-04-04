using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace The_Mole_Backend.Models
{
    public class SixDOWAlgorithm
    {
        string firstUrl = "https://www.sixdegreesofwikipedia.com/?source=";
        string endUrl = "&target=";
        string url;
        public SixDOWAlgorithm(string source, string target)
        {
            Source = source;
            Target = target;
            url = firstUrl + Source + endUrl + target;
        }

        public string Source { get; set; }
        public string Target { get; set; }

        public List<string> GetPaths()
        {
           // var options = new ChromeOptions();
            //options.AddArguments("headless");
            ChromeOptions chromeOptions = new ChromeOptions();

            //chromeOptions.AddArguments("disable-gpu");
            chromeOptions.AddArguments("no-sandbox");
            //chromeOptions.Proxy = null;
            //chromeOptions.AddArguments("headless");
            
            string root_path = HttpRuntime.AppDomainAppPath;

            ChromeDriver chromeDriver = new ChromeDriver(root_path + @"\Drivers", chromeOptions);
            chromeDriver.Navigate().GoToUrl(url);
            chromeDriver.FindElementByXPath("//*[@id='root']/div[2]/div/button").Click();

            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //need to wait for this element to show up.
            string text = chromeDriver.FindElementByXPath("//*[@id='root']/div[2]/div/div[5]").Text;
            string[] paths = text.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );

            chromeDriver.Close();
            chromeDriver.Dispose();
            int index = Array.IndexOf(paths, Target);
            List<string> newPath = new List<string>();
            for (int i = 0; i <= index; i++)
            {
                newPath.Add(paths[i]);
            }

            return newPath;
        }
        public void PrintPaths(string[] paths)
        {
            string[] pathResults = new string[paths.Length / 2];
            for (int i = 0, j = 0; i < pathResults.Length; i++)
            {
                if (i % 2 == 0)
                {
                    pathResults[j] = paths[i];
                    j++;
                }
            }
            foreach (string str in pathResults)
            {
                Console.Write(str + " | ");
            }
        }
    }
}