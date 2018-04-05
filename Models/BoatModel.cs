using System;

namespace JetsonAPI.Models
{
    public class Boat{

        public Boat(int id, int nr, string name)
        {
            this.BoatID = id;
            this.BoatNumber = nr;
            this.BoatName = name;
        }

        public int BoatID { get; set; }

        public int BoatNumber { get; set;}

        public string BoatName { get; set; }


    }

}