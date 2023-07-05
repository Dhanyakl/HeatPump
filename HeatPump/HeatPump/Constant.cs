using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump
{
    public static class Constant
    {
        public const int CX_PT1_HIGH_PREVENT_ALARM_START_SP = 50;
        public const int CX_PT1_HIGH_PREVENT_ALARM_STOP_SP = 35;
        public const int T_HIGH_PRESSURE_PREVENT = 5000;
        public const int CX_PT1_HIGH_PRESSURE_START_SP = 30;
        public const int CX_PT1_HIGH_PRESSURE_STOP_SP = 27;
        public const int T_COMPR_STOP_START = 2000;
        public const int T_COMPR_START_STOP = 2000;

        public const int T_COMPR_START = 5000;
        public const int T_COMPR_STOP = 5000;

        public const int T_SHUTDOWN_BETWEEN_STOPS = 5000;

        public const int T_COMPR_COOLDOWN = 2000;
        public const int T_COMPR_MIN_RUNTIME = 2000;

        public const int CS1_TS2_FROST_PREVENT_START_SP = 50;
        public const int CS1_TS2_FROST_PREVENT_STOP_SP = 35;
        public const int T_FROST_PREVENT = 5000;
        public const int CS1_TS2_FROST_ALARM_SP = 50;
    }
}
