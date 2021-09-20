
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreenForm));
            this.picLoadingIcon = new System.Windows.Forms.PictureBox();
            this.lblInfoText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picLoadingIcon
            // 
            this.picLoadingIcon.BackColor = System.Drawing.Color.White;
            this.picLoadingIcon.Location = new System.Drawing.Point(-1, -2);
            this.picLoadingIcon.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picLoadingIcon.Name = "picLoadingIcon";
            this.picLoadingIcon.Size = new System.Drawing.Size(435, 405);
            this.picLoadingIcon.TabIndex = 0;
            this.picLoadingIcon.TabStop = false;
            this.picLoadingIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lblInfoText
            // 
            this.lblInfoText.AutoSize = true;
            this.lblInfoText.BackColor = System.Drawing.Color.White;
            this.lblInfoText.Location = new System.Drawing.Point(109, 110);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(0, 17);
            this.lblInfoText.TabIndex = 1;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 402);
            this.Controls.Add(this.lblInfoText);
            this.Controls.Add(this.picLoadingIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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