using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IProductoRepository _productoRepository1;
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository,
            IProductoRepository productoRepository1, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _productoRepository1 = productoRepository1;
            _mapper = mapper;
        }

        // 20221021162857
        // http://localhost:55052/api/producto
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductoDto>>> GetProductos([FromQuery] ProductoSpecificationParams productoParams)
        {
            var spec = new ProductoWithCategoriaAndMarcaSpecification(productoParams);
            var productos = await _productoRepository.GetAllWithSpec(spec);
            
            if (productos == null)
            {
                return NotFound(new CodeErrorResponse(404, "El producto no existe"));
            }

            var specCount = new ProductoForCountingSpecification(productoParams);
            var totalProductos = await _productoRepository.CountAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProductos) / Convert.ToDecimal(productoParams.PageSize));
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos);

            return Ok(
                new Pagination<ProductoDto>
                {
                    Count = totalProductos,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = productoParams.PageIndex,
                    PageSize = productoParams.PageSize
                }
            );
        }

        // 20221021163245
        // http://localhost:55052/api/producto/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int? id) {

            if (id == null)
            {
                return BadRequest(new CodeErrorResponse(400, "El Request enviado tiene errores (Producto)"));
            }
            var spec = new ProductoWithCategoriaAndMarcaSpecification(id.Value);
            var producto = await _productoRepository.GetByIdWithSpec(spec);

            if (producto == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }
             

            return Ok(_mapper.Map<Producto, ProductoDto>(producto));
        }

    }
}
