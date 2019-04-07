using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class Player
    {
        string uid;
        string token;
        string createdAt;
        string lastLogin;
        string locale;
        string profilePic;
        int id;
        public int Id{
            get { return id; }
            set { id = value; }
        }

        string email;
        public string Email {
            get { return email; }
            set { email = value; }
        }

        string nickname;
        public string NickName {
            get { return nickname; }
            set { nickname = value; }
        }

        string birthday;
        public string BirthDate {
            get { return birthday; }
            set { birthday = value; }
        }

        string gender;
        public string Gender {
            get { return gender; }
            set { gender = value; }                       
        }

        int cashmole;
        public int CashMole {
            get { return cashmole; }
            set { cashmole = value; }
        }

        string userstatus;
        public string Userstatus {
            get { return userstatus; }
            set { userstatus = value; }
        }

        int numOfWinnings;
        public int NumOfWinnings {
            get { return numOfWinnings; }
            set { numOfWinnings = value; }
        }

        bool flagNotification;
        public bool FlagNotification {
            get { return flagNotification; }
            set { flagNotification = value; }
        }

        bool flagSound;
        public bool FlagSound {
            get { return flagSound; }
            set { flagSound = value; }
        }

        public string CreatedAt1 { get => createdAt; set => createdAt = value; }
        public string Locale { get => locale; set => locale = value; }
        public string ProfilePic { get => profilePic; set => profilePic = value; }
        public string LastLogin { get => lastLogin; set => lastLogin = value; }
        public string Uid { get => uid; set => uid = value; }
        public string Token { get => token; set => token = value; }

        public Player()
        {

        }

        public Player(string email, string nickName, string birthDate, string gender)
        {
            this.Email = email;
            this.NickName = nickName;
            this.BirthDate = birthDate;
            this.Gender = gender;
            this.CashMole = 250;
            this.NumOfWinnings = 0;
            this.FlagNotification = true;
            this.FlagSound = true;
        }

        public int Insert()
        {
            DBservices db = new DBservices();
            int rowAffected = db.insert(this);
            return rowAffected;
        }

        public string GetToken(string uid)
        {
            DBservices db = new DBservices();
            string token = db.getToken(uid);
            return token;
        }
        public int InsertToken(string token,string uid)
        {
            DBservices db = new DBservices();

            int rowAffected = db.insertToken(token,uid);
            return rowAffected;
        }
        public int InsertAvatar(string avatarUrl,string uid)
        {
            DBservices db = new DBservices();

            int rowAffected = db.insertToken(token, uid);
            return rowAffected;
        }
        //InsertLastLogin
        public int InsertLastLogin(string uid)
        {
            DBservices db = new DBservices();

            int rowAffected = db.insertLastLogin(uid);
            return rowAffected;
        }

        public List<Player> Read()
        {
            DBservices dbs = new DBservices();
            List<Player> lp = dbs.GetPlayers("TheMoleConnection", "Player");
            return lp;

        }

        //Count how many players signed up today
        public int TodaysPlayers()
        {
            DBservices dbs = new DBservices();
            List<Player> lp = dbs.TodaysPlayers("TheMoleConnection", "Player");
            int numofPlayers = lp.Count();
            return numofPlayers;
        }

        //Count how many players signed in this month
        public int MonthPlayers()
        {
            DBservices dbs = new DBservices();
            List<Player> lp = dbs.MonthPlayers("TheMoleConnection", "Player");
            int numofPlayers = lp.Count();
            return numofPlayers;
        }
    }
}