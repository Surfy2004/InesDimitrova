using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InesDimitrova
{
    /// <summary>
    /// Interaction logic for Restock.xaml
    /// </summary>
    public partial class Restock : Window
    {
        public Restock()
        {
            InitializeComponent();
            SqlConnection sqlCon = new SqlConnection(@"Server=localhost\SQLEXPRESS;Initial Catalog=LibraryProject;Trusted_Connection=True;");

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT Restock.ISBN AS isbn, Book_Info.Book_Title AS title, Date_of_Restock AS date, Number_of_Copies AS number"+
                                "FROM Book_Info"+
                                "LEFT JOIN Restock"+
                                "ON Restock.ISBN = Book_Info.ISBN";

                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                SqlDataReader reader = sqlCommand.ExecuteReader();
                List<Label> titles = new List<Label> { Title_1 };
                List<Label> isbn = new List<Label> { ISBN_1 };
                List<Label> date = new List<Label> { Date_1};
                List<Label> number = new List<Label> { Number_1 };

                for (int i = 0; i < titles.Count && reader.Read(); i++)
                {
                    var t = reader.GetSqlValue(4);
                    if (t == DBNull.Value)
                    {
                        titles[i].Content = "";
                    }
                    else
                    {
                        titles[i].Content = reader["title"]; ;
                        number[i].Content = reader["number"];
                        isbn[i].Content = reader["isbn"];
                        date[i].Content = reader["date"];
                    }
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
