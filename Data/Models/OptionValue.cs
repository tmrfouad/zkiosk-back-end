namespace Zkiosk.Data.Models
{
    public class OptionValue
    {
        // Fields
        public int Id { get; set; }
        public string Value { get; set; }
        public int OptionId { get; set; }

        // Navigation Properties
        public Option Option { get; set; }
    }
}