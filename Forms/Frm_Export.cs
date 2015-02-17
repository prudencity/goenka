using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Finance_Management_System.Forms
{
    public partial class Frm_Export : Form
    {
        public Frm_Export()
        {
            InitializeComponent();
        }

        private void Frm_Export_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.DialogResult = DialogResult.OK;
        }
    }
}
