using System.Collections.Generic;

namespace Zkiosk.Data.Models
{
    public class Option
    {
        // Constructor
        public Option()
        {
            Values = new HashSet<OptionValue>();
        }

        // Fields
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public ICollection<OptionValue> Values { get; set; }
    }
}