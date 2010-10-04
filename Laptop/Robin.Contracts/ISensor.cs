using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robin
{
	public interface ISensor
	{
		string Name { get; }
		SensorType SensorType { get; }
		string GetDataString();
	}
}
