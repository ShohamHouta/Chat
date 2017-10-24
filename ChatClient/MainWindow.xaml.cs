using ChatInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;
        public MainWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChatServiceEndPoint");
            server = _channelFactory.CreateChannel();
            
        }

        public void TakeMessage(string message, string userName)
        {
               MessagesBox.Text += userName + ": " + message + "\n";
            MessagesBox.ScrollToEnd();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text.Length == 0) {
                return;
            }

            if (LoginButton.IsEnabled == false)
            {
                server.SentToAll(MessageTextBox.Text, UsernameTextbox.Text);
                TakeMessage(MessageTextBox.Text, "You");
                MessageTextBox.Text = "";
            }
            else if (LoginButton.IsEnabled ==true)
            {
                MessageBox.Show("Please Login First.");
            }
           
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextbox.Text.Length > 0)
            {
                int returnValue = server.Login(UsernameTextbox.Text);
                if (returnValue == 1)
                {
                    MessageBox.Show("You are already logged in. Try again");
                }
                else if (returnValue == 0)
                {

                    LoadUserList(server.GetCurrentUsers());
                    MessageBox.Show("You logged in!");
                    welcomelabel.Content = "Welcome " + UsernameTextbox.Text + "!";
                    UsernameTextbox.IsEnabled = false;
                    LoginButton.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Login First!");
            }
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SendButton_Click(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.Logout();
        }
        public void AddUserToList(string userName)
        {
            if (UserListBox.Items.Contains(userName))
            {
                return;
            }
            UserListBox.Items.Add(userName);
        }
        public void RemoveUserFromList(string userName)
        {
            if (UserListBox.Items.Contains(userName))
            {
                UserListBox.Items.Remove(userName);
            }
        }
        private void LoadUserList(List<string> users)
        {
            foreach (var user in users)
            {
                AddUserToList(user);
            }
        }
    }
}
