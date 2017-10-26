using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using Microsoft.Win32;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        SqlConnection conn;
        SqlCommand CMD;
        public Register()
        {
            InitializeComponent();
            conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\App_Data\Chat_DataBase.mdf;Integrated Security=True");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            conn.Open();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string Insert = "INSERT [dbo].[USERS](Username,Password,First_Name,Last_Name,Birthday) values (@Username,@Password,@First_Name,@Last_Name,@Birthday)";
                CMD = new SqlCommand(Insert, conn);
                CMD.Parameters.AddWithValue("@Username", UsernameTextBox.Text);
                CMD.Parameters.AddWithValue("@Password", PasswordTextBox.Text);
                CMD.Parameters.AddWithValue("@First_Name", FirstNameTextBox.Text);
                CMD.Parameters.AddWithValue("@Last_Name", LastNameTextBox.Text);
                CMD.Parameters.AddWithValue("@Birthday", Birthday.SelectedDate);
                CMD.ExecuteNonQuery();
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

     
    }
}
