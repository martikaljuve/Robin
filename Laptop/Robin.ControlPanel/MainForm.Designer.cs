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
			this.uxFrame = new Emgu.CV.UI.ImageBox();
			this.uxSerialData = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
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
			this.label2.Size = new System.Drawing.Size(936, 27);
			this.label2.TabIndex = 2;
			this.label2.Text = "Control Panel";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// uxPorts
			// 
			this.uxPorts.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.uxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxPorts.FormattingEnabled = true;
			this.uxPorts.Location = new System.Drawing.Point(749, 20);
			this.uxPorts.Name = "uxPorts";
			this.uxPorts.Size = new System.Drawing.Size(121, 21);
			this.uxPorts.TabIndex = 2;
			// 
			// uxPortConnect
			// 
			this.uxPortConnect.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.uxPortConnect.AutoSize = true;
			this.uxPortConnect.Location = new System.Drawing.Point(876, 19);
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(936, 89);
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
			// uxFrame
			// 
			this.uxFrame.BackColor = System.Drawing.Color.Transparent;
			this.uxFrame.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
			this.uxFrame.Location = new System.Drawing.Point(12, 95);
			this.uxFrame.Name = "uxFrame";
			this.uxFrame.Size = new System.Drawing.Size(640, 480);
			this.uxFrame.TabIndex = 0;
			this.uxFrame.TabStop = false;
			// 
			// uxSerialData
			// 
			this.uxSerialData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxSerialData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.uxSerialData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.uxSerialData.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxSerialData.ForeColor = System.Drawing.Color.White;
			this.uxSerialData.Location = new System.Drawing.Point(658, 95);
			this.uxSerialData.Multiline = true;
			this.uxSerialData.Name = "uxSerialData";
			this.uxSerialData.ReadOnly = true;
			this.uxSerialData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.uxSerialData.Size = new System.Drawing.Size(266, 480);
			this.uxSerialData.TabIndex = 6;
			this.uxSerialData.WordWrap = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(936, 624);
			this.Controls.Add(this.uxSerialData);
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
		private Emgu.CV.UI.ImageBox uxFrame;
		private System.Windows.Forms.TextBox uxSerialData;
	}
}

