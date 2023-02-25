using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using WebApiPractica.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;   
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {//t_equipos =  tabla
            List<equipos>mi_lista = (from e in _equiposContexto.t_equipos select e).ToList();
            if (mi_lista.Count == 0 )
            {
                return NotFound();
            }
            else
            {
                return Ok(mi_lista);
            }
        }

    }
}
