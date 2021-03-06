﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataCapture;
using System.IO;
using Microsoft.VisualBasic;

namespace Inventory_Management_System
{
    public partial class EmpUpdateForm : Form
    {
        public EmpUpdateForm()
        {
            InitializeComponent();
            fillCombo();
        }

        public void SelectTab()
        {
            tabUpdateDelete.SelectedTab = tabSearch;
        }

        public void fillCombo()
        {
            combGender.Items.Add("Male");
            combGender.Items.Add("Female");

            combMarSt.Items.Add("Single");
            combMarSt.Items.Add("Married");
            combMarSt.Items.Add("Divorced");

            combReligion.Items.Add("Christianity");
            combReligion.Items.Add("Islam");

            combQuali.Items.Add("SSCE");
            combQuali.Items.Add("BSc");
            combQuali.Items.Add("MSc");
            combQuali.Items.Add("Ph.D");

        }

        private void searchDB()
        {
            try
            {
            UpDateEmp update = new UpDateEmp();
            update.Kwrd = txtbSearch.Text;
            DataTable Table = update.dTable();

            ListViewItem entry = null;
            listViewSearchEmp.Items.Clear();

                foreach (DataRow row in Table.Rows)
                {
                    
                    entry = new ListViewItem(row["Staff_ID"].ToString());
                    entry.SubItems.Add(row["Surname"].ToString());
                    entry.SubItems.Add(row["First_Name"].ToString());
                    entry.SubItems.Add(row["Gender"].ToString());
                    entry.SubItems.Add(row["Marital_Status"].ToString());
                    entry.SubItems.Add(row["DOB"].ToString());
                    entry.SubItems.Add(row["Qualification"].ToString()); 
                    entry.SubItems.Add(row["Religion"].ToString());
                    entry.SubItems.Add(row["State_of_Origin"].ToString());
                    entry.SubItems.Add(row["Email"].ToString());
                    entry.SubItems.Add(row["Address"].ToString());
                    entry.SubItems.Add(row["PhoneNumber"].ToString());
                    entry.SubItems.Add(row["RoleID"].ToString());
                    entry.SubItems.Add(row["RoleName"].ToString());
                    entry.SubItems.Add(row["DOE"].ToString());
                    entry.SubItems.Add(row["Picture"].ToString());
                    listViewSearchEmp.Items.Add(entry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FetchListView()
        {
            string staffid = null;

            try
            {
                if (string.IsNullOrEmpty(listViewSearchEmp.FocusedItem.Text))
                {

                }
                else
                {

                    staffid = listViewSearchEmp.FocusedItem.Text;
                    UpDateEmp update = new UpDateEmp();
                    update.staffid = staffid;
                    DataTable table = update.GetEmployeeTable();

                    foreach (DataRow row in table.Rows)
                    {
                        txtbStaffID.Text = row["Staff_ID"].ToString();
                        txtbSurname.Text = row["Surname"].ToString();
                        txtbFirstName.Text = row["First_Name"].ToString();
                        combGender.Text = row["Gender"].ToString();
                        combMarSt.Text = row["Marital_Status"].ToString();
                        txtbDOB.Text = row["DOB"].ToString();
                        txtbEmail.Text = row["Email"].ToString();
                        txtbAddress.Text = row["Address"].ToString();
                        combReligion.Text = row["Religion"].ToString();
                        combQuali.Text = row["Qualification"].ToString();
                        txtbPhone.Text = row["PhoneNumber"].ToString();
                        txtbRoleID.Text = row["RoleID"].ToString();
                        txtbRoleN.Text = row["RoleName"].ToString();
                        txtbSOO.Text = row["State_of_Origin"].ToString();
                        txtbDOE.Text = row["DOE"].ToString();
                        byte[] imm = row["Picture"] as byte[] ?? null;
                        if (imm == null)
                        {

                            pictImage.Image = null;
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(imm);
                            pictImage.Image = Image.FromStream(ms);
                        }
                    }
                    tabUpdateDelete.SelectedTab = tabUpdate; // Activates the Update employee form tab page

                }
            }
            catch 
            {
                Interaction.MsgBox("Please select record to update", MsgBoxStyle.Exclamation, "Update");
                return;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //private void gridSearchEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow rw = this.gridSearchEmp.Rows[e.RowIndex];

        //        txtbStaffID.Text = rw.Cells["Staff_ID"].Value.ToString();
        //        txtbSurname.Text = rw.Cells["Surname"].Value.ToString(); txtbFirstName.Text = rw.Cells["First_Name"].Value.ToString(); combGender.Text = rw.Cells["Gender"].Value.ToString(); combMarSt.Text = rw.Cells["Marital_Status"].Value.ToString();
        //        combQuali.Text = rw.Cells["Qualification"].Value.ToString(); combReligion.Text = rw.Cells["Religion"].Value.ToString(); txtbSOO.Text = rw.Cells["State_of_Origin"].Value.ToString(); txtbEmail.Text = rw.Cells["Email"].Value.ToString();
        //        txtbAddress.Text = rw.Cells["Address"].Value.ToString(); txtbPhone.Text = rw.Cells["PhoneNumber"].Value.ToString();
        //        txtbRoleID.Text = rw.Cells["RoleID"].Value.ToString(); txtbRoleN.Text = rw.Cells["RoleName"].Value.ToString(); txtbDOB.Text = rw.Cells["DOB"].Value.ToString(); txtbDOE.Text = rw.Cells["DOE"].Value.ToString();
        //        byte[] imm = rw.Cells["Picture"].Value as byte[] ?? null;
        //        if (imm == null)
        //        {

        //            pictImage.Image = null;
        //        }
        //        else
        //        {
        //            MemoryStream ms = new MemoryStream(imm);
        //            pictImage.Image = Image.FromStream(ms);
        //        }
        //    }
        //    tabUpdateDelete.SelectedTab = tabUpdate; // Activates the Update employee form tab page
                                   
        //}

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                pictImage.Image.Save(ms, pictImage.Image.RawFormat);
                byte[] imageBt = ms.ToArray();

                UpDateEmp upDate = new UpDateEmp();
                // Assign textbox contents to the object of the class UpDateEmp
                DateTime date = DateTime.Parse(txtbDOB.Text);
                DateTime datee = DateTime.Parse(txtbDOE.Text);
                upDate.StD = txtbStaffID.Text; upDate.Snam = txtbSurname.Text; upDate.Fnam = txtbFirstName.Text; upDate.gendr = combGender.Text; upDate.Mstats = combMarSt.Text;
                upDate.DOB = date; upDate.emal = txtbEmail.Text; upDate.rel = combReligion.Text; upDate.qual = combQuali.Text; upDate.RID = txtbRoleID.Text;
                upDate.Rnam = txtbRoleN.Text; upDate.DOE = datee; upDate.Adr = txtbAddress.Text; upDate.soor = txtbSOO.Text; upDate.Phon = txtbPhone.Text; upDate.IMG = imageBt;
                upDate.upDateEmp(); // Call the update method from the class

                txtbStaffID.Text = null; txtbFirstName.Text = null; txtbSurname.Text = null; combGender.Text = null; combMarSt.Text = null; combQuali.Text = null; combReligion.Text = null;
                txtbSOO.Text = null; txtbEmail.Text = null; txtbAddress.Text = null; txtbPhone.Text = null;
                txtbRoleID.Text = null; txtbRoleN.Text = null; txtbDOB.Text = null; txtbDOE.Text = null; pictImage.Image = null;

                MessageBox.Show("Employee Record Updated successfully!", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

        }

        private void EmpUpdateForm_Load(object sender, EventArgs e)
        {
            UpDateEmp update = new UpDateEmp();

            //gridSearchEmp.DataSource = update.Table();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchDB();                    
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string picPath = dlg.FileName.ToString();
                pictImage.ImageLocation = picPath;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult Confirm = MessageBox.Show("Are you sure you want to delete this employee's record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Confirm == DialogResult.Yes)
            {
                UpDateEmp delete = new UpDateEmp();
                delete.StD = txtbStaffID.Text;
                delete.deleteEmp();

                txtbStaffID.Text = null; txtbFirstName.Text = null; txtbSurname.Text = null; combGender.Text = null; combMarSt.Text = null; combQuali.Text = null; combReligion.Text = null;
                txtbSOO.Text = null; txtbEmail.Text = null; txtbAddress.Text = null; txtbPhone.Text = null;
                txtbRoleID.Text = null; txtbRoleN.Text = null; txtbDOB.Text = null; txtbDOE.Text = null; pictImage.Image = null;
                MessageBox.Show("Employee Record deleted successfully!", "Record deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Confirm == DialogResult.No)
            {
 
            }
        }

        private void txtbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchDB();
            }
        }

        private void listViewSearchEmp_MouseClick(object sender, MouseEventArgs e)
        {
            FetchListView();
        }
    }
}
