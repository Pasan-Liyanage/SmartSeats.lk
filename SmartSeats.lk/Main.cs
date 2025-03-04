using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartSeats.lk
{
	public class Interface
	{
        
        public static void Main(string[] args)
        {
            int SelectedBus;
            int SelectedSeat;
            int ExitBus = 0;
            string UserID = "23423v";
            int JourneyDate = 12;
            int JourneyMonth = 01;
            int JourneyYear = 2025;
            string From;
            string To;

            //Creating a bus pool
            BusPool busPool = new BusPool();
            busPool = Admin1();

            BusPool DateCheckedBuses = new BusPool();
            BusPool JourneyCheckedBuses = new BusPool();
            
            while (ExitBus != 1)
            {
                

                PrintLogo();
                //user input data
                PrintCenter("Enter your NIC : ");

                UserID = Console.ReadLine().ToLower(); NL(1);
          
                while (true)
                {
                    PrintCenter("Enter the date of your journey : ");
                    if (int.TryParse(Console.ReadLine(), out JourneyDate) && JourneyDate >= 1 && JourneyDate <= 31)
                        break;
                    PrintWithColorAndCenter(ConsoleColor.Red, "Invalid date. Please enter a number between 1 and 31.");
                    NL(1);
                }

                while (true)
                {
                    PrintCenter("Enter the month of your journey : ");
                    if (int.TryParse(Console.ReadLine(), out JourneyMonth) && JourneyMonth >= 1 && JourneyMonth <= 12)
                        break;
                    PrintWithColorAndCenter(ConsoleColor.Red, "Invalid month.Please enter a number between 1 and 12.");
                    NL(1);
                }

                while (true)
                {
                    PrintCenter("Enter the year of your journey : ");
                    if (int.TryParse(Console.ReadLine(), out JourneyYear) && JourneyYear >= 2025 && JourneyYear <= 2027)
                        break;
                    PrintWithColorAndCenter(ConsoleColor.Red, "Invalid year. Please enter a year from 2025 ");
                    NL(1);
                }
              
                NL(1);
                PrintCenter("Enter where the begin your journey : ");
                From = Console.ReadLine().ToLower(); NL(1);
                

                PrintCenter("Enter where the end of your journey : ");
                To = Console.ReadLine().ToLower(); NL(2);

                //Search for buses for user enterd data
                DateCheckedBuses = busPool.CheckDate(JourneyDate, JourneyMonth, JourneyYear);
                JourneyCheckedBuses = DateCheckedBuses.CheckJourney(From, To);
                JourneyCheckedBuses.PrintBusPool();
                //NL(1);

                // select a bus
                while (true)
                {
                    PrintCenter("Select one bus from its index : ");
                    if (int.TryParse(Console.ReadLine(), out SelectedBus) && SelectedBus >= 0 && SelectedBus <= JourneyCheckedBuses.Count)
                        break;
                    PrintWithColorAndCenter(ConsoleColor.Red, $"Invalid bus number. Please enter a number between 0 and {JourneyCheckedBuses.Count - 1}.");
                    NL(1);


                }
                NL(1);

                int ExitSeat = 0;
                while (ExitSeat != 1)
                {
                    //JourneyCheckedBuses.bus[SelectedBus].CalcSeatStatus(From, To);
                    //prints the seats of selected bus
                    JourneyCheckedBuses.bus[SelectedBus].CalcSeatStatus(From, To);
                    JourneyCheckedBuses.bus[SelectedBus].PrintSeats();
                    NL(3);

                    //select a seat and book
                    while (true)
                    {
                        PrintCenter("Select a seat from its index : ");
                        if (int.TryParse(Console.ReadLine(), out SelectedSeat) && SelectedSeat >= 0 && SelectedSeat <= JourneyCheckedBuses.bus[SelectedBus].NoOfSeats)
                            break;
                        PrintWithColorAndCenter(ConsoleColor.Red, $"Invalid seat number. Please enter a number between 0 and {JourneyCheckedBuses.bus[SelectedBus].NoOfSeats - 1}.");
                        NL(1);


                    }
                    NL(2);

                    JourneyCheckedBuses.bus[SelectedBus].BookSeat(UserID, SelectedSeat, From, To);
                    JourneyCheckedBuses.bus[SelectedBus].CalcSeatStatus(From, To);
                    JourneyCheckedBuses.bus[SelectedBus].PrintSeats();
                    NL(3);


                    PrintWithColor(ConsoleColor.Cyan, "Booked portion of the seat.");
                    //Console.WriteLine();
                    JourneyCheckedBuses.bus[SelectedBus].seat[SelectedSeat].BookedRoute.PrintRoute();
                    NL(3);
                    PrintWithColor(ConsoleColor.Cyan, "Non-Booked portion of the seat, You can book this portions,");
                    //Console.WriteLine("");
                    JourneyCheckedBuses.bus[SelectedBus].seat[SelectedSeat].NonBookedRoute.PrintRoute();
                    NL(4);


                    PrintWithColor(ConsoleColor.Cyan, "Booking status of the seat with the user.");
                    JourneyCheckedBuses.bus[SelectedBus].seat[SelectedSeat].PrintUserRoute();
                    NL(3);

                    while (true)
                    {
                        PrintWithColorAndCenter(ConsoleColor.Red, "Do you want to select another seat ? - If yes enter '0', no enter '1' : ");
                        if (int.TryParse(Console.ReadLine(), out ExitSeat) && ExitSeat >= 0 && ExitSeat <= 1)
                            break;
                        PrintWithColorAndCenterNoNL(ConsoleColor.Red, "Invalid input. Please enter a number '1' or '0'.");
                    }

                }
                NL(2);
                while (true)
                {
                    PrintWithColorAndCenter(ConsoleColor.Red, "Do you want to exit ? - if yes enter '1' else enter '0' : ");
                    if (int.TryParse(Console.ReadLine(), out ExitBus) && ExitBus >= 0 && ExitBus <= 1)
                        break;
                    PrintWithColorAndCenterNoNL(ConsoleColor.Red, "Invalid input. Please enter a number '1' or '0'.");
                }
                NL(2);
            }

        
    }
        public static BusPool Admin1()
        {
            BusPool busPool = new BusPool();

            /*string[] r1 = { "Badulla", "Haliela", "Bandarawela", "Diyathalawa", "Haputhale", "Balangoda", "Pelmadulla", "Ratnapura", "Kuruwita", "Eheliyagoda", "Awissawella", "Kosgama", "Hanwella", "Kaduwela", "Colombo" };
            Route R1 = new Route();
            R1.SetRoute(r1);
            busPool.InsertBuses("Normal", "NC-3245", "Leyland", "Badulla", 2025, 01, 13, "12.45", "Colombo", 2025, 01, 12, "07.15", 5, R1);

            string[] r2 = { "Badulla", "Haliela", "Bandarawela", "Diyathalawa", "Haputhale", "Balangoda", "Pelmadulla", "Ratnapura", "Kuruwita", "Eheliyagoda", "Awissawella", "Kosgama", "Hanwella", "Kaduwela", "Colombo" };
            Route R2 = new Route();
            R2.SetRoute(r2);
            busPool.InsertBuses("Normal", "NC-3246", "Leyland", "Badulla", 2025, 01, 12, "12.45", "Colombo", 2025, 01, 12, "07.15", 5, R2);

            string[] r3 = { "Badulla", "Haliela", "Bandarawela", "Diyathalawa", "Haputhale", "Balangoda", "Pelmadulla", "Ratnapura", "Kuruwita", "Eheliyagoda", "Awissawella", "Kosgama", "Hanwella", "Kaduwela", "Colombo" };
            Route R3 = new Route();
            R3.SetRoute(r3);
            busPool.InsertBuses("Normal", "NC-3247", "Leyland", "Badulla", 2025, 01, 12, "12.45", "Colombo", 2025, 01, 12, "07.15", 30, R3);
            */
            string[] r4 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };
            Route R4 = new Route();
            R4.SetRoute(r4);
            busPool.InsertBuses("Normal", "NC-3247", "Leyland", "a", 2025, 01, 12, "10.45", "o", 2025, 01, 12, "09.15", 30, R4);

            string[] r5 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o" };
            Route R5 = new Route();
            R5.SetRoute(r5);
            busPool.InsertBuses("Normal", "NC-3250", "Leyland", "a", 2025, 01, 12, "12.45", "o", 2025, 01, 12, "07.15", 30, R5);


            return busPool;

        }
        public static void MColorPrint(string text)
        {
            // Define an array of colors
            ConsoleColor[] colors = new ConsoleColor[]
            {
            ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue,
            ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta
            };

            // Loop through each character in the text
            for (int i = 0; i < text.Length; i++)
            {
                // Change the color for each character
                Console.ForegroundColor = colors[i % colors.Length]; // Cycle through colors
                Console.Write(text[i]); // Print the character
            }

            // Reset the color to default
            Console.ResetColor();
        }
        public static void PrintLogo()
        {
            Console.Clear();

            // Logo with different colored parts
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                    *******************************        ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                                   *                              *\\       ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   *      SMART SEATS BUS         * \\      ");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("                                   *                              *  \\     ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                   *******************************    \\____");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                   *      ______               *           *");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   *     |      |   _______    *   o       *");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                   *     |______|  |       |   *  o        *");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   *               |_______|   *           *");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                                   *******************************         *");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                   *                             *         *");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                   *      BOOK YOUR JOURNEY      *         *");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                                   *******************************         *");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("                                       (o)                     (o)          ");

            Console.ResetColor();
            Console.WriteLine(""); Console.WriteLine("");



            // Calculate console width and the padding needed
            int windowWidth = Console.WindowWidth;
            string welcomeText = "Welcome to SmartSeats.lk";
            int padding = (windowWidth - welcomeText.Length) / 2;

            // Center-align the welcome message
            Console.Write(new string(' ', padding)); // Add spaces for alignment

            MColorPrint("Welcome "); MColorPrint("to "); MColorPrint("Smart"); MColorPrint("Seats."); MColorPrint("lk");
            Console.WriteLine(" "); Console.WriteLine(" "); Console.WriteLine(" ");

            PrintWithColorAndCenter(ConsoleColor.Cyan, "WORLD NO 1 BUS SEAT BOOKING SYSTEM");
            Console.WriteLine(" ");
            PrintWithColorAndCenter(ConsoleColor.Gray, "** You can book seats for your specific journey segment **");

            Console.WriteLine(" "); Console.WriteLine(" "); Console.WriteLine(" ");


        }

        //print text center with new line after printing
        public static void NPrintCenter(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;

            // Center-align the welcome message
            Console.Write(new string(' ', padding)); // Add spaces for alignment
            Console.WriteLine(text);

            // Reset the color to default
            Console.ResetColor();
        }
        //print text center without new line after printing
        public static void PrintCenter(string text)
        {
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;

            // Center-align the welcome message
            Console.Write(new string(' ', padding)); // Add spaces for alignment
            Console.Write(text);

            // Reset the color to default
            Console.ResetColor();
        }
        public static void PrintWithColor(ConsoleColor color, string text)
        {
            // Save the original color
            var originalColor = Console.ForegroundColor;

            // Set the color to the desired one
            Console.ForegroundColor = color;

            // Print the text in the specified color
            Console.WriteLine(text);

            // Reset to the original color
            Console.ForegroundColor = originalColor;
        }
        public static void PrintWithColorAndCenter(ConsoleColor color, string text)
        {
            // Save the original color
            var originalColor = Console.ForegroundColor;

            // Set the color to the desired one
            Console.ForegroundColor = color;

            // Calculate the padding for centering the text
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;

            // Center-align the text by adding spaces
            Console.WriteLine(new string(' ', padding) + text);

            // Reset to the original color
            Console.ForegroundColor = originalColor;
        }
        public static void PrintWithColorAndCenterNoNL(ConsoleColor color, string text)
        {
            // Save the original color
            var originalColor = Console.ForegroundColor;

            // Set the color to the desired one
            Console.ForegroundColor = color;

            // Calculate the padding for centering the text
            int windowWidth = Console.WindowWidth;
            int padding = (windowWidth - text.Length) / 2;

            // Center-align the text by adding spaces
            Console.Write(new string(' ', padding) + text);

            // Reset to the original color
            Console.ForegroundColor = originalColor;
        }
        public static void NL(int val)
        {
            for(int i = 0; i < val; i++)
            {
                Console.WriteLine("");
            }
        }
    }
}

