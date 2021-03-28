using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class ParkingTicket
    {
        public Vehicle Vehicle { get; private set; }

        public int SlotId { get; private set; }

        public ParkingTicket(Vehicle vehicle, int slot)
        {
            Vehicle = vehicle;
            SlotId = slot;
        }
    }
}
