using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using AdminPage.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    //connection string: TheMoleConnection

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }
    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }

    public List<List<string>> GetEdges(string conString, string tableName)
    {
        SqlConnection con = null;
        List<List<string>> edgesList = new List<List<string>>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                List<string> pages = new List<string>();

                string page = Convert.ToString(dr["OriginPage"]);
                string linkedPage = Convert.ToString(dr["LinkedPage"]);
                pages.Add(page);
                pages.Add(linkedPage);

                edgesList.Add(pages);
            }

            return edgesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }
    public List<string> GetEdgesForCategoryAndSource(string conString, string tableName,string source)
    {
        SqlConnection con = null;
        List<string> edgesList = new List<string>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + " where OriginPage='"+source+"'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {   // Read till the end of the data into a row
                //string page = Convert.ToString(dr["OriginPage"]);
                string linkedPage = Convert.ToString(dr["LinkedPage"]);
                edgesList.Add(linkedPage);
            }

            return edgesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    public List<string> GetAllVertecies(string conString, string tableName)
    {
        SqlConnection con = null;

        List<string> verteciesList = new List<string>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end
            while (dr.Read())
            {
                string vertex = dr["TitlePage"].ToString();
                verteciesList.Add(vertex);
            }

            return verteciesList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }
    public int insert(Player player)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("TheMoleConnection"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = BuildInsertCommand(player);      // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    public int insertToken(string token,string uid)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("TheMoleConnection"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = "update Player set NotificationToken ='"+token+"' where uid = '"+uid+"'";    // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int insertAvatar(string avatarUrl, string uid)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("TheMoleConnection"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = "update Player set avatar_pic ='" + avatarUrl + "' where uid = '" + uid + "'";    // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int insertLastLogin(string uid)
    {
        SqlConnection con;
        SqlCommand cmd;
        string format = "yyyy-MM-dd HH:mm:ss";
        DateTime time = DateTime.Now;
        try
        {
            con = connect("TheMoleConnection"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        String pStr = "update Player set LastLogin ='" + time.ToString(format) + "' where uid = '" + uid + "'";    // helper method to build the insert string

        cmd = CreateCommand(pStr, con);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            return 0;
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    /// <summary>
    /// insert a new player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    private String BuildInsertCommand(Player player)
    {
        String command;
        //player nickName is: firstname + lastname
        StringBuilder sb = new StringBuilder();
        string format = "yyyy-MM-dd HH:mm:ss";
        DateTime time = DateTime.Now;
        // use a string builder to create the dynamic string
        sb.AppendFormat("Values('{0}', '{1}','{2}','{3}','{4}','{5}')", player.Email, player.NickName, time.ToString(format),player.Locale,player.ProfilePic,player.Uid);
        String prefix = "INSERT INTO Player " + "(UserEmail, UserNickname,CreatedAt,Locale,profile_pic,uid) ";
        command = prefix + sb.ToString();

        return command;
    }

    public bool CheckUser(string adminMail, string adminPassword)
    {
        bool userInDB = false;
        SqlConnection con = null;
        try
        {
            con = connect("TheMoleConnection"); // create a connection to the database using the connection String defined in the web config file
            string query = "select * from Admin where AdminEmail ='" + adminMail + "'";
            SqlCommand cmd = new SqlCommand(query, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                if (dr["AdminPassword"].ToString() == adminPassword)
                {
                    userInDB = true;
                }
            }

            return userInDB;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }

        }
    }

    //---------------------------------------------------------------------------------
    // Read Players from the DB into a list - dataReader withOut Filter
    //---------------------------------------------------------------------------------
    public List<Player> GetPlayers(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Player> lp = new List<Player>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Player p = new Player();
                p.NickName = dr["UserNickname"].ToString();
                p.Email = dr["UserEmail"].ToString();
                p.BirthDate = dr["birthDate"].ToString();
                p.Gender = dr["gender"].ToString();
                if (dr["numOfWinnings"].ToString() == "")
                {
                    p.NumOfWinnings = 0;
                }
                else p.NumOfWinnings = int.Parse(dr["numOfWinnings"].ToString());

                lp.Add(p);
            }

            return lp;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Read Games from the DB into a list - dataReader withOut Filter
    //---------------------------------------------------------------------------------
    public List<Game> GetGames(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Game> lg = new List<Game>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Game g = new Game();
                g.Id = Convert.ToInt32(dr["GameID"]);
                g.GameDate = Convert.ToString(dr["GameDate"]);
                g.GameDuration = Convert.ToString(dr["gameDuration"]);
                g.GameStartTime = Convert.ToString(dr["GameStartTime"]);
                g.GameFinishTime = Convert.ToString(dr["GameFinishTime"]);


                lg.Add(g);
            }

            return lg;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Read Vertecies from the DB into a list - dataReader withOut Filter
    //---------------------------------------------------------------------------------
    public List<VerteciesIsInGame> GetVertecies(string conString, string tableName)
    {

        SqlConnection con = null;
        List<VerteciesIsInGame> lv = new List<VerteciesIsInGame>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName;
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                VerteciesIsInGame v = new VerteciesIsInGame();
                v.VerteciesId = (Int32)dr["VerteciesId"];
                v.GameID = (Int32)dr["GameID"];
                v.VerteciesPosition = (Int32)dr["VerteciesPosition"];
                v.PlayerId = (Int32)dr["PlayerId"];

                lv.Add(v);
            }

            return lv;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    public string getToken(string uid)
    {
        SqlConnection con = null;
        string token = "";
        try
        {
            con = connect("TheMoleConnection");
            String selectSTR = "SELECT NotificationToken FROM Player where uid='" + uid + "'";
            SqlCommand cmd = new SqlCommand(selectSTR, con);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                token = dr["NotificationToken"].ToString();
            }

            return token;
        }
        catch (Exception ex)
        {

            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }
    //---------------------------------------------------------------------------------
    // Read Players WHO SIGNED UP TODAY from the DB into a list - dataReader with Filter
    //---------------------------------------------------------------------------------

    public List<Player> TodaysPlayers(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Player> lp = new List<Player>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT DISTINCT UserEmail, UserNickname, LastLogin FROM " + tableName + "  WHERE datediff(hour,lastlogin,getdate())<=24";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Player p = new Player();
                p.Email = dr["UserEmail"].ToString();
                p.NickName = dr["UserNickname"].ToString();
                p.LastLogin = dr["LastLogin"].ToString();

                lp.Add(p);
            }

            return lp;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    //---------------------------------------------------------------------------------
    // Read Players WHO SIGNED IN This MONTH from the DB into a list - dataReader with Filter
    //---------------------------------------------------------------------------------
    public List<Player> MonthPlayers(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Player> lp = new List<Player>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT DISTINCT * FROM " + tableName + "  WHERE datediff(DAY,CreatedAt,getdate())<=30";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Player p = new Player();
                p.Email = dr["UserEmail"].ToString();
                p.NickName = dr["UserNickname"].ToString();
                p.CreatedAt1 = dr["CreatedAt"].ToString();

                lp.Add(p);
            }

            return lp;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // Read Games WHO Created This MONTH from the DB into a list - dataReader with Filter
    //---------------------------------------------------------------------------------
    public List<Game> MonthGames(string conString, string tableName)
    {

        SqlConnection con = null;
        List<Game> lg = new List<Game>();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT DISTINCT * FROM " + tableName + "  WHERE datediff(DAY,GameDate,getdate())<=30";
            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                Game g = new Game();
                g.Id = Convert.ToInt32(dr["GameID"]);
                g.GameDate = dr["GameDate"].ToString();

                lg.Add(g);
            }

            return lg;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    //getting Admin's NickName & Pic
    //---------------------------------------------------------------------------------
    public Admin GetAdmin(string conString, string tableName, string email)
    {

        SqlConnection con = null;
        Admin a = new Admin();
        try
        {
            con = connect(conString); // create a connection to the database using the connection String defined in the web config file

            String selectSTR = "SELECT * FROM " + tableName + "  WHERE AdminEmail= '" + email + "'";

            SqlCommand cmd = new SqlCommand(selectSTR, con);

            // get a reader
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

            while (dr.Read())
            {   // Read till the end of the data into a row
                a.NickName = dr["AdminNickname"].ToString();
                a.URL = dr["Pic"].ToString();
                a.Password = dr["AdminPassword"].ToString();
            }

            return a;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


}
