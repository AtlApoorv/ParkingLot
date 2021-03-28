using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLot
{
    public class Vehicle
    {
        public string RegistrationId { get; private set; }

        public int Driver_Age { get; private set; }

        public Vehicle(string registrationId, int age)
        {
            RegistrationId = registrationId;
            Driver_Age = age;
        }
    }
}
