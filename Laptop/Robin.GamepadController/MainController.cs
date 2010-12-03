using System;
using System.ComponentModel.Composition;
using Robin.Core;
using SlimDX;
using SlimDX.DirectInput;

namespace Robin.GamepadController
{
	[ExportMetadata("Name", "Gamepad Controller")]
	[Export(typeof(IRobotController))]
	public class MainController : IRobotController
	{
		private Joystick gamepad;
		private JoystickState state = new JoystickState();
		private bool wasMoving;
		private short dribblerSpeed;

		public MainController()
		{
			var directInput = new DirectInput();
			LogicState = new LogicState();

			foreach (var deviceInstance in directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
			{
				try
				{
					gamepad = new Joystick(directInput, deviceInstance.InstanceGuid);
					gamepad.SetCooperativeLevel(Parent, CooperativeLevel.Exclusive | CooperativeLevel.Foreground);
					break;
				}
				catch (DirectInputException) { }
			}

			if (gamepad == null)
				return;

			foreach (var deviceObject in gamepad.GetObjects())
			{
				if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
					gamepad.GetObjectPropertiesById((int) deviceObject.ObjectType).SetRange(-1000, 1000);
			}
			
			gamepad.Acquire();
		}

		public VisionData VisionData { get; set; }

		public SensorData SensorData { get; set; }

		public LogicState LogicState { get; set; }

		public IRobotCommander Commander { get; set; }

		public void Update()
		{
			// HACK: Testime
			System.Threading.Thread.Sleep(100);

			if (gamepad == null)
				return;

			if (gamepad.Acquire().IsFailure)
				return;

			if (gamepad.Poll().IsFailure)
				return;

			state = gamepad.GetCurrentState();

			if (Result.Last.IsFailure)
				return;

			ControlRobot();
		}

		public IntPtr Parent { get; set; }

		/// <summary>
		/// Controls the robot.
		/// </summary>
		/// <remarks>
		/// X = left joystick left-right, -1000..1000
		/// Y = left joystick up-down, -1000..1000
		/// Z = left/right trigger, 1000..-1000
		/// rotationX = right joystick left-right, -1000..1000
		/// rotationY = right joystick up-down, -1000..1000
		/// buttons:
		/// XABY = 00, 01, 02, 03
		/// left button = 04
		/// right button = 05
		/// left trigger = 06
		/// right trigger = 07
		/// back, start = 08, 09
		/// left joystick = 10
		/// right joystick = 11
		/// left arrows none-up-right-down-left = -1, 0, 9000, 18000, 27000
		/// 
		/// 
		/// </remarks>
		private void ControlRobot()
		{
			// acceleration
			if (state.Z != 0)
			{
				wasMoving = true;
				var speed = (short)Map(state.Z, -1000, 1000, 300, -300);
				var direction = 0;
				short rotation = 0;

				if (state.Y == 0 && state.X == 0 && state.RotationX != 0)
				{
					if (speed != 0)
						Commander.Turn((short)(speed * (state.RotationX < 0 ? -1 : 1)));
				}
				else {
					if (state.X == 0)
						direction = state.Y <= 0 ? 0 : 180;
					else if (state.Y == 0)
						direction = state.X < 0 ? 270 : 90;
					else
					{
						direction = (int)(Math.Atan((double)Math.Abs(state.X) / Math.Abs(state.Y)) * (180 / Math.PI));
						if (state.X > 0 && state.Y > 0)
							direction += 90;
						else if (state.X < 0 && state.Y > 0)
							direction += 180;
						else if (state.X < 0 && state.Y < 0)
							direction += 270;
					}

					if (state.RotationX != 0)
						rotation = (short)Map(state.RotationX, -1000, 1000, -100, 100);

					Commander.MoveAndTurn((short)direction, speed, rotation);
				}
			}
			else if (wasMoving)
			{
				Commander.Stop();
				wasMoving = false;
			}

			var buttons = state.GetButtons();
			if (buttons.Length < 11) return;

			if (buttons[GamepadButtons.A])
				Commander.FireCoilgun(100);
			if (buttons[GamepadButtons.B])
				Commander.FireCoilgun(50);

			if (buttons[GamepadButtons.X])
			{
				dribblerSpeed = 255;
				Commander.SetDribbler(true);
			}
			if (buttons[GamepadButtons.Y])
			{
				dribblerSpeed = 0;
				Commander.SetDribbler(false);
			}

			if (buttons[GamepadButtons.LeftBumper])
			{
				dribblerSpeed -= 10;
				if (dribblerSpeed < -255)
					dribblerSpeed = -255;
				Commander.SetDribbler(dribblerSpeed);
			}
			if (buttons[GamepadButtons.RightBumper])
			{
				dribblerSpeed += 10;
				if (dribblerSpeed > 255)
					dribblerSpeed = 255;
				Commander.SetDribbler(dribblerSpeed);
			}
		}

		private static long Map(long value, long inMin, long inMax, long outMin, long outMax)
		{
			return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
		}

		public void Dispose()
		{
			if (gamepad != null)
			{
				gamepad.Unacquire();
				gamepad.Dispose();
			}
			gamepad = null;
		}
	}
}