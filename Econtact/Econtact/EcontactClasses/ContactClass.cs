using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Econtact.EcontactClasses
{
    class ContactClass
    {
        /// Getter Setter Properties
        
        public int ID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ContactNo { get; set; }
        public string Address { get; set; }
		public string Gender { get; set; }

		public bool IsDeleted { get; set; }

		static string myConection = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

        // Selecting Data from Data base 
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myConection);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT  ID, FirstName, LastName, ContactNo, Address, Gender FROM  Contact where IsDeleted = 0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
				conn.Open();
                adapter.Fill(dt);

			}
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return dt;
        }


		// Inserting  Data in to  Data base
        public bool Insert (ContactClass contact)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myConection);
            try
            {
				string sql = "INSERT INTO Contact  (FirstName,LastName,ContactNo,Address,Gender,IsDeleted) VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender,@IsDeleted)";
				SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
				cmd.Parameters.AddWithValue("@LastName", contact.LastName);
				cmd.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
				cmd.Parameters.AddWithValue("@Address", contact.Address);
				cmd.Parameters.AddWithValue("@Gender", contact.Gender);
				cmd.Parameters.AddWithValue("@IsDeleted", false);

				conn.Open();
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

		// Method to Upddate  Data in to  Data base from Our Application
        public bool Update (ContactClass contact)
        {
            bool isSuccess =false;
            SqlConnection con = new SqlConnection(myConection);

            try
            {
                string sql = "UPDATE Contact SET FirstName=@FirstName,LastName=@LastName,ContactNo=@ContactNo,Address=@Address,Gender=@Gender,IsDeleted=@IsDeleted Where ID=@ID";
                SqlCommand cmd =new SqlCommand(sql, con);
				cmd.Parameters.AddWithValue("@FirstName", contact.FirstName);
				cmd.Parameters.AddWithValue("@LastName", contact.LastName);
				cmd.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
				cmd.Parameters.AddWithValue("@Address", contact.Address);
				cmd.Parameters.AddWithValue("@Gender", contact.Gender);
                cmd.Parameters.AddWithValue("@ID",contact.ID);
				cmd.Parameters.AddWithValue("@IsDeleted", false);


				con.Open();
                int row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                
			}
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return isSuccess;
        }

		// Method to Upddate  Data in to  Data base from Our Application
		public bool Delete(ContactClass contact)
		{
			bool isSuccess = false;
			SqlConnection con = new SqlConnection(myConection);

			try
			{
				string sql = "UPDATE Contact SET  IsDeleted=@IsDeleted  WHERE  ID=@ID";
				SqlCommand cmd = new SqlCommand(sql, con);

				cmd.Parameters.AddWithValue("@ID", contact.ID);
				cmd.Parameters.AddWithValue("@IsDeleted", true);

				con.Open();
				int row = cmd.ExecuteNonQuery();
				if (row > 0)
				{
					isSuccess = true;
				}
				else
				{
					isSuccess = false;
				}

			}
			catch (Exception ex)
			{

			}
			finally
			{
				con.Close();
			}
			return isSuccess;
		}

	}
}
