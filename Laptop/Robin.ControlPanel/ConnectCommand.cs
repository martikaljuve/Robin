using Robin.Arduino;

namespace Robin.ControlPanel
{
	public class ConnectCommand : Command
	{
		private readonly ArduinoSerial _serial;

		public ConnectCommand(ArduinoSerial serial)
		{
			_serial = serial;
			DisplayName = "Connect";
		}

		public override void Execute()
		{
			if (_serial.IsOpen) {
				_serial.Close();
				DisplayName = "Connect";
			}
			else
			{
				_serial.Open(PortName);
				DisplayName = "Disconnect";
			}
		}

		public string PortName { get; set; }
	}
}