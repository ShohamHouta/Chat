using ChatInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChatServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ChattingService : IChattingService
    {

        public ConcurrentDictionary<string, ConnectedClient> _ConnectedClients = new ConcurrentDictionary<string, ConnectedClient>();

        public int Login(string userName)
        {

            foreach (var client in _ConnectedClients)
            {
                if(client.Key.ToLower() == userName.ToLower())
                {
                    return 1;
                }
            }

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            ConnectedClient newClient = new ConnectedClient
            {
                connection = establishedUserConnection,
                UserName = userName
            };

            _ConnectedClients.TryAdd(userName, newClient);

            UpdateHelper(0, newClient.UserName);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Clientn login: {newClient.UserName} { DateTime.Now}");
            Console.ResetColor();

            return 0;
        }

        public void Logout()
        {
            ConnectedClient client = GetMyClient();
            if(client != null)
            {
                _ConnectedClients.TryRemove(client.UserName, out ConnectedClient removedclient);

                UpdateHelper(1, removedclient.UserName);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Clientn logoff: {removedclient.UserName} { DateTime.Now}");
                Console.ResetColor();
            }
        }

        public ConnectedClient GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            foreach (var client in _ConnectedClients)
            {
                if(client.Value.connection == establishedUserConnection)
                {
                    return client.Value;
                }
            }

            return null;
        }

        public void SentToAll(string message, string userName)
        {
            foreach (var client in _ConnectedClients)
            {
                if (client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetMessage(message, userName);
                }
            }
        }
        private void UpdateHelper(int value,string userName)
        {
            foreach (var client in _ConnectedClients)
            {
                if (client.Value.UserName.ToLower() != userName.ToLower())
                {    
                    client.Value.connection.GetUpdate(value, userName);
                }
            }
        }

        public List<string> GetCurrentUsers()
        {
            List<string> listofusers = new List<string>();
            foreach (var client in _ConnectedClients)
            {
                listofusers.Add(client.Value.UserName);
            }
            return listofusers;
        }

        public void AddUser(string fname, string lname, string userName, string Password)
        {
            
            
             
        }
    }
}
