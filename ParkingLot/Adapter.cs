using ParkingLot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class Adapter : IAdapter
    {
        //Query on Vehicle registration ID
        public override string QueryOnRegistrationId(string[] command_parts)
        {
            //Check for existing ticket for the Id
            if(parkingLot.Tickets.Count()==0)
            {
                return "No car parked!";
            }
            string id = command_parts[1];

            //Get all tickets for this Id
            var tickets = parkingLot.Registration_Lookup[id];
            if (tickets.Count() == 0)
            {
                return "Vehicle with registration number " + id + " has not parked.";
            }

            //Return all slots
            return tickets.First().SlotId.ToString();
        }

        //Single Quering call on age
        public override string QueryOnDriverAge(string[] command_parts,string output)
        {
            if (parkingLot.Tickets.Count() == 0)
            {
                return "No car parked!";
            }
            int age = Convert.ToInt32(command_parts[1]);

            //Get tickets on age
            var tickets = parkingLot.Age_Lookup[age];
            string result = "";

            //Switch between asked output type
            if(output== "Vehicle")
            {
                foreach (var val in tickets)
                {
                    result += val.Vehicle.RegistrationId + ",";
                }
            }
            else
            {
                foreach (var val in tickets)
                {
                    result += val.SlotId + ",";
                }
            }

            //Return if no such result found
            if (result == "")
            {
                return "No such driver of age " + age + " has parked";
            }
            return result.Remove(result.Length - 1, 1);
        }

        public override string LeaveSlot(string[] command_parts)
        {
            //Find if there is an existing ticket for the slot.
            int slot = Convert.ToInt32(command_parts[1]);
            var ticket = parkingLot.Tickets.Where(x => x.SlotId == slot).SingleOrDefault();
            if (ticket == null)
            {
                return "Slot already vacant";
            }

            //Remove the ticket from parking lot, update the slots and the lookups
            else
            {
                parkingLot.Tickets.Remove(ticket);
                parkingLot.Slots_not_available.Remove(slot);
                parkingLot.Slots_available.Add(slot);
                parkingLot.Age_Lookup = (Lookup<int, ParkingTicket>)parkingLot.Tickets.ToLookup(x => x.Vehicle.Driver_Age);
                parkingLot.Registration_Lookup = (Lookup<string, ParkingTicket>)parkingLot.Tickets.ToLookup(x => x.Vehicle.RegistrationId);

                //Return result
                return "Slot number " + ticket.SlotId + " vacated, the car with vehicle registration numner \"" + ticket.Vehicle.RegistrationId + "\" has left the space, the driver of the car was of the age " + ticket.Vehicle.Driver_Age;
            }
        }
        public override string ParkVehicle(string[] command_parts)
        {
            //Check if slots available
            if (parkingLot.Slots_available.Count > 0)
            {
                string id = command_parts[1];

                //Check if a duplicate/illegal car is being parked
                if(parkingLot.Registration_Lookup != null && parkingLot.Registration_Lookup[id].Count()>0)
                {
                    return "Duplicate/Illegal car being parked"; 
                }

                //Create a Vehicle, a ticket and update the slots
                int age = Convert.ToInt32(command_parts[3]);
                var vehicle = new Vehicle(id, age);
                int slot = parkingLot.Slots_available.First();
                var ticket = new ParkingTicket(vehicle, slot);
                parkingLot.Slots_available.Remove(slot);
                parkingLot.Slots_not_available.Add(slot);

                //Add ticket to parking lot and setup lookups for quick querying
                parkingLot.Tickets.Add(ticket);
                parkingLot.Age_Lookup = (Lookup<int, ParkingTicket>)parkingLot.Tickets.ToLookup(x => x.Vehicle.Driver_Age);
                parkingLot.Registration_Lookup = (Lookup<string, ParkingTicket>)parkingLot.Tickets.ToLookup(x => x.Vehicle.RegistrationId);

                //Return result
                return "Car with vehicle registration numner \"" + id + "\" has been parked at slot number " + ticket.SlotId;
            }
            else
            {
                return "Parking lot full!!";
            }
        }
        public override string CommandCall(string command)
        {
            var command_parts = command.Split(" ");

            //Check for validity of the actions in the input file
            if (!parkingLot.IsValidCommand(command_parts[0]))
            {
                return "Invalid command";
            }
            if (parkingLot == null)
            {
                return "Initialize parking lot first";
            }

            //Extensible to add more methods
            else
            {
                switch (command_parts[0])
                {
                    case "Park":
                        {
                            return ParkVehicle(command_parts);
                        }
                    case "Slot_numbers_for_driver_of_age":
                        {
                            return QueryOnDriverAge(command_parts,"Slots");
                        }
                    case "Slot_number_for_car_with_number":
                        {
                            return QueryOnRegistrationId(command_parts);
                        }
                    case "Leave":
                        {
                            return LeaveSlot(command_parts);
                        }
                    case "Vehicle_registration_number_for_driver_of_age":
                        {
                            return QueryOnDriverAge(command_parts,"Vehicles");
                        }
                    default:
                        return "Invalid command";
                }
            }

        }

        //Initialize parking lot
        public override ParkingLot CreateParkingLot(string command)
        {
            var actions = command.Split(" ");
            try
            {
                if (actions.Length == 2 && actions[0] == "Create_parking_lot")
                {
                    return new ParkingLot(Convert.ToInt32(actions[1]));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid lot size");
            }
            return null;
            

        }
    }
}
