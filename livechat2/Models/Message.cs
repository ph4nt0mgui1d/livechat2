namespace livechat2.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string sender { get; set; }
        public int receiver { get; set; }
        public string text { get; set; }
    }
}
