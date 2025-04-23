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

        public ObservableCollection<Curso> LeerCursosPorGrado(int id_grado)
        {
                  
            string query = "SELECT id_curso, id_grado, nombre, descripcion, anio_inicio, anio_fin FROM fpc.cursos WHERE id_grado = @id_grado;";
            // Parámetros para la consulta
            var parametros = new Dictionary<string, object>
            {
                { "@id_grado", id_grado }
            };
            var resultado = db.LeerConParametros(query, parametros);
            foreach (ObservableCollection<object> fila in resultado)
            {
                try
                {
                    Curso curso = new Curso(
                        int.Parse(fila[0].ToString()), // id_curso
                        int.Parse(fila[1].ToString()), // id_grado
                        fila[2].ToString().Trim(), // nombre
                        fila[3].ToString().Trim(), // descripcion
                        int.Parse(fila[4].ToString()), // anio_inicio
                        int.Parse(fila[5].ToString())  // anio_fin
                    );
                    listaCursos.Add(curso);
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
