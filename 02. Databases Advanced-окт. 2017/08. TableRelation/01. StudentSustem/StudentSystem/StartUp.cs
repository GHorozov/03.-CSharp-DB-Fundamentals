namespace P01_StudentSystem
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using P01_StudentSystem.Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new StudentSystemContext();
            
            //ReseetDatabase(db);

            Seed(db);
        }

        private static void Seed(StudentSystemContext db)
        {
            
        }

        private static void ReseetDatabase(StudentSystemContext db)
        {
           

            using (db)
            {
                db.Database.EnsureDeleted();

                db.Database.EnsureCreated();

                db.Database.Migrate();
            }
        }
    }
}
