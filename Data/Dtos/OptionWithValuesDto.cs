using System.Collections.Generic;

namespace Zkiosk.Data.Dtos
{
    public class OptionWithValuesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public ICollection<OptionValueDto> Values { get; set; }
    }
}