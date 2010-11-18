using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Robin.ControlPanel
{
	public partial class VideoForm : Form
	{
		public VideoForm()
		{
			InitializeComponent();
		}

		public Bitmap Frame {
			set {
				uxPlayer.Image = value;
			}
		}
	}
}
