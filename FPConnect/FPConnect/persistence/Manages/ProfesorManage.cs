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
            int activo = 1;
            ObservableCollection<Profesor> profesoresPorCentro = new ObservableCollection<Profesor>();
            db = DBBroker.ObtenerAgente();

            // Consulta SQL para obtener los profesores por id_centro con el nombre de su familia profesional (departamento)
            string query = @"SELECT p.id_profesor, p.id_rol, p.id_centro, p.id_familia, p.id_curso, p.id_turno,
                    p.nombre, p.apellidos, p.email, p.password, p.sexo, p.first_char, 
                    p.bgColor, p.activo, f.nombre AS nombre_departamento
                    FROM fpc.profesores p
                    INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                    WHERE p.id_centro = @id_centro AND p.activo = @activo;";

            // Parámetros para la consulta
            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro },
                { "@activo", 1 }  // solo los profesores activos
            };

            // Ejecutar la consulta
            var resultado = db.LeerConParametros(query, parametros);

            // Procesar los resultados
            foreach (ObservableCollection<object> fila in resultado)
            {
                try
                {
                    Profesor profesor = new Profesor(
                        int.Parse(fila[0].ToString()),      // id_profesor
                        int.Parse(fila[1].ToString()),      // id_rol
                        int.Parse(fila[2].ToString()),      // id_centro
                        int.Parse(fila[3].ToString()),      // id_familia
                        int.Parse(fila[4].ToString()),      // id_curso
                        int.Parse(fila[5].ToString()),      // id_turno
                        fila[6].ToString().Trim(),          // nombre
                        fila[7].ToString().Trim(),          // apellidos
                        fila[8].ToString().Trim(),          // email
                        fila[9].ToString().Trim(),          // password
                        fila[10].ToString().Trim(),         // sexo
                        fila[11].ToString().Trim(),         // first_char
                        fila[12].ToString().Trim(),         // bgColor
                        int.Parse(fila[13].ToString()),     // activo                     
                        fila[14].ToString().Trim()          // nombre_departamento
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
            int activo = 1; // Solo vna a poder logearse usuarios activos
            string query = "SELECT id_profesor,id_rol,id_centro,id_familia, nombre,apellidos, email, password,sexo FROM fpc.profesores WHERE email = @email AND activo = @activo AND password = @password LIMIT 1;";
            var parametros = new Dictionary<string, object>
            {
                { "@email", correo }, 
                { "@password", passwordEncrypted },
                { "@activo", activo } 
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

        public void InsertarProfesor(Profesor profesor, int id_grado, int id_curso, int id_perfil) 
        {
            string query = "INSERT INTO fpc.profesores (id_rol,id_centro,id_familia,id_turno,nombre,apellidos,email,password,sexo,first_char,bgColor,activo) VALUES (@id_rol,@id_centro,@id_familia,@id_turno,@nombre,@apellidos,@email,@password,@sexo,@first_char,@bgColor,@activo);";
            var parametros = new Dictionary<string, object>
            {
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@id_turno", profesor.id_turno },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@password", Seguridad.EncriptarContraseña(profesor.password) }, // Encriptar la contraseña
                { "@sexo", profesor.sexo },
                { "@first_char", profesor.character },
                { "@bgColor", Colores.GetRandomColor() },
                { "@activo", 1 } // Pongo 1 por defecto ya que cuando creo un usuario esta activo
            };

            db.Modificar(query, parametros);

            string queryGrados = "INSERT INTO fpc.profesorescursos (id_profesor,id_curso,id_grado,id_perfil) VALUES (@id_profesor,@id_curso,@id_grado,@id_perfil);";

            var parametrosGrados = new Dictionary<string, object>
            {
                { "@id_profesor", db.LeerUltimoIdInsertado() },
                { "@id_curso", id_curso }, // ver que curso meter
                { "@id_grado", id_grado},
                { "@id_perfil", id_perfil }
            };
            db.Modificar(queryGrados, parametrosGrados);

        }

        public void ModificarProfesor(Profesor profesor, int[] grados)
        {
            string query = "UPDATE fpc.profesores SET id_rol = @id_rol, id_centro = @id_centro, id_familia = @id_familia,id_turno = @id_turno, nombre = @nombre, apellidos = @apellidos, email = @email, sexo = @sexo WHERE id_profesor = @id_profesor;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", profesor.id_profesor },
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@id_turno", profesor.id_turno },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@sexo", profesor.sexo }
            };

            db.Modificar(query, parametros);

            string queryGrados = "UPDATE fpc.profesoresgrados SET id_grado = @id_grado WHERE id_profesor = @id_profesor;";
            int id_grado = 0; 
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

        public void BorrarProfesor(int id_profesor)
        {
            // Borrado logico, modificar profesor y poner activo en 0
            string query = "UPDATE fpc.profesores SET activo = @activo WHERE id_profesor = @id_profesor;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", id_profesor },
                { "@activo", 0 } // Cambiar el activo a 0 para borrado lógico
            };
            db.Modificar(query, parametros);

        }
    }
}
