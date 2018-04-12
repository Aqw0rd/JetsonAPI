using System;

namespace JetsonAPI.Models
{
    public class Boat{

        public Boat(int id, int nr, string name, string skipperOne, string skipperTwo)
        {
            this.BoatID = id;
            this.BoatNumber = nr;
            this.BoatName = name;
            this.BoatSkipperOne = skipperOne;
            this.BoatSkipperTwo = skipperTwo;
        }

        public int BoatID { get; set; }

        public int BoatNumber { get; set;}

        public string BoatName { get; set; }
        
        public string BoatSkipperOne { get; set; }

        public string BoatSkipperTwo { get; set; }


    }

}