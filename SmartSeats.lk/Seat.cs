using System;
namespace SmartSeats.lk
{
	public class Seat
	{
        public Route BookedRoute;
        public Route NonBookedRoute;
		public string Status;

        public int UserCount;
        public int NoOfUsers;
        public UserRoute[] userRoute;

        public Seat()
        {
            Status = "Available";
            UserCount = 0;
            NoOfUsers = 10;
            userRoute = new UserRoute[NoOfUsers];
        }

        public void AddToUserRoute(string ID, Route route)
        {
            NoOfUsers++;
            userRoute[UserCount] = new UserRoute();
            userRoute[UserCount].UserID = ID;
            userRoute[UserCount].Uroute = route;
            UserCount++;

        }

        public void PrintUserRoute()
        {
            for (int i = 0; i < UserCount ; i++)
            {
                Console.Write("User NIC : " + userRoute[i].UserID + " : ");
                userRoute[i].Uroute.PrintRoute();
                Console.WriteLine();
            }

        }


    }
}

