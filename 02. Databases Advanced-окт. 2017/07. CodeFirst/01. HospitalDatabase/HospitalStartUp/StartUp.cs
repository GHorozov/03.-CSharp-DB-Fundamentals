namespace P01_HospitalDatabase
{
    using P01_HospitalDatabase.Data;

    public class StartUp
    {
        public static object DatabaseInitializer { get; private set; }

        public static void Main(string[] args)
        {
            //DatabaseInitializer.ResetDatabase(); //-reset database, add migration, add data to database

            using (var db = new HospitalContext())
            {
                DatabaseInitializer.SeedPatients(db, 100);
            }
           
        }
    }
}
