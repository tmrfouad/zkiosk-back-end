using System;
using System.Collections.Generic;
using System.Linq;
using Zkiosk.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Zkiosk.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Zkiosk.Data;
using Microsoft.EntityFrameworkCore;
using Zkiosk.Core;

[Route("[controller]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class OptionController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OptionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET Option
    [HttpGet]
    public async Task<IEnumerable<OptionWithValuesDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<OptionWithValuesDto>>(_unitOfWork.Options.GetAllWithValues());
        return await Task.Run(() => itemsDto);
    }

    // GET Option/product/1
    [HttpGet("product/{productId}")]
    public async Task<IEnumerable<OptionWithValuesDto>> GetByProduct(int productId)
    {
        var itemsDto = _mapper.Map<IEnumerable<OptionWithValuesDto>>(_unitOfWork.Options.GetByProductWithValues(productId));
        return await Task.Run(() => itemsDto);
    }

    // GET Option/1
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var itemDto = _mapper.Map<OptionWithValuesDto>(_unitOfWork.Options.GetWithValues(id));
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // POST Option/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]OptionWithValuesDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _mapper.Map<Option>(itemDto);

        _unitOfWork.Options.Add(item);
        _unitOfWork.Complete();

        itemDto.Id = item.Id;
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT Option/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]OptionWithValuesDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _unitOfWork.Options.GetWithValues(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        _mapper.Map(_mapper.Map<OptionDto>(itemDto), item);

        var deletedValues = item.Values
            .Where(v => itemDto.Values.SingleOrDefault(vd => vd.Id == v.Id) == null)
            .ToList();
        var updatedValues = item.Values
            .Where(v => itemDto.Values.SingleOrDefault(vd => vd.Id == v.Id) != null)
            .ToList();
        var updatedValuesDto = itemDto.Values
            .Where(vd => item.Values.SingleOrDefault(v => v.Id == vd.Id) != null)
            .ToList();
        var insertedValues = _mapper.Map<IEnumerable<OptionValue>>(itemDto.Values
            .Where(vd => item.Values.SingleOrDefault(v => v.Id == vd.Id) == null)
            .ToList());
        foreach (var insertedValue in insertedValues)
        {
            insertedValue.OptionId = item.Id;
        }

        _unitOfWork.OptionValues.RemoveRange(deletedValues);
        _mapper.Map(updatedValuesDto, updatedValues);
        _unitOfWork.OptionValues.AddRange(insertedValues);

        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // DELETE Option/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Option item = _unitOfWork.Options.Get(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<OptionDto>(item);
        _unitOfWork.Options.Remove(item);
        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}