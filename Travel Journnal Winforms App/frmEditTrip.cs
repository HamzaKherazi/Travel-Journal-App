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
    public partial class frmEditTrip : Form
    {
        private int _TripID;
        private clsTrip _Trip;
        public frmEditTrip(int tripID)
        {
            InitializeComponent();
            _TripID = tripID;
        }

        static DataTable dtPhotos = new DataTable();
        static string[] PhotoPaths;
        private int i = 0;

        private void frmEditTrip_Load(object sender, EventArgs e)
        {
            _Trip = clsTrip.Find(_TripID);

            tbTripName.Text = _Trip.TripName;
            dtpStartDate.Value = _Trip.StartDate;
            dtpEndDate.Value = _Trip.EndDate;
            tbLocation.Text = _Trip.Location;

            DataTable dtCategories = clsCategory.CategoriesList();

            foreach (DataRow row in dtCategories.Rows)
            {
                cbCategory.Items.Add(row["CategoryName"].ToString());
            }

            cbCategory.SelectedItem = clsCategory.Find(_Trip.CategoryID).CategoryName;

            tbRating.Text = _Trip.Rating;
            tbNotes.Text = _Trip.Notes;

            LoadPhotos();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string[] ImagePaths;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Select an Image File";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImagePaths = openFileDialog1.FileNames;

                if (ImagePaths.Length > 0)
                {
                    for (int i = 0; i < ImagePaths.Length; i++)
                    {
                        _Photo = new clsPhoto();
                        _Photo.TripID = _Trip.TripID;
                        _Photo.PhotoPath = ImagePaths[i];
                        _Photo.Save();
                    }
                }

                LoadPhotos();

            }

            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (i < dtPhotos.Rows.Count - 1)
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

            if (i > 0)
            {
                i--;
                pbPhotos.Image = Image.FromFile(PhotoPaths[i]);

            }
            if (i == 0)
            {
                btnPrev.Visible = false;
            }

        }

        private clsPhoto _Photo;

        private void LoadPhotos()
        {
            dtPhotos = clsPhoto.GetPhotos(_TripID);
            PhotoPaths = new string[dtPhotos.Rows.Count];

            i = 0;

            foreach (DataRow row in dtPhotos.Rows)
            {
                PhotoPaths[i] = row["PhotoPath"].ToString();
                i++;
            }

            btnPrev.Visible = false;

            if (dtPhotos.Rows.Count == 0)
            {
                lblDeletePhoto.Visible = false;
                pbPhotos.Visible = false;
                btnPrev.Visible = false;
                btnNext.Visible = false;
            }

            else
            {
                lblDeletePhoto.Visible = true;
                pbPhotos.Visible = true;
                pbPhotos.Image = Image.FromFile(PhotoPaths[0]);
                btnNext.Visible = false;

                if (dtPhotos.Rows.Count > 1)
                {
                    btnNext.Visible = true;
                    i = 0;
                }

            }

            i = 0;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Trip.TripName = tbTripName.Text;
            _Trip.StartDate = dtpStartDate.Value;
            _Trip.EndDate = dtpEndDate.Value;
            _Trip.Location = tbLocation.Text;
            _Trip.CategoryID = clsCategory.Find(cbCategory.Text).CategoryID;
            _Trip.Rating = tbRating.Text;
            _Trip.Notes = tbNotes.Text;

            string message = "";

            if(_Trip.Save(ref message))
            {
                if(MessageBox.Show("Trip edited successfully!") == DialogResult.OK)
                {
                    this.Close();

                    // Call a method in frmMain to load frmMyTrips
                    frmMain frm = (frmMain)Application.OpenForms["frmMain"];
                    if (frm != null)
                    {
                        frm.LoadMyTripsForm(); // This method will load frmMyTrips in pnlFormLoader
                    }
                }
            }

        }

        private void lblDeletePhoto_Click(object sender, EventArgs e)
        {
            clsPhoto.DeletePhoto(clsPhoto.Find(PhotoPaths[i]).PhotoId);
            LoadPhotos();
        }
    }
}
