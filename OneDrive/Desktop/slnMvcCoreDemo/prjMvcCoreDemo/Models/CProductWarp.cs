using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjMvcCoreDemo.Models
{
    public class CProductWrap
    {
        private TProduct _product = null;
        public TProduct product
        {
            get { return _product; }
            set { _product = value; }
        }
        public CProductWrap()
        {
            _product = new TProduct();
        }
        public CProductWrap(TProduct p)
        {
            _product = p;
        }
        public int FId
        {
            get { return _product.FId; }
            set { _product.FId = value; }
        }
        [Required(ErrorMessage = "產品名稱是必填項目")]
        [DisplayName("產品名稱")]
        public string? FName
        {
            get { return _product.FName; }
            set { _product.FName = value; }
        }
        [DisplayName("庫存")]
        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }
        [DisplayName("成本")]
        public decimal? FCost
        {
            get { return _product.FCost; }
            set { _product.FCost = value; }
        }
        [DisplayName("售價")]
        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }
        public string? FImagePath
        {
            get { return _product.FImagePath; }
            set { _product.FImagePath = value; }
        }
        public IFormFile photo { get; set; }
    }
}
