using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class Empresa
    {
        // Propiedades
        public int id_empresa { get; set; }
        public int id_centro { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public int anio_inicio_acuerdo { get; set; }
        public int anio_fin_acuerdo { get; set; }
        public int estado { get; set; } // 1 activa, 2 inactiva

        // Gestor para operaciones de base de datos
        private EmpresaManage em;

        #region Constructores

        // Constructor vacío
        public Empresa()
        {
            em = new EmpresaManage();
            estado = 1; // Por defecto activa
        }

        // Constructor completo con ID
        public Empresa(int id_empresa, int id_centro, string nombre, string email, string telefono,
                      int anio_inicio_acuerdo, int anio_fin_acuerdo, int estado)
        {
            this.id_empresa = id_empresa;
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
            this.anio_inicio_acuerdo = anio_inicio_acuerdo;
            this.anio_fin_acuerdo = anio_fin_acuerdo;
            this.estado = estado;
            em = new EmpresaManage();
        }

        // Constructor sin ID (para nuevas inserciones)
        public Empresa(int id_centro, string nombre, string email, string telefono,
                      int anio_inicio_acuerdo, int anio_fin_acuerdo, int estado)
        {
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.email = email;
            this.telefono = telefono;
            this.anio_inicio_acuerdo = anio_inicio_acuerdo;
            this.anio_fin_acuerdo = anio_fin_acuerdo;
            this.estado = estado;
            em = new EmpresaManage();
        }

        #endregion

        #region Métodos de Persistencia Básicos

        // Insertar empresa
        public bool Insertar()
        {
            return em.InsertarEmpresa(this);
        }

        // Insertar con asociación automática a perfiles por familia y grado
        public bool Insertar(int id_familia, int id_grado)
        {
            return em.InsertarEmpresa(this, id_familia, id_grado);
        }

        // Actualizar empresa
        public bool Actualizar()
        {
            return em.ActualizarEmpresa(this);
        }

        // Eliminar empresa físicamente (y sus relaciones)
        public bool Eliminar()
        {
            return em.EliminarEmpresa(this.id_empresa);
        }

        // Desactivar empresa (eliminación lógica)
        public bool Desactivar()
        {
            estado = 0;
            return em.DesactivarEmpresa(this.id_empresa);
        }

        #endregion

        #region Métodos para Gestión de Perfiles

        // Asociar esta empresa con un perfil específico
        public bool AsociarPerfil(int id_perfil)
        {
            return em.AsociarEmpresaPerfil(this.id_empresa, id_perfil);
        }

        // Desasociar esta empresa de un perfil específico
        public bool DesasociarPerfil(int id_perfil)
        {
            return em.DesasociarEmpresaPerfil(this.id_empresa, id_perfil);
        }

        // Obtener los IDs de perfiles asociados a esta empresa
        public ObservableCollection<int> ObtenerPerfiles()
        {
            return em.LeerPerfilesDeLaEmpresa(this.id_empresa);
        }

        #endregion

        #region Métodos Estáticos para Consultas

        // Obtener todas las empresas
        public static ObservableCollection<Empresa> ObtenerTodas()
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresas();
        }

        // Obtener empresas por centro
        public static ObservableCollection<Empresa> ObtenerPorCentro(int id_centro)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasPorCentro(id_centro);
        }

        // Obtener empresas activas por centro
        public static ObservableCollection<Empresa> ObtenerActivasPorCentro(int id_centro)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasActivasPorCentro(id_centro);
        }

        // Obtener empresas por perfil
        public static ObservableCollection<Empresa> ObtenerPorPerfil(int id_perfil)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasPorPerfil(id_perfil);
        }

        // Obtener empresas por familia profesional
        public static ObservableCollection<Empresa> ObtenerPorFamilia(int id_familia)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasPorFamilia(id_familia);
        }

        // Obtener empresas por grado
        public static ObservableCollection<Empresa> ObtenerPorGrado(int id_grado)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasPorGrado(id_grado);
        }

        // Obtener empresas por familia y grado
        public static ObservableCollection<Empresa> ObtenerPorFamiliaYGrado(int id_familia, int id_grado)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresasPorFamiliaYGrado(id_familia, id_grado);
        }

        // Obtener empresa por ID
        public static Empresa ObtenerPorId(int id_empresa)
        {
            EmpresaManage em = new EmpresaManage();
            return em.LeerEmpresaPorId(id_empresa);
        }

        #endregion

        // Sobrescribir ToString para mostrar en listas y ComboBox
        public override string ToString()
        {
            return this.nombre;
        }
    }
}
