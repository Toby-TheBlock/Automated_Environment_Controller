using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Data_Logging_and_Management_Application
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbm = DatabaseManager.Singleton;
        private Dictionary<string, string> selectParametersForEntryToUpdate;
        private Timer dataloggingTimer;

        public MainForm()
        {
            InitializeComponent();
            TopMost = true;
            Activate();

            dataloggingTimer = new Timer();
            dataloggingTimer.Interval = 3333;
            dataloggingTimer.Tick += ReadyDataLoggingTab;
        }

        /// <summary>
        /// Disables/Enables the access to the current textboxes present on the textbox-panel based on which query-operation is being executed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlockTextBoxAccess(Object sender, EventArgs e)
        {
            bool accessState = true;

            switch (GetSelectedComboBoxValue("Operation"))
            {
                case "SELECT":
                case "DELETE":
                    if (((TextBox)sender).Text.Length != 0)
                    {
                        accessState = false;
                    }
                    break;

                case "UPDATE":
                    if (selectParametersForEntryToUpdate == null)
                    {
                        accessState = false;
                    }
                    break;
            }

            foreach (TextBox currentTextBox in pnlTxtBoxes.Controls.OfType<TextBox>())
            {
                if (currentTextBox != ((TextBox)sender))
                {
                    currentTextBox.Enabled = accessState;
                }
            }
        }

        /// <summary>
        /// Changes the Enabled-property of the provieded button based on different conditions.
        /// </summary>
        /// <param name="button">The button object whos access is to be changed.</param>
        private void ChangeBtnAccess(object button)
        {
            Button btn = (Button)button;
            btn.Enabled = false;

            switch (btn.Name)
            {
                case "btnStartDL":
                    btnStopDL.Enabled = true;
                    btnMeasurementNow.Enabled = true;
                    break;
                case "btnStopDL":
                    btnStartDL.Enabled = true;
                    btnMeasurementNow.Enabled = false;
                    break;
                case "btnMeasurementNow":
                    btnMeasurementNow.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Changes the Enabled-property of the "Submit-/Get-button" based on different conditions.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeBtnAccess(Object sender, EventArgs e)
        {
            btnGet.Enabled = false;
            btnSubmit.Enabled = false;

            if (CheckForNonEmptyValues(GetValuesFromTextBoxes()))
            {
                switch (GetSelectedComboBoxValue("Operation"))
                {
                    case "SELECT":
                        btnGet.Enabled = true;
                        break;
                    case "UPDATE":
                        Button btn = selectParametersForEntryToUpdate == null ? btnGet : btnSubmit;
                        btn.Enabled = true;
                        break;
                    case "DELETE":
                    case "INSERT":
                        btnSubmit.Enabled = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Looks through all of the entry values of a dictionary, in the search of non null-values. 
        /// </summary>
        /// <param name="dicToCheck">The dictionary how's to be checked.</param>
        /// <returns>Returns "true" if a non null-value has been found, otherwise returns "false".</returns>
        private bool CheckForNonEmptyValues(Dictionary<string, string> dicToCheck)
        {
            foreach (KeyValuePair<string, string> entry in dicToCheck)
            {
                if (entry.Value.Length != 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates all of the needed labels and textboxes needed to perform standard SQL SELECT, INSERT and DELETE queries.
        /// All of the controls are being added to the main panel. 
        /// </summary>
        /// <param name="rows">A list containing information about all of the table rows and columns.</param>
        private void CreateLabelAndInput(List<Dictionary<string, string>> rows)
        {
            int Xcoord = 0;

            foreach (Dictionary<string, string> row in rows)
            {
                string columnName = "";
                string lableText = "";
                string charMaxLength = "";

                foreach (KeyValuePair<string, string> entry in row)
                {
                    switch (entry.Key)
                    {
                        case "COLUMN_NAME":
                            columnName = entry.Value;
                            break;

                        case "IS_NULLABLE":
                            lableText = entry.Value == "NO" ? "* " + columnName : columnName;
                            break;

                        case "CHARACTER_MAXIMUM_LENGTH":
                            charMaxLength = entry.Value.Length <= 1 ? "1000" : entry.Value;
                            break;
                    }
                }

                Label newLabel = new Label()
                {
                    Name = "lblInput" + columnName,
                    Tag = charMaxLength,
                    Location = new Point(Xcoord, 5),
                    Size = new Size(120, 25),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = lableText
                };

                TextBox newTextBox = new TextBox()
                {
                    Name = "Input" + columnName,
                    Size = new Size(120, 25),
                    Location = new Point(Xcoord, 35),
                };

                newTextBox.TextChanged += BlockTextBoxAccess;
                newTextBox.TextChanged += ChangeBtnAccess;
                pnlTxtBoxes.Controls.Add(newLabel);
                pnlTxtBoxes.Controls.Add(newTextBox);
                Xcoord += 140;
            }
        }

        private void CreateDataLoggingPanelContent(Control element)
        {
            Point defLoc = new Point(15, 0);

            foreach (Sensor s in Sensor.allSensors)
            {
                defLoc = new Point(defLoc.X, defLoc.Y + 15);
                TextBox txt1 = new TextBox()
                {
                    TextAlign = HorizontalAlignment.Right,
                    BorderStyle = BorderStyle.None,
                    Text = s.GetType().Name + " " + s.SensorID + ":",
                    Size = new Size(110, 15),
                    Location = defLoc
                };

                TextBox txt2 = new TextBox()
                {
                    TextAlign = HorizontalAlignment.Left,
                    BorderStyle = BorderStyle.None,
                    Text = element.AccessibleName.Contains("last") ? s.LastMeasurementTimestamp : s.GetNextMeasuringTimestamp(),
                    Size = new Size(160, 15),
                    Location = new Point(defLoc.X + 115, defLoc.Y)
                };

                element.Controls.Add(txt1);
                element.Controls.Add(txt2);
            }
        }

        /// <summary>
        /// Creates LED objects based on the LED-configurationdata stored in the database.
        /// </summary>
        private void CreateLEDObjects()
        {
            foreach (Dictionary<string, string> el in LED.GetStoredLEDInformation())
            {
                LED newLED = el["ChanIdentifier"] == "Dev1/port0/line0" ? new LED(int.Parse(el["LED_ID"]), el["ChanIdentifier"], true) : new LED(int.Parse(el["LED_ID"]), el["ChanIdentifier"]);
                newLED.StartNewChannelThread();

                LED.allLEDs.Add(newLED);
            }
        }

        /// <summary>
        /// Creates Sensor objects based on the sensor-configurationdata stored in the database.
        /// </summary>
        private void CreateSensorObjects()
        {
            foreach (Dictionary<string, string> el in Sensor.GetStoredSensorInformation())
            {
                Sensor newSensor;

                switch (el["SensorType"])
                {
                    case "Light level":
                        newSensor = new LightSensor(int.Parse(el["SensorID"]), float.Parse(el["VoltageRating"]), int.Parse(el["MeasureFrequency"]), el["ChanIdentifier"]);
                        break;
                    case "Temperature":
                        newSensor = new TemperatureSensor(int.Parse(el["SensorID"]), float.Parse(el["VoltageRating"]), int.Parse(el["MeasureFrequency"]), el["ChanIdentifier"]);
                        break;
                    default:
                        throw new Exception("The given SensorType " + el["SensorType"] + " does not have an implemented Class.");
                }

                Sensor.allSensors.Add(newSensor);
            }
        }

        /// <summary>
        /// Calls the stored SQL-procedures related to the SELECT and UPDATE operations by using
        /// the provided user input. Receives and then handles the returned data accordingly.
        /// </summary>
        private void GetData()
        {
            Dictionary<string, string> parameters = GetParameters("Table");

            if (GetSelectedComboBoxValue("Operation") == "SELECT")
            {
                string procedure = parameters.Count == 2 ? "SelectAllFromTable" : "SelectSpecificFromTable";
                dgvMainWindow.DataSource = dbm.CallProcedureWithReturn(dbm.DbName, procedure, parameters);
            }
            else
            {
                DataTable rawData = dbm.CallProcedureWithReturn(dbm.DbName, "SelectSpecificFromTable", parameters);

                if (rawData.Rows.Count > 0)
                {
                    selectParametersForEntryToUpdate = parameters;
                    InsertValuesIntoTextBoxes(dbm.ConvertDataTableToDictionary(rawData).First());
                }
                else
                {
                    MessageBox.Show("Couldn't find and entry with the provided information.");
                }
            }
        }

        /// <summary>
        /// Creates a dictionary with all of the necessary data, 
        /// based on the current query-operation, which is ready for use in the execution of a stored sql procedure.
        /// </summary>
        /// <returns>Dictionary which contains entries ready to be parameterized.</returns>
        private Dictionary<string, string> GetParameters(string type)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>() { { "TableName", GetSelectedComboBoxValue(type) } };
            int counter = 1;

            foreach (KeyValuePair<string, string> entry in GetValuesFromTextBoxes())
            {
                string currentOperation = GetSelectedComboBoxValue("Operation");
                if (currentOperation == "INSERT" || (currentOperation == "UPDATE" && selectParametersForEntryToUpdate != null))
                {
                    parameters.Add("ColumnName" + counter, entry.Key);
                    parameters.Add("Value" + counter, entry.Value);
                    counter++;
                }
                else if (entry.Value.Length > 0)
                {
                    if (entry.Key == "NumberOfRows")
                    {
                        parameters.Add(entry.Key, entry.Value);
                    }
                    else
                    {
                        parameters.Add("ColumnName", entry.Key);
                        parameters.Add("SearchData", entry.Value);
                    }
                }
            }

            return parameters;
        }

        /// <summary>
        /// Gets the currently selected index-value from the specified combobox.
        /// </summary>
        /// <param name="cbo">Identifier of the combobox whos selected index is to be returned.</param>
        /// <returns>The text value of the selected index.</returns>
        private string GetSelectedComboBoxValue(string cbo)
        {
            string result = "";
            switch (cbo)
            {
                case "Table":
                    result = cboDbTables.Text;
                    break;
                case "Operation":
                    result = cboSqlOperation.Text;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets all of the userinput from the textboxes in the main panel.
        /// </summary>
        /// <returns>A dictionary where the key is the column name, and the value is the userinput.</returns>
        private Dictionary<string, string> GetValuesFromTextBoxes()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (Label currentLabel in pnlTxtBoxes.Controls.OfType<Label>())
            {
                foreach (TextBox currentTextBox in pnlTxtBoxes.Controls.OfType<TextBox>())
                {
                    if (currentLabel.Name.Contains(currentTextBox.Name.Substring(5)))
                    {
                        data.Add(currentTextBox.Name.Substring(5), currentTextBox.Text);
                        break;
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// Adds a value to the text-property of the textboxes present in the main-panel
        /// </summary>
        /// <param name="data">A dictionary with keys identifying the textboxes to be used, 
        /// and values to be put into said textboxes.</param>
        private void InsertValuesIntoTextBoxes(Dictionary<string, string> data)
        {
            foreach (KeyValuePair<string, string> entry in data)
            {
                foreach (TextBox currentTextBox in pnlTxtBoxes.Controls.OfType<TextBox>())
                {
                    if (entry.Key.Contains(currentTextBox.Name.Substring(5)))
                    {
                        currentTextBox.Text = entry.Value;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the initial state of the DataLogging-Tab.
        /// </summary>
        /// <param name="panelStatus"></param>
        public void ReadyDataLoggingTab(bool panelStatus)
        {
            foreach (Control element in DataLogging.Controls.OfType<Panel>())
            {
                element.Controls.Clear();
                element.Visible = panelStatus;

                if (panelStatus)
                {
                    CreateDataLoggingPanelContent(element);
                }
            }
        }

        /// <summary>
        /// Sets the initial state of the DataLogging-Tab.
        /// </summary>
        /// <param name="panelStatus"></param>
        public void ReadyDataLoggingTab(Object sender, EventArgs e)
        {
            foreach (Control element in DataLogging.Controls.OfType<Panel>())
            {
                element.Controls.Clear();
                element.Visible = true;

                CreateDataLoggingPanelContent(element);
            }
        }


        /// <summary>
        /// Sets the initial state of the DataManagement-Tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadyDataManagementTab(Object sender, EventArgs e)
        {
            selectParametersForEntryToUpdate = null;
            ChangeBtnAccess(sender, e);

            pnlTxtBoxes.Controls.Clear();

            DataTable rawData = dbm.CallProcedureWithReturn(dbm.DbName, "GetTableInfo", new Dictionary<string, string>() { { "TableName", GetSelectedComboBoxValue("Table") } });
            List<Dictionary<string, string>> data = dbm.ConvertDataTableToDictionary(rawData);

            /// The metadata for the manually created "NumberOfRows" column must be added to the data-list, 
            /// as this is not a real column in the database.
            if (GetSelectedComboBoxValue("Operation") == "SELECT")
            {
                data.Add(new Dictionary<string, string>() { { "COLUMN_NAME", "NumberOfRows" }, { "IS_NULLABLE", "YES" }, { "CHARACTER_MAXIMUM_LENTGH", "" } });
            }

            CreateLabelAndInput(data);
        }

        /// <summary>
        /// Fills and selects the correct indexes of the combo-boxes present on the DataManagementTab.
        /// </summary>
        private void ReadyManagementComboBoxes()
        {
            List<string> tableData = dbm.GetTableInformation();
            
            foreach (string el in tableData)
            {
                cboDbTables.Items.Add(el);
            }

            cboDbTables.SelectedIndex = 2;
            cboSqlOperation.SelectedIndex = 0;
        }

        /// <summary>
        /// Calls the stored SQL-procedures related to the DELETE, INSERT ans UPDATE operations by using
        /// the provided user input. All user input is being checked before usage in order to prevent runtime errors. 
        /// </summary>
        private void SubmitData()
        {
            try
            {
                switch (GetSelectedComboBoxValue("Operation"))
                {
                    case "UPDATE":
                        ValidateInput();
                        Dictionary<string, string> updatePara = GetParameters("Table");
                        int columnAmount = (updatePara.Count - 1) / 2;

                        foreach (KeyValuePair<string, string> entry in selectParametersForEntryToUpdate)
                        {
                            if (entry.Key == "ColumnName" || entry.Key == "SearchData")
                            {
                                updatePara.Add(entry.Key, entry.Value);
                            }
                        }

                        dbm.CallProcedureWithoutReturn(dbm.DbName, "UpdateEntry" + columnAmount + "Columns", updatePara);
                        break;

                    case "DELETE":
                        dbm.CallProcedureWithoutReturn(dbm.DbName, "DeleteFromTable", GetParameters("Table"));
                        break;

                    case "INSERT":
                        ValidateInput();
                        Dictionary<string, string> insertPara = GetParameters("Table");
                        dbm.CallProcedureWithoutReturn(dbm.DbName, "InsertIntoTable" + (insertPara.Count - 1) / 2 + "Columns", insertPara);
                        break;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// Checks if all of the textboxes who are tied to a mandatory column have a value inside of them. 
        /// All of the provided values are also being check against there length, to ensure that they don't overstep their bounds. 
        /// An exception will be thrown if either of the to checks fail.
        /// </summary>
        /// <returns>Returns "true" only if all of the userinput is accepted.</returns>
        private bool ValidateInput()
        {
            foreach (Label currentLabel in pnlTxtBoxes.Controls.OfType<Label>())
            {
                string columnName = currentLabel.Text;

                foreach (TextBox currentTextBox in pnlTxtBoxes.Controls.OfType<TextBox>())
                {
                    if (columnName.Contains(currentTextBox.Name.Substring(5)))
                    {
                        if (currentTextBox.Text.Length == 0 && columnName.Contains("*"))
                        {
                            throw new Exception("\"" + currentLabel.Name.Substring(8) + "\" can't be empty, please provided a value and try again!");
                        }
                        else if (currentTextBox.Text.Length > Convert.ToInt32(currentLabel.Tag))
                        {
                            throw new Exception("The provided value for \"" + currentLabel.Name.Substring(8) + "\" can't be longer than " + Convert.ToInt32(currentLabel.Tag) + ". Please try again!");
                        }

                        break;
                    }
                }
            }

            return true;
        }

        private void btnStartDL_Click(object sender, EventArgs e)
        {
            ChangeBtnAccess(sender);
            CreateSensorObjects();
            CreateLEDObjects();
            ReadyDataLoggingTab(true);
            dataloggingTimer.Start();
        }

        private void btnStopDL_Click(object sender, EventArgs e)
        {
            foreach (LED led in LED.allLEDs)
            {
                led.TerminateChannelThread();
            }

            foreach (Sensor s in Sensor.allSensors)
            {
                s.TerminateChannelThread();
            }

            LED.allLEDs.Clear();
            Sensor.allSensors.Clear();

            ChangeBtnAccess(sender);
            ReadyDataLoggingTab(false);
            dataloggingTimer.Stop();
        }

        private void btnMeasurementNow_Click(object sender, EventArgs e)
        {
            foreach (Sensor s in Sensor.allSensors)
            {
                try
                {
                    s.GetData();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

            ReadyDataLoggingTab(true);
        }


        private void cboSqlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadyDataManagementTab(sender, e);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitData();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void cboDbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReadyDataManagementTab(sender, e);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            ReadyManagementComboBoxes();
        }
    }
}
