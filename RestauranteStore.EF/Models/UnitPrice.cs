namespace RestauranteStore.EF.Models
{
    public class UnitPrice
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortenName { get; set; }
        public bool isDelete { get; set; } = false;
    }
}
