namespace Shop
{
    public class Product
    {
        public string ImageName { get; set; }
        public string ProductName { get; set; }
        public string Info { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public Product(string imagename, string productName, int price, string info)
        {
            ImageName = imagename;
            ProductName = productName;  
            Price = price;  
            Info = info;
            Count = 0;
        }
    }
}
