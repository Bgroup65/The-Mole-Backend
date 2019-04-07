using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using LinqToWiki.Download;
using LinqToWiki;
using LinqToWiki.Generated;
namespace DataFromWiki
{
    //בניית הרשת והכנסה של הערכים מויקיפדיה לדאתא בייס שלנו
    class Program
    {
        static void Main(string[] args)
        {


            //DATA about popular pages taken from: https://en.wikipedia.org/wiki/Wikipedia:Multiyear_ranking_of_most_viewed_pages#Sources
            //and from: http://wikirank-2018.di.unimi.it/faq.html

            //שימוש בסיפרייה של ויקיפדיה
            //USING LINQ-TO-WIKI
            //Downloader.LogDownloading = true;
            var wiki = new Wiki("LinqToWiki.Samples", "https://en.wikipedia.org", "/w/api.php");
            PageResultPageId(wiki);
            
            //לכל קטגוריה יש 10,000 ערכים
            //לכל ערך יש קישורים נוספים 
            //למשל לארה"ב יש 2500 ערכים שאליהם היא מובילה

            //קריאה מהאקסל של כל הערכים 
            //קריאה מתיקייה ומספר הגיליון
            //1. Read from execl Articles titles from excel fourth sheet.
            ExcelReader er = new ExcelReader(@"C:\Users\ספיר כהן\Desktop\כיוונים לרשת בויקיפדיה\Best 10K\DataFromWiki\MoviesNetwork.xlsx", 1);
            //חילקנו א התוכנית למנות קטנות של 5108 ערכים כדי לא לגרום לתוכנית לעוף ולא לשמור את כל מה שעשתה עד אז
            List<string> articles = er.ReadCell(5108,1);
            er.Close();

            //בסוף שלב 2 יצרנו רשימת ערכים שיוצאים מכל ערך
            ////2. get all pages information
            List<PageInfo> vertexAndEdges = PageResultProps(wiki, articles);

            //שלב החיתוך - התאמה של הערכים שקיבלנו לערכים הרלוונטים לנו מהרשימה שיצרנו בשלב הקודם
            ////3. Intersect to find edges
            CheckEdges(vertexAndEdges, articles);

            //כתיבה לאקסל את כל הקשתות שיצרנו
            //נכתוב בגיליון מספר 2 בקובץ הנדרש
            //לכל דף (ערך) נכתוב בתא מסויים את הערך מוצא ואת הערכי יעד
            //4. Write to excel (sheet 3)
            ExcelReader ew = new ExcelReader(@"C:\Users\ספיר כהן\Desktop\כיוונים לרשת בויקיפדיה\Best 10K\DataFromWiki\MoviesNetwork.xlsx", 2);
            int row = 1;
            int counter = 0;
            foreach (PageInfo item in vertexAndEdges)
            {
                counter++;
                //LinksTitles זאת רשימה
                row = ew.WriteToExcel(row, item.Title, item.LinksTitles);
            }

            //סגירת האקסל וסלאמתק
            ew.Close();
            //François Girard

        }
        //FOR LINQ-TO-WIKI (CONTROLLER FUNCTIONS)

        //מתחברת לשרת של ויקיפדיה ומביאה עבור כל ערך את כל הקישורים שיוצאים ממנו
        //get all pages props and links. 
        private static List<PageInfo> PageResultProps(Wiki wiki, List<string> pageTitles)
        {
            List<PageInfo> pi = new List<PageInfo>();
            //get info for all pages titles 
            foreach (var pageTitle in pageTitles)
            {
                var pageInfo = wiki.Query.allpages().
                //גורם לקחת את הערל בדיוק כפי שרשום ולא חלק מהערך או משהו שמכיל את זה
                //תבחר לי את הדף שהכותרת שלו בדיוק כמו שכתבתי לך
                Where(page => page.from == pageTitle.TrimEnd() && page.to==pageTitle.TrimEnd()).Pages.
                //עבור הדף הזה תעשה לי שליפה של הפרטים הבאים:
                //פרטים שטחיים שנדרש כמו מס' מזהה לעמוד, כותרת וכו' והלינקים שיוצאים ממנו
                Select(p => PageResult.Create(
                        p.info,
                        p.links().Select(s => s.title).ToList())
                        );
                //חוזר אובייקט של העמוד
                //נהפוך את האובייקט למערך ובגלל זה נעשה את ההמרה למערך
                var pageInfo2 = pageInfo.ToEnumerable().ToArray();
                //בדיקת מקרה קצה - אם חוזר לי 0 קישורים זה אומר שאינלי קישורים שיוצאים ממנו
                if (pageInfo2.Length >= 1)
                {
                    //הכנסה של הערכים לתוך האובייקט שיצרנו
                    PageInfo article = new PageInfo((long)pageInfo2[0].Info.pageid, pageInfo2[0].Info.title, pageInfo2[0].Data.ToList());
                    //הכנסה לרשימת הערכים
                    //article.PrintPageDetails();
                    pi.Add(article);
                }
            }
            //החזרת הרשימת ערכים שיצרנו
            return pi;
        }
        //getting pages id for the networks
        private static List<PageInfo> PageResultPageId(Wiki wiki)
        {
            List<PageInfo> pi = new List<PageInfo>();
            string pageTitle = "François Girard";
            var pageInfo = wiki.Query.allpages().
            Where(page => page.from == pageTitle.TrimEnd() && page.to == pageTitle.TrimEnd()).Pages.
            Select(p => p.info);

            var pageInfo2 = pageInfo.ToEnumerable().ToArray();
            if (pageInfo2.Length >= 1)
            {
                //PageInfo article = new PageInfo((long)pageInfo2[0].Info.pageid, pageInfo2[0].Info.title, pageInfo2[0].Data.ToList());
                //pi.Add(article);
            }
            return pi;
        }

        //חיתוך של הערכים שקיבלנו - רק מי שמופיע בתוך הרשימה ערכים שיצרנו בשלב הקודם
        private static void CheckEdges(List<PageInfo> pages, List<string> vertecies)
        {
            foreach (PageInfo page in pages)
            {
                //לכל דף רשימת הדפים שיצרנו - לכל ערך תבדוק אם הקישורים שלו (לינקטייטלס)
                //מתוך הרשימה הגדולה שקיבלנו תעשה חיתוך עם הרשימה הקטנה 
                page.LinksTitles = vertecies.Intersect(page.LinksTitles).ToList();
            }
        }

        //נסיונות בדיקה - לא חשוב
        private static void Write<T>(WikiQueryPageResult<PageResult<T>> source)
        {
            Write(source.ToEnumerable());
        }

        private static void Write<T>(IEnumerable<PageResult<T>> source)
        {
            foreach (var page in source.Take(10))
            {
                Console.WriteLine(page.Info.title);
                foreach (var item in page.Data.Take(10))
                    Console.WriteLine("  " + item);
            }
        }
    }
}
//next missions:
//1. foreach of the articles we need to create edges ONLY if they exsits in the article LIST (in the 10000 list!).
//2. save the edges to excel sheets. 
//3. after all excel sheets are prepard, try the algorithm.
//4. try to create a graph visualization.