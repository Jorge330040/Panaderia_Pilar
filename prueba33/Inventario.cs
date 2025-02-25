using SQLite;
using System;

namespace prueba33.Models
{
    public class Inventario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Material { get; set; }
        public int? Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Fecha { get; set; } // Guardar la fecha completa
        public Guid TableId { get; set; }  // Identificador único para cada tabla
    }
}