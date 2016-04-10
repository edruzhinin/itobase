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

namespace ITOBase
{
    public partial class ChangeEmail : Form
    {
        private bool m_NewEmail;
        private string m_EmailID;
        private string m_UserID;
        
        ITODAL m_ITOSQLCommand;
        
        public ChangeEmail()
        {
            InitializeComponent();
            m_ITOSQLCommand = new ITODAL();
            m_ITOSQLCommand.OpenConnection("Data Source=10.15.140.2;Initial Catalog=ITO;Persist Security Info=True;User ID=evgeny;Password=ywfaggzu");
            
            m_NewEmail = true;
        }
       
        public ChangeEmail(bool _CreateEmail, string _UserID) : this()
        {
            m_UserID = _UserID;
            
        }
        
        public ChangeEmail(string _EmailID) : this()
        {
            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select email from emails where EmailID='"+_EmailID+"'");

            txbEmail.Text = dt.Rows[0][0].ToString();

            m_NewEmail = false;

            m_EmailID = _EmailID;
            

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_NewEmail)
            {
                m_ITOSQLCommand.ExecuteSQLNotQuery("insert into emails (email,UserId) values ('" + txbEmail.Text + "','" + m_UserID + "')");


            }
            else
            {
                m_ITOSQLCommand.ExecuteSQLNotQuery("update emails set email= '" + txbEmail.Text + "' where EmailID='" + m_EmailID + "'");
            }

            Close();

        }

        private void btnMakeMain_Click(object sender, EventArgs e)
        {

        }

       
    }
}
