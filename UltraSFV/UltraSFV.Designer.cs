namespace UltraSFV
{
	partial class UltraSFV
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltraSFV));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cRCCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.directoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
			this.manualCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addCRCToNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sFVFromNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewCommentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.supportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.webSiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.licenseAgreementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutUltraSFVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnName = new System.Windows.Forms.ColumnHeader();
			this.columnFileHash = new System.Windows.Forms.ColumnHeader();
			this.columnFileSize = new System.Windows.Forms.ColumnHeader();
			this.columnStatus = new System.Windows.Forms.ColumnHeader();
			this.columnPath = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.copyResultToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.renameBadFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteSelectedFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.sFVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mD5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.cRCCheckToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(392, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem4,
            this.viewLogToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sFVToolStripMenuItem,
            this.mD5ToolStripMenuItem});
			this.newToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.folder_plus;
			this.newToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.newToolStripMenuItem.MouseEnter += new System.EventHandler(this.newToolStripMenuItem_MouseEnter);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.folder_open;
			this.openToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.openToolStripMenuItem.Text = "&Open...";
			this.openToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.openToolStripMenuItem.MouseEnter += new System.EventHandler(this.openToolStripMenuItem_MouseEnter);
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(151, 6);
			// 
			// viewLogToolStripMenuItem
			// 
			this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
			this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.viewLogToolStripMenuItem.Text = "View &Log";
			this.viewLogToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.viewLogToolStripMenuItem.MouseEnter += new System.EventHandler(this.viewLogToolStripMenuItem_MouseEnter);
			this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 6);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.x_red;
			this.quitToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
			this.quitToolStripMenuItem.Text = "E&xit";
			this.quitToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.quitToolStripMenuItem.MouseEnter += new System.EventHandler(this.quitToolStripMenuItem_MouseEnter);
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// cRCCheckToolStripMenuItem
			// 
			this.cRCCheckToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.directoryToolStripMenuItem,
            this.toolStripMenuItem9,
            this.manualCheckToolStripMenuItem});
			this.cRCCheckToolStripMenuItem.Name = "cRCCheckToolStripMenuItem";
			this.cRCCheckToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.cRCCheckToolStripMenuItem.Text = "&Verify";
			// 
			// fileToolStripMenuItem1
			// 
			this.fileToolStripMenuItem1.Image = global::UltraSFV.Properties.Resources.checkmark;
			this.fileToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
			this.fileToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.fileToolStripMenuItem1.Text = "&Files...";
			this.fileToolStripMenuItem1.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.fileToolStripMenuItem1.MouseEnter += new System.EventHandler(this.fileToolStripMenuItem1_MouseEnter);
			this.fileToolStripMenuItem1.Click += new System.EventHandler(this.filesToolStripMenuItem_Click);
			// 
			// directoryToolStripMenuItem
			// 
			this.directoryToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.checkbox;
			this.directoryToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.directoryToolStripMenuItem.Name = "directoryToolStripMenuItem";
			this.directoryToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.directoryToolStripMenuItem.Text = "&Directory...";
			this.directoryToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.directoryToolStripMenuItem.MouseEnter += new System.EventHandler(this.directoryToolStripMenuItem_MouseEnter);
			this.directoryToolStripMenuItem.Click += new System.EventHandler(this.directoryToolStripMenuItem_Click);
			// 
			// toolStripMenuItem9
			// 
			this.toolStripMenuItem9.Name = "toolStripMenuItem9";
			this.toolStripMenuItem9.Size = new System.Drawing.Size(149, 6);
			// 
			// manualCheckToolStripMenuItem
			// 
			this.manualCheckToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.clipboard_check;
			this.manualCheckToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.manualCheckToolStripMenuItem.Name = "manualCheckToolStripMenuItem";
			this.manualCheckToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.manualCheckToolStripMenuItem.Text = "&Manual Check";
			this.manualCheckToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.manualCheckToolStripMenuItem.MouseEnter += new System.EventHandler(this.manualCheckToolStripMenuItem_MouseEnter);
			this.manualCheckToolStripMenuItem.Click += new System.EventHandler(this.manualCheckToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCRCToNamesToolStripMenuItem,
            this.sFVFromNamesToolStripMenuItem,
            this.viewCommentsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem6,
            this.checkForUpdatesToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// addCRCToNamesToolStripMenuItem
			// 
			this.addCRCToNamesToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.document_lock;
			this.addCRCToNamesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.addCRCToNamesToolStripMenuItem.Name = "addCRCToNamesToolStripMenuItem";
			this.addCRCToNamesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.addCRCToNamesToolStripMenuItem.Text = "&Add CRC To Names";
			this.addCRCToNamesToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.addCRCToNamesToolStripMenuItem.MouseEnter += new System.EventHandler(this.addCRCToNamesToolStripMenuItem_MouseEnter);
			this.addCRCToNamesToolStripMenuItem.Click += new System.EventHandler(this.addCRCToNamesToolStripMenuItem_Click);
			// 
			// sFVFromNamesToolStripMenuItem
			// 
			this.sFVFromNamesToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.connect;
			this.sFVFromNamesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.sFVFromNamesToolStripMenuItem.Name = "sFVFromNamesToolStripMenuItem";
			this.sFVFromNamesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.sFVFromNamesToolStripMenuItem.Text = "&SFV From Names";
			this.sFVFromNamesToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.sFVFromNamesToolStripMenuItem.MouseEnter += new System.EventHandler(this.sFVFromNamesToolStripMenuItem_MouseEnter);
			this.sFVFromNamesToolStripMenuItem.Click += new System.EventHandler(this.sFVFromNamesToolStripMenuItem_Click);
			// 
			// viewCommentsToolStripMenuItem
			// 
			this.viewCommentsToolStripMenuItem.Enabled = false;
			this.viewCommentsToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.notepad;
			this.viewCommentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.viewCommentsToolStripMenuItem.Name = "viewCommentsToolStripMenuItem";
			this.viewCommentsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.viewCommentsToolStripMenuItem.Text = "View &Comments";
			this.viewCommentsToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.viewCommentsToolStripMenuItem.MouseEnter += new System.EventHandler(this.viewCommentsToolStripMenuItem_MouseEnter);
			this.viewCommentsToolStripMenuItem.Click += new System.EventHandler(this.viewCommentsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 6);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.options;
			this.optionsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.optionsToolStripMenuItem.MouseEnter += new System.EventHandler(this.optionsToolStripMenuItem_MouseEnter);
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(164, 6);
			// 
			// checkForUpdatesToolStripMenuItem
			// 
			this.checkForUpdatesToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.checkForUpdatesToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.refresh;
			this.checkForUpdatesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
			this.checkForUpdatesToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.checkForUpdatesToolStripMenuItem.Text = "Check For &Updates";
			this.checkForUpdatesToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.checkForUpdatesToolStripMenuItem.MouseEnter += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_MouseEnter);
			this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.supportToolStripMenuItem,
            this.webSiteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.licenseAgreementToolStripMenuItem,
            this.toolStripMenuItem5,
            this.aboutUltraSFVToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// helpToolStripMenuItem1
			// 
			this.helpToolStripMenuItem1.Image = global::UltraSFV.Properties.Resources.question;
			this.helpToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
			this.helpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.helpToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
			this.helpToolStripMenuItem1.Text = "Help &Contents";
			this.helpToolStripMenuItem1.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.helpToolStripMenuItem1.MouseEnter += new System.EventHandler(this.helpToolStripMenuItem1_MouseEnter);
			this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
			// 
			// supportToolStripMenuItem
			// 
			this.supportToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.twodocs;
			this.supportToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.supportToolStripMenuItem.Name = "supportToolStripMenuItem";
			this.supportToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.supportToolStripMenuItem.Text = "&Support";
			this.supportToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.supportToolStripMenuItem.MouseEnter += new System.EventHandler(this.supportToolStripMenuItem_MouseEnter);
			this.supportToolStripMenuItem.Click += new System.EventHandler(this.supportToolStripMenuItem_Click);
			// 
			// webSiteToolStripMenuItem
			// 
			this.webSiteToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.globe;
			this.webSiteToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.webSiteToolStripMenuItem.Name = "webSiteToolStripMenuItem";
			this.webSiteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.webSiteToolStripMenuItem.Text = "&Web Site";
			this.webSiteToolStripMenuItem.MouseEnter += new System.EventHandler(this.webSiteToolStripMenuItem_MouseEnter);
			this.webSiteToolStripMenuItem.Click += new System.EventHandler(this.webSiteToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 6);
			// 
			// licenseAgreementToolStripMenuItem
			// 
			this.licenseAgreementToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.document;
			this.licenseAgreementToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.licenseAgreementToolStripMenuItem.Name = "licenseAgreementToolStripMenuItem";
			this.licenseAgreementToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.licenseAgreementToolStripMenuItem.Text = "&License Agreement";
			this.licenseAgreementToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.licenseAgreementToolStripMenuItem.MouseEnter += new System.EventHandler(this.licenseAgreementToolStripMenuItem_MouseEnter);
			this.licenseAgreementToolStripMenuItem.Click += new System.EventHandler(this.licenseAgreementToolStripMenuItem_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(162, 6);
			// 
			// aboutUltraSFVToolStripMenuItem
			// 
			this.aboutUltraSFVToolStripMenuItem.Image = global::UltraSFV.Properties.Resources.info;
			this.aboutUltraSFVToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.aboutUltraSFVToolStripMenuItem.Name = "aboutUltraSFVToolStripMenuItem";
			this.aboutUltraSFVToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.aboutUltraSFVToolStripMenuItem.Text = "&About UltraSFV";
			this.aboutUltraSFVToolStripMenuItem.MouseLeave += new System.EventHandler(this.menuItem_MouseLeave);
			this.aboutUltraSFVToolStripMenuItem.MouseEnter += new System.EventHandler(this.aboutUltraSFVToolStripMenuItem_MouseEnter);
			this.aboutUltraSFVToolStripMenuItem.Click += new System.EventHandler(this.aboutUltraSFVToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 246);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(392, 22);
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(361, 17);
			this.toolStripStatusLabel1.Spring = true;
			this.toolStripStatusLabel1.Text = "Welcome to UltraSFV - Ultra Simple File Verification";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabelStatus
			// 
			this.toolStripStatusLabelStatus.AutoToolTip = true;
			this.toolStripStatusLabelStatus.Image = global::UltraSFV.Properties.Resources.go;
			this.toolStripStatusLabelStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
			this.toolStripStatusLabelStatus.Size = new System.Drawing.Size(16, 17);
			this.toolStripStatusLabelStatus.ToolTipText = "All tested files are OK.";
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnFileHash,
            this.columnFileSize,
            this.columnStatus,
            this.columnPath});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 24);
			this.listView1.Name = "listView1";
			this.listView1.ShowItemToolTips = true;
			this.listView1.Size = new System.Drawing.Size(392, 222);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
			this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
			// 
			// columnName
			// 
			this.columnName.Text = "File Name";
			this.columnName.Width = 175;
			// 
			// columnFileHash
			// 
			this.columnFileHash.Text = "File Hash";
			this.columnFileHash.Width = 80;
			// 
			// columnFileSize
			// 
			this.columnFileSize.Text = "File Size";
			this.columnFileSize.Width = 75;
			// 
			// columnStatus
			// 
			this.columnStatus.Text = "Status";
			this.columnStatus.Width = 110;
			// 
			// columnPath
			// 
			this.columnPath.Text = "Path";
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "check.gif");
			this.imageList1.Images.SetKeyName(1, "x.gif");
			this.imageList1.Images.SetKeyName(2, "no.gif");
			this.imageList1.Images.SetKeyName(3, "lock.gif");
			this.imageList1.Images.SetKeyName(4, "caution.gif");
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripMenuItem8,
            this.copyResultToClipboardToolStripMenuItem,
            this.copyAllToClipboardToolStripMenuItem,
            this.toolStripMenuItem7,
            this.renameBadFilesToolStripMenuItem,
            this.deleteSelectedFilesToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.ShowImageMargin = false;
			this.contextMenuStrip1.Size = new System.Drawing.Size(182, 126);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.selectAllToolStripMenuItem.Text = "&Select All";
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(178, 6);
			// 
			// copyResultToClipboardToolStripMenuItem
			// 
			this.copyResultToClipboardToolStripMenuItem.Name = "copyResultToClipboardToolStripMenuItem";
			this.copyResultToClipboardToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.copyResultToClipboardToolStripMenuItem.Text = "Copy Selected To Clipboard";
			// 
			// copyAllToClipboardToolStripMenuItem
			// 
			this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
			this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.copyAllToClipboardToolStripMenuItem.Text = "Copy All To Clipboard";
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(178, 6);
			// 
			// renameBadFilesToolStripMenuItem
			// 
			this.renameBadFilesToolStripMenuItem.Name = "renameBadFilesToolStripMenuItem";
			this.renameBadFilesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.renameBadFilesToolStripMenuItem.Text = "Rename Bad File(s)";
			// 
			// deleteSelectedFilesToolStripMenuItem
			// 
			this.deleteSelectedFilesToolStripMenuItem.Name = "deleteSelectedFilesToolStripMenuItem";
			this.deleteSelectedFilesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.deleteSelectedFilesToolStripMenuItem.Text = "&Delete Selected File(s)";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "All Files|*.*";
			this.openFileDialog1.Multiselect = true;
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.Description = "Select the folder containing the files you would like to CRC check.";
			this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "sfv";
			this.saveFileDialog1.Filter = "SFV File|*.sfv";
			this.saveFileDialog1.Title = "Enter the file name to save the SFV results";
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// sFVToolStripMenuItem
			// 
			this.sFVToolStripMenuItem.Name = "sFVToolStripMenuItem";
			this.sFVToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.sFVToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.sFVToolStripMenuItem.Text = "&SFV File";
			this.sFVToolStripMenuItem.Click += new System.EventHandler(this.sFVToolStripMenuItem_Click);
			// 
			// mD5ToolStripMenuItem
			// 
			this.mD5ToolStripMenuItem.Name = "mD5ToolStripMenuItem";
			this.mD5ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.mD5ToolStripMenuItem.Text = "&MD5 File";
			this.mD5ToolStripMenuItem.Click += new System.EventHandler(this.mD5ToolStripMenuItem_Click);
			// 
			// UltraSFV
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 268);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "UltraSFV";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "UltraSFV";
			this.Load += new System.EventHandler(this.UltraSFV_Load);
			this.Shown += new System.EventHandler(this.UltraSFV_Shown);
			this.Activated += new System.EventHandler(this.UltraSFV_Activated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UltraSFV_FormClosed);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cRCCheckToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem directoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sFVFromNamesToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnName;
		private System.Windows.Forms.ColumnHeader columnFileHash;
		private System.Windows.Forms.ColumnHeader columnStatus;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem aboutUltraSFVToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem addCRCToNamesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem licenseAgreementToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ColumnHeader columnFileSize;
		private System.Windows.Forms.ColumnHeader columnPath;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
		private System.Windows.Forms.ToolStripMenuItem webSiteToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem deleteSelectedFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyResultToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.ToolStripMenuItem renameBadFilesToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripMenuItem viewCommentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem manualCheckToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sFVToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mD5ToolStripMenuItem;
	}
}