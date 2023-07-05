using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump
{
    public class CircuitDetails
    {
       
        public string NextStartCompressorCircuit { get; set; }
        public string LastStoppedCompressorCircuit { get; set; }
        public string LastStartedCompressorCircuit { get; set; }
        public string NextStopCompressorCircuit { get; set; }
        public List<Circuits> RunningComprDetails { get; set; }
        public List<Circuits> StoppedComprDetails { get; set; }
        public Circuits NextStartComprDetails { get; set; }
        public int CircuitCurrentPressure { get; set; }

    }
    public class Circuits
    {
        public string CompressorName { get; set; }
        public string CircuitName { get; set; }

    }


}
