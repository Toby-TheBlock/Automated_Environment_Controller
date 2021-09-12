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
            }
            else
            {
                btnStartDL.Enabled = true;
            }
            
        }

        private void ReadyCollectionPanels(bool panelStatus)
        {
            foreach(Control element in DataLogging.Controls.OfType<Panel>())
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

        private void btnStartDL_Click(object sender, EventArgs e)
        {
            ChangeBtnAccess(sender);

            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "SENSOR" },
                { "NumberOfRows", "100" }
            };

            List<Dictionary<string, string>> sensorData = dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));

            foreach(Dictionary<string, string> el in sensorData)
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
    }
}
