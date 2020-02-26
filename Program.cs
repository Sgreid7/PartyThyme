using System;
using System.Linq;

namespace PartyThyme
{
  class Program
  {


    static void Main(string[] args)
    {
      // define isRunning variable
      var isRunning = true;
      // DEFINE ERROR MESSAGE
      var errorMessage = "Not a valid input, please try again";
      // CONNECT TO THE PLANT DATABASE
      var db = new PlantContext();
      // GREET USER
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      Console.WriteLine("It's good to see you, fellow plant enthusiast!");
      Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
      // Add space 
      Console.WriteLine();
      while (isRunning)
      {
        // ASK WHAT THEY WOULD LIKE TO DO, PROVIDE OPTIONS
        Console.WriteLine("What would you like to do today?");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("(VIEW 'v') all plants, (PLANT 'p') a plant, (REMOVE 'r') a plant, (WATER 'w') a plant, ");
        Console.WriteLine("(VIEW 'n') plants not watered today, (LOCATION 'l') summary, or (QUIT 'q')");
        // GET USER INPUT AND CREATE SWITCH STATEMENT FOR OPTIONS ABOVE
        var input = Console.ReadLine().ToLower();
        // Add space
        Console.WriteLine();

        switch (input)
        {
          // * * * * * VIEW * * * * *
          case "v":
            // VIEW ALL PLANTS IN DATABASE
            var plants = db.Plants.OrderBy(p => p.LocatedPlant);
            foreach (var p in plants)
            {
              Console.WriteLine("-----------------------------------------------------------");
              Console.WriteLine($"Primary Key: {p.Id}");
              Console.WriteLine($"Species: {p.Species}");
              Console.WriteLine($"Location of plant: {p.LocatedPlant}");
              Console.WriteLine($"Date planted: {p.PlantedDate.ToString("MMMM dd, yyyy")}.");
              Console.WriteLine($"Last time plant was watered: {p.LastWateredDate.ToString("MMMM dd, yyyy")}.");
              Console.WriteLine($"How many hours of sunlight needed per day: {p.LightNeeded}.");
              Console.WriteLine($"How many milliliters of water needed per day: {p.WaterNeeded}.");
              Console.WriteLine("-----------------------------------------------------------");
            }
            break;

          // * * * * * PLANT * * * * *
          case "p":
            // PROMPT USER WITH QUESTIONS ABOUT THE PLANT
            Console.WriteLine("What type of species is the plant?");
            var species = Console.ReadLine().ToLower();
            // Add space
            System.Console.WriteLine();
            Console.WriteLine("Where did you find the plant?");
            var location = Console.ReadLine().ToLower();
            // Add space
            System.Console.WriteLine();
            Console.WriteLine("When was the plant planted?");
            DateTime plantedDate;
            var isValidDate = DateTime.TryParse(Console.ReadLine(), out plantedDate);
            // Validate planted date
            while (!isValidDate)
            {
              Console.WriteLine(errorMessage);
              DateTime.TryParse(Console.ReadLine(), out plantedDate);
            }
            // Add space
            System.Console.WriteLine();

            Console.WriteLine("When was the last time the plant was watered?");
            DateTime lastWateredDate;
            isValidDate = DateTime.TryParse(Console.ReadLine(), out lastWateredDate);
            // validate last watered date
            while (!isValidDate)
            {
              Console.WriteLine(errorMessage);
              DateTime.TryParse(Console.ReadLine(), out lastWateredDate);
            }
            // Add space
            System.Console.WriteLine();

            Console.WriteLine("How much sunlight is needed for this plant (hours per day)?");
            double sunlightNeeded;
            var isDouble = double.TryParse(Console.ReadLine(), out sunlightNeeded);
            // validate sunlight needed
            while (!isDouble)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out sunlightNeeded);
            }
            // Add space
            System.Console.WriteLine();
            Console.WriteLine("How much water is needed for this plant (in ml per day)?");
            double waterNeeded;
            isDouble = double.TryParse(Console.ReadLine(), out waterNeeded);
            // validate water needed
            while (!isDouble)
            {
              Console.WriteLine(errorMessage);
              double.TryParse(Console.ReadLine(), out waterNeeded);
            }
            // Add space
            System.Console.WriteLine();

            // Fill in the plant properties with the input provided above
            var plant = new Plant()
            {
              Species = species,
              LocatedPlant = location,
              PlantedDate = plantedDate,
              LastWateredDate = lastWateredDate,
              LightNeeded = sunlightNeeded,
              WaterNeeded = waterNeeded
            };
            // Add plant to the database
            db.Plants.Add(plant);
            // Save changes
            db.SaveChanges();
            break;

          // * * * * * REMOVE PLANT * * * * *
          case "r":
            // Ask which user what plant they would like to remove
            Console.WriteLine("Which plant would you like to remove from the database? Please select the ID.");
            // Get user input
            int removePlantID;
            // Parse to int
            var isInteger = int.TryParse(Console.ReadLine(), out removePlantID);
            // Check to see if ID is in database
            var isInDB = db.Plants.Any(p => p.Id == removePlantID);

            // validate: check if not an integer or not in the database
            while (!isInteger || !isInDB)
            {
              if (!isInteger)
              {
                // Add space 
                Console.WriteLine();
                System.Console.WriteLine("Not a valid integer");
              }
              else if (!isInDB)
              {
                // Add space 
                Console.WriteLine();
                System.Console.WriteLine("That value is not in the database");
              }
              // Add space 
              Console.WriteLine();
              // Prompt user again
              isInteger = int.TryParse(Console.ReadLine(), out removePlantID);
              isInDB = db.Plants.Any(p => p.Id == removePlantID);
            }
            // Set the first instance of the removeplantid where it is equal to that ID in the database to a variable
            var removePlant = db.Plants.First(p => p.Id == removePlantID);
            // Remove the plant that the user chose
            db.Plants.Remove(removePlant);
            db.SaveChanges();
            break;

          // * * * * * WATER PLANT * * * * *
          case "w":
            // Add space 
            Console.WriteLine();
            // Ask user which plant they would like to water
            Console.WriteLine("Which plant would you like to water? Please select the ID.");
            // Get user input
            int waterPlantID;
            // Parse to int
            isInteger = int.TryParse(Console.ReadLine(), out waterPlantID);
            // Check to see if ID is in database
            isInDB = db.Plants.Any(p => p.Id == waterPlantID);

            // validate: check if not an integer or not in the database
            while (!isInteger || !isInDB)
            {
              if (!isInteger)
              {
                // Add space 
                Console.WriteLine();
                System.Console.WriteLine("Not a valid integer");
              }
              else if (!isInDB)
              {
                // Add space 
                Console.WriteLine();
                System.Console.WriteLine("That value is not in the database");
              }
              // Add space 
              Console.WriteLine();
              // Prompt user again
              isInteger = int.TryParse(Console.ReadLine(), out waterPlantID);
              isInDB = db.Plants.Any(p => p.Id == waterPlantID);
            }
            // Set the first instance of the ID's matching to waterplant variable
            var waterPlant = db.Plants.First(p => p.Id == waterPlantID);
            // UPDATE the lastWateredDate property for that plant to the current time
            waterPlant.LastWateredDate = DateTime.Today;
            db.SaveChanges();
            break;

          // * * * * * VIEW PLANTS NOT WATERED TODAY * * * * *
          case "n":
            // Add space 
            Console.WriteLine();
            // Filter plants that haven't been watered today
            var plantsNotWateredToday = db.Plants.Where(p => p.LastWateredDate != DateTime.Today);
            foreach (var p in plantsNotWateredToday)
            {
              System.Console.WriteLine("-------------------------------------------");
              Console.WriteLine($"Species: {p.Species}");
              Console.WriteLine($"Last time watered: {p.LastWateredDate.ToString("MMMM dd, yyyy")}");
              System.Console.WriteLine("-------------------------------------------");
            }
            db.SaveChanges();
            // Add space 
            Console.WriteLine();
            break;

          // * * * * * VIEW plants given a location * * * * *
          case "l":
            // Add space 
            Console.WriteLine();
            System.Console.WriteLine("Please choose a location to view all the plants in that location:");
            var chosenLocation = Console.ReadLine().ToLower();
            // Add space 
            Console.WriteLine();
            // Validate to see if location exists in database
            while (!db.Plants.Any(l => l.LocatedPlant == chosenLocation))
            {
              System.Console.WriteLine(errorMessage);
              chosenLocation = Console.ReadLine().ToLower();
            }
            // Filter plants to the specific location
            var locatedPlants = db.Plants.Where(p => p.LocatedPlant == chosenLocation);
            // Loop over each of the plants in the plants that are in the specific location and print species and location
            foreach (var p in locatedPlants)
            {
              Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
              Console.WriteLine($"{p.Species}'s location is: {p.LocatedPlant}.");
              Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }
            // Add space 
            Console.WriteLine();
            break;

          // * * * * * QUIT * * * * *
          case "q":
            // Add Space
            System.Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("Thanks for checking out the flower database!");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            // Set is running to false and exit while loop
            isRunning = false;
            break;
        }

      }
    }
  }
}
