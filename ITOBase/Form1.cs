using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITO_DAL;
using ITO_DAL.ITODataSetTableAdapters;

namespace ITOBase
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ITODataSet.StaffDataTable staffTbl = new ITODataSet.StaffDataTable();
            StaffTableAdapter dAdapt = new StaffTableAdapter();
            dAdapt.Fill(staffTbl);

            dataGridView1.DataSource = staffTbl;


        }
    }
}
