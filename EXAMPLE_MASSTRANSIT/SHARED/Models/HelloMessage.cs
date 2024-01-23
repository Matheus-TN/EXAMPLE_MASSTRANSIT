namespace SHARED.Models
{
    public class HelloMessage
    {
        public string Message { get; set; }

        public HelloMessage() { }

        public HelloMessage(string message)
        {
            Message = message;
        }
    }
}
