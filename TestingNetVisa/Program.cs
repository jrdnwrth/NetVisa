using NetVisa;

namespace MyApp;

public class Program
{
    static void Main(string[] args)
    {
        // Create a resource manager.
        // This creates a singleton that will close when the app terminates.
        var rm = Resource_Manager.Get;

        // Read all the resources connected to this PC
        var resources = rm.FindResources();

        // Show them on the console
        Console.WriteLine(string.Join('\n', resources));

        // Create a connection to a power supply
        using var visa = new Visa_Session("GPIB0::29::INSTR", rm);

        // Set the timeout
        visa.Timeout = 5000;

        // Write
        visa.Write("VSET 5");

        // Read
        var vset = visa.ReadString();

        // ReadBytes
        //byte[] bytes = visa.ReadBytes();

        // Prints: "VSET 0.000   \r\n"
        Console.WriteLine('"' + vset.Replace("\n", @"\n").Replace("\r", @"\r") + '"' + "\n");
    }
}