using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class Profesor
    {
        // Propiedades
        public int id_profesor { get; set; }
        public int id_rol { get; set; }
        public int id_centro { get; set; }
        public int id_familia { get; set; }
        public int id_curso { get; set; }
        public int id_turno { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string sexo { get; set; }
        public string character { get; set; }
        public Brush bgColor { get; set; }
        public int activo { get; set; } // 1 = activo, 0 = inactivo
        public string nombre_departamento { get; set; } // Campo calculado (no está en la BD) lo obtengo haciendo un join en fmailias
        public int id_grado { get; set; }
        public string nombre_grado { get; set; }
        private ProfesorManage um;

        // Constructor por defecto
        public Profesor()
        {
            um = new ProfesorManage();
            activo = 1; // Por defecto, activo
        }

        // Constructor para CREATE (sin id_profesor que es generado por la BD ya q lo he puesto autoincremental)
        public Profesor(int id_rol, int id_centro, int id_familia, int id_curso,
                       int id_turno, string nombre, string apellidos, string email,
                       string password, string sexo, string character, string bgColor)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = 1; // Por defecto, activo
            um = new ProfesorManage();
        }

        // Constructor para CREATE con estado activo específico
        public Profesor(int id_rol, int id_centro, int id_familia, int id_curso,
                       int id_turno, string nombre, string apellidos, string email,
                       string password, string sexo, string character, string bgColor,
                       int activo)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            um = new ProfesorManage();
        }

        // Constructor para READ (con id_profesor)
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,
                       int id_curso, int id_turno, string nombre, string apellidos,
                       string email, string password, string sexo, string character,
                       string bgColor)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = 1;
            um = new ProfesorManage();
        }

        // Constructor para READ con estado activo
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,
                       int id_curso, int id_turno, string nombre, string apellidos,
                       string email, string password, string sexo, string character,
                       string bgColor, int activo)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            um = new ProfesorManage();
        }

        // Constructor completo para READ con nombre de departamento (JOIN con FamiliasProfesionales)
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,
                       int id_curso, int id_turno, string nombre, string apellidos,
                       string email, string password, string sexo, string character,
                       string bgColor, int activo, string nombre_departamento)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.nombre_departamento = nombre_departamento;
            um = new ProfesorManage();
        }

        // Constructor completo para READ con nombre de departamento y grado
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,
                       int id_curso, int id_turno, string nombre, string apellidos,
                       string email, string password, string sexo, string character,
                       string bgColor, int activo, string nombre_departamento,
                       int id_grado, string nombre_grado)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.nombre_departamento = nombre_departamento;
            this.id_grado = id_grado;
            this.nombre_grado = nombre_grado;
            um = new ProfesorManage();
        }

        // Constructor simple para autentificación (usado principalmente tras el login)
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,
                       int id_curso, string nombre, string apellidos, string email,
                       string password, string sexo, int id_grado, string nombre_grado)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_curso = id_curso;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.id_grado = id_grado;
            this.nombre_grado = nombre_grado;
            this.activo = 1; // Por defecto activo
            um = new ProfesorManage();
        }


        // Métodos CRUD
        public bool Insertar(Profesor profesor)
        {
            return um.InsertarProfesor(profesor);
        }

        public bool Actualizar(Profesor profesor)
        {
            return um.ActualizarProfesor(profesor);
        }

        public bool DesactivarProfesor(int id_profesor)
        {
            return um.EliminadoLogicoProfesor(id_profesor);
        }

        public ObservableCollection<Profesor> LeerTodos()
        {
            return um.LeerProfesores();
        }

        public ObservableCollection<Profesor> LeerPorCentro(int id_centro)
        {
            return um.LeerProfesoresPorCentro(id_centro);
        }

        public Profesor LeerPorId(int id_profesor)
        {
            return um.LeerProfesorPorId(id_profesor);
        }

        // Método ToString para mostrar información del profesor
        public override string ToString()
        {
            return $"{nombre} {apellidos} - {nombre_departamento}";
        }

        // Método para obtener el nombre completo del profesor (creo q lo voy a necesitar)
        public string NombreCompleto()
        {
            return $"{nombre} {apellidos}";
        }

        public Profesor autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

        

    }
}
