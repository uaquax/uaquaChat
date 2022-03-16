namespace ChatClient.Models
{
    public class MessageModel
    {
        public string Content { get; set; }
        public string Time { get; set; }
        public string Name { get; set; }

        public string Message
        {
            get
            {
                return $"{Name} > {Content} {Time}";
            }
        }
    }
}
