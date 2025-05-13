using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class FamiliaProfesionalManage
    {
        private ObservableCollection<FamiliaProfesional> listaFamilias { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public FamiliaProfesionalManage()
        {
            listaFamilias = new ObservableCollection<FamiliaProfesional>();
        }

        public ObservableCollection<FamiliaProfesional> LeerFamiliasCentro(int id_centro)
        {
            ObservableCollection<FamiliaProfesional> listaFamilias = new ObservableCollection<FamiliaProfesional>();
            FamiliaProfesional familia = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerConParametros(
                "SELECT id_familia, id_centro, nombre, activo FROM fpc.familiasprofesionales WHERE id_centro = @id_centro AND activo = 1;",
                new Dictionary<string, object> { { "@id_centro", id_centro } }
            );

            foreach (ObservableCollection<Object> c in resultado)
            {
                familia = new FamiliaProfesional(
                    int.Parse(c[0].ToString()),    // id_familia
                    int.Parse(c[1].ToString()),    // id_centro
                    c[2].ToString(),               // nombre
                    int.Parse(c[3].ToString())     // activo
                );

                listaFamilias.Add(familia);
            }

            return listaFamilias;
        }

        public ObservableCollection<FamiliaProfesional> LeerFamiliasPorCentroYGrado(int id_centro, int id_grado)
        {
            ObservableCollection<FamiliaProfesional> listaFamilias = new ObservableCollection<FamiliaProfesional>();
            db = DBBroker.ObtenerAgente();

            string query = @"
            SELECT DISTINCT f.id_familia, f.nombre, f.id_centro
            FROM FamiliasProfesionales f
            INNER JOIN Perfiles p ON f.id_familia = p.id_familia
            INNER JOIN Grados g ON p.id_grado = g.id_grado
            WHERE f.id_centro = @id_centro
            AND g.id_grado = @id_grado
            AND g.id_centro = f.id_centro
            ORDER BY f.nombre";

            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro },
                { "@id_grado", id_grado }
            };

            var resultado = db.LeerConParametros(query, parametros);
            foreach (ObservableCollection<object> fila in resultado)
            {
                try
                {
                    FamiliaProfesional familia = new FamiliaProfesional(
                        int.Parse(fila[0].ToString()),  // id_familia
                        int.Parse(fila[1].ToString()),   // id_centro
                        fila[2].ToString().Trim(), // nombre
                        int.Parse(fila[3].ToString()) // activo
                    );
                    Console.WriteLine(familia);
                    if(familia.activo == 1) // 1 quiere decir que esta activa
                    listaFamilias.Add(familia);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar la familia profesional: " + ex.Message);
                }
            }

            return listaFamilias;
        }

        public void InsertarFamilia(FamiliaProfesional familia)
        {
            db = DBBroker.ObtenerAgente();
            string query = "INSERT INTO fpc.familiasprofesionales (id_centro,nombre,activo) VALUES (@id_centro,@nombre,@activo);";
            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", familia.id_centro },
                { "@nombre", familia.nombre },
                { "@activo", 1 } // Por defecto 1 que quiere decir que esta activa
            };
            db.Modificar(query, parametros);
        }

        public void ModificarFamilia(FamiliaProfesional familia)
        {
            db = DBBroker.ObtenerAgente();
            string query = "UPDATE fpc.familiasprofesionales SET id_centro = @id_centro, nombre = @nombre WHERE id_familia = @id_familia;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_familia", familia.id_familia },
                { "@id_centro", familia.id_centro },
                { "@nombre", familia.nombre }
            };
            db.Modificar(query, parametros);
        }

        public void EliminadoLogicoFamilia(int id_familia)
        {
            db = DBBroker.ObtenerAgente();
            string query = "UPDATE fpc.familiasprofesionales SET activo = 2 WHERE id_familia = @id_familia;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_familia", id_familia }
            };
            db.Modificar(query, parametros);
        }
    }
}
