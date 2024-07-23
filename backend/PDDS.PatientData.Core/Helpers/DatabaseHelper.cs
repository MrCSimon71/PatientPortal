using Microsoft.Data.Sqlite;
using System;
﻿using System.Data.SQLite;
using System.IO;

namespace PDDS.PatientData.Core.Helpers
{
    public static class DatabaseHelper
    {
        private static string connectionString = @"Data Source=..\..\database\PatientData.db;Version=3;";

        public static void InitializeDatabase(bool dropDatabase = false)
        {
            if (dropDatabase && File.Exists(@"..\..\database\PatientData.db"))
            {
                File.Delete(@"..\..\database\PatientData.db");
            }

            if (!File.Exists(@"..\..\database\PatientData.db"))
            {
                SQLiteConnection.CreateFile(@"..\..\database\PatientData.db");
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = System.IO.File.ReadAllText(@"..\..\database\PatientData_DDL.sql");
                    command.ExecuteNonQuery();

                    command.CommandText = System.IO.File.ReadAllText(@"..\..\database\PatientData_SeedData.sql");
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}



