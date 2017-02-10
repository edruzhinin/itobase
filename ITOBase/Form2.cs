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
    public partial class Form2 : Form
    {
        bool m_NeedUpdate;
        bool m_InsertMode;

        string m_InventID;

        public void SetInsertMode()
        {
            m_InsertMode = true;

        }
        
        public Form2()
        {
            InitializeComponent();
            m_NeedUpdate = false;
            m_InsertMode = false;
        }

        public void ReadDataFromBookkeeping(string ID)
        {
            button2.Visible = false;
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;



                cmd.CommandText = "select [Наименование номенклатуры],[Номенклатура],[Номер],SerialNo,[Комментарий], MOL_ID from Bookkeeping where ID ='" + ID + "'";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                    txbComment.Text = dr[4].ToString();
                    txbInvNo.Text = dr[2].ToString();
                    txbModel.Text = dr[0].ToString();
                    txbSerialNo.Text = dr[3].ToString();
                    cbMOL_name.Text = dr[5].ToString();
                    }


                }
            }
        }
        public void ReadDataFromDevices(string ID)
        {
            button2.Visible = false;
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;



                cmd.CommandText = "SELECT   m.BrandName,m.Model,m.ProductNo,d.Serial_Number,d.Inv_Number,dbo.GetUserFIOfromStaff(d.UserID),d.PlaceRoom,d.Comment,d.State,d.PlaceBuilding FROM Devices d,  models m  WHERE  m.ID = d. ModelID  and d.ID='" + ID + "'";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        txbModel.Text = dr[0].ToString() + " " + dr[1].ToString();
                        txbModelNo.Text = dr[2].ToString();
                        txbSerialNo.Text = dr[3].ToString();
                        txbInvNo.Text = dr[4].ToString();
                        cmbUser.Text = dr[5].ToString();
                        cmbPlace.Text = dr[9].ToString();
                        txbRoom.Text = dr[6].ToString();
                        txbComment.Text = dr[7].ToString();
                        cmbState.Text = dr[8].ToString();
                    }


                }
            }
        }
        public void ReadDataFromInvent(string ID, bool ShowOnly=false)
        {
            m_NeedUpdate = true;
            m_InventID = ID;
            label1.Text = ID;
            button2.Visible = true;

            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;



                cmd.CommandText = "select ID,Type,Model,ModelNo,SerialNo,InvNo,dbo.GetUserFIOfromStaff(UserID),Place,Room,CompName,State,dbo.GetUserFIOfromStaff(MOL_ID) from Invent where ID ='" + ID + "'";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        
                        cmbType.Text = dr[1].ToString();
                        txbModel.Text = dr[2].ToString();
                        txbModelNo.Text = dr[3].ToString();
                        txbSerialNo.Text = dr[4].ToString();
                        txbInvNo.Text = dr[5].ToString();
                        cmbUser.Text = dr[6].ToString();
                        cmbPlace.Text = dr[7].ToString();
                        txbRoom.Text = dr[8].ToString();
                        txbComment.Text = dr[9].ToString();
                        cmbState.Text = dr[10].ToString();
                        cbMOL_name.Text = dr[11].ToString();
                        
                        
                    }


                }
                if (ShowOnly)
                {
                    cmbType.Enabled = false;
                    txbModel.Enabled = false;
                    txbModelNo.Enabled = false;
                    txbSerialNo.Enabled = false;
                    txbInvNo.Enabled = false;
                    cmbUser.Enabled = false;
                    cmbPlace.Enabled = false;
                    txbRoom.Enabled = false;
                    txbComment.Enabled = false;
                    cmbState.Enabled = false;
                    btnSave.Visible = false;
                        
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbPlace.Items.Clear();
            cmbUser.Items.Clear();
            cmbType.Items.Clear();
            cmbState.Items.Clear();
            
            
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;

                

                cmd.CommandText = "select distinct Place from Invent";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cmbPlace.Items.Add(dr[0].ToString());

                    }


                }

                cmd.CommandText = "select distinct Type from Invent";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cmbType.Items.Add(dr[0].ToString());

                    }


                }

                cmd.CommandText = "select distinct State from Invent";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cmbState.Items.Add(dr[0].ToString());

                    }


                }

                cmd.CommandText = "select UserID, LastName, Name, SecondName from Staff order by LastName";


                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ListElement le = new ListElement(dr[0].ToString(), dr[1].ToString() + " " + dr[2].ToString() + " " + dr[3].ToString());
                        
                        cmbUser.Items.Add(le);
                        cbMOL_name.Items.Add(le);

                    }


                }


            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;

                cmd.CommandText = "select ID from Invent where SerialNo = '" + txbSerialNo.Text + "'";

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (m_NeedUpdate && (dr[0].ToString() != m_InventID))
                        {
                            m_NeedUpdate = true;
                            Form2 fm = new Form2();
                            fm.ReadDataFromInvent(dr[0].ToString(), true);
                            fm.ShowDialog();
                            return;
                        }
                    }


                }
                if (m_InsertMode)
                {
                    cmd.CommandText = string.Format("insert into Invent (Type,Model,ModelNo,SerialNo,InvNo,[User],Place,Room,CompName,State,UserID,MOL_ID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',dbo.GetUserIdfromFIO('{10}'),dbo.GetUserIdfromFIO('{11}'))",
                                                    cmbType.Text,
                                                    txbModel.Text,
                                                    txbModelNo.Text,
                                                    txbSerialNo.Text,
                                                    txbInvNo.Text,
                                                    cmbUser.Text,
                                                    cmbPlace.Text,
                                                    txbRoom.Text,
                                                    txbComment.Text,
                                                    cmbState.Text,
                                                    cmbUser.Text,
                                                    cbMOL_name.Text);
                    cmd.ExecuteNonQuery();
                    Close();


                }

                //TODO необходимо уточнить логику
                if (m_NeedUpdate )
                {

                    cmd.CommandText = string.Format("update  Invent set Type = '{0}',Model = '{1}',ModelNo = '{2}',SerialNo ='{3}',InvNo = '{4}',[User] = '{5}',Place = '{6}',Room = '{7}',CompName = '{8}',State = '{9}', UserID = dbo.GetUserIdfromFIO('{11}'), MOL_ID =  dbo.GetUserIdfromFIO('{12}') where ID = '{10}'",
                                                    cmbType.Text,
                                                    txbModel.Text,
                                                    txbModelNo.Text,
                                                    txbSerialNo.Text,
                                                    txbInvNo.Text,
                                                    cmbUser.Text,
                                                    cmbPlace.Text,
                                                    txbRoom.Text,
                                                    txbComment.Text,
                                                    cmbState.Text,
                                                    m_InventID,
                                                    cmbUser.Text,
                                                    cbMOL_name.Text);
                }
                else
                {
                   
                    cmd.CommandText = string.Format("insert into Invent (Type,Model,ModelNo,SerialNo,InvNo,[User],Place,Room,CompName,State,UserID,MOL_ID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11})",
                                                    cmbType.Text,
                                                    txbModel.Text,
                                                    txbModelNo.Text,
                                                    txbSerialNo.Text,
                                                    txbInvNo.Text,
                                                    cmbUser.Text,
                                                    cmbPlace.Text,
                                                    txbRoom.Text,
                                                    txbComment.Text,
                                                    cmbState.Text,
                                                    (cmbUser.SelectedItem as ListElement).Index,
                                                    (cbMOL_name.SelectedItem as ListElement).Index);
                    
                }
                cmd.ExecuteNonQuery();

                Close();
            }
        }

        private void txbSerialNo_TextChanged(object sender, EventArgs e)
        {
            //if (m_NeedUpdate)
              //  m_NeedUpdate = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_NeedUpdate)
            {
                DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
                using (DbConnection cn = df.CreateConnection())
                {

                    cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                    cn.Open();

                    DbCommand cmd = df.CreateCommand();

                    cmd.Connection = cn;

                    cmd.CommandText = "delete from Invent where ID='" + m_InventID + "'";
                    cmd.ExecuteNonQuery();
                }


            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DbProviderFactory df = DbProviderFactories.GetFactory("System.Data.SqlClient");
            using (DbConnection cn = df.CreateConnection())
            {

                cn.ConnectionString = "Data Source=10.15.140.2;Initial Catalog=ITO;User ID=evgeny;Password=ywfaggzu";
                cn.Open();

                DbCommand cmd = df.CreateCommand();

                cmd.Connection = cn;

                cmd.CommandText = "select ID from Invent where SerialNo = '" + txbSerialNo.Text + "'";

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        if (m_NeedUpdate && (dr[0].ToString() != m_InventID))
                        {
                            m_NeedUpdate = true;
                            Form2 fm = new Form2();
                            fm.ReadDataFromInvent(dr[0].ToString(), true);
                            fm.ShowDialog();
                            label13.Text = "Такой серийный номер уже есть";
                            return;
                        }

                        
                        
                    }
                    
                    


                }

                if (m_InsertMode)
                {
                    cmd.CommandText = string.Format("insert into Invent (Type,Model,ModelNo,SerialNo,InvNo,[User],Place,Room,CompName,State,UserID,MOL_ID) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',dbo.GetUserIdfromFIO('{10}'),dbo.GetUserIdfromFIO('{11}'))",
                                                    cmbType.Text,
                                                    txbModel.Text,
                                                    txbModelNo.Text,
                                                    txbSerialNo.Text,
                                                    txbInvNo.Text,
                                                    cmbUser.Text,
                                                    cmbPlace.Text,
                                                    txbRoom.Text,
                                                    txbComment.Text,
                                                    cmbState.Text,
                                                    cmbUser.Text,
                                                    cbMOL_name.Text);
                    cmd.ExecuteNonQuery();
                    label13.Text = "Добавлено";
                    

                }

               
            }


        }
    }
}
