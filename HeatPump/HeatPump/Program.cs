using HeatPump.AnalogSensors;
using HeatPump.CompressorManagement;
using HeatPump.DigitalCompressor;

namespace HeatPump
{
    public class Program
    {
        // This method is only an example of how the IComprManagement interface
        // would be used.
        public static async Task Main()
        {
            AnalogSensor HS1_TS2 = new();
            AnalogSp HS1_TS2_SP = new();
            IComprManagement ComprManagement = null!;
            HS1_TS2_SP.Sp = 30;
            while (true)
            {
                HS1_TS2.Pv = HS1_TS2_SP.Sp - 1;
                ControlLoop();
                await Task.Delay(TimeSpan.FromSeconds(2));
                HS1_TS2.Pv = HS1_TS2_SP.Sp;
                ControlLoop();
                await Task.Delay(TimeSpan.FromSeconds(2));
                HS1_TS2.Pv = HS1_TS2_SP.Sp + 1;
                ControlLoop();
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            void ControlLoop()
            {
                if (HS1_TS2.Pv <= HS1_TS2_SP.Sp - 1)
                    _ = ComprManagement.StartCompr();
                else if (HS1_TS2.Pv >= HS1_TS2_SP.Sp + 1)
                    _ = ComprManagement.StopCompr();
            }
        }
    }

    // This is only used for demonstration purposes.
    internal class AnalogSp : IAnalogSp
    {
        public Decimal Sp { get; set; }
    }

    // This is only used for demonstration purposes.
    internal interface IAnalogSp
    {
        // The value of the SP
        Decimal Sp { get; }
    }
}