namespace RentJunction.Models
{
    public class Product
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int ProductCategory { get; set; }
        public string OwnerID { get; set; }
        public string CustomerID { get; set; }
        public string OwnerName { get; set; }
        public string City { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }


    }
}