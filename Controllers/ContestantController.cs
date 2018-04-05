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
    public class ContestantController : Controller
    {
        private SqlCommand cmd;

        SqlConnection jetsondb = new SqlConnection("Server=tcp:thejetsons.database.windows.net,1433;Initial Catalog=JetsonDB;Persist Security Info=False;User ID=Jetson@thejetsons;Password=Andersbank1-.-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public ContestantController()
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
        public IEnumerable<Contestant> Get()
        {
            List<Contestant> contestants = new List<Contestant>();
            try
            {
                cmd = new SqlCommand("SELECT * FROM Contestant", jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    Contestant c = new Contestant(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    );
                    contestants.Add(c);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();
            return contestants;
        }

        [HttpGet("{id}")]
        public Contestant Get(int id)
        {
            Contestant contestant = null;

            try
            {
                cmd = new SqlCommand("SELECT * FROM Contestant WHERE ContestantID = " + id, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    contestant = new Contestant(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                    );
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return contestant;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contestant contestant)
        {
            try
            {
                string fname = contestant.FirstName;
                string lname = contestant.LastName;
                cmd = new SqlCommand(
                    "insert into Contestant values " +
                    "(" +
                    "'" + fname + "'," +    
                    "'" + lname + "');"
                    ,jetsondb);

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
                    "delete from Contestant " +
                    "where ContestantID=" + id, jetsondb
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
