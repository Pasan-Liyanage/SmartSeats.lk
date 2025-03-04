using System;
using SmartSeats.lk;
using System.Reflection;

namespace SmartSeats.lk
{
	public class Bus
	{
        public string BusType; //Normal, Semi-Luxury, Luxury
        public string BusNo; //Registration number
        public string BusModel; //Leyland, Tata

        public string Departure;
        public int DepartureYear;
        public int DepartureMonth;
        public int DepartureDate;
        public string DepartureTime;

        public string Arrival;
        public int ArrivalYear;
        public int ArrivalMonth;
        public int ArrivalDate;
        public string ArrivalTime;

        public int NoOfSeats;
        public int AvailableSeats;

        public Seat[] seat;

        public Route BusRoute;

        
        public void CreateBus(string busType, string busNo, string busModel, string departure, int departureYear, int departureMonth, int departureDate, string departureTime, string arrival, int arrivalYear, int arrivalMonth, int arrivalDate, string arrivalTime, int noOfSeats, Route busroute)
        {
            BusType = busType;
            BusNo = busNo;
            BusModel = busModel;
            Departure = departure;
            DepartureYear = departureYear;
            DepartureMonth = departureMonth;
            DepartureDate = departureDate;
            DepartureTime = departureTime;
            Arrival = arrival;
            ArrivalYear = arrivalYear;
            ArrivalMonth = arrivalMonth;
            ArrivalDate = arrivalDate;
            ArrivalTime = arrivalTime;
            NoOfSeats = noOfSeats;
            BusRoute = busroute;
            seat = new Seat[NoOfSeats];

            for(int i = 0; i < NoOfSeats; i++)
            {
                seat[i] = new Seat();
            }

            for (int i = 0; i < NoOfSeats; i++)
            {
                seat[i].NonBookedRoute = new Route(BusRoute);
            }

            for (int i = 0; i < NoOfSeats; i++)
            {
                seat[i].BookedRoute = new Route();
            }
        }

        public void PrintBus()
        {
            Console.WriteLine("*********************************************************************");
            Console.WriteLine("{0,-25} {1,-25} {2,-25}", "Bus Type", "Departure", "Arrival");
            Console.WriteLine("{0,-25} {1,-25} {2,-25}", BusType, Departure, Arrival);
            Console.WriteLine("");

            Console.WriteLine("{0,-25} {1,-25} {2,-25}", "Model", "Date", "Date");
            Console.WriteLine("{0,-25} {1,-25} {2,-25}", BusModel, DepartureYear + "-" + DepartureMonth + "-" + DepartureDate, ArrivalYear + "-" + ArrivalMonth + "-" + ArrivalDate);
            Console.WriteLine("");

            Console.WriteLine("{0,-25} {1,-25} {2,-25}", "Bus No", "Time", "Time");
            Console.WriteLine("{0,-25} {1,-25} {2,-25}", BusNo, DepartureTime, ArrivalTime);
            Console.WriteLine("");

            //Console.WriteLine("Available seats : " + AvailableSeats);
            Console.WriteLine("*********************************************************************");
            Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
        }

        public void GetRoute()
        {
            BusRoute.PrintRoute();
        }

        public bool JourneyCheck(string from, string to)
        {
            Junction current = BusRoute.Departure;
            bool IsTrueFrom = false;
            bool IsTrueTo = false;

            while(current != null)
            {
                if (current.key == from)
                {
                    IsTrueFrom = true;
                }

                if(current.key == to)
                {
                    if(IsTrueFrom == true)
                    {
                        IsTrueTo = true;
                    }
                }
                current = current.next;
            }
            return IsTrueTo;
        }

        public void PrintSeats()
        {
            for(int i = 0; i < NoOfSeats; i++)
            {
                if(i % 5 == 0)
                {
                    Console.WriteLine(" ");
                }

                if(i < 10)
                {
                    if (seat[i].Status == "Available")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("|0" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else if (seat[i].Status == "Fully-Booked")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("|0" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else if (seat[i].Status == "Partially-Booked")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("|0" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("|0" + i + "|");
                        Console.ResetColor();
                    }
                }

                else
                {
                    if (seat[i].Status == "Available")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("|" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else if (seat[i].Status == "Fully-Booked")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("|" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else if (seat[i].Status == "Partially-Booked")
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write("|" + i + "|");
                        Console.Write("         ");
                        Console.ResetColor();
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("|" + i + "|");
                        Console.ResetColor();
                    }
                }
            }
        }

        public void CalcSeatStatus(string from, string to)
        {
            for (int i = 0; i < NoOfSeats; i++)
            {
                //fully booked status
                if (seat[i].NonBookedRoute.NoOfJunctions == 0)
                {
                    seat[i].Status = "Fully-Booked";
                }

                //Available status
                Junction current = seat[i].NonBookedRoute.Departure;
                while (current != null)
                {
                    if (from == current.key)
                    {
                        while (current != null)
                        {
                            if (to == current.key)
                            {
                                seat[i].Status = "Available";
                            }
                            current = current.next;
                        }
                    }
                    else
                    {
                        current = current.next;
                    }
                }


                //partially booked status
            }
        }



        public void BookSeat(string ID, int index, string from, string to)
        {
            if (seat[index].Status == "Available")
            {
                if((from == Departure) && (to == Arrival))
                {
                    seat[index].BookedRoute = new Route(seat[index].NonBookedRoute);
                    seat[index].NonBookedRoute.DeleteAllRoutes();
                    seat[index].Status = "Fully-Booked";
                    seat[index].AddToUserRoute(ID, seat[index].BookedRoute);
                }

                else if (from == Departure)
                {
                    Junction current = seat[index].NonBookedRoute.Departure;
                    //seat[index].BookedRoute = new Route();
                    while (current.key != to)
                    {
                        seat[index].BookedRoute.AddToRoute(current.key);
                        current = current.next;
                    }
                    seat[index].BookedRoute.AddToRoute(current.key);
                    //sorting
                    //seat[index].BookedRoute.BubbleSort();
                    seat[index].BookedRoute.MergeSort();

                    Route tmp = new Route(seat[index].NonBookedRoute);
                    seat[index].NonBookedRoute.DeleteAllRoutes();
                    current = tmp.Departure;
                    while (current != null)
                    {
                        if(current.key == to)
                        {
                            while (current != null)
                            {
                                seat[index].NonBookedRoute.AddToRoute(current.key);
                                
                                current = current.next;
                            }
                        }
                        if(current != null)
                        {
                            current = current.next;
                        }
                    }
                    if (seat[index].NonBookedRoute.NoOfJunctions == 1)
                    {
                        seat[index].NonBookedRoute.DeleteAllRoutes();
                    }
                        
                    seat[index].Status = "Partially-Booked";
                    seat[index].AddToUserRoute(ID, seat[index].BookedRoute);

                  
                    
                }

                else if (to == Arrival)
                {
                    Junction current = seat[index].NonBookedRoute.Departure;
                    //seat[index].BookedRoute = new Route();
                    while (current != null)
                    {
                        if (current.key == from)
                        {
                            while (current != null)
                            {
                                seat[index].BookedRoute.AddToRoute(current.key);

                                current = current.next;
                            }
                        }
                        else if (current != null)
                        {
                            current = current.next;
                        }
                    }
                    //sorting
                    //seat[index].BookedRoute.BubbleSort();
                    seat[index].BookedRoute.MergeSort();

                    Route tmp = new Route(seat[index].NonBookedRoute);
                    seat[index].NonBookedRoute.DeleteAllRoutes();
                    current = tmp.Departure;

                    while (current.key != from)
                    {
                        seat[index].NonBookedRoute.AddToRoute(current.key);
                        current = current.next;
                    }
                    seat[index].NonBookedRoute.AddToRoute(current.key);

                    if (seat[index].NonBookedRoute.NoOfJunctions == 1)
                    {
                        seat[index].NonBookedRoute.DeleteAllRoutes();
                    }

                    seat[index].Status = "Partially-Booked";
                    seat[index].AddToUserRoute(ID, seat[index].BookedRoute);
                }
                else
                {
                    Junction current = seat[index].NonBookedRoute.Departure;
                    //seat[index].BookedRoute = new Route();
                    while (current != null)
                    {
                        if(current.key == from)
                        {
                            while(current.key != to)
                            {
                                seat[index].BookedRoute.AddToRoute(current.key);
                                current = current.next;
                            }
                            seat[index].BookedRoute.AddToRoute(current.key);
                        }

                        if (current != null)
                        {
                            current = current.next;
                        }
                    }
                    //sorting
                    //seat[index].BookedRoute.BubbleSort();
                    seat[index].BookedRoute.MergeSort();

                    Route tmp = new Route(seat[index].NonBookedRoute);
                    seat[index].NonBookedRoute.DeleteAllRoutes();
                    current = tmp.Departure;

                    while (current.key != from)
                    {
                        seat[index].NonBookedRoute.AddToRoute(current.key);
                        current = current.next;
                    }
                    seat[index].NonBookedRoute.AddToRoute(current.key);

                    while (current.key != to)
                    {
                        current = current.next;
                    }

                    if (current.key == to)
                    {
                        while (current != null)
                        {
                            seat[index].NonBookedRoute.AddToRoute(current.key);
                            current = current.next;
                        }
                    }

                    if (seat[index].NonBookedRoute.NoOfJunctions == 1)
                    {
                        seat[index].NonBookedRoute.DeleteAllRoutes();
                    }

                    seat[index].Status = "Partially-Booked";
                    seat[index].AddToUserRoute(ID, seat[index].BookedRoute);
                }
            }

            //if (seat[index].Status == "Partially - Booked")
            //{
            //    Console.WriteLine("xxx");
            //}

            else
            {
                Console.WriteLine("Error ! - Seat is already booked.");
            }


        }



    }
}