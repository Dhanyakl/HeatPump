using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.Circuit
{
    // Implement this
    internal interface ICircuit
    {
        // Starts either C-a or C-b compressor in the circuit.
        // Fails if CanStart is false.
        Boolean StartCompr();
        // Stops either C-a or C-b compressor in the circuit.
        // Stops a compressor even if CanStop is false.
        Boolean StopCompr();
        // Indicates if either C-a or C-b compressors can start
        Boolean CanStartCompr { get; }
        // Indicates if either C-a or C-b compressors can stop
        Boolean CanStopCompr { get; }
    }
}
