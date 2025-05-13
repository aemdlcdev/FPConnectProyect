using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;
using FPConnect.view.Pages;

namespace FPConnect.persistence.Manages
{
    internal class PerfilManage
    {
        private ObservableCollection<Perfil> listaPerfiles { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public PerfilManage()
        {
            listaPerfiles = new ObservableCollection<Perfil>();
        }

        public ObservableCollection<Perfil> LeerPerfilesPorCentro(int id_centro)
        {
            ObservableCollection<Perfil> listaPerfiles = new ObservableCollection<Perfil>();
            Perfil perfil = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerConParametros(
                @"SELECT p.id_perfil, p.id_familia, p.id_grado, p.nombre, 
                 f.nombre AS nombre_familia, g.nombre AS nombre_grado, p.activo
                  FROM fpc.perfiles p
                  INNER JOIN fpc.grados g ON p.id_grado = g.id_grado
                  INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                  WHERE g.id_centro = @id_centro AND f.id_centro = @id_centro AND p.activo = 1
                  ORDER BY f.nombre, p.nombre;",

                new Dictionary<string, object> { { "@id_centro", id_centro } }
            );

            foreach (ObservableCollection<object> c in resultado)
            {
                perfil = new Perfil(
                    int.Parse(c[0].ToString()),   // id_perfil
                    int.Parse(c[1].ToString()),   // id_familia
                    int.Parse(c[2].ToString()),   // id_grado
                    c[3].ToString(),              // nombre
                    c[4].ToString(),              // nombre_familia
                    c[5].ToString(),              // nombre_grado
                    int.Parse(c[6].ToString())    // activo 
                );

                listaPerfiles.Add(perfil);
                Console.WriteLine("Perfil: " + perfil.ToString());
            }

            return listaPerfiles;
        }

        public ObservableCollection<Perfil> LeerPerfilesFiltrados(int id_centro, int id_grado, int id_familia)
        {
            ObservableCollection<Perfil> listaPerfiles = new ObservableCollection<Perfil>();
            Perfil perfil = null;
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT p.id_perfil, p.id_familia, p.id_grado, p.nombre 
                       FROM fpc.perfiles p
                       INNER JOIN fpc.familiasProfesionales f ON p.id_familia = f.id_familia
                       INNER JOIN fpc.grados g ON p.id_grado = g.id_grado
                       WHERE f.id_centro = @id_centro 
                       AND g.id_centro = @id_centro
                       AND p.id_grado = @id_grado
                       AND p.id_familia = @id_familia;";

            var resultado = db.LeerConParametros(consulta, new Dictionary<string, object>
            {
                { "@id_centro", id_centro },
                { "@id_grado", id_grado },
                { "@id_familia", id_familia }
            });

            foreach (ObservableCollection<Object> c in resultado)
            {
                perfil = new Perfil(
                    int.Parse(c[0].ToString()), //id_perfil
                    int.Parse(c[1].ToString()), //id_familia
                    int.Parse(c[2].ToString()), //id_grado
                    c[3].ToString()); //nombre
                listaPerfiles.Add(perfil);
            }

            return listaPerfiles;
        }

        public void InsertarPerfil(Perfil perfil)
        {
            db = DBBroker.ObtenerAgente();

            // 1. Inserto el perfil
            string query = "INSERT INTO fpc.perfiles (id_familia, id_grado, nombre, activo) VALUES (@id_familia, @id_grado, @nombre, 1);";
            var parametros = new Dictionary<string, object>
            {
                { "@id_familia", perfil.id_familia },
                { "@id_grado", perfil.id_grado },
                { "@nombre", perfil.nombre }
            };
            db.Modificar(query, parametros);

            // 2. Obtengo el ID del ultiomo perfil insertado
            int idPerfil = db.LeerUltimoIdInsertado();

            // 3. Crear los cursos para primer y segundo año
            int anioActual = DateTime.Now.Year;
            int anioSiguiente = anioActual + 1;

            // 4. Insertar el curso de primer año
            string queryPrimerCurso = @"INSERT INTO fpc.cursos (id_perfil, nivel, anio_inicio, anio_fin, activo) 
                               VALUES (@id_perfil, @nivel, @anio_inicio, @anio_fin, @activo);";
            var parametrosPrimerCurso = new Dictionary<string, object>
            {
                { "@id_perfil", idPerfil },
                { "@nivel", "Primer año" },
                { "@anio_inicio", anioActual },
                { "@anio_fin", anioSiguiente },
                { "@activo", 1 }
            };
            db.Modificar(queryPrimerCurso, parametrosPrimerCurso);

            // 5. Insertar el curso de segundo año
            string querySegundoCurso = @"INSERT INTO fpc.cursos (id_perfil, nivel, anio_inicio, anio_fin, activo) 
                                VALUES (@id_perfil, @nivel, @anio_inicio, @anio_fin, @activo);";
            var parametrosSegundoCurso = new Dictionary<string, object>
            {
                { "@id_perfil", idPerfil },
                { "@nivel", "Segundo año" },
                { "@anio_inicio", anioActual },
                { "@anio_fin", anioSiguiente },
                { "@activo", 1 }
            };
            db.Modificar(querySegundoCurso, parametrosSegundoCurso);
        }

        public bool ModificarPerfil(Perfil perfil)
        {
            try
            {
                db = DBBroker.ObtenerAgente();

                string query = @"UPDATE fpc.perfiles 
                        SET id_familia = @id_familia, 
                            id_grado = @id_grado, 
                            nombre = @nombre, 
                            activo = @activo
                        WHERE id_perfil = @id_perfil;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_perfil", perfil.id_perfil },
                    { "@id_familia", perfil.id_familia },
                    { "@id_grado", perfil.id_grado },
                    { "@nombre", perfil.nombre },
                    { "@activo", perfil.activo }
                };

                int filasAfectadas = db.Modificar(query, parametros);

                // aqui verificamos q se haya actualizado correctamente
                if (filasAfectadas > 0)
                {
                    Console.WriteLine($"Perfil con ID {perfil.id_perfil} actualizado exitosamente");
                    return true;
                }
                else
                {
                    Console.WriteLine($"No se encontró el perfil con ID {perfil.id_perfil} para actualizar");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el perfil: {ex.Message}");
                return false;
            }
        }


        public void EliminadoLogicoPerfil(int id_perfil)
        {
            db = DBBroker.ObtenerAgente();

            try
            {
                // 1. Marcamos el perfil como inactivo
                string queryPerfil = "UPDATE fpc.perfiles SET activo = 2 WHERE id_perfil = @id_perfil;";
                var parametrosPerfil = new Dictionary<string, object>
                {
                    { "@id_perfil", id_perfil }
                };
                int resultadoPerfil = db.Modificar(queryPerfil, parametrosPerfil);

                if (resultadoPerfil > 0)
                {
                    Console.WriteLine($"Perfil con ID {id_perfil} marcado como inactivo.");
                }

                // 2. Marcamos todos los cursos asociados como inactivos
                string queryCursos = "UPDATE fpc.cursos SET activo = 2 WHERE id_perfil = @id_perfil;";
                var parametrosCursos = new Dictionary<string, object>
                {
                    { "@id_perfil", id_perfil }
                };
                int resultadoCursos = db.Modificar(queryCursos, parametrosCursos);

                Console.WriteLine($"Se han marcado {resultadoCursos} cursos como inactivos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar el borrado lógico: {ex.Message}");
                throw; // relanzo la excepcion por si tengo q manejarla en un nivel superior
            }
        }

    }
}
