using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Data;
namespace InesDimitrova
{
    /// <summary>
    /// Interaction logic for BookInfo.xaml
    /// </summary>
    public partial class BookInfo : Window
    {
        public BookInfo()
        {
            InitializeComponent();
            SqlConnection sqlCon = new SqlConnection(@"Server=localhost\SQLEXPRESS;Initial Catalog=LibraryProject;Trusted_Connection=True;");

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT * FROM Book_Info";

                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Label> titles = new List<Label>{ label1Title, label2Title, label3Title, label4Title };
                List<Label> authors = new List<Label> { label1Author, label2Author, label3Author, label4Author };
                List<Label> availabilities = new List<Label> { label1Availability, label2Availability, label3Availability, label4Availability };
                List<Label> recentBooks = new List<Label> { label1IsNew, label2IsNew, label3IsNew, label4IsNew };

                for(int i=0; i<titles.Count && reader.Read(); i++)
                {
                    titles[i].Content = reader["Book_Title"];
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
