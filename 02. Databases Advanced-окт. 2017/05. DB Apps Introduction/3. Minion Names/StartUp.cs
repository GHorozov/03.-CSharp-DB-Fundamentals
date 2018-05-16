using System;
using System.Data.SqlClient;

namespace _3.Minion_Names
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=DESKTOP-QNEG77K\\SQLEXPRESS;Database=MinionsDb;Integrated Security= true";
            var connection = new SqlConnection(connectionString);

            int villainId = int.Parse(Console.ReadLine());

            connection.Open();
            using (connection)
            {
                var villainQuerry = "SELECT [Name] FROM Villains Where Id = @villainId";
                var villainCommand = new SqlCommand(villainQuerry, connection);
                villainCommand.Parameters.AddWithValue("@villainId", villainId);

                var reader = villainCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Villain: {reader[0]}");
                }

                reader.Dispose();

                var minionsQuerry = "SELECT name,age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE mv.VillainId = @villainId";
                var minnionsCommand = new SqlCommand(minionsQuerry, connection);
                minnionsCommand.Parameters.AddWithValue("@villainId", villainId);

                reader = minnionsCommand.ExecuteReader();
                
                var counter = 1;
                while (reader.Read())
                {
                    Console.WriteLine($"{counter}. {reader[0]} {reader[1]}");
                    counter++;
                }
            }
        }
    }
}