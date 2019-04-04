using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using The_Mole_Backend.Models;

namespace The_Mole_Backend.Controllers
{
    public class SIXDOWController : ApiController
    {
        // GET: api/SIXDOW
        public IEnumerable<string> Get(string source,string target)
        {
            SixDOWAlgorithm webScrapeAlgo = new SixDOWAlgorithm(source, target);
            List<string> paths = webScrapeAlgo.GetPaths();

            return paths;
        }

        //// GET: api/SIXDOW/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/SIXDOW
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/SIXDOW/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/SIXDOW/5
        //public void Delete(int id)
        //{
        //}
    }
}
