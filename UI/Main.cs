﻿using EmployeeCRUD.Classes;
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
            DGVLoad();
        }


        public void DGVLoad()
        {
            // Configure the DataGridView so that users can manually change 
            // only the column widths, which are set to fill mode. 
            DGVEmployees.AllowUserToAddRows = false;
            DGVEmployees.AllowUserToDeleteRows = false;
            DGVEmployees.AllowUserToResizeRows = false;
            DGVEmployees.RowHeadersWidthSizeMode =
                DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DGVEmployees.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGVEmployees.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // Configure the top left header cell as a reset button.
            DGVEmployees.TopLeftHeaderCell.Value = "reset";
            DGVEmployees.TopLeftHeaderCell.Style.ForeColor =
                System.Drawing.Color.Blue;

            // Add handlers to DataGridView events.
            DGVEmployees.CellClick +=
                new DataGridViewCellEventHandler(DGVEmployees_CellClick);
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

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                BindingSource bsDvd = new BindingSource();
                DGVEmployees.DataSource = dal.GetEmployeeList();
                bsDvd.DataSource = DGVEmployees.DataSource;
                bsDvd.Filter = string.Format(@"Emp_Name LIKE '%{0}%' 
                                               OR Emp_Address LIKE '%{0}%' 
                                               OR Emp_Mobile LIKE '%{0}%'", TxtSearch.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
