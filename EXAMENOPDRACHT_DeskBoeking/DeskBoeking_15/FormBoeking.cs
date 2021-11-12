using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.Entity; // for DbFunctions
using System.IO;

namespace DeskBoeking
{
    public partial class FormBoeking : Form
    {

        private UserNaamId _usernaamid;
        public UserNaamId userNaamId
        {
            set { _usernaamid = value; }
            get { return _usernaamid; }
        }

        string SelectedDate;
        DateTime SelectedDatum;

        void UpdateEvent(BoekingEventArgs e)
        {
            System.Console.WriteLine(e.Message + " (Id=" + e.userNaamId.UserId + ", Usernaam=" + e.userNaamId.Naam + ")"); 
        }
        

        public FormBoeking()
        {
            InitializeComponent();
            Shown += FormBoeking_Shown; // for https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown

            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_SelectedIndexChanged);
            dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

            FormMain.formUser.UpdateBoeking += new BoekingEventHandler(UpdateEvent);
        }


        class DeskDatum
        {
            public int bid { get; set; }
            public string naam { get; set; }
            public DateTime datum { get; set; }
        }


        // https://stackoverflow.com/questions/479329/how-to-bind-a-liststring-to-a-datagridview-control
        public class StringValue
        {
            public StringValue(string s)
            {
                _value = s;
            }
            public string Value { get { return _value; } set { _value = value; } }
            string _value;
        }

  
        private void FormBoeking_UpdateImage()
        {
            var ctx = new DeskBoekingEntities();

            // update van het image
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            string selectedDesk;
            List<string> selectedDesks = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                selectedDesk = row.Cells[0].Value.ToString(); // ingeval meerdere selects
                selectedDesks.Add(selectedDesk.ToString());
            }

            selectedDesk = selectedDesks.FirstOrDefault();

            if (selectedDesk != null)
            {
                byte[] imageData = (from d in ctx.Desks
                                    where d.Naam == selectedDesk
                                    select d.Grondplan).First();
                if (imageData != null)
                {
                    pictureBox1.Image  = (Bitmap)((new ImageConverter()).ConvertFrom(imageData));
                    this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    pictureBox1.Image = null; // in geval geen picture in db
                }
            }
            else
            {
                pictureBox1.Image = null;
            }
        }

        private void FormBoeking_UpdateFaciliteiten()
        {
            var ctx = new DeskBoekingEntities();

            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
            string selectedDesk;
            List<string> selectedDesks = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                selectedDesk = row.Cells[0].Value.ToString(); // ingeval meerdere selects
                selectedDesks.Add(selectedDesk.ToString());
            }

            selectedDesk = selectedDesks.FirstOrDefault();

            if (selectedDesk != null)
            {
                string faciliteiten = (from d in ctx.Desks
                                       where d.Naam == selectedDesk
                                       select d.Faciliteiten).First();

                textBox1.Text = faciliteiten;
            }
            else
            {
                textBox1.Text = null;
            }
        }


        private void FormBoeking_UpdateControlsTab1()
        {
            if (userNaamId != null)
            {
                label4.Text = "U bent gebruiker '" + userNaamId.Naam + "'";
            }
            
            var ctx = new DeskBoekingEntities();

            var query = (from d in ctx.Desks
                         where !ctx.Boekingens.Any(x => x.Datum.Year == SelectedDatum.Year // https://stackoverflow.com/questions/11597373/the-specified-type-member-date-is-not-supported-in-linq-to-entities-exception
                            && x.Datum.Month == SelectedDatum.Month
                            && x.Datum.Day == SelectedDatum.Day
                            && x.DeskId == d.DeskId)
                         select d.Naam).ToList();

            // https://stackoverflow.com/questions/253843/refresh-datagridview-when-updating-data-source
            this.dataGridView1.DataSource = null;

            // https://stackoverflow.com/questions/479329/how-to-bind-a-liststring-to-a-datagridview-control
            List<StringValue> freeDesks = new List<StringValue>();
            foreach (var i in query)
            {
                freeDesks.Add(new StringValue(i));
            }

            this.dataGridView1.DataSource = freeDesks;

            // update van het image & faciliteiten controls
            FormBoeking_UpdateImage();
            FormBoeking_UpdateFaciliteiten();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FormBoeking_UpdateImage();
            FormBoeking_UpdateFaciliteiten();
        }


        private void FormBoeking_UpdateControlsTab2()
        {
            if (userNaamId != null)
                {
                label2.Text = "U bent gebruiker '" + userNaamId.Naam + "'";
                var ctx = new DeskBoekingEntities();

                var query = (from b in ctx.Boekingens
                             join d in ctx.Desks on b.DeskId equals d.DeskId
                             where b.UserId == userNaamId.UserId
                             where (DbFunctions.TruncateTime(b.Datum) >= DbFunctions.TruncateTime(DateTime.Now)) // toon alleen toekomstige boekingen
                             select new { Naam = d.Naam, Datum = b.Datum, uid = b.UserId, Bid = b.BoekingId }).ToList();

                // https://stackoverflow.com/questions/612689/a-generic-list-of-anonymous-class
                // https://blog.matrixpost.net/using-list-tuples-in-c/
                BindingList<DeskDatum> myBookings = new BindingList<DeskDatum>();

                foreach (var i in query)
                {
                    myBookings.Add(new DeskDatum { naam = i.Naam, datum = i.Datum, bid = i.Bid });
                }

                this.dataGridView2.DataSource = myBookings;
                this.dataGridView2.Columns[0].Visible = false; // verberg eerste kolom (=id)
            }
        }


        // https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown
        private void FormBoeking_Shown(object sender, EventArgs e)
        {
            // https://stackoverflow.com/questions/3429128/how-to-get-the-selected-date-of-a-monthcalendar-control-in-c-sharp
            SelectedDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            SelectedDatum = Convert.ToDateTime(SelectedDate);

            FormBoeking_UpdateControlsTab1();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SelectedDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            SelectedDatum = Convert.ToDateTime(SelectedDate);
 
            if (userNaamId != null)
                {
                    var ctx = new DeskBoekingEntities();

                // https://stackoverflow.com/questions/2581265/for-a-datagridview-how-do-i-get-the-values-from-each-row/45633603
                List<string> selectedDesks = new List<string>();  // list in geval van meerdere selects

                DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;
                string selectedDesk;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    selectedDesk = row.Cells[0].Value.ToString(); // ingeval meerdere selects
                    selectedDesks.Add(selectedDesk.ToString());
                }

                selectedDesk = selectedDesks.FirstOrDefault();

                if (selectedDesk != null)
                {
                    var deskId = (from d in ctx.Desks
                                  where d.Naam == selectedDesk
                                  select d.DeskId).First();

                    var userId = (from u in ctx.Users
                                  where u.Naam == userNaamId.Naam
                                  select u.UserId).First();

                    var boeking = new Boekingen { UserId = userId, DeskId = deskId, Datum = SelectedDatum };
                    ctx.Boekingens.Add(boeking);
                    ctx.SaveChanges();
                }
                FormBoeking_UpdateControlsTab1();
            }
        }


        private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {

            SelectedDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            SelectedDatum = Convert.ToDateTime(SelectedDate);
            FormBoeking_UpdateControlsTab1();

        }

        private void tabControl1_SelectedIndexChanged(object sender, TabControlEventArgs e)
        {

            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        FormBoeking_UpdateControlsTab1();
                        break;
                    }
                case 1:
                    {
                        FormBoeking_UpdateControlsTab2();
                        break;
                    }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            List<int> listToDelete = new List<int>();
            var ctx = new DeskBoekingEntities();

            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                int id = (int)row.Cells[0].Value;
                listToDelete.Add(id);
                var query = (from b in ctx.Boekingens
                             where b.BoekingId == id
                             select b).First();
                ctx.Boekingens.Remove(query);
                ctx.SaveChanges();
            }

            FormBoeking_UpdateControlsTab2();
        }
    }

}
