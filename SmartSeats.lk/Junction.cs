using System;
namespace SmartSeats.lk
{
    public class Junction
    {
        public string key { get; set; }
        public Junction? next { get; set; }

        public Junction(string key)
        {
            this.key = key;
            next = null;
        }
    }
}

