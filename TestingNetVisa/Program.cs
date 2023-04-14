using System;
using NetVisa;

namespace MyApp;

public class Program
{
    static void Main(string[] args)
    {
        // Create a resource manager.  Consider creating a singleton for enforcing only creating one of these.
        // Make sure "Dispose" is called so the connecion is closed.
        using var rm = new Resource_Manager(new Driver(Visa_Plugin.NativeVisa));

        // Read all the resources connected to this PC
        var resources = rm.FindResources();

        // Show them on the console
        Console.WriteLine(string.Join('\n', resources));

        // Create a connection to a power supply
        using var visa = new Visa_Session("GPIB0::29::INSTR", rm);

        // Set the timeout
        visa.Timeout = 5000;

        // Write
        visa.Write("VSET?");

        // Read
        var vset1 = visa.ReadString(1024, out _, false, out _);
        var vset2 = visa.ReadStringUnknownLength();

        // Prints: "VSET 0.000   \r\n"
        Console.WriteLine('"' + vset2.Replace("\n", @"\n").Replace("\r", @"\r") + '"' + "\n");
    }
}