using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Finance_Management_System
{
    public partial class Frm_Preview : Form
    {
        public Frm_Preview(Object Obj,DataTable D)
        {
            InitializeComponent();
            ReportClass Rp = (ReportClass)Obj;
            Rp.SetDataSource(D);
            //Rp.SetDatabaseLogon(Program.ServerUserId, Program.ServerPassword, Program.ServerName, Program.DataBaseName);
            crystalReportViewer1.ReportSource = Rp;
            crystalReportViewer1.Refresh();
            //Rp.SetDatabaseLogon(Program.ServerUserId, Program.ServerPassword, Program.ServerName, Program.DataBaseName);
            crystalReportViewer1.Refresh();
        }
    }
}
