    using System;

    namespace Parkering
    {
        internal class Program
        {
            static void Main(string[] args)
            {
            
                int parkingSizeX = 15;
                int parkingSizeY = 2;
                double carsParked = 0; //double for motorcycle
                Vehicle[,] parkingSpaces = new Vehicle[parkingSizeX, parkingSizeY]; //2d array 15x2 
            


                while (true)
                {
                    Console.Clear();
                    Helpers.PrintParking(parkingSpaces, parkingSizeX, parkingSizeY); //print parking
                    Console.WriteLine();
                    Console.WriteLine("(A)dd a vehicle to parking");
                    Console.WriteLine("(C)heckout");
                    Console.WriteLine(carsParked.ToString());
                    string option = Console.ReadLine().ToLower();

                    switch (option)
                    {
                    case "c":
                        Console.Clear();
                        carsParked = Helpers.RemoveVehicle(parkingSpaces, parkingSizeX, parkingSizeY, carsParked);
                        break;
                    case "a":
                        Console.Clear();
                        Helpers.PrintParking(parkingSpaces, parkingSizeX, parkingSizeY); //print parking

                        Console.WriteLine("What type of vehicle do you want to park? (car, bus or motorcycle)");
                        string typeOfVehicle = Helpers.GetRandomVehicleType();
                        carsParked=Helpers.AddVehicle(typeOfVehicle, carsParked, parkingSizeX, parkingSpaces);
                        break;
                    }


                }

                
            }
        }
    }