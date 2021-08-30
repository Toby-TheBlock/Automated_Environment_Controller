using System;
using System.IO;

namespace Data_Logging_Application
{
    class Program
    {
        private static DatabaseManager dbm = DatabaseManager.Singleton;

        static void Main(string[] args)
        {
            /*
            bool databaseReady = false;

            do
            {
                databaseReady = await dbm.CheckForSqlConnection(dbm.DbName);
            }
            while (!databaseReady);
            */

            string sourcePath = Path.Combine(Environment.CurrentDirectory, @"Sql_Queries\");
            string[] dicFiles = Directory.GetFiles(sourcePath, "*", SearchOption.TopDirectoryOnly);
            Console.WriteLine(dicFiles);
        }
    }
}
