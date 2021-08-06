using BasketballTournament.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.DataBase
{ 
    public class DBController
    {
        public static DBController controller { get; private set; }

        private MySqlConnection conn;
        private MySqlCommand command;
        private StringBuilder builder = new StringBuilder();
        private MySqlDataReader reader;

        private string connStr = "server=localhost;user=root;database=basketball;password=d1I3m3A9n;";

        public DBController() 
        {
            controller = this;
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        public void CreateGame(int tid, int team1id, int team2id, int scores ) 
        {
            builder.Clear();
            builder.Append("INSERT INTO `games` (tournamentid, team1id, team2id, scores) VALUES (");
            builder.Append(tid);
            builder.Append(",");
            builder.Append(team1id);
            builder.Append(",");
            builder.Append(team2id);
            builder.Append(",");
            builder.Append(scores);
            builder.Append(")");

            command = new MySqlCommand(builder.ToString(), conn);
            command.ExecuteNonQuery();
            GetCurrentGames();
        }

        public void CreateTournament(string name, int[] ids) 
        {
            builder.Clear();
            builder.Append("INSERT INTO `tournaments` (name, userId, team1id,team2id,team3id,team4id,team5id,team6id,team7id,team8id) VALUES ('");
            builder.Append(name);
            builder.Append("',");
            builder.Append(Repository.User.Id);
            builder.Append(",");
            builder.Append(ids[0]);
            builder.Append(",");
            builder.Append(ids[1]);
            builder.Append(",");
            builder.Append(ids[2]);
            builder.Append(",");
            builder.Append(ids[3]);
            builder.Append(",");
            builder.Append(ids[4]);
            builder.Append(",");
            builder.Append(ids[5]);
            builder.Append(",");
            builder.Append(ids[6]);
            builder.Append(",");
            builder.Append(ids[7]);
            builder.Append(")");

            command = new MySqlCommand(builder.ToString(), conn);
            command.ExecuteNonQuery();
            GetCurrentTournaments();
        }

        public void CreateNewTeam(string name) 
        {
            builder.Clear();
            builder.Append("INSERT INTO `teams`(name) VALUES ('");
            builder.Append(name);
            builder.Append("')");

            command = new MySqlCommand(builder.ToString(), conn);
            command.ExecuteNonQuery();
            GetCurrentTeams();
        }

        public void UpdateTeam(int id, string name) 
        {
            builder.Clear();
            builder.Append("UPDATE `teams` SET name = '");
            builder.Append(name);
            builder.Append("' WHERE id = ");
            builder.Append(id);

            command = new MySqlCommand(builder.ToString(), conn);
            command.ExecuteNonQuery();
            GetCurrentTeams();
        }

        public void UpdateInfo() 
        {
            GetCurrentTeams();
            GetCurrentTournaments();
            GetCurrentGames();
        }

        public void GetCurrentTeams() 
        {
            Repository.Teams.Clear();

            builder.Clear();
            builder.Append("SELECT * FROM `teams`");

            command = new MySqlCommand(builder.ToString(), conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                Repository.Teams.Add(reader.GetInt32(0),new Entities.TeamEntity(reader.GetInt32(0),reader.GetString(1)));
            }

            reader.Close();
        }

        public void GetCurrentTournaments()
        {
            Repository.Tournaments.Clear();

            builder.Clear();
            builder.Append("SELECT * FROM `tournaments` WHERE userId = ");
            builder.Append(Repository.User.Id);

            command = new MySqlCommand(builder.ToString(), conn);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                TournamentEntity tournamentEntity = new TournamentEntity(reader.GetInt32(0), reader.GetString(1));
                for (int i = 2; i < 10; i++) 
                {
                    tournamentEntity.Teams.Add(Repository.Teams[reader.GetInt32(i)]);
                }
                Repository.Tournaments.Add(reader.GetInt32(0), tournamentEntity);
            }
            reader.Close();
        }

        public void GetCurrentGames()
        {
            if (Repository.Tournaments.Count > 0)
            {
                Repository.Games.Clear();

                builder.Clear();
                builder.Append("SELECT * FROM `games` WHERE tournamentid IN  (");

                for (int i = 0; i < Repository.Tournaments.Count; i++)
                {
                    builder.Append(Repository.Tournaments.Keys.ToArray()[i]);
                    builder.Append(",");
                }

                builder.Remove(builder.Length - 1, 1);
                builder.Append(")");

                command = new MySqlCommand(builder.ToString(), conn);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Repository.Games.Add(reader.GetInt32(0), new GameEntity(reader.GetInt32(0),Repository.Tournaments[reader.GetInt32(1)], Repository.Teams[reader.GetInt32(2)], Repository.Teams[reader.GetInt32(3)], reader.GetInt32(4) > 0, Math.Abs(reader.GetInt32(4))));
                }
                reader.Close();
            }
        }

        public UserEntity AuthorizeUser(string login) 
        {
            builder.Clear();
            builder.Append("SELECT * FROM `users` WHERE login = '");
            builder.Append(login);
            builder.Append("'");

            command = new MySqlCommand(builder.ToString(), conn);
            reader = command.ExecuteReader();
            
            if (reader.HasRows)
            {
                reader.Read();
                Entities.UserEntity user = new Entities.UserEntity(reader.GetInt32(0), reader.GetString(3), reader.GetString(4));
                user.SaltedPass = reader.GetString(2);
                reader.Close();
                return user;
            }
            else 
            {
                reader.Close();
                return null;    
            }
        }

        public bool AddUser(string name, string last, string login, string password) 
        {
            builder.Clear();
            builder.Append("SELECT id FROM `users` WHERE login = '");
            builder.Append(login);
            builder.Append("'");


            command = new MySqlCommand(builder.ToString(), conn);
            reader = command.ExecuteReader();
            if (reader.HasRows) 
            {
                reader.Close();
                return false;
            }
            reader.Close();

            builder.Clear();

            builder.Append("INSERT INTO `users` (name, last, login, password) VALUES ('");
            builder.Append(name);
            builder.Append("','");
            builder.Append(last);
            builder.Append("','");
            builder.Append(login);
            builder.Append("','");
            builder.Append(password);
            builder.Append("')");
            command = new MySqlCommand(builder.ToString(), conn);
            command.ExecuteNonQuery();

            return true;
        }

        public int GetUserIdAfterReg() 
        {
            builder.Clear();
            builder.Append("SELECT id FROM `users` ORDER BY id DESC LIMIT 1");


            command = new MySqlCommand(builder.ToString(), conn);
            reader = command.ExecuteReader();
            reader.Read();
            int id = reader.GetInt32(0);
            reader.Close();
            return id;

        }




        ~DBController() 
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
