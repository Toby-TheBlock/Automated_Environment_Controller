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
        public MainForm()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Activate();
        }
    }
}
