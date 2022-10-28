using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    public class ProductosController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProductoListDTO>>> Get([FromQuery] Params productParams)
        {
            var res = await _unitOfWork.Productos.GetAllAsync(productParams.PageIndex,productParams.PageSize,productParams.Search);
            var listProduct = _mapper.Map<List<ProductoListDTO>>(res.registros);
            Response.Headers.Add("X-InlineCounts", res.totalRegistros.ToString());
            return new Pager<ProductoListDTO>(listProduct, res.totalRegistros, productParams.PageIndex, productParams.PageSize, productParams.Search);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get11()
        {
            var productos = await _unitOfWork.Productos.GetAllAsync();
            return _mapper.Map<List<ProductoDTO>>(productos);
        }

        [HttpGet("{idsss}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto is null)
                return NotFound();
            else
              return  _mapper.Map<ProductoDTO>(producto);
               
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoAddUpdateDTO productoDto)
        {
            var producto = _mapper.Map<Producto>(productoDto);
            _unitOfWork.Productos.Add(producto);
            await _unitOfWork.SaveAsync();
            if (producto == null)
                return BadRequest();
            else
            {
                productoDto.Id= producto.Id;
                return CreatedAtAction(nameof(Post), new { id = productoDto.Id, productoDto });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoAddUpdateDTO>> Put(int id, [FromBody] ProductoAddUpdateDTO productoDTO)
        {
            if (productoDTO is null)
                return NotFound();
           
            var producto = _mapper.Map<Producto>(productoDTO);
            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveAsync();

            return productoDTO;
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);
            if (producto is null)
                return NotFound();
            _unitOfWork.Productos.Remove(producto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }



    }
}
