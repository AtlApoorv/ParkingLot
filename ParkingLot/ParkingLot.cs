using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingLot
{
    public class ParkingLot
    {
        public int lot_size;
        public SortedSet<int> Slots_available;
        public SortedSet<int> Slots_not_available;

        public HashSet<string> ValidActions;
        public List<ParkingTicket> Tickets;

        public Lookup<int, ParkingTicket> Age_Lookup ;
        public Lookup<string, ParkingTicket> Registration_Lookup;

        public ParkingLot(int size)
        {
            lot_size = size;
            Slots_available = new SortedSet<int>();
            Slots_not_available = new SortedSet<int>();
            InitializeSlots();
            Tickets = new List<ParkingTicket>();
            ValidActions = new HashSet<string>();
            FillValidActions();
        }

        private void InitializeSlots()
        {
            for(int i=1;i<=lot_size;i++)
            {
                Slots_available.Add(i);
            }
        }

        //Check for validity of the actions in the input file
        private void FillValidActions()
        {
            ValidActions.Add("Park");
            ValidActions.Add("Slot_numbers_for_driver_of_age");
            ValidActions.Add("Slot_number_for_car_with_number");
            ValidActions.Add("Leave");
            ValidActions.Add("Vehicle_registration_number_for_driver_of_age");
        }
        public bool IsValidCommand(string command)
        {
            return ValidActions.Contains(command);
        }
    }

    
}
