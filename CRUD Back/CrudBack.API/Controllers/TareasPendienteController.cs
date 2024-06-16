using CrudBack.Business.TareasPendientes;
using CrudBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CrudBack.API.Controllers
{
    [Route("api/tareas")]
    [ApiController]
    public class TareasPendienteController : ControllerBase
    {
        private readonly ITareasPendientesService _tareaService;

        public TareasPendienteController(ITareasPendientesService tareaService)
        {
            _tareaService = tareaService;
        }

        [EnableCors]
        [HttpGet("listar")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _tareaService.Listar();
            return Ok(tareas);
        }

        [EnableCors]
        [HttpGet("obtener")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var tarea = await _tareaService.Obtener(id);
            return Ok(tarea);
        }

        [EnableCors]
        [HttpPost("guardar")]
        [Authorize]
        public async Task<IActionResult> Insert([FromBody] TareasPendiente model)
        {
            var tarea = await _tareaService.Insertar(model);
            return Ok(tarea);
        }

        [EnableCors]
        [HttpPut("actualizar")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] TareasPendiente model)
        {
            var tarea = await _tareaService.Actualizar(model);
            return Ok(tarea);   
        }

        [EnableCors]
        [HttpDelete("eliminar")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _tareaService.Eliminar(id);
            return Ok(tarea);
        }
    }
}
