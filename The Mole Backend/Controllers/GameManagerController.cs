using AdminPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminPage.Controllers
{
    public class GameManagerController : ApiController
    {
        // GET: api/GameManager
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GameManager/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GameManager
        [HttpPost]
        [Route("api/GameManager")]//הקשר לאג'קס
        public void Post([FromBody]GameManager gm)
        {
            try
            {
                gm.insert();
            }

            catch (Exception)
            {
                throw new Exception("בעיה בהכנסת רשומה חדשה");
            }
        }

        // PUT: api/GameManager/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GameManager/5
        public void Delete(int id)
        {
        }
    }
}
