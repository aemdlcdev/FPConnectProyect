using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace FPConnect.persistence
{
    public class DBBroker
    {
        private static DBBroker _instancia;
        private static MySqlConnection conexion;
        private const string cadenaConexion = "server=localhost;database=fpc;uid=root;pwd=toor";

        private DBBroker()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        public static DBBroker ObtenerAgente()
        {
            if (_instancia == null)
            {
                _instancia = new DBBroker();
            }
            return _instancia;
        }

        // Leer sin parametros
        public ObservableCollection<object> LeerSinParametros(string sql)
        {
            ObservableCollection<object> resultado = new ObservableCollection<object>();
            conectar();

            using (MySqlCommand com = new MySqlCommand(sql, conexion))
            {
                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObservableCollection<object> fila = new ObservableCollection<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fila.Add(reader[i].ToString());
                        }
                        resultado.Add(fila);
                    }
                }
            }
            desconectar();
            return resultado;
        }


        // Leer con parametros
        public ObservableCollection<object> LeerConParametros(string sql, Dictionary<string, object> parametros)
        {
            ObservableCollection<object> resultado = new ObservableCollection<object>();
            conectar();

            using (MySqlCommand com = new MySqlCommand(sql, conexion))
            {
                
                foreach (var param in parametros)
                {
                    com.Parameters.AddWithValue(param.Key, param.Value);
                }

                using (MySqlDataReader reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ObservableCollection<object> fila = new ObservableCollection<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            fila.Add(reader[i].ToString());
                        }
                        resultado.Add(fila);
                    }
                }
            }
            desconectar();
            return resultado;
        }

        // Modificar (insertar, modificar, borrar) con parametros
        public int Modificar(string sql, Dictionary<string, object> parametros)
        {
            int resultado;
            conectar();

            using (MySqlCommand com = new MySqlCommand(sql, conexion))
            {
                // Agrega parámetros
                foreach (var param in parametros)
                {
                    com.Parameters.AddWithValue(param.Key, param.Value);
                }

                resultado = com.ExecuteNonQuery();
            }

            desconectar();
            return resultado;
        }

        private void conectar()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
        }

        private void desconectar()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
    }
}
