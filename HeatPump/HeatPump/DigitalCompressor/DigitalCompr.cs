using HeatPump.Circuit;

namespace HeatPump.DigitalCompressor
{
    internal class DigitalCompr : IDigitalCompr
    {
        private ICircuit circuit;
        public DigitalCompr(ICircuit circuit)
        {
            this.circuit = circuit;
        }

        private DigitalCompr digitalCompr;
        private CompressorDetails compressorDetails;
        private CircuitDetails circuitDetails;

        public bool CanStart { get; set; }
        public bool CanStop { get; set; }
        public bool Running { get; set; }
        public bool Enabled { get; set; }
        public bool Alarm { get; set; }


        public bool Start()
        {
            if (digitalCompr.CanStart)
            {               //2.4.1.2
                if ((DateTime.Now - compressorDetails.CompressorStopTime).TotalSeconds < Constant.T_COMPR_COOLDOWN)
                {
                    CanStart = false;
                    return CanStart;
                }
                else
                {
                    CanStart = true;
                    return CanStart;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Stop()
        {
            //2.4.1.1.
            if ((DateTime.Now - compressorDetails.CompressorStartTime).TotalSeconds < Constant.T_COMPR_MIN_RUNTIME)
            {
                CanStop = false;
                return CanStop;
            }
            else
            {
                CanStop = true;
                return CanStop;
            }
        }

        public void verifyDisabledCompr()
        {
            if (!Enabled)
            {
                CanStart = false;
                CanStop = true;
                if (ValidateCircuit.CheckStopCompressorCircuit(circuitDetails, compressorDetails))
                {
                    // stops compressor
                }
            }

        }

    }
}
