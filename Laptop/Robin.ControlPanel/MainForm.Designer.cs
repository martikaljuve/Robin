using System.Windows.Forms;

namespace Robin.ControlPanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.uxControlPanelLabel = new System.Windows.Forms.Label();
			this.uxPorts = new System.Windows.Forms.ComboBox();
			this.uxPortConnect = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.uxLogicFps = new System.Windows.Forms.Label();
			this.uxVisionFps = new System.Windows.Forms.Label();
			this.uxControllers = new System.Windows.Forms.ComboBox();
			this.uxSerialData = new System.Windows.Forms.TextBox();
			this.uxSerialPanel = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.uxGoalLabel = new System.Windows.Forms.Label();
			this.uxGoalNone = new System.Windows.Forms.RadioButton();
			this.uxGoalRed = new System.Windows.Forms.RadioButton();
			this.uxGoalBlue = new System.Windows.Forms.RadioButton();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uxSerialSendData = new System.Windows.Forms.TextBox();
			this.uxSerialSend = new System.Windows.Forms.Label();
			this.uxSerialReceive = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.uxVideoFile = new System.Windows.Forms.RadioButton();
			this.uxVideoWebcam = new System.Windows.Forms.RadioButton();
			this.uxVideoParameters = new System.Windows.Forms.PropertyGrid();
			this.uxShowVideo = new System.Windows.Forms.Button();
			this.uxIrChannel4 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel3 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel2 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel1 = new System.Windows.Forms.RadioButton();
			this.uxIrChannelNone = new System.Windows.Forms.RadioButton();
			this.uxIrChannelLabel = new System.Windows.Forms.Label();
			this.uxIrChannelPanel = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.uxSerialPanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.uxIrChannelPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.label1.Location = new System.Drawing.Point(105, 11);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5);
			this.label1.Size = new System.Drawing.Size(77, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "Robin";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// uxControlPanelLabel
			// 
			this.uxControlPanelLabel.AutoSize = true;
			this.uxControlPanelLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.tableLayoutPanel1.SetColumnSpan(this.uxControlPanelLabel, 5);
			this.uxControlPanelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxControlPanelLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxControlPanelLabel.ForeColor = System.Drawing.Color.White;
			this.uxControlPanelLabel.Location = new System.Drawing.Point(0, 62);
			this.uxControlPanelLabel.Margin = new System.Windows.Forms.Padding(0);
			this.uxControlPanelLabel.Name = "uxControlPanelLabel";
			this.uxControlPanelLabel.Padding = new System.Windows.Forms.Padding(5);
			this.uxControlPanelLabel.Size = new System.Drawing.Size(981, 27);
			this.uxControlPanelLabel.TabIndex = 2;
			this.uxControlPanelLabel.Text = "Control Panel";
			this.uxControlPanelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uxPorts
			// 
			this.uxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxPorts.FormattingEnabled = true;
			this.uxPorts.Location = new System.Drawing.Point(9, 35);
			this.uxPorts.Name = "uxPorts";
			this.uxPorts.Size = new System.Drawing.Size(170, 29);
			this.uxPorts.TabIndex = 2;
			// 
			// uxPortConnect
			// 
			this.uxPortConnect.AutoSize = true;
			this.uxPortConnect.Location = new System.Drawing.Point(188, 33);
			this.uxPortConnect.Name = "uxPortConnect";
			this.uxPortConnect.Size = new System.Drawing.Size(97, 31);
			this.uxPortConnect.TabIndex = 3;
			this.uxPortConnect.Text = "&Connect";
			this.uxPortConnect.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox1.Image = global::Robin.ControlPanel.Properties.Resources.robot;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(102, 62);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.uxLogicFps, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxControlPanelLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.uxVisionFps, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxControllers, 4, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(981, 89);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// uxLogicFps
			// 
			this.uxLogicFps.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.uxLogicFps.AutoSize = true;
			this.uxLogicFps.BackColor = System.Drawing.Color.Transparent;
			this.uxLogicFps.Location = new System.Drawing.Point(188, 24);
			this.uxLogicFps.Name = "uxLogicFps";
			this.uxLogicFps.Size = new System.Drawing.Size(23, 13);
			this.uxLogicFps.TabIndex = 6;
			this.uxLogicFps.Text = "fps";
			// 
			// uxVisionFps
			// 
			this.uxVisionFps.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.uxVisionFps.AutoSize = true;
			this.uxVisionFps.BackColor = System.Drawing.Color.Transparent;
			this.uxVisionFps.Location = new System.Drawing.Point(217, 24);
			this.uxVisionFps.Name = "uxVisionFps";
			this.uxVisionFps.Size = new System.Drawing.Size(23, 13);
			this.uxVisionFps.TabIndex = 6;
			this.uxVisionFps.Text = "fps";
			// 
			// uxControllers
			// 
			this.uxControllers.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.uxControllers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxControllers.FormattingEnabled = true;
			this.uxControllers.Location = new System.Drawing.Point(246, 20);
			this.uxControllers.Name = "uxControllers";
			this.uxControllers.Size = new System.Drawing.Size(224, 21);
			this.uxControllers.TabIndex = 7;
			// 
			// uxSerialData
			// 
			this.uxSerialData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxSerialData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.uxSerialData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uxSerialData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialData.ForeColor = System.Drawing.Color.White;
			this.uxSerialData.Location = new System.Drawing.Point(3, 24);
			this.uxSerialData.Multiline = true;
			this.uxSerialData.Name = "uxSerialData";
			this.uxSerialData.ReadOnly = true;
			this.uxSerialData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxSerialData.Size = new System.Drawing.Size(469, 131);
			this.uxSerialData.TabIndex = 6;
			this.uxSerialData.WordWrap = false;
			// 
			// uxSerialPanel
			// 
			this.uxSerialPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxSerialPanel.Controls.Add(this.flowLayoutPanel1);
			this.uxSerialPanel.Controls.Add(this.splitContainer1);
			this.uxSerialPanel.Controls.Add(this.uxIrChannelPanel);
			this.uxSerialPanel.Controls.Add(this.uxPorts);
			this.uxSerialPanel.Controls.Add(this.uxPortConnect);
			this.uxSerialPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxSerialPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialPanel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.uxSerialPanel.Location = new System.Drawing.Point(3, 3);
			this.uxSerialPanel.MinimumSize = new System.Drawing.Size(500, 200);
			this.uxSerialPanel.Name = "uxSerialPanel";
			this.uxSerialPanel.Size = new System.Drawing.Size(975, 241);
			this.uxSerialPanel.TabIndex = 0;
			this.uxSerialPanel.TabStop = false;
			this.uxSerialPanel.Text = "Arduino";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.flowLayoutPanel1.Controls.Add(this.uxGoalLabel);
			this.flowLayoutPanel1.Controls.Add(this.uxGoalNone);
			this.flowLayoutPanel1.Controls.Add(this.uxGoalRed);
			this.flowLayoutPanel1.Controls.Add(this.uxGoalBlue);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(291, 28);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(345, 43);
			this.flowLayoutPanel1.TabIndex = 14;
			// 
			// uxGoalLabel
			// 
			this.uxGoalLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxGoalLabel.AutoSize = true;
			this.uxGoalLabel.BackColor = System.Drawing.Color.Transparent;
			this.uxGoalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxGoalLabel.Location = new System.Drawing.Point(3, 8);
			this.uxGoalLabel.Name = "uxGoalLabel";
			this.uxGoalLabel.Size = new System.Drawing.Size(122, 20);
			this.uxGoalLabel.TabIndex = 11;
			this.uxGoalLabel.Text = "Opponent Goal:";
			// 
			// uxGoalNone
			// 
			this.uxGoalNone.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxGoalNone.AutoSize = true;
			this.uxGoalNone.BackColor = System.Drawing.Color.Transparent;
			this.uxGoalNone.Checked = true;
			this.uxGoalNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxGoalNone.Location = new System.Drawing.Point(131, 3);
			this.uxGoalNone.Name = "uxGoalNone";
			this.uxGoalNone.Padding = new System.Windows.Forms.Padding(3);
			this.uxGoalNone.Size = new System.Drawing.Size(71, 30);
			this.uxGoalNone.TabIndex = 13;
			this.uxGoalNone.TabStop = true;
			this.uxGoalNone.Text = "None";
			this.uxGoalNone.UseVisualStyleBackColor = false;
			// 
			// uxGoalRed
			// 
			this.uxGoalRed.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxGoalRed.AutoSize = true;
			this.uxGoalRed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
			this.uxGoalRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxGoalRed.Location = new System.Drawing.Point(208, 3);
			this.uxGoalRed.Name = "uxGoalRed";
			this.uxGoalRed.Padding = new System.Windows.Forms.Padding(3);
			this.uxGoalRed.Size = new System.Drawing.Size(63, 30);
			this.uxGoalRed.TabIndex = 12;
			this.uxGoalRed.Text = "Red";
			this.uxGoalRed.UseVisualStyleBackColor = false;
			// 
			// uxGoalBlue
			// 
			this.uxGoalBlue.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxGoalBlue.AutoSize = true;
			this.uxGoalBlue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
			this.uxGoalBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxGoalBlue.Location = new System.Drawing.Point(277, 3);
			this.uxGoalBlue.Name = "uxGoalBlue";
			this.uxGoalBlue.Padding = new System.Windows.Forms.Padding(3);
			this.uxGoalBlue.Size = new System.Drawing.Size(65, 30);
			this.uxGoalBlue.TabIndex = 10;
			this.uxGoalBlue.Text = "Blue";
			this.uxGoalBlue.UseVisualStyleBackColor = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(9, 77);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.uxSerialSendData);
			this.splitContainer1.Panel1.Controls.Add(this.uxSerialSend);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uxSerialData);
			this.splitContainer1.Panel2.Controls.Add(this.uxSerialReceive);
			this.splitContainer1.Size = new System.Drawing.Size(957, 158);
			this.splitContainer1.SplitterDistance = 478;
			this.splitContainer1.TabIndex = 1;
			// 
			// uxSerialSendData
			// 
			this.uxSerialSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxSerialSendData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.uxSerialSendData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uxSerialSendData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialSendData.ForeColor = System.Drawing.Color.White;
			this.uxSerialSendData.Location = new System.Drawing.Point(4, 24);
			this.uxSerialSendData.Multiline = true;
			this.uxSerialSendData.Name = "uxSerialSendData";
			this.uxSerialSendData.ReadOnly = true;
			this.uxSerialSendData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxSerialSendData.Size = new System.Drawing.Size(471, 131);
			this.uxSerialSendData.TabIndex = 14;
			this.uxSerialSendData.WordWrap = false;
			// 
			// uxSerialSend
			// 
			this.uxSerialSend.AutoSize = true;
			this.uxSerialSend.Location = new System.Drawing.Point(3, 0);
			this.uxSerialSend.Name = "uxSerialSend";
			this.uxSerialSend.Size = new System.Drawing.Size(48, 21);
			this.uxSerialSend.TabIndex = 15;
			this.uxSerialSend.Text = "Send:";
			// 
			// uxSerialReceive
			// 
			this.uxSerialReceive.AutoSize = true;
			this.uxSerialReceive.Location = new System.Drawing.Point(3, 0);
			this.uxSerialReceive.Name = "uxSerialReceive";
			this.uxSerialReceive.Size = new System.Drawing.Size(66, 21);
			this.uxSerialReceive.TabIndex = 16;
			this.uxSerialReceive.Text = "Receive:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoScroll = true;
			this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.uxSerialPanel, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 89);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(981, 440);
			this.tableLayoutPanel2.TabIndex = 15;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.uxVideoFile);
			this.groupBox1.Controls.Add(this.uxVideoWebcam);
			this.groupBox1.Controls.Add(this.uxVideoParameters);
			this.groupBox1.Controls.Add(this.uxShowVideo);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.groupBox1.Location = new System.Drawing.Point(3, 250);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(975, 187);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Vision Settings";
			// 
			// uxVideoFile
			// 
			this.uxVideoFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxVideoFile.AutoSize = true;
			this.uxVideoFile.Location = new System.Drawing.Point(812, 104);
			this.uxVideoFile.Name = "uxVideoFile";
			this.uxVideoFile.Size = new System.Drawing.Size(52, 25);
			this.uxVideoFile.TabIndex = 3;
			this.uxVideoFile.Text = "File";
			this.uxVideoFile.UseVisualStyleBackColor = true;
			// 
			// uxVideoWebcam
			// 
			this.uxVideoWebcam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxVideoWebcam.AutoSize = true;
			this.uxVideoWebcam.Checked = true;
			this.uxVideoWebcam.Location = new System.Drawing.Point(812, 73);
			this.uxVideoWebcam.Name = "uxVideoWebcam";
			this.uxVideoWebcam.Size = new System.Drawing.Size(89, 25);
			this.uxVideoWebcam.TabIndex = 2;
			this.uxVideoWebcam.TabStop = true;
			this.uxVideoWebcam.Text = "Webcam";
			this.uxVideoWebcam.UseVisualStyleBackColor = true;
			// 
			// uxVideoParameters
			// 
			this.uxVideoParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxVideoParameters.HelpVisible = false;
			this.uxVideoParameters.Location = new System.Drawing.Point(10, 28);
			this.uxVideoParameters.Name = "uxVideoParameters";
			this.uxVideoParameters.Size = new System.Drawing.Size(796, 150);
			this.uxVideoParameters.TabIndex = 1;
			this.uxVideoParameters.ToolbarVisible = false;
			this.uxVideoParameters.ViewBackColor = System.Drawing.SystemColors.Control;
			this.uxVideoParameters.ViewForeColor = System.Drawing.SystemColors.ControlText;
			// 
			// uxShowVideo
			// 
			this.uxShowVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxShowVideo.Location = new System.Drawing.Point(812, 28);
			this.uxShowVideo.Name = "uxShowVideo";
			this.uxShowVideo.Size = new System.Drawing.Size(154, 39);
			this.uxShowVideo.TabIndex = 0;
			this.uxShowVideo.Text = "Show &Video";
			this.uxShowVideo.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel4
			// 
			this.uxIrChannel4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel4.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel4.AutoSize = true;
			this.uxIrChannel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel4.Location = new System.Drawing.Point(262, 3);
			this.uxIrChannel4.Name = "uxIrChannel4";
			this.uxIrChannel4.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel4.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel4.TabIndex = 10;
			this.uxIrChannel4.Text = "4";
			this.uxIrChannel4.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel3
			// 
			this.uxIrChannel3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel3.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel3.AutoSize = true;
			this.uxIrChannel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel3.Location = new System.Drawing.Point(222, 3);
			this.uxIrChannel3.Name = "uxIrChannel3";
			this.uxIrChannel3.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel3.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel3.TabIndex = 9;
			this.uxIrChannel3.Text = "3";
			this.uxIrChannel3.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel2
			// 
			this.uxIrChannel2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel2.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel2.AutoSize = true;
			this.uxIrChannel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel2.Location = new System.Drawing.Point(182, 3);
			this.uxIrChannel2.Name = "uxIrChannel2";
			this.uxIrChannel2.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel2.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel2.TabIndex = 9;
			this.uxIrChannel2.Text = "2";
			this.uxIrChannel2.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel1
			// 
			this.uxIrChannel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel1.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel1.AutoSize = true;
			this.uxIrChannel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel1.Location = new System.Drawing.Point(142, 3);
			this.uxIrChannel1.Name = "uxIrChannel1";
			this.uxIrChannel1.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel1.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel1.TabIndex = 8;
			this.uxIrChannel1.Text = "1";
			this.uxIrChannel1.UseVisualStyleBackColor = true;
			// 
			// uxIrChannelNone
			// 
			this.uxIrChannelNone.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannelNone.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannelNone.AutoSize = true;
			this.uxIrChannelNone.Checked = true;
			this.uxIrChannelNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannelNone.Location = new System.Drawing.Point(102, 3);
			this.uxIrChannelNone.Name = "uxIrChannelNone";
			this.uxIrChannelNone.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannelNone.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannelNone.TabIndex = 12;
			this.uxIrChannelNone.TabStop = true;
			this.uxIrChannelNone.Text = "0";
			this.uxIrChannelNone.UseVisualStyleBackColor = true;
			// 
			// uxIrChannelLabel
			// 
			this.uxIrChannelLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannelLabel.AutoSize = true;
			this.uxIrChannelLabel.BackColor = System.Drawing.Color.Transparent;
			this.uxIrChannelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannelLabel.Location = new System.Drawing.Point(3, 11);
			this.uxIrChannelLabel.Name = "uxIrChannelLabel";
			this.uxIrChannelLabel.Size = new System.Drawing.Size(93, 20);
			this.uxIrChannelLabel.TabIndex = 11;
			this.uxIrChannelLabel.Text = "IR Channel:";
			// 
			// uxIrChannelPanel
			// 
			this.uxIrChannelPanel.AutoSize = true;
			this.uxIrChannelPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannelLabel);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannelNone);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel1);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel2);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel3);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel4);
			this.uxIrChannelPanel.Location = new System.Drawing.Point(642, 28);
			this.uxIrChannelPanel.Name = "uxIrChannelPanel";
			this.uxIrChannelPanel.Size = new System.Drawing.Size(304, 43);
			this.uxIrChannelPanel.TabIndex = 13;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(981, 529);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(600, 300);
			this.Name = "MainForm";
			this.Text = "Robin Control Panel";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.uxSerialPanel.ResumeLayout(false);
			this.uxSerialPanel.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.uxIrChannelPanel.ResumeLayout(false);
			this.uxIrChannelPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label uxControlPanelLabel;
		private System.Windows.Forms.ComboBox uxPorts;
		private System.Windows.Forms.Button uxPortConnect;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label uxLogicFps;
		private System.Windows.Forms.TextBox uxSerialData;
		private Label uxVisionFps;
		private GroupBox uxSerialPanel;
		private Label uxSerialSend;
		private TextBox uxSerialSendData;
		private Label uxSerialReceive;
		private ComboBox uxControllers;
		private TableLayoutPanel tableLayoutPanel2;
		private GroupBox groupBox1;
		private Button uxShowVideo;
		private SplitContainer splitContainer1;
		private PropertyGrid uxVideoParameters;
		private FlowLayoutPanel flowLayoutPanel1;
		private Label uxGoalLabel;
		private RadioButton uxGoalRed;
		private RadioButton uxGoalBlue;
		private RadioButton uxGoalNone;
		private RadioButton uxVideoFile;
		private RadioButton uxVideoWebcam;
		private FlowLayoutPanel uxIrChannelPanel;
		private Label uxIrChannelLabel;
		private RadioButton uxIrChannelNone;
		private RadioButton uxIrChannel1;
		private RadioButton uxIrChannel2;
		private RadioButton uxIrChannel3;
		private RadioButton uxIrChannel4;
	}
}

