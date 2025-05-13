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
        private DBBroker db;

        public ProfesorManage()
        {         
            listaUsuarios = new ObservableCollection<Profesor>();
        }

        // Método para insertar un profesor
        public bool InsertarProfesor(Profesor profesor)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"INSERT INTO fpc.profesores 
                          (id_rol, id_centro, id_familia, id_curso, nombre, apellidos, 
                          email, password, sexo, first_char, bgColor, activo, id_turno) 
                          VALUES 
                          (@id_rol, @id_centro, @id_familia, @id_curso, @nombre, @apellidos, 
                          @email, @password, @sexo, @first_char, @bgColor, @activo, @id_turno);";

            var parametros = new Dictionary<string, object>
            {
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@id_curso", profesor.id_curso },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@password", profesor.password },
                { "@sexo", profesor.sexo },
                { "@first_char", profesor.character },
                { "@bgColor", profesor.bgColor.ToString() },
                { "@activo", profesor.activo },
                { "@id_turno", profesor.id_turno }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para actualizar un profesor
        public bool ActualizarProfesor(Profesor profesor)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"UPDATE fpc.profesores 
                          SET id_rol = @id_rol, 
                              id_centro = @id_centro, 
                              id_familia = @id_familia, 
                              id_curso = @id_curso,
                              nombre = @nombre, 
                              apellidos = @apellidos, 
                              email = @email, 
                              sexo = @sexo,
                              first_char = @first_char, 
                              bgColor = @bgColor, 
                              activo = @activo, 
                              id_turno = @id_turno 
                          WHERE id_profesor = @id_profesor;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", profesor.id_profesor },
                { "@id_rol", profesor.id_rol },
                { "@id_centro", profesor.id_centro },
                { "@id_familia", profesor.id_familia },
                { "@id_curso", profesor.id_curso },
                { "@nombre", profesor.nombre },
                { "@apellidos", profesor.apellidos },
                { "@email", profesor.email },
                { "@sexo", profesor.sexo },
                { "@first_char", profesor.character },
                { "@bgColor", profesor.bgColor.ToString() },
                { "@activo", profesor.activo },
                { "@id_turno", profesor.id_turno }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para eliminado lógico
        public bool EliminadoLogicoProfesor(int id_profesor)
        {
            db = DBBroker.ObtenerAgente();

            string query = "UPDATE fpc.profesores SET activo = 2 WHERE id_profesor = @id_profesor;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", id_profesor }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para leer todos los profesores activos
        public ObservableCollection<Profesor> LeerProfesores()
        {
            ObservableCollection<Profesor> profesores = new ObservableCollection<Profesor>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT p.id_profesor, p.id_rol, p.id_centro, p.id_familia, p.id_curso, 
                           p.id_turno, p.nombre, p.apellidos, p.email, p.password, p.sexo, 
                           p.first_char, p.bgColor, p.activo, f.nombre AS nombre_departamento 
                           FROM fpc.profesores p
                           INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                           WHERE p.activo = 1;";

            var resultado = db.LeerSinParametros(query);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Profesor profesor = new Profesor(
                    int.Parse(fila[0].ToString()),      // id_profesor
                    int.Parse(fila[1].ToString()),      // id_rol
                    int.Parse(fila[2].ToString()),      // id_centro
                    int.Parse(fila[3].ToString()),      // id_familia
                    int.Parse(fila[4].ToString()),      // id_curso
                    int.Parse(fila[5].ToString()),      // id_turno
                    fila[6].ToString(),                 // nombre
                    fila[7].ToString(),                 // apellidos
                    fila[8].ToString(),                 // email
                    fila[9].ToString(),                 // password
                    fila[10].ToString(),                // sexo
                    fila[11].ToString(),                // first_char
                    fila[12].ToString(),                // bgColor
                    int.Parse(fila[13].ToString()),     // activo
                    fila[14].ToString()                 // nombre_departamento
                );

                profesores.Add(profesor);
            }

            return profesores;
        }

        // Método para leer profesores por centro
        public ObservableCollection<Profesor> LeerProfesoresPorCentro(int id_centro)
        {
            ObservableCollection<Profesor> profesores = new ObservableCollection<Profesor>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT p.id_profesor, p.id_rol, p.id_centro, p.id_familia, p.id_curso, 
                           p.id_turno, p.nombre, p.apellidos, p.email, p.password, p.sexo, 
                           p.first_char, p.bgColor, p.activo, f.nombre AS nombre_departamento 
                           FROM fpc.profesores p
                           INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                           WHERE p.id_centro = @id_centro AND p.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro }
            };

            var resultado = db.LeerConParametros(query, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Profesor profesor = new Profesor(
                    int.Parse(fila[0].ToString()),      // id_profesor
                    int.Parse(fila[1].ToString()),      // id_rol
                    int.Parse(fila[2].ToString()),      // id_centro
                    int.Parse(fila[3].ToString()),      // id_familia
                    int.Parse(fila[4].ToString()),      // id_curso
                    int.Parse(fila[5].ToString()),      // id_turno
                    fila[6].ToString(),                 // nombre
                    fila[7].ToString(),                 // apellidos
                    fila[8].ToString(),                 // email
                    fila[9].ToString(),                 // password
                    fila[10].ToString(),                // sexo
                    fila[11].ToString(),                // first_char
                    fila[12].ToString(),                // bgColor
                    int.Parse(fila[13].ToString()),     // activo
                    fila[14].ToString()                 // nombre_departamento
                );

                profesores.Add(profesor);
            }

            return profesores;
        }

        // Método para leer un profesor por ID
        public Profesor LeerProfesorPorId(int id_profesor)
        {
            Profesor profesor = null;
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT p.id_profesor, p.id_rol, p.id_centro, p.id_familia, p.id_curso, 
                           p.id_turno, p.nombre, p.apellidos, p.email, p.password, p.sexo, 
                           p.first_char, p.bgColor, p.activo, f.nombre AS nombre_departamento 
                           FROM fpc.profesores p
                           INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                           WHERE p.id_profesor = @id_profesor;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_profesor", id_profesor }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;

                profesor = new Profesor(
                    int.Parse(fila[0].ToString()),      // id_profesor
                    int.Parse(fila[1].ToString()),      // id_rol
                    int.Parse(fila[2].ToString()),      // id_centro
                    int.Parse(fila[3].ToString()),      // id_familia
                    int.Parse(fila[4].ToString()),      // id_curso
                    int.Parse(fila[5].ToString()),      // id_turno
                    fila[6].ToString(),                 // nombre
                    fila[7].ToString(),                 // apellidos
                    fila[8].ToString(),                 // email
                    fila[9].ToString(),                 // password
                    fila[10].ToString(),                // sexo
                    fila[11].ToString(),                // first_char
                    fila[12].ToString(),                // bgColor
                    int.Parse(fila[13].ToString()),     // activo
                    fila[14].ToString()                 // nombre_departamento
                );
            }

            return profesor;
        }



        public Profesor autentificarUsuario(string correo, string password)
        {           
            db = DBBroker.ObtenerAgente();
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

        
    }
}
