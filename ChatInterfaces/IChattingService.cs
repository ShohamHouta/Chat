using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatInterfaces
{
    [ServiceContract(CallbackContract =typeof(IClient))]

    public interface IChattingService
    {
        [OperationContract]
        int Login(string userName);
        [OperationContract]
        void Logout();
        [OperationContract]
        void SentToAll(string message,string userName);
        [OperationContract]
        List<string> GetCurrentUsers();
        [OperationContract]
        void AddUser(string fname, string lname, string userName, string Password);
    }
}
