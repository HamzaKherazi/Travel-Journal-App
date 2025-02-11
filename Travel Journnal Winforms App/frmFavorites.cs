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
    public partial class frmFavorites : Form
    {
        public frmFavorites()
        {
            InitializeComponent();
        }


        private void LoadFavoritesTrips()
        {
            DataTable FavoritesTrips = clsTrip.FavoritesTrips();

            Size buttonSize = new Size(650, 60); // Width x Height

            //Size deleteButtonSize = new Size(30, 50); // Width x Height of the delete button

            int verticalSpacing = 10; // Space between buttons

            // Starting position for the first button
            int yPosition = 10;
            // Loop through dtTrips and create a button for each trip
            foreach (DataRow row in FavoritesTrips.Rows)
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

                    }
                };

                pnlMyTrips.Controls.Add(btnTrip);
                //pnlMyTrips.Controls.Add(btnDelete);

                yPosition += buttonSize.Height + verticalSpacing;


            }
        }
       

        private void frmFavorites_Load(object sender, EventArgs e)
        {
            LoadFavoritesTrips();
        }

        private void removeFromFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsTrip.RemoveFromFavorites(Convert.ToInt16(contextMenuStrip1.Tag));

            pnlMyTrips.Controls.Clear();
            LoadFavoritesTrips();


        }
    }
    
}
