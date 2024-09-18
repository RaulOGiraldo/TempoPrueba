using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TempoPrueba.Api.Responses;
using TempoPrueba.Core.DTOs;
using TempoPrueba.Core.Entities;
using TempoPrueba.Core.Helpers;
using TempoPrueba.Core.Interfaces;
using TempoPrueba.Core.QueryFilters;

namespace TempoPrueba.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        /// <summary>
        /// Obtinene todos los registros de Proveedores
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProveedorQueryFilter filters)
        {
            var pasos = await _proveedorService.GetAll(filters);
            if (pasos == null) { return NotFound(); }

            var response = new ApiResponse<IEnumerable<Proveedor>>(pasos);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un registro mediante el Nit
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string Id)
        {
            var paso = await _proveedorService.Get(Id);
            if (paso == null)
            {
                return NotFound();
            }
            var response = new ApiResponse<Proveedor>(paso);
            return Ok(response);
        }

        /// <summary>
        /// Inserta un registro de Proveedores
        /// </summary>
        /// <param name="tbProveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(Proveedor tbProveedor)
        {
            string menx = Tools.Constantes.FAIL_INSERT_MESSAGE;
            var respx = await _proveedorService.Insert(tbProveedor);
            if (respx) { menx = Tools.Constantes.SUCCESS_INSERT_MESSAGE; }
            var response = new ApiResponse<string>(menx);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un registro mediante el Nit
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="tbProveedor"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string Id, ProveedorDTO tbProveedor)
        {
            string menx = Tools.Constantes.FAIL_UPDATE_MESSAGE;
            var flu = await _proveedorService.Get(Id);
            if (flu == null) { return NotFound(); }

            //tbProveedor.Id = Id;
            var respx = await _proveedorService.Update(Id, tbProveedor);
            if (respx) { menx = Tools.Constantes.SUCCESS_UPDATE_MESSAGE; }
            var response = new ApiResponse<string>(menx);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un registro mediante el Nit
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            string menx = Tools.Constantes.FAIL_DELETE_MESSAGE;
            var flu = await _proveedorService.Get(Id);
            if (flu == null) { return NotFound(); }

            var respx = await _proveedorService.Delete(Id);
            if (respx) { menx = Tools.Constantes.SUCCESS_DELETE_MESSAGE; }
            var response = new ApiResponse<string>(menx);
            return Ok(response);
        }
    }
}
