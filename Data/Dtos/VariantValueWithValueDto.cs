using Zkiosk.Data.Dtos;

public class VariantValueWithValueDto
    {
        // Fields
        public int Id { get; set; }
        public int OptionId { get; set; }
        public int ValueId { get; set; }
        public int VariantId { get; set; }
        public OptionValueDto Value { get; set; }
    }