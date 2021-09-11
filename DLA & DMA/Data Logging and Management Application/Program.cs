using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Logging_and_Management_Application
{
    static class Program
    {
        private static bool openMainForm = false;
        private static SplashScreenForm splashForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            splashForm = new SplashScreenForm();
            splashForm.Show();

            if (openMainForm)
            {
                Application.Run(new MainForm());
            }
            
        }

        public static bool OpenMainForm
        {
            set { openMainForm = value; }
        }

    }
}
