using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JetsonAPI.Models;
using System.Data.SqlClient;

namespace JetsonAPI.Controllers
{
    [Route("api/[controller]")]
    public class RaceController : Controller
    {
        private SqlCommand cmd;

        SqlConnection jetsondb = new SqlConnection("Server=tcp:thejetsons.database.windows.net,1433;Initial Catalog=JetsonDB;Persist Security Info=False;User ID=Jetson@thejetsons;Password=Andersbank1-.-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public RaceController()
        {
            try
            {
                jetsondb.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]Race race)
        {
            try
            {
                string name = race.RaceName;
                DateTime date = race.RaceDate;
                cmd = new SqlCommand(
                    "insert into Race values " +
                    "(" +
                    "'" + name + "'," +
                    "'" + date + "');"
                    , jetsondb);

                cmd.ExecuteNonQuery();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                jetsondb.Close();
                return NotFound();
            }
        }

        [HttpGet]
        public IEnumerable<Race> Get()
        {
            List<Race> races = new List<Race>();
            try
            {
                cmd = new SqlCommand("SELECT * FROM Race", jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Race r = new Race(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2)
                    );
                    races.Add(r);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();
            return races;
        }

        [HttpGet("{id}")]
        public Race Get(int id)
        {
            Race race = null;

            try
            {
                cmd = new SqlCommand("SELECT * FROM Race WHERE RaceID = " + id, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    race = new Race(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetDateTime(2)
                    );
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return race;
        }
    }
}
