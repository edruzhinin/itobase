using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ITO_DAL;
using ITO_DAL.dsITOTableAdapters;
using ADMethods;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using ITOCommon;
using InventHelper;

namespace ITOBase
{

    //тест

    public partial class MainForm : Form
    {

        int m_ProgramUserIdx = 213; //Программа работает от этого пользователя
        int m_SelectedUserIdx = -1;  //Выбранный в данный момент пользователь
        //проверка
        //Флаги изменения информации о пользователе

        bool m_NameChanged;
        bool m_LastNameChanged;
        bool m_SecondNameChanged;
        bool m_BirthdayChanged;
        bool m_DepartmentChanged;
        bool m_PositionChanged;
        bool m_PlaceChanged;
        bool m_RoomChanged;

        TableAdapterManager m_TableAdapterManager;
        dsITO.StaffDataTable m_StaffTbl;
        dsITO.stfOrgStructureDataTable m_OrgStructure;
        dsITO.stfPositionsDataTable m_Positions;
        dsITO.NewUserDataTable m_NewUser;
        QueriesTableAdapter m_CommonQuery;

        ITODAL m_ITOSQLCommand;
        // ConfigurationManager.AppSettings["ITOConnectionString"];


        public MainForm()
        {
            InitializeComponent();


        }

        public void BuildOrgStructureTree()
        {
            stfOrgStructureTableAdapter dAdapt = new stfOrgStructureTableAdapter();

            dsITO.stfOrgStructureDataTable orgStTbl = new dsITO.stfOrgStructureDataTable();

            dAdapt.Fill(orgStTbl);



            string filterString = "UpDepID is NULL";

            DataRow[] orgUnit = orgStTbl.Select(filterString);



            //Отбражаем организационные единицы верхнего уровня UpDepID NULL
            for (int i = 0; i < orgUnit.Length; i++)
            {
                tvDepartments.Nodes.Add(orgUnit[i]["DepartmentID"].ToString(), orgUnit[i]["ShortName"].ToString() + " " + orgUnit[i]["Name"].ToString());
                FillTreeChild(orgUnit[i]["DepartmentID"].ToString(), tvDepartments.Nodes[0]);


            }


        }

        public void FillTreeChild(string parentDep, TreeNode ParentNode)
        {
            stfOrgStructureTableAdapter dAdapt = new stfOrgStructureTableAdapter();

            ITO_DAL.dsITO.stfOrgStructureDataTable orgStTbl = new dsITO.stfOrgStructureDataTable();

            dAdapt.Fill(orgStTbl);



            string filterString = "UpDepID=" + parentDep;

            DataRow[] orgUnit = orgStTbl.Select(filterString);



            //Отбражаем организационные единицы верхнего уровня UpDepID NULL
            for (int i = 0; i < orgUnit.Length; i++)
            {
                ParentNode.Nodes.Add(orgUnit[i]["DepartmentID"].ToString(), orgUnit[i]["ShortName"].ToString() + " " + orgUnit[i]["Name"].ToString());

                FillTreeChild(orgUnit[i]["DepartmentID"].ToString(), ParentNode.Nodes[i]);
            }

        }

        public void FilllbStaff(string DepID)
        {
            lbStaff.Items.Clear();



            StaffTableAdapter dAdapt = new StaffTableAdapter();


            ITO_DAL.dsITO.StaffDataTable staffTbl = new dsITO.StaffDataTable();

            dAdapt.Fill(staffTbl);


            string filterString = "DepartmentID=" + DepID;

            DataRow[] staff = staffTbl.Select(filterString);



            //Отбражаем организационные единицы верхнего уровня UpDepID NULL
            for (int i = 0; i < staff.Length; i++)
            {
                ListElement le = new ListElement(staff[i]["UserID"].ToString(), staff[i]["LastName"].ToString() + " " +
                        staff[i]["Name"].ToString() + " " + staff[i]["SecondName"].ToString());

                lbStaff.Items.Add(le);

                //lbStaff.Items
                //  lvStaff.Items.Add(staff[i]["UserID"].ToString(), staff[i]["LastName"].ToString() + " " +
                // staff[i]["Name"].ToString() + " " + staff[i]["SecondName"].ToString(),0);
            }

        }

        public void FilllbStaff()
        {
            lbStaffAlpha.Items.Clear();



            StaffTableAdapter dAdapt = new StaffTableAdapter();


            ITO_DAL.dsITO.StaffDataTable staffTbl = new dsITO.StaffDataTable();

            dAdapt.Fill(staffTbl);

            string filterString = "";
            string sortString = "LastName ASC";
            
            DataRow[] staff = staffTbl.Select(filterString,sortString);
            

            //Отбражаем организационные единицы верхнего уровня UpDepID NULL
            for (int i = 0; i < staff.Length; i++)
            {
                ListElement le = new ListElement(staff[i]["UserID"].ToString(), staff[i]["LastName"].ToString() + " " +
                        staff[i]["Name"].ToString() + " " + staff[i]["SecondName"].ToString());
                
                lbStaffAlpha.Items.Add(le);

                //lbStaff.Items
                //  lvStaff.Items.Add(staff[i]["UserID"].ToString(), staff[i]["LastName"].ToString() + " " +
                // staff[i]["Name"].ToString() + " " + staff[i]["SecondName"].ToString(),0);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            BuildOrgStructureTree();

            //ADMethodsAccountManagement ADMethods = new ADMethodsAccountManagement();

            //UserPrincipal myUser = ADMethods.GetUser(@"druzhinin");

            //MessageBox.Show(myUser.GivenName + " " + myUser.EmailAddress);

            m_TableAdapterManager = new TableAdapterManager();
            m_TableAdapterManager.StaffTableAdapter = new StaffTableAdapter();
            m_TableAdapterManager.stfOrgStructureTableAdapter = new stfOrgStructureTableAdapter();
            m_TableAdapterManager.NewUserTableAdapter = new NewUserTableAdapter();

            m_CommonQuery = new QueriesTableAdapter();

            m_StaffTbl = new dsITO.StaffDataTable();
            m_NewUser = new dsITO.NewUserDataTable();
            m_TableAdapterManager.NewUserTableAdapter.Fill(m_NewUser);



            m_OrgStructure = new dsITO.stfOrgStructureDataTable();

            m_TableAdapterManager.StaffTableAdapter.Fill(m_StaffTbl);


            m_ITOSQLCommand = new ITODAL();

            m_ITOSQLCommand.OpenConnection("Data Source=10.15.140.2;Initial Catalog=ITO;Persist Security Info=True;User ID=evgeny;Password=ywfaggzu");

            //Заполняем ComboBox данными из базы

            //Должности
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select PositionID, Name from stfPositions");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(), dt.Rows[curRow][1].ToString());
                cbPosition.Items.Add(le);

            }

            //Подразделения
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select DepartmentID, ShortName, Name from stfOrgStructure");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(), dt.Rows[curRow][1].ToString() + " " + dt.Rows[curRow][2].ToString());
                cbDepartment.Items.Add(le);

            }

            //Здания
            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select BuildingID, Name from stfBuildings");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(), dt.Rows[curRow][1].ToString());
                cbWorkPlace.Items.Add(le);

            }

            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select UserID,LastName,Name,SecondName from Staff");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(), dt.Rows[curRow][1].ToString()+" "+dt.Rows[curRow][2].ToString()+" "+dt.Rows[curRow][3].ToString());
                cmbUser.Items.Add(le);

            }

            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select distinct Place from Invent");

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                cbPlace.Items.Add(dt.Rows[curRow][0].ToString());
            }

            dt = m_ITOSQLCommand.ExecuteSQLCommand("Select distinct Room from Invent");
            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                cbRoom.Items.Add(dt.Rows[curRow][0].ToString());
            }


        }


        private void tvDepartments_AfterSelect_1(object sender, TreeViewEventArgs e)
        {




        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FilllbStaff(tvDepartments.SelectedNode.Name);
        }

        public void FilllUserInfo(string _UserID)
        {

            btnADCreate.Visible = false;
            btnADSave.Visible = false;


            string filterString = "UserID=" + _UserID;

            DataRow[] staff = m_StaffTbl.Select(filterString);

            txbLastName.Text = staff[0]["LastName"].ToString();
            txbName.Text = staff[0]["Name"].ToString();
            txbSecondName.Text = staff[0]["SecondName"].ToString();






            txtbWorkRoom.Text = staff[0]["WorkRoom"].ToString();

            dtpBirthDay.Text = staff[0]["Birthday"].ToString();

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select dbo.GetUserFIOfromStaff(dep.ChiefID),dep.ShortName,dep.Name, pos.Name,dbo.GetBuildingName(stf.WorkPlace),dbo.GetStaffState(stf.State), stf.Login, stf.PayDoxLogin, dbo.GetEmailByID(stf.emailID), GKLogin, UserID, TbNo from stfOrgStructure dep, stfPositions pos ,  Staff stf" +
                " where dep.DepartmentID = stf.DepartmentID and pos.PositionID = stf.PositionID and stf.UserID=" + _UserID);


            if (dt.Rows.Count > 0)
            {
                //TODO вставить проверку есть ли записи
                lblChief.Text = dt.Rows[0][0].ToString();
                cbPosition.Text = dt.Rows[0][3].ToString();
                cbDepartment.Text = dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();


                cbWorkPlace.Text = dt.Rows[0][4].ToString();

                lblState.Text = dt.Rows[0][5].ToString();

                tbLogin.Text = dt.Rows[0][6].ToString();

                if (dt.Rows[0][7].ToString() != "")
                    lblPayDoxLogin.Text = dt.Rows[0][7].ToString();
                else
                    lblPayDoxLogin.Text = dt.Rows[0][6].ToString();

                lblMainEmail.Text = dt.Rows[0][8].ToString();
                    
               

                txbGKLogin.Text = dt.Rows[0][9].ToString();

                lblUserID.Text = dt.Rows[0][10].ToString();

                txbTabNo.Text = dt.Rows[0][11].ToString();
                //Заполняем список телефонов

                FillPhones(_UserID);

                //Заполняем список EMAIL

                FillEmails(_UserID);

                //Заполняем список паролей
                FillPasswords(_UserID);     //TODO Уточнить нужно ли это здесь

                //Обнуляем флаги изменения
                m_NameChanged = false;
                m_LastNameChanged = false;
                m_SecondNameChanged = false;
                m_BirthdayChanged = false;
                m_DepartmentChanged = false;
                m_PositionChanged = false;
                m_PlaceChanged = false;
                m_RoomChanged = false;
                btnSave.Visible = false;
            }

        }

        private void FillPhones(string _UserID)
        {
            lbPhones.Items.Clear();

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select PhoneID,dbo.GetPhoneType(Type),PhoneNumber  from Phones where UserID=" + _UserID);

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {


                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(),
                                                 dt.Rows[curRow][1].ToString() + ": " + dt.Rows[curRow][2].ToString());

                lbPhones.Items.Add(le);
            }

        }

        private void FillEmails(string _UserID)
        {
            lbEmails.Items.Clear();

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select EmailID, email from Emails where UserID=" + _UserID);

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {


                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(),
                                                 dt.Rows[curRow][1].ToString());

                lbEmails.Items.Add(le);
            }

        }

        private void FillPasswords(string _UserID)
        {
            cbPassType.Items.Clear();

            //Типы Телефонов
            foreach (Enum value in Enum.GetValues(typeof(ePasswordTypes)))
            {

                ListElement le = new ListElement(Convert.ToString((int)(ePasswordTypes)value), value.ToString());
                cbPassType.Items.Add(le);
            }


            lbPasswords.Items.Clear();

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select PasswordID, dbo.GetPassType(Type),Password, Type  from stfPasswords where UserID=" + _UserID);

            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {


                ListElement le = new ListElement(dt.Rows[curRow][0].ToString(),
                                                 dt.Rows[curRow][1].ToString() + ": " + dt.Rows[curRow][2].ToString(),
                                                 dt.Rows[curRow][3].ToString(),
                                                 dt.Rows[curRow][2].ToString());

                lbPasswords.Items.Add(le);
            }
        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbWorkPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_PlaceChanged = true;
            btnSave.Visible = true;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {


        }



        private void cmenuPhones_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Name == "toolStripMenuChange")
            {
                MessageBox.Show("Изменить");


            }

            if (e.ClickedItem.Name == "toolStripMenuConfirm")
            {

                string str = string.Format("update Phones set Type = - Type, ChangerID = {0} where PhoneID = {1}",
                                    m_ProgramUserIdx.ToString(),
                                    (lbPhones.Items[lbPhones.SelectedIndex] as ListElement).Index.ToString());
                m_ITOSQLCommand.ExecuteSQLNotQuery(str);




            }

            if (e.ClickedItem.Name == "toolStripMenuAdd")
            {

                string str = string.Format("update Phones set Type = - Type, ChangerID = {0} where PhoneID = {1}",
                                    m_ProgramUserIdx.ToString(),
                                    (lbPhones.Items[lbPhones.SelectedIndex] as ListElement).Index.ToString());
                m_ITOSQLCommand.ExecuteSQLNotQuery(str);




            }

            //Считываем телефоны заново
            FillPhones((lbStaff.SelectedItem as ListElement).Index);

        }

        private void cmenuPhones_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            checkedListBox1.Items.Clear();
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();

            StaffTableAdapter dAdapt = new StaffTableAdapter();


            ITO_DAL.dsITO.StaffDataTable staffTbl = new dsITO.StaffDataTable();

            dAdapt.Fill(staffTbl);



            string filterString = "State > 0";

            // DataRow[] staff = staffTbl.Select(filterString);
            DataRow[] staff = staffTbl.Select(filterString);


            //Отбражаем организационные единицы верхнего уровня UpDepID NULL
            for (int i = 0; i < staff.Length; i++)
            {
                if (!ADcon.IsUserExisiting(staff[i]["Login"].ToString()))
                {
                    listBox1.Items.Add(staff[i]["Login"].ToString() + " " +
                                       staff[i]["LastName"].ToString() + " " +
                                       staff[i]["Name"].ToString() + " " +
                                       staff[i]["SecondName"].ToString() + " not exist");

                    checkedListBox1.Items.Add(staff[i]["Login"].ToString());
                }
                // staff[i]["Login"].ToString()



                //lbStaff.Items
                //  lvStaff.Items.Add(staff[i]["UserID"].ToString(), staff[i]["LastName"].ToString() + " " +
                // staff[i]["Name"].ToString() + " " + staff[i]["SecondName"].ToString(),0);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();

            PasswordGenerator pass = new PasswordGenerator();



            for (int i = 0; i < checkedListBox1.SelectedItems.Count; i++)
            {
                DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select login, Name,SecondName,LastName,UserID, dbo.GetEmailByID(emailID) from staff where login='" +
                               checkedListBox1.SelectedItems[i].ToString() + "'");

                if (dt.Rows.Count > 0)
                {

                    //создаем пользователя в AD
                    string password = pass.GeneratePassword(5);
                    ADcon.CreateNewUser("CN=Users,DC=vniiaes-asutp,DC=lan", dt.Rows[0][0].ToString(), password, dt.Rows[0][1].ToString(), dt.Rows[0][3].ToString());
                    string UserID = dt.Rows[0][4].ToString();
                    string login = dt.Rows[0][0].ToString();
                    UserPrincipal up = ADcon.GetUser(dt.Rows[0][0].ToString());

                    up.DisplayName = dt.Rows[0][3].ToString() + " " + dt.Rows[0][1].ToString() + " " + dt.Rows[0][2].ToString();
                    up.MiddleName = dt.Rows[0][2].ToString();
                    up.PasswordNeverExpires = true;
                    up.EmailAddress = dt.Rows[0][5].ToString();
                    up.Save();
                    //TODO Переделать добавление в группы по умолчанию
                    // ADcon.AddUserToGroup(dt.Rows[0][0].ToString(),"1С-Битрикс - Сотрудники")
                    ADcon.AddUserToGroup(dt.Rows[0][0].ToString(), "grpPermLocalUsers");



                    //Сохраняем пароль

                    if (m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("insert into stfPasswords (UserID,Type,Password,LastChangeID) values ('{0}','1','{1}','{2}')",
                                                                       UserID,
                                                                       password,
                                                                       m_ProgramUserIdx.ToString())) != 1)
                        MessageBox.Show("Innsert Pass " + pass + " to User " + login + " fails");



                }



                //checkedListBox1.SelectedItems[i]
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void ReadBtn_Click(object sender, EventArgs e)
        {
            m_TableAdapterManager.NewUserTableAdapter.Fill(m_NewUser);

            
            dataGridView1.DataSource = m_NewUser;
            




        }

        private void button3_Click(object sender, EventArgs e)
        {


            m_NewUser.AcceptChanges();
            //if (m_NewUser.HasChanges())
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ChangeUserForm changeForm = new ChangeUserForm();

            
            changeForm.m_ChangerID = m_ProgramUserIdx;
            changeForm.PrepareData(m_NewUser.Rows[e.RowIndex]["FullName"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["Department"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["Birthday"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["login"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["phone"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["workplace"].ToString(),
                                    m_NewUser.Rows[e.RowIndex]["Position"].ToString());


            //помечаем заявку обработанной
            if (changeForm.ShowDialog() == DialogResult.OK)

                m_ITOSQLCommand.ExecuteSQLNotQuery("update NewUser set State = 1 where DocID='" + m_NewUser.Rows[e.RowIndex]["DocID"].ToString() + "'");

            m_TableAdapterManager.NewUserTableAdapter.Fill(m_NewUser);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label3.Text = "";

            if (e.RowIndex < 0)
                return;
            
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();


            if (ADcon.IsUserExisiting(m_NewUser.Rows[e.RowIndex]["login"].ToString()))
            { //такой пользователь уже есть в AD
                label3.Text = m_NewUser.Rows[e.RowIndex]["login"].ToString() + " уже существует в AD " + ADcon.GetUserFIO(m_NewUser.Rows[e.RowIndex]["login"].ToString());


            }

        }

        private void lbStaff_DoubleClick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "tabPage7")
                lbToSaperion.Items.Add(lbStaff.SelectedItem);




        }

        private void button4_Click(object sender, EventArgs e)
        {
            lbToSaperion.Items.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();
            ADcon.GetUser("druzhinin");

            listBox2.Items.Clear();
            listBox2.Items.Add("Логин;ФИО;Компания;Подразделение;Должность;Адрес электронной почты;Уволен;AD GUID");

            var excel = new Excel.Application();

            excel.Visible = true;

            Excel.Workbook workbook = excel.Workbooks.Add();

            excel.Cells[1, 1].Font.FontStyle = "Bold";
            excel.Cells[1, 1].Value2 = "Логин";

            excel.Cells[1, 2].Font.FontStyle = "Bold";
            excel.Cells[1, 2].Value2 = "ФИО";

            excel.Cells[1, 3].Font.FontStyle = "Bold";
            excel.Cells[1, 3].Value2 = "Компания";

            excel.Cells[1, 4].Font.FontStyle = "Bold";
            excel.Cells[1, 4].Value2 = "Подразделение";

            excel.Cells[1, 5].Font.FontStyle = "Bold";
            excel.Cells[1, 5].Value2 = "Должность";

            excel.Cells[1, 6].Font.FontStyle = "Bold";
            excel.Cells[1, 6].Value2 = "Адрес электронной почты";

            excel.Cells[1, 7].Font.FontStyle = "Bold";
            excel.Cells[1, 7].Value2 = "Уволен";

            excel.Cells[1, 8].Font.FontStyle = "Bold";
            excel.Cells[1, 8].Value2 = "AD GUID";



            for (int i = 0; i < lbToSaperion.Items.Count; i++)
            {
                DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select login , dbo.GetUserFIOfromStaff(UserID),dbo.GetDepartmentNameByID(DepartmentID), dbo.GetPositionNameByID(PositionID), dbo.GetEmailByID(emailID) from staff where UserID='"
                                                                   + (lbToSaperion.Items[i] as ListElement).Index + "'");

                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                {


                    listBox2.Items.Add(string.Format("vniiaes-asutp.lan\\{0};{1};АО \"ВНИИАЭС\";{2};{3};{4};false;{5}",
                                                        dt.Rows[curRow][0].ToString(),
                                                        dt.Rows[curRow][1].ToString(),
                                                        dt.Rows[curRow][2].ToString(),
                                                        dt.Rows[curRow][3].ToString(),
                                                        dt.Rows[curRow][4].ToString(),
                                                        ADcon.GetUserGUID(dt.Rows[curRow][0].ToString())));

                    excel.Cells[i + 2, 1].Value2 = "vniiaes-asutp\\" + dt.Rows[curRow][0].ToString();


                    excel.Cells[i + 2, 2].Value2 = dt.Rows[curRow][1].ToString();


                    excel.Cells[i + 2, 3].Value2 = "АО \"ВНИИАЭС\"";


                    excel.Cells[i + 2, 4].Value2 = dt.Rows[curRow][2].ToString();


                    excel.Cells[i + 2, 5].Value2 = dt.Rows[curRow][3].ToString();


                    excel.Cells[i + 2, 6].Value2 = dt.Rows[curRow][4].ToString();


                    excel.Cells[i + 2, 7].Value = "false";


                    excel.Cells[i + 2, 8].Value2 = ADcon.GetUserGUID(dt.Rows[curRow][0].ToString());


                }


            }
            workbook.SaveAs(@"c:\temp\FromExcel.xlsx");
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void lbEmails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txbLastName_TextChanged(object sender, EventArgs e)
        {

            m_LastNameChanged = true;
            btnSave.Visible = true;

        }

        private void lbStaff_SelectedValueChanged(object sender, EventArgs e)
        {

            if (lbStaff.SelectedItems.Count > 0)
            {
                m_SelectedUserIdx = Convert.ToInt32((lbStaff.SelectedItem as ListElement).Index);
                FilllUserInfo((lbStaff.SelectedItem as ListElement).Index);
                FillDevicesForUser((lbStaff.SelectedItem as ListElement).Index);
            }
        }

        private void txbName_TextChanged(object sender, EventArgs e)
        {
            m_NameChanged = true;
            btnSave.Visible = true;
        }

        private void txbSecondName_TextChanged(object sender, EventArgs e)
        {
            m_SecondNameChanged = true;
            btnSave.Visible = true;
        }

        private void dtpBirthDay_ValueChanged(object sender, EventArgs e)
        {
            m_BirthdayChanged = true;
            btnSave.Visible = true;
        }

        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_DepartmentChanged = true;
            btnSave.Visible = true;
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_PositionChanged = true;
            btnSave.Visible = true;
        }

        private void txtbWorkRoom_TextChanged(object sender, EventArgs e)
        {
            m_RoomChanged = true;
            btnSave.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strSQL = "update staff set ";
            bool firstUpdate = true;

            if (m_NameChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;
                strSQL += "Name='" + txbName.Text + "'";

            }

            if (m_LastNameChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;
                strSQL += "LastName='" + txbLastName.Text + "'";
            }

            if (m_SecondNameChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;
                strSQL += "SecondName='" + txbSecondName.Text + "'";
            }

            if (m_BirthdayChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;

                strSQL += "Birthday=convert(datetime,'" + dtpBirthDay.Value.ToShortDateString() + "',104 )";
            }

            if (m_DepartmentChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;

                strSQL += "DepartmentID='" + (cbDepartment.SelectedItem as ListElement).Index + "'";
            }

            if (m_PositionChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;

                strSQL += "PositionID='" + (cbPosition.SelectedItem as ListElement).Index + "'";
            }
            if (m_PlaceChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;

                strSQL += "WorkPlace='" + (cbWorkPlace.SelectedItem as ListElement).Index + "'";
            }

            if (m_RoomChanged)
            {
                if (!firstUpdate)    //если не первое изменяемое поле, то необходимо доабвить запятую перед
                {
                    strSQL += ", ";
                }
                else
                    firstUpdate = false;

                strSQL += "WorkRoom='" + txtbWorkRoom.Text + "'";
            }



            strSQL += ", LastChangeID ='" + m_ProgramUserIdx.ToString() + "',CreateTime = GETDATE(), Bitrix='1' where UserID ='" + m_SelectedUserIdx.ToString() + "'";

            m_ITOSQLCommand.ExecuteSQLNotQuery(strSQL);



            m_TableAdapterManager.StaffTableAdapter.Fill(m_StaffTbl);
            FilllUserInfo(m_SelectedUserIdx.ToString());

            //если изменилось место, предложим перевезти оборудование
            if (m_RoomChanged || m_PlaceChanged)
            {

                
            }


            btnSave.Visible = false;
        }

        private void btnAddPassword_Click(object sender, EventArgs e)
        {
            if (txbNewPass.Text == "" || cbPassType.SelectedIndex == -1)
                return;

            m_CommonQuery.AddPassword(m_SelectedUserIdx, int.Parse((cbPassType.SelectedItem as ListElement).Index), txbNewPass.Text, m_ProgramUserIdx);

            txbNewPass.Text = "";
            cbPassType.SelectedIndex = -1;


        }

        private void cbPassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cbPassType.SelectedItem != null) && (cbPassType.SelectedItem.ToString() == ePasswordTypes.Email.ToString()))
            {
                DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select e.email from staff s, Emails e where e.EmailID = s.emailID and  s.UserID='"
                                                                   + m_SelectedUserIdx.ToString() + "'");

                for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
                    if (dt.Rows.Count > 0)
                        lbEmail.Text = dt.Rows[0][0].ToString();
                    else
                    {
                        MessageBox.Show("Для пользователя не задан eamil");
                        cbPassType.SelectedItem = -1;
                    }
            }
            else
                lbEmail.Text = "";
        }

        private void cmenuPasswords_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tsMenuItemDeletePass")
            {
                try
                {
                    m_ITOSQLCommand.ExecuteSQLNotQuery("delete from stfPasswords where PasswordID='" + (lbPasswords.SelectedItem as ListElement).Index + "'");

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);

                }



            }


        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream aFile = new FileStream(tbLogin.Text + ".txt", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine("Учетный данные пользователя:\t{0} {1} {2}", txbLastName.Text, txbName.Text, txbSecondName.Text);
                sw.WriteLine("");

                for (int i = 0; i < lbPasswords.Items.Count; i++)
                {
                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.AD)
                    {
                        sw.WriteLine(@"Доступ к Компьютеру (Active Directory), Сетевым ресурсам, Wi-Fi (vniiaes-asutp), Корпоративному сайту (http://10.15.140.6), СЭД PayDox (http://10.15.140.3)");
                        sw.WriteLine("");
                        sw.WriteLine("Логин:\t\t{0}", tbLogin.Text);
                        sw.WriteLine("Пароль:\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                        sw.WriteLine("Прошу обратить внимание!!!");
                        sw.WriteLine(@"Для достпа к сетевым ресурсам с компьютеров, не являющихся членами домена vniiaes-asutp необходимо вводить полное имя в формате domen\user (vniiaes-asutp\" + tbLogin.Text + ")");
                        sw.WriteLine("");
                    }

                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.PayDox)
                    {
                        sw.WriteLine(@"Доступ к СЭД PayDox (http://10.15.140.3)");
                        sw.WriteLine("");
                        sw.WriteLine("Логин:\t\t{0}", lblPayDoxLogin.Text);
                        sw.WriteLine("Пароль:\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                    }

                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.Email)
                    {
                        sw.WriteLine("Доступ к электронной почте:\t{0}", lblMainEmail.Text);
                        sw.WriteLine("");
                        sw.WriteLine("Сервер web,imap,smtp,pop3:\tturnip.rasu.ru");
                        sw.WriteLine("Логин:\t\t\t\t{0}", lblMainEmail.Text);
                        sw.WriteLine("Пароль:\t\t\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                    }

                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.FTP)
                    {
                        sw.WriteLine(@"Доступ к FTP серверу (ftp://212.45.22.102)");
                        sw.WriteLine("");
                        sw.WriteLine("Логин:\t\t{0}", tbLogin.Text);
                        sw.WriteLine("Пароль:\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                    }

                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.SVN)
                    {
                        sw.WriteLine(@"Доступ к SVN серверу (svn://212.45.22.102)");
                        sw.WriteLine("");
                        sw.WriteLine("Логин:\t\t{0}", tbLogin.Text);
                        sw.WriteLine("Пароль:\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                    }

                    if (int.Parse((lbPasswords.Items[i] as ListElement).Type) == (int)ePasswordTypes.VPN)
                    {
                        sw.WriteLine(@"Доступ к VPN (212.45.22.99)");
                        sw.WriteLine("");
                        sw.WriteLine("Логин:\t\t{0}", tbLogin.Text);
                        sw.WriteLine("Пароль:\t\t{0}", (lbPasswords.Items[i] as ListElement).ShortName);
                        sw.WriteLine("");
                    }

                }

                sw.Close();

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);

            }

            /*  var word = new Word.Application();

            word.Visible = true;

            //object FileName = Application.StartupPath + "\\Template.docx";
            object FileName = @"q:\IT\Private\Документы\Новые пользователи\!!!Шаблон!!!.docx";
            object ConfirmConversions = false;
            object ReadOnly = false;
            object AddToRecentFiles = false;
            object PasswordDocument = "";
            object PasswordTemplate = "";
            object Revert = true;
            object WritePasswordDocument = "";
            object WritePasswordTemplate = "";
            object Format = Word.WdOpenFormat.wdOpenFormatAuto;
            object Encoding = Type.Missing;
            object Visible = true;
            object OpenAndRepair = false;
            object DocumentDirection = Word.WdDocumentDirection.wdLeftToRight;
            object NoEncodingDialog = true;
            object XMLTransform = Type.Missing;

            Word.Document doc = word.Documents.Open(ref FileName,
                                               ref ConfirmConversions,
                                               ref ReadOnly,
                                               ref AddToRecentFiles,
                                               ref PasswordDocument,
                                               ref PasswordTemplate,
                                               ref Revert,
                                               ref WritePasswordDocument,
                                               ref WritePasswordTemplate,
                                               ref Format,
                                               ref Encoding,
                                               ref Visible,
                                               ref OpenAndRepair,
                                               ref DocumentDirection,
                                               ref NoEncodingDialog,
                                               ref XMLTransform);

            object docnum = 1;
             word.Documents.get_Item(ref docnum).Activate();
            
            
            
            object oBookmark = "FullName";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = "Иванов Иван ПЕтрович";

           // word.Selection.Range.Text = "Иванов Иван ПЕтрович";

            
            //doc.Bookmarks

            */

            /*  Dim wordApp As Object
    Dim wordDoc As Objectg
    
    
    
    Set wordApp = New Word.Application
    
    
    wordApp.Visible = True
    
    Set wordDoc = wordApp.Documents.Open("q:\IT\Private\Äîêóìåíòû\Íîâûå ïîëüçîâàòåëè\!!!Øàáëîí!!!.docx")
    
    
    wordDoc.Bookmarks("FullName").Select
    
    wordApp.Selection.Range.Text = DelNoPrintSimbols(Cells(ActiveCell.Row, 1).Value)
    
    
    wordDoc.Bookmarks("email").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 8).Value)
    
    wordDoc.Bookmarks("email_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 14).Value)
    
    
    wordDoc.Bookmarks("ad_user").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value)
    
    
    wordDoc.Bookmarks("ad_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 13).Value)
    
    wordDoc.Bookmarks("paydox_user").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value)
    
    wordDoc.Bookmarks("paydox_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 15).Value)
    
    wordDoc.SaveAs ("q:\IT\Private\Äîêóìåíòû\Íîâûå ïîëüçîâàòåëè\" + DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value))
    
    wordDoc.Close
    
    wordApp.Quit*/
        }


        private void cmenuPasswords_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                //открыта вкладка оборудование
                FillDevicesForUser(m_SelectedUserIdx.ToString());

            }

        }

        private void FillDevicesForUser(string _UserIdx)
        {
           // DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("select m.Type, m.BrandName, m.Model, m.ProductNo, d.Serial_Number, Inv_Number, State, dbo.GetUserFIOfromStaff() ");
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("select ID,Type, Model, ModelNo, SerialNo, InvNo, Place, Room, CompName, State, dbo.GetUserFIOfromStaff(UserID), dbo.GetUserFIOfromStaff(MOL_ID) from Invent where UserID = " + _UserIdx + "order by Type ");

            dgwDevices.DataSource = dt; 
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
             ChangeUserForm changeForm = new ChangeUserForm();

            changeForm.m_ChangerID = m_ProgramUserIdx;
            


            //помечаем заявку обработанной
            if (changeForm.ShowDialog() == DialogResult.OK)
            { 
            }

        }

        private void dgwDevices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 InventForm = new Form2();
            //dgwDevices.Item["ID",e.RowIndex].ToString();
            string str = dgwDevices[0, e.RowIndex].Value.ToString();
            InventForm.ReadDataFromInvent(str);
            InventForm.ShowDialog();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            var excel = new Excel.Application();

            excel.Visible = true;

            Excel.Workbook workbook = excel.Workbooks.Add();

            //excel.Cells[1, 1].Font.FontStyle = "Bold";
            //excel.Cells[1, 1].Value2 = "Список оборудования не сданного при увольнении:";
            

            excel.Cells[3, 1].Font.FontStyle = "Bold";
            excel.Cells[3, 1].Value2 = "Тип";

            excel.Cells[3, 2].Font.FontStyle = "Bold";
            excel.Cells[3, 2].Value2 = "Модель";

            excel.Cells[3, 3].Font.FontStyle = "Bold";
            excel.Cells[3, 3].Value2 = "ModelNo";

            excel.Cells[3, 4].Font.FontStyle = "Bold";
            excel.Cells[3, 4].Value2 = "SerialNo";

            excel.Cells[3, 5].Font.FontStyle = "Bold";
            excel.Cells[3, 5].Value2 = "Инв. №";

            excel.Cells[3, 6].Font.FontStyle = "Bold";
            excel.Cells[3, 6].Value2 = "Здание";

            excel.Cells[3, 7].Font.FontStyle = "Bold";
            excel.Cells[3, 7].Value2 = "Кабинет";

            excel.Cells[2, 8].Font.FontStyle = "Bold";
            excel.Cells[2, 8].Value2 = "Коментарий";

            excel.Cells[2, 9].Font.FontStyle = "Bold";
            excel.Cells[2, 9].Value2 = "Состояние";

            excel.Cells[2, 10].Font.FontStyle = "Bold";
            excel.Cells[2, 10].Value2 = "Ответственный";

            excel.Cells[2, 11].Font.FontStyle = "Bold";
            excel.Cells[2, 11].Value2 = "МОЛ";
           

            for (int i = 1; i < dgwDevices.ColumnCount; i++)
           
                for (int j = 0; j < dgwDevices.RowCount-1; j++)              
                    {
                    excel.Cells[j+4,i].NumberFormat = "@";
                    excel.Cells[j+4, i].Value2 = dgwDevices[i,j].Value.ToString();
                    }
            excel.Columns.AutoFit();

            //excel.Cells[dgwDevices.RowCount+3, 1].Font.FontStyle = "Bold";
            //excel.Cells[dgwDevices.RowCount+3, 1 ].Value2 = "Данное оборудование временно осталось у меня в пользовании";

            


            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                workbook.SaveAs(saveFileDialog1.FileName);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnReadInvent_Click(object sender, EventArgs e)
        {
            string cmd = "select ID,Type, Model, ModelNo,SerialNo,InvNo,dbo.GetUserFIOfromStaff(UserID) as 'Пользователь',Place,Room,CompName as 'Комментарий',State, dbo.GetUserFIOfromStaff(MOL_ID) as 'МОЛ' from Invent where Model like '" + txbModel.Text + "'";

            if (txbModelNo.Text != "")
                cmd = cmd + "and ModelNo like '" + txbModelNo.Text + "'";

            if (txbSerialNo.Text != "")
                cmd = cmd + "and SerialNo like '" + txbSerialNo.Text + "'";
            
            if (txbInvNo.Text != "")
                cmd = cmd + "and InvNo like '" + txbInvNo.Text + "'";

            
                
            if (cbPlace.Text != "")
                cmd = cmd + "and Place ='" + cbPlace.Text + "'";

            if (cbRoom.Text != "")
                cmd = cmd + "and Room ='" + cbRoom.Text + "'";



            
            
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand(cmd);

            dgvInvent.DataSource = dt;
             
        }

        private void SaveDateGridToExcel( DataGridView _dgv)
        {
            var excel = new Excel.Application();

            excel.Visible = true;

            Excel.Workbook workbook = excel.Workbooks.Add();

           
            for (int i = 1; i < _dgv.ColumnCount - 1; i++)
                {
                excel.Cells[1,i].Font.FontStyle = "Bold";
                excel.Cells[1,i].Value2 = _dgv.Columns[i].Name;
                }

            


            for (int i = 1; i < _dgv.ColumnCount; i++)

                for (int j = 0; j < _dgv.RowCount - 1; j++)
                {
                    excel.Cells[j + 2, i].NumberFormat = "@";
                    excel.Cells[j + 2, i].Value2 = _dgv[i, j].Value.ToString();
                }
           
            excel.Columns.AutoFit();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                workbook.SaveAs(saveFileDialog1.FileName);



         }

        private void InventToExcel_Click(object sender, EventArgs e)
        {
            SaveDateGridToExcel(dgvInvent);
        }

        private void cbPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmd;
            cbRoom.Items.Clear();
            if (cbPlace.Text!="")
                cmd = "Select distinct Room from Invent where Place = '" + cbPlace.Text+"'";
            else
                cmd = "Select distinct Room from Invent";

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand(cmd);
            for (int curRow = 0; curRow < dt.Rows.Count; curRow++)
            {
                cbRoom.Items.Add(dt.Rows[curRow][0].ToString());
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string cmd = "select ID, [Наименование номенклатуры] , [Номенклатура], [Счет], [Номер],SerialNo ,Quntity, FactQuntity,Cost, dbo.GetUserFIOfromStaff(MOL_ID) as 'МОЛ', [Комментарий] from Bookkeeping  where [Наименование номенклатуры]  like '" + txbDevName.Text + "'";

            if (txbInvNo2.Text != "")
                cmd = cmd + "and ([Номер] like '" + txbInvNo2.Text + "' OR [Номенклатура] like '" + txbInvNo2.Text + "')";

            if (!checkBox1.Checked)
                cmd += "and rev > 80000";
            else
                cmd += "and rev > 50000";
            
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand(cmd);


            dgvBookkeeping.DataSource = dt;
        }

        private void dgvBookkeeping_SelectionChanged(object sender, EventArgs e)
        {

            //dgvBookkeeping.SelectedRows['Type'].ToString()
            
            //if (dgvIventVSBook.CurrentRow!=null)

            string InvNo="";


            if (dgvBookkeeping.SelectedRows.Count > 0)
                if (dgvBookkeeping.SelectedRows[0].Cells[4].Value.ToString()!="") 
                    InvNo=dgvBookkeeping.SelectedRows[0].Cells[4].Value.ToString();
                else
                    InvNo = dgvBookkeeping.SelectedRows[0].Cells[2].Value.ToString();
           

            if (InvNo!="")
            {
                string cmd = "select ID,Type, Model, ModelNo, SerialNo, InvNo, dbo.GetUserFIOfromStaff(UserID) as 'Пользователь', Place,Room, CompName as 'Комментарий', State, dbo.GetUserFIOfromStaff(MOL_ID) as 'МОЛ' from Invent where InvNo ='" + InvNo + "'";

                DataTable dt2 = m_ITOSQLCommand.ExecuteSQLCommand(cmd);

                dgvIventVSBook.DataSource = dt2;

                label21.Text = dt2.Rows.Count.ToString();

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //Сохранить в Excell
            SaveDateGridToExcel(dgvIventVSBook);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form2 InventForm = new Form2();
            //dgwDevices.Item["ID",e.RowIndex].ToString();
            string InvNo = "";
            if (dgvBookkeeping.SelectedRows.Count > 0)
                InvNo = dgvBookkeeping.SelectedRows[0].Cells[0].Value.ToString();
            
            
            InventForm.ReadDataFromBookkeeping(InvNo);
            InventForm.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 InventForm = new Form2();
            
            InventForm.ShowDialog();

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void dgvIventVSBook_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dgvIventVSBook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form2 InventForm = new Form2();
            //dgwDevices.Item["ID",e.RowIndex].ToString();
            string str = dgvIventVSBook[0, e.RowIndex].Value.ToString();
            InventForm.ReadDataFromInvent(str);
            InventForm.ShowDialog();
        }

        private void dgvInvent_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            Form2 InventForm = new Form2();
            //dgwDevices.Item["ID",e.RowIndex].ToString();
            string str = dgvInvent[0, e.RowIndex].Value.ToString();
            InventForm.ReadDataFromInvent(str);
            InventForm.ShowDialog();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form2 InventForm = new Form2();

            if (dgvInvent.SelectedRows.Count > 0)
                InventForm.ReadDataFromInvent(dgvInvent.SelectedRows[0].Cells[0].Value.ToString());
            
            //dgwDevices.Item["ID",e.RowIndex].ToString();
            //string str = dgvInvent[0, e.RowIndex].Value.ToString();
            //InventForm.ReadDataFromInvent(str);
            InventForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            //Нажатие кнопки уволен
            string cmd = " exec dbo.DismissUser " + m_ProgramUserIdx.ToString() + ',' + m_SelectedUserIdx.ToString();

            try
            {
                m_ITOSQLCommand.ExecuteSQLNotQuery(cmd);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);

            }
            

        }

        private void toolStripMenuAdd_Click(object sender, EventArgs e)
        {

        }

        private void lbPhones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmenuEmail_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmenuEmail_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "menuItemEditEmail")
            {
                ChangeEmail EmailForm = new ChangeEmail((lbEmails.Items[lbEmails.SelectedIndex] as ListElement).Index.ToString());
                
                
                EmailForm.ShowDialog();
            }

            if (e.ClickedItem.Name == "menuItemAddEmail")
            {

                ChangeEmail EmailForm = new ChangeEmail(true, m_SelectedUserIdx.ToString());


                EmailForm.ShowDialog();
            }

            if (e.ClickedItem.Name == "menuItemDeleteEmail")
            {
                


            }

            if (e.ClickedItem.Name == "menuItemMakeMainEmail")
            {
                m_ITOSQLCommand.ExecuteSQLNotQuery("update staff set EmailID= '" + (lbEmails.Items[lbEmails.SelectedIndex] as ListElement).Index.ToString() + "', Bitrix='1' where UserID='" + m_SelectedUserIdx.ToString() + "'");


            }

            
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            ChangeUserForm changeForm = new ChangeUserForm();

            changeForm.m_ChangerID = m_ProgramUserIdx;
            
            changeForm.ShowDialog();

            //помечаем заявку обработанной
         //  if (changeForm.ShowDialog() == DialogResult.OK)

               // m_ITOSQLCommand.ExecuteSQLNotQuery("update NewUser set State = 1 where DocID='" + m_NewUser.Rows[e.RowIndex]["DocID"].ToString() + "'");

           // m_TableAdapterManager.NewUserTableAdapter.Fill(m_NewUser);

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabCtrlStruct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtrlStruct.SelectedIndex == 1)
                FilllbStaff();
        }

        private void lbStaffAlpha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbStaffAlpha.SelectedItems.Count > 0)
            {
                m_SelectedUserIdx = Convert.ToInt32((lbStaffAlpha.SelectedItem as ListElement).Index);
                FilllUserInfo((lbStaffAlpha.SelectedItem as ListElement).Index);
                FillDevicesForUser((lbStaffAlpha.SelectedItem as ListElement).Index);
            }

        }

        private void btnADCheck_Click(object sender, EventArgs e)
        {
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();
            if (ADcon.IsUserExisiting(tbLogin.Text))
                {
                //такой пользователь уже есть
                }
            else
                {
                //такого пользователя нет
                    btnADCreate.Visible = true;
            
                }
        }

        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            btnADSave.Visible = true;
        }

        private void btnADSave_Click(object sender, EventArgs e)
        {
            ADMethodsAccountManagement ADcon = new ADMethodsAccountManagement();
            

            //проверяем, если такой пользователь уже есть
            if (ADcon.IsUserExisiting(tbLogin.Text))
            {
                MessageBox.Show("Такой пользователь уже есть");
                return;
            }

            
            
            
            string password= null;
            
            PasswordGenerator pass = new PasswordGenerator();
            password = pass.GeneratePassword(5);  //пароль для AD

            

            UserPrincipal oUserPrincipal = ADcon.CreateNewUser("OU=RASU,DC=vniiaes-asutp,DC=lan", tbLogin.Text, password, txbName.Text, txbLastName.Text);

            oUserPrincipal.DisplayName = txbLastName.Text + " " + txbName.Text + " " + txbSecondName.Text;
            oUserPrincipal.MiddleName = txbSecondName.Text;
            //oUserPrincipal.PasswordNeverExpires = true;
            oUserPrincipal.EmailAddress = lblMainEmail.Text;

            //Добавляем пользователя в группы по умолчанию

            GroupPrincipal oGroupPrincipal = ADcon.GetGroup("OU=Share Permission,DC=vniiaes-asutp,DC=lan", "grpPermLocalUsers");
            if (oGroupPrincipal != null)
            {
                oGroupPrincipal.Members.Add(oUserPrincipal);
                oGroupPrincipal.Save();
            }
            

            oGroupPrincipal = ADcon.GetGroup("OU=Bitrix, OU=Share Permission,DC=vniiaes-asutp,DC=lan", "1С-Битрикс - Сотрудники");
            if (oGroupPrincipal != null)
            {
                oGroupPrincipal.Members.Add(oUserPrincipal);
                oGroupPrincipal.Save();
            }
           
           

           oGroupPrincipal = ADcon.GetGroup("OU=Service Permission,DC=vniiaes-asutp,DC=lan", "Wi-Fi Users");
           if (oGroupPrincipal != null)
                {
                    oGroupPrincipal.Members.Add(oUserPrincipal);
                    oGroupPrincipal.Save();
                }
               


            oUserPrincipal.Save();

            try
            {
                m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("update staff set Login='{0}' where UserID='{1}'",
                                                                                                  tbLogin.Text,
                                                                                                  m_SelectedUserIdx.ToString()));


            }

            catch (SystemException ex)
            {


            }



            if (password != null)
            {
                try
                {
                    m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("insert into stfPasswords (Password, Type,LastChangerID,UserID) values ('{0}','{1}','{2}','{3}')",
                                                                                                      password,
                                                                                                      (int)ePasswordTypes.AD,
                                                                                                      m_ProgramUserIdx.ToString(),
                                                                                                       m_SelectedUserIdx.ToString()));
               
                
                }

                catch (SystemException ex)
                {
                   

                }
                
            }

        }

        private void btnADCreate_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            try
                {
                    m_ITOSQLCommand.ExecuteSQLNotQuery(string.Format("delete from Staff where UserID= '{0}'", m_SelectedUserIdx.ToString()));                      
                
                                                                                                   
               
                
                }

                catch (SystemException ex)
                {
                }
                   

        }

        private void button13_Click(object sender, EventArgs e)
        {
           var word = new Word.Application();

           word.Visible = true;

           object FileName = Application.StartupPath + "\\"+@"Форма № R7.CLB.1.docx";
          
           object ConfirmConversions = false;
           object ReadOnly = false;
           object AddToRecentFiles = false;
           object PasswordDocument = "";
           object PasswordTemplate = "";
           object Revert = true;
           object WritePasswordDocument = "";
           object WritePasswordTemplate = "";
           object Format = Word.WdOpenFormat.wdOpenFormatAuto;
           object Encoding = Type.Missing;
           object Visible = true;
           object OpenAndRepair = false;
           object DocumentDirection = Word.WdDocumentDirection.wdLeftToRight;
           object NoEncodingDialog = true;
           object XMLTransform = Type.Missing;

           Word.Document doc = word.Documents.Open(ref FileName,
                                              ref ConfirmConversions,
                                              ref ReadOnly,
                                              ref AddToRecentFiles,
                                              ref PasswordDocument,
                                              ref PasswordTemplate,
                                              ref Revert,
                                              ref WritePasswordDocument,
                                              ref WritePasswordTemplate,
                                              ref Format,
                                              ref Encoding,
                                              ref Visible,
                                              ref OpenAndRepair,
                                              ref DocumentDirection,
                                              ref NoEncodingDialog,
                                              ref XMLTransform);

           object docnum = 1;
           
            word.Documents.get_Item(ref docnum).Activate();
            
            
            
           object oBookmark = "FIO";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

           word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = txbLastName.Text+" "+txbName.Text+" "+txbSecondName.Text;

           oBookmark = "DATA";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = DateTime.Now.ToString("dd MMMM yyyy");

           oBookmark = "Department";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = cbDepartment.Text;

           oBookmark = "email";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = lblMainEmail.Text;


           oBookmark = "GKLogin";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = txbGKLogin.Text;

           oBookmark = "Position";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = cbPosition.Text;

/*
           oBookmark = "SubsriberFIO";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = "Курятов Алексей Викторович";

           oBookmark = "SubsriberPos";

           if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

               word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = "Первый заместитель Генерального директора - Технический директор,главный конструктор";
            
            // word.Selection.Range.Text = "Иванов Иван ПЕтрович";
            */
            
           //doc.Bookmarks



            /*  Dim wordApp As Object
    Dim wordDoc As Objectg
    
    
    
    Set wordApp = New Word.Application
    
    
    wordApp.Visible = True
    
    Set wordDoc = wordApp.Documents.Open("q:\IT\Private\Äîêóìåíòû\Íîâûå ïîëüçîâàòåëè\!!!Øàáëîí!!!.docx")
    
    
    wordDoc.Bookmarks("FullName").Select
    
    wordApp.Selection.Range.Text = DelNoPrintSimbols(Cells(ActiveCell.Row, 1).Value)
    
    
    wordDoc.Bookmarks("email").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 8).Value)
    
    wordDoc.Bookmarks("email_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 14).Value)
    
    
    wordDoc.Bookmarks("ad_user").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value)
    
    
    wordDoc.Bookmarks("ad_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 13).Value)
    
    wordDoc.Bookmarks("paydox_user").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value)
             
    
    wordDoc.Bookmarks("paydox_password").Select
    
    wordApp.Selection.TypeText Text:=DelNoPrintSimbols(Cells(ActiveCell.Row, 15).Value)
    
    
             * 
    

    wordDoc.SaveAs ("q:\IT\Private\Äîêóìåíòû\Íîâûå ïîëüçîâàòåëè\" + DelNoPrintSimbols(Cells(ActiveCell.Row, 4).Value))
    
    wordDoc.Close
    
    wordApp.Quit
              */




        }

        private void button14_Click(object sender, EventArgs e)
        {
            var word = new Word.Application();

            word.Visible = true;

            object FileName = Application.StartupPath + "\\" + @"Форма № R1.CLB.1.docx";

            object ConfirmConversions = false;
            object ReadOnly = false;
            object AddToRecentFiles = false;
            object PasswordDocument = "";
            object PasswordTemplate = "";
            object Revert = true;
            object WritePasswordDocument = "";
            object WritePasswordTemplate = "";
            object Format = Word.WdOpenFormat.wdOpenFormatAuto;
            object Encoding = Type.Missing;
            object Visible = true;
            object OpenAndRepair = false;
            object DocumentDirection = Word.WdDocumentDirection.wdLeftToRight;
            object NoEncodingDialog = true;
            object XMLTransform = Type.Missing;

            Word.Document doc = word.Documents.Open(ref FileName,
                                               ref ConfirmConversions,
                                               ref ReadOnly,
                                               ref AddToRecentFiles,
                                               ref PasswordDocument,
                                               ref PasswordTemplate,
                                               ref Revert,
                                               ref WritePasswordDocument,
                                               ref WritePasswordTemplate,
                                               ref Format,
                                               ref Encoding,
                                               ref Visible,
                                               ref OpenAndRepair,
                                               ref DocumentDirection,
                                               ref NoEncodingDialog,
                                               ref XMLTransform);

            object docnum = 1;

            word.Documents.get_Item(ref docnum).Activate();



            object oBookmark = "FIO";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = txbLastName.Text + " " + txbName.Text + " " + txbSecondName.Text;

            
            oBookmark = "Department";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = cbDepartment.Text;

            oBookmark = "email";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = lblMainEmail.Text;


            oBookmark = "GKLogin";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = txbGKLogin.Text;

            oBookmark = "Position";

            if (word.ActiveDocument.Bookmarks.Exists(oBookmark.ToString()))

                word.ActiveDocument.Bookmarks.get_Item(ref oBookmark).Range.Text = cbPosition.Text;
        }

           

    } 


    
}
