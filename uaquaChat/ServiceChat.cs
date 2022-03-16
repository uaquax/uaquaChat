using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


namespace uaquaChat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<ServiceUser> _users = new List<ServiceUser>();
        private int _nextID = 1;

        public int Connect(string userName)
        {
            var user = new ServiceUser()
            {
                ID = _nextID,
                name = userName,
                operationContext = OperationContext.Current
            };
            _users.Add(user);
            _nextID++;

            SendMessage($" {user.name} was connected", 0);

            return user.ID;
        }

        public void Disconnect(int ID)
        {
            var user = _users.FirstOrDefault(u => u.ID == ID);

            if (user != null)
            {
                _users.Remove(user);

                SendMessage($" {user.name} was disconnected", 0);
            }
        }

        public void SendMessage(string message, int ID)
        {
            foreach(var u in _users)
            {
                var user = _users.FirstOrDefault(i => i.ID == ID);

                u.operationContext.GetCallbackChannel<IServiceChatCallback>().MessageCallback(user.name, DateTime.Now.ToShortTimeString(), message);
            }
        }
    }
}
