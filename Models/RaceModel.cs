using System;

namespace JetsonAPI.Models
{
    public class Race{

        public Race(int id, string name, DateTime date)
        {
            this.RaceID = id;
            this.RaceName = name;
            this.RaceDate = date;
        }

        public int RaceID { get; set; }

        public string RaceName { get; set; }

        public DateTime RaceDate { get; set; }



    }

}