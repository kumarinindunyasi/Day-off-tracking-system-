using API.Entities;

namespace API.Models
{
    public class OffApproveModel
    {
        public int Id { get; set; }
        public bool OnayVerildi { get; set; }
        public DateTime? OnayTarihi { get; set; }
    }
}
