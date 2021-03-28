# ParkingLot
About the app:
-This ParkingLot is a console application, developed in a cross platform .NET core 2.1
-There is no external library used for coding.
-Unit tests are coded using NUnit3 and Moq for mocking
-The Console application is injected with a adapter dependency to separate the logic.
-Main processor code resides in the Adapter.cs

Follow the below steps to setup .Net core 2.1 on your platform

macOS
Install the .NET Core 2.1 Runtime on macOS:
- Go to the below link to install a .NET Core 2.1 runtime for macOS
 https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-2.1.26-macos-x64-installer
- Now, double click the installer(.pkg) and follow the on screen instructions to install it.
- After installation, verify in your bash by using the command:
  dotnet --version
- If it returns a version, then .NET Core is installed successfully.

UBUNTU
Install the .NET Core 2.1 Runtime on Ubuntu:
- Check your compatible Ubuntu version from the list for an active support
- Now, for any version, installing the .NET Runtime has the same steps and commands
- Before you install .NET, run the following commands to add the Microsoft package signing key to your list of trusted keys and add the   
  package repository. Open a terminal and run the following commands:

wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

- Now install the runtime by using the following command:
sudo apt-get install -y dotnet-runtime-2.1

- Use sudo apt-get update if the above command fails and try again.

Running the application and unit tests:
This is valid for any system: Windows, Mac or Ubuntu, considering that the .Net core runtime has been installed from the above steps

Running the ParkingLot console application:
In your terminal, navigate to the ParkingLot project directory:
..\ParkingLot\ParkingLot\bin\Release\netcoreapp2.1\publish
Run the below command:
 dotnet ParkingLot.dll
 <Paste the input file location>


Running the ParkingLot UnitTests from console:
In your terminal, navigate to the ParkingLot tests directory:
..\ParkingLot\ParkingLotTests\bin\Release\netcoreapp2.1\publish
Run the below command:
 dotnet test -v n ParkingLotTests.dll
