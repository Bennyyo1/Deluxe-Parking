using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class Helpers
    {
        public static string RandomLicencePlate(Random random)
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string licensePlate = "";

            for (int i = 0; i < 3; i++)
            {
                licensePlate += letters[random.Next(letters.Length)]; //generate three letters
            }

            for (int i = 0; i < 3; i++)
            {
                
                licensePlate += numbers[random.Next(numbers.Length)];//generate three numbers
            }

            return licensePlate;
        }

        public static string RandomColor(Random random)
        {
            string[] colors = { "red", "blue", "green", "yellow", "orange", "purple","black" };
            int index = random.Next(colors.Length);
            return colors[index];
        }

        public static void PrintParking(Vehicle[,] parkingSpaces, int parkingSizeX, int parkingSizeY)
        {
            int platsWidth = (int)Math.Log10(parkingSizeX + 1) + 1; //formatting

            for (int x = 0; x < parkingSizeX; x++)
            {
                string platsLabel = "Plats " + (x + 1).ToString().PadLeft(platsWidth); //formatting
                Console.Write(platsLabel);

                //track the vehicle type at the x position (first position)
                Vehicle vehicleX = parkingSpaces[x, 0];
                Vehicle vehicleY = parkingSpaces[x, 1];

                if(vehicleX == null)
                {
                    //check if the Y position is also a motorcycle
                    if (vehicleY is Motorcycle motorcycleY)
                    {
                        Console.WriteLine();
                        Console.Write("Plats " + (x + 1).ToString().PadLeft(platsWidth));
                        Console.Write(" " + motorcycleY.Type.PadRight(15) + motorcycleY.LicensePlate.PadRight(15) + motorcycleY.Color.PadRight(15));
                        Console.Write(" " + motorcycleY.Brand);
                    }
                }

                if (vehicleX != null)
                {
                    Console.Write(" " + vehicleX.Type.PadRight(15) + vehicleX.LicensePlate.PadRight(15) + vehicleX.Color.PadRight(15));

                    if (vehicleX is Car car)
                    {
                        if (car.IsElectric)
                        {
                            Console.Write(" Electric car");
                        }
                        else
                        {
                            Console.Write(" Not electric");
                        }
                    }
                    else if (vehicleX is Bus bus)
                    {
                        Console.Write(" " + bus.Passengers);
                    }
                    else if (vehicleX is Motorcycle motorcycle)
                    {
                        Console.Write(" " + motorcycle.Brand);

                        // Check if the Y position is also a motorcycle
                        if (vehicleY is Motorcycle motorcycleY)
                        {
                            Console.WriteLine();
                            Console.Write("Plats " + (x + 1).ToString().PadLeft(platsWidth));
                            Console.Write(" " + motorcycleY.Type.PadRight(15) + motorcycleY.LicensePlate.PadRight(15) + motorcycleY.Color.PadRight(15));
                            Console.Write(" " + motorcycleY.Brand);
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        public static double RemoveVehicle(Vehicle[,] parkingSpaces, int parkingSizeX, int parkingSizeY, double carsParked)
        {
            double parkPrice = 1.5; //current price per min
            Helpers.PrintParking(parkingSpaces, parkingSizeX, parkingSizeY); //print parking
            Console.WriteLine("Enter the License Plate of the car you want to check out of parking. ");
            string licensePlateToRemove = Console.ReadLine().ToUpper();
            bool found = false;

            for (int row = 0; row < parkingSizeX; row++) //loop X
            {
                for (int col = 0; col < parkingSizeY; col++) //loop Y
                {
                    Vehicle vehicle = parkingSpaces[row, col]; //get the object stored at location

                    if (vehicle != null && vehicle.LicensePlate == licensePlateToRemove)//License plate found, remove vehicle
                    {
                        found = true;
                        if (vehicle is Car)
                        {
                            carsParked -= 0.5; 
                        }
                        else if (vehicle is Bus)
                        {
                            carsParked -= 0.5; 
                        }
                        else if (vehicle is Motorcycle)
                        {
                            carsParked -= 0.5; 
                        }

                        parkingSpaces[row, col] = null;
                    }
                }
            }

            if (found) //if vehicle found
            {
                Console.Clear();
                Console.WriteLine("The vehicle with license plate " + licensePlateToRemove + " has been removed.");

                Random randomTimer = new Random();
                int minutesParked = randomTimer.Next(60);
                double randomParkCost = minutesParked * parkPrice; //calc park cost 0-59 min * 1.5kr
                Console.WriteLine("You have been parked for " + minutesParked + " minutes. You have been billed " + randomParkCost + "kr.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            else //not found
            {
                Console.Clear();
                Console.WriteLine("Vehicle with the specified license plate was not found in the parking.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            return carsParked;
        }

        public static double CurrentVehiclesParked(Vehicle[,] parkingSpaces, int parkingSizeX, double carsParked)
        {
            

            foreach (var vehicle in parkingSpaces)
            {
                if (vehicle is Car)
                {
                    carsParked +=0.5;
                }
                else if (vehicle is Bus)
                {
                    carsParked +=0.5;
                }
                else if (vehicle is Motorcycle)
                {
                    carsParked +=0.5;
                }
            }

            Console.WriteLine("There are currently " +carsParked + " parking spots occupied");
            Console.WriteLine("There are currently " +(parkingSizeX-carsParked) + " spots left");
            return carsParked;
        }

        public static string GetRandomVehicleType()
        {
            Random random = new Random();
            int randomNumber = random.Next(3); // Generates a random number between 0 and 2

            switch (randomNumber)
            {
                case 0:
                    return "car";
                case 1:
                    return "bus";
                case 2:
                    return "motorcycle";
                default:
                    return "car";
            }
        }

        public static double AddVehicle(string typeOfVehicle, double carsParked, int parkingSizeX, Vehicle[,] parkingSpaces)
        {
            switch (typeOfVehicle)
            {
                case "car":
                    if (carsParked < 14.5)
                    {

                        Console.Clear();
                        Console.WriteLine("You have chosen to park a car");

                        Random randomPlateCar = new Random();
                        string licensePlateCar = Helpers.RandomLicencePlate(randomPlateCar); //get a random plate

                        Random randomColorCar = new Random();
                        string colorCar = Helpers.RandomColor(randomColorCar); //get a random color

                        bool isValidInput = false;
                        bool isElectric = false;

                        while (!isValidInput)
                        {
                            Console.WriteLine("Do you have an electric car? (Y/N)");
                            string carElectricString = Console.ReadLine().ToLower();

                            if (carElectricString == "y")
                            {
                                isElectric = true;
                                isValidInput = true;
                            }
                            else if (carElectricString == "n")
                            {
                                isElectric = false;
                                isValidInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No.");
                            }
                        }

                        bool foundSingleSpot = false; //check if we can find a single open spot

                        for (int row = 0; row < parkingSizeX; row++) //loop through all parking spots
                        {
                            if (parkingSpaces[row, 0] == null && parkingSpaces[row, 1] == null)
                            {
                                if (row == parkingSizeX - 1 || parkingSpaces[row + 1, 0] != null || parkingSpaces[row + 1, 1] == null)
                                {
                                    foundSingleSpot = true; //a single spot was found

                                    Car car = new Car("Car", licensePlateCar, colorCar, isElectric);
                                    parkingSpaces[row, 0] = car;
                                    parkingSpaces[row, 1] = car;
                                    Console.WriteLine("Your car was parked at row " + row + " (single)");
                                    Console.WriteLine("Your car is the color: " + car.Color + " and has the license " + car.LicensePlate + " and is electric" + car.IsElectric);
                                    carsParked += 1;

                                    break;
                                }
                            }
                        }

                        if (!foundSingleSpot)
                        {
                            for (int row2 = 0; row2 < parkingSizeX; row2++) //loop through all parking spots again
                            {
                                if (parkingSpaces[row2, 0] == null && parkingSpaces[row2, 1] == null)
                                {
                                    Car car = new Car("Car", licensePlateCar, colorCar, isElectric);
                                    parkingSpaces[row2, 0] = car;
                                    parkingSpaces[row2, 1] = car;
                                    Console.WriteLine("Your car was parked at row " + row2 + " (double)");
                                    Console.WriteLine("Your car is the color: " + car.Color + " and has the license " + car.LicensePlate + " and is electric" + car.IsElectric);
                                    carsParked += 1;

                                    break;
                                }
                            }
                        }
                    }

                    else
                    {
                        Console.WriteLine("There is no space left for a car on the parking");
                        Thread.Sleep(2000);

                    }
                    break;
                
                case "bus":

                    if (carsParked <= 13)
                    {
                        Console.Clear();
                        int passengersAmount = 0;
                        Console.WriteLine("You have chosen to park a bus.");
                        Random randomPlateBus = new Random();
                        string licensePlateBus = Helpers.RandomLicencePlate(randomPlateBus); //get random plate

                        Random randomColorBus = new Random();
                        string colorBus = Helpers.RandomColor(randomColorBus); //get random color


                        bool isValidInput = false;

                        while (!isValidInput)
                        {
                            Console.WriteLine("How many passengers are on the bus? ");
                            string input = Console.ReadLine();

                            if (int.TryParse(input, out passengersAmount))
                            {
                                isValidInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number of passengers.");
                            }
                        }
                        for (int row = 0; row < parkingSizeX - 1; row++)//loop x
                        {
                            if (parkingSpaces[row, 0] == null && parkingSpaces[row, 1] == null)//titta om plats är ledig X och Y
                            {
                                if (parkingSpaces[row + 1, 0] == null && parkingSpaces[row + 1, 1] == null) //platsen bredvid också är ledig X och Y
                                {

                                    Bus bus = new Bus("Bus", licensePlateBus, colorBus, passengersAmount);
                                    parkingSpaces[row, 0] = bus; //add the bus to the matrix X
                                    parkingSpaces[row + 1, 0] = bus; //add the bus to the matri X+1
                                    parkingSpaces[row, 1] = bus; //add the bus to the matrix Y
                                    parkingSpaces[row + 1, 1] = bus; //add the bus to the matrix Y+1
                                    Console.WriteLine("Your Bus was parked at row " + row);
                                    Console.WriteLine("Your Bus is the color: " + bus.Color + " and has the license " + bus.LicensePlate + " and has " + bus.Passengers + " passengers.");
                                    carsParked += 2;

                                    break;
                                }


                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no space left for a bus on the parking");
                        Thread.Sleep(2000);
                    }




                    break;
                
                case "motorcycle":

                    Console.Clear();
                    Console.WriteLine("You have chosen to park a motorcycle.");
                    Random randomPlateMotorcycle = new Random();
                    string licensePlateMotorcycle = Helpers.RandomLicencePlate(randomPlateMotorcycle); //get random plate

                    Random randomColorMotorCycle = new Random();
                    string colorMotorCycle = Helpers.RandomColor(randomColorMotorCycle); //get random color

                    bool foundFreeSpace = false; //check if a free space is found

                    Console.WriteLine("What brand is your motorcycle? ");
                    string motorcycleBrand = Console.ReadLine();


                    for (int row = 0; row < parkingSizeX; row++)
                    {
                        if (parkingSpaces[row, 0] == null)
                        {
                            //park the motorcycle in an X spot
                            parkingSpaces[row, 0] = new Motorcycle("Motorcycle", licensePlateMotorcycle, colorMotorCycle, motorcycleBrand);
                            Console.WriteLine("Your motorcycle was parked at row " + (row + 1) + " in the X spot");
                            Console.WriteLine("Your motorcycle is " + colorMotorCycle + " and has the license " + licensePlateMotorcycle + " and the brand is " + motorcycleBrand);
                            foundFreeSpace = true; //a free space has been found
                            carsParked += 0.5;
                            Console.ReadKey();
                            break; //exit the loop after parking the motorcycle
                        }
                        else if (parkingSpaces[row, 1] == null)
                        {
                            //park the new motorcycle next to the existing motorcycle in the Y spot
                            parkingSpaces[row, 1] = new Motorcycle("Motorcycle", licensePlateMotorcycle, colorMotorCycle, motorcycleBrand);
                            Console.WriteLine("Your motorcycle was parked next to an existing motorcycle at row " + (row + 1) + " in the Y spot");
                            Console.WriteLine("Your motorcycle is " + colorMotorCycle + " and has the license " + licensePlateMotorcycle + " and the brand is " + motorcycleBrand);
                            foundFreeSpace = true; //a free space has been found
                            carsParked += 0.5;
                            Console.ReadKey();
                            break; //exit the loop after parking the motorcycle
                        }
                    }

                    if (!foundFreeSpace)
                    {
                        Console.WriteLine("No space for the motorcycle is available.");
                        Thread.Sleep(2000);
                    }
                    break;

                default:
                    Console.WriteLine("Please enter a valid vehicle");
                    Thread.Sleep(2000);
                    break;
                    
                
            }
            return carsParked;
        }



    }
}
