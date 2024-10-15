﻿using CORE.DTOs;
using DB.Data;
using DB.Models;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Persicuf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _servicio;

        public UsuarioController(IUsuarioServicio servicio)
        {
            _servicio = servicio;
        }

        [HttpPut("modificarUsuario")]
        public async Task<ActionResult<Confirmacion<UsuarioDTO>>> modificarUsuario(int ID, UsuarioDTO usuarioDTO)
        {
            var respuesta = await _servicio.PutUsuario(ID, usuarioDTO);
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

        [HttpPost("crearUsuario")]
        public async Task<ActionResult<Confirmacion<UsuarioDTO>>> crearUsuario(UsuarioDTO usuarioDTO)
        {
            var respuesta = await _servicio.PostUsuario(usuarioDTO);
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


        [HttpGet("obtenerUsuarios")]
        public async Task<ActionResult<Confirmacion<ICollection<UsuarioDTOconID>>>> obtenerUsuarios()
        {
            var respuesta = await _servicio.GetUsuario();
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

        [HttpDelete("eliminarUsuario")]
        public async Task<ActionResult<Confirmacion<Usuario>>> eliminarUsuario(int ID)
        {
            var respuesta = await _servicio.DeleteUsuario(ID);
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

        [HttpPatch("modificarPermisoUsuario")]
        public async Task<ActionResult<Confirmacion<UsuarioDTOconID>>> modificarUsuarioRol(int ID, int PermisoID)
        {
            var respuesta = await _servicio.PatchUsuarioPermiso(ID, PermisoID);
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