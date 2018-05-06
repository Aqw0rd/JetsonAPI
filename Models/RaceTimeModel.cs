using System;

namespace JetsonAPI.Models
{
    public class RaceTime{

        public RaceTime(int id, string nr, string time)
        {
            this.RaceId = id;
            this.BoatNumber = nr;
            this.Time = time;
        }

        public int RaceId { get; set; }

        public string BoatNumber { get; set; }

        public string Time { get; set; }
    }

}