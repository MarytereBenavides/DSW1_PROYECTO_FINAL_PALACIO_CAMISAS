using DSW_PROYECTO_PALACIO_CAMISAS_API.Data.Contrato;
using DSW_PROYECTO_PALACIO_CAMISAS_API.Models;
using Microsoft.Data.SqlClient;

namespace DSW_PROYECTO_PALACIO_CAMISAS_API.Data
{
    public class VentaRepositorio : IVenta
    {
        private readonly string cadenaConexion;
        private readonly IConfiguration _config;

        public VentaRepositorio(IConfiguration config)
        {
            _config = config;
            cadenaConexion = _config["ConnectionStrings:DB"];
        }

        public List<Venta> Listado()
        {
            var listado = new List<Venta>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarVentas", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listado.Add(new Venta()
                            {
                                id_venta = reader.GetInt32(0),
                                nombre_cliente = reader.GetString(1),
                                dni_cliente = reader.GetString(2),
                                tipo_pago = reader.GetString(3),
                                fecha = reader.GetDateTime(4),
                                precio_total = reader.GetDecimal(5),
                                estado = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return listado;
        }

        public Venta ObtenerPorID(int id)
        {
            Venta venta = null;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ObtenerVentaPorID", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_venta", id);
                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            venta = new Venta()
                            {
                                id_venta = reader.GetInt32(0),
                                nombre_cliente = reader.GetString(1),
                                dni_cliente = reader.GetString(2),
                                tipo_pago = reader.GetString(3),
                                fecha = reader.GetDateTime(4),
                                precio_total = reader.GetDecimal(5),
                                estado = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return venta;
        }

        public Venta Registrar(Venta venta, List<DetalleVenta> detalles)
        {
            Venta nuevaVenta = null;
            int nuevoId = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        // Insertar venta
                        using (var comando = new SqlCommand("RegistrarVenta", conexion, transaccion))
                        {
                            comando.CommandType = System.Data.CommandType.StoredProcedure;
                            comando.Parameters.AddWithValue("@nombre_cliente", venta.nombre_cliente);
                            comando.Parameters.AddWithValue("@dni_cliente", venta.dni_cliente);
                            comando.Parameters.AddWithValue("@tipo_pago", venta.tipo_pago);
                            comando.Parameters.AddWithValue("@fecha", venta.fecha);
                            comando.Parameters.AddWithValue("@precio_total", venta.precio_total);
                            comando.Parameters.AddWithValue("@estado", venta.estado);
                            nuevoId = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        // Insertar detalles
                        foreach (var detalle in detalles)
                        {
                            using (var comando = new SqlCommand("RegistrarDetalleVenta", conexion, transaccion))
                            {
                                comando.CommandType = System.Data.CommandType.StoredProcedure;
                                comando.Parameters.AddWithValue("@id_venta", nuevoId);
                                comando.Parameters.AddWithValue("@id_camisa", detalle.id_camisa);
                                comando.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                                comando.Parameters.AddWithValue("@precio", detalle.precio);
                                comando.Parameters.AddWithValue("@estado", detalle.estado);
                                comando.ExecuteNonQuery();
                            }
                        }

                        transaccion.Commit();
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
            nuevaVenta = ObtenerPorID(nuevoId);
            return nuevaVenta;
        }
    }
}
