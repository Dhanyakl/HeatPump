using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatPump.DigitalCompressor
{
    // Implement this
    internal interface IDigitalCompr
    {
        // Starts the compressor. Fails if CanStart is false.
        Boolean Start();
        // Stops the compressor. Will stop compressor event if CanStop is false.
        Boolean Stop();
        // Indicates if the compressor can start.
        // Compressor cannot start if it is disabled,
        // there is an alarm or the cooldown time has not passed yet.
        Boolean CanStart { get; }
        // Indicates if the compressor can stop.
        // Compressor cannot stop if the minimum runtime has not passed yet.
        Boolean CanStop { get; }
        // Indicates if the compressor is running.
        Boolean Running { get; }
        // Optional: Indicates if the compressor is enabled (optional task 2.4.3).
        Boolean Enabled { get; }
        // Optional: Indicates if the compressor has an alarm (optional task 2.4.4.1)
        Boolean Alarm { get; }
    };
}
