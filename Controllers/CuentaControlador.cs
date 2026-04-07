using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PruebaFuego.Models;
using System.Data;
using System.Runtime.CompilerServices;

namespace PruebaFuego.Controllers
{
    public class CuentaControlador : Controller
    {
        string cn = "Server=Localhost;Database=prueba;User Id=root;Password=;";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Bienvenida()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string Username, string Clave)
        {
            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                MySqlCommand cmd = new MySqlCommand("login", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_usuario", Username);
                cmd.Parameters.AddWithValue("p_clave", Clave);

                conectar.Open();

                var revisa = cmd.ExecuteReader();

                if (revisa.HasRows)
                {
                    return RedirectToAction("Bienvenida");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
        }

        [HttpPost]
        public IActionResult Registro(int id, string Username, string Clave, string Nombres, string Apellidos, string Dni, string Telefono, string Correo)
        {
            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                MySqlCommand cmd = new MySqlCommand("registro", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("r_id", id);
                cmd.Parameters.AddWithValue("r_username", Username);
                cmd.Parameters.AddWithValue("r_clave", Clave);
                cmd.Parameters.AddWithValue("r_nombres", Nombres);
                cmd.Parameters.AddWithValue("r_apellidos", Apellidos);
                cmd.Parameters.AddWithValue("r_dni", Dni);
                cmd.Parameters.AddWithValue("r_telefono", Telefono);
                cmd.Parameters.AddWithValue("r_correo", Correo);

                try
                {
                    conectar.Open();

                    int insertardato = cmd.ExecuteNonQuery();

                    if(insertardato > 0)
                    {
                        
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
        }

        [HttpPost]
        public IActionResult Mostrar(int id)
        {
            Usuario persona = new Usuario();
            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                MySqlCommand cmd = new MySqlCommand("mostrar", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("m_id", id);

                try
                {
                    conectar.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            persona.Nombres = dr["nombres"].ToString();
                            persona.Apellidos = dr["apellidos"].ToString();
                            persona.Dni = dr["dni"].ToString();
                            persona.Telefono = dr["telefono"].ToString();
                            persona.Correo = dr["correo"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }

            }
            return View(persona);
        }
   
    }
}
