using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Exam
{
    public partial class FrmVideoJuego : Form
    {
        int Id = 0;
        public FrmVideoJuego()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AgregarJuego();
            MostrarJuego();
        }

        private void MostrarJuego()
        {
            using (var context = new AplicacionDbConntext())
            {
                var Video = context.VideoJuego.ToList();
                dgvTabla.DataSource = Video;
            }
        }

        private void AgregarJuego()
        {
            using (var context = new AplicacionDbConntext())
            {
                //Paso 1: Creamos el objeto
                var Juego = new Video();
                Juego.Id = Id;
                Juego.Nombre = txtNombre.Text;
                Juego.Genero = txtGenero.Text;
                Juego.Precio = txtPrecio.Text;
                Juego.Fecha = dtFecha.Value.Date;

                //Paso 2: Notificamos que queremos agregar un empleado
                context.VideoJuego.Add(Juego);

                //Paso 3: Guardamos los cambios
                context.SaveChanges();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (var context = new AplicacionDbConntext())
            {
                if (Id != 0)
                {
                    //Busqueda con un ORM
                    var p = context.VideoJuego.First(x => x.Id == Id);
                    if (p != null)
                    {
                        context.Remove(p);
                        context.SaveChanges();
                    }
                }
            }
        }

        private void dgvTabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = Convert.ToInt32(dgvTabla.CurrentRow.Cells[0].Value.ToString());
            txtNombre.Text = dgvTabla.CurrentRow.Cells[1].Value.ToString();
            txtGenero.Text = dgvTabla.CurrentRow.Cells[2].Value.ToString();
            txtPrecio.Text = dgvTabla.CurrentRow.Cells[3].Value.ToString();
            dtFecha.Value = Convert.ToDateTime(dgvTabla.CurrentRow.Cells[4].Value.ToString());

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            using (var context = new AplicacionDbConntext())
            {
                if (Id != 0)
                {
                    //Busqueda con un ORM
                    var peli = context.VideoJuego.First(x => x.Id == Id);
                    if (peli != null)
                    {
                        peli.Nombre = txtNombre.Text;
                        peli.Genero = txtGenero.Text;
                        peli.Precio = txtPrecio.Text;
                        peli.Fecha = dtFecha.Value.Date;
                        context.SaveChanges();
                    }
                }
            }
        }

        private void FrmVideoJuego_Load(object sender, EventArgs e)
        {
            MostrarJuego();
        }

        private void txtxBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        private void BuscarNombre()
        {
            using (var context = new AplicacionDbConntext())
            {
                var video = context.VideoJuego.Where(x => x.Nombre.Contains(txtxBuscar.Text)).ToList();
                dgvTabla.DataSource = video;
            }
        }

    }
}
