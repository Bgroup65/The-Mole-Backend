using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace The_Mole_Backend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var config = GlobalConfiguration.Configuration;
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            InitializeApplicationVerteciesAndEdgesForAllCategories();

        }
        public void InitializeApplicationVerteciesAndEdgesForAllCategories()
        {
            DBservices db = new DBservices();

            List<string> vertecies = new List<string>();
            List<List<string>> edges = new List<List<string>>();

            string[] categoriesVertecies = new string[] { "NBAVertecies", "GeneralVertecies", "MoviesVertecies", "MusicVertecies", "CelebVertecies", "PoliticiansVertecies" };
            string[] categoriesEdges = new string[] { "NBAEdges", "GeneralEdges", "MoviesEdges", "MusicEdges", "CelebEdges", "PoliticiansEdges" };

            for (int i = 0; i < categoriesVertecies.Length; i++)
            {
                vertecies.Clear();
                edges.Clear();

                vertecies = db.GetAllVertecies("TheMoleConnection", categoriesVertecies[i]);
                edges = db.GetEdges("TheMoleConnection", categoriesEdges[i]);
                Application.Add(categoriesVertecies[i], new List<string>(vertecies));
                Application.Add(categoriesEdges[i],new List<List<string>>(edges));
            }

        }
    }
}
