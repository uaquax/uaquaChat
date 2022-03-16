using System.ServiceModel;


namespace uaquaChat
{
    public class ServiceUser
    {
        public int ID { get; set; }
        public string name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
