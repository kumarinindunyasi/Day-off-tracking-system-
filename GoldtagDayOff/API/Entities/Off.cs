using System;

namespace API.Entities
{
    public class Off: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string IzinAdresi { get; set; }
        public string IzinSebepi { get; set; }
        public bool OnayVerildi { get; set; }
        public DateTime? OnayTarihi { get; set; }
    }
}
