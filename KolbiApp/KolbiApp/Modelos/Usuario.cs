using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace KolbiApp.Modelos
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(100)]
        public String Username { get; set; }
        [MaxLength(100)]
        public String Email { get; set; }
        [MaxLength(70)]
        public String Password { get; set; }
        [MaxLength(1)]
        public String Admin { get; set; }
    }
}
