namespace ItExpertTestApi.DAL.Models
{
    public class Item
    {
        public int? Order { get; set; }

        public int Code { get; set; }

        public string Value { get; set; } = null!;
    }
}
