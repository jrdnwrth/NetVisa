# Readme

## Description

This is a minimalistic wrapper around NI-VISA's 64 bit dll.

National Instrument's current C# solution is only available for .NET Framework 4.8.

This project is written in .NET 6.0 but can be compiled for whatever version you are using.

## Special Thanks

I used the Rohde-Schwarz Visa implementation as a guide for creating this small project.
Their Nuget Package "RsInstruments" is a very powerfull implementation of Visa and
freely available under the MIT license.

Unfortunately, I needed something closer to the metal that could be adapted
to work with older equipement.

## Prerequisites

Install NI-VISA.

This will place the visa64.dll on your computer.  This code references that dll.

## Usage

See `TestingNetVisa\Program.cs`

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
			Console.WriteLine('"' + vset1.Replace("\n", @"\n").Replace("\r", @"\r") + '"' + "\n");
		}
	}

## Disclaimer

I am not responsible for this code.  I make no guarantees.
This is really meant as a starting point for others to continue the project if they are interested.

This works with GPIB.  In theory, this also works for Ethernet (Sockets), Serial (USB), VXI11, and LXI instruments.
But I haven't tried it.


## Helpful References:

https://www.ni.com/docs/en-US/bundle/labview/page/lvinstio/visa_find_resource.html

https://www.ni.com/docs/en-US/bundle/ni-visa/page/ni-visa/vireadstb.html

https://pyvisa.readthedocs.io/en/1.8/shell.html

https://forums.ni.com/t5/Measurement-Studio-for-NET/Ni-DAQmx-NET6-Support/m-p/4272106#M21908

https://www.ni.com/docs/en-US/bundle/ni-daqmx-c-api-ref/page/cdaqmx/help_file_title.html

https://www.ni.com/docs/en-US/bundle/ni-visa/page/ni-visa/description_of_the_api.html

C:\Program Files\IVI Foundation\VISA\Win64\Include\

https://documentation.help/VISA.NET/VISA%20Attributes%20Table.htm


# Definitions

**SRQ => Visa Service Request**

This is used for the instrument to communicate back to the controller 
at a time when the controller is not planning to talk with the device.
https://documentation.help/NI-VISA/StatusServiceRequestService.html

**OPC => Operation Complete**

You can send multiple commands to an instrument and they will be queued by the instrument.
However, before querying the instrument it is best to make sure the "Operation Completed"
using the "*OPC?" command.
https://www.keysight.com/us/en/lib/resources/training-materials/using-opc.html