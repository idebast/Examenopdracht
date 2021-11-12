using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskBoeking
{
    public partial class FormBezetting : Form
    {
        string SelectedDate;
        DateTime SelectedDatum;

        public FormBezetting()
        {
            InitializeComponent();
            Shown += FormBezetting_Shown; // for https://stackoverflow.com/questions/7462748/how-to-run-code-when-form-is-shown
            monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
        }


        private void FormBezetting_Shown(object sender, EventArgs e)
        {
            SelectedDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            SelectedDatum = Convert.ToDateTime(SelectedDate);
            FormBezetting_UpdateControls();
        }


        private void FormBezetting_UpdateControls()
        {
            var ctx = new DeskBoekingEntities();

            var query = (from b in ctx.Boekingens
                         where b.Datum.Year == SelectedDatum.Year // https://stackoverflow.com/questions/11597373/the-specified-type-member-date-is-not-supported-in-linq-to-entities-exception
                            && b.Datum.Month == SelectedDatum.Month
                            && b.Datum.Day == SelectedDatum.Day
                         select b).ToList();

            float numBoekingen = query.Count();

            var query2 = (from d in ctx.Desks
                          select d).ToList();

            float numDesks = query2.Count();

            this.label2.Text = ((int)(numBoekingen / numDesks * 100)).ToString() + "%";
            this.label2.Refresh();
        }


        private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            SelectedDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            SelectedDatum = Convert.ToDateTime(SelectedDate);
            FormBezetting_UpdateControls();
        }

    }
}
