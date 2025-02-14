namespace Session.DistributedSessionStore.SQL.WebApi2.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Data { get; set; }
    }
}
