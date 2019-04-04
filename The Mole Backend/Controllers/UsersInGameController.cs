using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using The_Mole_Backend.Models;

namespace The_Mole_Backend.Controllers
{
    public class UsersInGameController : ApiController
    {
        public UsersInGame UsersInGameList { get; private set; }

        // GET: api/UsersInGame
        [HttpGet]
        [Route("api/UsersInGame")]
        public UsersInGame Get()
        {
            try
            {
                UsersInGame u = new UsersInGame();
                UsersInGameList = u.Read();
                return UsersInGameList;
            }
            catch (Exception)
            {

                throw new Exception("בעיה בקריאת הנתונים מהמערכת");
            }
        }

        // GET: api/UsersInGame/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UsersInGame
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/UsersInGame/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UsersInGame/5
        public void Delete(int id)
        {
        }
    }
}
