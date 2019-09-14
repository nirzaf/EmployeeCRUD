using EmployeeCRUD.Classes;
using EmployeeCRUD.DAL;
using System;
using System.Windows.Forms;

namespace EmployeeCRUD
{
    public partial class Main : Form
    {
        Employee emp = new Employee();
        EmployeeDAL dal = new EmployeeDAL();
        public string EmployeeID;
        public Main()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        public void AddEmployee()
        {

            if (!string.IsNullOrEmpty(TxtName.Text.Trim()))
                emp.EmployeeName = TxtName.Text.Trim();
            else
                MessageBox.Show("Employee Name Cannot Be Empty");

            if(!string.IsNullOrEmpty(TxtAddress.Text.Trim()))
                emp.Address = TxtAddress.Text.Trim();
            else
                MessageBox.Show("Address Cannot Be Empty");

            if (!string.IsNullOrEmpty(TxtMobile.Text.Trim()))
               emp.MobileNumber = TxtMobile.Text.Trim();
            else
                MessageBox.Show("Mobile Number Cannot Be Empty");

            int Result = dal.InsertEmployee(emp);
            try
            {
                if (Result > 0)
                {
                    MessageBox.Show("Employee Added Successfully");
                    LoadEmployeeList();
                }
                else
                {
                    MessageBox.Show("Something went Wrong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        public void LoadEmployeeList()
        {
            DGVEmployees.DataSource = dal.GetEmployeeList();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(EmployeeID) > 0) {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected employee details?", "Warning Data going to be deleted!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    emp.Id = Convert.ToInt32(EmployeeID);
                    int Result = dal.DeleteEmployee(emp);
                    if (Result > 0)
                    {
                        MessageBox.Show("Selected Employee Details Deleted Successfully");
                        LoadEmployeeList();
                    }
                    else
                    {
                        MessageBox.Show("Something Went Wrong");
                    }
                }
                else {
                    return;
                }         
            }else {
                MessageBox.Show("Please select a row from the data grid view to delete");
            }
        }

        private void DGVEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DGVEmployees.SelectedRows.Count > 0)
                {
                    EmployeeID = DGVEmployees.SelectedRows[0].Cells[0].Value + string.Empty;
                    TxtName.Text = DGVEmployees.SelectedRows[0].Cells[1].Value + string.Empty;
                    TxtAddress.Text = DGVEmployees.SelectedRows[0].Cells[2].Value + string.Empty;
                    TxtMobile.Text = DGVEmployees.SelectedRows[0].Cells[3].Value + string.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
