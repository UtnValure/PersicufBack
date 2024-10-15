﻿using CORE.DTOs;
using DB.Data;
using DB.Models;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persicuf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrendaController : ControllerBase
    {
        private readonly IPrendaServicio _servicio;

        public PrendaController(IPrendaServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarPrenda")]
        public async Task<ActionResult<Confirmacion<PrendaDTO>>> modificarPrenda(int ID, PrendaDTO prendaDTO)
        {
            var respuesta = await _servicio.PutPrenda(ID, prendaDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpPost("crearPrenda")]
        public async Task<ActionResult<Confirmacion<PrendaDTO>>> crearPrenda(PrendaDTO prendaDTO)
        {
            var respuesta = await _servicio.PostPrenda(prendaDTO);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return StatusCode(StatusCodes.Status201Created, respuesta);
        }


        [HttpGet("obtenerPrendas")]
        public async Task<ActionResult<Confirmacion<ICollection<PrendaDTOconID>>>> obtenerPrendas()
        {
            var respuesta = await _servicio.GetPrenda();
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpDelete("eliminarPrenda")]
        public async Task<ActionResult<Confirmacion<Prenda>>> eliminarPrenda(int ID)
        {
            var respuesta = await _servicio.DeletePrenda(ID);
            if (respuesta.Datos == null)
            {
                if (respuesta.Mensaje.StartsWith("Error"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, respuesta);
                }
                return NotFound(respuesta);
            }
            return Ok(respuesta);
        }

    }

}