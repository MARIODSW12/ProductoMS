
namespace Producto.Domain.Exceptions
{
        public class ProductoNoEncontradoException : Exception
        {
            public ProductoNoEncontradoException() : base("Este producto no existe.") { }
        }
        public class CantidadInvalidaException : Exception
        {
            public CantidadInvalidaException() : base("La cantidad del producto debe ser mayor a cero.") { }
        }
        public class ConexionBdProductoInvalida : Exception
        {
            public ConexionBdProductoInvalida() : base("La cadena de conexión de MongoDB no está definida.") { }
        }
        public class NombreBdInvalido : Exception
        {
            public NombreBdInvalido() : base("El nombre de la base de datos de MongoDB no está definido.") { }
        }
        public class ErrorConexionBd : Exception
        {
            public ErrorConexionBd() : base("No se pudo conectar a la base de datos.") { }
        }
        public class ErrorGuardadoBd : Exception
        {
            public ErrorGuardadoBd() : base("No se pudo guardar el producto en la base de datos.") { }
        }
        public class ProductoNoModificadoException : Exception
        {
            public ProductoNoModificadoException() : base("No se logro modificar este producto.") { }
        }
        public class ProductoNoEliminadoException : Exception
        {
            public ProductoNoEliminadoException() : base("No se logro eliminar este producto.") { }
        }
}
