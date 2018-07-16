using Zkiosk.Data.Models;

public class VariantValue
    {
        // Fields
        public int Id { get; set; }
        public int OptionId { get; set; }
        public int ValueId { get; set; }
        public int VariantId { get; set; }

        // Navigation Properties
        public Option Option { get; set; }
        public OptionValue Value { get; set; }
        public Variant Variant { get; set; }
    }