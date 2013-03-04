namespace UltraSFV
{
	partial class AboutBox
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
			this.buttonOK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelVersion = new System.Windows.Forms.Label();
			this.linkLabelUrl = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelLocked = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.labelSkipped = new System.Windows.Forms.Label();
			this.labelBad = new System.Windows.Forms.Label();
			this.labelGood = new System.Windows.Forms.Label();
			this.labelBytes = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelTime = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.labelMissing = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonOK
			// 
			this.buttonOK.Location = new System.Drawing.Point(193, 217);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(75, 23);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(15, 185);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(167, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Copyright © 2008 Steven Benner.";
			// 
			// labelVersion
			// 
			this.labelVersion.AutoSize = true;
			this.labelVersion.BackColor = System.Drawing.Color.Transparent;
			this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelVersion.Location = new System.Drawing.Point(12, 9);
			this.labelVersion.Name = "labelVersion";
			this.labelVersion.Size = new System.Drawing.Size(40, 12);
			this.labelVersion.TabIndex = 3;
			this.labelVersion.Text = "v1.0.0.0";
			// 
			// linkLabelUrl
			// 
			this.linkLabelUrl.AutoSize = true;
			this.linkLabelUrl.BackColor = System.Drawing.Color.Transparent;
			this.linkLabelUrl.Location = new System.Drawing.Point(15, 222);
			this.linkLabelUrl.Name = "linkLabelUrl";
			this.linkLabelUrl.Size = new System.Drawing.Size(127, 13);
			this.linkLabelUrl.TabIndex = 4;
			this.linkLabelUrl.TabStop = true;
			this.linkLabelUrl.Text = "http://www.ultrasfv.com/";
			this.linkLabelUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelUrl_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Location = new System.Drawing.Point(15, 200);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.White;
			this.groupBox1.Controls.Add(this.labelMissing);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.labelTime);
			this.groupBox1.Controls.Add(this.labelLocked);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.labelSkipped);
			this.groupBox1.Controls.Add(this.labelBad);
			this.groupBox1.Controls.Add(this.labelGood);
			this.groupBox1.Controls.Add(this.labelBytes);
			this.groupBox1.Controls.Add(this.labelTotal);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(12, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(256, 79);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Statistics";
			// 
			// labelLocked
			// 
			this.labelLocked.AutoSize = true;
			this.labelLocked.Location = new System.Drawing.Point(75, 57);
			this.labelLocked.Name = "labelLocked";
			this.labelLocked.Size = new System.Drawing.Size(13, 13);
			this.labelLocked.TabIndex = 18;
			this.labelLocked.Text = "0";
			// 
			// label9
			// 
			this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label9.Location = new System.Drawing.Point(6, 57);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(66, 13);
			this.label9.TabIndex = 17;
			this.label9.Text = "Locked:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelSkipped
			// 
			this.labelSkipped.AutoSize = true;
			this.labelSkipped.Location = new System.Drawing.Point(197, 57);
			this.labelSkipped.Name = "labelSkipped";
			this.labelSkipped.Size = new System.Drawing.Size(13, 13);
			this.labelSkipped.TabIndex = 16;
			this.labelSkipped.Text = "0";
			// 
			// labelBad
			// 
			this.labelBad.AutoSize = true;
			this.labelBad.Location = new System.Drawing.Point(197, 31);
			this.labelBad.Name = "labelBad";
			this.labelBad.Size = new System.Drawing.Size(13, 13);
			this.labelBad.TabIndex = 15;
			this.labelBad.Text = "0";
			// 
			// labelGood
			// 
			this.labelGood.AutoSize = true;
			this.labelGood.Location = new System.Drawing.Point(197, 18);
			this.labelGood.Name = "labelGood";
			this.labelGood.Size = new System.Drawing.Size(13, 13);
			this.labelGood.TabIndex = 14;
			this.labelGood.Text = "0";
			// 
			// labelBytes
			// 
			this.labelBytes.AutoSize = true;
			this.labelBytes.Location = new System.Drawing.Point(75, 31);
			this.labelBytes.Name = "labelBytes";
			this.labelBytes.Size = new System.Drawing.Size(13, 13);
			this.labelBytes.TabIndex = 13;
			this.labelBytes.Text = "0";
			// 
			// labelTotal
			// 
			this.labelTotal.AutoSize = true;
			this.labelTotal.Location = new System.Drawing.Point(75, 18);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(13, 13);
			this.labelTotal.TabIndex = 12;
			this.labelTotal.Text = "0";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Location = new System.Drawing.Point(6, 31);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(66, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "Bytes:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(128, 57);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(66, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Skipped:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(128, 31);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(66, 13);
			this.label5.TabIndex = 9;
			this.label5.Text = "Bad:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(128, 18);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(66, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Good:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.Location = new System.Drawing.Point(6, 18);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66, 13);
			this.label8.TabIndex = 7;
			this.label8.Text = "Total Files:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 252);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(280, 99);
			this.panel1.TabIndex = 8;
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.BackColor = System.Drawing.Color.Transparent;
			this.labelTime.Location = new System.Drawing.Point(75, 44);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(49, 13);
			this.labelTime.TabIndex = 9;
			this.labelTime.Text = "00:00:00";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(6, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Total Time:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label10
			// 
			this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label10.Location = new System.Drawing.Point(128, 44);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(66, 13);
			this.label10.TabIndex = 20;
			this.label10.Text = "Missing:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelMissing
			// 
			this.labelMissing.AutoSize = true;
			this.labelMissing.Location = new System.Drawing.Point(197, 44);
			this.labelMissing.Name = "labelMissing";
			this.labelMissing.Size = new System.Drawing.Size(13, 13);
			this.labelMissing.TabIndex = 21;
			this.labelMissing.Text = "0";
			// 
			// AboutBox
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(280, 351);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.linkLabelUrl);
			this.Controls.Add(this.labelVersion);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutBox";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "About UltraSFV";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelVersion;
		private System.Windows.Forms.LinkLabel linkLabelUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label labelLocked;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label labelSkipped;
		private System.Windows.Forms.Label labelBad;
		private System.Windows.Forms.Label labelGood;
		private System.Windows.Forms.Label labelBytes;
		private System.Windows.Forms.Label labelTotal;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelTime;
		private System.Windows.Forms.Label labelMissing;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label3;
	}
}