using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class Admin
    {
        string email;
        public string Email {
            get { return email; }
            set { email = value; }
        }

        string password;
        public string Password {
            get { return password; }
            set { password = value; }
        }

        string nickName;
        public string NickName {
            get { return nickName; }
            set { nickName = value; }
        }


        public Admin()
        {
        }

        public Admin(string email, string password, string nickname)
        {
            this.Email = email;
            this.Password = password;
            this.NickName = nickname;
        }

        public bool CheckLoginDetails(string adminMail, string adminPassword)
        {
            DBservices db = new DBservices();
            return db.CheckUser(adminMail, adminPassword);
        }
    }
}