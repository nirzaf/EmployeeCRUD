using EmployeeCRUD.Classes;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace EmployeeCRUD.DAL
{
    class EmployeeDAL
    {
        DbConn C = new DbConn();
        public int InsertEmployee(Employee emp)
        {
            string sql = "INSERT INTO tbl_Employee (Emp_Name,Emp_Address,Emp_Mobile) VALUES (@Name,@Address,@Mobile)";

            int result = 0;
            try
            {
                C.Con.Open();  //opening the connection 
                using (SQLiteCommand cmd = new SQLiteCommand(C.Con))
                {
                    cmd.CommandText = sql;  //Assign sql string to sql command text
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Name", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@Address", emp.Address);
                    cmd.Parameters.AddWithValue("@Mobile", emp.MobileNumber);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (C.Con != null)
                {
                    C.Con.Close();
                }
            }
        }

        public DataTable GetEmployeeList()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM tbl_Employee";
                C.Con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(C.Con))
                {
                    cmd.CommandText = sql;
                    SQLiteDataAdapter Sda = new SQLiteDataAdapter(cmd);
                    Sda.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (C.Con != null)
                {
                    C.Con.Close();
                }
            }
        }

        public int DeleteEmployee(Employee emp)
        {
            string sql = "DELETE FROM tbl_Employee WHERE Employee_ID = @Emp_Id";
            int result = 0;
            try
            {
                C.Con.Open();  //opening the connection 
                using (SQLiteCommand cmd = new SQLiteCommand(C.Con))
                {
                    cmd.CommandText = sql;  //Assign sql string to sql command text
                    cmd.Prepare();
                    cmd.Parameters.AddWithValue("@Emp_Id", emp.Id);
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                if (C.Con != null)
                {
                    C.Con.Close();
                }
            }
        }
    }
}
