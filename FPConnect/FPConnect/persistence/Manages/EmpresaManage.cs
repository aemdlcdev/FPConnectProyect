using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FPConnect.domain;
using FPConnect.persistence;

namespace FPConnect.persistence.Manages
{
    public class EmpresaManage
    {
        private DBBroker db;

        // Constructor
        public EmpresaManage()
        {
            db = DBBroker.ObtenerAgente();
        }

        #region Operaciones CRUD básicas

        // Insertar empresa
        public bool InsertarEmpresa(Empresa empresa)
        {
            try
            {
                string query = @"INSERT INTO fpc.empresas 
                               (id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo) 
                               VALUES 
                               (@id_centro, @nombre, @email, @telefono, @anio_inicio_acuerdo, @anio_fin_acuerdo, @activo);";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", empresa.id_centro },
                    { "@nombre", empresa.nombre },
                    { "@email", empresa.email },
                    { "@telefono", empresa.telefono },
                    { "@anio_inicio_acuerdo", empresa.anio_inicio_acuerdo },
                    { "@anio_fin_acuerdo", empresa.anio_fin_acuerdo },
                    { "@activo", empresa.estado }  // En la BD es "activo", en la clase es "estado"
                };

                int resultado = db.Modificar(query, parametros);

                if (resultado > 0)
                {
                    // Usar el método específico para obtener el último ID
                    empresa.id_empresa = db.LeerUltimoIdInsertado();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar empresa: {ex.Message}");
                return false;
            }
        }

        // Insertar empresa con asociación a perfiles por familia y grado
        public bool InsertarEmpresa(Empresa empresa, int id_familia, int id_grado)
        {
            try
            {
                // Paso 1: Insertar la empresa básica
                string query = @"INSERT INTO fpc.empresas 
                               (id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo) 
                               VALUES 
                               (@id_centro, @nombre, @email, @telefono, @anio_inicio_acuerdo, @anio_fin_acuerdo, @activo);";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", empresa.id_centro },
                    { "@nombre", empresa.nombre },
                    { "@email", empresa.email },
                    { "@telefono", empresa.telefono },
                    { "@anio_inicio_acuerdo", empresa.anio_inicio_acuerdo },
                    { "@anio_fin_acuerdo", empresa.anio_fin_acuerdo },
                    { "@activo", empresa.estado }
                };

                int resultado = db.Modificar(query, parametros);

                if (resultado <= 0)
                    return false;

                // Paso 2: Obtener el ID de la empresa recién insertada usando el método específico
                empresa.id_empresa = db.LeerUltimoIdInsertado();

                // Paso 3: Buscar perfiles que coincidan con la familia y grado
                return AsociarEmpresaConPerfilesDeFamiliaYGrado(empresa.id_empresa, id_familia, id_grado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar empresa con asociación a perfiles: {ex.Message}");
                return false;
            }
        }

        // Método auxiliar para asociar una empresa con perfiles por familia y grado
        private bool AsociarEmpresaConPerfilesDeFamiliaYGrado(int id_empresa, int id_familia, int id_grado)
        {
            try
            {
                // Buscar todos los perfiles que coinciden con la familia y grado
                string query = "SELECT id_perfil FROM fpc.perfiles WHERE id_familia = @id_familia AND id_grado = @id_grado AND activo = 1;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia },
                    { "@id_grado", id_grado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                if (resultado.Count == 0)
                {
                    Console.WriteLine("No se encontraron perfiles activos para la familia y grado especificados");
                    return false;
                }

                // Asociar la empresa con cada perfil encontrado
                bool exito = true;
                foreach (ObservableCollection<object> fila in resultado)
                {
                    int id_perfil = int.Parse(fila[0].ToString());
                    bool asociacionExitosa = AsociarEmpresaPerfil(id_empresa, id_perfil);

                    if (!asociacionExitosa)
                    {
                        Console.WriteLine($"Error al asociar empresa {id_empresa} con perfil {id_perfil}");
                        exito = false;
                    }
                }

                return exito;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al asociar empresa con perfiles: {ex.Message}");
                return false;
            }
        }

        // El resto de los métodos permanecen igual
        // ... (actualizar, eliminar, desactivar, leer, etc.)

        // Actualizar empresa
        public bool ActualizarEmpresa(Empresa empresa)
        {
            try
            {
                string query = @"UPDATE fpc.empresas 
                               SET nombre = @nombre, 
                                   id_centro = @id_centro,
                                   email = @email, 
                                   telefono = @telefono, 
                                   anio_inicio_acuerdo = @anio_inicio_acuerdo, 
                                   anio_fin_acuerdo = @anio_fin_acuerdo, 
                                   activo = @activo
                               WHERE id_empresa = @id_empresa;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", empresa.id_empresa },
                    { "@id_centro", empresa.id_centro },
                    { "@nombre", empresa.nombre },
                    { "@email", empresa.email },
                    { "@telefono", empresa.telefono },
                    { "@anio_inicio_acuerdo", empresa.anio_inicio_acuerdo },
                    { "@anio_fin_acuerdo", empresa.anio_fin_acuerdo },
                    { "@activo", empresa.estado }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar empresa: {ex.Message}");
                return false;
            }
        }

        // Eliminación física
        public bool EliminarEmpresa(int id_empresa)
        {
            try
            {
                // Primero eliminar las relaciones en EmpresasPerfiles
                string queryRelaciones = "DELETE FROM fpc.empresasperfiles WHERE id_empresa = @id_empresa;";
                var parametrosRelaciones = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };
                db.Modificar(queryRelaciones, parametrosRelaciones);

                // También eliminar las relaciones en EmpresasFamilias si existe
                string queryRelacionesFamilias = "DELETE FROM fpc.empresasfamilias WHERE id_empresa = @id_empresa;";
                db.Modificar(queryRelacionesFamilias, parametrosRelaciones);

                // Luego eliminar la empresa
                string query = "DELETE FROM fpc.empresas WHERE id_empresa = @id_empresa;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar empresa: {ex.Message}");
                return false;
            }
        }

        // Eliminación lógica (desactivar)
        public bool DesactivarEmpresa(int id_empresa)
        {
            try
            {
                string query = "UPDATE fpc.empresas SET activo = 2 WHERE id_empresa = @id_empresa;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desactivar empresa: {ex.Message}");
                return false;
            }
        }

        // Leer todas las empresas
        public ObservableCollection<Empresa> LeerEmpresas()
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = "SELECT id_empresa, id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo FROM fpc.empresas;";
                var resultado = db.LeerSinParametros(query);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresa por ID
        public Empresa LeerEmpresaPorId(int id_empresa)
        {
            try
            {
                string query = "SELECT id_empresa, id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo FROM fpc.empresas WHERE id_empresa = @id_empresa;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };

                var resultado = db.LeerConParametros(query, parametros);

                if (resultado.Count > 0)
                {
                    var fila = resultado[0] as ObservableCollection<object>;

                    return new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresa por ID: {ex.Message}");
            }

            return null;
        }

        // Leer empresas por centro
        public ObservableCollection<Empresa> LeerEmpresasPorCentro(int id_centro)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = "SELECT id_empresa, id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo FROM fpc.empresas WHERE id_centro = @id_centro;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", id_centro }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por centro: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresas activas por centro
        public ObservableCollection<Empresa> LeerEmpresasActivasPorCentro(int id_centro)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = "SELECT id_empresa, id_centro, nombre, email, telefono, anio_inicio_acuerdo, anio_fin_acuerdo, activo FROM fpc.empresas WHERE id_centro = @id_centro AND activo = 1;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", id_centro }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas activas por centro: {ex.Message}");
            }

            return empresas;
        }

        #endregion

        #region Operaciones para EmpresasPerfiles

        // Asociar una empresa a un perfil
        public bool AsociarEmpresaPerfil(int id_empresa, int id_perfil)
        {
            try
            {
                // Verificar si la relación ya existe
                string queryExistente = "SELECT EXISTS(SELECT 1 FROM fpc.empresasperfiles WHERE id_empresa = @id_empresa AND id_perfil = @id_perfil) as existe;";
                var parametrosExistente = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa },
                    { "@id_perfil", id_perfil }
                };

                var resultadoExistente = db.LeerConParametros(queryExistente, parametrosExistente);

                if (resultadoExistente.Count > 0 && resultadoExistente[0] is ObservableCollection<object> fila && fila.Count > 0)
                {
                    
                    int existeInt = Convert.ToInt32(fila[0]);
                    bool existe = (existeInt == 1);

                    if (existe)
                        return true; // La relación ya existe
                }

                // Crear la nueva relación
                string query = "INSERT INTO fpc.empresasperfiles (id_empresa, id_perfil) VALUES (@id_empresa, @id_perfil);";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa },
                    { "@id_perfil", id_perfil }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al asociar empresa y perfil: {ex.Message}");
                return false;
            }
        }


        // Desasociar una empresa de un perfil
        public bool DesasociarEmpresaPerfil(int id_empresa, int id_perfil)
        {
            try
            {
                string query = "DELETE FROM fpc.empresasperfiles WHERE id_empresa = @id_empresa AND id_perfil = @id_perfil;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa },
                    { "@id_perfil", id_perfil }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desasociar empresa y perfil: {ex.Message}");
                return false;
            }
        }

        // Leer perfiles asociados a una empresa
        public ObservableCollection<int> LeerPerfilesDeLaEmpresa(int id_empresa)
        {
            ObservableCollection<int> idsPerfiles = new ObservableCollection<int>();

            try
            {
                string query = "SELECT id_perfil FROM fpc.empresasperfiles WHERE id_empresa = @id_empresa;";
                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    idsPerfiles.Add(int.Parse(fila[0].ToString()));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer perfiles de la empresa: {ex.Message}");
            }

            return idsPerfiles;
        }

        // Leer empresas por perfil
        public ObservableCollection<Empresa> LeerEmpresasPorPerfil(int id_perfil)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = @"SELECT e.id_empresa, e.id_centro, e.nombre, e.email, e.telefono, 
                                e.anio_inicio_acuerdo, e.anio_fin_acuerdo, e.activo
                                FROM fpc.empresas e
                                INNER JOIN fpc.empresasperfiles ep ON e.id_empresa = ep.id_empresa
                                WHERE ep.id_perfil = @id_perfil AND e.activo = 1
                                ORDER BY e.nombre;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_perfil", id_perfil }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por perfil: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresas por familia profesional
        public ObservableCollection<Empresa> LeerEmpresasPorFamilia(int id_familia)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = @"SELECT DISTINCT e.id_empresa, e.id_centro, e.nombre, e.email, e.telefono, 
                                e.anio_inicio_acuerdo, e.anio_fin_acuerdo, e.activo
                                FROM fpc.empresas e
                                INNER JOIN fpc.empresasperfiles ep ON e.id_empresa = ep.id_empresa
                                INNER JOIN fpc.perfiles p ON ep.id_perfil = p.id_perfil
                                WHERE p.id_familia = @id_familia AND e.activo = 1
                                ORDER BY e.nombre;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por familia: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresas por grado
        public ObservableCollection<Empresa> LeerEmpresasPorGrado(int id_grado)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = @"SELECT DISTINCT e.id_empresa, e.id_centro, e.nombre, e.email, e.telefono, 
                                e.anio_inicio_acuerdo, e.anio_fin_acuerdo, e.activo
                                FROM fpc.empresas e
                                INNER JOIN fpc.empresasperfiles ep ON e.id_empresa = ep.id_empresa
                                INNER JOIN fpc.perfiles p ON ep.id_perfil = p.id_perfil
                                WHERE p.id_grado = @id_grado AND e.activo = 1
                                ORDER BY e.nombre;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_grado", id_grado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por grado: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresas por familia profesional y grado
        public ObservableCollection<Empresa> LeerEmpresasPorFamiliaYGrado(int id_familia, int id_grado)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = @"SELECT DISTINCT e.id_empresa, e.id_centro, e.nombre, e.email, e.telefono, 
                                e.anio_inicio_acuerdo, e.anio_fin_acuerdo, e.activo
                                FROM fpc.empresas e
                                INNER JOIN fpc.empresasperfiles ep ON e.id_empresa = ep.id_empresa
                                INNER JOIN fpc.perfiles p ON ep.id_perfil = p.id_perfil
                                WHERE p.id_familia = @id_familia 
                                  AND p.id_grado = @id_grado
                                  AND e.activo = 1
                                ORDER BY e.nombre;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia },
                    { "@id_grado", id_grado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por familia y grado: {ex.Message}");
            }

            return empresas;
        }

        // Leer empresas por centro, familia profesional y grado
        public ObservableCollection<Empresa> LeerEmpresasPorCentroFamiliaYGrado(int id_centro, int id_familia, int id_grado)
        {
            ObservableCollection<Empresa> empresas = new ObservableCollection<Empresa>();

            try
            {
                string query = @"SELECT DISTINCT e.id_empresa, e.id_centro, e.nombre, e.email, e.telefono, 
                        e.anio_inicio_acuerdo, e.anio_fin_acuerdo, e.activo
                        FROM fpc.empresas e
                        INNER JOIN fpc.empresasperfiles ep ON e.id_empresa = ep.id_empresa
                        INNER JOIN fpc.perfiles p ON ep.id_perfil = p.id_perfil
                        WHERE p.id_familia = @id_familia 
                          AND p.id_grado = @id_grado
                          AND e.id_centro = @id_centro
                          AND e.activo = 1
                        ORDER BY e.nombre;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", id_centro },
                    { "@id_familia", id_familia },
                    { "@id_grado", id_grado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    Empresa empresa = new Empresa(
                        int.Parse(fila[0].ToString()),      // id_empresa
                        int.Parse(fila[1].ToString()),      // id_centro
                        fila[2].ToString(),                 // nombre
                        fila[3].ToString(),                 // email
                        fila[4].ToString(),                 // telefono
                        int.Parse(fila[5].ToString()),      // anio_inicio_acuerdo
                        fila[6] != DBNull.Value ? int.Parse(fila[6].ToString()) : 0, // anio_fin_acuerdo
                        int.Parse(fila[7].ToString())       // activo (estado)
                    );

                    empresas.Add(empresa);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer empresas por centro, familia y grado: {ex.Message}");
            }

            return empresas;
        }

        #endregion
    }
}
