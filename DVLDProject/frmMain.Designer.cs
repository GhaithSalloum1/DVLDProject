namespace DVLDProject
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsApplications,
            this.tsPeople,
            this.tsDrivers,
            this.tsUsers,
            this.tsAccountSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1340, 68);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsApplications
            // 
            this.tsApplications.Name = "tsApplications";
            this.tsApplications.Size = new System.Drawing.Size(117, 64);
            this.tsApplications.Text = "Applications";
            // 
            // tsPeople
            // 
            this.tsPeople.Image = global::DVLDProject.Properties.Resources.People_64;
            this.tsPeople.Name = "tsPeople";
            this.tsPeople.Size = new System.Drawing.Size(135, 64);
            this.tsPeople.Text = "People";
            this.tsPeople.Click += new System.EventHandler(this.tsPeople_Click);
            // 
            // tsDrivers
            // 
            this.tsDrivers.Name = "tsDrivers";
            this.tsDrivers.Size = new System.Drawing.Size(76, 64);
            this.tsDrivers.Text = "Drivers";
            // 
            // tsUsers
            // 
            this.tsUsers.Name = "tsUsers";
            this.tsUsers.Size = new System.Drawing.Size(65, 64);
            this.tsUsers.Text = "Users";
            // 
            // tsAccountSettings
            // 
            this.tsAccountSettings.Name = "tsAccountSettings";
            this.tsAccountSettings.Size = new System.Drawing.Size(153, 64);
            this.tsAccountSettings.Text = "Account Settings";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DVLDProject.Properties.Resources.cat_typing;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1340, 680);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsApplications;
        private System.Windows.Forms.ToolStripMenuItem tsDrivers;
        private System.Windows.Forms.ToolStripMenuItem tsUsers;
        private System.Windows.Forms.ToolStripMenuItem tsAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem tsPeople;
    }
}