using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.persistence.Manages
{

    class ProfesorManage
    {       
        private ObservableCollection<Profesor> listaUsuarios { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public ProfesorManage()
        {         
            listaUsuarios = new ObservableCollection<Profesor>();
        }

        public ObservableCollection<Profesor> LeerProfesoresPorCentro(int id_centro)
        {
            ObservableCollection<Profesor> profesoresPorCentro = new ObservableCollection<Profesor>();

            // Consulta SQL para obtener los profesores por id_centro
            string query = "SELECT id_profesor, id_rol, id_centro, id_familia, nombre, apellidos, email, password, sexo, first_char, bgColor FROM fpc.profesores WHERE id_centro = @id_centro;";

            // Parámetros para la consulta
            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro }
            };

            // Ejecutar la consulta
            var resultado = db.LeerConParametros(query, parametros);

            // Procesar los resultados
            foreach (ObservableCollection<object> fila in resultado)
            {
                try
                {
                    Profesor profesor = new Profesor(
                        int.Parse(fila[0].ToString()), // id_profesor
                        int.Parse(fila[1].ToString()), // id_rol
                        int.Parse(fila[2].ToString()), // id_centro
                        int.Parse(fila[3].ToString()), // id_familia
                        fila[4].ToString().Trim(), // nombre
                        fila[5].ToString().Trim(), // apellidos
                        fila[6].ToString().Trim(), // email
                        fila[7].ToString().Trim(), // password
                        fila[8].ToString().Trim(), // sexo
                        fila[9].ToString().Trim(), // character (primera letra del nombre)
                        fila[10].ToString().Trim() // bgColor
                    );

                    profesoresPorCentro.Add(profesor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el profesor: {ex.Message}");
                }
            }

            return profesoresPorCentro;
        }



        public Profesor autentificarUsuario(string correo, string password)
        {           
            string passwordEncrypted = Seguridad.EncriptarContraseña(password);

            string query = "SELECT id_profesor,id_rol,id_centro,id_familia, nombre,apellidos, email, password,sexo FROM fpc.profesores WHERE email = @email AND password = @password LIMIT 1;";
            var parametros = new Dictionary<string, object>
            {
                { "@email", correo }, 
                { "@password", passwordEncrypted }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                
                var fila = resultado[0] as ObservableCollection<object>; 

                
                Profesor profesor = new Profesor
                {
                    id_profesor = Convert.ToInt32(fila[0]),
                    id_rol = Convert.ToInt32(fila[1]),
                    id_centro = Convert.ToInt32(fila[2]),
                    id_familia = Convert.ToInt32(fila[3]),
                    nombre = fila[4].ToString(),
                    apellidos = fila[5].ToString(),
                    email = fila[6].ToString(), 
                    password = fila[7].ToString(),
                    sexo = fila[8].ToString(),
                    
                };

                Console.WriteLine("Inicio de sesión exitoso.");
                return profesor;
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas.");
                return null; // usuario no encontrado
            }
        }

        public void InsertarProfesor(Profesor profesor, int[] grados) 
        {
            string query = "INSERT INTO fpc.profesores (id_rol,id_centro,id_familia,nombre,apellidos,email,password,sexo,first_char,bgColor) VALUES (@id_rol,@id_centro,@id_familia,@nombre,@apellidos,@email,@password,@sexo,@first_char,@bgColor);";
            var parametros = new Dictionary<string, object>
            {
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@password", Seguridad.EncriptarContraseña(profesor.password) }, // Encriptar la contraseña
                { "@sexo", profesor.sexo },
                { "@first_char", profesor.character },
                { "@bgColor", Colores.GetRandomColor() } 
            };

            db.Modificar(query, parametros);

            string queryGrados = "INSERT INTO fpc.profesoresgrados (id_profesor,id_grado) VALUES (@id_profesor,@id_grado);";
            int id_grado = 0; // Inicializar id_grado

            for(int i = 0; i < grados.Length; i++)
            {
                if(grados[i] != 0) 
                {
                    id_grado = grados[i];
                    var parametrosGrados = new Dictionary<string, object>
                    {
                        { "@id_profesor", db.LeerUltimoIdInsertado() }, 
                        { "@id_grado", id_grado}
                    };
                    db.Modificar(queryGrados, parametrosGrados);
                }
            }              

        }

        public void ModificarProfesor(Profesor profesor, int[] grados)
        {
            string query = "UPDATE fpc.profesores SET id_rol = @id_rol, id_centro = @id_centro, id_familia = @id_familia, nombre = @nombre, apellidos = @apellidos, email = @email, password = @password, sexo = @sexo WHERE id_profesor = @id_profesor;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", profesor.id_profesor },
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@password", Seguridad.EncriptarContraseña(profesor.password) }, 
                { "@sexo", profesor.sexo }
            };

            db.Modificar(query, parametros);

            string queryGrados = "UPDATE fpc.profesoresgrados SET id_grado = @id_grado WHERE id_profesor = @id_profesor;";
            int id_grado = 0; // Inicializar id_grado
            for (int i = 0; i < grados.Length; i++)
            {
                if (grados[i] != 0)
                {
                    id_grado = grados[i];
                    var parametrosGrados = new Dictionary<string, object>
                    {
                        { "@id_profesor", db.LeerUltimoIdInsertado() },
                        { "@id_grado", id_grado }
                    };
                    db.Modificar(queryGrados, parametrosGrados);
                }
            }

        }

        public void BorrarProfesor(Profesor profesor)
        {
            string query = "DELETE FROM fpc.profesores WHERE id_profesor = @id_profesor;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", profesor.id_profesor}
            };
            db.Modificar(query, parametros);
        }
    }
}
