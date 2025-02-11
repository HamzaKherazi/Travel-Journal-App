using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelJournalDataAccessLayer;

namespace TravelJournalBusinessLayer
{
    public class clsPhoto
    {
        public int PhotoId { get; set; }
        public int TripID { get; set; }
        public string PhotoPath { get; set; }

        public enum enMode { AddNew, Update };

        public enMode Mode;

        public clsPhoto()
        {
            this.PhotoId = -1;
            this.TripID = -1;
            this.PhotoPath = null;

            this.Mode = enMode.AddNew;
        }

        public clsPhoto(int photoId, int tripID, string photoPath)
        {
            this.PhotoId = photoId;
            this.TripID = tripID;
            this.PhotoPath = photoPath;

            this.Mode = enMode.Update;
        }

        static public clsPhoto Find(int PhotoID)
        {
            int tripID = -1;
            string photoPath = null;

            if(PhotoData.Find(PhotoID, ref tripID, ref photoPath))
            {
                return new clsPhoto(PhotoID, tripID, photoPath);
            }

            else
            {
                return null;
            }
        }

        static public clsPhoto Find(string PhotoPath)
        {
            int tripID = -1;
            int PhotoID = -1;

            if (PhotoData.Find(PhotoPath,ref PhotoID, ref tripID))
            {
                return new clsPhoto(PhotoID, tripID, PhotoPath);
            }

            else
            {
                return null;
            }
        }

        static public DataTable GetPhotos(int TripID)
        {
            return PhotoData.GetPhotos(TripID);
        }

        private bool _AddNewPhoto()
        {
            return PhotoData.AddNewPhoto(this.TripID, this.PhotoPath) != -1;
        }

        private bool _UpdatePhoto()
        {
            return PhotoData.UpdatePhoto(this.PhotoId, this.TripID, this.PhotoPath);
        }

        public bool Save()
        {
            switch(this.Mode)
            {
                case enMode.AddNew:
                    if(_AddNewPhoto())
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePhoto();
            }

            return false;
        }

        static public bool isExist(int PhotoID)
        {
            return PhotoData.isExist(PhotoID);
        }

        static public bool DeletePhoto(int PhotoID)
        {
            if (isExist(PhotoID))
            {
                return PhotoData.DeletePhoto(PhotoID);

            }

            else
            {
                return false;
            }
        }


    }
}
