using System.Collections.Generic;
using Zkiosk.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Zkiosk.Data.Dtos;
using Zkiosk.Core;
using Microsoft.AspNetCore.Authorization;

[Route("[controller]")]
[EnableCors("AllowAnyOrigin")]
[Authorize]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET Product/
    [HttpGet]
    public async Task<IEnumerable<ProductGetDto>> Get()
    {
        var itemsDto = _mapper.Map<IEnumerable<ProductGetDto>>(_unitOfWork.Products.GetAllWithVariants());
        return await Task.Run(() => itemsDto);
    }

    // GET Product/1
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var itemDto = _mapper.Map<ProductGetDto>(_unitOfWork.Products.GetWithVariants(id));
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // POST Product/
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]ProductDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        // itemDto.Created = DateTime.Now;

        var item = _mapper.Map<Product>(itemDto);
        _unitOfWork.Products.Add(item);
        _unitOfWork.Complete();

        itemDto = _mapper.Map<ProductDto>(item);
        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // PUT Product/1
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody]ProductDto itemDto)
    {
        if (itemDto == null)
        {
            return await Task.Run(() => BadRequest());
        }

        var item = _unitOfWork.Products.Get(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        item = _mapper.Map(itemDto, item);
        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }

    // DELETE Product/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        Product item = _unitOfWork.Products.Get(id);

        if (item == null)
        {
            return await Task.Run(() => NotFound());
        }

        var itemDto = _mapper.Map<ProductDto>(item);
        _unitOfWork.Products.Remove(item);
        _unitOfWork.Complete();

        return await Task.Run(() => new ObjectResult(itemDto));
    }
}