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
using ChatInterfaces;
using System.ServiceModel;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public static IChattingService server;
        private static DuplexChannelFactory<IChattingService> _channelFactory;

        public Register()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IChattingService>(new ClientCallback(), "ChatServiceEndPoint");
            server = _channelFactory.CreateChannel();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
