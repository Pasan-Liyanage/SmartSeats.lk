using System;
using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartSeats.lk
{
	public class BusPool
	{
		public int NoOfBuses;
		public int Count;
		public Bus[] bus;

        public BusPool()
        {
            NoOfBuses = 1;
            Count = 0;
            bus = new Bus[NoOfBuses];
        }

        public void InsertBuses(string busType, string busNo, string busModel, string departure, int departureYear, int departureMonth, int departureDate, string departureTime, string arrival, int arrivalYear, int arrivalMonth, int arrivalDate, string arrivalTime, int noOfSeats, Route busroute)
        {
            if(Count < NoOfBuses)
            {
                bus[Count] = new Bus();
                bus[Count].CreateBus(busType, busNo, busModel, departure, departureYear, departureMonth, departureDate, departureTime, arrival, arrivalYear, arrivalMonth, arrivalDate, arrivalTime, noOfSeats, busroute);
                Count++;
            }

            else
            {
                Bus[] temp = new Bus[NoOfBuses];

                for(int i = 0; i < NoOfBuses; i++)
                {
                    temp[i] = bus[i];
                }

                bus = new Bus[++NoOfBuses];

                for (int i = 0; i < NoOfBuses - 1; i++)
                {
                    bus[i] = temp[i];
                }
                bus[Count] = new Bus();
                bus[Count].CreateBus(busType, busNo, busModel, departure, departureYear, departureMonth, departureDate, departureTime, arrival, arrivalYear, arrivalMonth, arrivalDate, arrivalTime, noOfSeats, busroute);
                Count++;
            }
        }

        public void CopyBus(Bus NewBus)
        {
            if(Count < NoOfBuses)
            {
                bus[Count] = new Bus();
                bus[Count] = NewBus;
                Count++;
            }

            else
            {
                Bus[] temp = new Bus[NoOfBuses];

                for (int i = 0; i < NoOfBuses; i++)
                {
                    temp[i] = bus[i];
                }

                bus = new Bus[++NoOfBuses];

                for (int i = 0; i < NoOfBuses - 1; i++)
                {
                    bus[i] = temp[i];
                }
                bus[Count] = new Bus();
                bus[Count] = NewBus;
                Count++;
            }
        }

        public void PrintBusPool()
        {
            for(int i = 0; i < NoOfBuses; i++)
            {
                PrintWithColor(ConsoleColor.Cyan, "Bus index : " + i.ToString());
                bus[i].PrintBus();
            }
        }

        public BusPool CheckDate(int date, int month, int year)
        {
            BusPool DateCheckedBuses = new BusPool();

            for (int i = 0; i < NoOfBuses; i++)
            {
                if ((date == bus[i].DepartureDate) && (month == bus[i].DepartureMonth) && (year == bus[i].DepartureYear))
                {
                    DateCheckedBuses.CopyBus(bus[i]);
                }
            }
            return DateCheckedBuses;
        }

        public BusPool CheckJourney(string from, string to)
        {
            BusPool JourneyCheckedBuses = new BusPool();

            for (int i = 0; i < NoOfBuses; i++)
            {
                if (bus[i].JourneyCheck(from, to) == true)
                {
                    JourneyCheckedBuses.CopyBus(bus[i]);
                }
            }
            return JourneyCheckedBuses;
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

    }
}

 