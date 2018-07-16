using System.Collections.Generic;
using System.Linq;
using Zkiosk.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Zkiosk.Data.Dtos;
using Zkiosk.Data;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class VariantController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VariantController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET Variant/
    [HttpGet]
    public async Task<IEnumerable<VariantWithValuesValueDto>> Get()
    {
        var items = _unitOfWork.Variants.GetAllWithProductAndValues();
        var itemsDto = _mapper.Map<IEnumerable<VariantWithValuesValueDto>>(items);
        return await Task.Run(() => itemsDto);
    }

    // GET Variant/Product/1
    [HttpGet("product/{productId}")]
    public async Task<IEnumerable<VariantWithValuesValueDto>> GetByProduct(int productId)
    {
        var items = _unitOfWork.Variants.GetByProductWithValues(productId);
        var itemsDto = _mapper.Map<IEnumerable<VariantWithValuesValueDto>>(items);
        return await Task.Run(() => itemsDto);
    }

    // GET Variant/Generate/1
    [HttpGet("generate/{productId}")]
    // public async Task<ActionResult> GenerateByProduct(int productId)
    public async Task<IEnumerable<VariantWithValuesValueDto>> GenerateByProduct(int productId)
    {
        var options = _unitOfWork.Options.GetByProductWithValues(productId);
        var items = GetPermulation(options as List<Option>);
        return await Task.Run(() => items);
    }

    public class GeneratedVariant
    {
        public ProductDto Product { get; set; }
        public string ValuesId { get; set; }
        public IDictionary<int, OptionValueDto> Values { get; set; }
    }

    List<VariantWithValuesValueDto> GetPermulation(List<Option> options)
    {
        var _result = new List<GeneratedVariant>();
        if (options.Count > 0)
        {
            _result = options.Skip(1).Aggregate<Option, List<GeneratedVariant>>(
            new List<GeneratedVariant>(options[0].Values.Select(s => new GeneratedVariant
            {
                Product = _mapper.Map<ProductDto>(options[0].Product),
                Values = new Dictionary<int, OptionValueDto>
                {
                {
                    options[0].Id, _mapper.Map<OptionValueDto>(s)
                }
                }
            })),
            (acc, atr) =>
            {
                var aggregateResult = new List<GeneratedVariant>();
                foreach (var createdVariant in acc)
                {
                    foreach (var possibleValue in atr.Values)
                    {
                        var newVariant = new GeneratedVariant
                        {
                            Product = createdVariant.Product,
                            Values = new Dictionary<int, OptionValueDto>(createdVariant.Values)
                        };
                        newVariant.Values[atr.Id] = _mapper.Map<OptionValueDto>(possibleValue);
                        aggregateResult.Add(newVariant);
                    }
                }

                return aggregateResult;
            });

            foreach (var variant in _result)
            {
                var valuesId = "";
                foreach (var value in variant.Values)
                {
                    valuesId += value.Value.Id;
                }
                variant.ValuesId = valuesId;
            }
        }

        var result = _result.Select(r => new VariantWithValuesValueDto
        {
            ProductId = r.Product.Id,
            ValuesId = r.ValuesId,
            Price = r.Product.Price,
            SKU = r.Product.SKU,
            Barcode = r.Product.Barcode,
            Values = r.Values.Select(a => new VariantValueWithValueDto
            {
                OptionId = a.Value.OptionId,
                ValueId = a.Value.Id,
                Value = a.Value
            }).ToList()
        }).ToList();
        return result;
    }

    // GET Variant/1
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var itemDto = _mapper.Map<VariantWithValuesValueDto>(_unitOfWork.Variants.Get(id));
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // POST Variant/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]VariantWithValuesDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _mapper.Map<Variant>(itemDto);
        _unitOfWork.Variants.Add(item);
        _unitOfWork.Complete();

        itemDto.Id = item.Id;
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // POST Variant/Range/
    [HttpPost("range")]
    public async Task<ActionResult> PostRange([FromBody]IEnumerable<VariantWithValuesDto> itemsDto)
    {
        if (itemsDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var items = _mapper.Map<IEnumerable<Variant>>(itemsDto);
        _unitOfWork.Variants.AddRange(items);
        _unitOfWork.Complete();

        itemsDto = _mapper.Map<IEnumerable<VariantWithValuesDto>>(items);
        return await Task.Run(() => new ObjectResult(itemsDto));
    }

    // PUT Variant/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]VariantWithValuesDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _unitOfWork.Variants.GetWithValues(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        _mapper.Map(_mapper.Map<Variant>(itemDto), item);

        var deletedValues = item.Values
            .Where(v => itemDto.Values.SingleOrDefault(vd => vd.Id == v.Id) == null)
            .ToList();
        var updatedValues = item.Values
            .Where(v => itemDto.Values.SingleOrDefault(vd => vd.Id == v.Id) != null)
            .ToList();
        var updatedValuesDto = itemDto.Values
            .Where(vd => item.Values.SingleOrDefault(v => v.Id == vd.Id) != null)
            .ToList();
        var insertedValues = _mapper.Map<IEnumerable<VariantValue>>(itemDto.Values
            .Where(vd => item.Values.SingleOrDefault(v => v.Id == vd.Id) == null)
            .ToList());
        foreach (var insertedValue in insertedValues)
        {
            insertedValue.VariantId = item.Id;
        }

        _unitOfWork.VariantValues.RemoveRange(deletedValues);
        _mapper.Map(updatedValuesDto, updatedValues);
        _unitOfWork.VariantValues.AddRange(insertedValues);

        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // DELETE Variant/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Variant item = _unitOfWork.Variants.Get(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<VariantDto>(item);
        _unitOfWork.Variants.Remove(item);
        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // DELETE Variant/Product/1
    [HttpDelete("product/{id}")]
    public async Task<ActionResult> DeleteByProduct(int productId)
    {
        IEnumerable<Variant> items = _unitOfWork.Variants.GetByProduct(productId);

        if (items == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemsDto = _mapper.Map<IEnumerable<VariantDto>>(items);
        _unitOfWork.Variants.RemoveRange(items);
        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemsDto));
    }
}