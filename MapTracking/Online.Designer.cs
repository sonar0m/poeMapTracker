namespace poeMapTracking
{
    partial class Online
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
            this.BaseSplit = new System.Windows.Forms.SplitContainer();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAuthToken = new System.Windows.Forms.Label();
            this.tbAuthToken = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.OpenLog = new System.Windows.Forms.OpenFileDialog();
            this.findConfig = new System.Windows.Forms.Button();
            this.tbClientLocation = new System.Windows.Forms.TextBox();
            this.lbChars = new System.Windows.Forms.Label();
            this.tbCharacter = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.BaseSplit)).BeginInit();
            this.BaseSplit.Panel1.SuspendLayout();
            this.BaseSplit.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BaseSplit
            // 
            this.BaseSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BaseSplit.Location = new System.Drawing.Point(0, 0);
            this.BaseSplit.Name = "BaseSplit";
            // 
            // BaseSplit.Panel1
            // 
            this.BaseSplit.Panel1.Controls.Add(this.tabSettings);
            this.BaseSplit.Size = new System.Drawing.Size(792, 273);
            this.BaseSplit.SplitterDistance = 264;
            this.BaseSplit.TabIndex = 0;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.tabPage1);
            this.tabSettings.Controls.Add(this.tabPage2);
            this.tabSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSettings.Location = new System.Drawing.Point(0, 0);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(264, 273);
            this.tabSettings.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(256, 247);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblAuthToken, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbAuthToken, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.findConfig, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbClientLocation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbChars, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbCharacter, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(250, 241);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblAuthToken
            // 
            this.lblAuthToken.AutoSize = true;
            this.lblAuthToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthToken.Location = new System.Drawing.Point(3, 0);
            this.lblAuthToken.Name = "lblAuthToken";
            this.lblAuthToken.Size = new System.Drawing.Size(85, 17);
            this.lblAuthToken.TabIndex = 0;
            this.lblAuthToken.Text = "Auth Token:";
            // 
            // tbAuthToken
            // 
            this.tbAuthToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAuthToken.Location = new System.Drawing.Point(128, 3);
            this.tbAuthToken.Name = "tbAuthToken";
            this.tbAuthToken.PasswordChar = '*';
            this.tbAuthToken.Size = new System.Drawing.Size(119, 20);
            this.tbAuthToken.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(256, 247);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // OpenLog
            // 
            this.OpenLog.FileName = "Client.txt";
            // 
            // findConfig
            // 
            this.findConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.findConfig.Location = new System.Drawing.Point(128, 30);
            this.findConfig.Name = "findConfig";
            this.findConfig.Size = new System.Drawing.Size(119, 21);
            this.findConfig.TabIndex = 2;
            this.findConfig.Text = "Find Config";
            this.findConfig.UseVisualStyleBackColor = true;
            this.findConfig.Click += new System.EventHandler(this.findConfig_Click);
            // 
            // tbClientLocation
            // 
            this.tbClientLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbClientLocation.Location = new System.Drawing.Point(3, 30);
            this.tbClientLocation.Name = "tbClientLocation";
            this.tbClientLocation.ReadOnly = true;
            this.tbClientLocation.Size = new System.Drawing.Size(119, 20);
            this.tbClientLocation.TabIndex = 3;
            // 
            // lbChars
            // 
            this.lbChars.AutoSize = true;
            this.lbChars.Location = new System.Drawing.Point(3, 54);
            this.lbChars.Name = "lbChars";
            this.lbChars.Size = new System.Drawing.Size(110, 13);
            this.lbChars.TabIndex = 4;
            this.lbChars.Text = "character one per line";
            // 
            // tbCharacter
            // 
            this.tbCharacter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCharacter.Location = new System.Drawing.Point(128, 57);
            this.tbCharacter.Multiline = true;
            this.tbCharacter.Name = "tbCharacter";
            this.tbCharacter.Size = new System.Drawing.Size(119, 150);
            this.tbCharacter.TabIndex = 5;
            // 
            // Online
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 273);
            this.Controls.Add(this.BaseSplit);
            this.Name = "Online";
            this.Text = "Online";
            this.BaseSplit.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BaseSplit)).EndInit();
            this.BaseSplit.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer BaseSplit;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblAuthToken;
        private System.Windows.Forms.TextBox tbAuthToken;
        private System.Windows.Forms.OpenFileDialog OpenLog;
        private System.Windows.Forms.Button findConfig;
        private System.Windows.Forms.TextBox tbClientLocation;
        private System.Windows.Forms.Label lbChars;
        private System.Windows.Forms.TextBox tbCharacter;
    }
}