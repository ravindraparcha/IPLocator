namespace IPLocator.Windows
{
    partial class DataUploader
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP4 = new System.Windows.Forms.TextBox();
            this.btn_IP4Browse = new System.Windows.Forms.Button();
            this.btn_CityBrowse = new System.Windows.Forms.Button();
            this.txtCityLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_uploadInDB = new System.Windows.Forms.Button();
            this.openFileDg = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP4 csv file";
            // 
            // txtIP4
            // 
            this.txtIP4.Location = new System.Drawing.Point(136, 24);
            this.txtIP4.Name = "txtIP4";
            this.txtIP4.Size = new System.Drawing.Size(232, 20);
            this.txtIP4.TabIndex = 1;
            // 
            // btn_IP4Browse
            // 
            this.btn_IP4Browse.Location = new System.Drawing.Point(374, 22);
            this.btn_IP4Browse.Name = "btn_IP4Browse";
            this.btn_IP4Browse.Size = new System.Drawing.Size(75, 23);
            this.btn_IP4Browse.TabIndex = 2;
            this.btn_IP4Browse.Text = "Browse";
            this.btn_IP4Browse.UseVisualStyleBackColor = true;
            this.btn_IP4Browse.Click += new System.EventHandler(this.btn_IP4Browse_Click);
            // 
            // btn_CityBrowse
            // 
            this.btn_CityBrowse.Location = new System.Drawing.Point(375, 62);
            this.btn_CityBrowse.Name = "btn_CityBrowse";
            this.btn_CityBrowse.Size = new System.Drawing.Size(75, 23);
            this.btn_CityBrowse.TabIndex = 5;
            this.btn_CityBrowse.Text = "Browse";
            this.btn_CityBrowse.UseVisualStyleBackColor = true;
            this.btn_CityBrowse.Click += new System.EventHandler(this.btn_CityBrowse_Click);
            // 
            // txtCityLocation
            // 
            this.txtCityLocation.Location = new System.Drawing.Point(136, 65);
            this.txtCityLocation.Name = "txtCityLocation";
            this.txtCityLocation.Size = new System.Drawing.Size(232, 20);
            this.txtCityLocation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP4 city location csv file";
            // 
            // btn_uploadInDB
            // 
            this.btn_uploadInDB.Location = new System.Drawing.Point(136, 96);
            this.btn_uploadInDB.Name = "btn_uploadInDB";
            this.btn_uploadInDB.Size = new System.Drawing.Size(135, 23);
            this.btn_uploadInDB.TabIndex = 6;
            this.btn_uploadInDB.Text = "Upload in database";
            this.btn_uploadInDB.UseVisualStyleBackColor = true;
            this.btn_uploadInDB.Click += new System.EventHandler(this.btn_uploadInDB_Click);
            // 
            // DataUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 131);
            this.Controls.Add(this.btn_uploadInDB);
            this.Controls.Add(this.btn_CityBrowse);
            this.Controls.Add(this.txtCityLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_IP4Browse);
            this.Controls.Add(this.txtIP4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DataUploader";
            this.Text = "Upload csv to database - Ravindra Parcha";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP4;
        private System.Windows.Forms.Button btn_IP4Browse;
        private System.Windows.Forms.Button btn_CityBrowse;
        private System.Windows.Forms.TextBox txtCityLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_uploadInDB;
        private System.Windows.Forms.OpenFileDialog openFileDg;
    }
}

