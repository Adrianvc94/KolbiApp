using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KolbiApp.Modelos
{
    public class Productos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public String Name { get; set; }
        [MaxLength(100)]
        public String Price { get; set; }
        [MaxLength(500)]
        public String Imagen { get; set; }
        [MaxLength(50)]
        public String Plan { get; set; }
        [MaxLength(50)]
        public String Memoria { get; set; }
        [MaxLength(50)]
        public String Color { get; set; }
        [MaxLength(50)]
        public DateTime Fecha { get; set; }
        [MaxLength(50)]
        public String Estado { get; set; }
    }
}
