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
			this.label2 = new System.Windows.Forms.Label();
			this.uxPorts = new System.Windows.Forms.ComboBox();
			this.uxPortConnect = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.uxFps = new System.Windows.Forms.Label();
			this.uxFilenameLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.uxFilename = new System.Windows.Forms.TextBox();
			this.uxFilenameBrowse = new System.Windows.Forms.Button();
			this.uxWebcam = new System.Windows.Forms.Button();
			this.uxFrame = new Emgu.CV.UI.ImageBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxFrame)).BeginInit();
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.tableLayoutPanel1.SetColumnSpan(this.label2, 5);
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(0, 62);
			this.label2.Margin = new System.Windows.Forms.Padding(0);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(5);
			this.label2.Size = new System.Drawing.Size(567, 27);
			this.label2.TabIndex = 2;
			this.label2.Text = "Control Panel";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uxPorts
			// 
			this.uxPorts.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.uxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxPorts.FormattingEnabled = true;
			this.uxPorts.Location = new System.Drawing.Point(380, 20);
			this.uxPorts.Name = "uxPorts";
			this.uxPorts.Size = new System.Drawing.Size(121, 21);
			this.uxPorts.TabIndex = 2;
			// 
			// uxPortConnect
			// 
			this.uxPortConnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.uxPortConnect.AutoSize = true;
			this.uxPortConnect.Location = new System.Drawing.Point(507, 19);
			this.uxPortConnect.Name = "uxPortConnect";
			this.uxPortConnect.Size = new System.Drawing.Size(57, 23);
			this.uxPortConnect.TabIndex = 3;
			this.uxPortConnect.Text = "Connect";
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
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.uxFps, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxPorts, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxPortConnect, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(567, 89);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// uxFps
			// 
			this.uxFps.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.uxFps.AutoSize = true;
			this.uxFps.BackColor = System.Drawing.Color.Transparent;
			this.uxFps.Location = new System.Drawing.Point(188, 24);
			this.uxFps.Name = "uxFps";
			this.uxFps.Size = new System.Drawing.Size(21, 13);
			this.uxFps.TabIndex = 6;
			this.uxFps.Text = "fps";
			// 
			// uxFilenameLabel
			// 
			this.uxFilenameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxFilenameLabel.AutoSize = true;
			this.uxFilenameLabel.Location = new System.Drawing.Point(3, 8);
			this.uxFilenameLabel.Name = "uxFilenameLabel";
			this.uxFilenameLabel.Size = new System.Drawing.Size(26, 13);
			this.uxFilenameLabel.TabIndex = 3;
			this.uxFilenameLabel.Text = "File:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.uxFilename, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.uxFilenameLabel, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.uxFilenameBrowse, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.uxWebcam, 3, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(422, 375);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// uxFilename
			// 
			this.uxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxFilename.Location = new System.Drawing.Point(35, 4);
			this.uxFilename.Name = "uxFilename";
			this.uxFilename.Size = new System.Drawing.Size(260, 20);
			this.uxFilename.TabIndex = 2;
			// 
			// uxFilenameBrowse
			// 
			this.uxFilenameBrowse.AutoSize = true;
			this.uxFilenameBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.uxFilenameBrowse.Location = new System.Drawing.Point(301, 3);
			this.uxFilenameBrowse.Name = "uxFilenameBrowse";
			this.uxFilenameBrowse.Size = new System.Drawing.Size(52, 23);
			this.uxFilenameBrowse.TabIndex = 4;
			this.uxFilenameBrowse.Text = "&Browse";
			this.uxFilenameBrowse.UseVisualStyleBackColor = true;
			this.uxFilenameBrowse.Click += new System.EventHandler(UxFilenameBrowseClick);
			// 
			// uxWebcam
			// 
			this.uxWebcam.AutoSize = true;
			this.uxWebcam.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.uxWebcam.Location = new System.Drawing.Point(359, 3);
			this.uxWebcam.Name = "uxWebcam";
			this.uxWebcam.Size = new System.Drawing.Size(60, 23);
			this.uxWebcam.TabIndex = 5;
			this.uxWebcam.Text = "&Webcam";
			this.uxWebcam.UseVisualStyleBackColor = true;
			this.uxWebcam.Click += new System.EventHandler(UxWebcamClick);
			// 
			// uxFrame
			// 
			this.uxFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxFrame.BackColor = System.Drawing.Color.Transparent;
			this.uxFrame.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
			this.uxFrame.Location = new System.Drawing.Point(12, 95);
			this.uxFrame.Name = "uxFrame";
			this.uxFrame.Size = new System.Drawing.Size(543, 449);
			this.uxFrame.TabIndex = 0;
			this.uxFrame.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(567, 556);
			this.Controls.Add(this.uxFrame);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(400, 240);
			this.Name = "MainForm";
			this.Text = "Robin Control Panel";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxFrame)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox uxPorts;
		private System.Windows.Forms.Button uxPortConnect;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label uxFps;
		private System.Windows.Forms.Label uxFilenameLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TextBox uxFilename;
		private System.Windows.Forms.Button uxFilenameBrowse;
		private System.Windows.Forms.Button uxWebcam;
		private Emgu.CV.UI.ImageBox uxFrame;
	}
}

