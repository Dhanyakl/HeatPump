using HeatPump.Circuit;
using HeatPump.CompressorManagement;
using HeatPump.DigitalCompressor;

namespace HeatPump
{
    internal class FrostPreventionFeature
    {
        private CircuitDetails circuitDetails;
        private DigitalCompr digitalCompr;
        private AlarmDetails alarmDetails;
        private ComprManagement comprManagement;
        private CompressorDetails compressorDetails;
        public int currentTemp;
        public void ActivatePressurePrevention()
        {
            int Ceiling = 4;
            if (CheckFrostAlarm())
            {
                return;
            }

            if (currentTemp < Constant.CS1_TS2_FROST_PREVENT_START_SP)
            {
                while (Ceiling > 0)
                {
                    Ceiling = Ceiling - 1;
                    if (RunningCompressorCount() > Ceiling)
                    {
                        if (ValidateCircuit.CheckStopCompressorCircuit(circuitDetails, compressorDetails))
                        {
                            // stops compressor
                        }
                    }

                    Task.Delay(TimeSpan.FromSeconds(Constant.T_FROST_PREVENT));
                    if (currentTemp > Constant.CS1_TS2_FROST_PREVENT_START_SP)
                    {
                        if (circuitDetails.CircuitCurrentPressure > Constant.CS1_TS2_FROST_PREVENT_STOP_SP)
                        {
                            Ceiling = 4;
                            return;
                        }
                    }
                }
            }
        }

        private bool CheckFrostAlarm()
        {
            if (currentTemp < Constant.CS1_TS2_FROST_ALARM_SP)
            {
                alarmDetails.IsFrostAlarm = true;
                digitalCompr.CanStart = false;
                comprManagement.StopCompr();
                return true;
            }
            return false;
        }

        public int? RunningCompressorCount()
        {
            int? count = circuitDetails.RunningComprDetails.GroupBy(x => x.CircuitName).FirstOrDefault()?.Count();
            return count;
        }
    }
}
