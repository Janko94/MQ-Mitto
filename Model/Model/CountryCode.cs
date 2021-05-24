using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Model
{
    [Table("CountryCode")]
    public class CountryCode
    {
        public int AUID { get; set; }
        public string Name { get; set; }
        public string mcc { get; set; }
        public string cc { get; set; }
        public decimal? pricePerSMS { get; set; }
    }
}
