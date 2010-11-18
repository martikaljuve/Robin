namespace Robin.ControlPanel
{
	partial class VideoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoForm));
			this.uxVisionPanel = new System.Windows.Forms.GroupBox();
			this.uxPlayer = new AForge.Controls.PictureBox();
			this.uxVisionPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.uxPlayer)).BeginInit();
			this.SuspendLayout();
			// 
			// uxVisionPanel
			// 
			this.uxVisionPanel.BackColor = System.Drawing.Color.Transparent;
			this.uxVisionPanel.Controls.Add(this.uxPlayer);
			this.uxVisionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxVisionPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
			this.uxVisionPanel.Location = new System.Drawing.Point(0, 0);
			this.uxVisionPanel.Name = "uxVisionPanel";
			this.uxVisionPanel.Size = new System.Drawing.Size(646, 508);
			this.uxVisionPanel.TabIndex = 2;
			this.uxVisionPanel.TabStop = false;
			this.uxVisionPanel.Text = "Video feed";
			// 
			// uxPlayer
			// 
			this.uxPlayer.BackColor = System.Drawing.Color.Transparent;
			this.uxPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxPlayer.Image = null;
			this.uxPlayer.Location = new System.Drawing.Point(3, 25);
			this.uxPlayer.Name = "uxPlayer";
			this.uxPlayer.Size = new System.Drawing.Size(640, 480);
			this.uxPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.uxPlayer.TabIndex = 7;
			this.uxPlayer.TabStop = false;
			// 
			// VideoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::Robin.ControlPanel.Properties.Resources.VisualStudioBackgroundBrown;
			this.ClientSize = new System.Drawing.Size(646, 508);
			this.Controls.Add(this.uxVisionPanel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "VideoForm";
			this.Text = "VideoForm";
			this.uxVisionPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.uxPlayer)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox uxVisionPanel;
		private AForge.Controls.PictureBox uxPlayer;
	}
}