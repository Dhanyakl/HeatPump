using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.AnalogSensors
{
    internal interface IAnalogSensor
    {
        // Implement this in a manner suitable for testing
        internal interface IAnalogSensor
        {
            // The PV of the sensor
            Decimal Pv { get; }
        }
    }
}
