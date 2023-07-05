using HeatPump.DigitalCompressor;

namespace HeatPump.CompressorManagement
{
    internal class ComprManagement : IComprManagement
    {
        private DigitalCompr digitalCompr;
        private CompressorDetails compressorDetails;
        private CircuitDetails circuitDetails;
        private AlarmDetails alarmDetails;
        public bool CanStartCompr { get; set; }
        public bool CanStopCompr { get; set; }

        public ComprManagement(DigitalCompr digitalCompr, CompressorDetails cmprDetails)
        {
            this.digitalCompr = digitalCompr;
        }

        public bool StartCompr()
        {
            if (digitalCompr.CanStart)
            {
                //2.4.1.3
                if ((DateTime.Now - compressorDetails.LastCompressorStartTime).TotalSeconds < Constant.T_COMPR_START)
                {
                    CanStartCompr = false;
                    return CanStartCompr;
                }
                else
                {
                    CanStartCompr = true;
                    return CanStartCompr;
                }
            }
            else
            {
                return false;
            }

        }

        public bool StopCompr()
        {
            if (CheckAlarmStatus())
            {
                // stop compressor
                return true;
            }
            if (digitalCompr.CanStop)
            {
                return CheckCompressorTimeBetweenStop();
            }
            else
            {
                return false;
            }

        }

        private bool CheckCompressorTimeBetweenStop()
        {
            //2.4.1.4 
            if ((DateTime.Now - compressorDetails.LastCompressorStopTime).TotalSeconds < Constant.T_COMPR_STOP)
            {
                CanStopCompr = false;
                return CanStopCompr;
            }
            else
            {
                CanStopCompr = true;
                return CanStopCompr;
            }
        }

        private bool CheckAlarmStatus()
        {
            return alarmDetails.IsFrostAlarm || digitalCompr.Alarm || alarmDetails.IsHighPressureAlarm || alarmDetails.IsGeneralCircuitAlarm;
        }

        public void StopMachine()
        {
            circuitDetails.RunningComprDetails.ForEach(circuit =>
            {
                CanStopCompr = true;
                Task.Delay(TimeSpan.FromSeconds(Constant.T_SHUTDOWN_BETWEEN_STOPS));
            });
        }
    }
}
