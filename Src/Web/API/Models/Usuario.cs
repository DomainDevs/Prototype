namespace API.Models
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string StrUsuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public static List<Usuario> DB()
        {
            var List = new List<Usuario>()
            {
                new Usuario(){
                    IdUsuario = "1",
                    StrUsuario = "Mateo",
                    Password = "12345",
                    Rol = "Empleado"

                },
                new Usuario(){
                    IdUsuario = "2",
                    StrUsuario = "Juan",
                    Password = "12345",
                    Rol = "Administrador"

                },
                new Usuario(){
                    IdUsuario = "3",
                    StrUsuario = "Lucas",
                    Password = "12345",
                    Rol = "Empleado"

                }
            };

            return List;
        }

    }
}
