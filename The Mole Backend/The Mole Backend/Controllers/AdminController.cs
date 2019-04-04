using AdminPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdminPage.Controllers
{
    public class AdminController : ApiController
    {
        // GET: api/Admin
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Admin/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Admin
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Admin/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Admin/5
        public void Delete(int id)
        {
        }

        //Check Admins LogIn
        [HttpGet]
        public bool Get(string adminMail, string adminPassword)
        {
            try
            {
                Admin a = new Admin();
                bool isExsits = a.CheckLoginDetails(adminMail, adminPassword);
                return isExsits;
            }
            catch (Exception ex)
            {
                throw new Exception("קרתה בעיה בעת כניסת המשתמש למערכת");
            }
        }
    }
}
