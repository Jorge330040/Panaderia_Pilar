using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace prueba33
{
    public class Ganancia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Producto { get; set; }
        public decimal PrecioPorUnidad { get; set; }
        public int CantidadVendida { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}