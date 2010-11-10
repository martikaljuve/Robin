using Robin.Arduino;

namespace Robin.ControlPanel
{
	public class ConnectCommand : Command
	{
		private readonly ArduinoSerial serial;

		public ConnectCommand(ArduinoSerial serial)
		{
			this.serial = serial;
			DisplayName = "Connect";
		}

		public override void Execute()
		{
			if (serial.IsOpen) {
				serial.Close();
				DisplayName = "Connect";
			}
			else
			{
				serial.Open(PortName);
				DisplayName = "Disconnect";
			}
		}

		public string PortName { get; set; }
	}
}