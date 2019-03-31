using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class GameManager
    {
        int playerId;
        public int PlayerId {
            get { return playerId; }
            set { playerId = value; }
        }

        int categoryId;
        public int CategoryId {
            get { return categoryId; }
            set { categoryId = value; }
        }

        string timeRegistration;
        public string TimeRegistration {
            get { return timeRegistration; }
            set { timeRegistration = value; }
        }

        public GameManager(int playerId, int categoryId, string timeRegistration)
        {
            this.PlayerId = playerId;
            this.CategoryId = categoryId;
            this.TimeRegistration = timeRegistration;
        }
    }
}