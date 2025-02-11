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
    public partial class frmMyTrips : Form
    {
        public frmMyTrips()
        {
            InitializeComponent();
        }

        private void _LoadMyTrips()
        {
            DataTable dtTrips = clsTrip.TripsList();

            Size buttonSize = new Size(650, 60); // Width x Height

            //Size deleteButtonSize = new Size(30, 50); // Width x Height of the delete button

            int verticalSpacing = 10; // Space between buttons

            // Starting position for the first button
            int yPosition = 10;
            // Loop through dtTrips and create a button for each trip
            foreach (DataRow row in dtTrips.Rows)
            {
                // Create a button for each trip
                Button btnTrip = new Button
                {
                    Text = $"{row["TripName"]}\nStart Date: {Convert.ToDateTime(row["StartDate"]).ToString("D")}",
                    Size = buttonSize,
                    Location = new Point(10, yPosition), // Set the location of the button
                    Margin = new Padding(10), // Space around the button
                    TextAlign = ContentAlignment.MiddleLeft, // Align text to the left
                    BackColor = Color.Tan, // Button background color
                    ForeColor = Color.White, // Button text color
                    Font = new Font("Segoe UI", 14, FontStyle.Bold), // Font for the button text
                    FlatStyle = FlatStyle.Flat, // Remove button border
                    FlatAppearance = { BorderSize = 1 }, // Remove button border
                    ContextMenuStrip = contextMenuStrip1
                };

                btnTrip.Tag = clsTrip.Find(row["TripName"].ToString()).TripID;

                btnTrip.Click += (btnTripSender, btnTripE) =>
                {
                    // Handle the click event here
                    frmTripCard frm = new frmTripCard(Convert.ToInt16(btnTrip.Tag));
                    frm.ShowDialog();

                };

                btnTrip.MouseUp += (sender2, e2) =>
                {
                    if (e2.Button == MouseButtons.Right) // Check if the right button is clicked
                    {
                        // Show the context menu at the mouse position
                        contextMenuStrip1.Tag = btnTrip.Tag;
                        if (clsTrip.Find(Convert.ToInt16(btnTrip.Tag)).IsFavorite)
                        {
                            addToFavoriteToolStripMenuItem.Visible = false;
                        }

                        else
                        {
                            addToFavoriteToolStripMenuItem.Visible = true;

                        }
                    }
                };

                pnlMyTrips.Controls.Add(btnTrip);
                //pnlMyTrips.Controls.Add(btnDelete);

                yPosition += buttonSize.Height + verticalSpacing;

            }
        }

        private void frmMyTrips_Load(object sender, EventArgs e)
        {
            _LoadMyTrips();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTrip frm = new frmEditTrip(Convert.ToInt16(contextMenuStrip1.Tag));
            frm.ShowDialog();
        }

        private void _DeletePhotos(int TripID)
        {
            DataTable PhotosToDelete = clsPhoto.GetPhotos(TripID);
            foreach(DataRow row in PhotosToDelete.Rows)
            {
                clsPhoto.DeletePhoto(Convert.ToInt16(row["PhotoID"]));
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this trip?") == DialogResult.OK)
            {
                _DeletePhotos(Convert.ToInt16(contextMenuStrip1.Tag));
                clsTrip.DeleteTrip(Convert.ToInt16(contextMenuStrip1.Tag));
                pnlMyTrips.Controls.Clear();
                _LoadMyTrips();
            }
        }

        private void addToFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTrip.AddToFavorites(Convert.ToInt16(contextMenuStrip1.Tag));
        }

        
    }
}
