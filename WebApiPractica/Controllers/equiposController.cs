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
        ////////////////////////////////////////////////////////////
        //selecciona todo de la tabla
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {//t_equipos =  tabla
            List<equipos>mi_lista = (from e in _equiposContexto.t_equipos
                                     where e.estado =="A"
                                     select e).ToList();

            if (mi_lista.Count == 0 ){
                return NotFound();
            }
            else{
                return Ok(mi_lista);
            }
        }
        ////////////////////////////////////////////////////////////
        //selecciona segun el id especificado.
        [HttpGet]
        [Route("getbyid/{id}")]
        //si modificamos ruta:
        //LOCALHOST:4455/api/equipos/getbyid/23/pwa
        public IActionResult get2(int id)
        {
            //hay 2 tipos de parametros 1. URL 2.Routing? 

            equipos? varEquipos = (from e in _equiposContexto.t_equipos
                           where e.id_equipos == id && e.estado =="A"
                           select e).FirstOrDefault();

            if(varEquipos == null) {
                return NotFound();
            }
            else
            {
                return Ok(varEquipos);
            }

            
        }
        ////////////////////////////////////////////////////////////
        // consulta con filtro especifico nombre o cualquier cosa
        //selecciona segun el filtro especificado especificado. en las columnas nombre y descripcion
        [HttpGet]
        [Route("find")]
        public IActionResult burcar(string filtro) {
            List<equipos>equipos_lista = (from e in _equiposContexto.t_equipos
                    where (e.nombre.Contains(filtro)
                    || e.descripcion.Contains(filtro))
                    && e.estado == "A"
                    select e).ToList();

            //mas rapido y mejor para no evaluar todos los registros
            if (equipos_lista.Any()){ 
                return Ok(equipos_lista);
            }
            else
            {
                return NotFound();
            }
        }
        ////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] equipos equipo_nuevo)
        {
            try{
                equipo_nuevo.estado = "A";
                _equiposContexto.t_equipos.Add(equipo_nuevo);
                _equiposContexto.SaveChanges();
                return Ok(equipo_nuevo);
            }
            catch(Exception ex){
              return BadRequest(ex.Message);
            }
        }
        ////////////////////////////////////////////////////////////
        [HttpPut]
        [Route("actualizar")]
        public IActionResult actualizarEquipo(int id, [FromBody]equipos eqmodificar)
        {
            equipos? equipoExiste = (from e in _equiposContexto.t_equipos
                                    where e.id_equipos == id
                                    select e).FirstOrDefault();

            if (equipoExiste == null) return NotFound();

            //solo tiene permisos de cambiar estos datos 
            //se pueden poner los demas parametros de la tabla 
            equipoExiste.nombre = eqmodificar.nombre;
            equipoExiste.descripcion = eqmodificar.descripcion;

            _equiposContexto.Entry(equipoExiste).State = EntityState.Modified; 
            _equiposContexto.SaveChanges();
            return Ok(equipoExiste);
        }
        ////////////////////////////////////////////////////////////
        ///metodo para modificar estado de elimindo
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Eliminar(int id) {
            equipos? equipoExiste = (from e in _equiposContexto.t_equipos
                                     where e.id_equipos == id
                                     select e).FirstOrDefault();

            if (equipoExiste == null) return NotFound();

            equipoExiste.estado = "E";
            _equiposContexto.SaveChanges(); 
            return Ok(equipoExiste);
        }
    }
}
