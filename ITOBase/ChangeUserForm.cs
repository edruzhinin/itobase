using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITO_DAL.dsITOTableAdapters;
using ITO_DAL;
using ADMethods;
using System.DirectoryServices.AccountManagement;
using ITOCommon;
using System.Net;
using  System.Net.Mail;

namespace ITOBase
{
    public partial class ChangeUserForm : Form
    {
        public int m_ChangerID;  //
        
        ITODAL m_ITOSQLCommand;
        ADMethodsAccountManagement m_ADcon;
        
        public ChangeUserForm()
        {
            InitializeComponent();

        /*    m_TableAdapterManager = new TableAdapterManager();
            m_TableAdapterManager.StaffTableAdapter = new StaffTableAdapter();
            m_TableAdapterManager.stfOrgStructureTableAdapter = new stfOrgStructureTableAdapter();
            m_TableAdapterManager.NewUserTableAdapter = new NewUserTableAdapter();

            m_StaffTbl = new dsITO.StaffDataTable();
            m_NewUser = new dsITO.NewUserDataTable();
            m_TableAdapterManager.NewUserTableAdapter.Fill(m_NewUser);



            m_OrgStructure = new dsITO.stfOrgStructureDataTable();

            m_TableAdapterManager.StaffTableAdapter.Fill(m_StaffTbl);*/

            m_ITOSQLCommand = new ITODAL();

            m_ADcon = new ADMethodsAccountManagement();

            m_ITOSQLCommand.OpenConnection("Data Source=10.15.140.2;Initial Catalog=ITO;Persist Security Info=True;User ID=evgeny;Password=ywfaggzu");

            //Заполняем ComboBox данными из базы

            //Должности
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select PositionID, Name from stfPositions");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(),dt.Rows[curRow][1].ToString());
                cbPosition.Items.Add(le);

            }

            //Подразделения
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select ShortName, Name, DepartmentID from stfOrgStructure");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][2].ToString(), dt.Rows[curRow][0].ToString() + " " + dt.Rows[curRow][1].ToString());
                cbDepartment.Items.Add(le);

            }

            //Здания
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select BuildingID, Name from stfBuildings");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(), dt.Rows[curRow][1].ToString());
                
                cbWorkPlace.Items.Add(le);

            }

            //Типы Телефонов
            foreach ( Enum value in Enum.GetValues(typeof(ePhoneTypes)))
            {

                ListElement le = new ListElement(Convert.ToString((int)(ePhoneTypes)value), ITO_StringConverter.PhoneTypeToStr((ePhoneTypes)value));
                cbPhoneType.Items.Add(le);
            }





        }

        private void ChangeUserForm_Load(object sender, EventArgs e)
        {


        }
        
        public void PrepareData(string _FullName, string _Department, string _BirthDay, string _login, string _Phone, string _WorkPlace, string _Position)
        {
            ITO_StringConverter fullname = new ITO_StringConverter(_FullName);

            txbName.Text = fullname.GetFirstName();
            txbSecondName.Text = fullname.GetSecondName();
            txbLastName.Text = fullname.GetLastName();
            dtpBirthday.Text= _BirthDay;
            txbLogin.Text = _login;
            txbPhone.Text = fullname.FormatPhone(_Phone);

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select ShortName,Name from stfOrgStructure where ShortName like '" +_Department+"%'");

            if (dt.Rows.Count > 0)
                cbDepartment.Text = dt.Rows[0][0].ToString() + " " + dt.Rows[0][1].ToString();

            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select Name from stfPositions where Name='" + _Position+"'");

            
            if (dt.Rows.Count > 0)
                cbPosition.Text = dt.Rows[0][0].ToString();

            CheckDataUnique();
          
            if (m_ADcon.IsUserExisiting(txbLogin.Text))
            {
                chbAddAD.Enabled = false;
            }
            else
            {
                chbAddAD.Enabled = true;
            }
        
        }
        
        private void CheckDataUnique()
        {
            //Проверяем, возможно такой логин уже есть в базе
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select Name, SecondName, LastName from Staff where Login='" + txbLogin.Text + "'");

            if (dt.Rows.Count > 0)
            {
                cbLog.Items.Add("Такой логин уже есть у пользователя: "+dt.Rows[0][2].ToString()+
                                                                        dt.Rows[0][0].ToString()+
                                                                        dt.Rows[0][1].ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;                
            }

             //Проверяем,есть ли такой email в базе
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select stf.Name, stf.SecondName, stf.LastName from Staff stf, Emails em where stf.emailID = em.EmailID and em.email='" + txbEmail.Text + "'");

            if (dt.Rows.Count > 0)
            {
                cbLog.Items.Add("Такой email указан как основной у пользователя: "+dt.Rows[0][2].ToString()+
                                                                        dt.Rows[0][0].ToString()+
                                                                        dt.Rows[0][1].ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;                
            }
            
            //Не все почтовые адреса привязаны к пользователям
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select stf.Name, stf.SecondName, stf.LastName from Staff stf, Emails em where stf.UserID = em.UserID and em.email='" + txbEmail.Text + "'");

            if (dt.Rows.Count > 0)
            {
                cbLog.Items.Add("Такой email  уже есть у пользователя: "+dt.Rows[0][2].ToString()+
                                                                        dt.Rows[0][0].ToString()+
                                                                        dt.Rows[0][1].ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;                
            }
            
            if (m_ADcon.IsUserExisiting(txbLogin.Text))
            {
                cbLog.Items.Add("Пользователь c таким логин уже есть в AD");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
            }

        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAddPhone_Click(object sender, EventArgs e)
        {
            if (cbPhoneType.SelectedIndex < 0)
                return;
            if (txbPhone.Text=="")
                return;
            lbPhoneType.Items.Add(cbPhoneType.SelectedItem);
            lbPhone.Items.Add(txbPhone.Text);
            

        }

        private void btnEmailFromLogin_Click(object sender, EventArgs e)
        {
            if (txbEmail.Text=="")
                txbEmail.Text = txbLogin.Text + "@vniiaes-asutp.ru";

            if (CheckEmailNotExist(txbEmail.Text))
            {
                btnEmailFromLogin.Enabled = false;
               
            } 
            else
            {
                btnEmailFromLogin.Text = "Проверить";
                checkBoxNotCheckEmail.Visible = true;
                

            }


        }


        private bool CheckEmailNotExist(string _email)
        {
            //Проверяем существование в базе
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select dbo.GetUserFIOfromStaff(UserID) from Emails where email='" + _email + "'");

            if (dt.Rows.Count > 0)
            {
                cbLog.Items.Add("Такой email уже есть в базе y " + dt.Rows[0][0].ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return false;
            }

            
            //Проверяем существование на сервере
            //создаем подключение
            if (!checkBoxNotCheckEmail.Checked)
            {
                SmtpClient client = new SmtpClient("rambutan.vniiaes-asutp.ru", 25);
                client.Credentials = new NetworkCredential("druzhinin@vniiaes-asutp.ru", "Gg3RYPOC");

                //От кого письмо
                string from = "druzhinin@vniiaes-asutp.ru";
                //Кому письмо
                string to = _email;
                //Тема письма
                string subject = "Проверка существования email";
                //Текст письма
                string body = "Здравствуйте! \n\nПроверка почтового ящика:\n" + _email;

                //Создаем сообщение

                try
                {
                    MailMessage mess = new MailMessage(from, to, subject, body);

                    client.Send(mess);

                }
                catch (System.FormatException ex)
                {
                    //строка не может быть email
                    cbLog.Items.Add(ex.Message.ToString());
                    cbLog.SelectedIndex = cbLog.Items.Count - 1;
                    return false;
                }

                catch (SmtpFailedRecipientException ex)
                {
                    //Отправлка не удалас с ошибкой адрес не сществует
                    return true;
                }

                cbLog.Items.Add("Отправка запроса на email удалась, адрес " + _email + " существует");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return false;
            }
            return true;
        }
        
        private bool AskEmailfromStarlines(string _email)
        {
            if (checkBoxNotCheckEmail.Checked)
            {
                cbLog.Items.Add("Отправка запроса на сервер пропущена");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return true;
            }
            //создаем подключение
            SmtpClient client = new SmtpClient("rambutan.vniiaes-asutp.ru", 25);
            client.Credentials = new NetworkCredential("druzhinin@vniiaes-asutp.ru", "Gg3RYPOC");
 
            //От кого письмо
            string from = "druzhinin@vniiaes-asutp.ru";
            //Кому письмо
            string to = "admins@starlines.ru";
            //Тема письма
            string subject = "Новый почтовый ящик";
            //Текст письма
            string body = "Здравствуйте! \n\nПрошу создать почтовый ящик:\n" + txbEmail.Text;
 
            //Создаем сообщение
            MailMessage mess = new MailMessage(from, to, subject, body);

            mess.CC.Add("admins@vniiaes.ru");

 
            try
            {
                client.Send(mess);
                
            }
            catch(Exception ex)
            {
                cbLog.Items.Add("Отправка запроса на email завершена c ошибкой "+ex.Message.ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return false;
            }

            

            
            return true;
            
        }
        private void btnAddEmail_Click(object sender, EventArgs e)
        {
           

            
        }

        
//Нажата кнопка записать        
        private void btnSave_Click(object sender, EventArgs e)
        {
            cbLog.Items.Clear();

            DataTable dt;

            string password=null;


            #region Check Data

            //Проверим все ли заполнено
            if (cbPosition.SelectedItem == null)
            {
                cbLog.Items.Add("Необходимо указать должность ");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
            }

            if (cbWorkPlace.SelectedItem == null)
            {
                cbLog.Items.Add("Необходимо местоположение");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
            }

            if (cbDepartment.SelectedItem == null)
            {
                cbLog.Items.Add("Необходимо подразделение");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
            }

            
            if (!CheckEmailNotExist(txbEmail.Text))
            {
                cbLog.Items.Add("Необходимо указать корректный email");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
            }
            
            
            #endregion

            #region Add User To Active Directory


            if (chbAddAD.Enabled && chbAddAD.Checked)
            {
                cbLog.Items.Add("Создаем пользователя AD");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                
                PasswordGenerator pass = new PasswordGenerator();
                password = pass.GeneratePassword(5);  //пароль для AD

                cbLog.Items.Add("Сгенерирован пароль: "+password);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                
                UserPrincipal oUserPrincipal = m_ADcon.CreateNewUser("CN=Users,DC=vniiaes-asutp,DC=lan", txbLogin.Text, password, txbName.Text, txbLastName.Text);

                oUserPrincipal.DisplayName = txbLastName.Text + " " +  txbName.Text + " " +  txbSecondName.Text;
                oUserPrincipal.MiddleName = txbSecondName.Text;
                //oUserPrincipal.PasswordNeverExpires = true;
                oUserPrincipal.EmailAddress = txbEmail.Text;
                
                //Добавляем пользователя в группы по умолчанию
                
                GroupPrincipal oGroupPrincipal = m_ADcon.GetGroup("OU=Share Permission,DC=vniiaes-asutp,DC=lan","grpPermLocalUsers");
                 if (oGroupPrincipal != null)
                    {
                     oGroupPrincipal.Members.Add(oUserPrincipal);
                     oGroupPrincipal.Save();
                     }
                 else
                     {
                     cbLog.Items.Add("Группа grpPermLocalUsers не найдена ");
                     cbLog.SelectedIndex = cbLog.Items.Count - 1;
                     }

                 oGroupPrincipal = m_ADcon.GetGroup("OU=Bitrix, OU=Share Permission,DC=vniiaes-asutp,DC=lan", "1С-Битрикс - Сотрудники");
                 if (oGroupPrincipal != null)
                 {
                     oGroupPrincipal.Members.Add(oUserPrincipal);
                     oGroupPrincipal.Save();
                 }
                 else
                 {
                     cbLog.Items.Add("Группа Bitrix не найдена ");
                     cbLog.SelectedIndex = cbLog.Items.Count - 1;
                 }

                oUserPrincipal.Save();
                cbLog.Items.Add("Пользователь  " + txbLogin.Text + " добавлен в AD");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;



            }

            #endregion


            #region Add User to Staff Table

            //Добавили пользователя
            string strFields="(Name,SecondName,LastName,Login,Birthday,WorkPlace,DepartmentID,PositionID,CreateTime,State,WorkRoom,LastChangeID)";
            string strValues=string.Format(" values ('{0}','{1}','{2}','{3}',convert(datetime,'{4}',104 ),'{5}','{6}','{7}',GETDATE(),'0','{8}','{9}')",
                                                         txbName.Text,
                                                         txbSecondName.Text,
                                                         txbLastName.Text,
                                                         txbLogin.Text,
                                                         dtpBirthday.Value.ToShortDateString(),
                                                         (cbWorkPlace.SelectedItem as ListElement).Index,
                                                         (cbDepartment.SelectedItem as ListElement).Index,
                                                         (cbPosition.SelectedItem as ListElement).Index,
                                                         txbRoom.Text,
                                                         m_ChangerID.ToString()
                                                         );
            try
            {
                if (m_ITOSQLCommand.ExecuteSQLNotQuery("insert into staff " + strFields + strValues) != 1)
                {
                    cbLog.Items.Add("insert into staff " + strFields + strValues + " FAIL");
                    cbLog.SelectedIndex = cbLog.Items.Count - 1;
                    return;
                }
            }
            catch (SystemException ex)
            {
                cbLog.Items.Add("insert into staff " + strFields + strValues + " throw Exception");
                cbLog.Items.Add(ex.Message);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
                
            }
            
            #endregion


            #region Add data to another tables
           
            //Считываем UserID втсавленной записи

            dt = m_ITOSQLCommand.ExecuteSQLCommand("select UserID  from staff where login="+"'"+txbLogin.Text+"'");
            if (dt.Rows.Count <= 0)
            {
                cbLog.Items.Add("Не найден UserId для Login " + txbLogin.Text);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                return;
            }
               
            string strUserID = dt.Rows[0][0].ToString();

            cbLog.Items.Add("Пользователь добавлен ID =  "+strUserID);
            cbLog.SelectedIndex = cbLog.Items.Count - 1;

            DialogResult = DialogResult.OK;

            
            try
            {
                if (m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("insert into Emails (email, ChangeTime,State,ChangerID, UserID) values ('{0}',GETDATE(),'1','{1}','{2}')",
                                                                                    txbEmail.Text,
                                                                                    m_ChangerID.ToString(),
                                                                                    strUserID)) != 1)
                    {
                        cbLog.Items.Add("Email  не добавлен " + txbEmail.Text);
                        cbLog.SelectedIndex = cbLog.Items.Count - 1;
                        return;
                    }

                dt = m_ITOSQLCommand.ExecuteSQLCommand("select EmailID  from Emails where email = '" + txbEmail.Text + "'");
                
                if (dt.Rows.Count <= 0)
                    {
                        cbLog.Items.Add("EmailID not find for " + txbEmail.Text);
                        cbLog.SelectedIndex = cbLog.Items.Count - 1;
                        return;
                    }
                m_ITOSQLCommand.ExecuteSQLNotQuery("update staff set emailID='" + dt.Rows[0][0].ToString() + "' where UserID='" + strUserID + "'");
            }
            
            catch (SystemException ex)
            {
                cbLog.Items.Add("Add email " + strFields + strValues + " throw Exception");
                cbLog.Items.Add(ex.Message);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
                    
            }
            cbLog.Items.Add("Email добавлен " + txbEmail.Text);
            cbLog.SelectedIndex = cbLog.Items.Count - 1;

            if (AskEmailfromStarlines(txbEmail.Text))
            {
                cbLog.Items.Add("Отправлена заявка на созадние email " + txbEmail.Text);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
            }
            else
            {
                cbLog.Items.Add("Заявку на создание email отправить не удалось " + txbEmail.Text);
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
            }
            
                 
            //запись телефонов в базу
            
            for (int i = 0; i < lbPhone.Items.Count; i++)
            {
                try
                {
                    m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("insert into Phones (PhoneNumber, Type, ChangerID, UserID, LastChanged) values ('{0}','{1}','{2}','{3}',GETDATE())",
                                                                                                      lbPhone.Items[i].ToString(),
                                                                                                      (lbPhoneType.Items[i] as ListElement).Index,
                                                                                                      m_ChangerID.ToString(),
                                                                                                      strUserID));
                }
                               
                catch (SystemException ex)
                {
                    cbLog.Items.Add("Add Phone " +lbPhoneType.Items[i].ToString()+" "+lbPhone.Items[i].ToString()+ " throw Exception");
                    cbLog.Items.Add(ex.Message);
                    cbLog.SelectedIndex = cbLog.Items.Count - 1;
    
                }
                cbLog.Items.Add("Телефон добавлен " + lbPhone.Items[i].ToString());
                cbLog.SelectedIndex = cbLog.Items.Count - 1;
            }
            //запись пароля

            

            if (password!=null)
            {
                try
                {
                    m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("insert into stfPasswords (Password, Type,LastChangerID,UserID) values ('{0}','{1}','{2}','{3}')",
                                                                                                      password,
                                                                                                      (int)ePasswordTypes.AD,
                                                                                                      m_ChangerID.ToString(),
                                                                                                      strUserID));
                }
                               
                catch (SystemException ex)
                {
                    cbLog.Items.Add("Add Password " +password+ " throw Exception");
                    cbLog.Items.Add(ex.Message);
                    cbLog.SelectedIndex = cbLog.Items.Count - 1;
                    return;
    
                }
                cbLog.Items.Add("Пароль AD добавлен ");
                cbLog.SelectedIndex = cbLog.Items.Count - 1;

            }

            
            #endregion

            btnSave.Enabled = false;
            ///Close();
            
        }

        private void txbLogin_TextChanged(object sender, EventArgs e)
        {
            if (m_ADcon.IsUserExisiting(txbLogin.Text))
            {
                chbAddAD.Enabled = false;
            }
            else
            {
                chbAddAD.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ePhoneTypes.Home.ToString());
        }

        private void txbEmail_TextChanged(object sender, EventArgs e)
        {
            btnEmailFromLogin.Enabled = true;
        }
    }

    
}
