using System;

namespace JetsonAPI.Models
{
    public class ImageFolder{

        public ImageFolder(int id, int numberofimages, string pname, string startfinish, DateTime folderdate)
        {
            this.FolderID = id;
            this.NumberOfImages = numberofimages;
            this.PathName = pname;
            this.StartFinish = startfinish;
            this.FolderDate = folderdate;
        }

        public int FolderID { get; set; }

        public int NumberOfImages { get; set; }

        public string PathName { get; set; }

        public string StartFinish { get; set; }

        public DateTime FolderDate { get; set; }

    }

}