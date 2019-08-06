namespace Olympia.Data.Domain
{
    public class UserMessages
    {
        public string SenderId { get; set; }

        public OlympiaUser Sender { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
