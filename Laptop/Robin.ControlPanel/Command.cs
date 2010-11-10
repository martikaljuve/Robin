using System.ComponentModel;

namespace Robin.ControlPanel
{
	public abstract class Command : INotifyPropertyChanged
	{
		private bool enabled;
		private string displayName;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Key { get; protected set; }

		public string DisplayName {
			get { return displayName; }
			protected set {
				if (displayName == value) return;
				displayName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
			}
		}

		public bool Enabled {
			get { return enabled; }
			set {
				if (enabled == value) return;
				enabled = value;

				OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
			}
		}

		public bool Disabled
		{
			get { return !Enabled; }
		}

		public abstract void Execute();

		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
				handler(this, e);
		}
	}
}