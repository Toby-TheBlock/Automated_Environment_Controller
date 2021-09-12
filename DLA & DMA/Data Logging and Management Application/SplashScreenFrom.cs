using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Data_Logging_and_Management_Application
{
    public partial class SplashScreenForm : Form
    {

        private LoadingIcon activeLoadingIcon;
        private DatabaseManager dbm = DatabaseManager.Singleton;
        private int connStatus = 0;
        private Point defaultTextLoc = new Point(70, 100);
        private Thread parallellThread;

        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private async void SetupDatabaseConnection()
        {
            int counter = 0;
            while (connStatus == 0)
            {
                if (parallellThread == null && counter > 20)
                {
                    StartNewThread();
                }

                picLoadingIcon.Refresh();
                Thread.Sleep(100);
                counter++;
            }

            switch (connStatus)
            {
                case 1:
                    Thread.Sleep(1000);
                    UpdateInfoText(connStatus);
                    Program.OpenMainForm = true;
                    break;

                case 2:
                    UpdateInfoText(connStatus);
                    Thread.Sleep(2000);

                    if (!await dbm.ConfigureDatabase())
                    {
                        MessageBox.Show("Something went wrong under the configuration of the database!");
                        Application.Exit();
                    }

                    Program.OpenMainForm = true;
                    break;

                case 3:
                    UpdateInfoText(connStatus);
                    break;
            }

            Thread.Sleep(2500);
            Application.Exit();
        }

        private void StartNewThread()
        {
            parallellThread = new Thread(GetConnectionStatus);
            parallellThread.IsBackground = true;
            parallellThread.Start();
        }

        /// <summary>
        /// Checks if the RGP_GAME database exists on the SQL-server. 
        /// Awaits the result and then merges the background thread back into the main thread.
        /// </summary>
        private async void GetConnectionStatus()
        {
            connStatus = await dbm.CheckForSqlConnection(dbm.DbName);
            parallellThread.Join();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (activeLoadingIcon == null)
            {
                activeLoadingIcon = new LoadingIcon(6, 0.34906585039, Color.Black, 10);
            }
            else
            {
                activeLoadingIcon.UpdateLoadingIcon(e.Graphics);
            }
        }


        
        /// <summary>
        /// Updates the info text label on the splash screen informing the user of what is happening.
        /// </summary>
        /// <param name="status">Integer indicating which status is to be shown.</param>
        public void UpdateInfoText(int status=0)
        {
            lblInfoText.Invalidate();
            string newText;
            Point newPosition;

            switch (status)
            {
                case 1:
                    newText = "Connection has successfully been established!";
                    newPosition = new Point(defaultTextLoc.X - 20, defaultTextLoc.Y);
                    break;

                case 2:
                    newText = "Couldn't connect to the " + dbm.DbName + " database.\n" +
                              "                    It doesn't exists on the SQL-server.\n" +
                              "       Setting up the missing DB and then trying again!\n";
                    newPosition = new Point(defaultTextLoc.X - 45, defaultTextLoc.Y - 15); ;
                    break;

                case 3:
                    newText = "                  Couldn't connect to the SQL-server.\n" +
                              " No SQL-server is running under: localhost\\SQLEXPRESS\n" +
                              "               Please start the SQL-server and try again.\n";
                    newPosition = new Point(defaultTextLoc.X - 50, defaultTextLoc.Y - 15);
                    break;

                default:
                    newText = "Setting up the Database connection...";
                    newPosition = defaultTextLoc;
                    break;
            }

            lblInfoText.Text = newText;
            lblInfoText.Location = newPosition;
            lblInfoText.Refresh();
        }


        private void SplashScreenForm_Activated(object sender, EventArgs e)
        {
            UpdateInfoText();
            SetupDatabaseConnection();
        }
    }
}
