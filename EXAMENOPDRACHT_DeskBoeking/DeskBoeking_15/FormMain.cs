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
    public partial class FormMain : Form
    {
        public static FormUser formUser = null;
        public static FormBoeking formBoeking = null;
        public static FormBezetting formBezetting = null;


        public FormMain()
        {
            InitializeComponent();
            formUser = new FormUser();
            formUser.MdiParent = this;
            formUser.Show();
        }


        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formUser != null)
            {
                formUser.Close(); // om meerdere child forms te vermijden: close + new
            }
            formUser = new FormUser();
            formUser.MdiParent = this;
            formUser.Show();
        }

        //private void boekingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (formBoeking != null)
        //    {
        //        formBoeking.Close(); // om meerdere child forms te vermijden: close + new
        //    }
        //    formBoeking = new FormBoeking();
        //    formBoeking.MdiParent = this;
        //    formBoeking.Show();
        //}

        private void bezettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formBezetting != null)
            {
                formBezetting.Close(); // om meerdere child forms te vermijden: close + new
            }
            formBezetting = new FormBezetting();
            formBezetting.MdiParent = this;
            formBezetting.Show();
        }
    }
}
