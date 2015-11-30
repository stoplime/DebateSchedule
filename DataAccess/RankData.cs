using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DataAccess
{
    public class RankData
    {
        private DatabaseLayer dl;

        public RankData()
        {
            dl = new DatabaseLayer();
        }

        public bool GetRanks(out List<string> teamNames, out List<int> teamScores, out List<int> teamWins, out List<int> teamTotalMatches)
        {
            dl.SqlConnection.Open();
            string getRanksString = "SELECT * FROM Team WHERE team_deleted IS NULL ORDER BY team_wins DESC, team_score DESC, team_name";
            SqlCommand sqlc = new SqlCommand(getRanksString, dl.SqlConnection);
            SqlDataReader reader = sqlc.ExecuteReader();
            teamNames = new List<string>();
            teamScores = new List<int>();
            teamWins = new List<int>();
            teamTotalMatches = new List<int>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    teamNames.Add(reader.GetString(1));
                    if (reader.GetValue(3) != DBNull.Value)
                    {
                        teamScores.Add(reader.GetInt32(3));
                    }
                    else
                    {
                        teamScores.Add(0);
                    }
                    if (reader.GetValue(4) != DBNull.Value)
                    {
                        teamWins.Add(reader.GetInt32(4));
                    }
                    else
                    {
                        teamWins.Add(0);
                    }
                    if (reader.GetValue(5) != DBNull.Value)
                    {
                        if (reader.GetInt32(5) != 0)
                        {
                            teamTotalMatches.Add(reader.GetInt32(5));
                        }
                        else
                        {
                            teamTotalMatches.Add(-1);
                        }
                    }
                    else
                    {
                        teamTotalMatches.Add(-1);
                    }
                }
            }
            dl.SqlConnection.Close();
            return true;
        }

    }
}
