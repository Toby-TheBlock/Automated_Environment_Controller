using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;

namespace Data_Monitoring_Application
{
    public class DatabaseManager
    {
        private static DatabaseManager singleton = null;
        private string dbName;

        public static DatabaseManager Singleton
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new DatabaseManager("Environment_Controller");
                }
                return singleton;
            }
        }

        public string DbName
        {
            get { return dbName; }
        }

        private DatabaseManager(string databaseName)
        {
            dbName = databaseName;
        }


        /// <summary>
        /// Adds parameters to a SQL-command, where parameter name and value (including data type if specified) 
        /// are being properly defined.
        /// </summary>
        /// <param name="cmd">The SQL-command who the parameters are to be added to.</param>
        /// <param name="rawData">Data containing the column names and belonging values.</param>
        /// <returns>Updated SQL-command containing parameters.</returns>
        private SqlCommand AddCmdParameters(SqlCommand cmd, Dictionary<string, string> rawData)
        {
            foreach (KeyValuePair<string, string> entry in rawData)
            {
                string datatype = entry.Value.Contains("@") ? entry.Value.Substring(0, 1) : "";
                string value = entry.Value.Contains("@") ? entry.Value.Substring(2) : entry.Value;

                switch (datatype)
                {
                    case "f":
                        cmd.Parameters.Add(new SqlParameter("@" + entry.Key, float.Parse(value)));
                        break;
                    default:
                        cmd.Parameters.Add(new SqlParameter("@" + entry.Key, value));
                        break;
                }
            }

            return cmd;
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

            AddCmdParameters(cmd, parameters);

            cmd.ExecuteNonQuery();
            conn.Close();
        }


        /// <summary>
        /// Opens a SQL-connection and runs a query based on a stored procedure with parameters through it. 
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

            AddCmdParameters(cmd, parameters);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            conn.Close();

            return data;
        }


        /// <summary>
        /// Opens a SQL-connection and runs a query based on a stored procedure without parameters through it. 
        /// The query is being run on the back of a SQL-reader, which stores the returned data in a datatable.
        /// </summary>
        /// <param name="databaseName">The name of the database who's to be used (connectionstring name).</param>
        /// <param name="procedureName">The name of the stored procedure who's to be used.</param>
        /// <returns>A datatable if all of the database data which was returned by the stored procedure.</returns>
        public DataTable CallProcedureWithReturn(string databaseName, string procedureName)
        {
            SqlConnection conn = OpenDatabaseConnection(databaseName);
            SqlCommand cmd = new SqlCommand(procedureName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            conn.Close();

            return data;
        }


        /// <summary>
        /// Checks if the application can establish a connection to the local SQL-server and a certain database on it.
        /// </summary>
        /// <param name="databaseName">Name of the database which is to be used.</param>
        /// <returns>
        /// Index value indicating the connection status.
        /// 1 = could connect to the specified database.
        /// 2 = couldn't connect to the specified database.
        /// 3 = SQL-server is not accessible.
        /// </returns>
        public async Task<int> CheckForSqlConnection(string databaseName)
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
                    return 3;
                }

                SqlConnection testConn = OpenDatabaseConnection(databaseName);
                testConn.Open();
                testConn.Close();

                return 1;
            }
            catch
            {
                return 2;
            }
        }


        /// <summary>
        /// Sets up and configures a new Database instance on the SQL-server, based on SQL-files stored in the debug folder.
        /// </summary>
        /// <returns>Verification that the configuration finished successfully.</returns>
        public async Task<bool> ConfigureDatabase()
        {
            try
            {
                string sourcePath = Path.Combine(Environment.CurrentDirectory, @"SqlQueries\");
                string sqlQuery = File.ReadAllText(sourcePath + "setupDB.sql");
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

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
                    dictRow.Add(col.ColumnName, row[col].ToString().Trim());
                }

                result.Add(dictRow);
            }

            return result;
        }


        /// <summary>
        /// Fetches all of the Table-names currently present in the initiated database.
        /// </summary>
        /// <returns>A list containing all of the Table-names.</returns>
        public List<string> GetTableInformation()
        {
            List<Dictionary<string, string>> result = ConvertDataTableToDictionary(CallProcedureWithReturn(DbName, "GetAllTableNames"));

            List<string> values = new List<string>();

            foreach (Dictionary<string, string> el in result)
            {
                values.Add(el["TableName"]);
            }

            return values;
        }


        private SqlConnection OpenDatabaseConnection(string databaseName)
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[databaseName].ConnectionString);
        }
    }
}