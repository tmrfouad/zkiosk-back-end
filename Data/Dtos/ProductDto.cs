using System.Collections.Generic;

namespace Zkiosk.Data.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string ImageId { get; set; }
        public int VariantCount { get; set; }
    }
}