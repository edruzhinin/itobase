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
using ITOCommon;


namespace InventHelper
{
    public partial class txbSerialNo : Form
    {
        public txbSerialNo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            cbBookkeping.Items.Clear();
            clbInvent.Items.Clear();
            clbDevices.Items.Clear();
            
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;

                //cmd.CommandText = "select ID, [Наименование номенклатуры]+'\t' +[Номер]+'\t'+SerialNo+'\tQ:'+CAST(Quntity as NVARCHAR(10))+'\tF:'+CAST(FactQuntity as NVARCHAR(10))+'\t'+CAST(rev as NVARCHAR(10))+'\t'+[Комментарий] from Bookkeeping where rev = '82015'";

                cmd.CommandText = "select ID, [Наименование номенклатуры],[Тип],[Номенклатура],[Номер],SerialNo,Quntity,FactQuntity,rev,[Комментарий],MOL_name from Bookkeeping where [Наименование номенклатуры] like '" + txbName.Text + "'";

                if (txbSerial.Text.Length > 0)
                    cmd.CommandText += " and SerialNo like '" + txbSerial.Text + "'";
                if (txbInvNo.Text.Length>0)
                    cmd.CommandText += " and ( [Номер] like '" + txbInvNo.Text + "' or [Номенклатура] like '" + txbInvNo.Text + "')";

                
                if (!cbAllRev.Checked)
                    cmd.CommandText += " and rev = '82015'";
 
                
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListElement le = new ListElement(dr[0].ToString(), dr[0].ToString() + "\t" + dr[1].ToString() + "\tтип:" + dr[2].ToString() + "\t" + dr[3].ToString() + "\tИнв.№ " + dr[4].ToString() + "\ts/n: " + dr[5].ToString() + "\tКол:" + dr[6].ToString() + "\tФакт:" + dr[7].ToString() + "\trev:" + dr[8].ToString() + "\t" + dr[9].ToString() + "\t" + dr[10].ToString());
                        cbBookkeping.Items.Add(le);

                    }
                    label1.Text = cbBookkeping.Items.Count.ToString();

                }

                cmd.CommandText = "select * from Invent where Model  like '" + txbName.Text + "'";

                if (txbSerial.Text.Length > 0)
                    cmd.CommandText += " and SerialNo like '" + txbSerial.Text + "'";
                if (txbInvNo.Text.Length > 0)
                    cmd.CommandText += " and InvNo like '" + txbInvNo.Text + "'";



                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListElement le = new ListElement(dr[0].ToString(), dr[0].ToString() + "\t" + dr[1].ToString() + "\t" + dr[2].ToString() + "\tp/n: " + dr[3].ToString() + "\ts/n: " + dr[4].ToString() + "\tInv:" + dr[5].ToString() + "\t" + dr[6].ToString() + "\t" + dr[7].ToString() + "\t" + dr[8].ToString() + "\t" + dr[9].ToString() + "\t" + dr[10].ToString() );
                        clbInvent.Items.Add(le);

                    }
                    label2.Text = clbInvent.Items.Count.ToString();

                }

                cmd.CommandText = "SELECT   d.ID,m.BrandName,m.Model,m.ProductNo,d.Serial_Number,d.Inv_Number,dbo.GetUserFIOfromStaff(d.UserID),d.PlaceRoom,d.Comment,d.State,d.PlaceBuilding FROM Devices d,  models m  WHERE  m.ID = d. ModelID  and ((m.Model like '" + txbName.Text + "') or (m.ProductNo like '" + txbName.Text + "'))";

                if (txbSerial.Text.Length > 0)
                    cmd.CommandText += " and d.Serial_Number like '" + txbSerial.Text + "'";
                if (txbInvNo.Text.Length > 0)
                    cmd.CommandText += " and d.Inv_Number like '" + txbInvNo.Text + "'";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListElement le = new ListElement(dr[0].ToString(), dr[0].ToString() + "\t" + dr[1].ToString() + "\t" + dr[2].ToString() + "\tp/n: " + dr[3].ToString() + "\ts/n: " + dr[4].ToString() + "\tinv: " + dr[5].ToString() + "\t" + dr[6].ToString() + "\t" + dr[7].ToString() + "\t" + dr[8].ToString() + "\t" + dr[9].ToString() + "\t" + dr[10].ToString());
                        clbDevices.Items.Add(le);

                    }
                    label5.Text = clbDevices.Items.Count.ToString();

                }


            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void clbInvent_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((clbInvent.CheckedItems.Count == 1) && (cbBookkeping.CheckedItems.Count == 1))
            {
                DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (DbConnection cn = df.CreateConnection())
                {

                    cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                    cn.Open();

                    DbCommand cmd = df.CreateCommand();

                    cmd.Connection = cn;

                    //cmd.CommandText = "select ID, [Наименование номенклатуры]+'\t' +[Номер]+'\t'+SerialNo+'\tQ:'+CAST(Quntity as NVARCHAR(10))+'\tF:'+CAST(FactQuntity as NVARCHAR(10))+'\t'+CAST(rev as NVARCHAR(10))+'\t'+[Комментарий] from Bookkeeping where rev = '82015'";

                    cmd.CommandText = "exec dbo.SetInventConnection'" + (clbInvent.CheckedItems[0] as ListElement).Index + "','" + (cbBookkeping.CheckedItems[0] as ListElement).Index + "'";

                    cmd.ExecuteNonQuery();

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                  
            Form2 changeForm = new Form2();

            if (cbBookkeping.CheckedItems.Count > 0)
                changeForm.ReadDataFromBookkeeping((cbBookkeping.CheckedItems[0] as ListElement).Index);
            else
                if (clbInvent.CheckedItems.Count > 0)
                    changeForm.ReadDataFromInvent((clbInvent.CheckedItems[0] as ListElement).Index);
            
            if (clbDevices.CheckedItems.Count >0)
                changeForm.ReadDataFromDevices((clbDevices.CheckedItems[0] as ListElement).Index);
            
            //помечаем заявку обработанной
            if (changeForm.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
