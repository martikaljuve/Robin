using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Robin.VideoProcessor;

namespace Robin.ControlPanel
{
	public partial class VideoForm : Form
	{
		public VideoForm()
		{
			InitializeComponent();

			KeyPress += OnKeyPress;
		}

		private static void OnKeyPress(object sender, KeyPressEventArgs keyPressEventArgs)
		{
			VisionExperiments.ProcessKey(keyPressEventArgs.KeyChar);
		}

		public Bitmap Frame {
			set {
				uxPlayer.Image = value;
			}
		}
	}
}
