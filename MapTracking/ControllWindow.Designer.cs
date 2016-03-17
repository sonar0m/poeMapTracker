namespace poeMapTracking
{
    partial class ControllWindow
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.OnlineButton = new System.Windows.Forms.Button();
            this.Offline = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.OnlineButton);
            this.flowLayoutPanel1.Controls.Add(this.Offline);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(256, 63);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // OnlineButton
            // 
            this.OnlineButton.Location = new System.Drawing.Point(3, 3);
            this.OnlineButton.Name = "OnlineButton";
            this.OnlineButton.Size = new System.Drawing.Size(75, 23);
            this.OnlineButton.TabIndex = 0;
            this.OnlineButton.Text = "Online";
            this.OnlineButton.UseVisualStyleBackColor = true;
            this.OnlineButton.Click += new System.EventHandler(this.OnlineButton_Click);
            // 
            // Offline
            // 
            this.Offline.Location = new System.Drawing.Point(84, 3);
            this.Offline.Name = "Offline";
            this.Offline.Size = new System.Drawing.Size(75, 23);
            this.Offline.TabIndex = 1;
            this.Offline.Text = "Offline";
            this.Offline.UseVisualStyleBackColor = true;
            this.Offline.Click += new System.EventHandler(this.Offline_Click);
            // 
            // ControllWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 63);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ControllWindow";
            this.Text = "ControllWindow";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button OnlineButton;
        private System.Windows.Forms.Button Offline;
    }
}