﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JetsonAPI.Models;
using System.Data.SqlClient;

namespace JetsonAPI.Controllers
{
    [Route("api/[controller]")]
    public class RaceTimeController : Controller
    {
        private SqlCommand cmd;

        SqlConnection jetsondb = new SqlConnection("Server=tcp:thejetsons.database.windows.net,1433;Initial Catalog=JetsonDB;Persist Security Info=False;User ID=Jetson@thejetsons;Password=Andersbank1-.-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public RaceTimeController()
        {
            try
            {
                jetsondb.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        [HttpGet]
        public IEnumerable<RaceTime> Get()
        {
            List<RaceTime> racetimes = new List<RaceTime>();
            try
            {
                cmd = new SqlCommand("SELECT * FROM RaceTime", jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RaceTime rt = new RaceTime(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2)
                    );
                    racetimes.Add(rt);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();
            return racetimes;
        }

        [HttpGet("{id}")]
        public List<RaceTime> Get(int id)
        {
            List<RaceTime> racetimes = new List<RaceTime>();

            try
            {
                cmd = new SqlCommand("SELECT * FROM RaceTime WHERE RaceId = " + id, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RaceTime rt = new RaceTime(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2)
                    );
                    racetimes.Add(rt);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return racetimes;
        }

        /*[HttpGet("/{boatNumber}")]
        public List<RaceTime> Get( boatNumber)
        {
            List<RaceTime> racetimes = new List<RaceTime>();

            try
            {
                cmd = new SqlCommand("SELECT * FROM RaceTime WHERE Boatnumber = " + boatNumber, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RaceTime rt = new RaceTime(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2)
                    );
                    racetimes.Add(rt);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return racetimes;
        }*/

        public IActionResult Index()
        {
            return View();
        }
    }
}