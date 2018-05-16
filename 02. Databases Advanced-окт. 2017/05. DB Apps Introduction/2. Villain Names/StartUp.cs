using System;
using System.Data.SqlClient;

namespace _2.Villain_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder() // write necessary atributes to use for connection
            {
                ["Server"] = "DESKTOP-QNEG77K\\SQLEXPRESS", //server name
                ["initial catalog"] = "MinionsDB", // database name that I will use
                ["Integrated Security"] = "true" //use windows autentication
            };

            var connection = new SqlConnection(builder.ToString());

            connection.Open();

            using (connection)
            {
                var querryNames = @"SELECT v.Name,COUNT(MinionId)
FROM Villains AS v
JOIN MinionsVillains AS mv ON mv.VillainId = v.Id
JOIN Minions AS m ON m.Id = mv.MinionId
GROUP BY v.Name
HAVING COUNT(MinionId) > 3
ORDER BY COUNT(MinionId) DESC";

                var commandNames = new SqlCommand(querryNames, connection);
                var reader = commandNames.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} - {reader[1]}");
                }
                reader.Dispose();
            }
        }
    }
}