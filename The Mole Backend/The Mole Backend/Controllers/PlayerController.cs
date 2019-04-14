using AdminPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminPage.Controllers
{
    public class PlayerController : ApiController
    {
        // GET: api/Player
        [HttpGet]
        [Route("api/Player")]
        public IEnumerable<Player> Get()
        {
            try
            {
                List<Player> playerList = new List<Player>();
                Player p = new Player();
                playerList = p.Read();
                return playerList;
            }
            catch (Exception ex)
            {

                throw new Exception("GET ALL players error: ",ex);
            }
        }
        [HttpGet]
        [Route("api/PlayerGetToken")]
        public string GetToken(string uid)
        {
            try
            {
                Player p = new Player();
                return p.GetToken(uid);
            }
            catch (Exception ex)
            {

                throw new Exception("GetToken error: ",ex);
            }
        }
        //insert new player
        [HttpPost]
        [Route("api/player")]
        public void Post([FromBody]Player p)
        {
            try
            {
                p.Insert();
            }

            catch (Exception ex)
            {
                throw new Exception("בעיה בהכנסת הנתונים למערכת");
            }
        }

        //insert token player
        [HttpPost]
        [Route("api/playerToken")]
        public void PostToken([FromBody]Player p)
        {
            try
            {
                
                p.InsertToken(p.Token, p.Uid);
            }

            catch (Exception ex)
            {
                throw new Exception("Post ");
            }
        }

        ////insert avatar player
        //[HttpPost]
        //[Route("api/playerAvatar")]
        //public void PostAvatar([FromBody] Player p )
        //{
        //    try
        //    {
        //        p.InsertAvatar(p., uid);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new Exception("בעיה בהכנסת הנתונים למערכת");
        //    }
        //}

        //insert last login player
        [HttpPost]
        [Route("api/playerLastLogin")]
        public void PostLastLogin([FromBody] Player p )
        {
            try
            {
                p.InsertLastLogin(p.Uid);
            }

            catch (Exception ex)
            {
                throw new Exception("בעיה בהכנסת הנתונים למערכת");
            }
        }

        // GET Todays Players: api/Player
        [Route("api/PlayerToday")]
        public int GetTodaysPlayers()
        {
            try
            {
                int numofPlayers;
                Player p = new Player();
                numofPlayers = p.TodaysPlayers();
                return numofPlayers;
            }
            catch (Exception ex)
            {

                throw new Exception("GET Todays players error: ", ex);
            }
        }

        // GET Todays Players: api/Player
        [Route("api/PlayerThismonth")]
        public int GetMonthPlayers()
        {
            try
            {
                int numofPlayers;
                Player p = new Player();
                numofPlayers = p.MonthPlayers();
                return numofPlayers;
            }
            catch (Exception ex)
            {

                throw new Exception("GET Players error: ", ex);
            }
        }



        // GET: api/Player/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Player
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }
    }
}
