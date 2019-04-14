namespace webapp.Models
{
    public class VisitClients
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public int ClientId { get; set; }
        public virtual Clients Client { get; set; }
    }
}