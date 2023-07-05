using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump
{
    public class CompressorDetails
    {
        public DateTime CompressorStartTime {  get; set; }
        public DateTime CompressorStopTime { get; set; }
        public DateTime LastCompressorStopTime { get; set; }
        public DateTime LastCompressorStartTime { get; set; }
        public List<string> RunningComprList { get; set; }
    }
}
