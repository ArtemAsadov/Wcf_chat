using ChatClient.ServiceChat;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int iD;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                iD = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                bConnDiscon.Content = "Disconect";
                isConnected = true;
            }
        }

        void DisconnectedUser()
        {
            if (isConnected)
            {
                client.Disconnect(iD);
                client = null;
                tbUserName.IsEnabled = true;
                bConnDiscon.Content = "Conect";
                isConnected = false;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectedUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectedUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                client.SendMessage(tbMessage.Text,iD);
                tbMessage.Text = string.Empty;
            }
        }
    }
}
