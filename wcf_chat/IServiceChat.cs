using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceChat" in both code and config file together.
    [ServiceContract(CallbackContract =typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);
        [OperationContract]
        int Disconnect(int id);
        [OperationContract(IsOneWay =true)]//Не ждем ответа
        void SendMessage(string message, int id);

    }

    public interface IServerChatCallback {
        [OperationContract(IsOneWay =true)]//Неожидаем ответа клиента
        void MessageCallback(string message);
    }
}
