using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Producto.Application.Commands;
using Producto.Application.DTOs;
using Producto.Domain.ValueObjects;
using Producto.Infrastructure.Interfaces;
using Producto.Infrastructure.Queries;
using Producto.Infrastructure.Queries.QueryHandler;
using Producto.Infrastructure.Services;

namespace Producto.API.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController: ControllerBase
    {
        private readonly IMediator Mediator;
        private readonly IPublishEndpoint PublishEndpoint;
        private readonly ICloudinaryService CloudinaryService;

        public ProductoController(IMediator mediator, IPublishEndpoint publishEndpoint, ICloudinaryService cloudinaryService)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            PublishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            CloudinaryService = cloudinaryService;
        }

        #region GetProductoPorId
        [HttpGet("GetProductoPorId")]
        public async Task<IActionResult> getProductoPorId([FromQuery] string idProducto)
        {
            try
            {
                var Producto = await Mediator.Send(new GetProductoPorIdQuery(idProducto));

                if (Producto == null)
                {
                    return NotFound($"No se encontró un usuario con el email {idProducto}");
                }

                return Ok(Producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region GetProductosPorCategoria
        [HttpGet("GetProductosPorCategoria")]
        public async Task<IActionResult> GetProductosPorCategoria([FromQuery] string categoria)
        {
            try
            {
                var Productos = await Mediator.Send(new GetProductosPorCategoriaQuery(categoria));

                if (Productos == null)
                {
                    return NotFound($"No se encontró un usuario con el email {categoria}");
                }

                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region GetProductosPorIdSubastador
        [HttpGet("GetProductosPorIdSubastador")]
        public async Task<IActionResult> GetProductosPorIdSubastador([FromQuery] string idSubastador)
        {
            try
            {
                var Productos = await Mediator.Send(new GetProductosPorIdSubastadorQuery(idSubastador));

                if (Productos == null)
                {
                    return NotFound($"No se encontró un usuario con el email {idSubastador}");
                }

                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region GetTodosLosProductos
        [HttpGet("GetTodosLosProductos")]
        public async Task<IActionResult> GetTodosLosProductos()
        {
            try
            {
                var Productos = await Mediator.Send(new GetTodosLosProductosQuery());

                if (Productos == null)
                {
                    return NotFound("No se encontró un usuario con el email");
                }

                return Ok(Productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor.");
            }
        }
        #endregion

        #region AgregarProducto
        [HttpPost("AgregarProducto")]
        public async Task<IActionResult> AgregarProducto([FromForm] AgregarProductoDto producto, IFormFile imagen)
        {

            try
            {
                using var stream = imagen.OpenReadStream();
                var url = await CloudinaryService.SubirImagen(stream, imagen.FileName);
                producto.ImagenProducto = url;

                var IdProducto = await Mediator.Send(new AgregarProductoCommand(producto));

                if (IdProducto == null)
                {
                    return BadRequest("No se pudo crear el usuario.");
                }


                return CreatedAtAction(nameof(AgregarProducto), new { id = IdProducto }, new
                {
                    id = IdProducto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor. AGREGAR PRODUCTO");
            }
        }
        #endregion

        #region ActualizarProducto
        [HttpPatch("ActualizarProducto/{idProducto}")]
        public async Task<IActionResult> ActualizarProducto([FromRoute] string idProducto, [FromForm] ActualizarProductoDto producto, IFormFile? nuevaImagen)
        {

            try
            {
                using var stream = nuevaImagen.OpenReadStream();
                var nuevaUrl = await CloudinaryService.SubirImagen(stream, nuevaImagen.FileName);
                producto.ImagenProducto = nuevaUrl;

                var result = await Mediator.Send(new ActualizarProductoCommand(idProducto, producto));

                if (!result)
                {
                    return NotFound("El producto no pudo ser actualizado.");
                }

                return Ok("Producto actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor. ACTUALIZAR PRODUCTO");
            }
        }
        #endregion

        #region EliminarProducto
        [HttpDelete("EliminarProducto/{idProducto}")]
        public async Task<IActionResult> EliminarProducto([FromRoute] string idProducto)
        {
            try
            {
                var result = await Mediator.Send(new EliminarProductoCommand(idProducto));
                if (!result)
                {
                    return NotFound("El producto no pudo ser eliminado.");
                }
                return Ok("Producto eliminado exitosamente.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno en el servidor. ELIMINAR PRODUCTO");
            }
        }
        #endregion

    }
}
