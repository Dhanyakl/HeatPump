using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.Circuit
{
    public static class ValidateCircuit
    {
        public static bool BalancedStoppedCircuits(CircuitDetails circuitDetails)
        {
            return circuitDetails.StoppedComprDetails.GroupBy(x => x.CircuitName).Where(x => x.Key != circuitDetails.NextStartComprDetails.CircuitName).FirstOrDefault()?.Count() == 2;
        }

        public static bool BalancedRunningCircuits(CircuitDetails circuitDetails)
        {
            return circuitDetails.RunningComprDetails.Where(x => x.CircuitName == circuitDetails.NextStartComprDetails.CircuitName).ToList().Count > 0;
        }

        public static bool StartAfterStop(CircuitDetails circuitDetails, CompressorDetails compressorDetails)
        {
            return circuitDetails.LastStoppedCompressorCircuit == circuitDetails.NextStartCompressorCircuit &&
                                ((DateTime.Now - compressorDetails.LastCompressorStopTime).TotalSeconds < Constant.T_COMPR_START_STOP);
        }

        public static bool CheckStopCompressorCircuit(CircuitDetails circuitDetails, CompressorDetails compressorDetails)
        {
            if (circuitDetails.LastStartedCompressorCircuit == circuitDetails.NextStopCompressorCircuit &&
                ((DateTime.Now - compressorDetails.LastCompressorStartTime).TotalSeconds < Constant.T_COMPR_STOP_START))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int? RunningCompressorCount(CircuitDetails circuitDetails)
        {
            int? count = circuitDetails.RunningComprDetails.GroupBy(x => x.CircuitName).FirstOrDefault()?.Count();
            return count;
        }
    }
}
