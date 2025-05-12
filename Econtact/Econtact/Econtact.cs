using Econtact.EcontactClasses;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Econtact
{
    public partial class Econtact: Form
    {
        public Econtact()
        {
            InitializeComponent();
        }

        ContactClass contactClass = new ContactClass();

		// Insert Contact in Data Base in Form 
		private void buttonAdd_Click(object sender, EventArgs e)
		{
            contactClass.FirstName = textBoxFirstName.Text;
			contactClass.LastName = textBoxLastName.Text;
			contactClass.ContactNo = textBoxContactNo.Text;
			contactClass.Address = textBoxAddress.Text;
			contactClass.Gender = comboGender.Text;

			bool success = contactClass.Insert(contactClass);
			if (success )
			{
				MessageBox.Show("New Contact Successfully Intersted ");
				//	Call the Clear Method Here
				Clear();
			}
			else
			{
				MessageBox.Show("Failed to add NewContact Try Agin");
			}

			// load Data on Data GridView
			DataTable dt = contactClass.Select();
			dataGridViewContactList.DataSource = dt;

		}

		// Get all select all Contact where isdeleted = false
		private void Econtact_Load(object sender, EventArgs e)
		{
			// load Data on Data GridView
			DataTable dt = contactClass.Select();
			dataGridViewContactList.DataSource = dt;
		}
		// Close Dialog  
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		//Method To Clear Field
		private void buttonClear_Click(object sender, EventArgs e)
		{
			//	Call the Clear Method Here
			Clear();
		}

		//Method To Clear Field
		private void Clear()
		{
			textBoxFirstName.Text = "";
			textBoxLastName.Text = "";
			textBoxContactNo.Text = "";
			textBoxAddress.Text = "";
			comboGender.Text = "";
		}

		//Method To Update to Data Base 
		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			// Get The Data Form Text Box 
			contactClass.ID = contactClass.ID;
			contactClass.FirstName = textBoxFirstName.Text;
			contactClass.LastName = textBoxLastName.Text;
			contactClass.ContactNo = textBoxContactNo.Text;
			contactClass.Address = textBoxAddress.Text;
			contactClass.Gender = comboGender.Text;

			bool success = contactClass.Update(contactClass);
			if (success)
			{
				//	Update Successfully
				MessageBox.Show("Contact has been Successfully Updated. ");
				// load Data on Data GridView
				DataTable dt = contactClass.Select();
				dataGridViewContactList.DataSource = dt;
				// Clear Meathod
				Clear();
			}
			else
			{
				MessageBox.Show("Failed to Update Contact.  Try Agin");
			}




		}

		private void dataGridViewContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			//Get the Date From Data Grid View and Land it to the textboxes respectively 
			// identify the row on which mouse is clicked
			int rowindex = e.RowIndex;
			contactClass.ID = int.Parse(dataGridViewContactList.Rows[rowindex].Cells[0].Value.ToString());
			textBoxFirstName.Text = dataGridViewContactList.Rows[rowindex].Cells[1].Value.ToString();
			textBoxLastName.Text = dataGridViewContactList.Rows[rowindex].Cells[2].Value.ToString();
			textBoxContactNo.Text = dataGridViewContactList.Rows[rowindex].Cells[3].Value.ToString();
			textBoxAddress.Text = dataGridViewContactList.Rows[rowindex].Cells[4].Value.ToString();
			comboGender.Text = dataGridViewContactList.Rows[rowindex].Cells[5].Value.ToString();

		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			// Get The Data Form Text Box 
			contactClass.ID = contactClass.ID;

			bool success = contactClass.Delete(contactClass);
			if (success)
			{
				//	Update Successfully
				MessageBox.Show("Contact has been Successfully Deleted. ");
				// load Refresh Data on Data GridView
				DataTable dt = contactClass.Select();
				dataGridViewContactList.DataSource = dt;
				//	Call the Clear Method Here
				Clear();
			}
			else
			{
				MessageBox.Show("Failed to Delete Contact.  Try Agin");
			}
		}

		static string myConection = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
	{
			string keyword = textBoxSearch.Text;

			using (SqlConnection sqlConnection = new SqlConnection(myConection))
			{
				string query = @"SELECT * FROM Contact WHERE IsDeleted = 0 AND (
                            FirstName LIKE @keyword OR
                            LastName LIKE @keyword OR
                            ContactNo LIKE @keyword OR
                            Address LIKE @keyword OR
                            Gender LIKE @keyword
                         )";

				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
				sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

				DataTable dataTable = new DataTable();
				sqlDataAdapter.Fill(dataTable);
				dataGridViewContactList.DataSource = dataTable;
			}
		}

	}
}
