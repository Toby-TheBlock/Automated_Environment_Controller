
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DataLogging = new System.Windows.Forms.TabPage();
            this.btnMeasurementNow = new System.Windows.Forms.Button();
            this.btnStartDL = new System.Windows.Forms.Button();
            this.pnlNextMeasurement = new System.Windows.Forms.Panel();
            this.pnlLastMeasurement = new System.Windows.Forms.Panel();
            this.btnStopDL = new System.Windows.Forms.Button();
            this.DataManagement = new System.Windows.Forms.TabPage();
            this.lblLastMeasurement = new System.Windows.Forms.Label();
            this.lblNextMeasurement = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.DataLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DataLogging);
            this.tabControl1.Controls.Add(this.DataManagement);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(600, 367);
            this.tabControl1.TabIndex = 0;
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
            this.DataLogging.Location = new System.Drawing.Point(4, 22);
            this.DataLogging.Name = "DataLogging";
            this.DataLogging.Padding = new System.Windows.Forms.Padding(3);
            this.DataLogging.Size = new System.Drawing.Size(592, 341);
            this.DataLogging.TabIndex = 0;
            this.DataLogging.Text = "Data Logging";
            this.DataLogging.UseVisualStyleBackColor = true;
            // 
            // btnMeasurementNow
            // 
            this.btnMeasurementNow.Location = new System.Drawing.Point(303, 17);
            this.btnMeasurementNow.Name = "btnMeasurementNow";
            this.btnMeasurementNow.Size = new System.Drawing.Size(134, 23);
            this.btnMeasurementNow.TabIndex = 8;
            this.btnMeasurementNow.Text = "Take Measurement Now";
            this.btnMeasurementNow.UseVisualStyleBackColor = true;
            // 
            // btnStartDL
            // 
            this.btnStartDL.Location = new System.Drawing.Point(14, 17);
            this.btnStartDL.Name = "btnStartDL";
            this.btnStartDL.Size = new System.Drawing.Size(134, 23);
            this.btnStartDL.TabIndex = 0;
            this.btnStartDL.Text = "Start Data Logging";
            this.btnStartDL.UseVisualStyleBackColor = true;
            this.btnStartDL.Click += new System.EventHandler(this.btnStartDL_Click);
            // 
            // pnlNextMeasurement
            // 
            this.pnlNextMeasurement.AccessibleName = "next";
            this.pnlNextMeasurement.Location = new System.Drawing.Point(303, 77);
            this.pnlNextMeasurement.Name = "pnlNextMeasurement";
            this.pnlNextMeasurement.Size = new System.Drawing.Size(274, 89);
            this.pnlNextMeasurement.TabIndex = 7;
            this.pnlNextMeasurement.Visible = false;
            // 
            // pnlLastMeasurement
            // 
            this.pnlLastMeasurement.AccessibleName = "last";
            this.pnlLastMeasurement.Location = new System.Drawing.Point(14, 77);
            this.pnlLastMeasurement.Name = "pnlLastMeasurement";
            this.pnlLastMeasurement.Size = new System.Drawing.Size(274, 89);
            this.pnlLastMeasurement.TabIndex = 6;
            this.pnlLastMeasurement.Visible = false;
            // 
            // btnStopDL
            // 
            this.btnStopDL.Location = new System.Drawing.Point(154, 17);
            this.btnStopDL.Name = "btnStopDL";
            this.btnStopDL.Size = new System.Drawing.Size(134, 23);
            this.btnStopDL.TabIndex = 1;
            this.btnStopDL.Text = "Stop Data Logging";
            this.btnStopDL.UseVisualStyleBackColor = true;
            this.btnStopDL.Click += new System.EventHandler(this.btnStopDL_Click);
            // 
            // DataManagement
            // 
            this.DataManagement.Location = new System.Drawing.Point(4, 22);
            this.DataManagement.Name = "DataManagement";
            this.DataManagement.Padding = new System.Windows.Forms.Padding(3);
            this.DataManagement.Size = new System.Drawing.Size(592, 341);
            this.DataManagement.TabIndex = 1;
            this.DataManagement.Text = "Data Management";
            this.DataManagement.UseVisualStyleBackColor = true;
            // 
            // lblLastMeasurement
            // 
            this.lblLastMeasurement.AutoSize = true;
            this.lblLastMeasurement.Location = new System.Drawing.Point(103, 61);
            this.lblLastMeasurement.Name = "lblLastMeasurement";
            this.lblLastMeasurement.Size = new System.Drawing.Size(94, 13);
            this.lblLastMeasurement.TabIndex = 10;
            this.lblLastMeasurement.Text = "Last Measurement";
            // 
            // lblNextMeasurement
            // 
            this.lblNextMeasurement.AutoSize = true;
            this.lblNextMeasurement.Location = new System.Drawing.Point(395, 61);
            this.lblNextMeasurement.Name = "lblNextMeasurement";
            this.lblNextMeasurement.Size = new System.Drawing.Size(96, 13);
            this.lblNextMeasurement.TabIndex = 11;
            this.lblNextMeasurement.Text = "Next Measurement";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.DataLogging.ResumeLayout(false);
            this.DataLogging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage DataLogging;
        private System.Windows.Forms.Button btnStopDL;
        private System.Windows.Forms.Button btnStartDL;
        private System.Windows.Forms.TabPage DataManagement;
        private System.Windows.Forms.Panel pnlNextMeasurement;
        private System.Windows.Forms.Panel pnlLastMeasurement;
        private System.Windows.Forms.Button btnMeasurementNow;
        private System.Windows.Forms.Label lblNextMeasurement;
        private System.Windows.Forms.Label lblLastMeasurement;
    }
}

