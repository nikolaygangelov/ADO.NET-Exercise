using System;
using System.Data.SqlClient;

namespace Villian_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {   
            // create connection to SQL server and database
            using SqlConnection sqlConnection = new SqlConnection(Config.congigString);
          
            // open connection 
            sqlConnection.Open();

            // implement generated SQL query using c#
            string sqlQuery =  "SELECT v.Name, COUNT(mv.MinionId) AS MinionsCount" +
                                 "FROM [Villains] AS v" +
                            "LEFT JOIN MinionsVillains AS mv ON v.Id = mv.VillainId" +
                             "GROUP BY v.Name" +
                               "HAVING COUNT(mv.MinionId) > 3" +
                             "ORDER BY MinionsCount";

            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            int count = (int)sqlCommand.ExecuteScalar();
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            // close both connections  regardless of "using"
            sqlConnection.Close();
            reader.Close();
        }
    }
}
