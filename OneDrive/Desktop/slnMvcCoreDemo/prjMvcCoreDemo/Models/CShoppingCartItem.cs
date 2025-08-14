namespace prjMvcCoreDemo.Models
{
    public class CShoppingCartItem
    {
        public TProduct product { get; set; }
        public int productId { get; set; }
        public int count { get; set; }
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
    }
}
