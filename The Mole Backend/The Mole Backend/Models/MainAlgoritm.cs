using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Mole_Backend.Models
{
    public class MainAlgorithm
    {
        public string Source { get; set; }
        public string Source2 { get; set; }
        public string Target { get; set; }
        static Random random = new Random();

        public MainAlgorithm(string source, string target)
        {
            Source = source;
            Target = target;
        }
        public MainAlgorithm()
        {

        }


        /// <summary>
        /// getting rendom paths of only two-direction vertecies for testing
        /// </summary>
        /// <param name="Paths">empty list of paths to fill</param>
        /// <returns></returns>
        /// 
        //test for benny
        public List<int> GetPathsAdvancedIntersect(ref List<List<string>> Paths)
        {
            List<string> vertecies = new List<string>();
            List<List<string>> edges = new List<List<string>>();

            // get all vertecies and edges from DB
            DBservices db = new DBservices();
            vertecies = db.GetAllVertecies("TheMoleConnection", "MoviesVertecies");
            edges = db.GetEdges("TheMoleConnection", "MoviesEdges");

            //1. create a graph
            var graph = new WeightedDiGraph<string, int>();
            //2. insert vertecies to the graph
            foreach (string vertex in vertecies)
            {
                graph.AddVertex(vertex);
            }
            //3. insert edges to the graph
            foreach (var edge in edges)
            {
                graph.AddEdge(edge[0], edge[1], 1);
            }
            //remove one-direction edges
            List<List<string>> twoDirectionsEdges = new List<List<string>>();
            foreach (var edge in edges)
            {
                if (!graph.HasEdge(edge[1], edge[0]))
                {
                    graph.RemoveEdge(edge[0], edge[1]);
                }
                if (graph.HasEdge(edge[1], edge[0]))
                {
                    twoDirectionsEdges.Add(edge);
                }
            }
            //4. create dijkstra algorithm
            var algorithm = new DijikstraShortestPath<string, int>(new DijikstraShortestPathOperators());


            List<int> pathsCount = new List<int>();
            //var result = algorithm.FindShortestPath(graph, Source, Target);
            //pathsCount.Add(result.Path.Count);
            //Paths.Add(result.Path

            //5.run the algoritm for 110 random vertecies.
            for (int i = 0; i < 200; i++)
            {
                int sourceVertex = random.Next(0, vertecies.Count);
                int targetVertex = random.Next(0, vertecies.Count);
                //if source and target pages are the same DON'T run the algorithm
                if (sourceVertex != targetVertex)
                {
                    var result = algorithm.FindShortestPath(graph, vertecies[sourceVertex], vertecies[targetVertex]);
                    pathsCount.Add(result.Path.Count);
                    Paths.Add(result.Path);

                }
            }

            return pathsCount;
        }

        /// <summary>
        ///getting a list of random paths for testing
        /// </summary>
        /// <param name="Paths">empty list of paths to fill</param>
        /// <returns></returns>
        /// מחזיר הפניה לרשימה של כל הדרכים
        public List<int> GetPathsSimple(ref List<List<string>> Paths)
        {
            //רשימת ערכים
            List<string> vertecies = new List<string>();
            //הקשתות - רשימה של רשימה
            List<List<string>> edges = new List<List<string>>();
            //שליפה מהדאתא בייס של כל הצמתים מאותה קטגוריה
            //בעתיד נשנה את זה מPoliticiansEdges לערך שאותו נקבל
            // get all vertecies and edges from DB
            DBservices db = new DBservices();
            vertecies = db.GetAllVertecies("TheMoleConnection", "PoliticiansVertecies");
            edges = db.GetEdges("TheMoleConnection", "PoliticiansEdges");

            //הכרזה על גרף עם משקולת של 1 על כל קשת כדי להריץ את אלגוריתם הדיקסטרה
            //1. create a graph
            var graph = new WeightedDiGraph<string, int>();

            //הוספה לגרף את כל הצמתים שהבאנו מהדאתא בייס
            //2. insert vertecies to the graph
            foreach (string vertex in vertecies)
            {
                graph.AddVertex(vertex);
            }
            //הוספת כל הקשתות - מכיל ממי יצאתי, למי אני יוצא, ומשקל על הקשת 1
            //3. insert edges to the graph
            foreach (var edge in edges)
            {
                graph.AddEdge(edge[0], edge[1], 1);
            }
            //יצירת האלגוריתם הדיקסטרה באמצעות סיפריית 
            //using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
            //using Advanced.Algorithms.Graph;
            //4. create dijkstra algorithm
            var algorithm = new DijikstraShortestPath<string, int>(new DijikstraShortestPathOperators());

            //רשימה אשר סופרת בכל הדרכים כמה ערכים ישלי
            //מכילה את התוצאות של הדיקסטרה
            List<int> pathsCount = new List<int>();

            //הרצת האלגוריתם על 200 דרכים כטסט לראות מה הדרכים הכי קצרות
            //5.run the algoritm for 110 random vertecies.
            for (int i = 0; i < 200; i++)
            {
                //קבלת ערך רנדומלי מהרשימה שייצאנו מהדאא בייס מאינדקס 0 ועד הסוף
                int sourceVertex = random.Next(0, vertecies.Count);
                int targetVertex = random.Next(0, vertecies.Count);

                //בדיקת מקרה קצה שהערכים שונים
                //אם שונה מפעיל את האלגוריתם דיקסטרה מלמעלה
                //if source and target pages are the same DON'T run the algorithm
                if (sourceVertex != targetVertex)
                {
                    List<string> pathsTwo = new List<string>();
                    var result = algorithm.FindShortestPath(graph, vertecies[sourceVertex], vertecies[targetVertex]);
                    //הוספת תכונה לרשימת הערכים בדרך מסויימת - כמה צעדים זה לקח
                    pathsCount.Add(result.Path.Count);
                    //מקרה קצה - אם אין ערך שמפריד בין ערך ההתחלה והסיום - בלי לעבור בשום ערך אחר בדרך
                    //נוסיף לליסט שיצרנו את ההערך התחלה וערך הסיום עם כמות צעדים של 1
                    if (result.Path.Count == 1)
                    {
                        pathsTwo.Add(vertecies[sourceVertex]);
                        pathsTwo.Add(vertecies[targetVertex]);
                        Paths.Add(pathsTwo);
                    }
                    //אחרת לא מדובר במקרה קצה ונכניס רגיל
                    else Paths.Add(result.Path);

                }
            }

            return pathsCount;
        }

        /// <summary>
        /// getting the path from a source vertex to a target vertex
        /// </summary>
        /// <param name="source">inital vertex to start from</param>
        /// <param name="traget"> target vertex</param>
        /// <param name="categoryName">choose from: NBA,Politicans,GeneralKnowledge,Movies,Music,Celeb</param>
        /// <returns></returns>
        //הרצה של הדיקסטרה פעם אחת בלבד
        public List<List<string>> GetPath(string source, string target, string categoryName)
        {

            List<List<string>> TwoPaths = new List<List<string>>();
            string edgeCategoryName = "";
            string verteciesCategoryName = "";
            //ממירים את הערך שקיבלנו בדיוק לשם הטבלה בדאתא בייס
            switch (categoryName.ToUpper())
            {
                case "NBA":
                    edgeCategoryName = "NBAEdges";
                    verteciesCategoryName = "NBAVertecies";
                    break;
                case "GENERALKNOWLEDGE":
                    edgeCategoryName = "GeneralEdges";
                    verteciesCategoryName = "GeneralVertecies";
                    break;
                case "MOVIES":
                    edgeCategoryName = "MoviesEdges";
                    verteciesCategoryName = "MoviesVertecies";
                    break;
                case "MUSIC":
                    edgeCategoryName = "MusicEdges";
                    verteciesCategoryName = "MusicVertecies";
                    break;
                case "CELEB":
                    edgeCategoryName = "CelebEdges";
                    verteciesCategoryName = "CelebVertecies";
                    break;
                default:
                    break;
            }
            //פה נשמור את הדרך שלנו
            List<string> path = new List<string>();

            //get all vertecies and edges from db
            DBservices db = new DBservices();
            //קריאה מדאתא בייס על ידי הקטגוןריה שקיבלנו מהמשתמש
            //get edges and vertecies for the given category
            List<string> vertecies = this.GetVerteciesForCategory("TheMoleConnection", verteciesCategoryName);
            List<List<string>> edges = this.GetEdgesForCategory("TheMoleConnection", edgeCategoryName);
            //create a graph 
            var graph = new WeightedDiGraph<string, int>();
            //insert vertecies to the graph
            foreach (string vertex in vertecies)
            {
                graph.AddVertex(vertex);
            }
            //insert edges to the graph
            foreach (var edge in edges)
            {
                graph.AddEdge(edge[0], edge[1], 1);
            }
            //create dijkstra algorithm
            var algorithm = new DijikstraShortestPath<string, int>(new DijikstraShortestPathOperators());


            var result = algorithm.FindShortestPath(graph, source, target);
            TwoPaths.Add(result.Path);

            return TwoPaths;
        }

        /// <summary>
        /// get random six vertecies from a given category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        //מקבל קטגוריה ומחזיר 6 צמתים רנדומלים מהDB
        //3 לכל שחקן
        public string[] GetRandomVertecies(string categoryName)
        {

            string[] sixVertecies = new string[6];
            string verteciesCategoryName = "";
            switch (categoryName.ToUpper())
            {
                case "NBA":
                    verteciesCategoryName = "NBAVertecies";
                    break;
                case "GENERALKNOWLEDGE":
                    verteciesCategoryName = "GeneralVertecies";
                    break;
                case "MOVIES":
                    verteciesCategoryName = "MoviesVertecies";
                    break;
                case "MUSIC":
                    verteciesCategoryName = "MusicVertecies";
                    break;
                case "CELEB":
                    verteciesCategoryName = "CelebVertecies";
                    break;
                default:
                    break;
            }
            //get all vertecies and edges from db
            DBservices db = new DBservices();
            //get vertecies for the given category
            List<string> vertecies = this.GetVerteciesForCategory("TheMoleConnection", verteciesCategoryName);
            //get six  random articles from vertecies list in a category
            for (int i = 0; i < 6; i++)
            {
                int randomVertex = random.Next(0, vertecies.Count);
                sixVertecies[i] = vertecies[randomVertex];
            }

            return sixVertecies;
        }


        /// <summary>
        /// //get vertecies from DB for a category
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        //מחזיר את כל הצמתים הקיימים בקטגוריה מסויימת בדאתא בייס
        public List<string> GetVerteciesForCategory(string connectionString, string categoryName)
        {
            List<string> vertecies = new List<string>();
            DBservices db = new DBservices();
            vertecies = db.GetAllVertecies(connectionString, categoryName);


            return vertecies;
        }

        /// <summary>
        /// get edges from DB for a category
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        //מביא את כל הקשתות הקיימות בדאתא בייס בקטגוריה מסויימת
        public List<List<string>> GetEdgesForCategory(string connectionString, string categoryName)
        {

            List<List<string>> edges = new List<List<string>>();
            DBservices db = new DBservices();
            edges = db.GetEdges(connectionString, categoryName);


            return edges;
        }


    }

    //קשור לסיפרייה - משהו שאמרו שצריך להכניס כשהורדנו את הסיפרייה 
    //helper for the algorithm
    public class DijikstraShortestPathOperators : IShortestPathOperators<int>
    {
        public int DefaultValue
        {
            get
            {
                return 0;
            }


        }

        public int MaxValue
        {
            get
            {
                return int.MaxValue;
            }
        }

        public int Sum(int a, int b)
        {
            return checked(a + b);
        }
    }

}