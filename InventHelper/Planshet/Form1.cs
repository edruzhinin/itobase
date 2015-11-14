using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventHelper;

namespace Planshet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;



                cmd.CommandText = "select Model, ModelNo, SerialNo, InvNo, Place,Room,[User], dbo.GetUserFIOfromStaff(UserID) from Invent where SerialNo like '" + textBox1.Text + "'";


                using (DbDataReader dr = cmd.ExecuteReader())
                {


                    while (dr.Read())
                    {

                        listBox1.Items.Add("Model:     "+dr[0].ToString());
                        listBox1.Items.Add("ModelNo:     "+dr[1].ToString());
                        listBox1.Items.Add("SerialNo:     "+dr[2].ToString());
                        listBox1.Items.Add("InvNo:     "+dr[3].ToString());
                        listBox1.Items.Add("Place:   "+dr[4].ToString()+"   Room:   "+dr[5].ToString());
                        listBox1.Items.Add("User:     "+dr[6].ToString());
                        listBox1.Items.Add("UserID:     "+dr[7].ToString());
                    }
                    dr.Close();
                 cmd.CommandText = "select Model, ModelNo, SerialNo, InvNo, Place,Room,[User], dbo.GetUserFIOfromStaff(UserID) from Invent where InvNo like '" + textBox1.Text + "'";


                 using (DbDataReader dr1 = cmd.ExecuteReader())
                 {


                     while (dr1.Read())
                     {
                         listBox1.Items.Add("Model:     " + dr1[0].ToString());
                         listBox1.Items.Add("ModelNo:     " + dr1[1].ToString());
                         listBox1.Items.Add("SerialNo:     " + dr1[2].ToString());
                         listBox1.Items.Add("InvNo:     " + dr1[3].ToString());
                         listBox1.Items.Add("Place:   " + dr1[4].ToString() + "   Room:   " + dr1[5].ToString());
                         listBox1.Items.Add("User:     " + dr1[6].ToString());
                         listBox1.Items.Add("UserID:     " + dr1[7].ToString());

                     }

                 }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 changeForm = new Form2();
            changeForm.ShowDialog();
        }
    }
}
