using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Mole_Backend.Models
{
    public class UsersInGame
    {
        int playerId1;
            public int PlayerId1 {
            get { return playerId1; }
            set { playerId1 = value; }
        }

        string playerName1;
        public string PlayerName1 {
            get { return playerName1; }
            set { playerName1 = value; }
        }

        int playerId2;
        public int PlayerId2
        {
            get { return playerId2; }
            set { playerId2 = value; }
        }

        string playerName2;
        public string PlayerName2
        {
            get { return playerName2; }
            set { playerName2 = value; }
        }

        int categoryId;
        public int CategoryId {
            get { return categoryId; }
            set { categoryId = value; }
        }

        string categoryName;
        public string CategoryName {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public UsersInGame Read()
        {
            DBservices dbs = new DBservices();
            UsersInGame lu = dbs.GetUsersInGame("TheMoleConnection", "QUEUE");
            return lu;

        }
    }
}