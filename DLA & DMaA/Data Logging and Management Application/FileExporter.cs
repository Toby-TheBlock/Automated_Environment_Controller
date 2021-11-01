using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Data_Logging_and_Management_Application
{
    class FileExporter
    {
        private string defaultFileLoc;

        public FileExporter()
        {
            defaultFileLoc = Path.Combine(Environment.CurrentDirectory, @"ExportedFiles\");
        }

        public FileExporter(string defaultFileLoc)
        {
            this.defaultFileLoc = defaultFileLoc;
        }


        /// <summary>
        /// Gets the values stored in a DataTables columns and rows, and writes these to the desired filetype.
        /// </summary>
        /// <param name="dt">The DataTable containing all of the sensordata.</param>
        /// <param name="filename">The name that is to be given to the new file.</param>
        /// <param name="filetype">The filetype which either can be csv or txt.</param>
        public void WriteToFile(DataTable dt, string filename, string filetype)
        {
            try
            {
                // The following code comes from Devesh Omar with some modifications done to it.
                // https://www.c-sharpcorner.com/UploadFile/deveshomar/export-datatable-to-csv-using-extension-method/ 

                StreamWriter sw = new StreamWriter(defaultFileLoc + @"\" + filename + "." + filetype);
                string seperator = filetype.Contains("csv") ? "," : "\t";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnHeader = dt.Columns[i].ToString().Length < dt.Rows[0][i].ToString().Length && filetype.Contains("txt") ?
                        dt.Columns[i].ToString().PadRight(dt.Rows[i].ToString().Length) : dt.Columns[i].ToString();

                    sw.Write(columnHeader);
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(seperator);
                    }
                }

                sw.Write(sw.NewLine);

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString().Trim();

                            if (filetype.Contains("csv") && value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                            }
                            else if (filetype.Contains("txt"))
                            {
                                value = value.PadLeft(dt.Columns[i].ToString().Length);
                            }

                            sw.Write(value);
                        }

                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(seperator);
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch
            {
                throw new Exception("The provided file path is not valid/does not work!");
            }
        }
    }
}
