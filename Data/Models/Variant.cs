using System.Collections.Generic;

namespace Zkiosk.Data.Models
{
    public class Variant
    {
        // Constructor
        public Variant()
        {
            Values = new HashSet<VariantValue>();
        }

        // Fields
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ValuesId { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public ICollection<VariantValue> Values { get; set; }
    }
}