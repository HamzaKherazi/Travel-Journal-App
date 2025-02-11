using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TravelJournalDataAccessLayer;

namespace TravelJournalBusinessLayer
{
    public class clsTrip
    {
        public int TripID { get; set; }
        public string TripName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int CategoryID { get; set; }
        public string Rating { get; set; }
        public string Notes { get; set; }
        public bool IsFavorite { get; set; }
        public enum enMode { AddNew, Update };
        public enMode Mode { get; set; }


        public clsTrip()
        {
            this.TripID = -1;
            this.TripName = "";
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.Location = "";
            this.CategoryID = -1;
            this.Rating = "";
            this.Notes = "";
            this.IsFavorite = false;

            this.Mode = enMode.AddNew;

        }

        public clsTrip(int tripID, string tripName, DateTime startDate, DateTime endDate, string location, int categoryID, string rating, string notes, bool isFavorite)
        {
            this.TripID = tripID;
            this.TripName = tripName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Location = location;
            this.CategoryID = categoryID;
            this.Rating = rating;
            this.Notes = notes;
            this.IsFavorite = isFavorite;

            this.Mode = enMode.Update;

        }

        static public clsTrip Find(int TripID)
        {
            string tripName = "", location = "", rating = "", notes = "";
            DateTime startDate = DateTime.Now, endDate = DateTime.Now;
            int categoryID = -1;
            bool isFavorite = false;

            if (TripData.GetTripByID(TripID, ref tripName, ref startDate, ref endDate, ref location, ref categoryID, ref rating, ref notes, ref isFavorite))
            {
                return new clsTrip(TripID, tripName, startDate, endDate, location, categoryID, rating, notes, isFavorite);
            }

            else
            {
                return null;
            }

        }

        static public clsTrip Find(string TripName)
        {
            int TripID = -1; 
            string Location = "", Rating = "", Notes = "";
            DateTime StartDate = DateTime.Now, EndDate = DateTime.Now;
            int CategoryID = -1;
            bool isFavorite = false;

            if (TripData.GetTripByName(TripName, ref TripID, ref StartDate, ref EndDate, ref Location, ref CategoryID, ref Rating, ref Notes, ref isFavorite))
            {
                return new clsTrip(TripID, TripName, StartDate, EndDate, Location, CategoryID, Rating, Notes, isFavorite);
            }

            else
            {
                return null;
            }

        }


        private bool _AddNewTrip(ref string message)
        {
            this.TripID = TripData.AddNewTrip(ref message, this.TripName, this.StartDate, this.EndDate, this.Location, this.CategoryID, this.Rating, this.Notes);
            return this.TripID != -1;
        }

        private bool _UpdateTrip()
        {
            return TripData.UpdateTrip(this.TripID, this.TripName, this.StartDate, this.EndDate, this.Location,
                                       this.CategoryID, this.Rating, this.Notes);
        }

        public bool Save(ref string message)
        {
            switch(this.Mode)
            {
                case enMode.AddNew:

                    if(_AddNewTrip(ref message))
                    {
                        this.Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;

                    }

                case enMode.Update:
                    return _UpdateTrip();
            }

            return false;
        }

        static public bool isExist(int TripID)
        {
            return TripData.isExist(TripID);
        }

        static public bool DeleteTrip(int TripID)
        {
            if(isExist(TripID))
            {
                return TripData.DeleteTrip(TripID);

            }

            else
            {
                return false;
            }
        }

        static public bool isExist(string TripName)
        {
            return TripData.isExist(TripName);
        }

        static public DataTable TripsList()
        {
            return TripData.GetAllTrips();
        }

        static public bool AddToFavorites(int TripID)
        {
            return TripData.SetFavorite(TripID, 1);
        }

        static public DataTable FavoritesTrips()
        {
            return TripData.GetFavoritesTrips();
        }

        static public bool RemoveFromFavorites(int TripID)
        {
            return TripData.SetFavorite(TripID, 0);
        }
    }





}
