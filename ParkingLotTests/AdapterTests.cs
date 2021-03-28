using Moq;
using NUnit.Framework;
using ParkingLot;
using ParkingLot.Interfaces;

namespace ParkingLotTests
{
    public class Tests
    {
        ParkingLot.ParkingLot lot;

        [Test]
        [TestCase("Create_parking_lot")]
        [TestCase("Create_parking_lot abcd")]
        [TestCase("Create_parking_ 10")]
        [TestCase("Create_parking_lot 10 20")]
        public void CreatParkingLot_Returns_Null_On_Invalid_Command(string command)
        {
            // Arrange
            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Setup(x => x.CreateParkingLot(It.IsAny<string>()));

            //Act
            var result = mock.Object.CreateParkingLot(command);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CommandCall_Handles_InvalidCommand()
        {
            // Arrange
            lot = new ParkingLot.ParkingLot(10);
            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.CommandCall(It.IsAny<string>()));
            string command = "Arrest_driver_of_age 17";
            var result = mock.Object.CommandCall(command);

            //Assert
            Assert.AreEqual(result,"Invalid command");
        }

        [Test]
        public void ParkVehicle_Returns_Invalid_On_DuplicateVehicle()
        {
            // Arrange
            InitializeParkingLot(10);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.ParkVehicle(It.IsAny<string[]>()));
            string command = "Park A driver_age 20";
            var result = mock.Object.ParkVehicle(command.Split(" "));

            //Assert
            Assert.AreEqual(result, "Duplicate/Illegal car being parked");
        }

        [Test]
        public void ParkVehicle_Returns_Full_On_Full_ParkingLot()
        {
            // Arrange
            InitializeParkingLot(5);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.ParkVehicle(It.IsAny<string[]>()));
            string command = "Park A driver_age 20";
            var result = mock.Object.ParkVehicle(command.Split(" "));

            //Assert
            Assert.AreEqual(result, "Parking lot full!!");
        }

        [Test]
        public void LeaveSlot_Returns_AlreadyVacant_On_Empty_Slot()
        {
            // Arrange
            InitializeParkingLot(6);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.LeaveSlot(It.IsAny<string[]>()));
            string command = "Leave 6";
            var result = mock.Object.LeaveSlot(command.Split(" "));

            //Assert
            Assert.AreEqual(result, "Slot already vacant");
        }

        [Test]
        public void LeaveSlot_Vacates_Slot_On_Removal()
        {
            // Arrange
            InitializeParkingLot(6);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.LeaveSlot(It.IsAny<string[]>()));
            string command = "Leave 2";
            var result = mock.Object.LeaveSlot(command.Split(" "));

            //Assert
            Assert.IsTrue(result.Contains("Slot number 2 vacated"));
        }

        [Test]
        [TestCase("Slot_numbers_for_driver_of_age 21", "Slots")]
        [TestCase("Slot_numbers_for_driver_of_age 90", "Vehicle")]
        public void QueryOnAge_Returns_No_Driver(string command, string output)
        {
            // Arrange
            InitializeParkingLot(6);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.QueryOnDriverAge(It.IsAny<string[]>(),It.IsAny<string>()));
            var result = mock.Object.QueryOnDriverAge(command.Split(" "), output);

            //Assert
            Assert.IsTrue(result.Contains("No such driver"));
        }

        [Test]
        [TestCase("Slot_numbers_for_driver_of_age 40", "Slots")]
        [TestCase("Slot_numbers_for_driver_of_age 40", "Vehicle")]
        public void QueryOnAge_Returns_Correct_Count(string command, string output)
        {
            // Arrange
            InitializeParkingLot(6);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.QueryOnDriverAge(It.IsAny<string[]>(), It.IsAny<string>()));
            var result = mock.Object.QueryOnDriverAge(command.Split(" "), output);

            //Assert
            Assert.AreEqual(result.Split(",").Length,2);
        }

        [TestCase("Slot_numbers_for_driver_of_age 40", "Vehicle")]
        public void QueryOnAge_Returns_Correct_Slot_On_Slot_Leaving(string command, string output)
        {
            // Arrange
            InitializeParkingLot(6);

            var mock = new Mock<Adapter>()
            {
                CallBase = true
            };
            mock.Object.parkingLot = lot;

            //Act
            mock.Setup(x => x.QueryOnDriverAge(It.IsAny<string[]>(), It.IsAny<string>()));
            mock.Setup(x => x.LeaveSlot(It.IsAny<string[]>()));
            var result = mock.Object.QueryOnDriverAge(command.Split(" "), output);

            //Assert
            Assert.AreEqual(result,"D,E");

            mock.Object.LeaveSlot("Leave 4".Split(" "));
            result = mock.Object.QueryOnDriverAge(command.Split(" "), output);
            //Assert
            Assert.AreEqual(result, "E");
        }

        private void InitializeParkingLot(int size)
        {
            lot = new ParkingLot.ParkingLot(size);
            Adapter obj = new Adapter();
            obj.parkingLot = lot;
            obj.CommandCall("Park A driver_age 10");
            obj.CommandCall("Park B driver_age 20");
            obj.CommandCall("Park C driver_age 30");
            obj.CommandCall("Park D driver_age 40");
            obj.CommandCall("Park E driver_age 40");
        }
    }
}