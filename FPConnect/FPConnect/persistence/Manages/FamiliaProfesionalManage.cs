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
            FamiliaProfesional familia = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerConParametros("SELECT id_familia,id_centro,nombre FROM fpc.familiasprofesionales WHERE id_centro = @id_centro;", new Dictionary<string, object> { { "@id_centro", id_centro } });
            foreach (ObservableCollection<Object> c in resultado)
            {
                familia = new FamiliaProfesional(
                    int.Parse(c[0].ToString()), //id
                    int.Parse(c[1].ToString()), //id_centro
                    c[2].ToString()); //nombre
                this.listaFamilias.Add(familia);
            }
            return listaFamilias;
        }

        public void InsertarFamilia(FamiliaProfesional familia)
        {
            db = DBBroker.ObtenerAgente();
            string query = "INSERT INTO fpc.familiasprofesionales (id_centro,nombre) VALUES (@id_centro,@nombre);";
            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", familia.id_centro },
                { "@nombre", familia.nombre }
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

        public void EliminarFamilia(int id_familia)
        {
            db = DBBroker.ObtenerAgente();
            string query = "DELETE FROM fpc.familiasprofesionales WHERE id_familia = @id_familia;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_familia", id_familia }
            };
            db.Modificar(query, parametros);
        }


    }
}
