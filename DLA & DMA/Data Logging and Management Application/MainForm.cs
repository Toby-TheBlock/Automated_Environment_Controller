using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Logging_and_Management_Application
{
    public partial class MainForm : Form
    {
        private DatabaseManager dbm = DatabaseManager.Singleton;

        public MainForm()
        {
            InitializeComponent();
            TopMost = true;
            Activate();
        }

        private void ChangeBtnAccess(object button)
        {
            Button btn = (Button)button;
            btn.Enabled = false;

            if (btn.Text.Contains("Start"))
            {    
                btnStopDL.Enabled = true;
                btnMeasurementNow.Enabled = true;
            }
            else
            {
                btnStartDL.Enabled = true;
                btnMeasurementNow.Enabled = false;
            }
            
        }

        private void ReadyManagementComboBoxes()
        {
            List<Dictionary<string, string>> sensorData = Sensor.GetStoredSensorInformation();

            foreach (Dictionary<string, string> el in sensorData)
            {
                cboSensors.Items.Add(el["SensorType"].Trim() + " " + el["SensorID"]);
            }

            cboSensors.SelectedIndex = 0;
            cboSqlOperation.SelectedIndex = 0;
        }

        private void ReadyCollectionPanels(bool panelStatus)
        {
            foreach (Control element in DataLogging.Controls.OfType<Panel>())
            {
                element.Controls.Clear();
                element.Visible = panelStatus;

                if (panelStatus)
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
                            Text = element.AccessibleName.Contains("last") ? s.LastMeasurement : s.GetNextMeasuringTimestamp(),
                            Size = new Size(160, 15),
                            Location = new Point(defLoc.X + 115, defLoc.Y)
                        };
                        
                        element.Controls.Add(txt1);
                        element.Controls.Add(txt2);
                    }
                }
            }
        }

        /// <summary>
        /// Creates all of the needed labels and textboxes needed to perform standard SQL SELECT, INSERT and DELETE queries.
        /// All of the controls are being added to the main panel. 
        /// </summary>
        /// <param name="rows">A list containing information about all of the table rows and columns.</param>
        private void CreateLabelAndInput(List<Dictionary<string, string>> rows)
        {
            int Xcoord = 10;

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
                            lableText = entry.Value == "NO" ? "* " + columnName + " :" : columnName + " :";
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
                    Location = new Point(Xcoord, 220),
                    Size = new Size(100, 25),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = lableText
                };

                TextBox newTextBox = new TextBox()
                {
                    Name = "Input" + columnName,
                    Size = new Size(100, 25),
                    Location = new Point(Xcoord, 250),
                };

                //newTextBox.TextChanged += BlockTextBoxAccess;
                //newTextBox.TextChanged += ToggleButtonStatus;
                tabControl.TabPages["DataManagement"].Controls.Add(newLabel);
                tabControl.TabPages["DataManagement"].Controls.Add(newTextBox);
                Xcoord += 115;
            }
        }


        private void btnStartDL_Click(object sender, EventArgs e)
        {
            ChangeBtnAccess(sender);

            List<Dictionary<string, string>> sensorData = Sensor.GetStoredSensorInformation();

            foreach (Dictionary<string, string> el in sensorData)
            {
                Sensor newSensor;

                if (el["SensorType"].Contains("Light level"))
                {
                    newSensor = new LightSensor(int.Parse(el["SensorID"]), el["Modell"], el["Producer"], float.Parse(el["VoltageRating"]), int.Parse(el["MeasureFrequency"]), el["ChanIdentifier"]);
                }
                else
                {
                    newSensor = new TemperatureSensor(int.Parse(el["SensorID"]), el["Modell"], el["Producer"], float.Parse(el["VoltageRating"]), int.Parse(el["MeasureFrequency"]), el["ChanIdentifier"]);
                }

                Sensor.allSensors.Add(newSensor);
            }

            ReadyCollectionPanels(true);
        }

        private void btnStopDL_Click(object sender, EventArgs e)
        {
            ChangeBtnAccess(sender);
            Sensor.allSensors.Clear();
            ReadyCollectionPanels(false);
        }

        private void btnMeasurementNow_Click(object sender, EventArgs e)
        {
            foreach (Sensor s in Sensor.allSensors)
            {
                s.GetData();
            }

            ReadyCollectionPanels(true);
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            ReadyManagementComboBoxes();

            DataTable rawData = dbm.CallProcedureWithReturn(dbm.DbName, "GetTableInfo", new Dictionary<string, string>() { { "TableName", "SENSOR" } });
            List<Dictionary<string, string>> data = dbm.ConvertDataTableToDictionary(rawData);

            CreateLabelAndInput(data);
        }
    }
}
