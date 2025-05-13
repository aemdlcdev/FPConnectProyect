using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class CursosManage
    {
        private ObservableCollection<Curso> listaCursos { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public CursosManage()
        {
            listaCursos = new ObservableCollection<Curso>();
        }

        public ObservableCollection<Curso> LeerCursosFiltrado(int id_centro, int id_perfil, int id_familia)
        {
            ObservableCollection<Curso> listaCursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            string query = @"
            SELECT c.id_curso, c.id_perfil, c.nombre, c.descripcion, c.anio_inicio, c.anio_fin
            FROM Cursos c
            INNER JOIN Perfiles p ON c.id_perfil = p.id_perfil
            INNER JOIN Grados g ON p.id_grado = g.id_grado
            INNER JOIN FamiliasProfesionales f ON p.id_familia = f.id_familia
            WHERE g.id_centro = @id_centro
            AND f.id_centro = @id_centro
            AND p.id_perfil = @id_perfil
            AND f.id_familia = @id_familia
            ORDER BY c.nombre";

            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro },
                { "@id_perfil", id_perfil },
                { "@id_familia", id_familia }
            };

            var resultado = db.LeerConParametros(query, parametros);
            foreach (ObservableCollection<object> fila in resultado)
            {
                try
                {
                    Curso curso = new Curso(
                        int.Parse(fila[0].ToString()),  // id_curso 
                        int.Parse(fila[1].ToString()),      // id_perfil
                        fila[2].ToString().Trim(),      // nivel
                        int.Parse(fila[4].ToString()),  // anio_inicio
                        int.Parse(fila[5].ToString()), // anio_fin
                        int.Parse(fila[6].ToString()) // activo
                    );
                    if (curso.activo == 1) // 1 quiere decir que esta activa
                    this.listaCursos.Add(curso);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar el curso: " + ex.Message);
                }
            }

            return listaCursos;
        }
    }
}
