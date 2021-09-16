
namespace Data_Logging_and_Management_Application
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.DataLogging = new System.Windows.Forms.TabPage();
            this.lblNextMeasurement = new System.Windows.Forms.Label();
            this.lblLastMeasurement = new System.Windows.Forms.Label();
            this.btnMeasurementNow = new System.Windows.Forms.Button();
            this.btnStartDL = new System.Windows.Forms.Button();
            this.pnlNextMeasurement = new System.Windows.Forms.Panel();
            this.pnlLastMeasurement = new System.Windows.Forms.Panel();
            this.btnStopDL = new System.Windows.Forms.Button();
            this.DataManagement = new System.Windows.Forms.TabPage();
            this.btnRunQuery = new System.Windows.Forms.Button();
            this.cboSensors = new System.Windows.Forms.ComboBox();
            this.lblSensorCbo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cboSqlOperation = new System.Windows.Forms.ComboBox();
            this.lblSqlOperation = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.DataLogging.SuspendLayout();
            this.DataManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.DataLogging);
            this.tabControl.Controls.Add(this.DataManagement);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 452);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // DataLogging
            // 
            this.DataLogging.Controls.Add(this.lblNextMeasurement);
            this.DataLogging.Controls.Add(this.lblLastMeasurement);
            this.DataLogging.Controls.Add(this.btnMeasurementNow);
            this.DataLogging.Controls.Add(this.btnStartDL);
            this.DataLogging.Controls.Add(this.pnlNextMeasurement);
            this.DataLogging.Controls.Add(this.pnlLastMeasurement);
            this.DataLogging.Controls.Add(this.btnStopDL);
            this.DataLogging.Location = new System.Drawing.Point(4, 25);
            this.DataLogging.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataLogging.Name = "DataLogging";
            this.DataLogging.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataLogging.Size = new System.Drawing.Size(792, 423);
            this.DataLogging.TabIndex = 0;
            this.DataLogging.Text = "Data Logging";
            this.DataLogging.UseVisualStyleBackColor = true;
            // 
            // lblNextMeasurement
            // 
            this.lblNextMeasurement.AutoSize = true;
            this.lblNextMeasurement.Location = new System.Drawing.Point(527, 75);
            this.lblNextMeasurement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNextMeasurement.Name = "lblNextMeasurement";
            this.lblNextMeasurement.Size = new System.Drawing.Size(126, 17);
            this.lblNextMeasurement.TabIndex = 11;
            this.lblNextMeasurement.Text = "Next Measurement";
            // 
            // lblLastMeasurement
            // 
            this.lblLastMeasurement.AutoSize = true;
            this.lblLastMeasurement.Location = new System.Drawing.Point(137, 75);
            this.lblLastMeasurement.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastMeasurement.Name = "lblLastMeasurement";
            this.lblLastMeasurement.Size = new System.Drawing.Size(125, 17);
            this.lblLastMeasurement.TabIndex = 10;
            this.lblLastMeasurement.Text = "Last Measurement";
            // 
            // btnMeasurementNow
            // 
            this.btnMeasurementNow.Enabled = false;
            this.btnMeasurementNow.Location = new System.Drawing.Point(404, 21);
            this.btnMeasurementNow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMeasurementNow.Name = "btnMeasurementNow";
            this.btnMeasurementNow.Size = new System.Drawing.Size(179, 28);
            this.btnMeasurementNow.TabIndex = 8;
            this.btnMeasurementNow.Text = "Take Measurement Now";
            this.btnMeasurementNow.UseVisualStyleBackColor = true;
            this.btnMeasurementNow.Click += new System.EventHandler(this.btnMeasurementNow_Click);
            // 
            // btnStartDL
            // 
            this.btnStartDL.Location = new System.Drawing.Point(19, 21);
            this.btnStartDL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStartDL.Name = "btnStartDL";
            this.btnStartDL.Size = new System.Drawing.Size(179, 28);
            this.btnStartDL.TabIndex = 0;
            this.btnStartDL.Text = "Start Data Logging";
            this.btnStartDL.UseVisualStyleBackColor = true;
            this.btnStartDL.Click += new System.EventHandler(this.btnStartDL_Click);
            // 
            // pnlNextMeasurement
            // 
            this.pnlNextMeasurement.AccessibleName = "next";
            this.pnlNextMeasurement.Location = new System.Drawing.Point(404, 95);
            this.pnlNextMeasurement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlNextMeasurement.Name = "pnlNextMeasurement";
            this.pnlNextMeasurement.Size = new System.Drawing.Size(365, 110);
            this.pnlNextMeasurement.TabIndex = 7;
            this.pnlNextMeasurement.Visible = false;
            // 
            // pnlLastMeasurement
            // 
            this.pnlLastMeasurement.AccessibleName = "last";
            this.pnlLastMeasurement.Location = new System.Drawing.Point(19, 95);
            this.pnlLastMeasurement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLastMeasurement.Name = "pnlLastMeasurement";
            this.pnlLastMeasurement.Size = new System.Drawing.Size(365, 110);
            this.pnlLastMeasurement.TabIndex = 6;
            this.pnlLastMeasurement.Visible = false;
            // 
            // btnStopDL
            // 
            this.btnStopDL.Enabled = false;
            this.btnStopDL.Location = new System.Drawing.Point(205, 21);
            this.btnStopDL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStopDL.Name = "btnStopDL";
            this.btnStopDL.Size = new System.Drawing.Size(179, 28);
            this.btnStopDL.TabIndex = 1;
            this.btnStopDL.Text = "Stop Data Logging";
            this.btnStopDL.UseVisualStyleBackColor = true;
            this.btnStopDL.Click += new System.EventHandler(this.btnStopDL_Click);
            // 
            // DataManagement
            // 
            this.DataManagement.Controls.Add(this.lblSqlOperation);
            this.DataManagement.Controls.Add(this.cboSqlOperation);
            this.DataManagement.Controls.Add(this.dataGridView1);
            this.DataManagement.Controls.Add(this.lblSensorCbo);
            this.DataManagement.Controls.Add(this.cboSensors);
            this.DataManagement.Controls.Add(this.btnRunQuery);
            this.DataManagement.Location = new System.Drawing.Point(4, 25);
            this.DataManagement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataManagement.Name = "DataManagement";
            this.DataManagement.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DataManagement.Size = new System.Drawing.Size(792, 423);
            this.DataManagement.TabIndex = 1;
            this.DataManagement.Text = "Data Management";
            this.DataManagement.UseVisualStyleBackColor = true;
            // 
            // btnRunQuery
            // 
            this.btnRunQuery.Location = new System.Drawing.Point(626, 385);
            this.btnRunQuery.Name = "btnRunQuery";
            this.btnRunQuery.Size = new System.Drawing.Size(144, 28);
            this.btnRunQuery.TabIndex = 0;
            this.btnRunQuery.Text = "Run Query";
            this.btnRunQuery.UseVisualStyleBackColor = true;
            // 
            // cboSensors
            // 
            this.cboSensors.FormattingEnabled = true;
            this.cboSensors.Location = new System.Drawing.Point(20, 48);
            this.cboSensors.Name = "cboSensors";
            this.cboSensors.Size = new System.Drawing.Size(159, 24);
            this.cboSensors.TabIndex = 1;
            // 
            // lblSensorCbo
            // 
            this.lblSensorCbo.AutoSize = true;
            this.lblSensorCbo.Location = new System.Drawing.Point(22, 24);
            this.lblSensorCbo.Name = "lblSensorCbo";
            this.lblSensorCbo.Size = new System.Drawing.Size(121, 17);
            this.lblSensorCbo.TabIndex = 2;
            this.lblSensorCbo.Text = "Available Sensors";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(750, 161);
            this.dataGridView1.TabIndex = 3;
            // 
            // cboSqlOperation
            // 
            this.cboSqlOperation.FormattingEnabled = true;
            this.cboSqlOperation.Items.AddRange(new object[] {
            "SELECT",
            "INSERT",
            "DELETE",
            "UPDATE"});
            this.cboSqlOperation.Location = new System.Drawing.Point(194, 48);
            this.cboSqlOperation.Name = "cboSqlOperation";
            this.cboSqlOperation.Size = new System.Drawing.Size(142, 24);
            this.cboSqlOperation.TabIndex = 4;
            // 
            // lblSqlOperation
            // 
            this.lblSqlOperation.AutoSize = true;
            this.lblSqlOperation.Location = new System.Drawing.Point(191, 24);
            this.lblSqlOperation.Name = "lblSqlOperation";
            this.lblSqlOperation.Size = new System.Drawing.Size(104, 17);
            this.lblSqlOperation.TabIndex = 5;
            this.lblSqlOperation.Text = "SQL-Operation";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tabControl.ResumeLayout(false);
            this.DataLogging.ResumeLayout(false);
            this.DataLogging.PerformLayout();
            this.DataManagement.ResumeLayout(false);
            this.DataManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage DataLogging;
        private System.Windows.Forms.Button btnStopDL;
        private System.Windows.Forms.Button btnStartDL;
        private System.Windows.Forms.TabPage DataManagement;
        private System.Windows.Forms.Panel pnlNextMeasurement;
        private System.Windows.Forms.Panel pnlLastMeasurement;
        private System.Windows.Forms.Button btnMeasurementNow;
        private System.Windows.Forms.Label lblNextMeasurement;
        private System.Windows.Forms.Label lblLastMeasurement;
        private System.Windows.Forms.Label lblSensorCbo;
        private System.Windows.Forms.ComboBox cboSensors;
        private System.Windows.Forms.Button btnRunQuery;
        private System.Windows.Forms.Label lblSqlOperation;
        private System.Windows.Forms.ComboBox cboSqlOperation;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

