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
        public int id_profesor { get; set; }
        public int id_rol { get; set; }
        public int id_centro { get; set; }
        public int id_familia { get; set; }
        public int id_turno { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string sexo { get; set; }
        public string character { get; set; }
        public Brush bgColor { get; set; }
        public int id_curso { get; set; } 
        public int activo { get; set; } // 1 = activo, 0 = inactivo
        public string nombre_departamento { get; set; } // Nombre del departamento al que pertenece el profesor



        private ProfesorManage um;

        public Profesor() { um = new ProfesorManage(); }

        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia, int id_curso, int id_turno, string nombre, string apellidos, string email, string password, string sexo, string character, string bgColor, int activo, string nombre_departamento)
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

        public Profesor(int id_rol, int id_centro, int id_familia, int id_turno, string nombre, string apellidos, string email, string password, string sexo, string charcter, string bgColor)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = charcter;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            um = new ProfesorManage();
        }

        public Profesor( int id_rol, int id_centro, int id_familia, int id_turno, string nombre, string apellidos, string email, string password, string sexo, string charcter, string bgColor, int activo)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = charcter;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            um = new ProfesorManage();
            
        }
        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia,  int id_turno ,string nombre, string apellidos, string email, string password, string sexo, string character, string bgColor)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.id_turno = id_turno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.character = character;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            um = new ProfesorManage();
        }
        public Profesor (int id_profesor,int id_rol,int id_centro,int id_familia, int id_turno, string nombre,string apellidos, string email, string password, string sexo, string character, string bgColor, int activo)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
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

        public Profesor(int id_profesor, int id_rol, int id_centro, int id_familia, int id_turno, string nombre, string apellidos, string email, string password, string sexo, string character, string bgColor, int activo,string nombre_departamento)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
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

        public ObservableCollection<Profesor> LeerProfesoresPorCentro(int id_centro) 
        {
            return um.LeerProfesoresPorCentro(id_centro);
        }

        public Profesor autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

        public void InsertarProfesor(Profesor profesor, int id_grado, int id_curso, int id_perfil)
        {
            um.InsertarProfesor(profesor, id_grado,id_curso,id_perfil);
        }

        public void ModificarProfesor(Profesor profesor, int[] grados)
        {
            um.ModificarProfesor(profesor, grados);
        }

        public void EliminarProfesor(int id_profesor)
        {
            um.BorrarProfesor(id_profesor);
        }

    }
}
