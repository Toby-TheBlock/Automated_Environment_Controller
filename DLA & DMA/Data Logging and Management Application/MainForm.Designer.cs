
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.pnlTxtBoxes = new System.Windows.Forms.Panel();
            this.btnGet = new System.Windows.Forms.Button();
            this.lblDbTables = new System.Windows.Forms.Label();
            this.cboDbTables = new System.Windows.Forms.ComboBox();
            this.lblSqlOperation = new System.Windows.Forms.Label();
            this.cboSqlOperation = new System.Windows.Forms.ComboBox();
            this.dgvMainWindow = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.DataLogging.SuspendLayout();
            this.DataManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.DataLogging);
            this.tabControl.Controls.Add(this.DataManagement);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(855, 367);
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
            this.DataLogging.Location = new System.Drawing.Point(4, 22);
            this.DataLogging.Name = "DataLogging";
            this.DataLogging.Padding = new System.Windows.Forms.Padding(3);
            this.DataLogging.Size = new System.Drawing.Size(847, 341);
            this.DataLogging.TabIndex = 0;
            this.DataLogging.Text = "Data Logging";
            this.DataLogging.UseVisualStyleBackColor = true;
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
            // lblLastMeasurement
            // 
            this.lblLastMeasurement.AutoSize = true;
            this.lblLastMeasurement.Location = new System.Drawing.Point(103, 61);
            this.lblLastMeasurement.Name = "lblLastMeasurement";
            this.lblLastMeasurement.Size = new System.Drawing.Size(94, 13);
            this.lblLastMeasurement.TabIndex = 10;
            this.lblLastMeasurement.Text = "Last Measurement";
            // 
            // btnMeasurementNow
            // 
            this.btnMeasurementNow.Enabled = false;
            this.btnMeasurementNow.Location = new System.Drawing.Point(303, 17);
            this.btnMeasurementNow.Name = "btnMeasurementNow";
            this.btnMeasurementNow.Size = new System.Drawing.Size(134, 23);
            this.btnMeasurementNow.TabIndex = 8;
            this.btnMeasurementNow.Text = "Take Measurement Now";
            this.btnMeasurementNow.UseVisualStyleBackColor = true;
            this.btnMeasurementNow.Click += new System.EventHandler(this.btnMeasurementNow_Click);
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
            this.btnStopDL.Enabled = false;
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
            this.DataManagement.Controls.Add(this.pnlTxtBoxes);
            this.DataManagement.Controls.Add(this.btnGet);
            this.DataManagement.Controls.Add(this.lblDbTables);
            this.DataManagement.Controls.Add(this.cboDbTables);
            this.DataManagement.Controls.Add(this.lblSqlOperation);
            this.DataManagement.Controls.Add(this.cboSqlOperation);
            this.DataManagement.Controls.Add(this.dgvMainWindow);
            this.DataManagement.Controls.Add(this.btnSubmit);
            this.DataManagement.Location = new System.Drawing.Point(4, 22);
            this.DataManagement.Name = "DataManagement";
            this.DataManagement.Padding = new System.Windows.Forms.Padding(3);
            this.DataManagement.Size = new System.Drawing.Size(847, 341);
            this.DataManagement.TabIndex = 1;
            this.DataManagement.Text = "Data Management";
            this.DataManagement.UseVisualStyleBackColor = true;
            // 
            // pnlTxtBoxes
            // 
            this.pnlTxtBoxes.Location = new System.Drawing.Point(14, 210);
            this.pnlTxtBoxes.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTxtBoxes.Name = "pnlTxtBoxes";
            this.pnlTxtBoxes.Size = new System.Drawing.Size(820, 61);
            this.pnlTxtBoxes.TabIndex = 9;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(574, 286);
            this.btnGet.Margin = new System.Windows.Forms.Padding(2);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(122, 23);
            this.btnGet.TabIndex = 8;
            this.btnGet.Text = "Get Data";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // lblDbTables
            // 
            this.lblDbTables.AutoSize = true;
            this.lblDbTables.Location = new System.Drawing.Point(29, 18);
            this.lblDbTables.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDbTables.Name = "lblDbTables";
            this.lblDbTables.Size = new System.Drawing.Size(79, 13);
            this.lblDbTables.TabIndex = 7;
            this.lblDbTables.Text = "Selected Table";
            // 
            // cboDbTables
            // 
            this.cboDbTables.FormattingEnabled = true;
            this.cboDbTables.Location = new System.Drawing.Point(15, 39);
            this.cboDbTables.Margin = new System.Windows.Forms.Padding(2);
            this.cboDbTables.Name = "cboDbTables";
            this.cboDbTables.Size = new System.Drawing.Size(108, 21);
            this.cboDbTables.TabIndex = 6;
            this.cboDbTables.SelectedIndexChanged += new System.EventHandler(this.cboDbTables_SelectedIndexChanged);
            // 
            // lblSqlOperation
            // 
            this.lblSqlOperation.AutoSize = true;
            this.lblSqlOperation.Location = new System.Drawing.Point(140, 18);
            this.lblSqlOperation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSqlOperation.Name = "lblSqlOperation";
            this.lblSqlOperation.Size = new System.Drawing.Size(77, 13);
            this.lblSqlOperation.TabIndex = 5;
            this.lblSqlOperation.Text = "SQL Operation";
            // 
            // cboSqlOperation
            // 
            this.cboSqlOperation.FormattingEnabled = true;
            this.cboSqlOperation.Items.AddRange(new object[] {
            "SELECT",
            "INSERT",
            "DELETE",
            "UPDATE"});
            this.cboSqlOperation.Location = new System.Drawing.Point(126, 39);
            this.cboSqlOperation.Margin = new System.Windows.Forms.Padding(2);
            this.cboSqlOperation.Name = "cboSqlOperation";
            this.cboSqlOperation.Size = new System.Drawing.Size(108, 21);
            this.cboSqlOperation.TabIndex = 4;
            this.cboSqlOperation.SelectedIndexChanged += new System.EventHandler(this.cboSqlOperation_SelectedIndexChanged);
            // 
            // dgvMainWindow
            // 
            this.dgvMainWindow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainWindow.Location = new System.Drawing.Point(15, 74);
            this.dgvMainWindow.Margin = new System.Windows.Forms.Padding(2);
            this.dgvMainWindow.Name = "dgvMainWindow";
            this.dgvMainWindow.RowHeadersWidth = 51;
            this.dgvMainWindow.RowTemplate.Height = 24;
            this.dgvMainWindow.Size = new System.Drawing.Size(820, 123);
            this.dgvMainWindow.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(713, 286);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(122, 23);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit Data";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 341);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Data Logging and Management";
            this.tabControl.ResumeLayout(false);
            this.DataLogging.ResumeLayout(false);
            this.DataLogging.PerformLayout();
            this.DataManagement.ResumeLayout(false);
            this.DataManagement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainWindow)).EndInit();
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
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblSqlOperation;
        private System.Windows.Forms.ComboBox cboSqlOperation;
        private System.Windows.Forms.DataGridView dgvMainWindow;
        private System.Windows.Forms.Label lblDbTables;
        private System.Windows.Forms.ComboBox cboDbTables;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Panel pnlTxtBoxes;
    }
}

