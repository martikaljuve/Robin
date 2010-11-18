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
			this.uxIrChannel1 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel3 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel2 = new System.Windows.Forms.RadioButton();
			this.uxIrChannel4 = new System.Windows.Forms.RadioButton();
			this.uxIrChannelLabel = new System.Windows.Forms.Label();
			this.uxIrChannelNone = new System.Windows.Forms.RadioButton();
			this.uxIrChannelPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.uxContentPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.uxSerialPanel = new System.Windows.Forms.GroupBox();
			this.uxSerialReceive = new System.Windows.Forms.Label();
			this.uxSerialSend = new System.Windows.Forms.Label();
			this.uxSerialSendData = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.uxShowVideo = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.uxIrChannelPanel.SuspendLayout();
			this.uxContentPanel.SuspendLayout();
			this.uxSerialPanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.uxControlPanelLabel.Size = new System.Drawing.Size(690, 27);
			this.uxControlPanelLabel.TabIndex = 2;
			this.uxControlPanelLabel.Text = "Control Panel";
			this.uxControlPanelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uxPorts
			// 
			this.uxPorts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxPorts.FormattingEnabled = true;
			this.uxPorts.Location = new System.Drawing.Point(9, 35);
			this.uxPorts.Name = "uxPorts";
			this.uxPorts.Size = new System.Drawing.Size(121, 29);
			this.uxPorts.TabIndex = 2;
			// 
			// uxPortConnect
			// 
			this.uxPortConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxPortConnect.AutoSize = true;
			this.uxPortConnect.Location = new System.Drawing.Point(136, 33);
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(690, 89);
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
			this.uxControllers.Size = new System.Drawing.Size(246, 21);
			this.uxControllers.TabIndex = 7;
			// 
			// uxSerialData
			// 
			this.uxSerialData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxSerialData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.uxSerialData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uxSerialData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialData.ForeColor = System.Drawing.Color.White;
			this.uxSerialData.Location = new System.Drawing.Point(329, 98);
			this.uxSerialData.Multiline = true;
			this.uxSerialData.Name = "uxSerialData";
			this.uxSerialData.ReadOnly = true;
			this.uxSerialData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxSerialData.Size = new System.Drawing.Size(323, 114);
			this.uxSerialData.TabIndex = 6;
			this.uxSerialData.WordWrap = false;
			// 
			// uxIrChannel1
			// 
			this.uxIrChannel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel1.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel1.AutoSize = true;
			this.uxIrChannel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel1.Location = new System.Drawing.Point(253, 3);
			this.uxIrChannel1.Name = "uxIrChannel1";
			this.uxIrChannel1.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel1.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel1.TabIndex = 8;
			this.uxIrChannel1.Text = "1";
			this.uxIrChannel1.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel3
			// 
			this.uxIrChannel3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel3.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel3.AutoSize = true;
			this.uxIrChannel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel3.Location = new System.Drawing.Point(333, 3);
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
			this.uxIrChannel2.Location = new System.Drawing.Point(293, 3);
			this.uxIrChannel2.Name = "uxIrChannel2";
			this.uxIrChannel2.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel2.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel2.TabIndex = 9;
			this.uxIrChannel2.Text = "2";
			this.uxIrChannel2.UseVisualStyleBackColor = true;
			// 
			// uxIrChannel4
			// 
			this.uxIrChannel4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannel4.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannel4.AutoSize = true;
			this.uxIrChannel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannel4.Location = new System.Drawing.Point(373, 3);
			this.uxIrChannel4.Name = "uxIrChannel4";
			this.uxIrChannel4.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannel4.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannel4.TabIndex = 10;
			this.uxIrChannel4.Text = "4";
			this.uxIrChannel4.UseVisualStyleBackColor = true;
			// 
			// uxIrChannelLabel
			// 
			this.uxIrChannelLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannelLabel.AutoSize = true;
			this.uxIrChannelLabel.BackColor = System.Drawing.Color.Transparent;
			this.uxIrChannelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannelLabel.Location = new System.Drawing.Point(114, 11);
			this.uxIrChannelLabel.Name = "uxIrChannelLabel";
			this.uxIrChannelLabel.Size = new System.Drawing.Size(93, 20);
			this.uxIrChannelLabel.TabIndex = 11;
			this.uxIrChannelLabel.Text = "IR Channel:";
			// 
			// uxIrChannelNone
			// 
			this.uxIrChannelNone.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.uxIrChannelNone.Appearance = System.Windows.Forms.Appearance.Button;
			this.uxIrChannelNone.AutoSize = true;
			this.uxIrChannelNone.Checked = true;
			this.uxIrChannelNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxIrChannelNone.Location = new System.Drawing.Point(213, 3);
			this.uxIrChannelNone.Name = "uxIrChannelNone";
			this.uxIrChannelNone.Padding = new System.Windows.Forms.Padding(3);
			this.uxIrChannelNone.Size = new System.Drawing.Size(34, 36);
			this.uxIrChannelNone.TabIndex = 12;
			this.uxIrChannelNone.TabStop = true;
			this.uxIrChannelNone.Text = "0";
			this.uxIrChannelNone.UseVisualStyleBackColor = true;
			// 
			// uxIrChannelPanel
			// 
			this.uxIrChannelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxIrChannelPanel.AutoSize = true;
			this.uxIrChannelPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel4);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel3);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel2);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannel1);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannelNone);
			this.uxIrChannelPanel.Controls.Add(this.uxIrChannelLabel);
			this.uxIrChannelPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.uxIrChannelPanel.Location = new System.Drawing.Point(242, 28);
			this.uxIrChannelPanel.Name = "uxIrChannelPanel";
			this.uxIrChannelPanel.Size = new System.Drawing.Size(410, 43);
			this.uxIrChannelPanel.TabIndex = 13;
			// 
			// uxContentPanel
			// 
			this.uxContentPanel.AutoScroll = true;
			this.uxContentPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxContentPanel.Controls.Add(this.uxSerialPanel);
			this.uxContentPanel.Controls.Add(this.groupBox1);
			this.uxContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxContentPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.uxContentPanel.Location = new System.Drawing.Point(0, 89);
			this.uxContentPanel.Name = "uxContentPanel";
			this.uxContentPanel.Size = new System.Drawing.Size(690, 440);
			this.uxContentPanel.TabIndex = 14;
			this.uxContentPanel.WrapContents = false;
			// 
			// uxSerialPanel
			// 
			this.uxSerialPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxSerialPanel.Controls.Add(this.uxSerialReceive);
			this.uxSerialPanel.Controls.Add(this.uxSerialSend);
			this.uxSerialPanel.Controls.Add(this.uxSerialSendData);
			this.uxSerialPanel.Controls.Add(this.uxIrChannelPanel);
			this.uxSerialPanel.Controls.Add(this.uxPorts);
			this.uxSerialPanel.Controls.Add(this.uxPortConnect);
			this.uxSerialPanel.Controls.Add(this.uxSerialData);
			this.uxSerialPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialPanel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.uxSerialPanel.Location = new System.Drawing.Point(3, 3);
			this.uxSerialPanel.MinimumSize = new System.Drawing.Size(100, 50);
			this.uxSerialPanel.Name = "uxSerialPanel";
			this.uxSerialPanel.Size = new System.Drawing.Size(658, 218);
			this.uxSerialPanel.TabIndex = 0;
			this.uxSerialPanel.TabStop = false;
			this.uxSerialPanel.Text = "Arduino";
			// 
			// uxSerialReceive
			// 
			this.uxSerialReceive.AutoSize = true;
			this.uxSerialReceive.Location = new System.Drawing.Point(332, 74);
			this.uxSerialReceive.Name = "uxSerialReceive";
			this.uxSerialReceive.Size = new System.Drawing.Size(66, 21);
			this.uxSerialReceive.TabIndex = 16;
			this.uxSerialReceive.Text = "Receive:";
			// 
			// uxSerialSend
			// 
			this.uxSerialSend.AutoSize = true;
			this.uxSerialSend.Location = new System.Drawing.Point(9, 74);
			this.uxSerialSend.Name = "uxSerialSend";
			this.uxSerialSend.Size = new System.Drawing.Size(48, 21);
			this.uxSerialSend.TabIndex = 15;
			this.uxSerialSend.Text = "Send:";
			// 
			// uxSerialSendData
			// 
			this.uxSerialSendData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.uxSerialSendData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.uxSerialSendData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uxSerialSendData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialSendData.ForeColor = System.Drawing.Color.White;
			this.uxSerialSendData.Location = new System.Drawing.Point(6, 98);
			this.uxSerialSendData.Multiline = true;
			this.uxSerialSendData.Name = "uxSerialSendData";
			this.uxSerialSendData.ReadOnly = true;
			this.uxSerialSendData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxSerialSendData.Size = new System.Drawing.Size(317, 114);
			this.uxSerialSendData.TabIndex = 14;
			this.uxSerialSendData.WordWrap = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.uxShowVideo);
			this.uxContentPanel.SetFlowBreak(this.groupBox1, true);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.groupBox1.Location = new System.Drawing.Point(3, 227);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(658, 187);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Vision Settings";
			// 
			// uxShowVideo
			// 
			this.uxShowVideo.Location = new System.Drawing.Point(495, 28);
			this.uxShowVideo.Name = "uxShowVideo";
			this.uxShowVideo.Size = new System.Drawing.Size(154, 39);
			this.uxShowVideo.TabIndex = 0;
			this.uxShowVideo.Text = "Show/Hide &Video";
			this.uxShowVideo.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(690, 529);
			this.Controls.Add(this.uxContentPanel);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 240);
			this.Name = "MainForm";
			this.Text = "Robin Control Panel";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.uxIrChannelPanel.ResumeLayout(false);
			this.uxIrChannelPanel.PerformLayout();
			this.uxContentPanel.ResumeLayout(false);
			this.uxSerialPanel.ResumeLayout(false);
			this.uxSerialPanel.PerformLayout();
			this.groupBox1.ResumeLayout(false);
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
		private RadioButton uxIrChannel1;
		private RadioButton uxIrChannel3;
		private RadioButton uxIrChannel2;
		private RadioButton uxIrChannel4;
		private Label uxIrChannelLabel;
		private RadioButton uxIrChannelNone;
		private FlowLayoutPanel uxIrChannelPanel;
		private Label uxVisionFps;
		private FlowLayoutPanel uxContentPanel;
		private GroupBox uxSerialPanel;
		private Label uxSerialSend;
		private TextBox uxSerialSendData;
		private Label uxSerialReceive;
		private GroupBox groupBox1;
		private ComboBox uxControllers;
		private Button uxShowVideo;
	}
}

