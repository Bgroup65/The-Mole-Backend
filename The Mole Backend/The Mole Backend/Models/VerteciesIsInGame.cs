using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class VerteciesIsInGame
    {
        int verteciesId;
        public int VerteciesId {
            get { return verteciesId; }
            set { verteciesId = value; }
        }

        int gameId;
        public int GameID {
            get { return gameId; }
            set { gameId = value; }
        }

        int verteciesPosition;
        public int VerteciesPosition {
            get { return verteciesPosition; }
            set { verteciesPosition = value; }
        }

        int playerId;
        public int PlayerId {
            get { return playerId; }
            set { playerId = value; }
        }

        public VerteciesIsInGame()
        {

        }

        public VerteciesIsInGame(int verteciesId, int gameID, int verteciesPosition)
        {
            this.VerteciesId = verteciesId;
            this.GameID = gameID;
            this.VerteciesPosition = verteciesPosition;
        }

        public List<VerteciesIsInGame> Read()
        {
            DBservices dbs = new DBservices();
            List<VerteciesIsInGame> lv = dbs.GetVertecies("TheMoleConnection", "VerteciesIsInGame");
            return lv;

        }
    }
}