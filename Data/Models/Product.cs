using System.Collections.Generic;

namespace Zkiosk.Data.Models
{
    public class Product
    {
        // Constructor
        public Product()
        {
            Options = new HashSet<Option>();
            Variants = new HashSet<Variant>();
        }

        // Fialds
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string ImageId { get; set; }

        // Navigation Properties
        public  ICollection<Option> Options { get; set; }
        public  ICollection<Variant> Variants { get; set; }
    }
}