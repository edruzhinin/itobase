using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.ProviderBase;
using System.Data;
//using MySql.Data.MySqlClient;


namespace ITO_DAL
{
    public class BitrixData
    {
       /* private MySqlConnection mysqlCn;

        public void OpenConnection(string _cnStr)
        {
            mysqlCn = new MySqlConnection(_cnStr);


            mysqlCn.Open();
        }

        public void CloseConnection()
        {
            mysqlCn.Close();
        }

        public DataTable ExecuteSQLCommand(string sqlCmd)
        {
            DataTable data = new DataTable();
            MySqlCommand cmd = new MySqlCommand(sqlCmd, mysqlCn);
            MySqlDataReader dr = cmd.ExecuteReader();


            data.Load(dr);

            dr.Close();

            return data;

        }

        public DataTable ReadBitrix()
        {
            string SQLcmd = @"SELECT 
                                    b_user.ID,
                                    b_user.LOGIN,
                                    b_user.LAST_NAME,
                                    b_user.NAME,
                                    b_user.SECOND_NAME,
                                    b_user.EMAIL,
                                    b_user.PERSONAL_MOBILE,
                                    b_user.WORK_PHONE,
                                    b_user.PERSONAL_BIRTHDAY,
                                    b_iblock_section.NAME AS DEPARTMENT,
                                    b_user.WORK_POSITION,
                                    (SELECT
                                            b_user.ID
                                     FROM   b_uts_iblock_5_section,
                                            b_user
                                     WHERE b_uts_iblock_5_section.UF_HEAD = b_user.ID AND b_iblock_section.ID = b_uts_iblock_5_section.VALUE_ID
                                    ) as HEAD,
                                    b_iblock_section.ID AS DEP,
                                    b_iblock_section.IBLOCK_SECTION_ID AS UpDEP  
                              FROM b_user
                              INNER JOIN b_utm_user
                                    ON b_user.ID = b_utm_user.VALUE_ID
                              INNER JOIN b_iblock_section
                                    ON b_utm_user.VALUE_INT = b_iblock_section.ID
                              WHERE b_user.ACTIVE='Y'";

            return ExecuteSQLCommand(SQLcmd);
        }
        */


    }
    public class ITODAL
    {
        private SqlConnection sqlCn = null;
        
        public void OpenConnection(string _cnStr)
        {
            sqlCn = new SqlConnection();
                     
            sqlCn.ConnectionString = _cnStr;

            sqlCn.Open();
        }
        public void CloseConnection()
        {
            sqlCn.Close();
        }

        public DataTable ExecuteSQLCommand(string sqlCmd)
        {
            DataTable data = new DataTable();

            SqlCommand cmd = new SqlCommand(sqlCmd, sqlCn);

            SqlDataReader dr = cmd.ExecuteReader();
            
            
            data.Load(dr);

            dr.Close();

            return data;


        }

        public int ExecuteSQLNotQuery(string sqlCmd)
        {
           

            SqlCommand cmd = new SqlCommand(sqlCmd, sqlCn);

            return cmd.ExecuteNonQuery();



            

        }

       

    }
}
