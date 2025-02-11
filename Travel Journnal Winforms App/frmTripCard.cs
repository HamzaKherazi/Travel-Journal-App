using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelJournalBusinessLayer;

namespace Travel_Journnal_Winforms_App
{
    public partial class frmTripCard : Form
    {
        clsTrip _Trip;
        static int _TripID;
        public frmTripCard(int TripID)
        {
            InitializeComponent();
            _TripID = TripID;
        }

        static DataTable dtPhotos = new DataTable();
        static string[] PhotoPaths;
        private int i = 0;

        private void frmTripCard_Load(object sender, EventArgs e)
        {
            dtPhotos = clsPhoto.GetPhotos(_TripID);

            if (dtPhotos.Rows.Count == 0)
            {
                this.Size = new System.Drawing.Size(527, 647); //1131, 636
                button1.Location = new System.Drawing.Point(487, 12);
                lblPhotos.Visible = false;

            }
            
            //else
            //{
            //    this.Size = new System.Drawing.Size(1131, 636);
            //}

            _Trip = clsTrip.Find(_TripID);
            lblTripName.Text = _Trip.TripName;
            lblStartDate.Text = Convert.ToDateTime(_Trip.StartDate).ToString("D");
            lblEndDate.Text = Convert.ToDateTime(_Trip.EndDate).ToString("D");
            lblLocation.Text = _Trip.Location;
            lblCategory.Text = clsCategory.Find(_Trip.CategoryID).CategoryName;
            lblRating.Text = _Trip.Rating;
            lblNotes.Text = _Trip.Notes;

            PhotoPaths = new string[dtPhotos.Rows.Count];

            foreach (DataRow row in dtPhotos.Rows)
            {
                PhotoPaths[i] = row["PhotoPath"].ToString();
                i++;
            }

            if (dtPhotos.Rows.Count == 0)
            {
                btnPrev.Visible = false;
                btnNext.Visible = false;
            }

            else
            {
                pbPhotos.Image = Image.FromFile(PhotoPaths[0]);

                if (dtPhotos.Rows.Count > 1)
                {
                    btnNext.Visible = true;
                    i = 0;
                }

            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (i < dtPhotos.Rows.Count-1)
            {
                i++;
                pbPhotos.Image = Image.FromFile(PhotoPaths[i]);

            }

            if (i == dtPhotos.Rows.Count - 1)
            {
                btnNext.Visible = false;
            }

            if (i > 0)
            {
                btnPrev.Visible = true;
            }
            

        }


        private void btnPrev_Click(object sender, EventArgs e)
        {
            btnNext.Visible = true;

            if(i>0)
            {
                i--;
                pbPhotos.Image = Image.FromFile(PhotoPaths[i]);

            }
            if(i == 0)
            {
                btnPrev.Visible =false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
