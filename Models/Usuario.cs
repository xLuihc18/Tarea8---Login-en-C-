using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Schema;
using System.Data;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using MySql.Data.MySqlClient;

namespace PruebaFuego.Models
{
    public class Usuario
    {
        public string Username { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

    }
}


