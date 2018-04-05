using System;

namespace JetsonAPI.Models
{
    public class Contestant{

        public Contestant(int id, string fname, string lname)
        {
            this.ContestantID = id;
            this.FirstName = fname;
            this.LastName = lname;
        }

        public int ContestantID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

}

