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
        //מביא את כל הדרכים מהערך ההתחלתי לערך יעד
        public List<string> GetPaths()
        {
            //בגלל שאין לאתר API אנחנו מרימים דפדפן של כרום והוא זה שיכנס לאתר
            ChromeOptions chromeOptions = new ChromeOptions();
            //קונפיגורציה לאיך מתחזים ומרימים את הדפדפן הזה
            chromeOptions.AddArguments("no-sandbox");
            string root_path = HttpRuntime.AppDomainAppPath;
            //הדפדפן עצמו זה הכרום דריבר הוא חייב להיות מותקן על הפרויקט שלנו 
            //איפה ההתקנה של הכרום נמצא ביחס לתיקייה המקורית שלי
            ChromeDriver chromeDriver = new ChromeDriver(root_path + @"\Drivers", chromeOptions);
            //סיום חלק קונפיגורציה

            //נביגייט לך לאנשהו
            //גו טו url - לאיזה url - לאותו אחד שעשינו בבנאי ה6דוקס
            chromeDriver.Navigate().GoToUrl(url);
            //הדרך ללחצן של הGo - כדי לבצע את פעולת החיפוש- תלחץ עליו
            chromeDriver.FindElementByXPath("//*[@id='root']/div[2]/div/button").Click();
            //עברנו לדף של התוצאות שקיבלנו מהלחיצה
            //ובגלל שהכל א-סינכרוני נחכה 10 שניות לפני שנבצע את הפעולות הבאות
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //בתום ה10 שלוקח לדף להתעדכן בודאות כולל זמן ביטחון נתחיל לבצע את הפעולות הבאות
            //need to wait for this element to show up.
            //כל הדרכים מופיעות בדיב מספר 5 (התוצאות) -אותם ניקח ונשמור כמשתנה text
            string text = chromeDriver.FindElementByXPath("//*[@id='root']/div[2]/div/div[5]").Text;
            //זה מגיע כחתיכה אחת כל הדרכים, לא מסודרת ולכן המשפטים הבאים:
            //עושה פיצול לגוש מילים שקיבלנו כדי לסדר אותם לערכים שונים
            string[] paths = text.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            );
            //סגירה של הכרום דריבר
            //השני לסגור את הפרוסס בשרת
            chromeDriver.Close();
            chromeDriver.Dispose();

            //ניקח את האינדקס מיקום הראשון שנפגוש של העדך יעד
            //מבצע חיפוש על המערך שיצרנו עד שיפגוש לראשונה את הערך
            int index = Array.IndexOf(paths, Target);
            //לתוך מערך נכניס את כל הערכים מאינדקס 0 עד האינדקס בו נמצא הערך יעד לראשונה וכך יצרנו את המסלול הכי קצר
            List<string> newPath = new List<string>();
            for (int i = 0; i <= index; i++)
            {
                newPath.Add(paths[i]);
            }

            return newPath;
        }
        //פונקציה להדפסה יפה של המערך של כל הדרכים שקיבלנו
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