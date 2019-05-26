using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPage.Models
{
    public class Game
    {
        int id;
        public int Id {
            get { return id; }
            set { id = value; }
        }

        string gameDate;
        public string GameDate {
            get { return gameDate; }
            set { gameDate = value; }
        }

        string gameDuration;
        public string GameDuration {
            get { return gameDuration; }
            set { gameDuration = value; }
        }

        double averageQueueDuration;
        public double AverageQueueDuration {
            get { return averageQueueDuration; }
            set { averageQueueDuration = value; }
        }

        string gameStartTime;
        public string GameStartTime {
            get { return gameStartTime; }
            set { gameStartTime = value; }
        }

        string gameFinishTime;
        public string GameFinishTime {
            get { return gameFinishTime; }
            set { gameFinishTime = value; }
        }

        public Game()
        {
        }

        public Game(int id, string gameDate, string gameStartTime)
        {
            this.Id = id;
            this.GameDate = gameDate;
            this.GameStartTime = gameStartTime;
        }

        public List<Game> Read()
        {
            DBservices dbs = new DBservices();
            List<Game> lg = dbs.GetGames("TheMoleConnection", "Game");
            return lg;

        }

        //Count how many games created in this month
        public int MonthGames()
        {
            DBservices dbs = new DBservices();
            List<Game> lg = dbs.MonthGames("TheMoleConnection", "Game");
            int numofPlayers = lg.Count();
            return numofPlayers;
        }
    }
}