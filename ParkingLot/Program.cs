using Microsoft.Extensions.DependencyInjection;
using ParkingLot.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace ParkingLot
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Setup DI
            var serviceProvider = new ServiceCollection().AddSingleton<IAdapter,Adapter>().BuildServiceProvider();

            //Face object of the adapter
            var face = serviceProvider.GetService<IAdapter>();

            Console.WriteLine("Welcome to the Parking Lot. Please enter the complete location of the input file /n ");
            string inputfile = Console.ReadLine();
            var commands = File.ReadLines(inputfile);
            bool initializedLot = false;
            foreach (var cmd in commands)
            {
                //First command should create parking lot else program will exit
                if (!initializedLot)
                {

                    face.parkingLot = face.CreateParkingLot(cmd);
                    if (face.parkingLot == null)
                    {
                        Console.WriteLine("Invalid parking lot initialization. Please setup Parking Lot in your first command!!");
                        break;
                    }
                    else
                    {
                        initializedLot = true;
                        Console.WriteLine("Created parking lot of " + face.parkingLot.lot_size + " slots");
                    }
                }
                else
                {

                    Console.WriteLine(face.CommandCall(cmd));
                }
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
    
}
