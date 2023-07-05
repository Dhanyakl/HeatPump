using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.CompressorManagement
{
    // Implement this
    internal interface IComprManagement
    {
        // Starts either C1-a, C1-b, C2-a or C2-b. Fails if CanStart is false.
        Boolean StartCompr();
        // Stops either C1-a, C1-b, C2-a or C2-b. Fails if CanStop is false.
        Boolean StopCompr();
        // Indicates if either C1-a, C1-b, C2-a or C2-b can start.
        Boolean CanStartCompr { get; }
        // Indicates if either C1-a, C1-b, C2-a or C2-b can stop.
        Boolean CanStopCompr { get; }
        // Stops all compressors quickly in order to stop machine.
        void StopMachine();
    }
}
