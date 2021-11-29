using Exam.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Exam
{
    public partial class FrmPelicula : Form
    {
        int Id = 0;
        public FrmPelicula()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            AgregarPelicula();
            MostrarPelicula();
        }

        private void MostrarPelicula()
        {
            using (var context = new AplicacionDbConntext())
            {
                var peli = context.Peli11.ToList();
                dgvTabla.DataSource = peli;
            }

        }

        private void AgregarPelicula()
        {
            using (var context = new AplicacionDbConntext())
            {
                //Paso 1: Creamos el objeto
                var peli1 = new Peli11();
                peli1.Id = Id;
                peli1.Nombre = txtNombre.Text;
                peli1.Genero = txtGenero.Text;
                peli1.Precio = txtPrecio.Text;
                peli1.Fecha = dtFecha.Value.Date;

                //Paso 2: Notificamos que queremos agregar un empleado
                context.Peli11.Add(peli1);

                //Paso 3: Guardamos los cambios
                context.SaveChanges();
            }

        }

        private void FrmPelicula_Load(object sender, EventArgs e)
        {
            MostrarPelicula();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (var context = new AplicacionDbConntext())
            {
                if (Id != 0)
                {
                    //Busqueda con un ORM
                    var p = context.Peli11.First(x => x.Id == Id);
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
                    var peli = context.Peli11.First(x => x.Id == Id);
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        private void BuscarNombre()
        {
            using (var context = new AplicacionDbConntext())
            {
                var peli = context.Peli11.Where(x => x.Nombre.Contains(txtBuscar.Text)).ToList();
                dgvTabla.DataSource = peli;
            }
        }
    }
}
