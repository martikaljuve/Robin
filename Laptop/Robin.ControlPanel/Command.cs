using System.ComponentModel;

namespace Robin.ControlPanel
{
	public abstract class Command : INotifyPropertyChanged
	{
		private bool _enabled;
		private string _displayName;

		public event PropertyChangedEventHandler PropertyChanged;

		public string Key { get; protected set; }
		public string DisplayName {
			get { return _displayName; }
			protected set {
				if (_displayName == value) return;
				_displayName = value;
				OnPropertyChanged(new PropertyChangedEventArgs("DisplayName"));
			}
		}

		public bool Enabled {
			get { return _enabled; }
			set {
				if (_enabled == value) return;
				_enabled = value;

				OnPropertyChanged(new PropertyChangedEventArgs("Enabled"));
			}
		}

		public abstract void Execute();

		protected void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null)
				handler(this, e);
		}
	}
}