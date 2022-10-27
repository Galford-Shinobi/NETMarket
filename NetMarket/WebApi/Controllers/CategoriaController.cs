using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class CategoriaController : BaseApiController
    {
        private readonly IGenericRepository<Categoria> _categoriaRepository;

        public CategoriaController(IGenericRepository<Categoria> categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> GetCategoriaAll()
        {
          var categorias = await _categoriaRepository.GetAllAsync();
            if (categorias == null)
            {
                return NotFound(new CodeErrorResponse(404, "La Categoria no existe"));
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaById(int? id)
        {
            if (id == null)
            {
                return BadRequest(new CodeErrorResponse(404, "El Request enviado tiene errores (Objecto)"));
            }

           var categoria = await _categoriaRepository.GetByIdAsync(id.Value);
            if (categoria == null)
            {
                return NotFound(new CodeErrorResponse(404));
            }


            return Ok(categoria);
        }
    }
}
