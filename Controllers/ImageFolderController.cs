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
    public class ImageFolderController : Controller
    {
        private SqlCommand cmd;

        SqlConnection jetsondb = new SqlConnection("Server=tcp:thejetsons.database.windows.net,1433;Initial Catalog=JetsonDB;Persist Security Info=False;User ID=Jetson@thejetsons;Password=Andersbank1-.-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public ImageFolderController()
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
        public IEnumerable<ImageFolder> Get()
        {
            List<ImageFolder> imagefolders = new List<ImageFolder>();
            try
            {
                cmd = new SqlCommand("SELECT * FROM ImageFolder", jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ImageFolder img = new ImageFolder(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDateTime(4)
                    );
                    imagefolders.Add(img);
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();
            return imagefolders;
        }

        [HttpGet("{id}")]
        public ImageFolder Get(int id)
        {
            ImageFolder imagefolder = null;

            try
            {
                cmd = new SqlCommand("SELECT * FROM ImageFolder WHERE FolderID = " + id, jetsondb);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    imagefolder = new ImageFolder(
                        reader.GetInt32(0),
                        reader.GetInt32(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetDateTime(4)
                    );
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            jetsondb.Close();

            return imagefolder;
        }
    
    }
}

