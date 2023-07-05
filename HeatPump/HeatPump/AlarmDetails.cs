using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump
{
    internal class AlarmDetails
    {
        public bool IsAlarm { get; set; }

        public bool IsFrostAlarm { get; set; }
        public bool IsHighPressureAlarm { get; set; }
        public bool IsGeneralCircuitAlarm { get; set; }

    }

    internal class HighPressureAlarm
    {
        public string CompressorName { get; set; }
        public int CompressorStoppedCount { get; set; }
        public DateTime CompressorStopTime { get; set; }

    }
}
