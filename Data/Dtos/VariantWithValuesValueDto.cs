using System.Collections.Generic;

namespace Zkiosk.Data.Dtos
{
    public class VariantWithValuesValueDto
    {
        // Fields
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ValuesId { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public ICollection<VariantValueWithValueDto> Values { get; set; }
    }
}