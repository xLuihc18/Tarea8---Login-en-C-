using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PruebaFuego.Models;
using System.Data;

namespace PruebaFuego.Controllers
{
    public class CuentaControlador : Controller
    {
        string cn = "Server=localhost;Database=lprueba;User Id=root;Password=;";

        [HttpGet] public IActionResult Login() => View();
        [HttpGet] public IActionResult Registro() => View();
        [HttpGet] public IActionResult Bienvenida() => View();
        [HttpGet] public IActionResult Error() => View();
        [HttpGet]public IActionResult CambiarClave() => View();

        [HttpPost]
        public IActionResult Login(string Username, string Clave)
        {
            Usuario usuarioLogeado = null;

            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                MySqlCommand cmd = new MySqlCommand("login", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_usuario", Username);
                cmd.Parameters.AddWithValue("p_clave", Clave);

                conectar.Open();

                using (var revisa = cmd.ExecuteReader())
                {
                    if (revisa.Read())
                    {
                        usuarioLogeado = new Usuario
                        {
                            Username = revisa["username"].ToString(),
                            Nombres = revisa["nombres"].ToString(),
                            Apellidos = revisa["apellidos"].ToString(),
                            Dni = revisa["dni"].ToString(),
                            Telefono = revisa["telefono"].ToString(),
                            Correo = revisa["correo"].ToString()
                        };

                        return View("Bienvenida", usuarioLogeado);
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult Registro(Usuario u)
        {
            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                MySqlCommand cmd = new MySqlCommand("registro", conectar);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("r_username", u.Username);
                cmd.Parameters.AddWithValue("r_nombres", u.Nombres);
                cmd.Parameters.AddWithValue("r_apellidos", u.Apellidos);
                cmd.Parameters.AddWithValue("r_dni", u.Dni);
                cmd.Parameters.AddWithValue("r_clave", u.Clave);
                cmd.Parameters.AddWithValue("r_telefono", u.Telefono);
                cmd.Parameters.AddWithValue("r_correo", u.Correo);

                try
                {
                    conectar.Open();
                    cmd.ExecuteNonQuery();
                    return RedirectToAction("Login");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
        }

        [HttpPost]
        public IActionResult CambiarClave(string Username, string NuevaClave)
        {
            using (MySqlConnection conectar = new MySqlConnection(cn))
            {
                string sql = "UPDATE usuario SET clave = @clave WHERE username = @user";
                MySqlCommand cmd = new MySqlCommand(sql, conectar);

                cmd.Parameters.AddWithValue("@clave", NuevaClave);
                cmd.Parameters.AddWithValue("@user", Username);

                try
                {
                    conectar.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
                catch
                {
                    return RedirectToAction("Error");
                }
            }
        }
    }
}