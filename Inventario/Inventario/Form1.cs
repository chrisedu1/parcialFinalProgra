using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario
{
    public partial class Form1 : Form
    {
        public int Id { get; internal set; }

        public object Nombre { get; internal set; }
        public object Descripcion { get; internal set; }
        public object Precio { get; internal set; }
        public object Imagen { get; internal set; }


        ArticuloDAO articuloDAO = new ArticuloDAO();
        public Form1()
        {
            InitializeComponent();
        }

        private void listarArticulos()
        {
            //articulo DAO estaba escrito como "articulodAO", asi mismo dgvArticulo estaba escrito como dgvArticulos.
            dgvArticulo.DataSource = articuloDAO.ReadAll();
            dgvArticulo.Columns["image"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Todos los archivos (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtiene la ruta completa del archivo seleccionado

                    // Carga la imagen en el PictureBox
                }
            }
        }

        private void guardarNuevo()
        {
            Form1 articulo = new Form1
            {
                Nombre = txtNombre.Text,
                //txtDescripcion estaba escrito como "TxtDEscripcion"
                Descripcion = txtDescripcion.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Imagen = null
            };

            articuloDAO.Create(articulo);
        }

        private byte[] obtenerImagen()
        {
            if (pcbImagen.Image == null)
            {
                MessageBox.Show("Por favor, selecciona una imagen.");
                return null;
            }
            byte[] imagenBytes;
            using (var ms = new System.IO.MemoryStream())
            {
                pcbImagen.Image.Save(ms, pcbImagen.Image.RawFormat);
                imagenBytes = ms.ToArray();
            }
            return imagenBytes;
        }
    }
}
