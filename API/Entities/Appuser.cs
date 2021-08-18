namespace API.Entities
{
    // Testting Github checkin
    public class Appuser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash{get; set;}
        public byte[] PasswordSalt { get; set; }
    }
}