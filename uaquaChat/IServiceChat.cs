using System.ServiceModel;


namespace uaquaChat
{
    [ServiceContract(CallbackContract = typeof(IServiceChatCallback))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string userName);

        [OperationContract]
        void Disconnect(int ID);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int ID);
    }

    public interface IServiceChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string name, string time, string message);
    }
}
