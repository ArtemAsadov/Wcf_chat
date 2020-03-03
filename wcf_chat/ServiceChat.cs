using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceChat" in both code and config file together.
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]//Все подключающиеся работают с 1-м сервисом
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string name)
        {
            var user = new ServerUser
            {
                Id = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMessage($"{user.Name} Подключился к чату", 0);
            users.Add(user);
            return user.Id;

        }

        public int Disconnect(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null) {
                users.Remove(user);
                SendMessage($"{user.Name} Вышел из чата",0);
            }
            return id;
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users) {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(u => u.Id == id);
                
                if (user != null)
                {
                    answer += $": {user.Name} ";
                }
                answer += message;

                item.operationContext.GetCallbackChannel<IServerChatCallback>().MessageCallback(answer);
            }
        }
    }
}
