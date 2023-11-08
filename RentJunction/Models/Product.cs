namespace RentJunction.Models
{
    public class Product
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int ProductCategory { get; set; }
        public string OwnerName { get; set; }
        public long OwnerNum { get; set; }
        public string City { get; set; }
        
       
    }
}