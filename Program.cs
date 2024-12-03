using System.Security.Cryptography.X509Certificates;

namespace ParkingSystem{
    class Program{
        static List<Vehicle> ParkingLot = new List<Vehicle>();
        const int hargaMobil = 3000;
        const int hargaMotor = 2000;
        static int vehicleCounter = 1;
        static int totalLots = 0;

        class Vehicle{
            public string Key {get; set;}
            public string Plat {get; set;}
            public string Jenis { get; set; }
            public string Warna { get; set; }

            public Vehicle(string key, string plat, string jenis, string warna){
                Key = key;
                Plat = plat;
                Jenis = jenis;
                Warna = warna;
            }
        }
        static void Checkin(string key, string plat, string jenis, string warna)
        {
            ParkingLot.Add(new Vehicle(key, plat, jenis, warna));
        }

        static void ShowVehicleCount(string vehicleType)
        {
            int count = 0;
            if(vehicleType.Equals("mobil", StringComparison.OrdinalIgnoreCase))
            {
                count = ParkingLot.Count(v => v.Jenis.Equals("mobil", StringComparison.OrdinalIgnoreCase));
                Console.WriteLine(count);
            }
            if(vehicleType.Equals("motor", StringComparison.OrdinalIgnoreCase))
            {
                count = ParkingLot.Count(v => v.Jenis.Equals("motor", StringComparison.OrdinalIgnoreCase));
                Console.WriteLine(count);
            }
        }
        static void Main (string[] args){
            string inputuser;
            while ((inputuser = Console.ReadLine()) != "exit")
            {
                var input = inputuser.Split(" ");
                switch(input[0])
                { 
                    case "create_parking_lot":
                    totalLots = int.Parse(input[1]);
                    Console.WriteLine("Created a parking lot with " + totalLots + " slots");
                    break;
                    
                    case "park":
                    string newKey = vehicleCounter.ToString();
                        vehicleCounter++;
                        // Panggil fungsi Checkin di sini
                        if (ParkingLot.Count < totalLots)
                        {
                            Checkin(newKey, input[1].ToUpper(), input[2], input[3]);
                            Console.WriteLine("Allocated slot number : " + newKey);
                        }
                        else
                        {
                            Console.WriteLine("Sorry, parking lot is full");
                        }
                        break;

                    case "status":
                    Console.WriteLine("slot\tNo.\tType\tRegistration Number\tColour");
                    foreach(var vehicle in ParkingLot)
                    {
                        Console.WriteLine($"{vehicle.Key}\t{vehicle.Plat}\t{vehicle.Jenis}\t{vehicle.Warna}");
                    };
                    break;

                    case "leave":
                    if (input.Length > 1)
                        {
                            if (int.TryParse(input[1], out int slotNumber))
                            {
                                // Cari kendaraan berdasarkan slot (key)
                                var vehicleToRemove = ParkingLot.FirstOrDefault(v => int.Parse(v.Key) == slotNumber);

                                if (vehicleToRemove != null)
                                {
                                    ParkingLot.Remove(vehicleToRemove);
                                    Console.WriteLine($"Slot number {slotNumber} is free");
                                }
                                else
                                {
                                    Console.WriteLine("Slot not found or already empty.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid slot number");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please provide a slot number.");
                        }
                        break;

                        case "slot_number_for_registration_number" :
                        string plat = input[1];
                        var vehiclesearch = ParkingLot.FirstOrDefault(v => v.Plat.Equals(plat, StringComparison.OrdinalIgnoreCase));
                        if(vehiclesearch != null)
                        {
                            Console.WriteLine($"{vehiclesearch.Key}");
                        }
                        else{
                            Console.WriteLine("Tidak ada plat tersebut dalam slot parkir");
                        }
                        break;

                        case "type_of_vehicle" :
                        string vehicleType = input[1];
                        ShowVehicleCount(vehicleType);
                        break;
                }
            }
        }
    }
}