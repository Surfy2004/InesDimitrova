using System;
using System.Collections.Generic;
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
using System.Data;

namespace InesDimitrova
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LABSCIFIPC17\LOCALHOST; Initial catalog =LoginData; Integrated Security=True");

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM UserInfo Where Username = @Username and Password= @password";

                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Password);

                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());

                if (count == 1)
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password or username not found.");
                }
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
