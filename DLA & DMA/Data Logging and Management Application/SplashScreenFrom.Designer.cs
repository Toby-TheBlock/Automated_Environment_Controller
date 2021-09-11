
namespace Data_Logging_and_Management_Application
{
    partial class SplashScreenForm
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
            this.picLoadingIcon = new System.Windows.Forms.PictureBox();
            this.lblInfoText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picLoadingIcon
            // 
            this.picLoadingIcon.BackColor = System.Drawing.Color.White;
            this.picLoadingIcon.Location = new System.Drawing.Point(-1, -2);
            this.picLoadingIcon.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picLoadingIcon.Name = "picLoadingIcon";
            this.picLoadingIcon.Size = new System.Drawing.Size(326, 329);
            this.picLoadingIcon.TabIndex = 0;
            this.picLoadingIcon.TabStop = false;
            this.picLoadingIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lblInfoText
            // 
            this.lblInfoText.AutoSize = true;
            this.lblInfoText.BackColor = System.Drawing.Color.White;
            this.lblInfoText.Location = new System.Drawing.Point(82, 89);
            this.lblInfoText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(169, 13);
            this.lblInfoText.TabIndex = 1;
            this.lblInfoText.Text = "Setting up Database connection...";
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 327);
            this.Controls.Add(this.lblInfoText);
            this.Controls.Add(this.picLoadingIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "SplashScreenForm";
            this.Text = "Configuration";
            this.Activated += new System.EventHandler(this.SplashScreenForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLoadingIcon;
        private System.Windows.Forms.Label lblInfoText;
    }
}