using HeatPump.CompressorManagement;
using HeatPump.DigitalCompressor;

namespace HeatPump.Circuit
{
    internal class Circuit : ICircuit
    {
        private DigitalCompr digitalCompr;
        private CircuitDetails circuitDetails;
        private CompressorDetails compressorDetails;
        private ComprManagement comprManagement;
        private AlarmDetails alarmDetails;

        public bool CanStartCompr { get; set; }
        public bool CanStopCompr { get; set; }

        public bool StartCompr()
        {
            if (digitalCompr.CanStart)
            {
                //2.4.1.6
                if (ValidateCircuit.StartAfterStop(circuitDetails, compressorDetails))
                {
                    CanStartCompr = false;
                    return CanStartCompr;
                }

                // 2.4.2 
                else if (ValidateCircuit.BalancedRunningCircuits(circuitDetails))
                {
                    if (ValidateCircuit.BalancedStoppedCircuits(circuitDetails))
                    {
                        CanStartCompr = true;
                        return CanStartCompr;
                    }
                    else
                    {
                        CanStartCompr = false;
                        return CanStartCompr;
                    }
                }

                //2.4.4.1
                else if (circuitDetails.CircuitCurrentPressure > Constant.CX_PT1_HIGH_PRESSURE_START_SP)
                {
                    return CheckHighPressureGuard();
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
            //2.4.1.5  
            if (digitalCompr.CanStop)
            {
                if (ValidateCircuit.CheckStopCompressorCircuit(circuitDetails, compressorDetails))
                {
                    CanStopCompr = true;
                    return CanStopCompr;
                }
                else
                {
                    CanStopCompr = false;
                    return CanStopCompr;
                }
            }
            else
            {
                return false;
            }
        }

        //2.4.4.2
        public void ActivatePressurePrevention()
        {
            int Ceiling = 2;
            string currentCompressor = string.Empty;
            List<HighPressureAlarm> highPressureAlarmList = new List<HighPressureAlarm>();

            if (circuitDetails.CircuitCurrentPressure > Constant.CX_PT1_HIGH_PREVENT_ALARM_START_SP)
            {
                while (Ceiling > 0)
                {
                    Ceiling = Ceiling - 1;
                    if (ValidateCircuit.RunningCompressorCount(circuitDetails) > Ceiling)
                    {
                        if (ValidateCircuit.CheckStopCompressorCircuit(circuitDetails, compressorDetails))
                        {
                            // stops compressor
                        }
                        if (!CheckHighPressureAlarm(currentCompressor, highPressureAlarmList))
                        {
                            return;
                        }
                    }

                    Task.Delay(TimeSpan.FromSeconds(Constant.T_HIGH_PRESSURE_PREVENT));
                    if (circuitDetails.CircuitCurrentPressure < Constant.CX_PT1_HIGH_PREVENT_ALARM_START_SP)
                    {
                        if (!(circuitDetails.CircuitCurrentPressure > Constant.CX_PT1_HIGH_PREVENT_ALARM_STOP_SP))
                        {
                            Ceiling = 2;
                            CanStartCompr = true;
                        }
                        return;
                    }
                }
            }
        }

        private bool CheckHighPressureAlarm(string currentCompressor, List<HighPressureAlarm> highPressureAlarmList)
        {
            var filtered = highPressureAlarmList.Where(x => x.CompressorName == currentCompressor);
            if (filtered.Count() > 0)
            {
                var data = filtered.FirstOrDefault();
                int count = data != null ? ++data.CompressorStoppedCount : 0;
                AddHighPressureAlamDetails(currentCompressor, highPressureAlarmList, count);
                if (highPressureAlarmList.Where(x => x.CompressorName == currentCompressor).Count() == 3)
                {
                    var subtract = filtered.ToList()[0].CompressorStopTime - filtered.ToList()[2].CompressorStopTime;
                    if (subtract.TotalMinutes < 60)
                    {
                        alarmDetails.IsHighPressureAlarm = true;
                        digitalCompr.CanStart = false;
                        comprManagement.StopCompr();
                        return false;
                    }
                }
            }
            else
            {
                AddHighPressureAlamDetails(currentCompressor, highPressureAlarmList, 1);
            }
            return true;
        }

        private static void AddHighPressureAlamDetails(string currentCompressor, List<HighPressureAlarm> highPressureAlarmList, int count)
        {
            highPressureAlarmList.Add(new HighPressureAlarm
            {
                CompressorName = currentCompressor,
                CompressorStoppedCount = count,
                CompressorStopTime = DateTime.Now,
            });
        }

        private bool CheckHighPressureGuard()
        {
            if (circuitDetails.CircuitCurrentPressure < Constant.CX_PT1_HIGH_PRESSURE_STOP_SP)
            {
                digitalCompr.CanStart = true;
                return digitalCompr.CanStart;
            }
            else
            {
                digitalCompr.Alarm = true;
                digitalCompr.CanStart = false;
                comprManagement.StopCompr();
                return digitalCompr.CanStart;
            }
        }
    }
}
