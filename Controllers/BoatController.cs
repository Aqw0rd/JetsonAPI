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
    public class BoatController : Controller
    {
        private SqlCommand cmd;

        SqlConnection jetsondb = new SqlConnection("Server=tcp:thejetsons.database.windows.net,1433;Initial Catalog=JetsonDB;Persist Security Info=False;User ID=Jetson@thejetsons;Password=Andersbank1-.-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public BoatController()
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

        [HttpGet]
        public IEnumerable<Boat> Get()
        {
            List<Boat> boats = new List<Boat>();
            try
            {
                cmd = new SqlCommand("SELECT * FROM Boat", jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Boat b = new Boat(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    );
                    boats.Add(b);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();
            return boats;
        }

        [HttpGet("{id}")]
        public Boat Get(int id)
        {
            Boat boat = null;

            try
            {
                cmd = new SqlCommand("SELECT * FROM Boat WHERE BoatID = " + id, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    boat = new Boat(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    );
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return boat;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Boat boat)
        {
            try
            {
                string name = boat.BoatName;
                string nr = boat.BoatNumber;
                string skipperOne = boat.BoatSkipperOne;
                string skipperTwo = boat.BoatSkipperTwo;
                cmd = new SqlCommand(
                    "insert into Boat values " +
                    "(" +
                    "'" + nr + "'," +
                    "'" + name + "'," +
                    "'" + skipperOne + "'," +
                    "'" + skipperTwo + "');"
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                cmd = new SqlCommand(
                    "delete from Boat " +
                    "where BoatID=" + id, jetsondb
                    );
                cmd.ExecuteNonQuery();
                jetsondb.Close();
                return Ok();
            }
            catch
            {
                jetsondb.Close();
                return NotFound();
            }
        }
    }
}
