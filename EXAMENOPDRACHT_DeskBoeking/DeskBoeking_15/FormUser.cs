using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeskBoeking
{
    public partial class FormUser : Form
    {

        public event BoekingEventHandler UpdateBoeking;

        public FormUser()
        {
            InitializeComponent();
            Shown += FormUser_Load;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string naamInTextBox1 = textBox1.Text;

            if (!String.IsNullOrWhiteSpace(naamInTextBox1)) // voeg alleen toe wanneer naam gegeven
            {
                var ctx = new DeskBoekingEntities();

                if (!ctx.Users.Any(x => x.Naam == naamInTextBox1)) // bestaat gebruiker al?
                {
                    Console.WriteLine("## user name " + naamInTextBox1 + " not in database");

                    // insert user in db
                    var user = new User { Naam = naamInTextBox1 };
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    Console.WriteLine("User " + naamInTextBox1 + " added");

                    var usernaamid = (from u in ctx.Users
                                      where u.Naam == naamInTextBox1
                                      select new UserNaamId() { UserId = u.UserId, Naam = u.Naam }).First();

                    if (FormMain.formBoeking != null)
                    {
                        FormMain.formBoeking.Close();
                    }
                    FormMain.formBoeking = new FormBoeking();
                    FormMain.formBoeking.MdiParent = this.MdiParent;
                    FormMain.formBoeking.userNaamId = usernaamid;
                    FormMain.formBoeking.Show();

                    // stuur een voorbeeld event naar FormBoeking met een boodschap & usernaamid
                    if (UpdateBoeking != null)
                        UpdateBoeking(new BoekingEventArgs("Usernaam en id doorgegeven van User form aan Boeking form", usernaamid));
                }
                else
                {
                    MessageBox.Show("Deze naam bestaat al", "Fout");
                }
            }
            else
            {
                string naamInComboBox1 = comboBox1.SelectedValue.ToString();

                if (!String.IsNullOrEmpty(naamInComboBox1))
                {
                    var ctx = new DeskBoekingEntities();
                    var usernaamid = (from u in ctx.Users
                                      where u.Naam == naamInComboBox1
                                      select new UserNaamId() { UserId = u.UserId, Naam = u.Naam }).First();

                    if (FormMain.formBoeking != null)
                    {
                        FormMain.formBoeking.Close();
                    }
                    FormMain.formBoeking = new FormBoeking();
                    FormMain.formBoeking.MdiParent = this.MdiParent;
                    FormMain.formBoeking.userNaamId = usernaamid;
                    FormMain.formBoeking.Show();

                    // stuur een voorbeeld event naar FormBoeking met een boodschap & usernaamid
                    if (UpdateBoeking != null)
                        UpdateBoeking(new BoekingEventArgs("Usernaam en id doorgegeven van User form aan Boeking form", usernaamid));
                }
                else
                {
                    MessageBox.Show("Geen naam gekozen", "Fout");
                }
            }
        }


        private void FormUser_Load(object sender, EventArgs e)
        {
            // vul de combo box
            var ctx = new DeskBoekingEntities();

            // query alle namen
            var query = (from u in ctx.Users
                         select u.Naam).ToList();
            comboBox1.DataSource = query;
        }
    }
}
