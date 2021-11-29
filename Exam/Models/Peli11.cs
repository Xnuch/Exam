using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Exam.Models
{
    class Peli11
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public string Precio { get; set; }
        public DateTime Fecha { get; set; }

    }
}
