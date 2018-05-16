using System;
using System.Data.SqlClient;

namespace _4.Add_Minion
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var connectionString = "Server=DESKTOP-QNEG77K\\SQLEXPRESS;Database=MinionsDb;Integrated Security= true";
            var connection = new SqlConnection(connectionString);

            var minionInfo = Console.ReadLine().Split();
            var minionName = minionInfo[1];
            var minionAge = int.Parse(minionInfo[2]);
            var minionTown = minionInfo[3];

            var villainInfo = Console.ReadLine().Split();
            var villainName = villainInfo[1];

            connection.Open();
            using (connection)
            {
                //check if town is present
                var townQuerry = "Select Count(*) FROM Towns as t WHERE t.Name = @townName";
                var commandToTowns = new SqlCommand(townQuerry, connection);
                commandToTowns.Parameters.AddWithValue("@townName", minionTown);
                var reader = commandToTowns.ExecuteReader();
                var isTownInTowns = false;
                while (reader.Read())
                {
                    if ((int)reader[0] > 0)
                    {
                        isTownInTowns = true;
                    }
                }
                reader.Dispose();

                if (!isTownInTowns)
                {
                    try
                    {
                        //add town
                        var querryToTowns = "INSERT Towns(Name) VALUES (@minionTown)";
                        var insertCommand = new SqlCommand(querryToTowns, connection);
                        insertCommand.Parameters.AddWithValue("@minionTown", minionTown);
                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine($"Town {minionTown} was added to the database.");
                    }
                    catch (Exception e)
                    {

                    }
                }

                //chaeck if minion is present
                var querryMinionPresent = "SELECT COUNT(*) FROM Minions WHERE Name = @minionName";
                var commandMinionPresent = new SqlCommand(querryMinionPresent, connection);
                commandMinionPresent.Parameters.AddWithValue("@minionName", minionName);
                reader = commandMinionPresent.ExecuteReader();
                var isMinionIn = false;
                while (reader.Read())
                {
                    if ((int)reader[0] > 0)
                    {
                        isMinionIn = true;
                    }
                }
                reader.Dispose();

                //if minion is present add minion
                if (!isMinionIn)
                {
                    var querryInsertMinion = "INSERT Minions (Name, Age) VALUES (@minionName, @minionAge)";
                    var commandInsertMinion = new SqlCommand(querryInsertMinion, connection);
                    commandInsertMinion.Parameters.AddWithValue("@minionName", minionName);
                    commandInsertMinion.Parameters.AddWithValue("@minionAge", minionAge);
                    commandInsertMinion.ExecuteNonQuery();
                }

                //check if villain is present
                var villainQuerry = "SELECT COUNT(*) FROM Villains WHERE Name = @villainName";
                var commandVillainName = new SqlCommand(villainQuerry, connection);
                commandVillainName.Parameters.AddWithValue("@villainName", villainName);
                reader = commandVillainName.ExecuteReader();
                var isVillainIn = false;
                while (reader.Read())
                {
                    if ((int)reader[0] > 0)
                    {
                        isVillainIn = true;
                    }
                }
                reader.Dispose();

                if (!isVillainIn)
                {
                    //add villain
                    var querryVillainInsert = "INSERT Villains (Name, EvilnessFactorId) VALUES (@villainName, 4)";
                    var commandInsertVillain = new SqlCommand(querryVillainInsert, connection);
                    commandInsertVillain.Parameters.AddWithValue("@villainName", villainName);
                    commandInsertVillain.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }


                var queryMinionId = "SELECT Id FROM Minions WHERE Name = @minionName";
                var commandIdMinnion = new SqlCommand(queryMinionId, connection);
                commandIdMinnion.Parameters.AddWithValue("@minionName", minionName);
                var currentMinionId = (int)commandIdMinnion.ExecuteScalar();

                var queryVillainId = "SELECT Id FROM Villains WHERE Name = @villainName";
                var commandIdVillain = new SqlCommand(queryVillainId, connection);
                commandIdVillain.Parameters.AddWithValue("@villainName", villainName);
                var currentVillainId = (int)commandIdVillain.ExecuteScalar();

                try
                {
                    var querryMinionToVillain = "INSERT MinionsVillains(MinionId, VillainId) VALUES (@minionId, @villianId)";
                    var insertToMinionsVillains = new SqlCommand(querryMinionToVillain, connection);
                    insertToMinionsVillains.Parameters.AddWithValue("@minionId", currentMinionId);
                    insertToMinionsVillains.Parameters.AddWithValue("@villianId", currentVillainId);
                    insertToMinionsVillains.ExecuteNonQuery();
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
                catch(Exception ex)
                {

                }

            }
        }
    }
}