namespace Rihma.FindGolfBalls
{
	partial class Window
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
			this.uxImage = new Emgu.CV.UI.ImageBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.uxFilename = new System.Windows.Forms.TextBox();
			this.uxFilenameLabel = new System.Windows.Forms.Label();
			this.uxFilenameBrowse = new System.Windows.Forms.Button();
			this.uxWebcam = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.uxSidePanel = new System.Windows.Forms.TabControl();
			this.uxSidePanelTab1 = new System.Windows.Forms.TabPage();
			this.uxTable = new System.Windows.Forms.TableLayoutPanel();
			this.uxCannyCheckbox = new System.Windows.Forms.CheckBox();
			this.uxImageProcessor = new System.Windows.Forms.ComboBox();
			this.uxImageProcessorLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.uxImage)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.uxSidePanel.SuspendLayout();
			this.uxSidePanelTab1.SuspendLayout();
			this.uxTable.SuspendLayout();
			this.SuspendLayout();
			// 
			// uxImage
			// 
			this.uxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.uxImage, 4);
			this.uxImage.Location = new System.Drawing.Point(3, 32);
			this.uxImage.Name = "uxImage";
			this.uxImage.Size = new System.Drawing.Size(416, 340);
			this.uxImage.TabIndex = 0;
			this.uxImage.TabStop = false;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.uxImage, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.uxFilename, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxFilenameLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxFilenameBrowse, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.uxWebcam, 3, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(422, 375);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// uxFilename
			// 
			this.uxFilename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxFilename.Location = new System.Drawing.Point(35, 4);
			this.uxFilename.Name = "uxFilename";
			this.uxFilename.Size = new System.Drawing.Size(260, 20);
			this.uxFilename.TabIndex = 2;
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
			this.uxFilenameBrowse.Click += new System.EventHandler(this.uxFilenameBrowse_Click);
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
			this.uxWebcam.Click += new System.EventHandler(this.uxWebcam_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.uxSidePanel);
			this.splitContainer1.Size = new System.Drawing.Size(716, 375);
			this.splitContainer1.SplitterDistance = 422;
			this.splitContainer1.TabIndex = 2;
			// 
			// uxSidePanel
			// 
			this.uxSidePanel.Controls.Add(this.uxSidePanelTab1);
			this.uxSidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxSidePanel.Location = new System.Drawing.Point(0, 0);
			this.uxSidePanel.Name = "uxSidePanel";
			this.uxSidePanel.SelectedIndex = 0;
			this.uxSidePanel.Size = new System.Drawing.Size(290, 375);
			this.uxSidePanel.TabIndex = 0;
			// 
			// uxSidePanelTab1
			// 
			this.uxSidePanelTab1.BackColor = System.Drawing.SystemColors.Control;
			this.uxSidePanelTab1.Controls.Add(this.uxTable);
			this.uxSidePanelTab1.Location = new System.Drawing.Point(4, 22);
			this.uxSidePanelTab1.Name = "uxSidePanelTab1";
			this.uxSidePanelTab1.Padding = new System.Windows.Forms.Padding(3);
			this.uxSidePanelTab1.Size = new System.Drawing.Size(282, 349);
			this.uxSidePanelTab1.TabIndex = 0;
			this.uxSidePanelTab1.Text = "Threshold";
			// 
			// uxTable
			// 
			this.uxTable.ColumnCount = 3;
			this.uxTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.uxTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.uxTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
			this.uxTable.Controls.Add(this.uxCannyCheckbox, 0, 0);
			this.uxTable.Controls.Add(this.uxImageProcessor, 1, 1);
			this.uxTable.Controls.Add(this.uxImageProcessorLabel, 0, 1);
			this.uxTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxTable.Location = new System.Drawing.Point(3, 3);
			this.uxTable.Name = "uxTable";
			this.uxTable.RowCount = 3;
			this.uxTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.uxTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.uxTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.uxTable.Size = new System.Drawing.Size(276, 343);
			this.uxTable.TabIndex = 1;
			// 
			// uxCannyCheckbox
			// 
			this.uxCannyCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxCannyCheckbox.AutoSize = true;
			this.uxCannyCheckbox.Checked = true;
			this.uxCannyCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.uxCannyCheckbox.Enabled = false;
			this.uxCannyCheckbox.Location = new System.Drawing.Point(3, 6);
			this.uxCannyCheckbox.Name = "uxCannyCheckbox";
			this.uxCannyCheckbox.Size = new System.Drawing.Size(111, 17);
			this.uxCannyCheckbox.TabIndex = 0;
			this.uxCannyCheckbox.Text = "Enabled";
			this.uxCannyCheckbox.UseVisualStyleBackColor = true;
			// 
			// uxImageProcessor
			// 
			this.uxImageProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxTable.SetColumnSpan(this.uxImageProcessor, 2);
			this.uxImageProcessor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxImageProcessor.FormattingEnabled = true;
			this.uxImageProcessor.Location = new System.Drawing.Point(120, 34);
			this.uxImageProcessor.Name = "uxImageProcessor";
			this.uxImageProcessor.Size = new System.Drawing.Size(153, 21);
			this.uxImageProcessor.TabIndex = 1;
			// 
			// uxImageProcessorLabel
			// 
			this.uxImageProcessorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.uxImageProcessorLabel.AutoSize = true;
			this.uxImageProcessorLabel.Location = new System.Drawing.Point(3, 38);
			this.uxImageProcessorLabel.Name = "uxImageProcessorLabel";
			this.uxImageProcessorLabel.Size = new System.Drawing.Size(111, 13);
			this.uxImageProcessorLabel.TabIndex = 2;
			this.uxImageProcessorLabel.Text = "Processor";
			// 
			// Window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(716, 375);
			this.Controls.Add(this.splitContainer1);
			this.MinimumSize = new System.Drawing.Size(200, 100);
			this.Name = "Window";
			this.Text = "Finding golf balls";
			((System.ComponentModel.ISupportInitialize)(this.uxImage)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.uxSidePanel.ResumeLayout(false);
			this.uxSidePanelTab1.ResumeLayout(false);
			this.uxTable.ResumeLayout(false);
			this.uxTable.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Emgu.CV.UI.ImageBox uxImage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox uxFilename;
		private System.Windows.Forms.Label uxFilenameLabel;
		private System.Windows.Forms.Button uxFilenameBrowse;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl uxSidePanel;
		private System.Windows.Forms.TabPage uxSidePanelTab1;
		private System.Windows.Forms.CheckBox uxCannyCheckbox;
		private System.Windows.Forms.TableLayoutPanel uxTable;
		private System.Windows.Forms.Button uxWebcam;
		private System.Windows.Forms.ComboBox uxImageProcessor;
		private System.Windows.Forms.Label uxImageProcessorLabel;
	}
}