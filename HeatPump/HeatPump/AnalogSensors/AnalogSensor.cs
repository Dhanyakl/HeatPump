using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.AnalogSensors
{
    // This is only used for demonstration purposes.
    internal class AnalogSensor : IAnalogSensor
    {
        public Decimal Pv { get; set; }
    }
}
