namespace UltraSFV
{
	partial class WorkingDialog
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkingDialog));
			this.buttonStop = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.labelRemain = new System.Windows.Forms.Label();
			this.labelChecked = new System.Windows.Forms.Label();
			this.labelTotal = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.labelPerformance = new System.Windows.Forms.Label();
			this.labelTime = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.labelFileName = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.labelFilePercentage = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.labelTotalPercentage = new System.Windows.Forms.Label();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(186, 135);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(75, 23);
			this.buttonStop.TabIndex = 1;
			this.buttonStop.Text = "&Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
			// 
			// buttonPause
			// 
			this.buttonPause.Location = new System.Drawing.Point(103, 135);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(75, 23);
			this.buttonPause.TabIndex = 0;
			this.buttonPause.Text = "&Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(9, 31);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(229, 15);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 2;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelRemain);
			this.groupBox1.Controls.Add(this.labelChecked);
			this.groupBox1.Controls.Add(this.labelTotal);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 173);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(250, 54);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Statistics";
			// 
			// labelRemain
			// 
			this.labelRemain.AutoSize = true;
			this.labelRemain.Location = new System.Drawing.Point(125, 30);
			this.labelRemain.Name = "labelRemain";
			this.labelRemain.Size = new System.Drawing.Size(13, 13);
			this.labelRemain.TabIndex = 9;
			this.labelRemain.Text = "0";
			// 
			// labelChecked
			// 
			this.labelChecked.AutoSize = true;
			this.labelChecked.Location = new System.Drawing.Point(125, 17);
			this.labelChecked.Name = "labelChecked";
			this.labelChecked.Size = new System.Drawing.Size(13, 13);
			this.labelChecked.TabIndex = 7;
			this.labelChecked.Text = "0";
			// 
			// labelTotal
			// 
			this.labelTotal.AutoSize = true;
			this.labelTotal.Location = new System.Drawing.Point(49, 17);
			this.labelTotal.Name = "labelTotal";
			this.labelTotal.Size = new System.Drawing.Size(13, 13);
			this.labelTotal.TabIndex = 6;
			this.labelTotal.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(80, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(46, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Remain:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(90, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Done:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Total:";
			// 
			// labelPerformance
			// 
			this.labelPerformance.AutoSize = true;
			this.labelPerformance.Location = new System.Drawing.Point(9, 147);
			this.labelPerformance.Name = "labelPerformance";
			this.labelPerformance.Size = new System.Drawing.Size(63, 13);
			this.labelPerformance.TabIndex = 5;
			this.labelPerformance.Text = "00.00 MB/s";
			this.labelPerformance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// labelTime
			// 
			this.labelTime.AutoSize = true;
			this.labelTime.Location = new System.Drawing.Point(9, 132);
			this.labelTime.Name = "labelTime";
			this.labelTime.Size = new System.Drawing.Size(49, 13);
			this.labelTime.TabIndex = 4;
			this.labelTime.Text = "00:00:00";
			this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(26, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "File:";
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(9, 31);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(229, 15);
			this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar2.TabIndex = 5;
			// 
			// labelFileName
			// 
			this.labelFileName.AutoEllipsis = true;
			this.labelFileName.Location = new System.Drawing.Point(32, 15);
			this.labelFileName.Name = "labelFileName";
			this.labelFileName.Size = new System.Drawing.Size(162, 13);
			this.labelFileName.TabIndex = 6;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(63, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Total Done:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.labelFilePercentage);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.progressBar2);
			this.groupBox2.Controls.Add(this.labelFileName);
			this.groupBox2.Location = new System.Drawing.Point(12, 5);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(248, 59);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			// 
			// labelFilePercentage
			// 
			this.labelFilePercentage.Location = new System.Drawing.Point(197, 15);
			this.labelFilePercentage.Name = "labelFilePercentage";
			this.labelFilePercentage.Size = new System.Drawing.Size(40, 13);
			this.labelFilePercentage.TabIndex = 7;
			this.labelFilePercentage.Text = "0%";
			this.labelFilePercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.labelTotalPercentage);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.progressBar1);
			this.groupBox3.Location = new System.Drawing.Point(12, 70);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(248, 59);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			// 
			// labelTotalPercentage
			// 
			this.labelTotalPercentage.Location = new System.Drawing.Point(197, 15);
			this.labelTotalPercentage.Name = "labelTotalPercentage";
			this.labelTotalPercentage.Size = new System.Drawing.Size(40, 13);
			this.labelTotalPercentage.TabIndex = 8;
			this.labelTotalPercentage.Text = "0%";
			this.labelTotalPercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 500;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// WorkingDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(273, 169);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.labelPerformance);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.labelTime);
			this.Controls.Add(this.buttonStop);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "WorkingDialog";
			this.RightToLeftLayout = true;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "UltraSFV - Processing";
			this.Load += new System.EventHandler(this.WorkingDialog_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WorkingDialog_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkingDialog_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonPause;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelRemain;
		private System.Windows.Forms.Label labelChecked;
		private System.Windows.Forms.Label labelTotal;
		private System.Windows.Forms.Label labelPerformance;
		private System.Windows.Forms.Label labelTime;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ProgressBar progressBar2;
		private System.Windows.Forms.Label labelFileName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label labelFilePercentage;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label labelTotalPercentage;
		private System.Windows.Forms.Timer timer2;
	}
}