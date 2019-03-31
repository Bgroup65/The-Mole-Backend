using AdminPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminPage.Controllers
{
    public class VerteciesIsInGameController : ApiController
    {
        // GET: api/VerteciesIsInGame
        [HttpGet]
        [Route("api/VerteciesIsInGame")]
        public IEnumerable<VerteciesIsInGame> Get()
        {
            try
            {
                List<VerteciesIsInGame> verteciesList = new List<VerteciesIsInGame>();
                VerteciesIsInGame v = new VerteciesIsInGame();
                verteciesList = v.Read();
                return verteciesList;
            }
            catch (Exception)
            {

                throw new Exception("בעיה בקריאת הנתונים מהמערכת");
            }
        }

        // GET: api/VerteciesIsInGame/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VerteciesIsInGame
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VerteciesIsInGame/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VerteciesIsInGame/5
        public void Delete(int id)
        {
        }
    }
}
