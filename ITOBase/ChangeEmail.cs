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
        ITODAL m_ITOSQLCommand;
        
        public ChangeEmail()
        {
            InitializeComponent();
        }
        public ChangeEmail(string _EmailID)
        {
            InitializeComponent();

            m_ITOSQLCommand = new ITODAL();
            m_ITOSQLCommand.OpenConnection("Data Source=10.15.140.2;Initial Catalog=ITO;Persist Security Info=True;User ID=evgeny;Password=ywfaggzu");

            DataTable dt = m_ITOSQLCommand.ExecuteSQLCommand("Select email from emails where EmailID='"+_EmailID+"'");

            txbEmail.Text = dt.Rows[0][0].ToString();

            

        }


        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnMakeMain_Click(object sender, EventArgs e)
        {

        }
    }
}
