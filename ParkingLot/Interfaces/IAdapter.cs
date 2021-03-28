using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot.Interfaces
{
    public abstract class IAdapter
    {
        public ParkingLot parkingLot;
        public abstract string CommandCall(string command);
        public abstract ParkingLot CreateParkingLot(string command);
        public abstract string QueryOnRegistrationId(string[] command_parts);
        public abstract string QueryOnDriverAge(string[] command_parts, string output);
        public abstract string LeaveSlot(string[] command_parts);
        public abstract string ParkVehicle(string[] command_parts);

    }
}
