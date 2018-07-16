using AutoMapper;
using Zkiosk.Data.Dtos;
using Zkiosk.Data.Models;

namespace Zkiosk.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<Product, ProductGetDto>()
                .ReverseMap();
            CreateMap<Option, OptionDto>()
                .ReverseMap();
            CreateMap<OptionDto, OptionWithValuesDto>()
                .ReverseMap();
            CreateMap<Option, OptionWithValuesDto>()
                .ReverseMap();
            CreateMap<OptionValue, OptionValueDto>()
                .ReverseMap();
            CreateMap<Variant, VariantDto>()
                .ReverseMap();
            CreateMap<Variant, VariantWithValuesDto>()
                .ReverseMap();
            CreateMap<Variant, VariantWithValuesValueDto>()
                .ReverseMap();
            CreateMap<VariantDto, VariantWithValuesDto>()
                .ReverseMap();
            CreateMap<VariantValue, VariantValueDto>()
                .ReverseMap();
            CreateMap<VariantValue, VariantValueWithValueDto>()
                .ReverseMap();
        }
    }
}