using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using The_Mole_Backend.Models;

namespace The_Mole_Backend.Controllers
{
    public class NetworkController : ApiController
    {

        // GET: api/Network
        public IEnumerable<List<string>> Get100Paths()
        {
            try
            {
                MainAlgorithm mainAlgorithm = new MainAlgorithm();
                List<List<string>> literalPaths = new List<List<string>>();
                List<int> pathsCount = new List<int>();
                pathsCount = mainAlgorithm.GetPathsSimple(ref literalPaths);

                return literalPaths;
            }
            catch (Exception ex)
            {
                throw new Exception("problem with get100paths, the error: " + ex);
            }

        }

        public IEnumerable<List<string>> GetPaths(string source, string target, string categoryName)
        {
            try
            {
                MainAlgorithm mainAlgorithm = new MainAlgorithm();
                List<List<string>> TwoPaths = new List<List<string>>();
                TwoPaths = mainAlgorithm.GetPath(source, target, categoryName);

                return TwoPaths;
            }
            catch (Exception ex)
            {

                throw new Exception("problem with GetTwoPaths, the error: " + ex);
            }
        }

        public string[] GetRandomVertecies(string categoryName)
        {
            try
            {
                MainAlgorithm mainAlgorithm = new MainAlgorithm();
                string[] randomVertecies = new string[6];
                randomVertecies = mainAlgorithm.GetRandomVertecies(categoryName);

                return randomVertecies;
            }
            catch (Exception ex)
            {

                throw new Exception("problem with GetRandomVertecies, the error: " + ex);
            }
        }


    }
}
