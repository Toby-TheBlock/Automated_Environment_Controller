﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

namespace Data_Logging_Application
{
    class DatabaseManager
    {
        private static DatabaseManager singleton = null;
        private string dbName = "Enviroment_Controller";

        public static DatabaseManager Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new DatabaseManager();
                }
                return singleton;
            }
        }

        public string DbName { get; }


        public async Task<bool> ConfigureDatabase()
        {
            try
            {
                string sourcePath = Path.Combine(Environment.CurrentDirectory, @"Sql_Queries\");
                string sqlQuery = File.ReadAllText(sourcePath + "setupDBStucture.sql");
                SqlConnection conn = OpenDatabaseConnection("Setup");
                SqlCommand comm = new SqlCommand(sqlQuery, conn);
                conn.Open();
                comm.ExecuteNonQuery();

                // Runs remaining sql-scripts containing creation of procedures. 
                for (int i = 0; i < Directory.GetFiles(sourcePath, "*", SearchOption.TopDirectoryOnly).Length - 1; i++)
                {
                    string currentQuery = File.ReadAllText(sourcePath + i + ".sql");
                    SqlCommand cmd = new SqlCommand(currentQuery, conn);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
                return true;
            }
            catch (Exception error)
            {
                Console.WriteLine("Something went wrong under the configuration of the database!" +
                    error.Message);
                return false;
            }

        }


        private SqlConnection OpenDatabaseConnection(string databaseName)
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[databaseName].ConnectionString);
        }


        /// <summary>
        /// Opens a SQL-connection and runs a query based on a stored procedure through it. 
        /// </summary>
        /// <param name="databaseName">The name of the database who's to be used (connectionstring name).</param>
        /// <param name="procedureName">The name of the stored procedure who's to be used.</param>
        /// <param name="parameters">A dictionary containing all of the necessary parameters need in the specified procedure.</param>
        public void CallProcedureWithoutReturn(string databaseName, string procedureName, Dictionary<string, string> parameters)
        {
            SqlConnection conn = OpenDatabaseConnection(databaseName);
            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                cmd.Parameters.Add(new SqlParameter("@" + entry.Key, entry.Value));
            }

            cmd.ExecuteNonQuery();
            conn.Close();
        }


        /// <summary>
        /// Opens a SQL-connection and runs a query based on a stored procedure through it. 
        /// The query is being run on the back of a SQL-reader, which stores the returned data in a datatable.
        /// </summary>
        /// <param name="databaseName">The name of the database who's to be used (connectionstring name).</param>
        /// <param name="procedureName">The name of the stored procedure who's to be used.</param>
        /// <param name="parameters">A dictionary containing all of the necessary parameters need in the specified procedure.</param>
        /// <returns>A datatable if all of the database data which was returned by the stored procedure.</returns>
        public DataTable CallProcedureWithReturn(string databaseName, string procedureName, Dictionary<string, string> parameters)
        {
            SqlConnection conn = OpenDatabaseConnection(databaseName);
            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            foreach (KeyValuePair<string, string> entry in parameters)
            {
                cmd.Parameters.Add(new SqlParameter("@" + entry.Key, entry.Value));
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            conn.Close();

            return data;
        }


        /// <summary>
        /// Takes a datatable and converts each row to a dictionary with entries where the key is the column-name,
        /// and the value is the row-value. All of the row-dictionaries are stored in a list. 
        /// </summary>
        /// <param name="dataTable">The datatable which is to be converted.</param>
        /// <returns>A list of dictionaries who store the row information.</returns>
        public List<Dictionary<string, string>> ConvertDataTableToDictionary(DataTable dataTable)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            foreach (DataRow row in dataTable.Rows)
            {
                Dictionary<string, string> dictRow = new Dictionary<string, string>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    dictRow.Add(col.ColumnName, row[col].ToString());
                }

                result.Add(dictRow);
            }

            return result;
        }


        /// <summary>
        /// Checks each value in a datatable if it is of the Byte[] type, and if so converts it to boolean. 
        /// This has to be done since data grid views can't handle the Byte[] type be default. 
        /// </summary>
        /// <param name="rawTable">The datatable who's to be checked.</param>
        /// <returns>A sanitized datatable.</returns>
        private DataTable ReadyDataTableForGridView(DataTable rawTable)
        {
            foreach (DataColumn col in rawTable.Columns)
            {
                if (col.DataType.Name == "Byte[]")
                {
                    DataTable readiedTable = rawTable.Clone();
                    readiedTable.Columns[col.ColumnName].DataType = typeof(bool);

                    foreach (DataRow row in rawTable.Rows)
                    {
                        DataRow modifiedRow = readiedTable.NewRow();

                        for (int i = 0; i < row.ItemArray.Length; i++)
                        {
                            var value = row.ItemArray[i];

                            if (row.ItemArray[i].GetType() == typeof(Byte[]))
                            {
                                value = BitConverter.ToBoolean((Byte[])row.ItemArray[i], 0);
                            }

                            modifiedRow[i] = value;
                        }

                        readiedTable.Rows.Add(modifiedRow);
                    }

                    return readiedTable;
                }
            }

            return rawTable;
        }


        public async Task<bool> CheckForSqlConnection(string databaseName)
        {
            try
            {
                try
                {
                    SqlConnection serverConn = OpenDatabaseConnection("Setup");
                    serverConn.Open();
                    serverConn.Close();
                }
                catch
                {
                    Console.WriteLine("Couldn't connect to the SQL-server. No SQL-server is running under: localhost\\SQLEXPRESS \n" +
                        "Please start the SQL-server and try again");

                    return false;
                }

                SqlConnection testConn = OpenDatabaseConnection(databaseName);
                testConn.Open();
                testConn.Close();

                Console.WriteLine("Successfully connected to the database " + databaseName + "!");

                return true;
            }
            catch
            {
                Console.WriteLine("Couldn't connect to the database! " + databaseName + " doesn't seem to exists on the SQL-server. " +
                    "Trying to setup the missing DB!");

                return await ConfigureDatabase(); ;
            }
        }
    }
}
