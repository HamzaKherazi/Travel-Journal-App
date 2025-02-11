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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Travel_Journnal_Winforms_App
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblTitle.SendToBack();
            pnlFormLoader.SendToBack();
        }

        private void _ResetButtonBackgroundColor()
        {
            if (btnMyTrips.BackColor == Color.Beige)
            {
                btnMyTrips.BackColor = Color.FromArgb(243, 169, 80);
            }

            if (btnAddNewTrip.BackColor == Color.Beige)
            {
                btnAddNewTrip.BackColor = Color.FromArgb(243, 169, 80);
            }

            if (btnFavorites.BackColor == Color.Beige)
            {
                btnFavorites.BackColor = Color.FromArgb(243, 169, 80);
            }

            if (btnSettings.BackColor == Color.Beige)
            {
                btnSettings.BackColor = Color.FromArgb(243, 169, 80);
            }

            if (btnAboutUs.BackColor == Color.Beige)
            {
                btnAboutUs.BackColor = Color.FromArgb(243, 169, 80);
            }

        }

        public void LoadMyTripsForm()
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\dev\\PROJECTS\\Travel Journal App\\pics\\background.jpg");

            pnlNav.Height = btnMyTrips.Height;
            pnlNav.Top = btnMyTrips.Top;
            pnlNav.Left = btnMyTrips.Left;
            _ResetButtonBackgroundColor();
            btnMyTrips.BackColor = Color.Beige;


            lblTitle.Text = "My Trips";
            pnlFormLoader.Controls.Clear();
            frmMyTrips frm = new frmMyTrips() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            pnlFormLoader.Size = frm.Size;
            frm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(frm);
            frm.Show();
            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();
        }

        private void btnMyTrips_Click(object sender, EventArgs e)
        {
            LoadMyTripsForm();
        }

        private void btnAddNewTrip_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\dev\\PROJECTS\\Travel Journal App\\pics\\background.jpg");

            pnlNav.Height = btnAddNewTrip.Height;
            pnlNav.Top = btnAddNewTrip.Top;
            pnlNav.Left = btnAddNewTrip.Left;
            _ResetButtonBackgroundColor();
            btnAddNewTrip.BackColor = Color.Beige;

            lblTitle.Text = "Add New Trip";
            pnlFormLoader.Controls.Clear();
            frmAddNewTrip frm = new frmAddNewTrip() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            pnlFormLoader.Size = frm.Size;

            frm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(frm);
            frm.Show();

            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();


        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\dev\\PROJECTS\\Travel Journal App\\pics\\background.jpg");

            pnlNav.Height = btnFavorites.Height;
            pnlNav.Top = btnFavorites.Top;
            pnlNav.Left = btnFavorites.Left;
            _ResetButtonBackgroundColor();
            btnFavorites.BackColor = Color.Beige;

            lblTitle.Text = "Favorites";
            pnlFormLoader.Controls.Clear();
            frmFavorites frm = new frmFavorites() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            pnlFormLoader.Size = frm.Size;

            frm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(frm);
            frm.Show();

            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\dev\\PROJECTS\\Travel Journal App\\pics\\background.jpg");

            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            pnlNav.Left = btnSettings.Left;
            _ResetButtonBackgroundColor();
            btnSettings.BackColor = Color.Beige;

            //Settings

            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();
        }

        private void btnAboutUs_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("C:\\Users\\Admin\\Desktop\\dev\\PROJECTS\\Travel Journal App\\pics\\background.jpg");

            pnlNav.Height = btnAboutUs.Height;
            pnlNav.Top = btnAboutUs.Top;
            pnlNav.Left = btnAboutUs.Left;
            _ResetButtonBackgroundColor();
            btnAboutUs.BackColor = Color.Beige;

            lblTitle.Text = "About Us";
            pnlFormLoader.Controls.Clear();
            frmAboutUs frm = new frmAboutUs() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            pnlFormLoader.Size = frm.Size;

            frm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(frm);
            frm.Show();

            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewTrip_FormClosed(object sender, FormClosedEventArgs e)
        {
            lblTitle.Text = "My Trips";
            pnlFormLoader.Controls.Clear();
            frmMyTrips frm = new frmMyTrips() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            pnlFormLoader.Size = frm.Size;
            frm.FormBorderStyle = FormBorderStyle.None;
            pnlFormLoader.Controls.Add(frm);
            frm.Show();
            lblTitle.BringToFront();
            pnlFormLoader.BringToFront();
        }

    }
}
