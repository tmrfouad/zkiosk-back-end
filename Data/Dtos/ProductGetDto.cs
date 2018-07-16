using System.Collections.Generic;

namespace Zkiosk.Data.Dtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string ImageId { get; set; }
        public ICollection<VariantDto> Variants { get; set; }
    }
}