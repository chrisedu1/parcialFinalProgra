using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario
{
    public class ArticuloDAO
    {
        private string connectionString = "server=sql5.freesqldatabase.com;" +
            "user=sql5712512;" +
            "database=sql5712512;" +
            "port=3306;" +
            "password=rUpYP1VwYa";

        public ArticuloDAO()
        {

        }

        public void Create(Form1 articulo)
        {
            //La palabra conection debia escribirse "connection"
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO articulos (nombre, descripcion, precio, imagen) VALUES (@nombre, @descripcion, @precio, @imagen)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                    cmd.Parameters.AddWithValue("@imagen", articulo.Imagen);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Form1 read(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM articulos WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Form1
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Descripcion = reader.GetString("descripcion"),
                                Precio = reader.GetDecimal("precio"),
                                Imagen = reader["image"] as byte[]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Form1> ReadAll()
        {
            List<Form1> articulos = new List<Form1>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM articulos";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articulos.Add(new Form1
                            {
                                Id = reader.GetInt32("id"),
                                Nombre = reader.GetString("nombre"),
                                Descripcion = reader.GetString("descripcion"),
                                Precio = reader.GetDecimal("precio"),
                                Imagen = reader["imagen"] as byte[]
                            });
                        }
                    }
                }
            }
            return articulos;
        }

        public void Update(Form1 articulo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE articulo SET nombre = @nombre, descripcion = @descripcion, precio = @precio, imagen = @imagen WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", articulo.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", articulo.Precio);
                    cmd.Parameters.AddWithValue("@image", articulo.Imagen);
                    cmd.Parameters.AddWithValue("@id", articulo.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE  articulos WHERE id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
